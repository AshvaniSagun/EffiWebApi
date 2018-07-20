using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EfficiencyClassWebAPI.Controllers;
using EfficiencyClass.UnitTests.MockData;
using EfficiencyClassWebAPI.Repository;
using Moq;
using System.Net.Http;
using System.Web.Http;
using System.Linq.Expressions;
using EfficiencyClassWebAPI.EF;


namespace EfficiencyClass.UnitTests.ControllersTests
{
    [TestClass]
    public class MasterDataControllerTests
    {
        private MasterDataController controller;
        private MockUnitOfWork muow;
        Mock<UnitofWork> mocObj = new Mock<UnitofWork>();

        [TestInitialize]
        public void SetUp()
        {
            muow = new MockUnitOfWork();
            controller = new MasterDataController(mocObj.Object);
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
            int uid=1;
            mocObj.Setup(x => x.MarketRepository.GetAll()).Returns(() => muow.MarketRepository.GetAll());
            mocObj.Setup(y => y.UserDetailRepository.Find(It.IsAny<Expression<Func<UserDetail, bool>>>())).Returns(() => muow.UserDetailRepository.Find(x => x.CDSID == cdsid));
            mocObj.Setup(y => y.UserMarketRepository.Find(It.IsAny<Expression<Func<UserMarket, bool>>>())).Returns(() => muow.UserMarketRepository.Find(x => x.UserId == uid));

            var response = controller.GetMarket(cdsid);
            Assert.IsNotNull(response.Content);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void GetSpecificMarketDetails_Test()
        {
            mocObj.Setup(x => x.MarketRepository.GetAll()).Returns(() => muow.MarketRepository.GetAll());
           
            var response = controller.GetSpecificMarketList();
            Assert.IsNotNull(response.Content);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void GetMarketTypeDetails_Test()
        {
            mocObj.Setup(x => x.MarketTypeRepository.GetAll()).Returns(() => muow.MarketTypeRepository.GetAll());
           
            var response = controller.GetMarketType();
            Assert.IsNotNull(response.Content);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void GetVariableTypeDetails_Test()
        {
            int parameterGroupId = 3;
            mocObj.Setup(y => y.ParameterGroupRepository.Find(It.IsAny<Expression<Func<ParameterGroupMaster, bool>>>())).Returns(() => muow.ParameterGroupRepository.Find(x => x.ParameterGroupName == "VariableType"));
            mocObj.Setup(y => y.VariableTypeRepository.Find(It.IsAny<Expression<Func<VariableType, bool>>>())).Returns(() => muow.VariableTypeRepository.Find(x => x.ParameterGroupId == parameterGroupId));
           
            var response = controller.GetVariableType();
            Assert.IsNotNull(response.Content);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]

        public void GetMarketModelYearDetails_Test()
        {
            int marketId = 1;
            mocObj.Setup(y => y.Market2MarketTypeParameterGroupRepository.Find(It.IsAny<Expression<Func<Market2MarketTypeParameterGroup, bool>>>())).Returns(() => muow.Market2MarketTypeParameterGroupRepository.Find(x => x.MarketId== marketId));
            
            var response = controller.GetMarketModelYear(marketId);
            Assert.IsNotNull(response.Content);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
        [TestMethod]
        public void GetFuelTypeDetails_Test()
        {
            int parameterGroupId = 1;
            mocObj.Setup(y => y.ParameterGroupRepository.Find(It.IsAny<Expression<Func<ParameterGroupMaster, bool>>>())).Returns(() => muow.ParameterGroupRepository.Find(x => x.ParameterGroupName == "FuelType"));
            mocObj.Setup(y => y.VariableTypeRepository.Find(It.IsAny<Expression<Func<VariableType, bool>>>())).Returns(() => muow.VariableTypeRepository.Find(x => x.ParameterGroupId == parameterGroupId));
           
            var response = controller.GetFuelType();
            Assert.IsNotNull(response.Content);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
        [TestMethod]
        public void GetVariableDetails_Test()
        {
            int mmid = 1;
            mocObj.Setup(y => y.VariableTypeRepository.Find(It.IsAny<Expression<Func<VariableType, bool>>>())).Returns(() => muow.VariableTypeRepository.Find(x => x.VariableTypeName == "Calculated"));
            mocObj.Setup(y => y.StagedVariableRepository.Find(It.IsAny<Expression<Func<StagedVariable, bool>>>())).Returns(() => muow.StagedVariableRepository.Find(x => x.MMId==mmid));
            mocObj.Setup(y => y.VariableRepository.Find(It.IsAny<Expression<Func<Variable, bool>>>())).Returns(() => muow.VariableRepository.Find(x => x.MMId == mmid));
            
            var response = controller.GetVariableList(mmid);
            Assert.IsNotNull(response.Content);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void GetInputTypeVariables_Test()
        {
            int parameterGroupId = 4;
            mocObj.Setup(y => y.ParameterGroupRepository.Find(It.IsAny<Expression<Func<ParameterGroupMaster, bool>>>())).Returns(() => muow.ParameterGroupRepository.Find(x => x.ParameterGroupName == "InputType"));
            mocObj.Setup(y => y.VariableTypeRepository.Find(It.IsAny<Expression<Func<VariableType, bool>>>())).Returns(() => muow.VariableTypeRepository.Find(x => x.ParameterGroupId == parameterGroupId));
          
            var response = controller.GetInputTypeVariables();
            Assert.IsNotNull(response.Content);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
        [TestMethod]
        public void GetFuelTypeVariables_Test()
        {
            int parameterGroupId = 5;
            mocObj.Setup(y => y.ParameterGroupRepository.Find(It.IsAny<Expression<Func<ParameterGroupMaster, bool>>>())).Returns(() => muow.ParameterGroupRepository.Find(x => x.ParameterGroupName == "FuelTypeVariable"));
            mocObj.Setup(y => y.VariableTypeRepository.Find(It.IsAny<Expression<Func<VariableType, bool>>>())).Returns(() => muow.VariableTypeRepository.Find(x => x.ParameterGroupId == parameterGroupId));
           
            var response = controller.GetInputTypeVariables();
            Assert.IsNotNull(response.Content);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
    }
}
