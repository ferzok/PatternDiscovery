//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PatternDiscovery
{
    using System;
    using System.Collections.Generic;
    
    public partial class ri_data
    {
        public string Code { get; set; }
        public System.DateTime TradeTimestamp { get; set; }
        public Nullable<double> PriceSize { get; set; }
        public Nullable<double> VolumeSize { get; set; }
        public System.DateTime Up2WinTimestamp { get; set; }
        public System.DateTime Up4WinTimestamp { get; set; }
        public System.DateTime Up4Win2SlTimestamp { get; set; }
        public System.DateTime Down4Win2SlTimestamp { get; set; }
        public Nullable<bool> Up2Win { get; set; }
        public Nullable<bool> Up4Win { get; set; }
        public Nullable<bool> Up4Win2Sl { get; set; }
        public Nullable<bool> Down4Win2Sl { get; set; }
        public long id { get; set; }
        public Nullable<double> win { get; set; }
    }
}
