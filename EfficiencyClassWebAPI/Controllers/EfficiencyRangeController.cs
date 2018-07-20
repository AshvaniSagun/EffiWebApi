using EfficiencyClassWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EfficiencyClassWebAPI.Repository;

namespace EfficiencyClassWebAPI.Controllers
{
    [Authorize]
    public class EfficiencyRangeController : ApiController
    {
        readonly EfficiencyRange ECrangeObj ;

        public EfficiencyRangeController()
        {
            ECrangeObj = new EfficiencyRange();
        }

        public EfficiencyRangeController(UnitofWork unitofWork)
        {
            ECrangeObj = new EfficiencyRange(unitofWork);
        }
        [HttpGet]
        [Route("api/EfficiencyRange/GetEfficiencyClassRange")]
        public HttpResponseMessage Get(int MMId)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, ECrangeObj.Get(MMId));
            }
            catch (Exception ex)
            {

                string message = String.Empty;
                string error = Resource.GetResxValueByName("EfficiencyClassRangeDuplicatemsg");
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
        public HttpResponseMessage UpdateEfficiencyClassRange([FromBody] IEnumerable<EfficiencyRange> ECRangeValue)
        {
            try
            {
                if (ModelState.IsValid && ECRangeValue != null)
                {
                    int EcRangeId=0;
                    var response = ECrangeObj.UpdateSpecificFuelTypeEfficiencyRange(ECRangeValue);
                    EcRangeId = (int)response.First().Id;
                    return Request.CreateResponse(HttpStatusCode.OK, "Efficiency Range Updated successfully for "  + EcRangeId);
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
                string error = Resource.GetResxValueByName("EfficiencyRangeDuplicatemsg");
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
        public HttpResponseMessage AddEfficiencyClassRange([FromBody] IEnumerable<EfficiencyRange> ECRangeValue)
        {
            try
            {
                if (ModelState.IsValid && ECRangeValue != null)
                {
                    int EcRangeId = 0;
                    var response = ECrangeObj.AddSpecificFuelTypeEfficiencyRange(ECRangeValue);
                    EcRangeId = (int)response.First().Id;
                    return Request.CreateResponse(HttpStatusCode.Created, "Efficiency Range added successfully for " + EcRangeId);
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
                string error = Resource.GetResxValueByName("EfficiencyRangeDuplicatemsg");
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
        public HttpResponseMessage DeleteEfficiencyClassRange(int Id,int MMID)
         {
            try
            {
                ECrangeObj.DeleteEfficiencyRange(Id, MMID);
                return Request.CreateResponse(HttpStatusCode.OK, "Efficiency Range deleted successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Error.ParameterEmpty(System.Convert.ToString(ex.Message)));

            }
        }
       
        [HttpPost]
        [Route("api/EfficiencyRange/CheckExixtanceofData")]
        public HttpResponseMessage CheckExixtanceofData([FromUri] int MMId,int copyToModelYear)
        {
            try
            {
                string message = ECrangeObj.MessageforCopyDetails(MMId, copyToModelYear);
                return Request.CreateResponse(HttpStatusCode.OK, message);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Error.ParameterEmpty(System.Convert.ToString(ex.Message)));
            }
        }

        [HttpPost]
        [Route("api/EfficiencyRange/CopyMarketDetails")]

        public HttpResponseMessage CopyMarketDetails([FromUri] int MMId, int copyToModelYear)
        {
            try
            {
                string message=ECrangeObj.CopyMarketDetails(MMId, copyToModelYear);
                return Request.CreateResponse(HttpStatusCode.OK, message);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Error.ParameterEmpty(System.Convert.ToString(ex.Message)));
            }
        }
    }
}
