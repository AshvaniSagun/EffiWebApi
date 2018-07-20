using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EfficiencyClassWebAPI.Models;

namespace EfficiencyClassWebAPI.Controllers
{
    public class WeightSegmentCO2Controller : ApiController
    {
        readonly WeightSegmentCO2 segmentco2obj = new WeightSegmentCO2();
        [HttpPost]
        [Route("api/WeightSegmentCO2/AddSegmentCO2")]
        public HttpResponseMessage AddSegmentCO2(IEnumerable<WeightSegmentCO2> CO2Value)
        {
            try
            {

                if (ModelState.IsValid && (CO2Value != null))
                {
                    var response = segmentco2obj.AddSegmentCO2(CO2Value);
                    int varId = (int)CO2Value.First().EwId;
                    return Request.CreateResponse(HttpStatusCode.Created, "Weight & SegmentCO2  added successfully");
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
                string error = Resource.GetResxValueByName("segmentco2Duplicatemsg");
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
    }
}
