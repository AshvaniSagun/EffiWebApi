using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EfficiencyClassWebAPI.Models;

namespace EfficiencyClassWebAPI.Controllers
{
    public class Co2RangeController : ApiController
    {
        readonly CO2RangeModel CO2obj = new CO2RangeModel();
        private List<CO2RangeModel> GetCO2List()
        {
            List<CO2RangeModel> lstCOMMONCO2 = new List<CO2RangeModel>();
            try
            {
                var result = CO2obj.Get();
                foreach (var item in result)
                {
                    lstCOMMONCO2.Add(new CO2RangeModel()
                    {
                        Co2Id = item.Co2Id,
                        MMID = item.MMId,
                        StartRange = item.StartRange,
                        EndRange = item.EndRange,
                        ValueId = item.ValueId,
                        CreatedBy = item.CreatedBy,
                        CreatedOn = DateTime.UtcNow,
                        UpdatedBy = item.UpdatedBy,
                        UpdatedOn = DateTime.UtcNow
                    });
                }
                return lstCOMMONCO2;
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        [Route("api/COMMONCO2/GetCo2Range")]
        public HttpResponseMessage Get()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, GetCO2List());
            }
            catch (Exception ex)
            {

                string message = String.Empty;
                string error = Resource.GetResxValueByName("CO2RangeDuplicatemsg");
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
        [HttpPut]
        [Route("api/COMMONCO2/UpdateCo2Range")]
        public HttpResponseMessage Marketimport(IEnumerable<CO2RangeModel> CO2Value)
        {
            try
            {
                if (ModelState.IsValid && CO2Value != null)
                {
                    CO2obj.UpdateCO2Range(CO2Value);
                    int varId = (int)CO2Value.First().Co2Id;
                    return Request.CreateResponse(HttpStatusCode.OK, "CommonCO2 range Updated successfully");
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
                string error = Resource.GetResxValueByName("CO2RangeDuplicatemsg");
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
        [Route("api/COMMONCO2/FullImport")]
        public HttpResponseMessage FullImport(IEnumerable<CO2RangeModel> CO2Value)
        {
            try
            {

                if (ModelState.IsValid && (CO2Value != null))
                {
                    var response = CO2obj.AddCO2Range(CO2Value);
                    int varId = (int)CO2Value.First().Co2Id;
                    return Request.CreateResponse(HttpStatusCode.Created, "Common CO2 Range added successfully");
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
                string error = Resource.GetResxValueByName("CO2RangeDuplicatemsg");
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
        public HttpResponseMessage Delete(int CO2Id)
        {
            try
            {
                int CId = CO2obj.DeleteCO2Range(CO2Id);
                return Request.CreateResponse(HttpStatusCode.Created, "CommonCO2 range deleted successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Error.ParameterEmpty(ex.Message.ToString()));

            }
        }
    }
}
