using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EfficiencyClassWebAPI.EF;

namespace EfficiencyClass.UnitTests.MockData
{
    public class MockDataContext
    {
        public List<EfficiencyClassRange>  EfficiencyClassRange
        {
            get
            {
                return new List<EfficiencyClassRange>
                {
                    new EfficiencyClassRange{Id=35, MMID=1, VariableTypeId=9, StartRange=Convert.ToDecimal(-999.0000), EndRange= Convert.ToDecimal(-20.0001), ECValue="A", CreatedBy="ADMIN", CreatedOn= Convert.ToDateTime("2018-06-19T13:27:15.043"), UpdatedBy="ADMIN", UpdatedOn=Convert.ToDateTime("2018-06-19T13:27:15.043")},
                    new EfficiencyClassRange{Id = 34, MMID = 1, VariableTypeId = 9, StartRange = Convert.ToDecimal(-20.0000), EndRange = Convert.ToDecimal(-10.0001), ECValue = "B", CreatedBy = "ADMIN", CreatedOn = Convert.ToDateTime("2018-06-19T13:27:15.043"), UpdatedBy = "ADMIN", UpdatedOn = Convert.ToDateTime("2018-06-19T13:27:15.043") },
                    new EfficiencyClassRange{Id = 33, MMID = 1, VariableTypeId = 9, StartRange = Convert.ToDecimal(-10.0000), EndRange = Convert.ToDecimal(-0.0001), ECValue = "C", CreatedBy = "ADMIN", CreatedOn = Convert.ToDateTime("2018-06-19T13:27:15.043"), UpdatedBy = "ADMIN", UpdatedOn = Convert.ToDateTime("2018-06-19T13:27:15.043")},
                    new EfficiencyClassRange{Id = 32, MMID = 1, VariableTypeId = 9, StartRange = Convert.ToDecimal(0.0000), EndRange = Convert.ToDecimal(10.0000), ECValue = "D", CreatedBy = "ADMIN", CreatedOn = Convert.ToDateTime("2018-06-19T13:27:15.043"), UpdatedBy = "ADMIN", UpdatedOn = Convert.ToDateTime("2018-06-19T13:27:15.043")},
                    new EfficiencyClassRange{Id = 31, MMID = 1, VariableTypeId = 9,  StartRange = Convert.ToDecimal(10.0001), EndRange = Convert.ToDecimal(20.0000), ECValue = "E", CreatedBy = "ADMIN", CreatedOn = Convert.ToDateTime("2018-06-19T13:27:15.043"), UpdatedBy = "ADMIN", UpdatedOn = Convert.ToDateTime("2018-06-19T13:27:15.043")},
                    new EfficiencyClassRange{Id = 30, MMID = 1, VariableTypeId = 9,  StartRange = Convert.ToDecimal(20.0001), EndRange = Convert.ToDecimal(30.0000), ECValue = "F", CreatedBy = "ADMIN", CreatedOn = Convert.ToDateTime("2018-06-19T13:27:15.043"), UpdatedBy = "ADMIN", UpdatedOn = Convert.ToDateTime("2018-06-19T13:27:15.043")},
                    new EfficiencyClassRange{ Id = 29, MMID = 1, VariableTypeId = 9, StartRange = Convert.ToDecimal(30.0001), EndRange = Convert.ToDecimal(999.0000), ECValue = "G ", CreatedBy = "ADMIN", CreatedOn = Convert.ToDateTime("2018-06-19T13:27:15.043"), UpdatedBy = "ADMIN", UpdatedOn = Convert.ToDateTime("2018-06-19T13:27:15.043")}
                };
            }
        }
        public List<Formula> Formula
        {
            get
            {
                return new List<Formula>
                {
                    new Formula{ID=1,MMId=3,FormulaDefinition="36.59079+0.08987*Weight",VariableId=1,FormulaPriority=2,CreatedBy = "ADMIN", CreatedOn = Convert.ToDateTime("2018-06-19T13:27:15.043"), UpdatedBy = "ADMIN", UpdatedOn = Convert.ToDateTime("2018-06-19T13:27:15.043") },
                     new Formula{ID=2,MMId=3,FormulaDefinition="(CO2-refco2emission)/refco2emission*100",VariableId=4,FormulaPriority=3,CreatedBy = "ADMIN", CreatedOn = Convert.ToDateTime("2018-06-19T13:27:15.043"), UpdatedBy = "ADMIN", UpdatedOn = Convert.ToDateTime("2018-06-19T13:27:15.043") },
                     new Formula{ID=9,MMId=3,FormulaDefinition="ActualMass",VariableId=2,FormulaPriority=1,CreatedBy = "ADMIN", CreatedOn = Convert.ToDateTime("2018-06-19T13:27:15.043"), UpdatedBy = "ADMIN", UpdatedOn = Convert.ToDateTime("2018-06-19T13:27:15.043") },
                     new Formula{ID=3,MMId=1,FormulaDefinition="((CO2-SegmentCO2)/SegmentCO2*100)",VariableId=7,FormulaPriority=1,CreatedBy = "ADMIN", CreatedOn = Convert.ToDateTime("2018-06-19T13:27:15.043"), UpdatedBy = "ADMIN", UpdatedOn = Convert.ToDateTime("2018-06-19T13:27:15.043") }
                };
            }
        }
        public List<FormulaDependencyDetail> FormulaDependencyDetail
        {
            get
            {
                return new List<FormulaDependencyDetail>
                {
                     new FormulaDependencyDetail{Id=13,FormulaId=1,VariableId=2,CreatedBy = "ADMIN", CreatedOn = Convert.ToDateTime("2018-06-19T13:27:15.043"), UpdatedBy = "ADMIN", UpdatedOn = Convert.ToDateTime("2018-06-19T13:27:15.043") },
                    new FormulaDependencyDetail{Id=14,FormulaId=2,VariableId=1,CreatedBy = "ADMIN", CreatedOn = Convert.ToDateTime("2018-06-19T13:27:15.043"), UpdatedBy = "ADMIN", UpdatedOn = Convert.ToDateTime("2018-06-19T13:27:15.043") },
                    new FormulaDependencyDetail{Id=15,FormulaId=2,VariableId=3,CreatedBy = "ADMIN", CreatedOn = Convert.ToDateTime("2018-06-19T13:27:15.043"), UpdatedBy = "ADMIN", UpdatedOn = Convert.ToDateTime("2018-06-19T13:27:15.043") },
                    new FormulaDependencyDetail{Id=18,FormulaId=9,VariableId=24,CreatedBy = "ADMIN", CreatedOn = Convert.ToDateTime("2018-06-19T13:27:15.043"), UpdatedBy = "ADMIN", UpdatedOn = Convert.ToDateTime("2018-06-19T13:27:15.043") },
                    new FormulaDependencyDetail{Id=11,FormulaId=3,VariableId=5,CreatedBy = "ADMIN", CreatedOn = Convert.ToDateTime("2018-06-19T13:27:15.043"), UpdatedBy = "ADMIN", UpdatedOn = Convert.ToDateTime("2018-06-19T13:27:15.043") },
                    new FormulaDependencyDetail{Id=12,FormulaId=3,VariableId=6,CreatedBy = "ADMIN", CreatedOn = Convert.ToDateTime("2018-06-19T13:27:15.043"), UpdatedBy = "ADMIN", UpdatedOn = Convert.ToDateTime("2018-06-19T13:27:15.043") }
                };
            }
        }
        public List<Market> Market
        {
            get
            {
                return new List<Market>
                {
                    new Market{Id=1,SpecMarket=211,MarketName="NetherLands",CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                    new Market{Id=2,SpecMarket=232,MarketName="Switzerland",CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                    new Market{Id=3,SpecMarket=231,MarketName="Germany",CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")}
                };
            }
        }
        public List<Market2MarketTypeParameterGroup> Market2MarketTypeParameterGroup
        {
            get
            {
                return new List<Market2MarketTypeParameterGroup>
                {
                    new Market2MarketTypeParameterGroup{MMId=1,MarketId=1,MarketTypeId=2,MYear=2019,ParameterGroupId=null,CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                    new Market2MarketTypeParameterGroup{MMId=2,MarketId=2,MarketTypeId=2,MYear=2019,ParameterGroupId=null,CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                    new Market2MarketTypeParameterGroup{MMId=3,MarketId=3,MarketTypeId=2,MYear=2019,ParameterGroupId=null,CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                    new Market2MarketTypeParameterGroup{MMId=4,MarketId=1,MarketTypeId=2,MYear=2020,ParameterGroupId=null,CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                      new Market2MarketTypeParameterGroup{MMId=5,MarketId=2,MarketTypeId=2,MYear=2020,ParameterGroupId=null,CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")}
                };
            }
        }

        public List<MarketType> MarketType
        {
            get
            {
                return new List<MarketType>
                {
                    new MarketType{Id=1,MarketTypeName="Generic",CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                    new MarketType{Id=2,MarketTypeName="Specific",CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")}

                };
            }
        }
        public List<ParameterGroupMaster> ParameterGroupMaster
        {
            get
            {
                return new List<ParameterGroupMaster>
                {
                    new ParameterGroupMaster{ParameterGroupId=1,ParameterGroupName="FuelType",IsActive=true,CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                    new ParameterGroupMaster{ParameterGroupId=2,ParameterGroupName="Co2",IsActive=true,CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                    new ParameterGroupMaster{ParameterGroupId=3,ParameterGroupName="VariableType",IsActive=false,CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                    new ParameterGroupMaster{ParameterGroupId=4,ParameterGroupName="InputType",IsActive=false,CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                    new ParameterGroupMaster{ParameterGroupId=5,ParameterGroupName="FuelTypeVariable",IsActive=false,CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")}
                };
            }
        }
        public List<Role> Role
        {
            get
            {
                return new List<Role>
                {
                    new Role{Id=1,RoleName="SuperUser",CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                    new Role{Id=2,RoleName="User",CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")}
                };
            }
        }
        public List<StagedEfficiencyClassRange> StagedEfficiencyClassRange
        {
            get
            {
                return new List<StagedEfficiencyClassRange>
                {
                    new StagedEfficiencyClassRange{Id=44, MMID=1, VariableTypeId=9, StartRange=Convert.ToDecimal(-999.0000), EndRange= Convert.ToDecimal(-20.0001), ECValue="A", CreatedBy="ADMIN", CreatedOn= Convert.ToDateTime("2018-06-19T13:27:15.043"), UpdatedBy="ADMIN", UpdatedOn=Convert.ToDateTime("2018-06-19T13:27:15.043")},
                    new StagedEfficiencyClassRange{Id = 34, MMID = 1, VariableTypeId = 9, StartRange = Convert.ToDecimal(-20.0000), EndRange = Convert.ToDecimal(-10.0001), ECValue = "B", CreatedBy = "ADMIN", CreatedOn = Convert.ToDateTime("2018-06-19T13:27:15.043"), UpdatedBy = "ADMIN", UpdatedOn = Convert.ToDateTime("2018-06-19T13:27:15.043") },
                };
            }
        }
        public List<StagedFormula> StagedFormula
        {
            get
            {
                return new List<StagedFormula>
                {
                    new StagedFormula{ID=3,MMId=11,FormulaDefinition="((CO2-SegmentCO2)/SegmentCO2*100)",VariableId=7,FormulaPriority=1,CreatedBy = "ADMIN", CreatedOn = Convert.ToDateTime("2018-06-19T13:27:15.043"), UpdatedBy = "ADMIN", UpdatedOn = Convert.ToDateTime("2018-06-19T13:27:15.043") }
                };
            }
        }
        public List<StagedFormulaDependencyDetail> StagedFormulaDependencyDetail
        {
            get
            {
                return new List<StagedFormulaDependencyDetail>
                {
                     new StagedFormulaDependencyDetail{Id=1,FormulaId=6,VariableId=5,CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                    new StagedFormulaDependencyDetail{Id=1,FormulaId=6,VariableId=6,CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                    new StagedFormulaDependencyDetail{Id=1,FormulaId=6,VariableId=7,CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                    new StagedFormulaDependencyDetail{Id=1,FormulaId=3,VariableId=5,CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                    new StagedFormulaDependencyDetail{Id=1,FormulaId=3,VariableId=6,CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                    new StagedFormulaDependencyDetail{Id=1,FormulaId=3,VariableId=7,CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")}
                };
            }
        }

        public List<StagedVariable> StagedVariable
        {
            get
            {
                return new List<StagedVariable>
                {
                    new StagedVariable{Id=1,VariableName="CO2",VariableValue=null,MMId=10,VariableTypeId=4,CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                    new StagedVariable{Id=2,VariableName="SegmentCO2",VariableValue=null,MMId=10,VariableTypeId=4,CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                    new StagedVariable{Id=7,VariableName="PercentageDeviation",VariableValue=null,MMId=10,VariableTypeId=2,CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                    new StagedVariable{Id=17,VariableName="PercentageDeviation",VariableValue=null,MMId=10,VariableTypeId=2,CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")}
                };
            }
        }

        public List<StagedWeightSegmentCo2> StagedWeightSegmentCo2
        {
            get
            {
                return new List<StagedWeightSegmentCo2>
                {
                     new StagedWeightSegmentCo2{EwId=2,MMID=1,PNO12="225A8120D119",PWeight=null,SegmentCo2="104",CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                    new StagedWeightSegmentCo2{EwId=3,MMID=1,PNO12="225A81201119",PWeight=null,SegmentCo2="104",CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                    new StagedWeightSegmentCo2{EwId=4,MMID=1,PNO12="225A8130D119",PWeight=null,SegmentCo2="104",CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                    new StagedWeightSegmentCo2{EwId=5,MMID=1,PNO12="225A81301119",PWeight=null,SegmentCo2="104",CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")}
                };
            }
        }

        public List<UserDetail> UserDetail
        {
            get
            {
                return new List<UserDetail>
                {
                    new UserDetail{Id=1,CDSID="XYZ1",UserName="xyz",Email="xyz1@volvocars.com",CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                    new UserDetail{Id=2,CDSID="ABC1",UserName="abc",Email="abc1@volvocars.com",CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")}
                };
            }
        }

        public List<UserMarket> UserMarket
        {
            get
            {
                return new List<UserMarket>
                {
                    new UserMarket{UserId=1,MarketId=1,CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                    new UserMarket{UserId=1,MarketId=2,CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                    new UserMarket{UserId=1,MarketId=3,CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")}
                };
            }
        }

        public List<UserRole> UserRole
        {
            get
            {
                return new List<UserRole>
                {
                    new UserRole{UserId=1,RoleId=2,CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                    new UserRole{UserId=2,RoleId=1,CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                };
            }
        }

        public List<Variable> Variable
        {
            get
            {
                return new List<Variable>
                {
                    new Variable{Id=1,VariableName="refco2emission",VariableValue=null,MMId=3,VariableTypeId=3,CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                    new Variable{Id=2,VariableName="Weight",VariableValue=null,MMId=3,VariableTypeId=8,CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                    new Variable{Id=3,VariableName="CO2",VariableValue=null,MMId=3,VariableTypeId=4,CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                    new Variable{Id=4,VariableName="PercentageDeviation",VariableValue=null,MMId=3,VariableTypeId=2,CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                    new Variable{Id=24,VariableName="ActualMass",VariableValue=null,MMId=3,VariableTypeId=4,CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                    new Variable{Id=5,VariableName="CO2",VariableValue=null,MMId=1,VariableTypeId=4,CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                    new Variable{Id=6,VariableName="SegmentCO2",VariableValue=null,MMId=1,VariableTypeId=4,CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")}
                };
            }
        }

        public List<VariableType> VariableType
        {
            get
            {
                return new List<VariableType>
                {
                    new VariableType{Id=1,ParameterGroupId=3,VariableTypeName="Dependency",CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                    new VariableType{Id=2,ParameterGroupId=3,VariableTypeName="Result",CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                    new VariableType{Id=3,ParameterGroupId=3,VariableTypeName="Calculated",CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                    new VariableType{Id=4,ParameterGroupId=3,VariableTypeName="Input",CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                    new VariableType{Id=5,ParameterGroupId=3,VariableTypeName="Fixed",CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                    new VariableType{Id=6,ParameterGroupId=1,VariableTypeName="Petrol",CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                    new VariableType{Id=7,ParameterGroupId=1,VariableTypeName="Diesel",CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                    new VariableType{Id=8,ParameterGroupId=3,VariableTypeName="WeightData",CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                    new VariableType{Id=9,ParameterGroupId=1,VariableTypeName="All",CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                    new VariableType{Id=16,ParameterGroupId=4,VariableTypeName="CO2",CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                    new VariableType{Id=17,ParameterGroupId=4,VariableTypeName="SegmentCO2",CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                    new VariableType{Id=18,ParameterGroupId=4,VariableTypeName="ElectricConsumption",CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                    new VariableType{Id=20,ParameterGroupId=4,VariableTypeName="FuelEfficiency",CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                    new VariableType{Id=21,ParameterGroupId=4,VariableTypeName="ActualMass",CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                    new VariableType{Id=22,ParameterGroupId=4,VariableTypeName="MassInRunningOrderTotal",CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                    new VariableType{Id=23,ParameterGroupId=4,VariableTypeName="MassOfOptionalEquipmentTotal",CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                    new VariableType{Id=26,ParameterGroupId=3,VariableTypeName="FuelType",CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                    new VariableType{Id=27,ParameterGroupId=5,VariableTypeName="Petrol",CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                    new VariableType{Id=28,ParameterGroupId=5,VariableTypeName="Diesel",CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                    new VariableType{Id=29,ParameterGroupId=5,VariableTypeName="Electric",CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                    new VariableType{Id=31,ParameterGroupId=4,VariableTypeName="HomologationCurbWeightTotal",CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                    new VariableType{Id=32,ParameterGroupId=4,VariableTypeName="NEDC_ActualMass",CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                    new VariableType{Id=33,ParameterGroupId=4,VariableTypeName="TestMassInd",CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                };
            }
        }

        public List<WeightSegmentCo2> WeightSegmentCo2
        {
            get
            {
                return new List<WeightSegmentCo2>
                {
                    new WeightSegmentCo2{EwId=2,MMID=1,PNO12="225A3110C119",PWeight=null,SegmentCo2="137",CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                    new WeightSegmentCo2{EwId=3,MMID=1,PNO12="225A3120C119",PWeight=null,SegmentCo2="137",CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                    new WeightSegmentCo2{EwId=4,MMID=1,PNO12="225A3130C119",PWeight=null,SegmentCo2="137",CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                    new WeightSegmentCo2{EwId=5,MMID=1,PNO12="225A8110D119",PWeight=null,SegmentCo2="104",CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")},
                    new WeightSegmentCo2{EwId=6,MMID=1,PNO12="225A81101119",PWeight=null,SegmentCo2="104",CreatedBy="ADMIN",CreatedOn=Convert.ToDateTime("2018-05-14 09:46:57.147"),UpdatedBy="ADMIN",UpdatedOn=Convert.ToDateTime("2018-06-08 06:31:29.417")}
                };
            }
        }
    }
}
