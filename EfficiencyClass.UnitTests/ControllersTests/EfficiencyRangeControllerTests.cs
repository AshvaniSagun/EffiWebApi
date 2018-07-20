using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EfficiencyClassWebAPI.Controllers;
using EfficiencyClassWebAPI.Models;
using EfficiencyClassWebAPI.EF;
using EfficiencyClass.UnitTests.MockData;
using EfficiencyClassWebAPI.Repository;
using System.Collections.Generic;
using Moq;
using System.Web.Http;
using System.Net.Http;
using System.Linq.Expressions;

namespace EfficiencyClass.UnitTests.ControllersTests
{
    [TestClass]
    public class EfficiencyRangeControllerTests
    {
        private EfficiencyRangeController controller;
        private MockUnitOfWork muow;
        Mock<UnitofWork> mocObj = new Mock<UnitofWork>();

        [TestInitialize]
        public void SetUp()
        {
            muow = new MockUnitOfWork();
            controller = new EfficiencyRangeController(mocObj.Object);
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
        public void GetRangeDetails()
        {
            int mmid = 1;
            var data = muow.EfficiencyClassRangeRepository.Find(x => x.MMID == mmid);
            mocObj.Setup(x => x.EfficiencyClassRangeRepository.GetAll()).Returns(() => muow.EfficiencyClassRangeRepository.GetAll());
            mocObj.Setup(x => x.EfficiencyClassRangeRepository.Find(It.IsAny<Expression<Func<EfficiencyClassRange, bool>>>())).Returns(() => muow.EfficiencyClassRangeRepository.Find(y => y.MMID == mmid));
            mocObj.Setup(x => x.VariableTypeRepository.GetAll()).Returns(() => muow.VariableTypeRepository.GetAll());
            mocObj.Setup(x => x.StagedEfficiencyClassRangeRepository.Find(It.IsAny<Expression<Func<StagedEfficiencyClassRange, bool>>>())).Returns(() => muow.StagedEfficiencyClassRangeRepository.Find(y => y.MMID == mmid));
           
            var response = controller.Get(mmid);
            Assert.IsNotNull(response.Content);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void AddRangeDetails_Test()
        {
            IList<EfficiencyRange> rangeDetails = new MockInputData().EfficiencyRangeInput();
           List<StagedEfficiencyClassRange> rangeData = new List<StagedEfficiencyClassRange>();
            mocObj.Setup(x => x.EfficiencyClassRangeRepository.GetAll()).Returns(() => muow.EfficiencyClassRangeRepository.GetAll());
            mocObj.Setup(y => y.StagedEfficiencyClassRangeRepository.Find(It.IsAny<Expression<Func<StagedEfficiencyClassRange, bool>>>())).Returns(() => muow.StagedEfficiencyClassRangeRepository.Find(x => x.MMID == rangeDetails[0].Mmid && x.VariableTypeId == rangeDetails[0].FuelTypeId && x.ECValue == rangeDetails[0].EcValue));
            mocObj.Setup(x => x.StagedEfficiencyClassRangeRepository.AddRange(It.IsAny<List<StagedEfficiencyClassRange>>())).Callback(() => muow.StagedEfficiencyClassRangeRepository.AddRange(rangeData));
            var response = controller.AddEfficiencyClassRange(rangeDetails);
            Assert.AreEqual(System.Net.HttpStatusCode.Created, response.StatusCode);
        }

        [TestMethod]
        public void AddDuplicateRangeDetails_Test()
        {
            IList<EfficiencyRange> rangeDetails = new MockInputData().EfficiencyRangeDuplicateInput();
            List<StagedEfficiencyClassRange> rangeData = new List<StagedEfficiencyClassRange>();
            mocObj.Setup(y => y.StagedEfficiencyClassRangeRepository.Find(It.IsAny<Expression<Func<StagedEfficiencyClassRange, bool>>>())).Returns(() => muow.StagedEfficiencyClassRangeRepository.Find(x => x.MMID == rangeDetails[0].Mmid && x.VariableTypeId == rangeDetails[0].FuelTypeId && x.ECValue == rangeDetails[0].EcValue));
            mocObj.Setup(x => x.StagedEfficiencyClassRangeRepository.AddRange(It.IsAny<List<StagedEfficiencyClassRange>>())).Callback(() => muow.StagedEfficiencyClassRangeRepository.AddRange(rangeData));
            var response = controller.AddEfficiencyClassRange(rangeDetails);
            List<ErrorMessage> jsonContent =(List<ErrorMessage>) response.Content.ReadAsAsync(typeof(List<ErrorMessage>)).Result;
            var message=jsonContent[0].ModelState[0].Message;
            Assert.AreEqual(System.Net.HttpStatusCode.InternalServerError, response.StatusCode);
            Assert.AreEqual("Range already exists", message);
        }
   
        [TestMethod]

        public void DeleteRangeDetails_Test()
        {
            int id = 44;
            int mmid = 2;
            StagedEfficiencyClassRange rangeData = new StagedEfficiencyClassRange();
            mocObj.Setup(x => x.StagedEfficiencyClassRangeRepository.Find(It.IsAny<Expression<Func<StagedEfficiencyClassRange, bool>>>())).Returns(() =>muow.StagedEfficiencyClassRangeRepository.Find(x=>x.Id==id));
            mocObj.Setup(x => x.StagedEfficiencyClassRangeRepository.Remove(It.IsAny<StagedEfficiencyClassRange>())).Callback(() => muow.StagedEfficiencyClassRangeRepository.Remove(rangeData));
            var response = controller.DeleteEfficiencyClassRange(id, mmid);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);

        }

        [TestMethod]
        public void CheckExixtanceofData_Test()
        {
            int mmid = 1;
            int mYear = 2020;

            mocObj.Setup(x => x.Market2MarketTypeParameterGroupRepository.Find(It.IsAny<Expression<Func<Market2MarketTypeParameterGroup, bool>>>())).Returns(() => muow.Market2MarketTypeParameterGroupRepository.Find(x => x.MMId == mmid));
            mocObj.Setup(x => x.FormulaRepository.Find(It.IsAny<Expression<Func<Formula, bool>>>())).Returns(() => muow.FormulaRepository.Find(x => x.MMId == mmid));
            mocObj.Setup(x => x.VariableRepository.Find(It.IsAny<Expression<Func<Variable, bool>>>())).Returns(() => muow.VariableRepository.Find(x => x.MMId == mmid));
            mocObj.Setup(x => x.EfficiencyClassRangeRepository.Find(It.IsAny<Expression<Func<EfficiencyClassRange, bool>>>())).Returns(() => muow.EfficiencyClassRangeRepository.Find(x => x.MMID == mmid));
            mocObj.Setup(x => x.WeightSegmentCo2Repository.Find(It.IsAny<Expression<Func<WeightSegmentCo2, bool>>>())).Returns(() => muow.WeightSegmentCo2Repository.Find(x => x.MMID== mmid));
            
            var response = controller.CheckExixtanceofData(mmid, mYear);
            var message = response.Content.ReadAsStringAsync().Result.Trim('\"').ToString();
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("Do you want to copy the Efficiency Class data from 2019 to 2020? ALERT:On Copy, the 2020 unpublished data will be permanently replaced by the 2019 data ", message);
        }
        [TestMethod]
        public void CheckExixtanceofData_PublishedDataNotExistTest()
        {
            int mmid = 2;
            int mYear = 2020;
            mocObj.Setup(x => x.Market2MarketTypeParameterGroupRepository.Find(It.IsAny<Expression<Func<Market2MarketTypeParameterGroup, bool>>>())).Returns(() => muow.Market2MarketTypeParameterGroupRepository.Find(x => x.MMId == mmid));
            mocObj.Setup(x => x.FormulaRepository.Find(It.IsAny<Expression<Func<Formula, bool>>>())).Returns(() => muow.FormulaRepository.Find(x => x.MMId == mmid));
            mocObj.Setup(x => x.VariableRepository.Find(It.IsAny<Expression<Func<Variable, bool>>>())).Returns(() => muow.VariableRepository.Find(x => x.MMId == mmid));
            mocObj.Setup(x => x.EfficiencyClassRangeRepository.Find(It.IsAny<Expression<Func<EfficiencyClassRange, bool>>>())).Returns(() => muow.EfficiencyClassRangeRepository.Find(x => x.MMID == mmid));
            mocObj.Setup(x => x.WeightSegmentCo2Repository.Find(It.IsAny<Expression<Func<WeightSegmentCo2, bool>>>())).Returns(() => muow.WeightSegmentCo2Repository.Find(x => x.MMID == mmid));

            var response = controller.CheckExixtanceofData(mmid, mYear);
            List<ErrorMessage> jsonContent = (List<ErrorMessage>)response.Content.ReadAsAsync(typeof(List<ErrorMessage>)).Result;
            var message = jsonContent[0].ModelState[0].Message;
            Assert.AreEqual(System.Net.HttpStatusCode.InternalServerError, response.StatusCode);
            Assert.AreEqual("No published data is present in 2019 to copy", message);
        }
        
    }
}
