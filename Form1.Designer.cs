namespace PatternDiscovery
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.LogTextBox = new System.Windows.Forms.RichTextBox();
            this.numberofpoints = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SizeOfPriceWindow = new System.Windows.Forms.NumericUpDown();
            this.SizeOfVolumeWindow = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.SizeOfPriceWindowEnd = new System.Windows.Forms.NumericUpDown();
            this.SizeOfVolumeWindowEnd = new System.Windows.Forms.NumericUpDown();
            this.numberofpointsEnd = new System.Windows.Forms.NumericUpDown();
            this.StatReady = new System.Windows.Forms.CheckBox();
            this.Probability = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.Withdetails = new System.Windows.Forms.CheckBox();
            this.upload = new System.Windows.Forms.Button();
            this.dateTimeStartUploadPrices = new System.Windows.Forms.DateTimePicker();
            this.PricesFromDb = new System.Windows.Forms.CheckBox();
            this.dateTimeEndUploadPrices = new System.Windows.Forms.DateTimePicker();
            this.dateTimeStartControlPrices = new System.Windows.Forms.DateTimePicker();
            this.dateTimeEndControlPrices = new System.Windows.Forms.DateTimePicker();
            this.PricesUploaded = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.QtyStat = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.timeframeupdown = new System.Windows.Forms.NumericUpDown();
            this.ProfitPoints = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.StattoDB = new System.Windows.Forms.CheckBox();
            this.adaptCheckBox = new System.Windows.Forms.CheckBox();
            this.endprofitpoints = new System.Windows.Forms.NumericUpDown();
            this.sizepricetextbox = new System.Windows.Forms.TextBox();
            this.volumemty = new System.Windows.Forms.TextBox();
            this.profitpointmty = new System.Windows.Forms.TextBox();
            this.timefreame2 = new System.Windows.Forms.Label();
            this.timeframeUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.button3 = new System.Windows.Forms.Button();
            this.DifferentTimeframe = new System.Windows.Forms.CheckBox();
            this.Intraday = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numberofpoints)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SizeOfPriceWindow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SizeOfVolumeWindow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SizeOfPriceWindowEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SizeOfVolumeWindowEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numberofpointsEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Probability)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.QtyStat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeframeupdown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProfitPoints)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.endprofitpoints)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeframeUpDown2)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(23, 23);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Multiselect = true;
            // 
            // LogTextBox
            // 
            this.LogTextBox.Location = new System.Drawing.Point(23, 263);
            this.LogTextBox.Name = "LogTextBox";
            this.LogTextBox.Size = new System.Drawing.Size(579, 101);
            this.LogTextBox.TabIndex = 2;
            this.LogTextBox.Text = "";
            // 
            // numberofpoints
            // 
            this.numberofpoints.Location = new System.Drawing.Point(214, 16);
            this.numberofpoints.Name = "numberofpoints";
            this.numberofpoints.Size = new System.Drawing.Size(38, 20);
            this.numberofpoints.TabIndex = 3;
            this.numberofpoints.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(124, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "NumberOfPoints";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(107, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "SizeOfPriceWindow";
            // 
            // SizeOfPriceWindow
            // 
            this.SizeOfPriceWindow.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.SizeOfPriceWindow.Location = new System.Drawing.Point(214, 48);
            this.SizeOfPriceWindow.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.SizeOfPriceWindow.Name = "SizeOfPriceWindow";
            this.SizeOfPriceWindow.Size = new System.Drawing.Size(38, 20);
            this.SizeOfPriceWindow.TabIndex = 6;
            this.SizeOfPriceWindow.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // SizeOfVolumeWindow
            // 
            this.SizeOfVolumeWindow.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.SizeOfVolumeWindow.Location = new System.Drawing.Point(214, 79);
            this.SizeOfVolumeWindow.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.SizeOfVolumeWindow.Name = "SizeOfVolumeWindow";
            this.SizeOfVolumeWindow.Size = new System.Drawing.Size(38, 20);
            this.SizeOfVolumeWindow.TabIndex = 8;
            this.SizeOfVolumeWindow.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(96, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "SizeOfVolumeWindow";
            // 
            // SizeOfPriceWindowEnd
            // 
            this.SizeOfPriceWindowEnd.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.SizeOfPriceWindowEnd.Location = new System.Drawing.Point(259, 48);
            this.SizeOfPriceWindowEnd.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.SizeOfPriceWindowEnd.Name = "SizeOfPriceWindowEnd";
            this.SizeOfPriceWindowEnd.Size = new System.Drawing.Size(38, 20);
            this.SizeOfPriceWindowEnd.TabIndex = 9;
            this.SizeOfPriceWindowEnd.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // SizeOfVolumeWindowEnd
            // 
            this.SizeOfVolumeWindowEnd.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.SizeOfVolumeWindowEnd.Location = new System.Drawing.Point(259, 78);
            this.SizeOfVolumeWindowEnd.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.SizeOfVolumeWindowEnd.Name = "SizeOfVolumeWindowEnd";
            this.SizeOfVolumeWindowEnd.Size = new System.Drawing.Size(38, 20);
            this.SizeOfVolumeWindowEnd.TabIndex = 10;
            this.SizeOfVolumeWindowEnd.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // numberofpointsEnd
            // 
            this.numberofpointsEnd.Location = new System.Drawing.Point(260, 16);
            this.numberofpointsEnd.Name = "numberofpointsEnd";
            this.numberofpointsEnd.Size = new System.Drawing.Size(38, 20);
            this.numberofpointsEnd.TabIndex = 11;
            this.numberofpointsEnd.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // StatReady
            // 
            this.StatReady.AutoSize = true;
            this.StatReady.Location = new System.Drawing.Point(368, 16);
            this.StatReady.Name = "StatReady";
            this.StatReady.Size = new System.Drawing.Size(76, 17);
            this.StatReady.TabIndex = 12;
            this.StatReady.Text = "StatReady";
            this.StatReady.UseVisualStyleBackColor = true;
            // 
            // Probability
            // 
            this.Probability.DecimalPlaces = 2;
            this.Probability.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.Probability.Location = new System.Drawing.Point(214, 105);
            this.Probability.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Probability.Name = "Probability";
            this.Probability.Size = new System.Drawing.Size(48, 20);
            this.Probability.TabIndex = 13;
            this.Probability.Value = new decimal(new int[] {
            95,
            0,
            0,
            131072});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(153, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Probability";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.DecimalPlaces = 2;
            this.numericUpDown2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDown2.Location = new System.Drawing.Point(268, 105);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(50, 20);
            this.numericUpDown2.TabIndex = 15;
            this.numericUpDown2.Value = new decimal(new int[] {
            9,
            0,
            0,
            65536});
            // 
            // Withdetails
            // 
            this.Withdetails.AutoSize = true;
            this.Withdetails.Location = new System.Drawing.Point(364, 39);
            this.Withdetails.Name = "Withdetails";
            this.Withdetails.Size = new System.Drawing.Size(78, 17);
            this.Withdetails.TabIndex = 16;
            this.Withdetails.Text = "Withdetails";
            this.Withdetails.UseVisualStyleBackColor = true;
            // 
            // upload
            // 
            this.upload.Location = new System.Drawing.Point(379, 228);
            this.upload.Name = "upload";
            this.upload.Size = new System.Drawing.Size(75, 23);
            this.upload.TabIndex = 17;
            this.upload.Text = "upload";
            this.upload.UseVisualStyleBackColor = true;
            this.upload.Click += new System.EventHandler(this.upload_Click);
            // 
            // dateTimeStartUploadPrices
            // 
            this.dateTimeStartUploadPrices.Location = new System.Drawing.Point(471, 59);
            this.dateTimeStartUploadPrices.Name = "dateTimeStartUploadPrices";
            this.dateTimeStartUploadPrices.Size = new System.Drawing.Size(147, 20);
            this.dateTimeStartUploadPrices.TabIndex = 18;
            this.dateTimeStartUploadPrices.Value = new System.DateTime(2013, 1, 1, 0, 0, 0, 0);
            // 
            // PricesFromDb
            // 
            this.PricesFromDb.AutoSize = true;
            this.PricesFromDb.Checked = true;
            this.PricesFromDb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.PricesFromDb.Location = new System.Drawing.Point(362, 62);
            this.PricesFromDb.Name = "PricesFromDb";
            this.PricesFromDb.Size = new System.Drawing.Size(92, 17);
            this.PricesFromDb.TabIndex = 19;
            this.PricesFromDb.Text = "PricesFromDb";
            this.PricesFromDb.UseVisualStyleBackColor = true;
            // 
            // dateTimeEndUploadPrices
            // 
            this.dateTimeEndUploadPrices.Location = new System.Drawing.Point(471, 88);
            this.dateTimeEndUploadPrices.Name = "dateTimeEndUploadPrices";
            this.dateTimeEndUploadPrices.Size = new System.Drawing.Size(147, 20);
            this.dateTimeEndUploadPrices.TabIndex = 20;
            this.dateTimeEndUploadPrices.Value = new System.DateTime(2015, 4, 30, 0, 0, 0, 0);
            // 
            // dateTimeStartControlPrices
            // 
            this.dateTimeStartControlPrices.Location = new System.Drawing.Point(471, 114);
            this.dateTimeStartControlPrices.Name = "dateTimeStartControlPrices";
            this.dateTimeStartControlPrices.Size = new System.Drawing.Size(147, 20);
            this.dateTimeStartControlPrices.TabIndex = 21;
            this.dateTimeStartControlPrices.Value = new System.DateTime(2015, 5, 4, 0, 0, 0, 0);
            // 
            // dateTimeEndControlPrices
            // 
            this.dateTimeEndControlPrices.Location = new System.Drawing.Point(471, 140);
            this.dateTimeEndControlPrices.Name = "dateTimeEndControlPrices";
            this.dateTimeEndControlPrices.Size = new System.Drawing.Size(147, 20);
            this.dateTimeEndControlPrices.TabIndex = 22;
            this.dateTimeEndControlPrices.Value = new System.DateTime(2015, 6, 3, 0, 0, 0, 0);
            // 
            // PricesUploaded
            // 
            this.PricesUploaded.AutoSize = true;
            this.PricesUploaded.Location = new System.Drawing.Point(362, 81);
            this.PricesUploaded.Name = "PricesUploaded";
            this.PricesUploaded.Size = new System.Drawing.Size(101, 17);
            this.PricesUploaded.TabIndex = 23;
            this.PricesUploaded.Text = "PricesUploaded";
            this.PricesUploaded.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 218);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(57, 30);
            this.button2.TabIndex = 24;
            this.button2.Text = "Update";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(85, 211);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 25;
            this.label5.Text = "LastDateTime";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(85, 228);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 13);
            this.label6.TabIndex = 26;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(148, 166);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 13);
            this.label7.TabIndex = 28;
            this.label7.Text = "QtyStat";
            // 
            // QtyStat
            // 
            this.QtyStat.Location = new System.Drawing.Point(196, 164);
            this.QtyStat.Name = "QtyStat";
            this.QtyStat.Size = new System.Drawing.Size(38, 20);
            this.QtyStat.TabIndex = 27;
            this.QtyStat.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(140, 192);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 13);
            this.label8.TabIndex = 29;
            this.label8.Text = "TimeFrame";
            // 
            // timeframeupdown
            // 
            this.timeframeupdown.Location = new System.Drawing.Point(205, 190);
            this.timeframeupdown.Name = "timeframeupdown";
            this.timeframeupdown.Size = new System.Drawing.Size(38, 20);
            this.timeframeupdown.TabIndex = 30;
            this.timeframeupdown.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // ProfitPoints
            // 
            this.ProfitPoints.Location = new System.Drawing.Point(214, 127);
            this.ProfitPoints.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.ProfitPoints.Name = "ProfitPoints";
            this.ProfitPoints.Size = new System.Drawing.Size(38, 20);
            this.ProfitPoints.TabIndex = 32;
            this.ProfitPoints.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(148, 129);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(60, 13);
            this.label9.TabIndex = 31;
            this.label9.Text = "ProfitPoints";
            // 
            // StattoDB
            // 
            this.StattoDB.AutoSize = true;
            this.StattoDB.Location = new System.Drawing.Point(362, 102);
            this.StattoDB.Name = "StattoDB";
            this.StattoDB.Size = new System.Drawing.Size(75, 17);
            this.StattoDB.TabIndex = 33;
            this.StattoDB.Text = "Stat to DB";
            this.StattoDB.UseVisualStyleBackColor = true;
            // 
            // adaptCheckBox
            // 
            this.adaptCheckBox.AutoSize = true;
            this.adaptCheckBox.Checked = true;
            this.adaptCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.adaptCheckBox.Location = new System.Drawing.Point(362, 128);
            this.adaptCheckBox.Name = "adaptCheckBox";
            this.adaptCheckBox.Size = new System.Drawing.Size(68, 17);
            this.adaptCheckBox.TabIndex = 34;
            this.adaptCheckBox.Text = "Adaptive";
            this.adaptCheckBox.UseVisualStyleBackColor = true;
            // 
            // endprofitpoints
            // 
            this.endprofitpoints.Location = new System.Drawing.Point(260, 128);
            this.endprofitpoints.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.endprofitpoints.Name = "endprofitpoints";
            this.endprofitpoints.Size = new System.Drawing.Size(38, 20);
            this.endprofitpoints.TabIndex = 35;
            this.endprofitpoints.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // sizepricetextbox
            // 
            this.sizepricetextbox.Location = new System.Drawing.Point(303, 48);
            this.sizepricetextbox.Name = "sizepricetextbox";
            this.sizepricetextbox.Size = new System.Drawing.Size(30, 20);
            this.sizepricetextbox.TabIndex = 36;
            this.sizepricetextbox.Text = "10";
            // 
            // volumemty
            // 
            this.volumemty.Location = new System.Drawing.Point(303, 77);
            this.volumemty.Name = "volumemty";
            this.volumemty.Size = new System.Drawing.Size(30, 20);
            this.volumemty.TabIndex = 37;
            this.volumemty.Text = "100";
            // 
            // profitpointmty
            // 
            this.profitpointmty.Location = new System.Drawing.Point(304, 127);
            this.profitpointmty.Name = "profitpointmty";
            this.profitpointmty.Size = new System.Drawing.Size(30, 20);
            this.profitpointmty.TabIndex = 38;
            this.profitpointmty.Text = "100";
            // 
            // timefreame2
            // 
            this.timefreame2.AutoSize = true;
            this.timefreame2.Location = new System.Drawing.Point(256, 192);
            this.timefreame2.Name = "timefreame2";
            this.timefreame2.Size = new System.Drawing.Size(59, 13);
            this.timefreame2.TabIndex = 39;
            this.timefreame2.Text = "TimeFrame";
            // 
            // timeframeUpDown2
            // 
            this.timeframeUpDown2.Location = new System.Drawing.Point(321, 190);
            this.timeframeUpDown2.Name = "timeframeUpDown2";
            this.timeframeUpDown2.Size = new System.Drawing.Size(38, 20);
            this.timeframeUpDown2.TabIndex = 40;
            this.timeframeUpDown2.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(23, 59);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 41;
            this.button3.Text = "start3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // DifferentTimeframe
            // 
            this.DifferentTimeframe.AutoSize = true;
            this.DifferentTimeframe.Checked = true;
            this.DifferentTimeframe.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DifferentTimeframe.Location = new System.Drawing.Point(364, 151);
            this.DifferentTimeframe.Name = "DifferentTimeframe";
            this.DifferentTimeframe.Size = new System.Drawing.Size(115, 17);
            this.DifferentTimeframe.TabIndex = 42;
            this.DifferentTimeframe.Text = "DifferentTimeframe";
            this.DifferentTimeframe.UseVisualStyleBackColor = true;
            // 
            // Intraday
            // 
            this.Intraday.AutoSize = true;
            this.Intraday.Checked = true;
            this.Intraday.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Intraday.Location = new System.Drawing.Point(362, 174);
            this.Intraday.Name = "Intraday";
            this.Intraday.Size = new System.Drawing.Size(64, 17);
            this.Intraday.TabIndex = 43;
            this.Intraday.Text = "Intraday";
            this.Intraday.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 385);
            this.Controls.Add(this.Intraday);
            this.Controls.Add(this.DifferentTimeframe);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.timeframeUpDown2);
            this.Controls.Add(this.timefreame2);
            this.Controls.Add(this.profitpointmty);
            this.Controls.Add(this.volumemty);
            this.Controls.Add(this.sizepricetextbox);
            this.Controls.Add(this.endprofitpoints);
            this.Controls.Add(this.adaptCheckBox);
            this.Controls.Add(this.StattoDB);
            this.Controls.Add(this.ProfitPoints);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.timeframeupdown);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.QtyStat);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.PricesUploaded);
            this.Controls.Add(this.dateTimeEndControlPrices);
            this.Controls.Add(this.dateTimeStartControlPrices);
            this.Controls.Add(this.dateTimeEndUploadPrices);
            this.Controls.Add(this.PricesFromDb);
            this.Controls.Add(this.dateTimeStartUploadPrices);
            this.Controls.Add(this.upload);
            this.Controls.Add(this.Withdetails);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Probability);
            this.Controls.Add(this.StatReady);
            this.Controls.Add(this.numberofpointsEnd);
            this.Controls.Add(this.SizeOfVolumeWindowEnd);
            this.Controls.Add(this.SizeOfPriceWindowEnd);
            this.Controls.Add(this.SizeOfVolumeWindow);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.SizeOfPriceWindow);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numberofpoints);
            this.Controls.Add(this.LogTextBox);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numberofpoints)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SizeOfPriceWindow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SizeOfVolumeWindow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SizeOfPriceWindowEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SizeOfVolumeWindowEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numberofpointsEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Probability)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.QtyStat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeframeupdown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProfitPoints)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.endprofitpoints)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timeframeUpDown2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.RichTextBox LogTextBox;
        private System.Windows.Forms.NumericUpDown numberofpoints;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown SizeOfPriceWindow;
        private System.Windows.Forms.NumericUpDown SizeOfVolumeWindow;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown SizeOfPriceWindowEnd;
        private System.Windows.Forms.NumericUpDown SizeOfVolumeWindowEnd;
        private System.Windows.Forms.NumericUpDown numberofpointsEnd;
        private System.Windows.Forms.CheckBox StatReady;
        private System.Windows.Forms.NumericUpDown Probability;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.CheckBox Withdetails;
        private System.Windows.Forms.Button upload;
        private System.Windows.Forms.DateTimePicker dateTimeStartUploadPrices;
        private System.Windows.Forms.CheckBox PricesFromDb;
        private System.Windows.Forms.DateTimePicker dateTimeEndUploadPrices;
        private System.Windows.Forms.DateTimePicker dateTimeStartControlPrices;
        private System.Windows.Forms.DateTimePicker dateTimeEndControlPrices;
        private System.Windows.Forms.CheckBox PricesUploaded;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown QtyStat;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown timeframeupdown;
        private System.Windows.Forms.NumericUpDown ProfitPoints;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox StattoDB;
        private System.Windows.Forms.CheckBox adaptCheckBox;
        private System.Windows.Forms.NumericUpDown endprofitpoints;
        private System.Windows.Forms.TextBox sizepricetextbox;
        private System.Windows.Forms.TextBox volumemty;
        private System.Windows.Forms.TextBox profitpointmty;
        private System.Windows.Forms.Label timefreame2;
        private System.Windows.Forms.NumericUpDown timeframeUpDown2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.CheckBox DifferentTimeframe;
        private System.Windows.Forms.CheckBox Intraday;
    }
}

