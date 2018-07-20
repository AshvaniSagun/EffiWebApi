using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using EfficiencyClassWebAPI.Models;
using EfficiencyClassWebAPI.Repository;

namespace EfficiencyClassWebAPI.Controllers
{
    [Authorize]
    //[EnableCors(origins: "https://dev-efficiency-class-ui.azurewebsites.net", headers: "*", methods: "*")]
    public class UserManagementController : ApiController
    {
        readonly UserManagementModel userObject;

        public UserManagementController()
        {
            userObject = new UserManagementModel();
        }

        public UserManagementController(UnitofWork unitofWork)
        {
            userObject = new UserManagementModel(unitofWork);
        }

        [HttpGet]
        [Route("api/UserManagement/GetUserDetails")]
        public HttpResponseMessage GetUserDetails()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, userObject.GetUserDetails());
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Error.ParameterEmpty(System.Convert.ToString(ex.Message)));
            }
        }

        [HttpGet]
        [Route("api/UserManagement/GetUserMarketDetails")]
        public HttpResponseMessage GetUserMarketDetails(int uId)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, userObject.GetUserMarketDetails(uId));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Error.ParameterEmpty(System.Convert.ToString(ex.Message)));
            }
        }

        [HttpGet]
        [Route("api/UserManagement/GetTypeOfUser")]
        public HttpResponseMessage GetTypeOfUser(string cdsid)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, userObject.GetTypeOfUser(cdsid));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Error.ParameterEmpty(System.Convert.ToString(ex.Message)));
            }
        }

        [HttpPost]
        public HttpResponseMessage AddUserDetails(UserManagementModel userDetails)
        {
            try
            {
                if (ModelState.IsValid && (userDetails != null))
                {

                    userObject.AddUserDetails(userDetails);
                    return Request.CreateResponse(HttpStatusCode.Created, "User details added successfully");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError);
                }
            }
            catch (Exception ex)
            {
                string message = String.Empty;
                string error = Resource.GetResxValueByName("UserDuplicatemsg");
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
        public HttpResponseMessage UpdateUserDetails(UserManagementModel userDetails)
        {
            try
            {
                if (ModelState.IsValid && userDetails != null)
                {
                    userObject.UpdateUserDetails(userDetails);
                    return Request.CreateResponse(HttpStatusCode.OK, "User Details Updated successfully");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError);
                }
            }
            catch (Exception ex)
            {
                string message = String.Empty;
                string error = Resource.GetResxValueByName("UserDuplicatemsg");
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
        public HttpResponseMessage DeleteUserDetails(int id)
        {
            try
            {
                userObject.DeleteUserDetails(id);
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
