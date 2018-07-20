using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EF = EfficiencyClassWebAPI.EF;
using EfficiencyClassWebAPI.Repository;
using EfficiencyClassWebAPI.ResourceFiles;

namespace EfficiencyClassWebAPI.Models
{
    public class CO2RangeModel
    {
        public long Co2Id { get; set; }
        public int MMID { get; set; }
        public Nullable<decimal> StartRange { get; set; }
        public Nullable<decimal> EndRange { get; set; }
        public Nullable<int> ValueId { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }

        public List<EF.CommonCo2Range> Get()
        {
            try
            {
                using (var co2 = new UnitofWork())
                {
                    return co2.CommonCo2RangeRepository.GetAll().ToList();
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error while loading Data. Error : " + ex.Message);
            }
        }
        public List<EF.CommonCo2Range> GetCommonCO2(int id)
        {
            try
            {
                using (var CO2 = new UnitofWork())
                {
                    return CO2.CommonCo2RangeRepository.Find(p => p.Co2Id == id).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error while loading Data. Error : " + ex.Message);
            }
        }

        public List<EF.CommonCo2Range> AddCO2Range(IEnumerable<CO2RangeModel> CO2Value)
        {

            try
            {
                List<EF.CommonCo2Range> lstCOMMONCO2 = new List<EF.CommonCo2Range>();
                using (var CO2 = new UnitofWork())
                {

                    foreach (var item in CO2Value)
                    {


                        //long COMMONCO2id = item.Co2Id;
                        //long marketmodelyear = item.ModelYear;
                        //if (CO2.CommonCO2RangeRepository.Find(x => x.Co2Id == COMMONCO2id || (x.ModelYear == marketmodelyear)).ToList().Count > 0)
                        //{
                        //    throw new InvalidOperationException(Resource.GetResxValueByName("CO2RangeDuplicatemsg"));
                        //}
                        //else
                        //{

                        lstCOMMONCO2.Add(new EF.CommonCo2Range()
                        {
                            Co2Id = item.Co2Id,
                            MMId = item.MMID,
                            StartRange = item.StartRange,
                            EndRange = item.EndRange,
                            ValueId = item.ValueId,
                            CreatedBy = item.CreatedBy,
                            CreatedOn = DateTime.UtcNow,
                            UpdatedBy = item.UpdatedBy,
                            UpdatedOn = DateTime.UtcNow

                        });
                        // }
                        // }
                        CO2.CommonCo2RangeRepository.AddRange(lstCOMMONCO2);
                    }

                }
                return lstCOMMONCO2;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(Resource.GetResxValueByName("CmnError") + ex.Message);
            }
        }
        public List<EF.CommonCo2Range> UpdateCO2Range(IEnumerable<CO2RangeModel> CO2Data)
        {
            try
            {

                List<string> lstField = new List<string>();
                List<EF.CommonCo2Range> lstCOMMONCO2 = new List<EF.CommonCo2Range>();

                using (var CO2Repo = new UnitofWork())
                {
                    foreach (var CO2 in CO2Data)
                    {
                        lstCOMMONCO2.Add(new EF.CommonCo2Range()
                        {
                            Co2Id = CO2.Co2Id,
                            MMId = CO2.MMID,
                            StartRange=CO2.StartRange,
                            EndRange=CO2.EndRange,
                            ValueId=CO2.ValueId,
                            CreatedBy = CO2.CreatedBy,
                            CreatedOn = DateTime.UtcNow,
                            UpdatedBy = CO2.UpdatedBy,
                            UpdatedOn = DateTime.UtcNow
                        });
                    }
                    lstField.Add("Co2StartRange");
                    lstField.Add("Co2EndRange");
                    lstField.Add("ValueId");
                    lstField.Add("MarketId");
                    lstField.Add("ModelYear");
                    lstField.Add("UpdatedBy");
                    lstField.Add("UpdatedOn");
                    using (var CO2obj = new UnitofWork())
                    {
                        CO2obj.CommonCo2RangeRepository.UpdateRange(lstCOMMONCO2, lstField);
                    }
                }
                return lstCOMMONCO2;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public int DeleteCO2Range(int CO2id)
        {
            try
            {
                int DeleteCO2Id = 0;
                using (var CO2 = new UnitofWork())
                {
                    var mid = CO2id;
                    List<EF.CommonCo2Range> lstCOMMONCO2 = new List<EF.CommonCo2Range>();
                    lstCOMMONCO2 = CO2.CommonCo2RangeRepository.Find(p => p.Co2Id == mid).ToList();
                    if (lstCOMMONCO2.Count == 0)
                    {
                        throw new InvalidOperationException(Resource.GetResxValueByName("CmnDataNotFound"));
                    }
                    DeleteCO2Id = (int)lstCOMMONCO2.First().Co2Id;
                    CO2.CommonCo2RangeRepository.RemoveRange(lstCOMMONCO2);

                }
                return DeleteCO2Id;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(Resource.GetResxValueByName("CmnError") + ex.Message);
            }
        }

    }
}