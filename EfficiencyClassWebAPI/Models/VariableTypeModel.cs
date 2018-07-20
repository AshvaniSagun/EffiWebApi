using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EF = EfficiencyClassWebAPI.EF;
using EfficiencyClassWebAPI.Repository;
using EfficiencyClassWebAPI.ResourceFiles;


namespace EfficiencyClassWebAPI.Models
{
    public class VariableTypeModel
    {
        public int Id { get; set; }
        public string VariableTypeName { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }

        public List<EF.VariableType> GetAllVariableType()
        {
            try
            {
                using (var vartype = new UnitofWork())
                {
                    List<EF.VariableType> result = vartype.VariableTypeRepository.GetAll().ToList();
                    return result;
                }
            }
            catch (Exception ex)
            {
                new Microsoft.ApplicationInsights.TelemetryClient().TrackException(ex);
                throw;
            }
        }
    }
}