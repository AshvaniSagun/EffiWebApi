using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EfficiencyClassWebAPI.Repository;
using System.Net.Http;
using System.Threading.Tasks;

namespace EfficiencyClassWebAPI.Models
{
    public class PublishModel
    {
       readonly FormulaModel formulaObj = new FormulaModel();
        public void PublishMarketDetails(int MMID)
        {
            var result = ValidationForRangeValues(MMID);
            PublishPno12Details(MMID);
            PublishEfficiencyRangeDetails(result,MMID);
            PublishVariableDetails(MMID);
            FlushRedisCache();
        }
        public async Task<HttpResponseMessage> FlushRedisCache()
        {
            try
            {
                const string url = "/api/EfficiencyTestFunctionality/ClearRedisCache";
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.PostAsJsonAsync(System.Configuration.ConfigurationManager.AppSettings["EfficiencyClassServiceURL"] + url, string.Empty);
                return response;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }

        public void PublishPno12Details(int MMID)
        {
            List<EF.StagedWeightSegmentCo2> Pno12DetailsStaged = new List<EF.StagedWeightSegmentCo2>();
            List<EF.WeightSegmentCo2> pno12DetailsNew = new List<EF.WeightSegmentCo2>();
            List<EF.WeightSegmentCo2> pno12DetailsExist = new List<EF.WeightSegmentCo2>();
            using (var pno12 = new UnitofWork())
            {
                Pno12DetailsStaged = pno12.StagedWeightSegmentCo2Repository.Find(x => x.MMID == MMID).ToList();
                foreach (var item in Pno12DetailsStaged)
                {
                    long id = pno12.WeightSegmentCo2Repository.Find(x => x.MMID == item.MMID && x.PNO12 == item.PNO12).Select(y => y.EwId).SingleOrDefault();
                    if (id != 0)
                    {
                        pno12DetailsExist.Add(new EF.WeightSegmentCo2()
                        {
                            EwId = id,
                            MMID = item.MMID,
                            PNO12 = item.PNO12,
                            PWeight = item.PWeight,
                            SegmentCo2 = item.SegmentCo2,
                            CreatedBy = item.CreatedBy,
                            CreatedOn = DateTime.Now,
                            UpdatedBy = item.UpdatedBy,
                            UpdatedOn = DateTime.Now

                        });
                    }
                    else
                    {
                        pno12DetailsNew.Add(new EF.WeightSegmentCo2()
                        {
                            EwId = 0,
                            MMID = item.MMID,
                            PNO12 = item.PNO12,
                            PWeight = item.PWeight,
                            SegmentCo2 = item.SegmentCo2,
                            CreatedBy = item.CreatedBy,
                            CreatedOn = DateTime.Now,
                            UpdatedBy = item.UpdatedBy,
                            UpdatedOn = DateTime.Now

                        });
                    }
                }
                List<string> pno12Fields = new List<string>();
                pno12Fields.Add("EwId");
                pno12Fields.Add("MMID");
                pno12Fields.Add("PNO12");
                pno12Fields.Add("PWeight");
                pno12Fields.Add("SegmentCo2");
                pno12Fields.Add("CreatedBy");
                pno12Fields.Add("CreatedOn");
                pno12Fields.Add("UpdatedBy");
                pno12Fields.Add("UpdatedOn");

                using (var pno = new UnitofWork())
                {
                    pno.WeightSegmentCo2Repository.AddRange(pno12DetailsNew);
                    pno.WeightSegmentCo2Repository.UpdateRange(pno12DetailsExist, pno12Fields);
                }
                pno12.StagedWeightSegmentCo2Repository.RemoveRange(Pno12DetailsStaged);
            }
        }

        public void PublishEfficiencyRangeDetails(List<EF.StagedEfficiencyClassRange> rageDetailsStaged, int MMID)
        {
            List<EF.EfficiencyClassRange> rangeDetailsNew = new List<EF.EfficiencyClassRange>();
            List<EF.EfficiencyClassRange> rangeDetailsExist = new List<EF.EfficiencyClassRange>();
            using (var range = new UnitofWork())
            {
                List<EF.StagedEfficiencyClassRange> rageDetailsToBeDeleted = range.StagedEfficiencyClassRangeRepository.Find(x => x.MMID == MMID).ToList();
                foreach (var item in rageDetailsStaged)
                {
                    long id = range.EfficiencyClassRangeRepository.Find(x => x.MMID == item.MMID && x.ECValue == item.ECValue && x.VariableTypeId == item.VariableTypeId).Select(y => y.Id).SingleOrDefault();
                    if (id != 0)
                    {
                        rangeDetailsExist.Add(new EF.EfficiencyClassRange()
                        {
                            Id = id,
                            VariableTypeId = item.VariableTypeId,
                            StartRange = item.StartRange,
                            EndRange = item.EndRange,
                            MMID = item.MMID,
                            ECValue = item.ECValue,
                            CreatedBy = item.CreatedBy,
                            CreatedOn = DateTime.Now,
                            UpdatedBy = item.UpdatedBy,
                            UpdatedOn = DateTime.Now
                        });
                    }
                    else
                    {
                        rangeDetailsNew.Add(new EF.EfficiencyClassRange()
                        {
                            Id = 0,
                            VariableTypeId = item.VariableTypeId,
                            StartRange = item.StartRange,
                            EndRange = item.EndRange,
                            MMID = item.MMID,
                            ECValue = item.ECValue,
                            CreatedBy = item.CreatedBy,
                            CreatedOn = DateTime.Now,
                            UpdatedBy = item.UpdatedBy,
                            UpdatedOn = DateTime.Now
                        });
                    }
                }
                List<string> rangeFields = new List<string>();
                rangeFields.Add("VariableTypeId");
                rangeFields.Add("StartRange");
                rangeFields.Add("EndRange");
                rangeFields.Add("MMID");
                rangeFields.Add("ECValue");
                rangeFields.Add("CreatedBy");
                rangeFields.Add("CreatedOn");
                rangeFields.Add("UpdatedBy");
                rangeFields.Add("UpdatedOn");

                using (var eff = new UnitofWork())
                {
                    eff.EfficiencyClassRangeRepository.AddRange(rangeDetailsNew);
                    eff.EfficiencyClassRangeRepository.UpdateRange(rangeDetailsExist, rangeFields);
                }
                range.StagedEfficiencyClassRangeRepository.RemoveRange(rageDetailsToBeDeleted);
            }
        }
        public void PublishVariableDetails(int MMID)
        {
            List<EF.StagedVariable> VariableDetailsStaged = new List<EF.StagedVariable>();
            List<EF.Variable> variableDetailsNew = new List<EF.Variable>();
            List<EF.Variable> variableDetailsExist = new List<EF.Variable>();
            using (var variable = new UnitofWork())
            {
                VariableDetailsStaged = variable.StagedVariableRepository.Find(x => x.MMId == MMID).ToList();
                foreach (var item in VariableDetailsStaged)
                {
                    long id = variable.VariableRepository.Find(x => x.MMId == MMID && x.VariableName == item.VariableName).Select(y => y.Id).SingleOrDefault();
                    if (id != 0)
                    {
                        variableDetailsExist.Add(new EF.Variable()
                        {
                            Id = id,
                            VariableName = item.VariableName,
                            MMId = item.MMId,
                            VariableValue = item.VariableValue,
                            VariableTypeId = item.VariableTypeId,
                            CreatedBy = item.CreatedBy,
                            CreatedOn = DateTime.Now,
                            UpdatedBy = item.UpdatedBy,
                            UpdatedOn = item.UpdatedOn

                        });
                    }
                    else
                    {
                        variableDetailsNew.Add(new EF.Variable()
                        {
                            Id = 0,
                            VariableName = item.VariableName,
                            MMId = item.MMId,
                            VariableValue = item.VariableValue,
                            VariableTypeId = item.VariableTypeId,
                            CreatedBy = item.CreatedBy,
                            CreatedOn = DateTime.Now,
                            UpdatedBy = item.UpdatedBy,
                            UpdatedOn = item.UpdatedOn
                        });
                    }
                }
                List<string> variableFields = new List<string>();
                variableFields.Add("VariableName");
                variableFields.Add("MMId");
                variableFields.Add("VariableValue");
                variableFields.Add("VariableTypeId");
                variableFields.Add("CreatedBy");
                variableFields.Add("CreatedOn");
                variableFields.Add("UpdatedBy");
                variableFields.Add("UpdatedOn");

                using (var var = new UnitofWork())
                {
                    var.VariableRepository.AddRange(variableDetailsNew);
                    var.VariableRepository.UpdateRange(variableDetailsExist, variableFields);
                }
                PublishFormulaDetails(MMID);
                variable.StagedVariableRepository.RemoveRange(VariableDetailsStaged);
            }
        }

        public void PublishFormulaDetails(int MMID)
        {
            List<EF.StagedFormula> FormulaDetailsStaged = new List<EF.StagedFormula>();
            List<EF.Formula> formulaDetailsNew = new List<EF.Formula>();
            List<EF.Formula> formulaDetailsExist = new List<EF.Formula>();
            List<long> fidList = new List<long>();
            List<EF.StagedFormulaDependencyDetail> formulaDependencyList = new List<EF.StagedFormulaDependencyDetail>();
            using (var formula = new UnitofWork())
            {
                FormulaDetailsStaged = formula.StagedFormulaRepository.Find(x => x.MMId == MMID).ToList();
                foreach (var item in FormulaDetailsStaged)
                {
                    formulaDependencyList.AddRange(formula.StagedFormulaDependencyRepository.Find(x => x.FormulaId == item.ID).ToList());
                    string varName = formula.StagedVariableRepository.Find(x => x.Id == item.VariableId).Select(y => y.VariableName).SingleOrDefault();
                    long varId = formula.VariableRepository.Find(x => x.MMId == MMID && x.VariableName == varName).Select(y => y.Id).SingleOrDefault();
                    long id = formula.FormulaRepository.Find(x => x.MMId == MMID && x.VariableId == varId).Select(y => y.ID).SingleOrDefault();
                    if (id != 0)
                    {
                        fidList.Add(id);
                        formulaDetailsExist.Add(new EF.Formula()
                        {
                            ID = Convert.ToInt32(id),
                            MMId = item.MMId,
                            FormulaDefinition = item.FormulaDefinition,
                            VariableId = varId,
                            FormulaPriority = item.FormulaPriority,
                            CreatedBy = item.CreatedBy,
                            CreatedOn = DateTime.Now,
                            UpdatedBy = item.UpdatedBy,
                            UpdatedOn = item.UpdatedOn
                        });
                    }
                    else
                    {
                        formulaDetailsNew.Add(new EF.Formula()
                        {
                            ID = 0,
                            MMId = item.MMId,
                            FormulaDefinition = item.FormulaDefinition,
                            VariableId = varId,
                            FormulaPriority = item.FormulaPriority,
                            CreatedBy = item.CreatedBy,
                            CreatedOn = DateTime.Now,
                            UpdatedBy = item.UpdatedBy,
                            UpdatedOn = item.UpdatedOn
                        });
                    }
                }

                List<string> formulaFields = new List<string>();
                formulaFields.Add("MMId");
                formulaFields.Add("FormulaDefinition");
                formulaFields.Add("VariableId");
                formulaFields.Add("FormulaPriority");
                formulaFields.Add("CreatedBy");
                formulaFields.Add("CreatedOn");
                formulaFields.Add("UpdatedBy");
                formulaFields.Add("UpdatedOn");

                using (var data = new UnitofWork())
                {
                    data.FormulaRepository.AddRange(formulaDetailsNew);
                    data.FormulaRepository.UpdateRange(formulaDetailsExist, formulaFields);
                }
                foreach (var item in formulaDetailsNew)
                {
                    fidList.Add(item.ID);
                }
                UpdateFormulaDependencyDetails(fidList, MMID);
                formula.StagedFormulaDependencyRepository.RemoveRange(formulaDependencyList);
                formula.StagedFormulaRepository.RemoveRange(FormulaDetailsStaged);
            }


        }
        public void UpdateFormulaDependencyDetails(List<long> formulaIds, int MMID)
        {
            using (var formula = new UnitofWork())
            {
                            
                foreach (var fid in formulaIds)
                {
                    List<long> varIdList = new List<long>();
                    List<string> VarList = new List<string>();
                    List<EF.FormulaDependencyDetail> formulaDependencyAddLst = new List<EF.FormulaDependencyDetail>();
                    var formulaDetails = formula.FormulaRepository.Find(x => x.ID == fid).SingleOrDefault();
                    VarList = formulaObj.GetVariableList(formulaDetails.FormulaDefinition);

                    foreach (var variable in VarList)
                    {
                        var id = formula.VariableRepository.Find(x => x.VariableName == variable && x.MMId == MMID).Select(y => y.Id).SingleOrDefault();
                        varIdList.Add(id);
                    }

                    foreach (var vid in varIdList)
                    {
                        formulaDependencyAddLst.Add(new EF.FormulaDependencyDetail()
                        {
                            Id = 0,
                            FormulaId = Convert.ToInt32(fid),
                            VariableId = vid,
                            CreatedBy = formulaDetails.CreatedBy,
                            CreatedOn = DateTime.Now,
                            UpdatedBy = formulaDetails.UpdatedBy,
                            UpdatedOn = formulaDetails.UpdatedOn
                        });
                    }

                    var formulaDependencyRemoveList = formula.FormulaDependencyDetail.Find(x => x.FormulaId == fid).ToList();
                    formula.FormulaDependencyDetail.RemoveRange(formulaDependencyRemoveList);
                    formula.FormulaDependencyDetail.AddRange(formulaDependencyAddLst);
                }                
            }
        }

        public string DetailsTobePublished(int MMId)
        {
            List<string> detailsMessage = new List<string>();
            string marketName;
            int year;
            string message;
            using (var details = new UnitofWork())
            {
                var marketDetails = details.Market2MarketTypeParameterGroupRepository.Find(x => x.MMId == MMId).SingleOrDefault();
                marketName = marketDetails.Market.MarketName;
                year = marketDetails.MYear;
                var rangeDetails = details.StagedEfficiencyClassRangeRepository.Find(x => x.MMID == MMId).ToList();
                if (rangeDetails.Any())
                {
                    detailsMessage.Add("Efficiency Range");
                }
                var formulaDetails = details.StagedFormulaRepository.Find(x => x.MMId == MMId).ToList();
                if (formulaDetails.Any())
                {
                    detailsMessage.Add("Formula");
                }
                var variableDetails = details.StagedVariableRepository.Find(x => x.MMId == MMId).ToList();
                if (variableDetails.Any())
                {
                    detailsMessage.Add("Variable");
                }
                var pno12Details = details.StagedWeightSegmentCo2Repository.Find(x => x.MMID == MMId).ToList();
                if (pno12Details.Any())
                {
                    detailsMessage.Add("PNO12 Details");
                }
            }
            message = string.Join(",", detailsMessage);
            message = "Do you want to publish the Efficiency Class data from " + message + " for " + marketName + " of " + year + "?";
            return message;

        }

        public List<EF.StagedEfficiencyClassRange> ValidationForRangeValues(int mmid)
        {
            try
            {
                List<EF.StagedEfficiencyClassRange> rangeToBeAdded = new List<EF.StagedEfficiencyClassRange>();
                using (var range = new UnitofWork())
                {
                    var rangeStaged = range.StagedEfficiencyClassRangeRepository.Find(x => x.MMID == mmid).ToList();
                    var rangePublished = range.EfficiencyClassRangeRepository.Find(x => x.MMID == mmid).ToList();
                    var rangeListPublished = rangePublished.Where(x => !rangeStaged.Any(y => y.ECValue == x.ECValue && y.VariableTypeId == x.VariableTypeId)).ToList();
                    rangeToBeAdded.AddRange(rangeStaged);
                    foreach (var item in rangeListPublished)
                    {
                        rangeToBeAdded.Add(new EF.StagedEfficiencyClassRange()
                        {
                            Id = 0,
                            MMID = item.MMID,
                            VariableTypeId = item.VariableTypeId,
                            StartRange = item.StartRange,
                            EndRange = item.EndRange,
                            ECValue = item.ECValue,
                            CreatedBy = item.CreatedBy,
                            CreatedOn = item.CreatedOn,
                            UpdatedBy = item.UpdatedBy,
                            UpdatedOn = item.UpdatedOn
                        });
                    }

                    foreach (var check in rangeToBeAdded)
                    {
                        var rangeToBeChecked = rangeToBeAdded.Where(x => x.ECValue != check.ECValue && x.VariableTypeId == check.VariableTypeId).ToList();
                        var rangeexists = rangeToBeChecked.Find(x => (x.StartRange <= check.StartRange && x.EndRange >= check.StartRange) || (x.StartRange <= check.EndRange && x.EndRange >= check.EndRange));
                        if (rangeexists != null)
                        {
                            throw new InvalidOperationException("Enter valid range, the range is conflicting with range " + rangeexists.StartRange + " and " + rangeexists.EndRange);
                        }
                        rangeexists = rangeToBeChecked.Find(x => (x.StartRange >= check.StartRange && x.StartRange <= check.EndRange) || (x.EndRange >= check.StartRange && x.EndRange <= check.EndRange));
                        if (rangeexists != null)
                        {
                            throw new InvalidOperationException("Enter valid range, " + rangeexists.StartRange + " and " + rangeexists.EndRange + " has conflicting range values");
                        }
                    }
                }
                return rangeToBeAdded;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}