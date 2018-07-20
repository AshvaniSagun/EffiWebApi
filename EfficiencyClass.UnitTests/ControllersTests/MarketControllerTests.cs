using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EfficiencyClassWebAPI.Controllers;
using EfficiencyClassWebAPI.Models;
using EfficiencyClassWebAPI.EF;
using EfficiencyClass.UnitTests.MockData;
using System.Collections.Generic;
using EfficiencyClassWebAPI.Repository;
using Moq;
using System.Net.Http;
using System.Linq.Expressions;
using System.Web.Http;

namespace EfficiencyClass.UnitTests.ControllersTests
{
    [TestClass]
    public class MarketControllerTests
    {
        private MarketController controller;
        private MockUnitOfWork muow;
        Mock<UnitofWork> mocObj = new Mock<UnitofWork>();

        [TestInitialize]
        public void SetUp()
        {
            muow = new MockUnitOfWork();
            controller = new MarketController(mocObj.Object);
            controller.Request = new HttpRequestMessage()
            {
                Properties = { { System.Web.Http.Hosting.HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
            };
        }

        [TestCleanup]
        public void CleanUp()
        {
            controller.Dispose();
        }
        [TestMethod]
        public void GetMarketDetails_Test()
        {
            string cdsid = "XYZ1";
            int userId = 1;
            int marketId = 1;
            mocObj.Setup(x => x.UserDetailRepository.Find(It.IsAny<Expression<Func<UserDetail, bool>>>())).Returns(() => muow.UserDetailRepository.Find(x=>x.CDSID==cdsid));
            mocObj.Setup(x => x.UserMarketRepository.Find(It.IsAny<Expression<Func<UserMarket, bool>>>())).Returns(() => muow.UserMarketRepository.Find(x=>x.UserId ==userId));
            mocObj.Setup(x => x.MarketTypeRepository.GetAll()).Returns(() => muow.MarketTypeRepository.GetAll());
            mocObj.Setup(x => x.MarketRepository.GetAll()).Returns(() => muow.MarketRepository.GetAll());
            mocObj.Setup(x => x.Market2MarketTypeParameterGroupRepository.Find(It.IsAny<Expression<Func<Market2MarketTypeParameterGroup, bool>>>())).Returns(() => muow.Market2MarketTypeParameterGroupRepository.Find(x => x.MarketId==marketId));

            var response = controller.Get(cdsid);
            Assert.IsNotNull(response.Content);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);

        }

        [TestMethod]
        public void AddMarketDetails_Test()
        {
            IList<MarketDataModel> marketDetails = new MockInputData().MarketDetailsInput();
            List<Market2MarketTypeParameterGroup> marketData = new List<Market2MarketTypeParameterGroup>();
            mocObj.Setup(y => y.Market2MarketTypeParameterGroupRepository.Find(It.IsAny<Expression<Func<Market2MarketTypeParameterGroup, bool>>>())).Returns(() => muow.Market2MarketTypeParameterGroupRepository.Find(x => x.MarketId == marketDetails[0].Marketid && x.MYear == marketDetails[0].Year));
            mocObj.Setup(x => x.Market2MarketTypeParameterGroupRepository.AddRange(It.IsAny<List<Market2MarketTypeParameterGroup>>())).Callback(() => muow.Market2MarketTypeParameterGroupRepository.AddRange(marketData));
        
            var response = controller.AddMarket(marketDetails);
            Assert.AreEqual(System.Net.HttpStatusCode.Created, response.StatusCode);
        }

        
        [TestMethod]
        public void DeleteMarketDetails_Test()
        {
            int mmid = 5;
            MockInitiation(mocObj);
           
            var response = controller.Delete(mmid);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        public void MockInitiation(Mock<UnitofWork> mocObj)
        {
            int mmid = 5;
            int formulaId = 2;
            List<Market2MarketTypeParameterGroup> lstMarketDetails = new List<Market2MarketTypeParameterGroup>();
            List<StagedEfficiencyClassRange> lstStagedRangeDetails = new List<StagedEfficiencyClassRange>();
            List<StagedWeightSegmentCo2> lstStagedPno12Details = new List<StagedWeightSegmentCo2>();
            List<StagedVariable> lstStagedVariables = new List<StagedVariable>();
            List<StagedFormula> lstStagedFormulae = new List<StagedFormula>();
            List<StagedFormulaDependencyDetail> lstStagedFormulaDependency = new List<StagedFormulaDependencyDetail>();

            List<EfficiencyClassRange> lstRangeDetails = new List<EfficiencyClassRange>();
            List<WeightSegmentCo2> lstPno12Details = new List<WeightSegmentCo2>();
            List<Variable> lstVariables = new List<Variable>();
            List<Formula> lstFormulae = new List<Formula>();
            List<FormulaDependencyDetail> lstFormulaDependency = new List<FormulaDependencyDetail>();

            mocObj.Setup(x => x.Market2MarketTypeParameterGroupRepository.RemoveRange(It.IsAny<List<Market2MarketTypeParameterGroup>>())).Callback(() => muow.Market2MarketTypeParameterGroupRepository.RemoveRange(lstMarketDetails));

            mocObj.Setup(x => x.StagedEfficiencyClassRangeRepository.RemoveRange(It.IsAny<List<StagedEfficiencyClassRange>>())).Callback(() => muow.StagedEfficiencyClassRangeRepository.RemoveRange(lstStagedRangeDetails));
            mocObj.Setup(x => x.StagedWeightSegmentCo2Repository.RemoveRange(It.IsAny<List<StagedWeightSegmentCo2>>())).Callback(() => muow.StagedWeightSegmentCo2Repository.RemoveRange(lstStagedPno12Details));
            mocObj.Setup(x => x.StagedFormulaDependencyRepository.RemoveRange(It.IsAny<List<StagedFormulaDependencyDetail>>())).Callback(() => muow.StagedFormulaDependencyRepository.RemoveRange(lstStagedFormulaDependency));
            mocObj.Setup(x => x.StagedVariableRepository.RemoveRange(It.IsAny<List<StagedVariable>>())).Callback(() => muow.StagedVariableRepository.RemoveRange(lstStagedVariables));
            mocObj.Setup(x => x.StagedFormulaRepository.RemoveRange(It.IsAny<List<StagedFormula>>())).Callback(() => muow.StagedFormulaRepository.RemoveRange(lstStagedFormulae));

            mocObj.Setup(x => x.EfficiencyClassRangeRepository.RemoveRange(It.IsAny<List<EfficiencyClassRange>>())).Callback(() => muow.EfficiencyClassRangeRepository.RemoveRange(lstRangeDetails));
            mocObj.Setup(x => x.WeightSegmentCo2Repository.RemoveRange(It.IsAny<List<WeightSegmentCo2>>())).Callback(() => muow.WeightSegmentCo2Repository.RemoveRange(lstPno12Details));
            mocObj.Setup(x => x.FormulaDependencyDetail.RemoveRange(It.IsAny<List<FormulaDependencyDetail>>())).Callback(() => muow.FormulaDependencyDetail.RemoveRange(lstFormulaDependency));
            mocObj.Setup(x => x.VariableRepository.RemoveRange(It.IsAny<List<Variable>>())).Callback(() => muow.VariableRepository.RemoveRange(lstVariables));
            mocObj.Setup(x => x.FormulaRepository.RemoveRange(It.IsAny<List<Formula>>())).Callback(() => muow.FormulaRepository.RemoveRange(lstFormulae));

            mocObj.Setup(x => x.Market2MarketTypeParameterGroupRepository.Find(It.IsAny<Expression<Func<Market2MarketTypeParameterGroup, bool>>>())).Returns(() => muow.Market2MarketTypeParameterGroupRepository.Find(x => x.MMId == mmid));
            mocObj.Setup(x => x.FormulaRepository.Find(It.IsAny<Expression<Func<Formula, bool>>>())).Returns(() => muow.FormulaRepository.Find(x => x.MMId == mmid));
            mocObj.Setup(x => x.FormulaDependencyDetail.Find(It.IsAny<Expression<Func<FormulaDependencyDetail, bool>>>())).Returns(() => muow.FormulaDependencyDetail.Find(x => x.FormulaId == formulaId));
            mocObj.Setup(x => x.VariableRepository.Find(It.IsAny<Expression<Func<Variable, bool>>>())).Returns(() => muow.VariableRepository.Find(x => x.MMId == mmid));
            mocObj.Setup(x => x.EfficiencyClassRangeRepository.Find(It.IsAny<Expression<Func<EfficiencyClassRange, bool>>>())).Returns(() => muow.EfficiencyClassRangeRepository.Find(x => x.MMID == mmid));
            mocObj.Setup(x => x.WeightSegmentCo2Repository.Find(It.IsAny<Expression<Func<WeightSegmentCo2, bool>>>())).Returns(() => muow.WeightSegmentCo2Repository.Find(x => x.MMID == mmid));

            mocObj.Setup(x => x.StagedFormulaRepository.Find(It.IsAny<Expression<Func<StagedFormula, bool>>>())).Returns(() => muow.StagedFormulaRepository.Find(x => x.MMId == mmid));
            mocObj.Setup(x => x.StagedFormulaDependencyRepository.Find(It.IsAny<Expression<Func<StagedFormulaDependencyDetail, bool>>>())).Returns(() => muow.StagedFormulaDependencyRepository.Find(x => x.FormulaId == formulaId));
            mocObj.Setup(x => x.StagedVariableRepository.Find(It.IsAny<Expression<Func<StagedVariable, bool>>>())).Returns(() => muow.StagedVariableRepository.Find(x => x.MMId == mmid));
            mocObj.Setup(x => x.StagedEfficiencyClassRangeRepository.Find(It.IsAny<Expression<Func<StagedEfficiencyClassRange, bool>>>())).Returns(() => muow.StagedEfficiencyClassRangeRepository.Find(x => x.MMID == mmid));
            mocObj.Setup(x => x.StagedWeightSegmentCo2Repository.Find(It.IsAny<Expression<Func<StagedWeightSegmentCo2, bool>>>())).Returns(() => muow.StagedWeightSegmentCo2Repository.Find(x => x.MMID == mmid));
        }
    }
}
