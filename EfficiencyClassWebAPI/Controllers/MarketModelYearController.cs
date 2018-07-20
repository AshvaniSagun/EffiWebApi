using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EfficiencyClassWebAPI.Models;
namespace EfficiencyClassWebAPI.Controllers
{
    public class MarketModelYearController : ApiController
    {
        readonly MarketModelYear modelyearobj = new MarketModelYear();
        private List<MarketModelYear> GetmodelyearList()
        {
            List<MarketModelYear> lstmodelyear = new List<MarketModelYear>();
            try
            {
                var result = modelyearobj.GetModelYear();
                foreach (var item in result)
                {
                    lstmodelyear.Add(new MarketModelYear()
                    {
                        Id = item.MMYearId,
                        ModelYear = item.ModelYear,
                        MarketId = item.MarketId,
                        CreatedBy = item.CreatedBy,
                        CreatedOn = DateTime.UtcNow,
                        UpdatedBy = item.UpdatedBy,
                        UpdatedOn = DateTime.UtcNow
                    });
                }
                return lstmodelyear;
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        [Route("api/MarketModelYear/GetModelYear")]
        public HttpResponseMessage Get()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, GetmodelyearList());
            }
            catch (Exception ex)
            {

                string message = String.Empty;
                string error = Resource.GetResxValueByName("ModelYearDuplicatemsg");
                if (ex.Message.Contains(error))
                {
                    message = error;
                }
                else
                {
                    message = ex.Message.ToString();
                }
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Error.ParameterEmpty(message));
            }
        }

        [HttpPost]
        public HttpResponseMessage AddModelYear([FromBody] IEnumerable<MarketModelYear> modelYearValue)
        {
            try
            {

                if (ModelState.IsValid && (modelYearValue != null))
                {
                    var response = modelyearobj.AddModelyear(modelYearValue);
                    int varId = (int)modelYearValue.First().Id;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
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
                string error = Resource.GetResxValueByName("ModelYearDuplicatemsg");
                if (ex.Message.Contains(error))
                {
                    message = error;
                }
                else
                {
                    message = ex.Message.ToString();
                }
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Error.ParameterEmpty(message));

            }

        }
        [HttpDelete]
        public HttpResponseMessage Delete(int modelyearId)
        {
            try
            {
                int yearId = modelyearobj.DeleteModelYear(modelyearId);
                return Request.CreateResponse(HttpStatusCode.Created, "ModelYear deleted successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Error.ParameterEmpty(ex.Message.ToString()));

            }
        }
    }
}
