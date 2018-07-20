using EfficiencyClassWebAPI.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace EfficiencyClassWebAPI.Models
{
    public class MarketDataModel
    {
        #region
        [JsonProperty(PropertyName = "MMId")]
        public long Mmid { get; set; }
        [JsonProperty(PropertyName = "Marketid")]
        public int Marketid { get; set; }
        [JsonProperty(PropertyName = "SpecMarket")]
        public int SpecMarket { get; set; }
        [JsonProperty(PropertyName = "MarketName")]
        public string MarketName { get; set; }
        [JsonProperty(PropertyName = "MarketTypeId")]
        public int MarketTypeId { get; set; }
        [JsonProperty(PropertyName = "MarketTypeName")]
        public string MarketTypeName { get; set; }
        [JsonProperty(PropertyName = "ParameterGroupId")]
        public int? ParameterGroupId { get; set; }
        [JsonProperty(PropertyName = "ParameterGroupName")]
        public string ParameterGroupName { get; set; }
        [JsonProperty(PropertyName = "Year")]
        public int Year { get; set; }
        [JsonProperty(PropertyName = "CreatedBy")]
        public string CreatedBy { get; set; }
        [JsonProperty(PropertyName = "CreatedOn")]
        public Nullable<System.DateTime> CreatedOn { get; set; }
        [JsonProperty(PropertyName = "UpdatedBy")]
        public string UpdatedBy { get; set; }
        [JsonProperty(PropertyName = "UpdatedOn")]
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        #endregion
        private UnitofWork uow;
        public MarketDataModel()
        {
            uow = new UnitofWork();
        }

        public MarketDataModel(UnitofWork unitofWork)
        {
            uow = unitofWork;
        }
        public List<EF.Market2MarketTypeParameterGroup> GetMarket2MarketTypeParameterGroup()
        {
            using (var marketRepo = new UnitofWork())
            {
                var marketDetails = marketRepo.Market2MarketTypeParameterGroupRepository.GetAll().ToList();
                return marketDetails;
            }

        }

        public List<MarketDataModel> GetMarketData(string cdsid)
        {
            List<MarketDataModel> lstMarketDetails = new List<MarketDataModel>();
            try
            {
                //using (var marketRepo = new UnitofWork())
                //{
                    var uid = uow.UserDetailRepository.Find(x => x.CDSID == cdsid).Select(y => y.Id).SingleOrDefault();
                    var marketIds = uow.UserMarketRepository.Find(x => x.UserId == uid).Select(y => y.MarketId).ToList();
                    List<EF.Market2MarketTypeParameterGroup> marketDetails = new List<EF.Market2MarketTypeParameterGroup>();
                    foreach (var id in marketIds)
                    {
                        marketDetails.AddRange(uow.Market2MarketTypeParameterGroupRepository.Find(x => x.MarketId == id).ToList());
                    }
                    var lstMarketType = uow.MarketTypeRepository.GetAll();
                    var lstMarket = uow.MarketRepository.GetAll();
                    var result = (from t1 in marketDetails
                                  join t2 in lstMarketType on t1.MarketTypeId equals t2.Id
                                  join t4 in lstMarket on t1.MarketId equals t4.Id
                                  select new
                                  {
                                      t2.MarketTypeName,
                                      t4.SpecMarket,
                                      t4.MarketName,
                                      t1.MarketId,
                                      t1.MarketTypeId,
                                      t1.MMId,
                                      t1.MYear,
                                      t1.CreatedBy,
                                      t1.CreatedOn,
                                      t1.UpdatedBy,
                                      t1.UpdatedOn
                                  }).ToList();

                    foreach (var value in result)
                    {
                        lstMarketDetails.Add(new MarketDataModel()
                        {
                            Mmid = value.MMId,
                            Marketid = value.MarketId,
                            SpecMarket = value.SpecMarket,
                            MarketName = value.MarketName,
                            MarketTypeId = value.MarketTypeId,
                            MarketTypeName = value.MarketTypeName,
                            Year = value.MYear,
                            CreatedBy = value.CreatedBy,
                            CreatedOn = value.CreatedOn,
                            UpdatedBy = value.UpdatedBy,
                            UpdatedOn = value.UpdatedOn
                        });
                    }
                uow.Dispose();
                return lstMarketDetails;
               // }
            }
            catch (Exception ex )
            {
                new Microsoft.ApplicationInsights.TelemetryClient().TrackException(ex);
                throw;
            }
        }

        public List<EF.Market2MarketTypeParameterGroup> UpdateMarketData(IEnumerable<MarketDataModel> marketValue)
        {
            try
            {
                using (var market = new UnitofWork())
                {
                    foreach (var data in marketValue)
                    {
                        long mmId = data.Mmid;
                        long marketid = data.Marketid;
                        long marketTypeId = data.MarketTypeId;
                        long? parameterGroupId = data.ParameterGroupId;
                        long modelYear = data.Year;
                        if (market.Market2MarketTypeParameterGroupRepository.Find(x => x.MMId == mmId).ToList().Count == 0)
                        {
                            throw new Exception();
                        }
                        else if (market.Market2MarketTypeParameterGroupRepository.Find(x =>x.MMId!= mmId && x.MarketId == marketid && x.MYear == modelYear).ToList().Count > 0)
                        {
                            throw new Exception(Resource.GetResxValueByName("MarketDuplicatemsg"));
                        }
                    }
                }

                List<EF.Market2MarketTypeParameterGroup> lstmarket = new List<EF.Market2MarketTypeParameterGroup>();
                List<string> lstField = new List<string>();
                foreach (var value in marketValue)
                {
                    lstmarket.Add(new EF.Market2MarketTypeParameterGroup()
                    {
                        MMId=value.Mmid,
                        MarketId=value.Marketid,
                        MarketTypeId=value.MarketTypeId,
                        ParameterGroupId=value.ParameterGroupId,
                        MYear=value.Year,
                        CreatedBy=value.CreatedBy,
                        CreatedOn=value.CreatedOn,
                        UpdatedBy = value.UpdatedBy,
                        UpdatedOn = DateTime.UtcNow
                    });
                }

                lstField.Add("MMId");
                lstField.Add("MarketId");
                lstField.Add("MarketTypeId");
                lstField.Add("ParameterGroupId");
                lstField.Add("MYear");
                lstField.Add("UpdatedBy");
                lstField.Add("UpdatedOn");

                using (var marketobj = new UnitofWork())
                {
                    marketobj.Market2MarketTypeParameterGroupRepository.UpdateRange(lstmarket, lstField);
                }
                return lstmarket;
            }
            catch (Exception ex)
            {
                new Microsoft.ApplicationInsights.TelemetryClient().TrackException(ex);
                throw new Exception(ex.Message);
            }
        }

        public void AddMarketData(IEnumerable<MarketDataModel> marketValue)
        {
            try
            {
                //using (var market = new UnitofWork())
                //{
                    foreach (var item in marketValue)
                    {
                        long marketid = item.Marketid;
                        long marketTypeId = item.MarketTypeId;
                        long? parameterGroupId = item.ParameterGroupId;
                        long modelYear = item.Year;
                        if (uow.Market2MarketTypeParameterGroupRepository.Find(x => x.MarketId == marketid && x.MYear == modelYear).ToList().Count > 0)
                        {
                            throw new Exception(Resource.GetResxValueByName("MarketDuplicatemsg"));
                        }
                    }
                    List<EF.Market2MarketTypeParameterGroup> lstmarket = new List<EF.Market2MarketTypeParameterGroup>();
                    foreach (var item in marketValue)
                    {
                        lstmarket.Add(new EF.Market2MarketTypeParameterGroup()
                        {
                            MMId=item.Mmid,
                            MarketId=item.Marketid,
                            MarketTypeId=item.MarketTypeId,
                            ParameterGroupId=item.ParameterGroupId,
                            MYear=item.Year,
                            CreatedBy = item.CreatedBy,
                            CreatedOn = item.CreatedOn,
                            UpdatedBy = item.UpdatedBy,
                            UpdatedOn = item.UpdatedOn
                        });
                    }
                uow.Market2MarketTypeParameterGroupRepository.AddRange(lstmarket);
                uow.Dispose();
                // }
            }
            catch (DbEntityValidationException e)
            {
                new Microsoft.ApplicationInsights.TelemetryClient().TrackException(e);
                throw new Exception(e.Message);
            }
            catch (Exception ex)
            {
                new Microsoft.ApplicationInsights.TelemetryClient().TrackException(ex);
                throw new Exception(ex.Message);
            }
        }

        public void DeleteMarket(long mmid)
        {
            try
            {
               //using (var market = new UnitofWork())
               //{
                    List<EF.Market2MarketTypeParameterGroup> lstmarket = new List<EF.Market2MarketTypeParameterGroup>();
                    lstmarket = uow.Market2MarketTypeParameterGroupRepository.Find(p => p.MMId == mmid).ToList();
                    if (lstmarket.Count == 0)
                    {
                         throw new Exception();
                    }
                    List<EF.Formula> lstFomula = uow.FormulaRepository.Find(x => x.MMId == mmid).ToList();
                    List<EF.FormulaDependencyDetail> lstFormulaDependency = new List<EF.FormulaDependencyDetail>();
                    foreach (var item in lstFomula)
                    {
                        lstFormulaDependency.AddRange(uow.FormulaDependencyDetail.Find(x => x.FormulaId == item.ID).ToList());
                    }
                    List<EF.Variable> lstVariable = uow.VariableRepository.Find(x => x.MMId == mmid).ToList();
                    List<EF.WeightSegmentCo2> lstPNO12Details = uow.WeightSegmentCo2Repository.Find(x => x.MMID == mmid).ToList();
                    List<EF.EfficiencyClassRange> efficiencyClassRanges = uow.EfficiencyClassRangeRepository.Find(x => x.MMID == mmid).ToList();
                uow.FormulaDependencyDetail.RemoveRange(lstFormulaDependency);
                uow.FormulaRepository.RemoveRange(lstFomula);
                uow.VariableRepository.RemoveRange(lstVariable);
                uow.EfficiencyClassRangeRepository.RemoveRange(efficiencyClassRanges);
                uow.WeightSegmentCo2Repository.RemoveRange(lstPNO12Details);

                    List<EF.StagedFormula> lstStagedFomula = uow.StagedFormulaRepository.Find(x => x.MMId == mmid).ToList();
                    List<EF.StagedFormulaDependencyDetail> lstStagedFormulaDependency = new List<EF.StagedFormulaDependencyDetail>();
                    foreach (var item in lstStagedFomula)
                    {
                        lstStagedFormulaDependency.AddRange(uow.StagedFormulaDependencyRepository.Find(x => x.FormulaId == item.ID).ToList());
                    }
                    List<EF.StagedVariable> lstStagedVariable = uow.StagedVariableRepository.Find(x => x.MMId == mmid).ToList();
                    List<EF.StagedWeightSegmentCo2> lstStagedPno12Details = uow.StagedWeightSegmentCo2Repository.Find(x => x.MMID == mmid).ToList();
                    List<EF.StagedEfficiencyClassRange> stagedEfficiencyClassRanges = uow.StagedEfficiencyClassRangeRepository.Find(x => x.MMID == mmid).ToList();
                uow.StagedFormulaDependencyRepository.RemoveRange(lstStagedFormulaDependency);
                uow.StagedFormulaRepository.RemoveRange(lstStagedFomula);
                uow.StagedVariableRepository.RemoveRange(lstStagedVariable);
                uow.StagedEfficiencyClassRangeRepository.RemoveRange(stagedEfficiencyClassRanges);
                uow.StagedWeightSegmentCo2Repository.RemoveRange(lstStagedPno12Details);

                    uow.Market2MarketTypeParameterGroupRepository.RemoveRange(lstmarket);
                uow.Dispose();

                //}

            }

            catch (Exception ex)
            {
                new Microsoft.ApplicationInsights.TelemetryClient().TrackException(ex);
                throw new Exception(ex.Message);
            }
        }
    }
        public class MarketDataList
        {
            [JsonProperty(PropertyName = "MMId")]
            public int Mmid { get; set; }
            [JsonProperty(PropertyName = "marketId")]
            public int MarketId { get; set; }
        }
    }
