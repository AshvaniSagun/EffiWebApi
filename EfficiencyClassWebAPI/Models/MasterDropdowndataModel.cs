using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EF = EfficiencyClassWebAPI.EF;
using EfficiencyClassWebAPI.Repository;
using EfficiencyClassWebAPI.ResourceFiles;

namespace EfficiencyClassWebAPI.Models
{
    public class MasterDropdowndataModel
    {
        private UnitofWork uow;
        public MasterDropdowndataModel()
        {
            uow = new UnitofWork();
        }

        public MasterDropdowndataModel(UnitofWork unitofWork)
        {
            uow = unitofWork;
        }
        #region Get All MarketType Data
        public List<EF.MarketType> GetAllMarketType()
        {
            try
            {
                //using (var marketTypeRepo = new UnitofWork())
                //{
                    List<EF.MarketType> result = uow.MarketTypeRepository.GetAll().ToList();
                uow.Dispose();
                return result;
               // }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Get All parameter group Data
        public List<EF.ParameterGroupMaster> GetAllParameterGroupMaster()
        {
            try
            {
                //using (var marketRepo = new UnitofWork())
                //{
                    List<EF.ParameterGroupMaster> result = uow.ParameterGroupRepository.Find(p=>p.IsActive ==true).ToList();
                uow.Dispose();
                return result;
                //}
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<EF.Market2MarketTypeParameterGroup> GetMarketModelYearData(int mId)
        {
            try
            {
                //using (var marketRepo = new UnitofWork())
                //{
                    List<EF.Market2MarketTypeParameterGroup> result = uow.Market2MarketTypeParameterGroupRepository.Find(p => p.MarketId == mId).ToList();
                uow.Dispose();
                return result;
                //}
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        public List<EF.VariableType> GetAllVariableType()
        {
            try
            {
                //using (var variableTypeRepo = new UnitofWork())
                //{
                    int parameterGroupId = uow.ParameterGroupRepository.Find(x => x.ParameterGroupName == "VariableType").Select(y => y.ParameterGroupId).SingleOrDefault();
                    List<EF.VariableType> result = uow.VariableTypeRepository.Find(p=>p.ParameterGroupId== parameterGroupId).ToList();
                uow.Dispose();
                return result;
                //}
            }
            catch (Exception ex)
            {
                new Microsoft.ApplicationInsights.TelemetryClient().TrackException(ex);
                throw;
            }
        }

        public List<EF.Variable> GetResultTypeVariable()
        {
            try
            {
                //using (var variableTypeRepo = new UnitofWork())
                //{
                    List<EF.Variable> result = uow.VariableRepository.Find(p => p.VariableTypeId == 2).ToList();
                uow.Dispose();
                return result;
                //}
            }
            catch (Exception ex)
            {
                new Microsoft.ApplicationInsights.TelemetryClient().TrackException(ex);
                throw;
            }
        }
        public List<EF.VariableType> GetFuelTypeList()
        {
            try
            {
                //using (var variableTypeRepo = new UnitofWork())
                //{
                    int parameterGroupId = uow.ParameterGroupRepository.Find(x => x.ParameterGroupName == "FuelType").Select(y => y.ParameterGroupId).SingleOrDefault();
                    List<EF.VariableType> result = uow.VariableTypeRepository.Find(p => p.ParameterGroupId == parameterGroupId).ToList();
                uow.Dispose();
                return result;
                //}
            }
            catch (Exception ex )
            {
                new Microsoft.ApplicationInsights.TelemetryClient().TrackException(ex);
                throw;
            }
        }

        public List<EF.StagedVariable> GetVariableList(int MMId)
        {
            try
            {
                List<EF.StagedVariable> variablesTobeDisplayed = new List<EF.StagedVariable>();
                //using (var variable = new UnitofWork())
                //{
                    int resultTypeId = uow.VariableTypeRepository.Find(x => x.VariableTypeName == "Result").Select(y => y.Id).SingleOrDefault();
                    int calculatedTypeId = uow.VariableTypeRepository.Find(x => x.VariableTypeName == "Calculated").Select(y => y.Id).SingleOrDefault();
                    int weightDataTypeId = uow.VariableTypeRepository.Find(x => x.VariableTypeName == "WeightData").Select(y => y.Id).SingleOrDefault();

                   
                    var stagedVariableNameList = uow.StagedVariableRepository.Find(v => v.MMId == MMId).ToList();
                    var publishedVariableNameList = uow.VariableRepository.Find(v => v.MMId == MMId).ToList();
                    var variableListPublished = publishedVariableNameList.Where(x => !stagedVariableNameList.Any(y => y.VariableName.ToUpper() == x.VariableName.ToUpper())).ToList();
                    variablesTobeDisplayed.AddRange(stagedVariableNameList.Where(v => v.VariableTypeId == resultTypeId || v.VariableTypeId == calculatedTypeId || v.VariableTypeId == weightDataTypeId).ToList());
                    foreach (var item in variableListPublished)
                    {
                        if (item.VariableTypeId == resultTypeId || item.VariableTypeId == calculatedTypeId || item.VariableTypeId == weightDataTypeId)
                        {
                            variablesTobeDisplayed.Add(new EF.StagedVariable()
                            {
                                Id = item.Id,
                                VariableName = item.VariableName,
                                VariableValue = item.VariableValue,
                                VariableTypeId = item.VariableTypeId,
                                MMId = item.MMId,
                                CreatedBy = item.CreatedBy,
                                CreatedOn = item.CreatedOn,
                                UpdatedBy = item.UpdatedBy,
                                UpdatedOn = DateTime.UtcNow
                            });
                        }
                        else
                        {
                            continue;
                        }

                    }
                uow.Dispose();
                return variablesTobeDisplayed;
                //}
            }
            catch (Exception ex)
            {
                new Microsoft.ApplicationInsights.TelemetryClient().TrackException(ex);
                throw;
            }
        }

        public List<EF.Role> GetUserRole()
        {
            try
            {
                //using (var user = new UnitofWork())
                //{
                    List<EF.Role> userRoles = uow.RoleRepository.GetAll().ToList();
                uow.Dispose();
                return userRoles;
               // }
            }
            catch (Exception ex )
            {
                new Microsoft.ApplicationInsights.TelemetryClient().TrackException(ex);
                throw;
            }
        }

        public List<EF.VariableType> GetInputTypeVariableList()
        {
            try
            {
                //using (var variable = new UnitofWork())
                //{
                    int parameterGroupId = uow.ParameterGroupRepository.Find(x => x.ParameterGroupName == "InputType").Select(y => y.ParameterGroupId).SingleOrDefault();
                    List<EF.VariableType> inputVariables = uow.VariableTypeRepository.Find(x=>x.ParameterGroupId== parameterGroupId).ToList();
                uow.Dispose();
                return inputVariables;
                //}
            }
            catch (Exception ex )
            {
                new Microsoft.ApplicationInsights.TelemetryClient().TrackException(ex);
                throw;
            }
        }

        public List<EF.VariableType> GetFuelTypeVariableList()
        {
            try
            {
                //using (var variable = new UnitofWork())
                //{
                    int parameterGroupId = uow.ParameterGroupRepository.Find(x => x.ParameterGroupName== "FuelTypeVariable").Select(y => y.ParameterGroupId).SingleOrDefault();
                    List<EF.VariableType> fuelTypeVariables = uow.VariableTypeRepository.Find(x => x.ParameterGroupId == parameterGroupId).ToList();
                uow.Dispose();
                return fuelTypeVariables;
                //}
            }
            catch (Exception ex)
            {
                new Microsoft.ApplicationInsights.TelemetryClient().TrackException(ex);
                throw;
            }
        }

    }
}