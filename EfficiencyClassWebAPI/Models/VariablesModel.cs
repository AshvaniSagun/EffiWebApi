using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EF = EfficiencyClassWebAPI.EF;
using EfficiencyClassWebAPI.Repository;
using EfficiencyClassWebAPI.ResourceFiles;
using Newtonsoft.Json;

namespace EfficiencyClassWebAPI.Models
{
    public class VariablesModel
    {
        [JsonProperty(PropertyName = "Id")]
        public long Id { get; set; }
        [JsonProperty(PropertyName = "VariableName")]
        public string VariableName { get; set; }
        [JsonProperty(PropertyName = "VariableValue")]
        public string VariableValue { get; set; }
        [JsonProperty(PropertyName = "MMId")]
        public long Mmid { get; set; }
        [JsonProperty(PropertyName = "VariableTypeId")]
        public int VariableTypeId { get; set; }
        [JsonProperty(PropertyName = "VariableTypeName")]
        public string VariableTypeName { get; set; }
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
        public VariablesModel()
        {
            uow = new UnitofWork();
        }

        public VariablesModel(UnitofWork unitofWork)
        {
            uow = unitofWork;
        }

        public List<VariablesModel> Get(int mmid)
        {
            try
            {
                List<VariablesModel> lstVariabledata = new List<VariablesModel>();
                //using (var variable = new UnitofWork())
                //{
                var lstVariable = GetVariableDetails(mmid);
                var result = (from v1 in uow.VariableTypeRepository.GetAll()
                              join v2 in lstVariable on v1.Id equals v2.VariableTypeId
                              select new { v2.Id, v2.VariableName, v2.VariableValue, v2.VariableTypeId, v2.MMId, v1.VariableTypeName, v2.CreatedBy, v2.CreatedOn, v2.UpdatedBy, v2.UpdatedOn }).ToList();
                foreach (var value in result)
                {
                    lstVariabledata.Add(new VariablesModel()
                    {
                        Id = value.Id,
                        VariableName = value.VariableName,
                        VariableValue = value.VariableValue,
                        Mmid = value.MMId,
                        VariableTypeId = value.VariableTypeId,
                        VariableTypeName = value.VariableTypeName,
                        CreatedBy = value.CreatedBy,
                        CreatedOn = value.CreatedOn,
                        UpdatedBy = value.UpdatedBy,
                        UpdatedOn = value.UpdatedOn,
                        IsPublished = true
                    });
                }
                var lstVariablesStaged = GetStagedVariableDetails(mmid);
                var resultStaged = (from v1 in uow.VariableTypeRepository.GetAll()
                                    join v2 in lstVariablesStaged on v1.Id equals v2.VariableTypeId
                                    select new { v2.Id, v2.VariableName, v2.VariableValue, v2.VariableTypeId, v2.MMId, v1.VariableTypeName, v2.CreatedBy, v2.CreatedOn, v2.UpdatedBy, v2.UpdatedOn }).ToList();
                foreach (var value in resultStaged)
                {
                    lstVariabledata.Add(new VariablesModel()
                    {
                        Id = value.Id,
                        VariableName = value.VariableName,
                        VariableValue = value.VariableValue,
                        Mmid = value.MMId,
                        VariableTypeId = value.VariableTypeId,
                        VariableTypeName = value.VariableTypeName,
                        CreatedBy = value.CreatedBy,
                        CreatedOn = value.CreatedOn,
                        UpdatedBy = value.UpdatedBy,
                        UpdatedOn = value.UpdatedOn,
                        IsPublished = false
                    });
                }
                uow.Dispose();
                return lstVariabledata;
            }
            //}
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error while loading Data. Error : " + ex.Message);
            }
        }

        public List<EF.Variable> GetVariableDetails(int mmid)
        {
            //using (var variable = new UnitofWork())
            //{
            return uow.VariableRepository.Find(x => x.MMId == mmid).ToList();
            //}
        }
        public List<EF.StagedVariable> GetStagedVariableDetails(int mmid)
        {
            //using (var variable = new UnitofWork())
            //{
            return uow.StagedVariableRepository.Find(x => x.MMId == mmid).ToList();
            //}
        }
        public List<EF.StagedVariable> AddVariable(IEnumerable<VariablesModel> variableValue)
        {
            try
            {
                List<EF.StagedVariable> lstvariable = new List<EF.StagedVariable>();
                //using (var variable = new UnitofWork())
                //{
                foreach (var item in variableValue)
                {
                    string variableName = item.VariableName;
                    long variableid = item.Id;
                    if (uow.StagedVariableRepository.Find(x => (x.VariableName == variableName && x.MMId == item.Mmid)).ToList().Count > 0)
                    {
                        throw new InvalidOperationException(Resource.GetResxValueByName("VariableDuplicatemsg"));
                    }
                    if (uow.StagedVariableRepository.Find(x => (x.VariableTypeId == 2 && x.MMId == item.Mmid)).ToList().Count > 0 && item.VariableTypeId == 2)
                    {
                        throw new InvalidOperationException("Multiple Result type variables not allowed");
                    }
                    if (uow.VariableRepository.Find(x => x.VariableName != item.VariableName && (x.VariableTypeId == 2 && x.MMId == item.Mmid)).ToList().Count > 0 && item.VariableTypeId == 2)
                    {
                        throw new InvalidOperationException("Result type variable already exists in published details for different Variable");
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(item.VariableValue))
                            item.VariableValue = Convert.ToString(DBNull.Value);
                        lstvariable.Add(new EF.StagedVariable()
                        {
                            Id = item.Id,
                            VariableName = item.VariableName,
                            VariableValue = item.VariableValue,
                            VariableTypeId = item.VariableTypeId,
                            MMId = item.Mmid,
                            CreatedBy = item.CreatedBy,
                            CreatedOn = item.CreatedOn,
                            UpdatedBy = item.UpdatedBy,
                            UpdatedOn = DateTime.UtcNow
                        });
                    }
                }
                uow.StagedVariableRepository.AddRange(lstvariable);
                uow.Dispose();
                //}
                return lstvariable;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(Resource.GetResxValueByName("CmnError") + ex.Message);
            }
        }

        public List<EF.StagedVariable> UpdateVariable(IEnumerable<VariablesModel> variableValue)
        {
            try
            {
                List<EF.StagedVariable> lstvariable = new List<EF.StagedVariable>();
                List<string> lstField = new List<string>();
                using (var variable = new UnitofWork())
                {
                    foreach (var item in variableValue)
                    {
                        long vId = item.Id;
                        string variableName = item.VariableName;
                        if (variable.StagedVariableRepository.Find(x => x.Id == vId).ToList().Count == 0)
                        {
                            throw new InvalidOperationException(Resource.GetResxValueByName("CmnDataNotFound"));
                        }
                        else if (variable.StagedVariableRepository.Find(x => x.Id != vId && (x.VariableName == variableName && x.MMId == item.Mmid)).ToList().Count > 0)
                        {
                            throw new InvalidOperationException(Resource.GetResxValueByName("VariableDuplicatemsg"));
                        }
                        else if (variable.StagedVariableRepository.Find(x => (x.VariableTypeId == 2 && x.MMId == item.Mmid)).ToList().Count > 0 && item.VariableTypeId == 2)
                        {
                            throw new InvalidOperationException("Multiple Result type variables not allowed");
                        }
                        if (variable.VariableRepository.Find(x => x.VariableName != item.VariableName && (x.VariableTypeId == 2 && x.MMId == item.Mmid)).ToList().Count > 0 && item.VariableTypeId == 2)
                        {
                            throw new InvalidOperationException("Result type variable already exists in published details for different Variable");
                        }
                        else if (variable.StagedVariableRepository.Find(x => x.Id == vId).Select(x => x.VariableName).First() != item.VariableName)
                        {
                            List<string> lstvariablename = CheckVariableAndFormulaDependency(vId);
                            if (lstvariablename.Any())
                            {
                                throw new InvalidOperationException("Cannot delete as Variable is being used in " + String.Join(",", lstvariablename) + " formulae");
                            }
                        }
                    }
                }
                foreach (var item in variableValue)
                {
                    lstvariable.Add(new EF.StagedVariable()
                    {
                        Id = item.Id,
                        VariableName = item.VariableName,
                        VariableValue = item.VariableValue,
                        VariableTypeId = item.VariableTypeId,
                        MMId = item.Mmid,
                        CreatedBy = item.CreatedBy,
                        CreatedOn = item.CreatedOn,
                        UpdatedBy = item.UpdatedBy,
                        UpdatedOn = DateTime.UtcNow
                    });
                }
                lstField.Add("VariableName");
                lstField.Add("VariableValue");
                lstField.Add("VariableTypeId");
                lstField.Add("MMId");
                lstField.Add("UpdatedBy");
                lstField.Add("UpdatedOn");

                using (var variableobj = new UnitofWork())
                {
                    variableobj.StagedVariableRepository.UpdateRange(lstvariable, lstField);
                }
                return lstvariable;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(Resource.GetResxValueByName("CmnError") + ex.Message);
            }

        }

        public void DeleteVariable(int variableId)
        {
            try
            {
                EF.StagedVariable variableDetails = new EF.StagedVariable();
                //using (var variable = new UnitofWork())
                //{
                variableDetails = uow.StagedVariableRepository.Find(p => p.Id == variableId).SingleOrDefault();
                if (variableDetails == null)
                {
                    throw new InvalidOperationException(Resource.GetResxValueByName("CmnDataNotFound"));
                }
                var formulaExists = uow.StagedFormulaRepository.Find(x => x.VariableId == variableId).SingleOrDefault();
                if (formulaExists != null)
                {
                    throw new InvalidOperationException("Cannot delete Variable as " + variableDetails.VariableName + " is used as formula, please delete Fomula first");
                }
                List<string> lstvariablename = CheckVariableAndFormulaDependency(variableId);
                if (lstvariablename.Any())
                {
                    throw new InvalidOperationException("Cannot delete Variable as " + variableDetails.VariableName + " is being used in " + String.Join(",", lstvariablename) + " formulae");
                }
                uow.StagedVariableRepository.Remove(variableDetails);
                uow.Dispose();
                //}
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error while loading Data. Error : " + ex.Message);
            }
        }

        public List<string> CheckVariableAndFormulaDependency(long vId)
        {
            //using (var variable = new UnitofWork())
            //{
            List<string> lstvariablename = new List<string>();
            var lstformulaid = uow.StagedFormulaDependencyRepository.Find(x => x.VariableId == vId).Select(x => x.FormulaId).ToList();
            if (lstformulaid.Count > 0)
            {
                List<long> lstvariableid = new List<long>();
                foreach (var item in lstformulaid)
                {
                    long variableid = uow.StagedFormulaRepository.Find(x => x.ID == item).Select(x => x.VariableId).First();
                    lstvariableid.Add(variableid);
                }

                foreach (var param in lstvariableid)
                {
                    string variablename = uow.StagedVariableRepository.Find(x => x.Id == param).Select(x => x.VariableName).First();
                    lstvariablename.Add(variablename);
                }
                return lstvariablename;
            }
            else
            {
                return lstvariablename;
            }
            //}
        }

    }
}