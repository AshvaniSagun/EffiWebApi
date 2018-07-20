using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EfficiencyClassWebAPI.Controllers;
using EfficiencyClassWebAPI.Models;
using EfficiencyClassWebAPI.EF;
using EfficiencyClass.UnitTests.MockData;
using System.Collections.Generic;
using EfficiencyClassWebAPI.Repository;
using Moq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Web.Http;

namespace EfficiencyClass.UnitTests.ControllersTests
{
    [TestClass]
    public class CsvUploadControllerTests
    {
        private CsvUploadController controller;
        private MockUnitOfWork muow;
        Mock<UnitofWork> mocObj = new Mock<UnitofWork>();

        [TestInitialize]
        public void SetUp()
        {
            muow = new MockUnitOfWork();
            controller = new CsvUploadController(mocObj.Object);
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
        public void GetPNO12Details_Test()
        {
            int mmid = 1;
            var data = muow.EfficiencyClassRangeRepository.Find(x => x.MMID == mmid);
            mocObj.Setup(x => x.WeightSegmentCo2Repository.GetAll()).Returns(() => muow.WeightSegmentCo2Repository.GetAll());
            mocObj.Setup(x => x.WeightSegmentCo2Repository.Find(It.IsAny<Expression<Func<WeightSegmentCo2, bool>>>())).Returns(() => muow.WeightSegmentCo2Repository.Find(y => y.MMID == mmid));
            mocObj.Setup(x => x.StagedWeightSegmentCo2Repository.Find(It.IsAny<Expression<Func<StagedWeightSegmentCo2, bool>>>())).Returns(() => muow.StagedWeightSegmentCo2Repository.Find(y => y.MMID == mmid));
            var response = controller.Get(mmid);
            Assert.IsNotNull(response.Content);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void DeletePNO12Details_Test()
        {
            int ewid = 2;
            StagedWeightSegmentCo2 rangeData = new StagedWeightSegmentCo2();
            mocObj.Setup(x => x.StagedWeightSegmentCo2Repository.Find(It.IsAny<Expression<Func<StagedWeightSegmentCo2, bool>>>())).Returns(() => muow.StagedWeightSegmentCo2Repository.Find(x => x.EwId == ewid));
            mocObj.Setup(x => x.StagedWeightSegmentCo2Repository.Remove(It.IsAny<StagedWeightSegmentCo2>())).Callback(() => muow.StagedWeightSegmentCo2Repository.Remove(rangeData));
            var response = controller.DeleteCsvUploadedData(ewid);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
    }
}
