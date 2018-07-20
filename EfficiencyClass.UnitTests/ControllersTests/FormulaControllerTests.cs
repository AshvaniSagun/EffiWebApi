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
    public class FormulaControllerTests
    {
        private FormulaController controller;
        private MockUnitOfWork muow;
        Mock<UnitofWork> mocObj = new Mock<UnitofWork>();

        [TestInitialize]
        public void SetUp()
        {
            muow = new MockUnitOfWork();
            controller = new FormulaController(mocObj.Object);
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
        public void GetFormulaDetails()
        {
            int mmid = 3;
            var data = muow.FormulaRepository.Find(x => x.MMId == mmid);
            mocObj.Setup(x => x.FormulaRepository.GetAll()).Returns(() => muow.FormulaRepository.GetAll());
            mocObj.Setup(x => x.FormulaRepository.Find(It.IsAny<Expression<Func<Formula, bool>>>())).Returns(() => muow.FormulaRepository.Find(y => y.MMId == mmid));

            mocObj.Setup(x => x.VariableTypeRepository.GetAll()).Returns(() => muow.VariableTypeRepository.GetAll());

            mocObj.Setup(x => x.VariableRepository.GetAll()).Returns(() => muow.VariableRepository.GetAll());
            mocObj.Setup(x => x.VariableRepository.Find(It.IsAny<Expression<Func<Variable, bool>>>())).Returns(() => muow.VariableRepository.Find(y => y.MMId == mmid));

            mocObj.Setup(x => x.StagedFormulaRepository.Find(It.IsAny<Expression<Func<StagedFormula, bool>>>())).Returns(() => muow.StagedFormulaRepository.Find(y => y.MMId == mmid));
            mocObj.Setup(x => x.StagedVariableRepository.Find(It.IsAny<Expression<Func<StagedVariable, bool>>>())).Returns(() => muow.StagedVariableRepository.Find(y => y.MMId == mmid));
            
            var response = controller.Get(mmid);
            Assert.IsNotNull(response.Content);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void AddFormulaDetails_Test()
        {
            IList<FormulaModel> formulaDetails = new MockInputData().FormulaDetailsInput();
            List<StagedFormula> formulaData = new List<StagedFormula>();
            StagedFormula stf = new StagedFormula();
            stf.ID = 0;
            stf.MMId = 10;
            stf.FormulaDefinition = "((CO2-SegmentCO2)/SegmentCO2*100)";
            stf.VariableId = 7;
            stf.FormulaPriority = 1;
            stf.CreatedBy = "Admin";
            stf.CreatedOn = DateTime.Now;
            stf.UpdatedBy = "Admin";
            stf.UpdatedOn = DateTime.Now;
            formulaData.Add(stf);
            List<StagedFormulaDependencyDetail> formulaDepDet = new List<StagedFormulaDependencyDetail>();
          
            mocObj.Setup(x => x.FormulaRepository.GetAll()).Returns(() => muow.FormulaRepository.GetAll());

            mocObj.Setup(y => y.StagedFormulaRepository.Find(It.IsAny<Expression<Func<StagedFormula, bool>>>())).Returns(() => muow.StagedFormulaRepository.Find(x => (x.MMId == formulaDetails[0].Mmid && x.VariableId == formulaDetails[0].VariableId)));
            mocObj.Setup(x => x.StagedFormulaRepository.AddRange(It.IsAny<List<StagedFormula>>())).Callback(() => muow.StagedFormulaRepository.AddRange(formulaData));

            mocObj.Setup(x => x.StagedFormulaDependencyRepository.AddRange(It.IsAny<List<StagedFormulaDependencyDetail>>())).Callback(() => muow.StagedFormulaDependencyRepository.AddRange(formulaDepDet));
            mocObj.Setup(x => x.VariableRepository.GetAll()).Returns(() => muow.VariableRepository.GetAll());
            mocObj.Setup(x => x.VariableRepository.Find(It.IsAny<Expression<Func<Variable, bool>>>())).Returns(() => muow.VariableRepository.Find(y => y.Id == formulaDetails[0].VariableId && y.MMId == formulaDetails[0].Mmid));
            mocObj.Setup(x => x.StagedVariableRepository.Find(It.IsAny<Expression<Func<StagedVariable, bool>>>())).Returns(() => muow.StagedVariableRepository.Find(y => y.Id == formulaDetails[0].VariableId && y.MMId == formulaDetails[0].Mmid));

            var response = controller.AddFormulaAndDependency(formulaDetails);

            Assert.AreEqual(System.Net.HttpStatusCode.Created, response.StatusCode);
        }

        [TestMethod]
        public void DeleteFormulaDetails_Test()
        {
            int formulaId = 3;
            StagedFormula formulaData = new StagedFormula();
            StagedFormulaDependencyDetail formulaDepData = new StagedFormulaDependencyDetail();

            mocObj.Setup(x => x.StagedFormulaDependencyRepository.Find(It.IsAny<Expression<Func<StagedFormulaDependencyDetail, bool>>>())).Returns(() => muow.StagedFormulaDependencyRepository.Find(x => x.FormulaId == formulaId));
            mocObj.Setup(x => x.StagedFormulaDependencyRepository.Remove(It.IsAny<StagedFormulaDependencyDetail>())).Callback(() => muow.StagedFormulaDependencyRepository.Remove(formulaDepData));

            mocObj.Setup(x => x.StagedFormulaRepository.Find(It.IsAny<Expression<Func<StagedFormula, bool>>>())).Returns(() => muow.StagedFormulaRepository.Find(x => x.ID == formulaId));
            mocObj.Setup(x => x.StagedFormulaRepository.Remove(It.IsAny<StagedFormula>())).Callback(() => muow.StagedFormulaRepository.Remove(formulaData));
           
            var response = controller.DeleteFormula(formulaId);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode);
        }
    }
}
