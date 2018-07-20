using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EfficiencyClassWebAPI.Controllers;
using EfficiencyClassWebAPI.Models;
using EfficiencyClassWebAPI.EF;
using EfficiencyClass.UnitTests.MockData;
using System.Collections.Generic;
using EfficiencyClassWebAPI.Repository;
using Moq;
using System.Web.Http;
using System.Net.Http;
using System.Linq.Expressions;

namespace EfficiencyClass.UnitTests.ControllersTests
{
    [TestClass]
    public class VariableControllerTests
    {
        private VariableController controller;
        private MockUnitOfWork muow;
        Mock<UnitofWork> mocObj = new Mock<UnitofWork>();

        [TestInitialize]
        public void SetUp()
        {
            muow = new MockUnitOfWork();
            controller = new VariableController(mocObj.Object);
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
        public void GetVariableDetails()
        {
            int mmid = 3;
           
            var data = muow.VariableRepository.Find(x => x.MMId == mmid);

            mocObj.Setup(x => x.VariableRepository.GetAll()).Returns(() => muow.VariableRepository.GetAll());
            mocObj.Setup(x => x.VariableRepository.Find(It.IsAny<Expression<Func<Variable, bool>>>())).Returns(() => muow.VariableRepository.Find(y => y.MMId == mmid));

            mocObj.Setup(x => x.VariableTypeRepository.GetAll()).Returns(() => muow.VariableTypeRepository.GetAll());

            mocObj.Setup(x => x.StagedVariableRepository.Find(It.IsAny<Expression<Func<StagedVariable, bool>>>())).Returns(() => muow.StagedVariableRepository.Find(y => y.MMId == mmid));

            var response = controller.Get(mmid);
            Assert.IsNotNull(response.Content);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void AddVariableDetails_Test()
        {
            IList<VariablesModel> variableDetails = new MockInputData().VariablesDetailsInput();
            List<StagedVariable> variableData = new List<StagedVariable>();

            mocObj.Setup(x => x.VariableRepository.GetAll()).Returns(() => muow.VariableRepository.GetAll());

            mocObj.Setup(y => y.StagedVariableRepository.Find(It.IsAny<Expression<Func<StagedVariable, bool>>>())).Returns(() => muow.StagedVariableRepository.Find(x => (x.MMId == variableDetails[0].Mmid && x.Id == variableDetails[0].Id)));
            mocObj.Setup(x => x.StagedVariableRepository.AddRange(It.IsAny<List<StagedVariable>>())).Callback(() => muow.StagedVariableRepository.AddRange(variableData));

            var response = controller.AddVariable(variableDetails);

            Assert.AreEqual(System.Net.HttpStatusCode.Created, response.StatusCode);
        }

        [TestMethod]
        public void DeleteVariableDetails_Test()
        {
            int variableId = 17;
            StagedVariable variableData = new StagedVariable();

            mocObj.Setup(x => x.StagedVariableRepository.Find(It.IsAny<Expression<Func<StagedVariable, bool>>>())).Returns(() => muow.StagedVariableRepository.Find(x => x.Id == variableId));
            mocObj.Setup(x => x.StagedVariableRepository.Remove(It.IsAny<StagedVariable>())).Callback(() => muow.StagedVariableRepository.Remove(variableData));

            mocObj.Setup(x => x.StagedFormulaRepository.Find(It.IsAny<Expression<Func<StagedFormula, bool>>>())).Returns(() => muow.StagedFormulaRepository.Find(y => y.VariableId == variableId));

            mocObj.Setup(x => x.StagedFormulaDependencyRepository.Find(It.IsAny<Expression<Func<StagedFormulaDependencyDetail, bool>>>())).Returns(() => muow.StagedFormulaDependencyRepository.Find(y => y.VariableId == variableId));

            var response = controller.DeleteVariable(variableId);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

    }
}
