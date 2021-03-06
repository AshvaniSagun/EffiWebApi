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
    
    public partial class StagedVariable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StagedVariable()
        {
            this.StagedFormulas = new HashSet<StagedFormula>();
            this.StagedFormulaDependencyDetails = new HashSet<StagedFormulaDependencyDetail>();
        }
    
        public long Id { get; set; }
        public string VariableName { get; set; }
        public string VariableValue { get; set; }
        public long MMId { get; set; }
        public int VariableTypeId { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    
        public virtual Market2MarketTypeParameterGroup Market2MarketTypeParameterGroup { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StagedFormula> StagedFormulas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StagedFormulaDependencyDetail> StagedFormulaDependencyDetails { get; set; }
        public virtual VariableType VariableType { get; set; }
    }
}
