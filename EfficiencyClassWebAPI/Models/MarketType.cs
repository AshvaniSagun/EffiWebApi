using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EF = EfficiencyClassWebAPI.EF;
using EfficiencyClassWebAPI.Repository;
using EfficiencyClassWebAPI.ResourceFiles;

namespace EfficiencyClassWebAPI.Models
{
    public class MarketType
    {
        #region MarketType Property
        public int Id { get; set; }
        public string MarketTypeName { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        #endregion

        #region Get All MarketType Data
        public List<EF.MarketType> GetAllMarketType()
        {
            try
            {
                using (var marketTypeRepo = new UnitofWork())
                {
                    List<EF.MarketType> result = marketTypeRepo.MarketTypeRepository.GetAll().ToList();
                    return result;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

    }
}