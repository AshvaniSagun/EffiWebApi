using EfficiencyClassWebAPI.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EfficiencyClassWebAPI.Models
{
    public class EfficiencyRange
    {
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }
        [JsonProperty(PropertyName = "MMID")]
        public long Mmid { get; set; }
        [JsonProperty(PropertyName = "FuelTypeId")]
        public int? FuelTypeId { get; set; }
        [JsonProperty(PropertyName = "FuelTypeName")]
        public string FuelTypeName { get; set; }
        [JsonProperty(PropertyName = "startRange")]
        public decimal StartRange { get; set; }
        [JsonProperty(PropertyName = "endRange")]
        public decimal EndRange { get; set; }
        [JsonProperty(PropertyName = "ecValue")]
        public string EcValue { get; set; }
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
        public EfficiencyRange()
        {
            uow = new UnitofWork();
        }
        
        public EfficiencyRange(UnitofWork unitofWork)
        {
            uow = unitofWork;
        }

        #region get
        public List<EfficiencyRange> Get(long mmId)
        {
            try
            {
                List<EfficiencyRange> lstefficiencyRangedata = new List<EfficiencyRange>();
                //using (var efficiencyRange = new UnitofWork(uow))
                //{
                    var rangeDetails = GetEfficiencyClassRanges(mmId);
                    var result = from e1 in uow.VariableTypeRepository.GetAll()
                                 join e2 in rangeDetails on e1.Id equals e2.VariableTypeId
                                 select new { e2.ECValue, e2.MMID, e2.Id, e2.VariableTypeId, e1.VariableTypeName, e2.StartRange, e2.EndRange, e2.CreatedBy, e2.CreatedOn, e2.UpdatedBy, e2.UpdatedOn };
                    foreach (var value in result)
                        {

                            lstefficiencyRangedata.Add(new EfficiencyRange(uow)
                            {
                                Id = value.Id,
                                FuelTypeId = value.VariableTypeId,
                                FuelTypeName = value.VariableTypeName,
                                 Mmid = value.MMID,
                                StartRange = value.StartRange,
                                EndRange = value.EndRange,
                                EcValue=value.ECValue,
                                CreatedBy = value.CreatedBy,
                                CreatedOn = value.CreatedOn,
                                UpdatedBy = value.UpdatedBy,
                                UpdatedOn = value.UpdatedOn,
                                IsPublished=true
                            });
                        }
                    var rangeDetailsStaged = GetStagedEfficiencyClassRanges(mmId);
                    var resultStaged = from e1 in uow.VariableTypeRepository.GetAll()
                                 join e2 in rangeDetailsStaged on e1.Id equals e2.VariableTypeId
                                 select new { e2.ECValue, e2.MMID, e2.Id, e2.VariableTypeId, e1.VariableTypeName, e2.StartRange, e2.EndRange, e2.CreatedBy, e2.CreatedOn, e2.UpdatedBy, e2.UpdatedOn };
                    foreach (var value in resultStaged)
                    {

                        lstefficiencyRangedata.Add(new EfficiencyRange(uow)
                        {
                            Id = value.Id,
                            FuelTypeId = value.VariableTypeId,
                            FuelTypeName = value.VariableTypeName,
                            Mmid = value.MMID,
                            StartRange = value.StartRange,
                            EndRange = value.EndRange,
                            EcValue = value.ECValue,
                            CreatedBy = value.CreatedBy,
                            CreatedOn = value.CreatedOn,
                            UpdatedBy = value.UpdatedBy,
                            UpdatedOn = value.UpdatedOn,
                            IsPublished = false
                        });
                    }
                //}
                uow.Dispose();

                lstefficiencyRangedata = lstefficiencyRangedata.OrderBy(y=>y.StartRange).ToList();
                lstefficiencyRangedata = lstefficiencyRangedata.OrderBy(x => x.FuelTypeId).ToList();
             

                return lstefficiencyRangedata;
            }
            catch(Exception ex)
            {
                new Microsoft.ApplicationInsights.TelemetryClient().TrackException(ex);
                throw;
            }
        }

        public List<EF.StagedEfficiencyClassRange> GetStagedEfficiencyClassRanges(long mmid)
        {
            return uow.StagedEfficiencyClassRangeRepository.Find(x => x.MMID == mmid).ToList(); 
        }

        public List<EF.EfficiencyClassRange> GetEfficiencyClassRanges(long mmid)
        {
            return uow.EfficiencyClassRangeRepository.Find(x => x.MMID == mmid).ToList();
        }
        #endregion
       

        #region update
       
        public List<EF.StagedEfficiencyClassRange> UpdateSpecificFuelTypeEfficiencyRange(IEnumerable<EfficiencyRange> ECRangeValue)
        {
            try
            {
                List<EF.StagedEfficiencyClassRange> lstEfficiencyRange = new List<EF.StagedEfficiencyClassRange>();
                using (var efficiencyRange = new UnitofWork())
                {
                    List<string> lstField = new List<string>();
                    foreach (var item in ECRangeValue)
                    {
                        if (item.StartRange > item.EndRange)
                        {
                            throw new InvalidOperationException("Start range should be less than End range");
                        }
                        if (uow.StagedEfficiencyClassRangeRepository.Find(x => x.Id != item.Id && (x.MMID == item.Mmid && x.VariableTypeId == item.FuelTypeId && (x.ECValue == item.EcValue || x.StartRange == item.StartRange || x.EndRange == item.EndRange))).ToList().Count > 0)
                        {
                            throw new InvalidOperationException(Resource.GetResxValueByName("EfficiencyRangeDuplicatemsg"));
                        }
                        var rangeList = uow.StagedEfficiencyClassRangeRepository.Find(x => x.MMID == item.Mmid && x.VariableTypeId==item.FuelTypeId).ToList();
                       
                        var rangeexists = rangeList.Find(x =>x.Id!=item.Id && ((item.StartRange >= x.StartRange && item.StartRange <= x.EndRange) || (item.StartRange >= x.StartRange && item.EndRange <= x.EndRange)));
                        if (rangeexists != null)
                        {
                            throw new InvalidOperationException("Enter valid range, the range is conflicting with range " + rangeexists.StartRange + " and " + rangeexists.EndRange);
                        }
                        rangeexists = rangeList.Find(x=>x.Id!=item.Id && ((x.StartRange >= item.StartRange && x.StartRange <= item.EndRange) || (x.EndRange >= item.StartRange && x.EndRange <= item.EndRange)));
                        if (rangeexists != null)
                        {
                            throw new InvalidOperationException("Enter valid range, " + rangeexists.StartRange + "and" + rangeexists.EndRange + " has conflicting range values");
                        }
                        lstEfficiencyRange.Add(new EF.StagedEfficiencyClassRange()
                        {
                            Id = item.Id,
                            VariableTypeId=item.FuelTypeId,
                            MMID = item.Mmid,
                            StartRange = item.StartRange,
                            EndRange = item.EndRange,
                            ECValue=item.EcValue,
                            CreatedBy = item.CreatedBy,
                            CreatedOn = item.CreatedOn,
                            UpdatedBy = item.UpdatedBy,
                            UpdatedOn = DateTime.UtcNow
                        });
                    }
                    lstField.Add("VariableTypeId");
                    lstField.Add("MMID");
                    lstField.Add("StartRange");
                    lstField.Add("EndRange");
                    lstField.Add("ECValue");
                    lstField.Add("UpdatedBy");
                    lstField.Add("UpdatedOn");
                using (var range =new UnitofWork())
                {
                    range.StagedEfficiencyClassRangeRepository.UpdateRange(lstEfficiencyRange, lstField);
                    
                }
                }
                return lstEfficiencyRange;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(Resource.GetResxValueByName("CmnError") + ex.Message);
            }
        }
        #endregion

        #region add
        public List<EF.StagedEfficiencyClassRange> AddSpecificFuelTypeEfficiencyRange(IEnumerable<EfficiencyRange> ECRangeValue)
        {
            try
            {
                List<EF.StagedEfficiencyClassRange> lstEfficiencyRange = new List<EF.StagedEfficiencyClassRange>();
                //using (var efficiencyRange = new UnitofWork())
                //{
                    foreach (var item in ECRangeValue)
                    {
                        if(item.StartRange>item.EndRange)
                        {
                            throw new InvalidOperationException("Start range should be less than End range");
                        }

                        if (uow.StagedEfficiencyClassRangeRepository.Find(x => x.Id == item.Id || (x.MMID == item.Mmid && x.VariableTypeId==item.FuelTypeId &&( x.ECValue==item.EcValue ||  x.StartRange == item.StartRange || x.EndRange == item.EndRange))).ToList().Count > 0)
                        {
                            throw new InvalidOperationException(Resource.GetResxValueByName("EfficiencyRangeDuplicatemsg"));
                        }
                        var rangeList = uow.StagedEfficiencyClassRangeRepository.Find(x => x.MMID == item.Mmid && x.VariableTypeId==item.FuelTypeId).ToList();

                        var rangeexists = rangeList.Find(x => (x.StartRange <= item.StartRange && x.EndRange >= item.StartRange) || (x.StartRange <= item.EndRange && x.EndRange >= item.EndRange));
                        if (rangeexists != null)
                        {
                            throw new InvalidOperationException("Enter valid range, the range is conflicting with range " + rangeexists.StartRange + " and " + rangeexists.EndRange);
                        }
                        rangeexists = rangeList.Find(x => (x.StartRange >= item.StartRange && x.StartRange <= item.EndRange) || (x.EndRange >= item.StartRange && x.EndRange <= item.EndRange));
                        if (rangeexists != null)
                        {
                            throw new InvalidOperationException("Enter valid range, " + rangeexists.StartRange + " and " + rangeexists.EndRange + " has conflicting range values");
                        }
                       
                        lstEfficiencyRange.Add(new EF.StagedEfficiencyClassRange()
                        {
                            Id = item.Id,
                            VariableTypeId=item.FuelTypeId,
                            MMID = (int)item.Mmid,
                            StartRange = item.StartRange,
                            EndRange = item.EndRange,
                            ECValue=item.EcValue,
                            CreatedBy = item.CreatedBy,
                            CreatedOn = item.CreatedOn,
                            UpdatedBy = item.UpdatedBy,
                            UpdatedOn = DateTime.UtcNow
                        });
                    }
                uow.StagedEfficiencyClassRangeRepository.AddRange(lstEfficiencyRange);
                //}
                uow.Dispose();
                return lstEfficiencyRange;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(Resource.GetResxValueByName("CmnError") + ex.Message);
            }
        }
        #endregion

        #region delete
        public void DeleteEfficiencyRange(int Id,int MMID)
        {
            try
            {
                //using (var efficiencyRange = new UnitofWork())
                //{
                        List<EF.StagedEfficiencyClassRange> lstSpecFuelTypeEfficiencyRange ;
                        lstSpecFuelTypeEfficiencyRange = uow.StagedEfficiencyClassRangeRepository.Find(p => p.Id == Id).ToList();
                        foreach (var param in lstSpecFuelTypeEfficiencyRange)
                        {
                             uow.StagedEfficiencyClassRangeRepository.Remove(param);
                        }
                uow.Dispose();
                //}
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error while loading Data. Error : " + ex.Message);
            }
        }
                #endregion

         #region CopyEfficiencyClass
       
        public void CopyFormulaDetails(long fromMMID, long toMMID)
        {
            using (var formulaDetails = new UnitofWork())
            {
                List<EF.StagedVariable> lstVariables = new List<EF.StagedVariable>();
                var variableDetails = formulaDetails.VariableRepository.Find(x => x.MMId == fromMMID).ToList();
                foreach (var item in variableDetails)
                {
                    lstVariables.Add(new EF.StagedVariable
                    {
                        Id = item.Id,
                        VariableName = item.VariableName,
                        VariableValue = item.VariableValue,
                        MMId = toMMID,
                        VariableTypeId = item.VariableTypeId,
                        CreatedBy = item.CreatedBy,
                        CreatedOn = DateTime.Now,
                        UpdatedBy = item.UpdatedBy,
                        UpdatedOn = DateTime.Now
                    });
                }
                formulaDetails.StagedVariableRepository.AddRange(lstVariables);
                List<EF.StagedFormula> lstFormulae = new List<EF.StagedFormula>();
                var formulaeDetails = formulaDetails.FormulaRepository.Find(x => x.MMId == fromMMID).ToList();
                foreach (var item in formulaeDetails)
                {
                    string varName = formulaDetails.VariableRepository.Find(x => x.Id == item.VariableId).Select(y => y.VariableName).SingleOrDefault();
                    long varId = formulaDetails.StagedVariableRepository.Find(x => x.VariableName == varName && x.MMId == toMMID).Select(y => y.Id).SingleOrDefault();
                    lstFormulae.Add(new EF.StagedFormula
                    {
                        ID = item.ID,
                        FormulaDefinition = item.FormulaDefinition,
                        FormulaPriority = item.FormulaPriority,
                        MMId = toMMID,
                        VariableId = varId,
                        CreatedBy = item.CreatedBy,
                        CreatedOn = DateTime.Now,
                        UpdatedBy = item.UpdatedBy,
                        UpdatedOn = DateTime.Now
                    });
                }
                formulaDetails.StagedFormulaRepository.AddRange(lstFormulae);
                List<long> variableIDlist = formulaDetails.StagedFormulaRepository.Find(x => x.MMId == toMMID).Select(y => y.VariableId).ToList();
                FormulaModel fomulaModelObject = new FormulaModel();
                fomulaModelObject.AddFormulaDependencyDetailsStaged(variableIDlist);
            }
        }
        #endregion

        public bool CheckModelYearExistance(long mmId,int modelYear)
        {
            try
            {
                using (var year = new UnitofWork())
                {
                    var marketDetails = year.Market2MarketTypeParameterGroupRepository.Find(x => x.MMId == mmId).SingleOrDefault();
                    int marketId = marketDetails.MarketId;
                    var modelYearExists = year.Market2MarketTypeParameterGroupRepository.Find(x => x.MarketId == marketId && x.MYear == modelYear).SingleOrDefault();
                    if(modelYearExists==null)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch(Exception ex)
            {
                new Microsoft.ApplicationInsights.TelemetryClient().TrackException(ex);
                throw;
            }
        }

        public string MessageforCopyDetails(long mmId, int modelYear)
        {
            try
            {
                //using (var year = new UnitofWork())
                //{
                    var marketDetails = uow.Market2MarketTypeParameterGroupRepository.Find(x => x.MMId == mmId).SingleOrDefault();
                    int fromyear = marketDetails.MYear;
                    if(uow.FormulaRepository.Find(x => x.MMId == mmId).Any()|| uow.VariableRepository.Find(x => x.MMId == mmId).Any()|| uow.EfficiencyClassRangeRepository.Find(x => x.MMID == mmId).Any()|| uow.WeightSegmentCo2Repository.Find(x => x.MMID == mmId).Any())
                    {
                        return "Do you want to copy the Efficiency Class data from " + fromyear + " to " + modelYear + "? ALERT:On Copy, the " + modelYear + " unpublished data will be permanently replaced by the " + fromyear + " data ";
                    }
                    else
                    {
                        throw new InvalidOperationException("No published data is present in "+ fromyear+" to copy");
                    }
                    
               // }
            }
            catch (Exception ex)
            {
                new Microsoft.ApplicationInsights.TelemetryClient().TrackException(ex);
                throw;
            }
        }
        public string CopyMarketDetails(long mmid,int modelYear)
        {
            using (var market = new UnitofWork())
            {
                long toMMId;
                int genericMarketTypeId;
                string responseMessage = "";
                EF.Market2MarketTypeParameterGroup marketDetails = market.Market2MarketTypeParameterGroupRepository.Find(x => x.MMId == mmid).SingleOrDefault();
                var currentMarketDetails = market.Market2MarketTypeParameterGroupRepository.Find(x => x.MarketId == marketDetails.MarketId && x.MYear == modelYear).SingleOrDefault();
                toMMId = currentMarketDetails.MMId;
                genericMarketTypeId = market.MarketTypeRepository.Find(x => x.MarketTypeName == "Generic").Select(y => y.Id).SingleOrDefault();
                if(currentMarketDetails.MarketTypeId== genericMarketTypeId)
                {
                    DeleteMarketDetails(toMMId);
                    CopyRangeDetails(mmid, toMMId);
                    responseMessage = "Efficiency Range details copied successfully";
                }
                else
                {
                    DeleteMarketDetails(toMMId);
                    CopyRangeDetails(mmid, toMMId);
                    CopyPno12Details(mmid, toMMId);
                    CopyFormulaDetails(mmid, toMMId);
                    responseMessage = "PNO12 details , Efficiency Range and Formula copied successfully";
                }
                return responseMessage;
            }
        }

        public void CopyRangeDetails(long fromMMID, long toMMID)
        {
            using (var range = new UnitofWork())
            {
                List<EF.StagedEfficiencyClassRange> lstRangeDetails = new List<EF.StagedEfficiencyClassRange>();
                var effRangeDetails = range.EfficiencyClassRangeRepository.Find(x => x.MMID == fromMMID).ToList();
                foreach (var item in effRangeDetails)
                {
                    lstRangeDetails.Add(new EF.StagedEfficiencyClassRange()
                    {
                        Id = 0,
                        MMID = toMMID,
                        VariableTypeId = item.VariableTypeId,
                        StartRange = item.StartRange,
                        EndRange = item.EndRange,
                        ECValue = item.ECValue,
                        CreatedBy = item.CreatedBy,
                        CreatedOn = DateTime.UtcNow,
                        UpdatedBy = item.UpdatedBy,
                        UpdatedOn = DateTime.UtcNow

                    });
                }
                range.StagedEfficiencyClassRangeRepository.AddRange(lstRangeDetails);
            }
        }

        public void CopyPno12Details(long fromMMID, long toMMID)
        {
            using (var pno12 = new UnitofWork())
            {
                List<EF.StagedWeightSegmentCo2> lstPno12Details = new List<EF.StagedWeightSegmentCo2>();
                var pno12Details = pno12.WeightSegmentCo2Repository.Find(x => x.MMID == fromMMID).ToList();
                foreach (var item in pno12Details)
                {
                    lstPno12Details.Add(new EF.StagedWeightSegmentCo2()
                    {
                        EwId = item.EwId,
                        MMID = toMMID,
                        PNO12 = item.PNO12,
                        PWeight = item.PWeight,
                        SegmentCo2 = item.SegmentCo2,
                        CreatedBy = item.CreatedBy,
                        CreatedOn = DateTime.UtcNow,
                        UpdatedBy = item.UpdatedBy,
                        UpdatedOn = DateTime.UtcNow

                    });
                }

                pno12.StagedWeightSegmentCo2Repository.AddRange(lstPno12Details);
            }
        }

        public void DeleteMarketDetails(long mmid)
        {
            using (var market = new UnitofWork())
            {
                List<EF.StagedFormula> lstFomula = market.StagedFormulaRepository.Find(x => x.MMId == mmid).ToList();
                List<EF.StagedFormulaDependencyDetail> lstFormulaDependency = new List<EF.StagedFormulaDependencyDetail>();
                foreach (var item in lstFomula)
                {
                    lstFormulaDependency.AddRange(market.StagedFormulaDependencyRepository.Find(x => x.FormulaId == item.ID).ToList());
                }
                List<EF.StagedVariable> lstVariable = market.StagedVariableRepository.Find(x => x.MMId == mmid).ToList();
                List<EF.StagedEfficiencyClassRange> lstEffRange = market.StagedEfficiencyClassRangeRepository.Find(x => x.MMID == mmid).ToList();
                List<EF.StagedWeightSegmentCo2> lstPno12Details = market.StagedWeightSegmentCo2Repository.Find(x => x.MMID == mmid).ToList();
                market.StagedFormulaDependencyRepository.RemoveRange(lstFormulaDependency);
                market.StagedFormulaRepository.RemoveRange(lstFomula);
                market.StagedVariableRepository.RemoveRange(lstVariable);
                market.StagedEfficiencyClassRangeRepository.RemoveRange(lstEffRange);
                market.StagedWeightSegmentCo2Repository.RemoveRange(lstPno12Details);
            }
               
        }

    }
}