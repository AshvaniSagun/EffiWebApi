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
    public class MasterDataController : ApiController
    {
        public class MasterData
        {
            public long id;
            public string value;
        }
        readonly MasterDropdowndataModel marketTypeObj;
        readonly Marketdetails marketObj;

       

        public MasterDataController()
        {
            marketObj = new Marketdetails();
            marketTypeObj = new MasterDropdowndataModel();
        }

        public MasterDataController(UnitofWork unitofWork)
        {
            marketObj = new Marketdetails(unitofWork);
            marketTypeObj = new MasterDropdowndataModel(unitofWork);
        }

        private List<Marketdetails> GetMarketList(string cdsid)
        {
            List<Marketdetails> lstMarket = new List<Marketdetails>();
            try
            {
                lstMarket = marketObj.GetMarketDetails(cdsid);
                lstMarket = lstMarket.OrderBy(x => x.MarketName).ToList();
                return lstMarket;
            }
            catch (Exception ex)
            {
                new Microsoft.ApplicationInsights.TelemetryClient().TrackException(ex);
                throw;
            }
        }
        private List<Marketdetails> GetMarketList()
        {
            List<Marketdetails> lstMarket = new List<Marketdetails>();
            try
            {
                lstMarket = marketObj.GetSpecificMarketDetails();
                lstMarket = lstMarket.OrderBy(x => x.MarketName).ToList();
                return lstMarket;
            }
            catch (Exception ex)
            {
                new Microsoft.ApplicationInsights.TelemetryClient().TrackException(ex);
                throw;
            }
        }
        private List<MasterData> GetMarketTypeList()
        {
            List<MasterData> lstMarketType = new List<MasterData>();
            try
            {
                var result = marketTypeObj.GetAllMarketType();
                foreach (var param in result)
                {
                    lstMarketType.Add(new MasterData()
                    {
                       id=param.Id,
                       value=param.MarketTypeName
                      
                    });
                }
                lstMarketType = lstMarketType.OrderBy(x => x.value).ToList();
                return lstMarketType;
            }
            catch (Exception ex )
            {
                new Microsoft.ApplicationInsights.TelemetryClient().TrackException(ex);
                throw;
            }
        }
        private List<MasterData> GetParameterGroupMasterList()
        {
            List<MasterData> lstParameterGroupMaster = new List<MasterData>();
            try
            {
                var result = marketTypeObj.GetAllParameterGroupMaster();
                foreach (var param in result)
                {
                    lstParameterGroupMaster.Add(new MasterData()
                    {
                        id = param.ParameterGroupId,
                        value = param.ParameterGroupName

                    });
                }
                lstParameterGroupMaster = lstParameterGroupMaster.OrderBy(x => x.value).ToList();
                return lstParameterGroupMaster;
            }
            catch (Exception ex)
            {
                new Microsoft.ApplicationInsights.TelemetryClient().TrackException(ex);
                throw;
            }
        }
        private List<MasterData> GetVariableTypeList()
        {
            List<MasterData> lstVariableType = new List<MasterData>();
            try
            {
                var result = marketTypeObj.GetAllVariableType();
                foreach (var param in result)
                {
                    lstVariableType.Add(new MasterData()
                    {
                        id = param.Id,
                        value = param.VariableTypeName

                    });
                }
                lstVariableType = lstVariableType.OrderBy(x => x.value).ToList();
                return lstVariableType;
            }
            catch (Exception ex )
            {
                new Microsoft.ApplicationInsights.TelemetryClient().TrackException(ex);
                throw;
            }
        }
        private List<MasterData> GetMarketModelYearList(int mID)
        {
            List<MasterData> lstMarketModelYear = new List<MasterData>();
            try
            {
                var result = marketTypeObj.GetMarketModelYearData(mID);
                foreach (var param in result)
                {
                    lstMarketModelYear.Add(new MasterData()
                    {
                        id = param.MMId,
                        value = Convert.ToString(param.MYear)

                    });
                }
                lstMarketModelYear = lstMarketModelYear.OrderBy(x => x.value).ToList();
                return lstMarketModelYear;
            }
            catch (Exception ex )
            {
                new Microsoft.ApplicationInsights.TelemetryClient().TrackException(ex);
                throw;
            }
        }

        private List<MasterData> GetResultTypeVariables()
        {
            List<MasterData> lstResultTypeVariables = new List<MasterData>();
            try
            {
                var result = marketTypeObj.GetResultTypeVariable();
                foreach (var param in result)
                {
                    lstResultTypeVariables.Add(new MasterData()
                    {
                        id = param.Id,
                        value = param.VariableName
                    });
                }
                lstResultTypeVariables = lstResultTypeVariables.OrderBy(x => x.value).ToList();
                return lstResultTypeVariables;
            }
            catch (Exception ex )
            {
                new Microsoft.ApplicationInsights.TelemetryClient().TrackException(ex);
                throw;
            }
        }

        private List<MasterData> GetFuelTypeList()
        {
            List<MasterData> lstFuelType = new List<MasterData>();
            try
            {
                var result = marketTypeObj.GetFuelTypeList();
                foreach (var param in result)
                {
                    lstFuelType.Add(new MasterData()
                    {
                        id = param.Id,
                        value = param.VariableTypeName
                    });
                }
                lstFuelType = lstFuelType.OrderBy(x => x.value).ToList();
                return lstFuelType;
            }
            catch (Exception ex )
            {
                new Microsoft.ApplicationInsights.TelemetryClient().TrackException(ex);
                throw;
            }
        }

        private List<MasterData> GetVariables(int MMId)
        {
            List<MasterData> lstvariableName = new List<MasterData>();
            try
            {
                var result = marketTypeObj.GetVariableList(MMId);
                foreach (var param in result)
                {
                    lstvariableName.Add(new MasterData()
                    {
                        id = param.Id,
                        value = param.VariableName

                    });
                }
                lstvariableName = lstvariableName.OrderBy(x => x.value).ToList();
                return lstvariableName;
            }
            catch (Exception ex )
            {
                new Microsoft.ApplicationInsights.TelemetryClient().TrackException(ex);
                throw;
            }
        }

        private List<MasterData> GetUserRoleDetails()
        {
            List<MasterData> userRoleList= new List<MasterData>();
            try
            {
                var result = marketTypeObj.GetUserRole();
                foreach (var param in result)
                {
                    userRoleList.Add(new MasterData()
                    {
                        id = param.Id,
                        value = param.RoleName

                     });
                }
                userRoleList = userRoleList.OrderBy(x => x.value).ToList();
                return userRoleList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private List<MasterData> GetInputTypeVariableList()
        {
            List<MasterData> inputVariableList = new List<MasterData>();
            try
            {
                var result = marketTypeObj.GetInputTypeVariableList();
                foreach (var param in result)
                {
                    inputVariableList.Add(new MasterData()
                    {
                        id = param.Id,
                        value = param.VariableTypeName

                    });
                }
                inputVariableList = inputVariableList.OrderBy(x => x.value).ToList();
                return inputVariableList;
            }
            catch (Exception ex )
            {
                new Microsoft.ApplicationInsights.TelemetryClient().TrackException(ex);
                throw;
            }
        }

        private List<MasterData> GetFuelTypeVariableList()
        {
            List<MasterData> fuelTypeVariableList = new List<MasterData>();
            try
            {
                var result = marketTypeObj.GetFuelTypeVariableList();
                foreach (var param in result)
                {
                    fuelTypeVariableList.Add(new MasterData()
                    {
                        id = param.Id,
                        value = param.VariableTypeName

                    });
                }
                fuelTypeVariableList = fuelTypeVariableList.OrderBy(x => x.value).ToList();
                return fuelTypeVariableList;
            }
            catch (Exception ex)
            {
                new Microsoft.ApplicationInsights.TelemetryClient().TrackException(ex);
                throw;
            }
        }

        [HttpGet]
        [Route("api/MasterData/GetMarket")]
        public HttpResponseMessage GetMarket(string cdsid)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, GetMarketList(cdsid));
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }
        [HttpGet]
        [Route("api/MasterData/GetSpecificMarketList")]
        public HttpResponseMessage GetSpecificMarketList()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, GetMarketList());
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [HttpGet]
        [Route("api/MasterData/GetMarketType")]
        public HttpResponseMessage GetMarketType()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, GetMarketTypeList());
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [HttpGet]
        [Route("api/MasterData/GetParameterGroupMaster")]
        public HttpResponseMessage GetParameterGroupMaster()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, GetParameterGroupMasterList());
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [HttpGet]
        [Route("api/MasterData/GetVariableType")]
        public HttpResponseMessage GetVariableType()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, GetVariableTypeList());
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [HttpGet]
        [Route("api/MasterData/GetMarketModelYear")]
        public HttpResponseMessage GetMarketModelYear(int marketId)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, GetMarketModelYearList(marketId));
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [HttpGet]
        [Route("api/MasterData/GetResultTypeVariable")]
        public HttpResponseMessage GetResultTypeVariable()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, GetResultTypeVariables());
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [HttpGet]
        [Route("api/MasterData/GetFuelType")]
        public HttpResponseMessage GetFuelType()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, GetFuelTypeList());
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [HttpGet]
        [Route("api/MasterData/GetVariableList")]

        public HttpResponseMessage GetVariableList(int MMId)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, GetVariables(MMId));
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [HttpGet]
        [Route("api/MasterData/GetRoleDetails")]

        public HttpResponseMessage GetRoleDetails()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, GetUserRoleDetails());
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [HttpGet]
        [Route("api/MasterData/GetInputTypeVariables")]

        public HttpResponseMessage GetInputTypeVariables()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, GetInputTypeVariableList());
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

        [HttpGet]
        [Route("api/MasterData/GetFuelTypeVariables")]

        public HttpResponseMessage GetFuelTypeVariables()
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, GetFuelTypeVariableList());
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.ToString());
            }
        }

    }
}
