using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EF = EfficiencyClassWebAPI.EF;
using EfficiencyClassWebAPI.Repository;
using EfficiencyClassWebAPI.ResourceFiles;

namespace EfficiencyClassWebAPI.Models
{
    public class WeightSegmentCO2
    {
        public long EwId { get; set; }
        public int MarketId { get; set; }
        public int ModelYear { get; set; }
        public string Pno12 { get; set; }
        public string PWeight { get; set; }
        public string SegmentCo2 { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public List<EF.WeightSegmentCo2> AddSegmentCO2(IEnumerable<WeightSegmentCO2> CO2Value)
        {

            try
            {
                List<EF.WeightSegmentCo2> lstsegmentCO2 = new List<EF.WeightSegmentCo2>();
                using (var CO2 = new UnitofWork())
                {

                    foreach (var item in CO2Value)
                    {


                        long segmentCO2id = item.EwId;
                        long marketmodelyear = item.ModelYear;
                        if (CO2.WeightSegmentCO2Repository.Find(x => x.EwId == segmentCO2id || (x.ModelYear == marketmodelyear)).ToList().Count > 0)
                        {
                            throw new InvalidOperationException(Resource.GetResxValueByName("SegmentCO2Duplicatemsg"));
                        }
                        else
                        {

                            lstsegmentCO2.Add(new EF.WeightSegmentCo2()
                            {
                                EwId = item.EwId,
                                ModelYear = item.ModelYear,
                                MarketId = item.MarketId,
                                PWeight=item.PWeight,
                                PNO12=item.Pno12,
                                SegmentCo2=item.SegmentCo2,
                                CreatedBy = item.CreatedBy,
                                CreatedOn = DateTime.UtcNow,
                                UpdatedBy = item.UpdatedBy,
                                UpdatedOn = DateTime.UtcNow

                            });
                        }
                    }
                    CO2.WeightSegmentCO2Repository.AddRange(lstsegmentCO2);

                }
                return lstsegmentCO2;


            }
            catch (Exception ex)
            {

                throw new InvalidOperationException(Resource.GetResxValueByName("CmnError") + ex.Message);
            }
        }
    }
}