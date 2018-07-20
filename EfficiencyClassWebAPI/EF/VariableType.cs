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
    
    public partial class VariableType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VariableType()
        {
            this.EfficiencyClassRanges = new HashSet<EfficiencyClassRange>();
            this.SpecMarketFuelTypeRange_old = new HashSet<SpecMarketFuelTypeRange_old>();
            this.StagedEfficiencyClassRanges = new HashSet<StagedEfficiencyClassRange>();
            this.StagedVariables = new HashSet<StagedVariable>();
            this.Variables = new HashSet<Variable>();
        }
    
        public int Id { get; set; }
        public int ParameterGroupId { get; set; }
        public string VariableTypeName { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EfficiencyClassRange> EfficiencyClassRanges { get; set; }
        public virtual ParameterGroupMaster ParameterGroupMaster { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SpecMarketFuelTypeRange_old> SpecMarketFuelTypeRange_old { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StagedEfficiencyClassRange> StagedEfficiencyClassRanges { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StagedVariable> StagedVariables { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Variable> Variables { get; set; }
    }
}