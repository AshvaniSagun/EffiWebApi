using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EfficiencyClassWebAPI.Models;
using EF = EfficiencyClassWebAPI.EF;
using static EfficiencyClassWebAPI.Models.MarketDataModel;
using EfficiencyClassWebAPI.Repository;

namespace EfficiencyClassWebAPI.Controllers
{
    [Authorize]
    public class MarketController : ApiController
    {
        readonly MarketDataModel marketObj;

        public MarketController()
        {
            marketObj = new MarketDataModel();
        }

        public MarketController(UnitofWork unitofWork)
        {
            marketObj = new MarketDataModel(unitofWork);
        }

        [HttpGet]
        [Route("api/Market/Get")]
        public HttpResponseMessage Get(string cdsid)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, marketObj.GetMarketData(cdsid));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Error.ParameterEmpty(System.Convert.ToString(ex.Message)));
            }
        }

        private List<EF.Market2MarketTypeParameterGroup> GetMarketList()
        {
            List<EF.Market2MarketTypeParameterGroup> lstmarket = new List<EF.Market2MarketTypeParameterGroup>();
            try
            {
                var result = marketObj.GetMarket2MarketTypeParameterGroup();
                foreach(var item in result)
                {
                    lstmarket.Add(new EF.Market2MarketTypeParameterGroup()
                    {
                        MMId = item.MMId,
                        MarketId = item.MarketId,
                        MarketTypeId = item.MarketTypeId,
                        ParameterGroupId = item.ParameterGroupId,
                        MYear = item.MYear,
                        CreatedBy = item.CreatedBy,
                        CreatedOn = DateTime.Now,
                        UpdatedBy = item.UpdatedBy,
                        UpdatedOn = item.UpdatedOn
                    });
                }
                return lstmarket;
            }
            catch (Exception ex)
            {
                new Microsoft.ApplicationInsights.TelemetryClient().TrackException(ex);
                throw;
            }
        }
        [HttpPut]
        public HttpResponseMessage UpdateMarket([FromBody] IEnumerable<MarketDataModel> marketValue)
        {
            try
            {
                if (ModelState.IsValid && marketValue != null)
                {
                    List<EF.Market2MarketTypeParameterGroup> lstupdatedmarket = marketObj.UpdateMarketData(marketValue);
                    return Request.CreateResponse(HttpStatusCode.OK, lstupdatedmarket);
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
        [HttpPost]
        public HttpResponseMessage AddMarket(IEnumerable<MarketDataModel> marketValue)
        {
            try
            {
                if (ModelState.IsValid && (marketValue != null))
                {
                    marketObj.AddMarketData(marketValue);
                    return Request.CreateResponse(HttpStatusCode.Created, "Market Details Added Successfully");
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
        public HttpResponseMessage Delete(int mmid)
        {
            try
            {
                marketObj.DeleteMarket(mmid);
                return Request.CreateResponse(HttpStatusCode.OK, "Market Details deleted successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError,ex.Message);
            }
        }
    }
}
