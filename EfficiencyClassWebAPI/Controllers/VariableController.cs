using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EfficiencyClassWebAPI.Models;
using EfficiencyClassWebAPI.Repository;

namespace EfficiencyClassWebAPI.Controllers
{
    [Authorize]
    public class VariableController : ApiController
    {
        readonly VariablesModel variableObj;

        public VariableController()
        {
            variableObj = new VariablesModel();
        }

        public VariableController(UnitofWork unitofWork)
        {
            variableObj = new VariablesModel(unitofWork);
        }

        [HttpGet]
        [Route("api/Variable/GetVariable")]
        public HttpResponseMessage Get(int mmid)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, variableObj.Get(mmid));
            }
            catch (Exception ex)
            {

                string message = String.Empty;
                string error = Resource.GetResxValueByName("VariableDuplicatemsg");
                if (ex.Message.Contains(error))
                {
                    message = error;
                }
                else
                {
                    message = System.Convert.ToString(ex.Message);
                }
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Error.ParameterEmpty(message));
            }
        }

        [HttpPut]
        public HttpResponseMessage UpdateVariable([FromBody] IEnumerable<VariablesModel> variableValue)
        {
            try
            {
                if (ModelState.IsValid && variableValue != null)
                {
                    var response = variableObj.UpdateVariable(variableValue);
                    int varId = (int)response.First().Id;
                    return Request.CreateResponse(HttpStatusCode.OK, "Variable updated successfully for " + varId);
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
                string error = Resource.GetResxValueByName("VariableDuplicatemsg");
                if (ex.Message.Contains(error))
                {
                    message = error;
                }
                else
                {
                    message = System.Convert.ToString(ex.Message);
                }
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Error.ParameterEmpty(message));

            }
        }
        [HttpPost]
        public HttpResponseMessage AddVariable([FromBody] IEnumerable<VariablesModel> variableValue)
        {
            try
            {

                if (ModelState.IsValid && (variableValue != null))
                {
                    var response = variableObj.AddVariable(variableValue);
                    int varId = (int)response.First().Id;
                    return Request.CreateResponse(HttpStatusCode.Created, "Variable added successfully for " + varId);
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
                string error = Resource.GetResxValueByName("VariableDuplicatemsg");
                if (ex.Message.Contains(error))
                {
                    message = error;
                }
                else
                {
                    message = System.Convert.ToString(ex.Message);
                }
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Error.ParameterEmpty(message));

            }

        }
        [HttpDelete]
        public HttpResponseMessage DeleteVariable(int variableId)
        {
            try
            {
                variableObj.DeleteVariable(variableId);
                return Request.CreateResponse(HttpStatusCode.OK, "Variable deleted successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Error.ParameterEmpty(ex.Message.ToString()));

            }
        }

    }
}
