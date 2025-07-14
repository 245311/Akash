using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using WBS_API.DAL;
using WBS_API.Interface;
using WBS_API.Model;
using WBS_API.Repositories;

namespace WBS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubProjectController : ControllerBase
    {
        private readonly DBInsert dBInsert;
        private readonly ISubProjectRepository subProjectRepository;
        private readonly IRequestResponseLogRepository requestResponseLogRepository;

        public SubProjectController(DBInsert dBInsert, ISubProjectRepository subProjectRepository, IRequestResponseLogRepository requestResponseLogRepository)
        {

            this.dBInsert = dBInsert;
            this.subProjectRepository = subProjectRepository;
            this.requestResponseLogRepository = requestResponseLogRepository;
        }
        string reqDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        [HttpPost("AddSubProject")]

        public async Task<object> AddSubProject(JObject jsonrequest)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to process file." };

            try
            {
                // Validate file
                if (jsonrequest == null || !jsonrequest.HasValues)
                {
                    returnResponse.ResponseCode = "01";
                    returnResponse.ResponseMessage = "Invalid json request.";
                    return returnResponse;
                }

                // Process the JSON request
                returnResponse = subProjectRepository.AddSubProjectAsync(JsonConvert.SerializeObject(jsonrequest));
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "SubProjectController",
                    sourcepagemethod = "AddSubProject",
                    message = ex.Message,
                    stacktrace = ex.StackTrace,
                    param = jsonrequest.ToString(),
                    errortype = "Controller",
                    checkedcomment = "",
                    checkedby = "",
                };

                dBInsert.FunTmsErrorLog(errorLog);
                returnResponse.ResponseMessage = "Something went wrong";
            }
            finally
            {
                // Always log request and response
                //request = "File Upload: " + file.FileName,
                RequestResponseLog requestResponseLog = new RequestResponseLog();
                requestResponseLog.request = JsonConvert.SerializeObject(jsonrequest);
                requestResponseLog.response = JsonConvert.SerializeObject(returnResponse);
                requestResponseLog.participantid = "";
                requestResponseLog.reqtype = "AddSubProject";
                requestResponseLog.reqdate = reqDate;
                requestResponseLog.rspdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                requestResponseLogRepository.LOG_DB_ApiRequestResponseLog(requestResponseLog);
            }

            return returnResponse;
        }


        [HttpPost("GetTeam")]

        public async Task<object> GetTeam(JObject jsonrequest)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to process file." };

            try
            {
                

                // Process the JSON request
                returnResponse = subProjectRepository.GetTeamAsync(JsonConvert.SerializeObject(jsonrequest));
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "SubProjectController",
                    sourcepagemethod = "GetTeam",
                    message = ex.Message,
                    stacktrace = ex.StackTrace,
                    param = jsonrequest.ToString(),
                    errortype = "Controller",
                    checkedcomment = "",
                    checkedby = "",
                };

                dBInsert.FunTmsErrorLog(errorLog);
                returnResponse.ResponseMessage = "Something went wrong";
            }
            finally
            {
                // Always log request and response
                //request = "File Upload: " + file.FileName,
                RequestResponseLog requestResponseLog = new RequestResponseLog();
                requestResponseLog.request = JsonConvert.SerializeObject(jsonrequest);
                requestResponseLog.response = JsonConvert.SerializeObject(returnResponse);
                requestResponseLog.participantid = "";
                requestResponseLog.reqtype = "GetTeam";
                requestResponseLog.reqdate = reqDate;
                requestResponseLog.rspdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                requestResponseLogRepository.LOG_DB_ApiRequestResponseLog(requestResponseLog);
            }

            return returnResponse;
        }

        [HttpPost("GetClient")]

        public async Task<object> GetClient(JObject jsonrequest)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to process file." };

            try
            {
              
                // Process the JSON request
                returnResponse = subProjectRepository.GetClientAsync(JsonConvert.SerializeObject(jsonrequest));
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "SubProjectController",
                    sourcepagemethod = "GetClient",
                    message = ex.Message,
                    stacktrace = ex.StackTrace,
                    param = jsonrequest.ToString(),
                    errortype = "Controller",
                    checkedcomment = "",
                    checkedby = "",
                };

                dBInsert.FunTmsErrorLog(errorLog);
                returnResponse.ResponseMessage = "Something went wrong";
            }
            finally
            {
                // Always log request and response
                //request = "File Upload: " + file.FileName,
                RequestResponseLog requestResponseLog = new RequestResponseLog();
                requestResponseLog.request = JsonConvert.SerializeObject(jsonrequest);
                requestResponseLog.response = JsonConvert.SerializeObject(returnResponse);
                requestResponseLog.participantid = "";
                requestResponseLog.reqtype = "GetClient";
                requestResponseLog.reqdate = reqDate;
                requestResponseLog.rspdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                requestResponseLogRepository.LOG_DB_ApiRequestResponseLog(requestResponseLog);
            }

            return returnResponse;
        }


        [HttpPost("GetProject")]

        public async Task<object> GetProject(JObject jsonrequest)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to process file." };

            try
            {

                // Process the JSON request
                returnResponse = subProjectRepository.GetProjectAsync(JsonConvert.SerializeObject(jsonrequest));
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "SubProjectController",
                    sourcepagemethod = "GetProject",
                    message = ex.Message,
                    stacktrace = ex.StackTrace,
                    param = jsonrequest.ToString(),
                    errortype = "Controller",
                    checkedcomment = "",
                    checkedby = "",
                };

                dBInsert.FunTmsErrorLog(errorLog);
                returnResponse.ResponseMessage = "Something went wrong";
            }
            finally
            {
                // Always log request and response
                //request = "File Upload: " + file.FileName,
                RequestResponseLog requestResponseLog = new RequestResponseLog();
                requestResponseLog.request = JsonConvert.SerializeObject(jsonrequest);
                requestResponseLog.response = JsonConvert.SerializeObject(returnResponse);
                requestResponseLog.participantid = "";
                requestResponseLog.reqtype = "GetProject";
                requestResponseLog.reqdate = reqDate;
                requestResponseLog.rspdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                requestResponseLogRepository.LOG_DB_ApiRequestResponseLog(requestResponseLog);
            }

            return returnResponse;
        }

        [HttpPost("GetSubProjectList")]
        public async Task<object> GetSubProjectList(JObject jsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to add user." };
            try
            {
                returnResponse = subProjectRepository.GetSubProjectList(JsonConvert.SerializeObject(jsonRequest));
            }
            catch (Exception ex)
            {

                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "SubProjectController",
                    sourcepagemethod = "GetSubProjectList",
                    message = ex.Message,
                    stacktrace = ex.StackTrace,
                    param = jsonRequest.ToString(),
                    errortype = "Controller",
                    checkedcomment = "",
                    checkedby = "",
                };


                dBInsert.FunTmsErrorLog(errorLog);
                returnResponse.ResponseMessage = "Something went wrong";

            }
            finally
            {
                // Always log request and response
                RequestResponseLog requestResponseLog = new RequestResponseLog();
                requestResponseLog.request = JsonConvert.SerializeObject(jsonRequest);
                //requestResponseLog.response = Convert.ToString(resposne);
                requestResponseLog.response = JsonConvert.SerializeObject(returnResponse);
                requestResponseLog.participantid = "";
                requestResponseLog.reqtype = "";
                requestResponseLog.reqdate = reqDate;
                requestResponseLog.rspdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                requestResponseLogRepository.LOG_DB_ApiRequestResponseLog(requestResponseLog);
            }
            return returnResponse;
        }

        [HttpPost("GetViewTeamDataList")]
        public async Task<object> GetViewTeamDataList(JObject jsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to add user." };
            try
            {
                returnResponse = subProjectRepository.GetViewTeamDataList(JsonConvert.SerializeObject(jsonRequest));
            }
            catch (Exception ex)
            {

                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "SubProjectController",
                    sourcepagemethod = "GetViewTeamDataList",
                    message = ex.Message,
                    stacktrace = ex.StackTrace,
                    param = jsonRequest.ToString(),
                    errortype = "Controller",
                    checkedcomment = "",
                    checkedby = "",
                };
                dBInsert.FunTmsErrorLog(errorLog);
                returnResponse.ResponseMessage = "Something went wrong";

            }
            finally
            {
                // Always log request and response
                RequestResponseLog requestResponseLog = new RequestResponseLog();
                requestResponseLog.request = JsonConvert.SerializeObject(jsonRequest);
                requestResponseLog.response = JsonConvert.SerializeObject(returnResponse);
                requestResponseLog.participantid = "";
                requestResponseLog.reqtype = "Role";
                requestResponseLog.reqdate = reqDate;
                requestResponseLog.rspdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                requestResponseLogRepository.LOG_DB_ApiRequestResponseLog(requestResponseLog);
            }
            return returnResponse;
        }

        [HttpPost("GetSprint")]

        public async Task<object> GetSprint(JObject jsonrequest)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to process file." };

            try
            {

                // Process the JSON request
                returnResponse = subProjectRepository.GetSprintAsync(JsonConvert.SerializeObject(jsonrequest));
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "SubProjectController",
                    sourcepagemethod = "GetSprint",
                    message = ex.Message,
                    stacktrace = ex.StackTrace,
                    param = jsonrequest.ToString(),
                    errortype = "Controller",
                    checkedcomment = "",
                    checkedby = "",
                };

                dBInsert.FunTmsErrorLog(errorLog);
                returnResponse.ResponseMessage = "Something went wrong";
            }
            finally
            {
                // Always log request and response
                //request = "File Upload: " + file.FileName,
                RequestResponseLog requestResponseLog = new RequestResponseLog();
                requestResponseLog.request = JsonConvert.SerializeObject(jsonrequest);
                requestResponseLog.response = JsonConvert.SerializeObject(returnResponse);
                requestResponseLog.participantid = "";
                requestResponseLog.reqtype = "GetSprint";
                requestResponseLog.reqdate = reqDate;
                requestResponseLog.rspdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                requestResponseLogRepository.LOG_DB_ApiRequestResponseLog(requestResponseLog);
            }

            return returnResponse;
        }

        [HttpPost("GetPM")]

        public async Task<object> GetPM(JObject jsonrequest)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to process file." };

            try
            {

                // Process the JSON request
                returnResponse = subProjectRepository.GetPMAsync(JsonConvert.SerializeObject(jsonrequest));
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "SubProjectController",
                    sourcepagemethod = "GetPM",
                    message = ex.Message,
                    stacktrace = ex.StackTrace,
                    param = jsonrequest.ToString(),
                    errortype = "Controller",
                    checkedcomment = "",
                    checkedby = "",
                };

                dBInsert.FunTmsErrorLog(errorLog);
                returnResponse.ResponseMessage = "Something went wrong";
            }
            finally
            {
                // Always log request and response
                //request = "File Upload: " + file.FileName,
                RequestResponseLog requestResponseLog = new RequestResponseLog();
                requestResponseLog.request = JsonConvert.SerializeObject(jsonrequest);
                requestResponseLog.response = JsonConvert.SerializeObject(returnResponse);
                requestResponseLog.participantid = "";
                requestResponseLog.reqtype = "GetPM";
                requestResponseLog.reqdate = reqDate;
                requestResponseLog.rspdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                requestResponseLogRepository.LOG_DB_ApiRequestResponseLog(requestResponseLog);
            }

            return returnResponse;
        }



        [HttpPost("GetDH")]

        public async Task<object> GetDH(JObject jsonrequest)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to process file." };

            try
            {

                // Process the JSON request
                returnResponse = subProjectRepository.GetDH(JsonConvert.SerializeObject(jsonrequest));
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "SubProjectController",
                    sourcepagemethod = "GetDH",
                    message = ex.Message,
                    stacktrace = ex.StackTrace,
                    param = jsonrequest.ToString(),
                    errortype = "Controller",
                    checkedcomment = "",
                    checkedby = "",
                };

                dBInsert.FunTmsErrorLog(errorLog);
                returnResponse.ResponseMessage = "Something went wrong";
            }
            finally
            {
                // Always log request and response
                //request = "File Upload: " + file.FileName,
                RequestResponseLog requestResponseLog = new RequestResponseLog();
                requestResponseLog.request = JsonConvert.SerializeObject(jsonrequest);
                requestResponseLog.response = JsonConvert.SerializeObject(returnResponse);
                requestResponseLog.participantid = "";
                requestResponseLog.reqtype = "GetDH";
                requestResponseLog.reqdate = reqDate;
                requestResponseLog.rspdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                requestResponseLogRepository.LOG_DB_ApiRequestResponseLog(requestResponseLog);
            }

            return returnResponse;
        }




        [HttpPost("GetTL")]

        public async Task<object> GetTL(JObject jsonrequest)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to process file." };

            try
            {

                // Process the JSON request
                returnResponse = subProjectRepository.GetTL(JsonConvert.SerializeObject(jsonrequest));
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "SubProjectController",
                    sourcepagemethod = "GetTL",
                    message = ex.Message,
                    stacktrace = ex.StackTrace,
                    param = jsonrequest.ToString(),
                    errortype = "Controller",
                    checkedcomment = "",
                    checkedby = "",
                };

                dBInsert.FunTmsErrorLog(errorLog);
                returnResponse.ResponseMessage = "Something went wrong";
            }
            finally
            {
                // Always log request and response
                //request = "File Upload: " + file.FileName,
                RequestResponseLog requestResponseLog = new RequestResponseLog();
                requestResponseLog.request = JsonConvert.SerializeObject(jsonrequest);
                requestResponseLog.response = JsonConvert.SerializeObject(returnResponse);
                requestResponseLog.participantid = "";
                requestResponseLog.reqtype = "GetTL";
                requestResponseLog.reqdate = reqDate;
                requestResponseLog.rspdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                requestResponseLogRepository.LOG_DB_ApiRequestResponseLog(requestResponseLog);
            }

            return returnResponse;
        }

        [HttpPost("GetCR")]

        public async Task<object> GetCR(JObject jsonrequest)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to process file." };

            try
            {

                // Process the JSON request
                returnResponse = subProjectRepository.GetCR(JsonConvert.SerializeObject(jsonrequest));
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "SubProjectController",
                    sourcepagemethod = "GetCR",
                    message = ex.Message,
                    stacktrace = ex.StackTrace,
                    param = jsonrequest.ToString(),
                    errortype = "Controller",
                    checkedcomment = "",
                    checkedby = "",
                };

                dBInsert.FunTmsErrorLog(errorLog);
                returnResponse.ResponseMessage = "Something went wrong";
            }
            finally
            {
                // Always log request and response
                //request = "File Upload: " + file.FileName,
                RequestResponseLog requestResponseLog = new RequestResponseLog();
                requestResponseLog.request = JsonConvert.SerializeObject(jsonrequest);
                requestResponseLog.response = JsonConvert.SerializeObject(returnResponse);
                requestResponseLog.participantid = "";
                requestResponseLog.reqtype = "GetCR";
                requestResponseLog.reqdate = reqDate;
                requestResponseLog.rspdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                requestResponseLogRepository.LOG_DB_ApiRequestResponseLog(requestResponseLog);
            }

            return returnResponse;
        }
    }
}
