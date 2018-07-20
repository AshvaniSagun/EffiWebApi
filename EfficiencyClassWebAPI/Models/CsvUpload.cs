using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EF = EfficiencyClassWebAPI.EF;
using EfficiencyClassWebAPI.Repository;
using EfficiencyClassWebAPI.ResourceFiles;
using System.Text.RegularExpressions;
using System.Data;
using System.Transactions;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace EfficiencyClassWebAPI.Models
{
    public class CsvUpload
    {
        [JsonProperty(PropertyName = "EwId")]
        public long EwId { get; set; }
        [JsonProperty(PropertyName = "MMID")]
        public long Mmid { get; set; }
        [JsonProperty(PropertyName = "PNO12")]
        [RegularExpression("^([0-9 a-z A-Z])+$", ErrorMessage = "Special characters are not allowed in PNO12 value")]
        public string Pno12 { get; set; }
        [JsonProperty(PropertyName = "PWeight")]
        [RegularExpression("^([0-9 .])+$", ErrorMessage = "Weight shoud be decimal value")]
        public string PWeight { get; set; }
        [JsonProperty(PropertyName = "SegmentCo2")]
        [RegularExpression("^[0-9 .]+$", ErrorMessage = "Segment Co2 shoud be decimal value")]
        public string SegmentCo2 { get; set; }
        [JsonProperty(PropertyName = "CreatedBy")]
        public string CreatedBy { get; set; }
        [JsonProperty(PropertyName = "CreatedOn")]
        public Nullable<System.DateTime> CreatedOn { get; set; }
        [JsonProperty(PropertyName = "UpdatedBy")]
        public string UpdatedBy { get; set; }
        [JsonProperty(PropertyName = "UpdatedOn")]
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        [JsonProperty(PropertyName = "isPublished")]
        public bool IsPublished { get; set; }


        private UnitofWork uow;
        public CsvUpload()
        {
            uow = new UnitofWork();
        }

        public CsvUpload(UnitofWork unitofWork)
        {
            uow = unitofWork;
        }
        public void AddCsv(IEnumerable<CsvUpload> csvUpload)
        {
            try
            {
                if (csvUpload.Any(x => String.IsNullOrEmpty(x.PWeight) && String.IsNullOrEmpty(x.SegmentCo2)))
                {
                    throw new InvalidOperationException("Both Weight and Segment Co2 values cannot be null");
                }
                List<string> lstField = new List<string>();
                EF.StagedWeightSegmentCo2 pno12Details = new EF.StagedWeightSegmentCo2();
                lstField.Add("EwId");
                lstField.Add("MMID");
                lstField.Add("PNO12");
                lstField.Add("PWeight");
                lstField.Add("SegmentCo2");
                lstField.Add("UpdatedBy");
                lstField.Add("UpdatedOn");
                using (var csv = new UnitofWork())
                {
                    foreach (var data in csvUpload)
                    {
                        long pid = csv.StagedWeightSegmentCo2Repository.Find(x => (x.MMID == data.Mmid && x.PNO12 == data.Pno12)).Select(y => y.EwId).SingleOrDefault();
                        if (pid!=0)
                        {

                            pno12Details.EwId = pid;
                            pno12Details.MMID = data.Mmid;
                            pno12Details.PNO12 = data.Pno12;
                            pno12Details.PWeight = data.PWeight;
                            pno12Details.SegmentCo2 = data.SegmentCo2;
                            pno12Details.CreatedBy = data.CreatedBy;
                            pno12Details.CreatedOn = DateTime.Now;
                            pno12Details.UpdatedBy = data.UpdatedBy;
                            pno12Details.UpdatedOn = DateTime.Now;
                            using (var pno12 = new UnitofWork())
                            {
                                pno12.StagedWeightSegmentCo2Repository.Update(pno12Details, lstField);
                            }
                        }
                        else
                        {
                            pno12Details.EwId = data.EwId;
                            pno12Details.MMID = data.Mmid;
                            pno12Details.PNO12 = data.Pno12;
                            pno12Details.PWeight = data.PWeight;
                            pno12Details.SegmentCo2 = data.SegmentCo2;
                            pno12Details.CreatedBy = data.CreatedBy;
                            pno12Details.CreatedOn = DateTime.Now;
                            pno12Details.UpdatedBy = data.UpdatedBy;
                            pno12Details.UpdatedOn = DateTime.Now;
                            using (var pno12 = new UnitofWork())
                            {
                                pno12.StagedWeightSegmentCo2Repository.Add(pno12Details);
                            }
                            
                        }

                    }
                } 
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }

        }

        public List<EF.StagedWeightSegmentCo2> AddNewPno12Details(IEnumerable<CsvUpload> csvUpload)
        {
            try
            {
                List<EF.StagedWeightSegmentCo2> lstCsv = new List<EF.StagedWeightSegmentCo2>();
                using (var csv = new UnitofWork())
                {
                    foreach(var data in csvUpload)
                    {
                        if (csv.StagedWeightSegmentCo2Repository.Find(x => (x.MMID == data.Mmid && x.PNO12 == data.Pno12)).Any())
                        {
                            continue;
                        }
                        lstCsv.Add(new EF.StagedWeightSegmentCo2()
                        {
                            EwId = data.EwId,
                            MMID = data.Mmid,
                            PNO12 = data.Pno12,
                            PWeight = data.PWeight,
                            SegmentCo2 = data.SegmentCo2,
                            CreatedBy = data.CreatedBy,
                            CreatedOn = data.CreatedOn,
                            UpdatedBy = data.UpdatedBy,
                            UpdatedOn = data.UpdatedOn
                        });
                    }
                    csv.StagedWeightSegmentCo2Repository.AddRange(lstCsv);
                }
                return lstCsv;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(Resource.GetResxValueByName("CmnError") + ex.Message);
            }
        }
        public List<EF.StagedWeightSegmentCo2> UpdateCsvdata(IEnumerable<CsvUpload> csv)
        {
            try
            {
                List<EF.StagedWeightSegmentCo2> lstCSV = new List<EF.StagedWeightSegmentCo2>();
                List<string> lstField = new List<string>();
                using (var csvData = new UnitofWork())
                {
                    foreach (var value in csv)
                    {
                        if (csvData.StagedWeightSegmentCo2Repository.Find(x =>x.EwId!=value.EwId&&(x.MMID == value.Mmid && x.PNO12 == value.Pno12)).Any())
                        {
                            throw new InvalidOperationException(Resource.GetResxValueByName("PNO12Duplicatemsg"));
                        }
                        lstCSV.Add(new EF.StagedWeightSegmentCo2()
                        {
                            EwId = value.EwId,
                            MMID = value.Mmid,
                            PNO12 = value.Pno12,
                            PWeight = value.PWeight,
                            SegmentCo2 = value.SegmentCo2,
                            CreatedBy = value.CreatedBy,
                            CreatedOn = value.CreatedOn,
                            UpdatedBy = value.UpdatedBy,
                            UpdatedOn = DateTime.Now
                        });
                    }
                }
                lstField.Add("EwId");
                lstField.Add("MMID");
                lstField.Add("PNO12");
                lstField.Add("PWeight");
                lstField.Add("SegmentCo2");
                lstField.Add("UpdatedBy");
                lstField.Add("UpdatedOn");

                using (var csvobj = new UnitofWork())
                {
                    csvobj.StagedWeightSegmentCo2Repository.UpdateRange(lstCSV, lstField);
                }
                return lstCSV;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<CsvUpload> GetCsv(int mmid)
        {
            try
            {
                List<CsvUpload> pno12List = new List<CsvUpload>();
                //using (var csvUpload = new UnitofWork())
                //{
                    var pno12Details = GetWeightSegmentCo2s(mmid);
                    foreach (var param in pno12Details)
                    {
                        pno12List.Add(new CsvUpload(uow)
                        {
                            EwId = param.EwId,
                            Mmid = param.MMID,
                            Pno12 = param.PNO12,
                            PWeight = param.PWeight,
                            SegmentCo2 = param.SegmentCo2,
                            CreatedBy = param.CreatedBy,
                            CreatedOn = param.CreatedOn,
                            UpdatedBy = param.UpdatedBy,
                            UpdatedOn = param.UpdatedOn,
                            IsPublished = true
                        }
                        );
                    }
                    var pno12DetailsStaged = GetStagedWeightSegmentCo2s(mmid);
                    foreach (var param in pno12DetailsStaged)
                    {
                        pno12List.Add(new CsvUpload(uow)
                        {
                            EwId = param.EwId,
                            Mmid = param.MMID,
                            Pno12 = param.PNO12,
                            PWeight = param.PWeight,
                            SegmentCo2 = param.SegmentCo2,
                            CreatedBy = param.CreatedBy,
                            CreatedOn = param.CreatedOn,
                            UpdatedBy = param.UpdatedBy,
                            UpdatedOn = param.UpdatedOn,
                            IsPublished = false
                        }
                        );
                    }
                uow.Dispose();
                //}
                return pno12List;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(Resource.GetResxValueByName("CmnError") + ex.Message);
            }
        }

        public List<EF.StagedWeightSegmentCo2> GetStagedWeightSegmentCo2s(int mmid)
        {
            //using (var csv = new UnitofWork())
            //{
                return uow.StagedWeightSegmentCo2Repository.Find(x => x.MMID == mmid).ToList();
           // }
        }
        public List<EF.WeightSegmentCo2> GetWeightSegmentCo2s(int mmid)
        {
            //using (var csv = new UnitofWork())
            //{
                return uow.WeightSegmentCo2Repository.Find(x => x.MMID == mmid).ToList();
            //}
        }


        public int DeleteCsvUploadedDataDetails(int ewId)
        {
            try
            {
                int eId = 0;
                //using (var weightSegmentCo2 = new UnitofWork())
                //{
                    var lstWeightSegmentCo2 = uow.StagedWeightSegmentCo2Repository.Find(p => p.EwId == ewId).ToList();
                    if (lstWeightSegmentCo2.Count == 0)
                    {
                        throw new InvalidOperationException(Resource.GetResxValueByName("CmnDataNotFound"));
                    }
                    eId = (int)lstWeightSegmentCo2.First().EwId;
                uow.StagedWeightSegmentCo2Repository.RemoveRange(lstWeightSegmentCo2);
                // }
                uow.Dispose();
                return eId;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(Resource.GetResxValueByName("CmnError") + ex.Message);
            }
        }
    }
}