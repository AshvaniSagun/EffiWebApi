//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EfficiencyClassWebAPI.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class StagedWeightSegmentCo2
    {
        public long EwId { get; set; }
        public long MMID { get; set; }
        public string PNO12 { get; set; }
        public string PWeight { get; set; }
        public string SegmentCo2 { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    
        public virtual Market2MarketTypeParameterGroup Market2MarketTypeParameterGroup { get; set; }
    }
}
