using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using EfficiencyClassWebAPI.Models;
using EfficiencyClassWebAPI.Repository;

namespace EfficiencyClassWebAPI.Controllers
{
    [Authorize]
    public class FormulaController : ApiController
    {
        readonly FormulaModel formulaObj;
        public FormulaController()
        {
            formulaObj = new FormulaModel();
        }

        public FormulaController(UnitofWork unitofWork)
        {
            formulaObj = new FormulaModel(unitofWork);
        }

        [HttpGet]
        public HttpResponseMessage Get(int mmid)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, formulaObj.GetFormula(mmid));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Error.ParameterEmpty(ex.Message.ToString()));
            }
        }

        [HttpPost]
        [Route("api/Formula/AddFromulaAndDependency")]
        public HttpResponseMessage AddFormulaAndDependency(IEnumerable<FormulaModel> formulaValue)
        {
            try
            {
                if (ModelState.IsValid && (formulaValue != null))
                {
                    formulaObj.AddFormulaDetails(formulaValue);
                    return Request.CreateResponse(HttpStatusCode.Created, "formula details added successfully");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest,
                        Error.ParameterEmpty(string.Join(", ", ModelState.Values
                                                .SelectMany(x => x.Errors)
                                                .Select(x => x.ErrorMessage))));
                }
            }
            catch (Exception ex)
            {
                string message = String.Empty;
                string error = Resource.GetResxValueByName("FormulaDuplicatemsg");
                if (ex.Message.Contains(error))
                {
                    message = error;
                }
                {
                    message = System.Convert.ToString(ex.Message);
                }
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Error.ParameterEmpty(message));
            }
        }


        [HttpPut]
        public HttpResponseMessage UpdateFormula([FromBody] IEnumerable<FormulaModel> formulaValue)
        {
            try
            {
                if (ModelState.IsValid && formulaValue != null)
                {
                    formulaObj.UpdateFormulaDetails(formulaValue);
                    return Request.CreateResponse(HttpStatusCode.OK, "Formulae Updated successfully");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest,
                         Error.ParameterEmpty(string.Join(", ", ModelState.Values
                                                 .SelectMany(x => x.Errors)
                                                 .Select(x => x.ErrorMessage))));
                }
            }
            catch (Exception ex)
            {
                string message = String.Empty;
                string error = Resource.GetResxValueByName("FormulaDuplicatemsg");
                if (ex.Message.Contains(error))
                {
                    message = error;
                }
                {
                    message = System.Convert.ToString(ex.Message);
                }
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Error.ParameterEmpty(message));
            }
        }
        [HttpDelete]
        public HttpResponseMessage DeleteFormula(int formulaId)
        {
            try
            {
                int fId = formulaObj.DeleteFormulaAndDependency(formulaId);
                return Request.CreateResponse(HttpStatusCode.OK, "Formula deleted successfully for Id: " + fId);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Error.ParameterEmpty(System.Convert.ToString(ex.Message)));
            }

        }

        [HttpPost]
        [Route("api/Formula/FormulaParsing")]
        public HttpResponseMessage FormulaParsing(TestFormula testFormula)
        {
            try
            {
                var variableList = formulaObj.ParseFormula(testFormula);
                return Request.CreateResponse(HttpStatusCode.OK, variableList);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Error.ParameterEmpty(System.Convert.ToString(ex.Message)));
            }

        }

        [HttpPost]
        [Route("api/Formula/TestFormula")]
        public HttpResponseMessage TestFormula(TestFormula testFormula)
        {
            try
            {
                var result = formulaObj.TestFormulaFunctionality(testFormula);
                return Request.CreateResponse(HttpStatusCode.OK, result);

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Error.ParameterEmpty(System.Convert.ToString(ex.Message)));
            }

        }

        [HttpPost]
        [Route("api/Formula/EfficiencyClassCalculation")]
        public async Task<IEnumerable<TestResponseVo>> EfficiencyClassCalculation(IEnumerable<InputRequest> inputparam)
        {
            const string url = "/api/EfficiencyTestFunctionality/EfficiencyClassTestCalculation";
            IEnumerable<TestResponseVo> responseparameter = null;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await GetResponse(inputparam, client, url);
            if (response.IsSuccessStatusCode)
            {
                responseparameter = await response.Content.ReadAsAsync<IEnumerable<TestResponseVo>>();
            }
            return responseparameter;
        }
        private static System.Threading.Tasks.Task<HttpResponseMessage> GetResponse(IEnumerable<InputRequest> inputparam, HttpClient client, string url)
        {

            return client.PostAsJsonAsync(System.Configuration.ConfigurationManager.AppSettings["EfficiencyClassServiceURL"] + url, inputparam);
        }
    }

}