using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Http;
using System.Web.Http;
using EfficiencyClass.UnitTests.MockData;
using EfficiencyClassWebAPI.Controllers;
using EfficiencyClassWebAPI.EF;
using EfficiencyClassWebAPI.Models;
using EfficiencyClassWebAPI.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EfficiencyClass.UnitTests.ControllersTests
{
    [TestClass]
    public class SepcificMarketControllerTests
    {
        private SpecificMarketController controller;
        private MockUnitOfWork muow;
        Mock<UnitofWork> mocObj = new Mock<UnitofWork>();

        [TestInitialize]
        public void SetUp()
        {
            muow = new MockUnitOfWork();
            controller = new SpecificMarketController(mocObj.Object);
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
        public void GetSpecificMarketDetails_Test()
        { 
            mocObj.Setup(x => x.MarketRepository.GetAll()).Returns(() => muow.MarketRepository.GetAll());
            
            var response = controller.GetSpecificMarketDetails();
            Assert.IsNotNull(response.Content);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void AddSpecificMarketDetails_Test()
        {
            Marketdetails marketDetails = new MockInputData().SpecificMarketDetailsInput();
            Market marketData = new Market();
            mocObj.Setup(y => y.MarketRepository.Find(It.IsAny<Expression<Func<Market, bool>>>())).Returns(() => muow.MarketRepository.Find(x => x.SpecMarket == marketDetails.SpecMarketCode || x.MarketName == marketDetails.MarketName));
            mocObj.Setup(x => x.MarketRepository.Add(It.IsAny<Market>())).Callback(() => muow.MarketRepository.Add(marketData));
            
            var response = controller.AddSpecificMarketDetails(marketDetails);
            Assert.AreEqual(System.Net.HttpStatusCode.Created, response.StatusCode);
        }

        [TestMethod]
        public void AddSpecificMarketDuplicateDetails_Test()
        {
            Marketdetails marketDetails = new MockInputData().SpecificMarketDetailsDuplicateInput();
            Market marketData = new Market();
            mocObj.Setup(y => y.MarketRepository.Find(It.IsAny<Expression<Func<Market, bool>>>())).Returns(() => muow.MarketRepository.Find(x => x.SpecMarket == marketDetails.SpecMarketCode || x.MarketName == marketDetails.MarketName));
            mocObj.Setup(x => x.MarketRepository.Add(It.IsAny<Market>())).Callback(() => muow.MarketRepository.Add(marketData));
            
            var response = controller.AddSpecificMarketDetails(marketDetails);
            List<ErrorMessage> jsonContent = (List<ErrorMessage>)response.Content.ReadAsAsync(typeof(List<ErrorMessage>)).Result;
            var message = jsonContent[0].ModelState[0].Message;
            Assert.AreEqual(System.Net.HttpStatusCode.InternalServerError, response.StatusCode);
            Assert.AreEqual("Market Details already exist", message);
        }

    }
}
