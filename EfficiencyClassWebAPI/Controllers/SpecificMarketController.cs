using EfficiencyClassWebAPI.Models;
using EfficiencyClassWebAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EfficiencyClassWebAPI.Controllers
{
    [Authorize]
    public class SpecificMarketController : ApiController
    {
        readonly Marketdetails marketObj;

        public SpecificMarketController()
        {
            marketObj = new Marketdetails();
        }

        public SpecificMarketController(UnitofWork unitofWork)
        {
            marketObj = new Marketdetails(unitofWork);
        }

        [HttpGet]
        public HttpResponseMessage GetSpecificMarketDetails()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, marketObj.GetSpecificMarketDetails());
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Error.ParameterEmpty(System.Convert.ToString(ex.Message)));
            }
        }

        [HttpPost]
        public HttpResponseMessage AddSpecificMarketDetails(Marketdetails marketdetails)
        {
            try
            {
                if (ModelState.IsValid && (marketdetails != null))
                {
                    marketObj.AddSpecificMarketDetails(marketdetails);
                    return Request.CreateResponse(HttpStatusCode.Created, "Market details added successfully");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError);
                }
            }
            catch (Exception ex)
            {
                string message = String.Empty;
                string error = Resource.GetResxValueByName("MarketDuplicatemsg");
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
        public HttpResponseMessage UpdateSpecificMarketDetails(Marketdetails marketdetails)
        {
            try
            {
                if (ModelState.IsValid && marketdetails != null)
                {
                    marketObj.UpdateSpecificMarketDetails(marketdetails);
                    return Request.CreateResponse(HttpStatusCode.OK, "Market Details Updated successfully");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError);
                }
            }
            catch (Exception ex)
            {
                string message = String.Empty;
                string error = Resource.GetResxValueByName("MarketDuplicatemsg");
                if (ex.Message.Contains(error))
                {
                    message = error;
                }
                {
                    message = ex.Message.ToString();
                }
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Error.ParameterEmpty(message));
            }
        }

        [HttpDelete]
        public HttpResponseMessage DeleteSpecificMarketDetails(int id)
        {
            try
            {
                marketObj.DeleteSpecificMarketDetails(id);
                return Request.CreateResponse(HttpStatusCode.OK, " Data deleted successfully for Id: " + id);

            }
            catch (Exception ex)
            {
                new Microsoft.ApplicationInsights.TelemetryClient().TrackException(ex);
                throw;
            }
        }
    }
}
