using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EfficiencyClassWebAPI.Models;

namespace EfficiencyClassWebAPI.Controllers
{
    [Authorize]
    public class PublishController : ApiController
    {
      readonly PublishModel publishObj = new PublishModel();
        [HttpPost]
        public HttpResponseMessage PublishMarketDetails(int MMID)
        {
            try
            {
                publishObj.PublishMarketDetails(MMID);
                return Request.CreateResponse(HttpStatusCode.OK,"Market Details Published successfully");
            }
            catch(Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Error.ParameterEmpty(System.Convert.ToString(ex.Message)));
            }
        }

        [HttpPost]
        [Route("api/Publish/PreviewDetailsTobePublished")]
        public HttpResponseMessage PreviewDetailsTobePublished(int MMID)
        {
            try
            {
                string message = publishObj.DetailsTobePublished(MMID);
                return Request.CreateResponse(HttpStatusCode.OK, message);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Error.ParameterEmpty(System.Convert.ToString(ex.Message)));
            }
        }
    }
}
