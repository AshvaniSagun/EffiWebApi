using System;

namespace EfficiencyClassWebAPI.Models
{
    public class FormulaDependencyModel
    {
        public int Id { get; set; }
        public int FormulaId { get; set; }
        public long VariableId { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    }
}