using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PatternDiscovery
{
    public partial class Form1 : Form
    {
        private const char Delimiter = ',';
        private const string Filedirectory = "C:/Dropbox/documents/_Финансовые рынки/_PatternDiscover/History/RIRTSH5/";
        private Dictionary<string, Results> stat;
        private List <PriceTime> pricelist;
        private List<PriceTime> pricelist1;
        private List<PriceTime> pricelist2;
        private List<PriceTime> pricelist3;
        private int NumberPointPrice=3;
        private int NumberPointVol=3;

        public Form1()
        {
            InitializeComponent();
        }

        internal class PriceTime
        {
            public DateTime Date { get; set; }
            public double Price  { get; set; }
            public double Volume { get; set; }
        }

        internal class Itemresult
        {
            public double profitpoints { get; set; }
            public int numberofiteration { get; set; }
        }

        internal class Coderesult
        {
            public string Code { get; set; }
            public Dictionary<int,int> Levelresults { get; set; }
        }

        private static void SaveDBChanges(dataEntities db)
        {
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException er)
            {
                foreach (var eve in er.EntityValidationErrors)
                {
                    Console.WriteLine(
                        "Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                          ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        private List<PriceTime> Uploaddatafromfile()
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                var Pricelist = new List<PriceTime>();
                foreach (string oFilename in openFileDialog1.FileNames)
                {
                    DateTime TimeStartConvert = DateTime.Now;
                    LogTextBox.AppendText("\r\n" + TimeStartConvert.ToLongTimeString() + ": " + "start " + oFilename +
                                          " parsing");

                    var reader = new StreamReader(oFilename);
                    var lineFromFile = reader.ReadLine();
                    string[] rowstring;
                    if (lineFromFile != null)
                    {
                        rowstring = lineFromFile.Split(Delimiter);
                    }
                    var i = 0;
                    while (!reader.EndOfStream)
                    {
                        lineFromFile = reader.ReadLine();
                        rowstring = lineFromFile.Split(Delimiter);
                        var time = DateTime.ParseExact(rowstring[2], "HH:mm:ss.FFF", CultureInfo.CurrentCulture);
                        var ts = new TimeSpan(time.Hour, time.Minute, time.Second);
                        var datefromrow =
                            DateTime.ParseExact(rowstring[1], "yyyyMMdd", CultureInfo.CurrentCulture).Date + ts;
                        Pricelist.Add(new PriceTime { Price = Convert.ToDouble(rowstring[6]), Date = datefromrow, Volume = Convert.ToDouble(rowstring[5]) });
                        i++;
                    }
                    DateTime TimeEndConvert = DateTime.Now;
                    LogTextBox.AppendText("\r\n" + TimeEndConvert.ToLongTimeString() + ": " + "End. Total items: " +i.ToString());
                }
                return Pricelist;
            }
            else
            {
                return null;
            }
        }

        private static PriceTime GetPriceByTimeFrameFromDB(DateTime startdate, int timeframe,int shift =0)
        {
            var db = new dataEntities();
            var enddate = startdate.AddMinutes(-timeframe*(shift+1));//45 46 47 48 49   50 51 52 53 54   55
            startdate = startdate.AddMinutes(-timeframe*shift);//             50 
            var list = (db.temp_upload.Where(pr => pr.Datetime > enddate && pr.Datetime <= startdate).Select(pr => new PriceTime {Date = (DateTime) (pr.Datetime), Volume = (double) pr.Qty, Price = (double) pr.VWAP})).ToList();
            var tempprices = new List<PriceTime>(list.OrderBy(t => t.Date));
            double price = 0;
            double volume = 0;
            foreach (PriceTime t in tempprices)
            {
                volume = volume + t.Volume;
                price = price + t.Volume * t.Price;
            }
            return new PriceTime {Date = startdate, Volume = volume, Price = price/volume};
        }

        private static PriceTime GetPriceByTimeFrameFromArray(List<PriceTime>  pricelist,int startpos, int timeframe, int shift = 0)
        {
            var db = new dataEntities();
            var endpos = startpos-timeframe * (shift + 1)+1;//45 46 47 48 49   50 51 52 53 54   55
            startpos = startpos -timeframe * shift;//             50 
          //  var list = (db.temp_upload.Where(pr => pr.Datetime > enddate && pr.Datetime <= startdate).Select(pr => new PriceTime { Date = (DateTime)(pr.Datetime), Volume = (double)pr.Qty, Price = (double)pr.VWAP })).ToList();
          //  var tempprices = new List<PriceTime>(list.OrderBy(t => t.Date));
            double price = 0;
            double volume = 0;

            for (var i = endpos; i <= startpos;i++ ){
                volume = volume + pricelist[i].Volume;
                price = price + pricelist[i].Volume * pricelist[i].Price;
                }
            return new PriceTime { Date = pricelist[startpos].Date, Volume = volume, Price = price / volume };
        }

        private List<PriceTime> UploaddatafromDb(DateTime startdate,DateTime enddate,int timeframe)
        {
            var Pricelist = new List<PriceTime>();
            DateTime TimeStartConvert = DateTime.Now;
            var i = 0;
            var db = new dataEntities();
            List<PriceTime> tempprices = new List<PriceTime>((from pr in db.temp_upload
                                               where pr.Date < enddate.Date && pr.Date > startdate.Date
                                               select new PriceTime{Date = (DateTime) (pr.Datetime),Volume = (double) pr.Qty, Price = (double) pr.VWAP}).ToList().OrderBy(t=>t.Date));
            List<PriceTime> prices = new List<PriceTime>();
            for (int j = 0; j < tempprices.Count; j=(j+1)*timeframe)
            {
                double price = 0;
                double volume = 0;
                int k = 0;
                while ((j+k<tempprices.Count)&&(k<timeframe))
                //for (int k = 0; k < timeframe; k++)
                {
                    volume = volume + tempprices[j + k].Volume;
                    price = price+tempprices[j + k].Volume * tempprices[j + k].Price;
                    k++;
                }
                prices.Add(new PriceTime { Date = tempprices[j].Date, Volume = volume, Price = price / volume });
            }
            DateTime TimeEndConvert = DateTime.Now;
            LogTextBox.AppendText("\r\n" + TimeEndConvert.ToLongTimeString() + ": " + "Prices uploaded. Total items: " + prices.Count().ToString());
            return prices;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            //var pricelist = Uploaddatafromfile();
            var sizeStart = (int) SizeOfPriceWindow.Value;
            var sizeEnd = (int)SizeOfPriceWindowEnd.Value;
            var vsize = (int) SizeOfVolumeWindow.Value*100;
            var vsizeEnd = (int) SizeOfVolumeWindowEnd.Value*100;
            var sizestep = 100;
            var vsizestep = 500;
            int timeframe = (int) timeframeupdown.Value;
         
            if (!PricesUploaded.Checked)
            {
                pricelist = PricesFromDb.Checked ? UploaddatafromDb(dateTimeStartUploadPrices.Value.Date, dateTimeEndUploadPrices.Value.Date, timeframe) : Uploaddatafromfile();
                PricesUploaded.Checked = true;
            }   
               

            if (!StatReady.Checked)
            {
                if (adaptCheckBox.Checked)
                {
                    stat = calculateAdap(pricelist, (int) numberofpoints.Value, (int) numberofpointsEnd.Value,
                                         (int)timeframeupdown.Value, NumberPointPrice, NumberPointVol);
                }
                else
                {
                    stat = calculate(pricelist, sizeStart, sizeEnd, sizestep, vsize, vsizeEnd, vsizestep,
                                     (int) numberofpoints.Value, (int) numberofpointsEnd.Value,
                                     (int) timeframeupdown.Value);
                }
                if (StattoDB.Checked)
                {
                    var db = new dataEntities();
                    var i = 0;
                    foreach (string key in stat.Keys)
                    {
                        db.stats.Add(new stat
                            {
                                Code = key,
                                Count = stat[key].Count,
                                Up2win = stat[key].pos2window,
                                // Down2win = stat[key].neg2window,
                                Up4win = stat[key].pos4window,
                                // Down4win = stat[key].neg4window
                            });
                        i++;
                        if (i % 500 == 0) SaveDBChanges(db);
                    }
                    SaveDBChanges(db);
                    db.Dispose();
                }
            }
             var pricelistControl = PricesFromDb.Checked ? UploaddatafromDb(dateTimeStartControlPrices.Value.Date, dateTimeEndControlPrices.Value.Date,timeframe) : Uploaddatafromfile();
              
            checkAlgorithm(pricelistControl, stat, sizeStart, sizeEnd, sizestep, vsize, vsizeEnd, vsizestep, (int)numberofpoints.Value, (int)numberofpointsEnd.Value,(int) timeframeupdown.Value);
            var t = 1;// stat.Where(t => t.Value > 1);
        }
        public static decimal GetMedian(IEnumerable<int> source)
        {
            // Create a copy of the input, and sort the copy
            int[] temp = source.ToArray();
            Array.Sort(temp);

            int count = temp.Length;
            if (count == 0)
            {
                throw new InvalidOperationException("Empty collection");
            }
            else if (count % 2 == 0)
            {
                // count is even, average two middle elements
                int a = temp[count / 2 - 1];
                int b = temp[count / 2];
                return (a + b) / 2m;
            }
            else
            {
                // count is odd, return the middle element
                return temp[count / 2];
            }
        }

        private void checkAlgorithm(List<PriceTime> pricelist, Dictionary<string, Results> result, int sizestart, int sizeEnd, int sizestep, int vsizestart, int vsizeEnd, int vsizestep, int nop, int nopend,int timeframe)
        {
            var numberofIteration = 3000;
            var qtyprofittrades = 0;
            var qtylosstrades = 0;
            var qty4profittrades = 0;
            var qty4losstrades = 0;
            double prpoint=(double) ProfitPoints.Value;
            for (int numberofpoint = nop; numberofpoint < nopend; numberofpoint++)
            {
                for (int size = sizestart; size <= sizeEnd; size = size + sizestep)
                {
                    for (int vsize = vsizestart; vsize <= vsizeEnd; vsize = vsize + vsizestep)
                    {
                        qtyprofittrades = 0;
                        qtylosstrades = 0;
                        qty4profittrades = 0;
                        qty4losstrades = 0;
                        
                         
                        for (int i = numberofpoint; i < pricelist.Count; i++)
                        {
                            var code = timeframe + ";" + numberofpoint + ";";
                            if (adaptCheckBox.Checked)
                            {
                                double minPrice = 1000;
                                double maxPrice = 0;
                                double minVol = 1000;
                                double maxVol = 0;
                                for (int j = 1; j <= numberofpoint; j++)
                                {
                                    var currentprice = Math.Abs(pricelist[i - j].Price - pricelist[i].Price);
                                    var currentVol = Math.Abs(pricelist[i - j].Volume - pricelist[i].Volume);
                                    if (currentprice < minPrice) minPrice = currentprice;
                                    if (currentprice > maxPrice) maxPrice = currentprice;
                                    if (currentVol < minVol) minVol = currentVol;
                                    if (currentVol > maxVol) maxVol = currentVol;
                                }
                                size = (int) ((maxPrice - minPrice)/NumberPointPrice);
                                vsize = (int) ((maxVol - minVol)/NumberPointVol);
                                code = code + "0;0;";
                            }
                            else
                            {
                               code = code + size + ";" + vsize + ";";  
                            }

                            for (int j = 0; j < numberofpoint; j++)
                            {
                                code = code +
                                       (Math.Round((pricelist[i - j - 1].Price - pricelist[i].Price)/size)).ToString() +"," +
                                       (Math.Round((pricelist[i - j - 1].Volume - pricelist[i].Volume)/vsize)).ToString() +";";
                            }
                            Results res;
                            var check = 0;
                            if (result.TryGetValue(code, out res))
                            {
                                double Prob2 = (double)res.pos2window / (double)res.Count;
                                double Prob4 = (double)res.pos4window / (double)res.Count;
                                if ((res.Count >= QtyStat.Value) && (Prob2 > (double) Probability.Value))
                                {
                                    check = CheckUpTrend(pricelist, i + 1, pricelist[i].Price + prpoint, pricelist[i].Price - prpoint);
                                    if (Withdetails.Checked)
                                    {
                                        var median = GetMedian(res.pos2windowStep);
                                        LogTextBox.AppendText("\r\n" + "Up2;StatQty:" + res.Count + ";Pr:" + Math.Round(Prob2, 2).ToString() + ";" + pricelist[i].Date.ToString() + ";" + pricelist[i].Price.ToString() + ";Result:" + check.ToString() + " mins" + ";Median:" + median.ToString());
                                    }
                                }
                                else
                                {

                                    if ((res.Count >= QtyStat.Value) && (Prob2 < (1 - (double)Probability.Value)))
                                    {
                                        check = CheckDownTrend(pricelist, i + 1, pricelist[i].Price - prpoint, pricelist[i].Price + prpoint);
                                        if (Withdetails.Checked)
                                        {
                                            LogTextBox.AppendText("\r\n" + "Down2;StatQty:" + res.Count + ";Pr:" + Math.Round(Prob2, 2).ToString() + ";" + pricelist[i].Date.ToString() + ";" + pricelist[i].Price.ToString() + ";Result:" + check.ToString() + " mins");
                                        }
                                    }
                                }

                                if (check > 0) qtyprofittrades++;
                                if (check == -1) qtylosstrades++;
                                check = 0;
                                if ((res.Count >= QtyStat.Value) && (Prob4 > (double) Probability.Value))
                                {
                                    check = CheckUpTrend(pricelist, i + 1, pricelist[i].Price + 2 * prpoint, pricelist[i].Price - 2 * prpoint);
                                    if (Withdetails.Checked)
                                    {
                                        LogTextBox.AppendText("\r\n" + "Up4;StatQty:" + res.Count + ";Pr:" + Math.Round(Prob4, 2).ToString() + ";" + pricelist[i].Date.ToString() + ";" + pricelist[i].Price.ToString() + ";Result:" + check.ToString() + " mins");
                                    }
                                }
                                else
                                {
                                    if ((res.Count >= QtyStat.Value) && (Prob4 < (1 - (double)Probability.Value)))
                                    {
                                        check = CheckDownTrend(pricelist, i + 1, pricelist[i].Price - 4 * size, pricelist[i].Price + 4 * size);
                                        if (Withdetails.Checked)
                                        {
                                            LogTextBox.AppendText("\r\n" + "Down4;StatQty:" + res.Count + ";Pr:" + Math.Round(Prob4, 2).ToString() + ";" + pricelist[i].Date.ToString() + ";" + pricelist[i].Price.ToString() + ";Result:" + check.ToString() + " mins");
                                        }
                                    }
                                }
                                
                                if (check > 0) qty4profittrades++;
                                if (check == -1) qty4losstrades++;
                            }
                        }
                        double profitfactor = 0;
                        if ((qtyprofittrades + qtylosstrades) != 0) profitfactor=Math.Round((double)(100 * qtyprofittrades / (qtyprofittrades + qtylosstrades)));
                        double profitfactor2 = 0;
                        if ((qty4profittrades + qty4losstrades) != 0) profitfactor2 = Math.Round((double)(100 * qty4profittrades / (qty4profittrades + qty4losstrades)));
                        //                     
//                        LogTextBox.AppendText("\r\n" + "Config: " + "nop=" + numberofpoint + ";size=" + size + ";vsize=" + vsize + ";QtyTrades:" + qtyprofittrades.ToString() + ";Profit2:" + ((qtyprofittrades-qtylosstrades)*size).ToString()+";Profit4:" + ((qty4profittrades-qty4losstrades)*size).ToString());
                        LogTextBox.AppendText("\r\n" + numberofpoint + ";" + size + ";" + vsize + ";prpoint=" + prpoint.ToString() + ";1stProfit:" + (qtyprofittrades + qtylosstrades).ToString() + ";"
                            + ((qtyprofittrades - qtylosstrades) * prpoint).ToString() + ";" + profitfactor.ToString() + ";2ndProfit:" + (qty4profittrades + qty4losstrades).ToString() + ";" + ((qty4profittrades - qty4losstrades) * 2 * prpoint).ToString() + ";" + profitfactor2.ToString());
                   /*     LogTextBox.AppendText("\r\n" + "Total trades: " + pricelist.Count.ToString());
                        LogTextBox.AppendText("\r\n" + "Total win: " + qtyprofittrades.ToString());
                        LogTextBox.AppendText("\r\n" + "Total loss: " + qtylosstrades.ToString());
                        LogTextBox.AppendText("\r\n" + "Total win4: " + qty4profittrades.ToString());
                        LogTextBox.AppendText("\r\n" + "Total loss4: " + qty4losstrades.ToString());*/
                        if (adaptCheckBox.Checked)
                        {
                            size = sizeEnd;
                            vsize = vsizeEnd;
                        }
                    }
                }
            }
        }

        private void checkAlgorithm2(List<PriceTime> pricelist, Dictionary<string, Results> result, int sizestart, int sizeEnd, int sizestep, int vsizestart, int vsizeEnd, int vsizestep, int nop, int nopend, int timeframe)
        {
            var numberofIteration = 3000;
            var qtyprofittrades = 0;
            var qtylosstrades = 0;
            var qty4profittrades = 0;
            var qty4losstrades = 0;
            double prpoint = (double)ProfitPoints.Value*Convert.ToInt32(profitpointmty.Text);
            for (int numberofpoint = nop; numberofpoint < nopend; numberofpoint++)
            {
                     qtyprofittrades = 0;
                        qtylosstrades = 0;
                        qty4profittrades = 0;
                        qty4losstrades = 0;

                        var code = "";
                        for (int i = numberofpoint*timeframe*3; i < pricelist.Count; i++)
                        {
                             code = "ADAPT3;1;" + timeframe.ToString() + ";" + (3*timeframe).ToString() + ";" +numberofpoint + ";";
                                ///first cycle
                             code = code + GetAdaptCode(pricelist, numberofpoint, NumberPointPrice, NumberPointVol, i, 1);
                             code = code + GetAdaptCode(pricelist, numberofpoint, NumberPointPrice, NumberPointVol, i, timeframe);
                             code = code + GetAdaptCode(pricelist, numberofpoint, NumberPointPrice, NumberPointVol, i, 3 * timeframe);
                        
                        Results res;
                            var check = 0;
                            if (result.TryGetValue(code, out res))
                            {
                                double Prob2 = (double)res.pos2window / (double)res.Count;
                                double Prob4 = (double)res.pos4window / (double)res.Count;
                                if ((res.Count >= QtyStat.Value) && (Prob2 > (double)Probability.Value))
                                {
                                    check = CheckUpTrend(pricelist, i + 1, pricelist[i].Price + prpoint, pricelist[i].Price - prpoint);
                                    if (Withdetails.Checked)
                                    {
                                        var median = GetMedian(res.pos2windowStep);
                                        LogTextBox.AppendText("\r\n" + "Up2;StatQty:" + res.Count + ";Pr:" + Math.Round(Prob2, 2).ToString() + ";" + pricelist[i].Date.ToString() + ";" + pricelist[i].Price.ToString() + ";Result:" + check.ToString() + " mins" + ";Median:" + median.ToString());
                                    }
                                }
                                else
                                {

                                    if ((res.Count >= QtyStat.Value) && (Prob2 < (1 - (double)Probability.Value)))
                                    {
                                        check = CheckDownTrend(pricelist, i + 1, pricelist[i].Price - prpoint, pricelist[i].Price + prpoint);
                                        if (Withdetails.Checked)
                                        {
                                            LogTextBox.AppendText("\r\n" + "Down2;StatQty:" + res.Count + ";Pr:" + Math.Round(Prob2, 2).ToString() + ";" + pricelist[i].Date.ToString() + ";" + pricelist[i].Price.ToString() + ";Result:" + check.ToString() + " mins");
                                        }
                                    }
                                }

                                if (check > 0) qtyprofittrades++;
                                if (check == -1) qtylosstrades++;
                                check = 0;
                                if ((res.Count >= QtyStat.Value) && (Prob4 > (double)Probability.Value))
                                {
                                    check = CheckUpTrend(pricelist, i + 1, pricelist[i].Price + 2 * prpoint, pricelist[i].Price - 2 * prpoint);
                                    if (Withdetails.Checked)
                                    {
                                        LogTextBox.AppendText("\r\n" + "Up4;StatQty:" + res.Count + ";Pr:" + Math.Round(Prob4, 2).ToString() + ";" + pricelist[i].Date.ToString() + ";" + pricelist[i].Price.ToString() + ";Result:" + check.ToString() + " mins");
                                    }
                                }
                                else
                                {
                                    if ((res.Count >= QtyStat.Value) && (Prob4 < (1 - (double)Probability.Value)))
                                    {
                                        check = CheckDownTrend(pricelist, i + 1, pricelist[i].Price - 2 * prpoint, pricelist[i].Price + 2 * prpoint);
                                        if (Withdetails.Checked)
                                        {
                                            LogTextBox.AppendText("\r\n" + "Down4;StatQty:" + res.Count + ";Pr:" + Math.Round(Prob4, 2).ToString() + ";" + pricelist[i].Date.ToString() + ";" + pricelist[i].Price.ToString() + ";Result:" + check.ToString() + " mins");
                                        }
                                    }
                                }

                                if (check > 0) qty4profittrades++;
                                if (check == -1) qty4losstrades++;
                            }
                        }
                        double profitfactor = 0;
                        if ((qtyprofittrades + qtylosstrades) != 0) profitfactor = Math.Round((double)(100 * qtyprofittrades / (qtyprofittrades + qtylosstrades)));
                        double profitfactor2 = 0;
                        if ((qty4profittrades + qty4losstrades) != 0) profitfactor2 = Math.Round((double)(100 * qty4profittrades / (qty4profittrades + qty4losstrades)));
                        //                     
                        //                        LogTextBox.AppendText("\r\n" + "Config: " + "nop=" + numberofpoint + ";size=" + size + ";vsize=" + vsize + ";QtyTrades:" + qtyprofittrades.ToString() + ";Profit2:" + ((qtyprofittrades-qtylosstrades)*size).ToString()+";Profit4:" + ((qty4profittrades-qty4losstrades)*size).ToString());
                        LogTextBox.AppendText("\r\n" + numberofpoint +  ";prpoint=" + prpoint.ToString() + ";1stProfit:" + (qtyprofittrades + qtylosstrades).ToString() + ";"
                            + ((qtyprofittrades - qtylosstrades) * prpoint).ToString() + ";" + profitfactor.ToString() + ";2ndProfit:" + (qty4profittrades + qty4losstrades).ToString() + ";" + ((qty4profittrades - qty4losstrades) * 2 * prpoint).ToString() + ";" + profitfactor2.ToString());
                        /*     LogTextBox.AppendText("\r\n" + "Total trades: " + pricelist.Count.ToString());
                             LogTextBox.AppendText("\r\n" + "Total win: " + qtyprofittrades.ToString());
                             LogTextBox.AppendText("\r\n" + "Total loss: " + qtylosstrades.ToString());
                             LogTextBox.AppendText("\r\n" + "Total win4: " + qty4profittrades.ToString());
                             LogTextBox.AppendText("\r\n" + "Total loss4: " + qty4losstrades.ToString());*/
                        }
        }
        public class Results
        {
            public int numberofpoints { get; set; }
            public double size { get; set; }
            public double vsize { get; set;}
            public int Count { get; set; }
            public int pos2window { get; set; }
            public List<int> pos2windowStep { get; set; }
            public int neg2window { get; set; }
            public List<int> neg2windowStep { get; set; }
            public int pos4window { get; set; }
            public int neg4window { get; set; }
            public int pos8window { get; set; }
            public int neg8window { get; set; }
        }
                                                                                     
        private Dictionary<string, Results> calculate(List<PriceTime> pricelist,int sizestart, int sizeEnd,int sizestep,int vsizestart,int vsizeEnd,int vsizestep,int nop, int nopend,int timeframe)
        {
            var result = new Dictionary<string, Results>();
            var numberofIteration = 3000;
            var listresults = new List<ri_data>();
            double prpoint = (double) ProfitPoints.Value;

            for (int numberofpoint = nop; numberofpoint < nopend; numberofpoint++)
            {
                for (int size = sizestart; size <= sizeEnd; size = size + sizestep)
                {
                    for (int vsize = vsizestart; vsize <= vsizeEnd; vsize = vsize + vsizestep)
                    {
                        for (int i = numberofpoint; i < pricelist.Count; i++)
                        {
                            var code =timeframe+";"+ numberofpoint + ";" + size + ";" + vsize + ";";
                            for (int j = 0; j < numberofpoint; j++)
                            {
                                code = code +(Math.Round((pricelist[i - j - 1].Price - pricelist[i].Price)/size)).ToString() +"," +(Math.Round((pricelist[i - j - 1].Volume - pricelist[i].Volume)/vsize)).ToString() +";";
                            }
                            
                            if (result.ContainsKey(code))
                            {
                                result[code].Count = result[code].Count + 1;
                            }
                            else
                            {
                                result.Add(code,
                                           new Results
                                               {
                                                   size = size,
                                                   vsize = vsize,
                                                   numberofpoints = numberofpoint,
                                                   Count = 1,
                                                   pos2windowStep = new List<int>(),
                                                   neg2windowStep = new List<int>()
                                               });
                            }
                            var pos2win = CheckUpTrend(pricelist, i + 1, pricelist[i].Price + prpoint, pricelist[i].Price - prpoint);
                          //  var pos2win = CheckUpTrend(pricelist, i + 1, pricelist[i].Price + size, pricelist[i].Price - size, numberofIteration);
                            if (pos2win > 0)
                            {
                                result[code].pos2window = result[code].pos2window + 1;
                                result[code].pos2windowStep.Add(pos2win);
                            }

                            var Up4win2sl = CheckUpTrend(pricelist, i + 1, pricelist[i].Price + 2 * prpoint, pricelist[i].Price - prpoint);
                            var Down4win2sl = CheckDownTrend(pricelist, i + 1, pricelist[i].Price - 2 * prpoint, pricelist[i].Price + prpoint);

                            var pos4win = CheckUpTrend(pricelist, i + 1, pricelist[i].Price + 2 * prpoint, pricelist[i].Price - 2 * prpoint);
                           // var pos4win = CheckUpTrend(pricelist, i + 1, pricelist[i].Price + 2 * size, pricelist[i].Price - 2 * size, numberofIteration);
                            if (pos4win > 0)
                            {
                                result[code].pos4window = result[code].pos4window + 1;
                                //  result[code].pos4windowStep.Add(check);
                            }

                            
                            listresults.Add(new ri_data
                                {
                                    Code =code,
                                    TradeTimestamp = pricelist[i].Date,
                                    PriceSize = size,
                                    VolumeSize = vsize,
                                    win = prpoint,
                                    Up2Win = (pos2win > 0) ? true : false,
                                    Up2WinTimestamp = (pos2win > 0) ? pricelist[pos2win].Date : pricelist[i].Date,
                                    Up4Win = (pos4win > 0) ? true : false,
                                    Up4WinTimestamp = (pos4win > 0) ? pricelist[pos4win].Date : pricelist[i].Date,
                                    Down4Win2Sl = (Down4win2sl > 0) ? true : false,
                                    Down4Win2SlTimestamp = (Down4win2sl > 0) ? pricelist[Down4win2sl].Date : pricelist[i].Date,
                                    Up4Win2Sl = (Up4win2sl > 0) ? true : false,
                                    Up4Win2SlTimestamp = (Up4win2sl > 0) ? pricelist[Up4win2sl].Date : pricelist[i].Date
                                });
                          //  CheckLastUpdate(code,pricelist[i].Date, pos2win, pos4win);
                    }
                    }
                }

            }
          /*  var db = new dataEntities();
            foreach (ri_data riData in listresults)
            {
                db.ri_data.Add(riData);
                //if (!checkCodeTradeinDb(riData.Code, riData.TradeTimestamp)){}
            }
            SaveDBChanges(db);
            db.Dispose();*/
            return result;
        }

        private bool checkCodeTradeinDb(string code, DateTime tradeTimestamp)
        {
            var db = new dataEntities();
            var rowstat = (from pr in db.ri_data
                            where pr.Code == code && pr.TradeTimestamp == tradeTimestamp
                            select pr).Count();
            if (rowstat ==0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void CheckLastUpdate(string code, DateTime currenttime,int pos2win,int pos4win)
        {
        /*    var db = new dataEntities();
            stat rowstat = (from pr in db.stats
                           where pr.Code==code //&& pr.LastTime < currenttime
                           select pr).FirstOrDefault();
            
           //if (currenttime>rowstat.LastTime.Value)
           // {
                 /*   db.stats.Add(new stat
                        {
                            Code = key,
                            Count = stat[key].Count,
                            Up2win = stat[key].pos2window,
                            Down2win = stat[key].neg2window,
                            Up4win = stat[key].pos4window,
                            Down4win = stat[key].neg4window
                        });*/
         /*       
                SaveDBChanges(db);
            }*/
        }

        private int CheckDownTrend(List<PriceTime> pricelist, int i, double target, double fault)
        {
            var start = i;
            while ((i < pricelist.Count) && (pricelist[i].Price > target) && (pricelist[i].Price < fault)) i++;
            if (i < pricelist.Count)
            {
                if (pricelist[i].Price <= target)
                {
                    return i-start;
                }
                else
                {
                    if (pricelist[i].Price >= fault)
                    {
                        return (start - i); 
                    }
                    else return 0;
                }
            }
            else return 0;
        }

        private int CheckUpTrend(List<PriceTime> pricelist, int i,double target,double fault)
        {
            var start = i; ;
            while ((i < pricelist.Count) && (pricelist[i].Price < target) && (pricelist[i].Price > fault)) i++;
            if (i < pricelist.Count)
            {
                if (pricelist[i].Price >= target)
                {
                    return i-start;
                }
                else
                {
                    if (pricelist[i].Price <= fault)
                    {
                        return (start-i);
                    }
                    else return 0;
                }
            }
            else return 0;
        }

        private void upload_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                var Pricelist = new List<PriceTime>();
                foreach (string oFilename in openFileDialog1.FileNames)
                {
                    DateTime TimeStartConvert = DateTime.Now;
                    LogTextBox.AppendText("\r\n" + TimeStartConvert.ToLongTimeString() + ": " + "start " + oFilename +" parsing");

                    var reader = new StreamReader(oFilename);
                    var lineFromFile = reader.ReadLine();
                    string[] rowstring;
                    if (lineFromFile != null)
                    {
                        rowstring = lineFromFile.Split(Delimiter);
                    }
                    var i = 0;
                    while (!reader.EndOfStream)
                    {
                        lineFromFile = reader.ReadLine();
                        rowstring = lineFromFile.Split(Delimiter);
                        var time = DateTime.ParseExact(rowstring[2], "HH:mm:ss.FFF", CultureInfo.CurrentCulture);
                        var ts = new TimeSpan(time.Hour, time.Minute, time.Second);
                        var datefromrow =
                            DateTime.ParseExact(rowstring[1], "yyyyMMdd", CultureInfo.CurrentCulture).Date + ts;
                        Pricelist.Add(new PriceTime { Price = Convert.ToDouble(rowstring[6]), Date = datefromrow, Volume = Convert.ToDouble(rowstring[5]) });
                        i++;
                    }
                    DateTime TimeEndConvert = DateTime.Now;
                    LogTextBox.AppendText("\r\n" + TimeEndConvert.ToLongTimeString() + ": " + "End. Total items: " +
                                          i.ToString());
                }
               
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var db = new dataEntities();
            string lastupdate = (from pr in db.temp_upload 
                                 select pr.Datetime).Max().Value.ToString();

            label6.Text = lastupdate;
        }

        private Dictionary<string, Results> calculateAdap(List<PriceTime> pricelist,   int nop, int nopend, int timeframe,int NumberPointPrice,int NumberPointVol)
        {
            var result = new Dictionary<string, Results>();
            var numberofIteration = 3000;
            var listresults = new List<ri_data>();
            double prpoint = (double)ProfitPoints.Value;

            for (int numberofpoint = nop; numberofpoint < nopend; numberofpoint++)
            {
              for (int i = numberofpoint; i < pricelist.Count; i++)
              {
                  double minPrice = 1000;
                  double maxPrice = 0;
                  double minVol = 1000;
                  double maxVol = 0;
                  for (int j = 1; j <= numberofpoint; j++)
                  {
                      var currentprice = Math.Abs(pricelist[i - j].Price-pricelist[i].Price);
                      var currentVol = Math.Abs(pricelist[i - j].Volume-pricelist[i].Volume);
                      if (currentprice < minPrice) minPrice = currentprice;
                      if (currentprice > maxPrice) maxPrice = currentprice;
                      if (currentVol < minVol) minVol = currentVol;
                      if (currentVol > maxVol) maxVol = currentVol;
                  }
                  var code = timeframe + ";" + numberofpoint + ";0;0;";
                  var size = (maxPrice - minPrice)/NumberPointPrice;
                  var vsize = (maxVol - minVol)/NumberPointVol;
                  for (int j = 0; j < numberofpoint; j++)
                  {
                      code = code + (Math.Round((pricelist[i - j - 1].Price - pricelist[i].Price) / size)).ToString() + "," + (Math.Round((pricelist[i - j - 1].Volume - pricelist[i].Volume) / vsize)).ToString() + ";";
                  }

                            if (result.ContainsKey(code))
                            {
                                result[code].Count = result[code].Count + 1;
                            }
                            else
                            {
                                result.Add(code,
                                           new Results
                                           {
                                               size = 0,
                                               vsize = 0,
                                               numberofpoints = numberofpoint,
                                               Count = 1,
                                               pos2windowStep = new List<int>(),
                                               neg2windowStep = new List<int>()
                                           });
                            }
                            var pos2win = CheckUpTrend(pricelist, i + 1, pricelist[i].Price + prpoint, pricelist[i].Price - prpoint);
                            //  var pos2win = CheckUpTrend(pricelist, i + 1, pricelist[i].Price + size, pricelist[i].Price - size, numberofIteration);
                            if (pos2win > 0)
                            {
                                result[code].pos2window = result[code].pos2window + 1;
                                result[code].pos2windowStep.Add(pos2win);
                            }

                            var Up4win2sl = CheckUpTrend(pricelist, i + 1, pricelist[i].Price + 2 * prpoint, pricelist[i].Price - prpoint);
                            var Down4win2sl = CheckDownTrend(pricelist, i + 1, pricelist[i].Price - 2 * prpoint, pricelist[i].Price + prpoint);

                            var pos4win = CheckUpTrend(pricelist, i + 1, pricelist[i].Price + 2 * prpoint, pricelist[i].Price - 2 * prpoint);
                            // var pos4win = CheckUpTrend(pricelist, i + 1, pricelist[i].Price + 2 * size, pricelist[i].Price - 2 * size, numberofIteration);
                            if (pos4win > 0)
                            {
                                result[code].pos4window = result[code].pos4window + 1;
                                //  result[code].pos4windowStep.Add(check);
                            }


                            listresults.Add(new ri_data
                            {
                                Code = code,
                                TradeTimestamp = pricelist[i].Date,
                                PriceSize = size,
                                VolumeSize = vsize,
                                win = prpoint,
                                Up2Win = (pos2win > 0) ? true : false,
                                Up2WinTimestamp = (pos2win > 0) ? pricelist[pos2win].Date : pricelist[i].Date,
                                Up4Win = (pos4win > 0) ? true : false,
                                Up4WinTimestamp = (pos4win > 0) ? pricelist[pos4win].Date : pricelist[i].Date,
                                Down4Win2Sl = (Down4win2sl > 0) ? true : false,
                                Down4Win2SlTimestamp = (Down4win2sl > 0) ? pricelist[Down4win2sl].Date : pricelist[i].Date,
                                Up4Win2Sl = (Up4win2sl > 0) ? true : false,
                                Up4Win2SlTimestamp = (Up4win2sl > 0) ? pricelist[Up4win2sl].Date : pricelist[i].Date
                            });
                            //  CheckLastUpdate(code,pricelist[i].Date, pos2win, pos4win);
                        }
                    }
            /*  var db = new dataEntities();
              foreach (ri_data riData in listresults)
              {
                  db.ri_data.Add(riData);
                  //if (!checkCodeTradeinDb(riData.Code, riData.TradeTimestamp)){}
              }
              SaveDBChanges(db);
              db.Dispose();*/
            return result;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var sizeStart = (int)SizeOfPriceWindow.Value;
            var sizeEnd = (int)SizeOfPriceWindowEnd.Value;
            var vsize = (int)SizeOfVolumeWindow.Value * 100;
            var vsizeEnd = (int)SizeOfVolumeWindowEnd.Value * 100;
            var sizestep = 100;
            var vsizestep = 500;
            int timeframe = (int)timeframeupdown.Value;
            int timeframe2 = (int)timeframeUpDown2.Value;

            if (!PricesUploaded.Checked)
            {
                pricelist = PricesFromDb.Checked ? UploaddatafromDb(dateTimeStartUploadPrices.Value.Date, dateTimeEndUploadPrices.Value.Date, 1) : Uploaddatafromfile();
                PricesUploaded.Checked = true;
            }


            if (!StatReady.Checked)
            {
                stat = calculateAdap3Time(pricelist, (int)numberofpoints.Value, (int)numberofpointsEnd.Value,(int)timeframeupdown.Value, NumberPointPrice, NumberPointVol);
                if (StattoDB.Checked)
                {
                    var db = new dataEntities();
                    var i = 0;
                    foreach (string key in stat.Keys)
                    {
                        db.stats.Add(new stat
                        {
                            Code = key,
                            Count = stat[key].Count,
                            Up2win = stat[key].pos2window,
                            // Down2win = stat[key].neg2window,
                            Up4win = stat[key].pos4window,
                            // Down4win = stat[key].neg4window
                        });
                        i++;
                        if (i % 500 == 0) SaveDBChanges(db);
                    }
                    SaveDBChanges(db);
                    db.Dispose();
                }
            }
            var pricelistControl = PricesFromDb.Checked ? UploaddatafromDb(dateTimeStartControlPrices.Value.Date, dateTimeEndControlPrices.Value.Date,1) : Uploaddatafromfile();

           // checkAlgorithm2(pricelistControl, stat, sizeStart, sizeEnd, sizestep, vsize, vsizeEnd, vsizestep, (int)numberofpoints.Value, (int)numberofpointsEnd.Value, (int)timeframeupdown.Value);
            var t = 1;// stat.Where(t => t.Value > 1);
        }

        private void /*Dictionary<string, Results> */ calculateAdap3Time(List<PriceTime> pricelist, int nop, int nopend,int timeframe, int NumberPointPrice, int NumberPointVol)
        {
            var result = new Dictionary<string, Results>();
            var numberofIteration = 3000;
            var listresults = new List<ri_data>();
            double prpoint = (double) ProfitPoints.Value*Convert.ToInt32(profitpointmty.Text);
            for (int numberofpoint = nop; numberofpoint < nopend; numberofpoint++)
             {
                 for (int i = numberofpoint*timeframe*3; i < this.pricelist.Count; i++)
                 {
                     getListResults(pricelist, timeframe, NumberPointPrice, NumberPointVol, i, numberofpoint, prpoint, 100);
                     var code = "";
                     var nextday = true;
                     var pos2win = 1;
                     var pos4win = 1;
                     
                     if (!(Intraday.Checked) || (!nextday))
                     {
                         if (result.ContainsKey(code))
                         {
                             result[code].Count = result[code].Count + 1;
                         }
                         else
                         {
                             result.Add(code, new Results
                             {
                                 size = 0,
                                 vsize = 0,
                                 numberofpoints = numberofpoint,
                                 Count = 1,
                                 pos2windowStep = new List<int>(),
                                 neg2windowStep = new List<int>()
                             });
                             if (pos2win > 0)
                             {
                                 result[code].pos2window = result[code].pos2window + 1;
                                 result[code].pos2windowStep.Add(pos2win);
                             }
                             if (pos4win > 0)
                             {
                                 result[code].pos4window = result[code].pos4window + 1;
                                 //  result[code].pos4windowStep.Add(check);
                             }
                         }
                     }
/*
                         var Up4win2sl = CheckUpTrend(this.pricelist, i + 1, this.pricelist[i].Price + 2*prpoint,this.pricelist[i].Price - prpoint);
                         if (i + numberofIteration > pricelist.Count)
                         {
                             nextday = true;
                         }
                         else
                         {
                             if ((Intraday.Checked) && (pricelist[i].Date.Date == pricelist[i + numberofIteration].Date.Date)) nextday = true;
                         }
                         var Down4win2sl = CheckDownTrend(this.pricelist, i + 1, this.pricelist[i].Price - 2*prpoint,this.pricelist[i].Price + prpoint);
                         if (i + numberofIteration > pricelist.Count)
                         {
                             nextday = true;
                         }
                         else
                         {
                             if ((Intraday.Checked) && (pricelist[i].Date.Date == pricelist[i + numberofIteration].Date.Date)) nextday = true;
                         }
                          if(!nextday){
                             
                             listresults.Add(new ri_data{
                             Code = code,TradeTimestamp = this.pricelist[i].Date,//  PriceSize = size,//  VolumeSize = vsize,win = prpoint,Up2Win = (pos2win > 0) ? true : false,
                             Up2WinTimestamp = (pos2win > 0) ? this.pricelist[pos2win].Date : this.pricelist[i].Date,
                             Up4Win = (pos4win > 0) ? true : false,
                             Up4WinTimestamp = (pos4win > 0) ? this.pricelist[pos4win].Date : this.pricelist[i].Date,
                             Down4Win2Sl = (Down4win2sl > 0) ? true : false,
                             Down4Win2SlTimestamp =(Down4win2sl > 0) ? this.pricelist[Down4win2sl].Date : this.pricelist[i].Date,
                             Up4Win2Sl = (Up4win2sl > 0) ? true : false,
                             Up4Win2SlTimestamp =(Up4win2sl > 0) ? this.pricelist[Up4win2sl].Date : this.pricelist[i].Date
                         });}
                         //  CheckLastUpdate(code,pricelist[i].Date, pos2win, pos4win);
                     */
                 }
             }
        }

        private Coderesult getListResults(List<PriceTime> pricelist, int timeframe, int NumberPointPrice, int NumberPointVol, int i, int numberofpoint,double startpoint,int step)
        {
            if (!((Intraday.Checked) && (pricelist[i].Date.Date != pricelist[i - numberofpoint*timeframe*3].Date.Date)))
            {
                var code = "ADAPT3;1;" + timeframe.ToString() + ";" + (3*timeframe).ToString() + ";" + numberofpoint +
                           ";";
                code = code + GetAdaptCode(pricelist, numberofpoint, NumberPointPrice, NumberPointVol, i, 1);
                code = code + GetAdaptCode(pricelist, numberofpoint, NumberPointPrice, NumberPointVol, i, timeframe);
                code = code + GetAdaptCode(pricelist, numberofpoint, NumberPointPrice, NumberPointVol, i, 3*timeframe);
                var result = new Coderesult {Code = code};
                for (var pfpoint = startpoint; pfpoint < pfpoint + 10*step; pfpoint = pfpoint + step)
                {
                    var numberofiteration = CheckUpTrend(this.pricelist, i + 1, this.pricelist[i].Price + pfpoint,this.pricelist[i].Price - pfpoint);
                    if (numberofiteration > 0)
                    {
                        result.Levelresults.Add((int) pfpoint, numberofiteration);
                    }
                    else
                    {
                        CheckDownTrend(pricelist, i + 1, this.pricelist[i].Price - pfpoint,this.pricelist[i].Price + pfpoint);
                        result.Levelresults.Add(-(int) pfpoint, -numberofiteration);
                    }
                }
                return result;

            }
            else return null;
        }
        

        private static string GetAdaptCode(List<PriceTime> pricelist, int nop, int numberPointPrice, int numberPointVol, int i,int timeframe)
        {
            double minPrice = 1000;
            double maxPrice = 0;
            double minVol = 1000;
            double maxVol = 0;
            string code = "";
            var priceitem = GetPriceByTimeFrameFromArray(pricelist,i, timeframe);

            var initialprice = priceitem.Price;
            var initialVol = priceitem.Volume;
            for (int j = 0; j < nop; j++)
            {
                var currentItem = GetPriceByTimeFrameFromArray(pricelist,i,timeframe, j);
                var currentprice = Math.Abs(currentItem.Price - initialprice);
                var currentVol = Math.Abs(currentItem.Volume - initialVol);
                if (currentprice < minPrice) minPrice = currentprice;
                if (currentprice > maxPrice) maxPrice = currentprice;
                if (currentVol < minVol) minVol = currentVol;
                if (currentVol > maxVol) maxVol = currentVol;
            }
            double size = (maxPrice - minPrice)/numberPointPrice;
            double vsize = (maxVol - minVol)/numberPointVol;
            for (int j = 0; j < nop; j++)
            {
                var currentItem = GetPriceByTimeFrameFromArray(pricelist, i, timeframe, j);
                code = code + (Math.Round((currentItem.Price - initialprice) / size)).ToString() + "," + (Math.Round((currentItem.Volume - initialVol) / vsize)).ToString() + ";";
            }
            return code;
        }
    }
}
