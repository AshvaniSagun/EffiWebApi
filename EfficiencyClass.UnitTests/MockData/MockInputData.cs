using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EfficiencyClassWebAPI.Models;
using EfficiencyClassWebAPI.EF;

namespace EfficiencyClass.UnitTests.MockData
{
    public class MockInputData
    {
        public IList<MarketDataModel> MarketDetailsInput()
        {
            return new List<MarketDataModel>()
            {
                new MarketDataModel()
                {
                   Mmid =0,
                   Marketid=9,
                   MarketTypeId=2,
                   Year=2040,
                   CreatedBy="Admin",
                   CreatedOn=DateTime.Now,
                   UpdatedBy="Admin",
                   UpdatedOn=DateTime.Now}
                
            };
        }
        public IList<MarketDataModel> MarketDetailsUpdateInput()
        {
            return new List<MarketDataModel>()
            {
                new MarketDataModel()
                {
                   Mmid =10,
                   Marketid=9,
                   MarketTypeId=1,
                   ParameterGroupId=2,
                   Year=2025,
                   CreatedBy="Admin",
                   CreatedOn=DateTime.Now,
                   UpdatedBy="Admin",
                   UpdatedOn=DateTime.Now}

            };
        }
        public Marketdetails SpecificMarketDetailsInput()
        {
            return new Marketdetails()
            {
                MarketId =0,
                SpecMarketCode =456,
                MarketName ="France",
                CreatedBy ="ADMIN",
                CreatedOn = DateTime.Now,
                UpdatedBy = "Admin",
                UpdatedOn = DateTime.Now
            };
        }
        public Marketdetails SpecificMarketDetailsDuplicateInput()
        {
            return new Marketdetails()
            {
                MarketId = 0,
                SpecMarketCode = 211,
                MarketName = "NetherLands",
                CreatedBy = "ADMIN",
                CreatedOn = DateTime.Now,
                UpdatedBy = "Admin",
                UpdatedOn = DateTime.Now
            };
        }
        public IList<FormulaModel> FormulaDetailsInput()
        {
            return new List<FormulaModel>()
            {
                new FormulaModel()
                {
                   Id= 3,
                    Mmid= 10,
                    FormulaDefinition="((CO2-SegmentCO2)/SegmentCO2*100)",
                    VariableId=7,
                    VariableName= "PercentageDeviation",
                    FormulaPriority=1,
                    CreatedBy= "Admin",
                    CreatedOn=DateTime.Now,
                    UpdatedBy= "Admin",
                    UpdatedOn= DateTime.Now
                }
            };
        }

        public IList<FormulaModel> UpdateFormulaeInput()
        {
            return new List<FormulaModel>()
            {
                new FormulaModel()
                {
                    Id= 7,
                    Mmid= 26,
                    FormulaDefinition= "X+Y*0.43/W",
                    VariableId=16,
                    FormulaPriority= 2,
                    CreatedBy="admin",
                    CreatedOn= DateTime.Now,
                    UpdatedBy= "admin",
                    UpdatedOn= DateTime.Now,
                }
            };
        }

        public IList<EfficiencyRange> EfficiencyRangeInput()
        {
            return new List<EfficiencyRange>()
            {
                new EfficiencyRange()
                {
                  Id= 0,
                  Mmid= 1,
                  FuelTypeId= 9,
                  //FuelTypeName=" ",
                  StartRange=  Convert.ToDecimal(26.6),
                  EndRange= Convert.ToDecimal(50),
                  EcValue= "X",
                  CreatedBy= "Admin",
                  CreatedOn= DateTime.Now,
                  UpdatedBy= "Admin",
                  UpdatedOn=DateTime.Now
                }
            };
        }

        public IList<EfficiencyRange> EfficiencyRangeDuplicateInput()
        {
            return new List<EfficiencyRange>()
            {
                new EfficiencyRange()
                {
                  Id= 0,
                  Mmid= 1,
                  FuelTypeId= 9,
                  //FuelTypeName=" ",
                  StartRange=  Convert.ToDecimal(26),
                  EndRange= Convert.ToDecimal(50),
                  EcValue= "B",
                  CreatedBy= "Admin",
                  CreatedOn= DateTime.Now,
                  UpdatedBy= "Admin",
                  UpdatedOn=DateTime.Now
                }
            };
        }

        public IList<EfficiencyRange> UpdateEfficiencyRangeInput()
        {
            return new List<EfficiencyRange>()
            {
                new EfficiencyRange()
                {
                  Id= 44,
                  Mmid= 2,
                  FuelTypeId= 9,
                  FuelTypeName=" ",
                  StartRange= Convert.ToDecimal(5),
                  EndRange= Convert.ToDecimal(15),
                  EcValue= "A",
                  CreatedBy= "Admin",
                  CreatedOn= DateTime.Now,
                  UpdatedBy= "Admin",
                  UpdatedOn=DateTime.Now
                }
            };
        }

        public IList<VariablesModel> VariablesDetailsInput()
        {
            return new List<VariablesModel>()
            {
                new VariablesModel()
                {
                    Id= 0,
                    VariableName= "W",
                    VariableValue= "4",
                    Mmid= 26,
                    VariableTypeId=5,
                    //VariableTypeName="sample string 6",
                    CreatedBy= "admin",
                    CreatedOn= DateTime.Now,
                    UpdatedBy="admin",
                    UpdatedOn=DateTime.Now
                }
            };
        }

        public IList<VariablesModel> UpdateVariablesDetailsInput()
        {
            return new List<VariablesModel>()
            {
                new VariablesModel()
                {
                    Id= 102,
                    VariableName= "XY",
                    VariableValue= "2.5",
                    Mmid= 28,
                    VariableTypeId= 4,
                    //VariableTypeName="sample string 6",
                    CreatedBy= "Admin",
                    CreatedOn= DateTime.Now,
                    UpdatedBy="Admin",
                    UpdatedOn=DateTime.Now
                }
            };
        }

        public IList<CsvUpload> PNO12DetailsInput()
        {
            return new List<CsvUpload>()
            {
                new CsvUpload()
                {
                    EwId= 0,
                    Mmid= 28,
                    Pno12= "pono",
                    PWeight= " ",
                    SegmentCo2= "69",
                    CreatedBy= "Admin",
                    CreatedOn= DateTime.Now,
                    UpdatedBy= "Admin",
                    UpdatedOn= DateTime.Now

                }
            };
        }

        public IList<CsvUpload> UpdatePNO12DetailsInput()
        {
            return new List<CsvUpload>()
            {
                new CsvUpload()
                {
                    EwId= 568,
                    Mmid= 2,
                    Pno12= "PNO12A",
                    PWeight= "150",
                    SegmentCo2= "null",
                    CreatedBy= "Admin",
                    CreatedOn= DateTime.Now,
                    UpdatedBy= "Admin",
                    UpdatedOn= DateTime.Now

                }
            };
        }

        public UserManagementModel AddUserDetailsInput()
        {
            return new UserManagementModel()
            {
                Id = 0,
                Cdsid = "PQR",
                MarketNames = "pqr18",
                Email = "pqr.18@volvocars.com",
                MarketIds = new List<int> { 2, 3 },
                RoleId = 2,
                CreatedBy = "Admin",
                CreatedOn = DateTime.Now,
                UpdatedBy = "Admin",
                UpdatedOn = DateTime.Now
            };
        }

        public UserManagementModel UpdateUserDetailsInput()
        {
            return new UserManagementModel()
            {
                Id = 6,
                Cdsid = "PQR",
                UserName = "pqr18",
                Email = "pqr.18@volvocars.com",
                MarketNames = "",
                MarketIds = new List<int> { 4, 5 },
                RoleId = 2,
                CreatedBy = "Admin",
                CreatedOn = DateTime.Now,
                UpdatedBy = "Admin",
                UpdatedOn = DateTime.Now
            };
        }
    }
}
