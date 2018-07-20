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
    public class CsvUploadController : ApiController
    {
        readonly CsvUpload csvObj;

        public CsvUploadController()
        {
            csvObj = new CsvUpload();
        }

        public CsvUploadController(UnitofWork unitofWork)
        {
            csvObj = new CsvUpload(unitofWork);
        }
        [HttpGet]
        public HttpResponseMessage Get(int mmid)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, csvObj.GetCsv(mmid));
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Error.ParameterEmpty(System.Convert.ToString(ex.Message)));
            }
        }
        [HttpPut]
        public HttpResponseMessage UpdateCsv([FromBody] IEnumerable<CsvUpload> csvUpload)
        {
            try
            {
                if (ModelState.IsValid && csvUpload != null)
                {
                    var lstupdatedcsv = csvObj.UpdateCsvdata(csvUpload);
                    return Request.CreateResponse(HttpStatusCode.OK, lstupdatedcsv);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError);
                }
            }
            catch (Exception ex)
            {
                string message = String.Empty;
                string error = Resource.GetResxValueByName("PNO12Duplicatemsg");
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
        [Route("api/CsvUpload/AddCsv")]
        public HttpResponseMessage AddCsv([FromBody] IEnumerable<CsvUpload> CsvValue)
        {
            try
            {
                if( ModelState.IsValid && (CsvValue)!= null)
                {
                    csvObj.AddCsv(CsvValue);
                    return Request.CreateResponse(HttpStatusCode.Created, "Csv file uploaded successfully");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest,
                        Error.ParameterEmpty(string.Join(", ", ModelState.Values
                                                .SelectMany(x => x.Errors)
                                                .Select(x => x.ErrorMessage))));
                }
            }
            catch(Exception ex)
            {
                string message = String.Empty;
                string error = Resource.GetResxValueByName("PNO12Duplicatemsg");
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
        [Route("api/CsvUpload/DeleteCsvUploadedData")]
        public HttpResponseMessage DeleteCsvUploadedData(int ewId)
        {
            try
            {
                int eId = csvObj.DeleteCsvUploadedDataDetails(ewId);
                return Request.CreateResponse(HttpStatusCode.OK, "Uploaded data deleted successfully for Id: " + eId);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, Error.ParameterEmpty(System.Convert.ToString(ex.Message)));
            }

        }

    }
}
