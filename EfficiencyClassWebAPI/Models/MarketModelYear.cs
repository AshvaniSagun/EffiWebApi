using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EF = EfficiencyClassWebAPI.EF;
using EfficiencyClassWebAPI.Repository;
using EfficiencyClassWebAPI.ResourceFiles;

namespace EfficiencyClassWebAPI.Models
{
    public class MarketModelYear
    {
        public int Id { get; set; }
        public int MarketId { get; set; }
        public int ModelYear { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }

        public List<EF.MarketModelYear> GetModelYear()
        {
            try
            {
                using (var modelyear = new UnitofWork())
                {
                    List<EF.MarketModelYear> result = modelyear.MarketModelYearRepository.GetAll().ToList();
                    return result;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<EF.MarketModelYear> GetModelYear(int id)
        {
            try
            {
                using (var modelyear = new UnitofWork())
                {
                    return modelyear.MarketModelYearRepository.Find(p => p.MarketId == id).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error while loading Data. Error : " + ex.Message);
            }
        }
        public List<EF.MarketModelYear> AddModelyear(IEnumerable<MarketModelYear> modelValue)
        {

            try
            {
                List<EF.MarketModelYear> lstmodelyear = new List<EF.MarketModelYear>();
                using (var year = new UnitofWork())
                {

                    foreach (var item in modelValue)
                    {
                        long modelyearid = item.Id;
                        long marketmodelyear = item.ModelYear;
                        long marketId = item.MarketId;
                        if (year.MarketModelYearRepository.Find(x => x.MMYearId == modelyearid || (x.MarketId == marketId && x.ModelYear == marketmodelyear)).ToList().Count > 0)
                        {
                            throw new InvalidOperationException(Resource.GetResxValueByName("VariableDuplicatemsg"));
                        }
                        else
                        {

                            lstmodelyear.Add(new EF.MarketModelYear()
                            {
                                MMYearId = item.Id,
                                ModelYear = item.ModelYear,
                                MarketId = item.MarketId,
                                CreatedBy = item.CreatedBy,
                                CreatedOn = DateTime.UtcNow,
                                UpdatedBy = item.UpdatedBy,
                                UpdatedOn = DateTime.UtcNow

                            });
                        }
                    }
                    year.MarketModelYearRepository.AddRange(lstmodelyear);

                }
                return lstmodelyear;

            }
            catch (Exception ex)
            {

                throw new InvalidOperationException(Resource.GetResxValueByName("CmnError") + ex.Message);
            }
        }   

        public int DeleteModelYear(int modelid)
        {
            try
            {
                int DeleteyearId = 0;
                using (var year = new UnitofWork())
                {
                    var mid = modelid;
                    List<EF.MarketModelYear> lstModelYear = new List<EF.MarketModelYear>();
                    lstModelYear = year.MarketModelYearRepository.Find(p => p.MMYearId == mid).ToList();
                    if (lstModelYear.Count == 0)
                    {
                        throw new InvalidOperationException(Resource.GetResxValueByName("CmnDataNotFound"));
                    }
                    DeleteyearId = (int)lstModelYear.First().MMYearId;
                    year.MarketModelYearRepository.RemoveRange(lstModelYear);

                }
                return DeleteyearId;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(Resource.GetResxValueByName("CmnError") + ex.Message);
            }
        }


    }
}