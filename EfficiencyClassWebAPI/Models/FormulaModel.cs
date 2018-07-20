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
using System.Globalization;
using Newtonsoft.Json;
using ClosedXML.Excel;

namespace EfficiencyClassWebAPI.Models
{
    public class FormulaModel
    {
        [JsonProperty(PropertyName = "ID")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "MMID")]
        public long Mmid { get; set; }
        [JsonProperty(PropertyName = "FormulaDefinition")]
        public string FormulaDefinition { get; set; }
        [JsonProperty(PropertyName = "VariableId")]
        public long VariableId { get; set; }
        [JsonProperty(PropertyName = "VariableName")]
        public string VariableName { get; set; }
        [JsonProperty(PropertyName = "FormulaPriority")]
        public int FormulaPriority { get; set; }
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
        public FormulaModel()
        {
            uow = new UnitofWork();
        }

        public FormulaModel(UnitofWork unitofWork)
        {
            uow = unitofWork;
        }
        public List<FormulaModel> GetFormula(int mmid)
        {
            try
            {
                List<FormulaModel> lstFormula = new List<FormulaModel>();
                //using (var formula = new UnitofWork())
                //{
                List<EF.Formula> formulaList = GetFormulas(mmid);

                var result = (from f1 in uow.VariableRepository.GetAll()
                              join f2 in formulaList on f1.Id equals f2.VariableId
                              select new { f1.VariableName, f2.ID, f2.MMId, f2.FormulaDefinition, f2.VariableId, f2.FormulaPriority, f2.CreatedBy, f2.CreatedOn, f2.UpdatedBy, f2.UpdatedOn }
                            );
                foreach (var param in result)
                {
                    lstFormula.Add(new FormulaModel()
                    {
                        Id = param.ID,
                        Mmid = param.MMId,
                        FormulaDefinition = param.FormulaDefinition,
                        VariableId = param.VariableId,
                        VariableName = param.VariableName,
                        FormulaPriority = param.FormulaPriority,
                        CreatedBy = param.CreatedBy,
                        CreatedOn = param.CreatedOn,
                        UpdatedBy = param.UpdatedBy,
                        UpdatedOn = param.UpdatedOn,
                        IsPublished = true

                    });
                }
                var formulaListStaged = GetStagedFormulas(mmid);
                var resultStaged = (from f1 in uow.StagedVariableRepository.GetAll()
                                    join f2 in formulaListStaged on f1.Id equals f2.VariableId
                                    select new { f1.VariableName, f2.ID, f2.MMId, f2.FormulaDefinition, f2.VariableId, f2.FormulaPriority, f2.CreatedBy, f2.CreatedOn, f2.UpdatedBy, f2.UpdatedOn }
                           );
                foreach (var param in resultStaged)
                {
                    lstFormula.Add(new FormulaModel()
                    {
                        Id = param.ID,
                        Mmid = param.MMId,
                        FormulaDefinition = param.FormulaDefinition,
                        VariableId = param.VariableId,
                        VariableName = param.VariableName,
                        FormulaPriority = param.FormulaPriority,
                        CreatedBy = param.CreatedBy,
                        CreatedOn = param.CreatedOn,
                        UpdatedBy = param.UpdatedBy,
                        UpdatedOn = param.UpdatedOn,
                        IsPublished = false

                    });
                }

                //}
                lstFormula = lstFormula.OrderBy(x => x.FormulaPriority).ToList();
                uow.Dispose();
                return lstFormula;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(Resource.GetResxValueByName("CmnError") + ex.Message);
            }
        }

        public List<EF.StagedFormula> GetStagedFormulas(int mmid)
        {
            //using (var formula = new UnitofWork())
            //{
            return uow.StagedFormulaRepository.Find(x => x.MMId == mmid).ToList();
            //}
        }

        public List<EF.Formula> GetFormulas(int mmid)
        {
            //using (var formula = new UnitofWork())
            //{
            return uow.FormulaRepository.Find(x => x.MMId == mmid).ToList();
            //}
        }

        public int DeleteFormulaAndDependency(int formulaId)
        {
            try
            {

                //using (var formula = new UnitofWork())
                //{
                VariablesModel varObj = new VariablesModel();
                long varId = uow.StagedFormulaRepository.Find(x => x.ID == formulaId).Select(y => y.VariableId).SingleOrDefault();
                List<string> lstvariablename = varObj.CheckVariableAndFormulaDependency(varId);
                if (lstvariablename.Count() > 0)
                {
                    throw new InvalidOperationException("Cannot delete as Formula is being used in " + String.Join(",", lstvariablename) + " formulae");
                }
                EF.StagedFormula formulaDetails = uow.StagedFormulaRepository.Find(p => p.ID == formulaId).SingleOrDefault();
                List<EF.StagedFormulaDependencyDetail> lstFormulaDependency = new List<EF.StagedFormulaDependencyDetail>();
                lstFormulaDependency = uow.StagedFormulaDependencyRepository.Find(x => x.FormulaId == formulaId).ToList();
                uow.StagedFormulaDependencyRepository.RemoveRange(lstFormulaDependency);
                uow.StagedFormulaRepository.Remove(formulaDetails);
                uow.Dispose();
                return formulaId;
                //}
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(Resource.GetResxValueByName("CmnError") + ex.Message);
            }
        }

        public void UpdateFormulaDetails(IEnumerable<FormulaModel> formulaValue)
        {
            try
            {
                List<long> lstVariableId = new List<long>();
                List<string> lstVariables = new List<string>();
                List<string> lstFormulaFields = new List<string>();
                List<EF.StagedFormulaDependencyDetail> dependencyDetails = new List<EF.StagedFormulaDependencyDetail>();
                using (var formula = new UnitofWork())
                {
                    foreach (var item in formulaValue)
                    {
                        int formulaId = item.Id;
                        string formulaName = item.VariableName;
                        long varId = 0;
                        lstVariables = GetVariableList(item.FormulaDefinition);
                        foreach (var varName in lstVariables)
                        {
                            if (formula.StagedVariableRepository.Find(x => x.VariableName.ToUpper() == varName.ToUpper() && x.MMId == item.Mmid).Count() == 0 && formula.VariableRepository.Find(x => x.VariableName.ToUpper() == varName.ToUpper() && x.MMId == item.Mmid).Count() == 0)
                            {
                                throw new InvalidOperationException("Variable " + varName + " in formula definition does not exist");
                            }
                            if (varName == formulaName)
                            {
                                throw new InvalidOperationException("Formula name " + varName + " cannot be used in Definition");
                            }
                        }
                        if (!formula.StagedVariableRepository.Find(x => x.VariableName == formulaName && x.MMId == item.Mmid).Any())
                        {
                            var variableDetails = formula.VariableRepository.Find(x => x.VariableName == formulaName && x.MMId == item.Mmid).SingleOrDefault();
                            EF.StagedVariable publishedVariable = new EF.StagedVariable();
                            publishedVariable.Id = 0;
                            publishedVariable.VariableName = variableDetails.VariableName;
                            publishedVariable.VariableTypeId = variableDetails.VariableTypeId;
                            publishedVariable.MMId = variableDetails.MMId;
                            publishedVariable.VariableValue = variableDetails.VariableValue;
                            publishedVariable.CreatedBy = variableDetails.CreatedBy;
                            publishedVariable.CreatedOn = variableDetails.CreatedOn;
                            publishedVariable.UpdatedBy = variableDetails.UpdatedBy;
                            publishedVariable.UpdatedOn = variableDetails.UpdatedOn;
                            formula.StagedVariableRepository.Add(publishedVariable);
                            varId = publishedVariable.Id;
                            item.VariableId = varId;
                        }
                        if (formula.StagedFormulaRepository.Find(x => x.ID != formulaId && (x.MMId == item.Mmid && x.VariableId == item.VariableId)).ToList().Count > 0)
                        {
                            throw new Exception(Resource.GetResxValueByName("FormulaDuplicatemsg"));
                        }
                        if (formula.StagedFormulaRepository.Find(x => x.ID != formulaId && (x.MMId == item.Mmid && x.FormulaPriority == item.FormulaPriority)).ToList().Count > 0)
                        {
                            throw new InvalidOperationException("Multiple formulae cannot have same Priority");
                        }
                        long varIdPublished = formula.FormulaRepository.Find(x => x.MMId == item.Mmid && x.FormulaPriority == item.FormulaPriority).Select(y => y.VariableId).SingleOrDefault();
                        string varNamePublished = formula.VariableRepository.Find(x => x.Id == varIdPublished).Select(y => y.VariableName).SingleOrDefault();
                        if (varNamePublished != null && formulaName != varNamePublished)
                        {
                            throw new InvalidOperationException("Another formula with same Priority already exists in published details");
                        }

                    }
                    List<EF.StagedFormula> lstFormula = new List<EF.StagedFormula>();
                    foreach (var data in formulaValue)
                    {
                        lstVariableId.Add(data.VariableId);
                        lstFormula.Add(new EF.StagedFormula()
                        {
                            ID = data.Id,
                            MMId = data.Mmid,
                            VariableId = data.VariableId,
                            FormulaDefinition = data.FormulaDefinition,
                            FormulaPriority = data.FormulaPriority,
                            CreatedBy = data.CreatedBy,
                            CreatedOn = data.CreatedOn,
                            UpdatedBy = data.UpdatedBy,
                            UpdatedOn = data.UpdatedOn
                        });
                        dependencyDetails = formula.StagedFormulaDependencyRepository.Find(x => x.FormulaId == data.Id).ToList();
                    }
                    lstFormulaFields.Add("MMId");
                    lstFormulaFields.Add("FormulaDefinition");
                    lstFormulaFields.Add("VariableId");
                    lstFormulaFields.Add("FormulaPriority");
                    lstFormulaFields.Add("UpdatedBy");
                    lstFormulaFields.Add("UpdatedOn");
                    formula.StagedFormulaRepository.UpdateRange(lstFormula, lstFormulaFields);
                    formula.StagedFormulaDependencyRepository.RemoveRange(dependencyDetails);
                }
                AddFormulaDependencyDetailsStaged(lstVariableId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        #region Adding a formula and Formuladependency Details



        public void AddFormulaDetails(IEnumerable<FormulaModel> formulaValue)
        {
            try
            {
                List<long> lstVariableId = new List<long>();
                List<string> lstVariables = new List<string>();
                //using (var formula = new UnitofWork())
                //{
                foreach (var item in formulaValue)
                {
                    int formulaId = item.Id;
                    string formulaName = item.VariableName;
                    long varId = 0;
                    lstVariables = GetVariableList(item.FormulaDefinition);
                    foreach (var varName in lstVariables)
                    {
                        if (uow.StagedVariableRepository.Find(x => x.VariableName.ToUpper() == varName.ToUpper() && x.MMId == item.Mmid).Count() == 0 && uow.VariableRepository.Find(x => x.VariableName.ToUpper() == varName.ToUpper() && x.MMId == item.Mmid).Count() == 0)
                        {
                            throw new InvalidOperationException("Variable " + varName + " in formula definition does not exist");
                        }
                        if (varName == formulaName)
                        {
                            throw new InvalidOperationException("Formula name " + varName + " cannot be used in Definition");
                        }
                    }
                    if (!uow.StagedVariableRepository.Find(x => x.VariableName == formulaName && x.MMId == item.Mmid).Any())
                    {
                        var variableDetails = uow.VariableRepository.Find(x => x.VariableName == formulaName && x.MMId == item.Mmid).SingleOrDefault();
                        EF.StagedVariable publishedVariable = new EF.StagedVariable();
                        publishedVariable.Id = 0;
                        publishedVariable.VariableName = variableDetails.VariableName;
                        publishedVariable.VariableTypeId = variableDetails.VariableTypeId;
                        publishedVariable.MMId = variableDetails.MMId;
                        publishedVariable.VariableValue = variableDetails.VariableValue;
                        publishedVariable.CreatedBy = variableDetails.CreatedBy;
                        publishedVariable.CreatedOn = variableDetails.CreatedOn;
                        publishedVariable.UpdatedBy = variableDetails.UpdatedBy;
                        publishedVariable.UpdatedOn = variableDetails.UpdatedOn;
                        uow.StagedVariableRepository.Add(publishedVariable);
                        varId = publishedVariable.Id;
                        item.VariableId = varId;
                    }

                    if (uow.StagedFormulaRepository.Find(x => (x.MMId == item.Mmid && x.VariableId == item.VariableId)).ToList().Count > 0)
                    {
                        throw new Exception(Resource.GetResxValueByName("FormulaDuplicatemsg"));
                    }
                    if (uow.StagedFormulaRepository.Find(x => (x.MMId == item.Mmid && x.FormulaPriority == item.FormulaPriority)).ToList().Count > 0)
                    {
                        throw new InvalidOperationException("Multiple formulae cannot have same Priority");
                    }
                    long varIdPublished = uow.FormulaRepository.Find(x => x.MMId == item.Mmid && x.FormulaPriority == item.FormulaPriority).Select(y => y.VariableId).SingleOrDefault();
                    string varNamePublished = uow.VariableRepository.Find(x => x.Id == varIdPublished).Select(y => y.VariableName).SingleOrDefault();
                    if (varNamePublished != null && formulaName != varNamePublished)
                    {
                        throw new InvalidOperationException("Another formula with same Priority already exists in published details");
                    }
                }
                List<EF.StagedFormula> lstFormula = new List<EF.StagedFormula>();
                foreach (var data in formulaValue)
                {
                    lstVariableId.Add(data.VariableId);
                    lstFormula.Add(new EF.StagedFormula()
                    {
                        ID = data.Id,
                        MMId = data.Mmid,
                        VariableId = data.VariableId,
                        FormulaDefinition = data.FormulaDefinition,
                        FormulaPriority = data.FormulaPriority,
                        CreatedBy = data.CreatedBy,
                        CreatedOn = data.CreatedOn,
                        UpdatedBy = data.UpdatedBy,
                        UpdatedOn = data.UpdatedOn
                    });
                }
                uow.StagedFormulaRepository.AddRange(lstFormula);
                //}
                AddFormulaDependencyDetailsStaged(lstVariableId);
                uow.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception(Resource.GetResxValueByName("CmnError") + ex.Message);
            }
        }

        public void AddFormulaDependencyDetailsStaged(List<long> lstVariableId)
        {
            //using (var formulaDependency = new UnitofWork())
            //{
            foreach (var item in lstVariableId)
            {
                var details = uow.StagedFormulaRepository.Find(x => x.VariableId == item).FirstOrDefault();
                long mmid = details.MMId;
                var variableNames = GetVariableList(details.FormulaDefinition);
                if (variableNames.Count != 0)
                {
                    List<long> lstId = new List<long>();
                    foreach (var data in variableNames)
                    {
                        var contentInStaged = uow.StagedVariableRepository.Find(x => x.VariableName.ToUpper().Equals(data.Trim().ToUpper()) && x.MMId == mmid).FirstOrDefault();
                        if (contentInStaged != null)
                        {
                            lstId.Add(contentInStaged.Id);
                        }
                        else
                        {
                            var contentInPublished = uow.VariableRepository.Find(x => x.VariableName.ToUpper().Equals(data.Trim().ToUpper()) && x.MMId == mmid).FirstOrDefault();
                            EF.StagedVariable variableTobeAdded = new EF.StagedVariable();
                            variableTobeAdded.Id = 0;
                            variableTobeAdded.VariableName = contentInPublished.VariableName;
                            variableTobeAdded.VariableTypeId = contentInPublished.VariableTypeId;
                            variableTobeAdded.MMId = contentInPublished.MMId;
                            variableTobeAdded.VariableValue = contentInPublished.VariableValue;
                            variableTobeAdded.CreatedBy = contentInPublished.CreatedBy;
                            variableTobeAdded.CreatedOn = contentInPublished.CreatedOn;
                            variableTobeAdded.UpdatedBy = contentInPublished.UpdatedBy;
                            variableTobeAdded.UpdatedOn = contentInPublished.UpdatedOn;
                            uow.StagedVariableRepository.Add(variableTobeAdded);
                            lstId.Add(variableTobeAdded.Id);
                        }


                    }

                    List<EF.StagedFormulaDependencyDetail> lstdependency = new List<EF.StagedFormulaDependencyDetail>();
                    foreach (var param in lstId)
                    {
                        lstdependency.Add(new EF.StagedFormulaDependencyDetail()
                        {
                            FormulaId = details.ID,
                            VariableId = (int)param,
                            CreatedBy = details.CreatedBy,
                            CreatedOn = details.CreatedOn,
                            UpdatedBy = details.UpdatedBy,
                            UpdatedOn = details.UpdatedOn

                        });
                    }
                    uow.StagedFormulaDependencyRepository.AddRange(lstdependency);
                }
                else
                {
                    continue;
                }
            }
            //}
        }
        #endregion

        public TestFormula ParseFormula(TestFormula formulaTest)
        {
            try
            {
                using (var formula = new UnitofWork())
                {
                    List<string> formulaParam = GetVariableList(formulaTest.FormulaDefinition);
                    List<TestVariable> variables = new List<TestVariable>();
                    foreach (var item in formulaParam)
                    {
                        variables.Add(new TestVariable()
                        {
                            Name = item,
                            Value = 0
                        });
                    }
                    formulaTest.VariableList = variables;
                    return formulaTest;
                }

            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public List<string> GetVariableList(string formula)
        {
            var pattern = @"[\s\^\(\)*+/-]";
            decimal y;
            var result = Regex.Split(formula, pattern);
            var formulaParam = result.Where(x => !string.IsNullOrWhiteSpace(x) && !decimal.TryParse(x, out y)).Distinct().OrderByDescending(z => z.Length).ToList();
            return formulaParam;
        }

        public decimal TestFormulaFunctionality(TestFormula formulaTest)
        {
            try
            {
                FormulaDefinition = formulaTest.FormulaDefinition;
                foreach (var item in formulaTest.VariableList)
                {
                    FormulaDefinition = FormulaDefinition.Replace(item.Name, System.Convert.ToString(item.Value));
                }
                var result = Convert.ToDecimal(XLWorkbook.EvaluateExpr(FormulaDefinition));
                return result;
            }
            catch (Exception)
            {
                throw new Exception("Please enter valid formula");
            }
        }
    }

    public class TestFormula
    {
        [JsonProperty(PropertyName = "formulaDefinition")]
        public string FormulaDefinition { get; set; }
        [JsonProperty(PropertyName = "variableList")]
        public List<TestVariable> VariableList { get; set; }
    }

    public class TestVariable
    {
        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "value")]
        public decimal Value { get; set; }
    }

}