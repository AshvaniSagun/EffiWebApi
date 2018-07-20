using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EF = EfficiencyClassWebAPI.EF;
using EfficiencyClassWebAPI.Repository;
using EfficiencyClassWebAPI.ResourceFiles;

namespace EfficiencyClassWebAPI.Models
{
    public class RangeValue
    {
        public int Id { get; set; }
        public int MarketId { get; set; }
        public string ECValue { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }

        public List<EF.RangeValue> GetAllRangeValue()
        {
            try
            {
                using (var range = new UnitofWork())
                {
                    List<EF.RangeValue> result = range.RangeValueRepository.GetAll().ToList();
                    return result;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}