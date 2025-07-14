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
    public class ManageTaskController : ControllerBase
    {

        private readonly DBInsert dBInsert;
        private readonly ICommonRepository commonRepository;
        private readonly IRequestResponseLogRepository requestResponseLogRepository;

        public ManageTaskController(DBInsert dBInsert, ICommonRepository commonRepository, IRequestResponseLogRepository requestResponseLogRepository)
        {

            this.dBInsert = dBInsert;
            this.commonRepository = commonRepository;
            this.requestResponseLogRepository = requestResponseLogRepository;
        }
        string reqDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        [HttpPost("Get_ManageTask_details")]

        public async Task<object> Get_ManageTask_details(JObject jsonrequest)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to process file." };

            try
            {


                // Process the JSON request
                returnResponse = commonRepository.CommonRepositoryAsync(JsonConvert.SerializeObject(jsonrequest), "fun_get_task_details_by_id_api");
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "ManageTaskController",
                    sourcepagemethod = "Get_ManageTask_details",
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
                requestResponseLog.reqtype = "Get_ManageTask_details";
                requestResponseLog.reqdate = reqDate;
                requestResponseLog.rspdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                requestResponseLogRepository.LOG_DB_ApiRequestResponseLog(requestResponseLog);
            }

            return returnResponse;
        }

        [HttpPost("GetViewTeamManageTaskdetails")]

        public async Task<object> GetViewTeamManageTaskdetails(JObject jsonrequest)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to process file." };
            try
            {
                returnResponse = commonRepository.CommonRepositoryAsync1(JsonConvert.SerializeObject(jsonrequest), "fun_getviewteamtaskdetails");
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "ManageTaskController",
                    sourcepagemethod = "GetViewTeamManageTaskdetails",
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
                RequestResponseLog requestResponseLog = new RequestResponseLog();
                requestResponseLog.request = JsonConvert.SerializeObject(jsonrequest);
                requestResponseLog.response = JsonConvert.SerializeObject(returnResponse);
                requestResponseLog.participantid = "";
                requestResponseLog.reqtype = "Get_ManageTask_details";
                requestResponseLog.reqdate = reqDate;
                requestResponseLog.rspdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                requestResponseLogRepository.LOG_DB_ApiRequestResponseLog(requestResponseLog);
            }

            return returnResponse;
        }

        [HttpPost("GetTimesheetList")]
        public async Task<object> GetTimesheetList(JObject jsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to add user." };
            try
            {
                returnResponse = commonRepository.GetTimesheetList(JsonConvert.SerializeObject(jsonRequest));
            }
            catch (Exception ex)
            {

                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "ManageTaskController",
                    sourcepagemethod = "GetTimesheetList",
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
                requestResponseLog.reqtype = "Role";
                requestResponseLog.reqdate = reqDate;
                requestResponseLog.rspdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                requestResponseLogRepository.LOG_DB_ApiRequestResponseLog(requestResponseLog);
            }
            return returnResponse;
        }

        [HttpPost("GetNestedTimesheetList")]
        public async Task<object> GetNestedTimesheetList(JObject jsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to add user." };
            try
            {
                returnResponse = commonRepository.GetNestedTimesheetList(JsonConvert.SerializeObject(jsonRequest));
            }
            catch (Exception ex)
            {

                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "ManageTaskController",
                    sourcepagemethod = "GetNestedTimesheetList",
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


        [HttpPost("completetask")]
        public async Task<object> completetask(JObject jsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to add user." };
            try
            {
                returnResponse = commonRepository.CommonRepositoryAsync(JsonConvert.SerializeObject(jsonRequest), "fun_complete_by_taskid_api");

                // ✅ Send mail only if response is success
                if (returnResponse.ResponseCode == "00" && returnResponse.RspData is JObject rspDataObj)
                {
                    string toEmail = rspDataObj?["emailid"]?.ToString();
                    string documentPath = rspDataObj?["docpath"]?.ToString();
                    string username = rspDataObj?["username"]?.ToString();
                    string taskname = rspDataObj?["taskname"]?.ToString();

                    if (!string.IsNullOrWhiteSpace(toEmail) && !string.IsNullOrWhiteSpace(documentPath))
                    {
                        string subject = "Task Completed - QCC";
                        string body = $@"<p>Dear {username},</p><p>The task <strong>{taskname}</strong> has been successfully approved by the QC team. Please consider this email as the official sign-off for the task.For your reference, the test case document is attached.</p>";

                        // ✅ Send mail from separate service class
                        MailService mailService = new MailService();
                        mailService.SendQccSuccessEmail(toEmail, subject, body, documentPath);
                    }
                }
            }
            catch (Exception ex)
            {

                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "ManageTaskController",
                    sourcepagemethod = "completetask",
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
                RequestResponseLog requestResponseLog = new RequestResponseLog();
                requestResponseLog.request = JsonConvert.SerializeObject(jsonRequest);
                requestResponseLog.response = JsonConvert.SerializeObject(returnResponse);
                requestResponseLog.participantid = "";
                requestResponseLog.reqtype = "completetask";
                requestResponseLog.reqdate = reqDate;
                requestResponseLog.rspdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                requestResponseLogRepository.LOG_DB_ApiRequestResponseLog(requestResponseLog);
            }
            return returnResponse;
        }

        [HttpPost("get_team")]
        public async Task<object> get_team(JObject jsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to add user." };
            try
            {
                returnResponse = commonRepository.CommonRepositoryAsync(JsonConvert.SerializeObject(jsonRequest), "fun_get_team_members_by_userid");
            }
            catch (Exception ex)
            {

                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "ManageTaskController",
                    sourcepagemethod = "get_team",
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
                RequestResponseLog requestResponseLog = new RequestResponseLog();
                requestResponseLog.request = JsonConvert.SerializeObject(jsonRequest);
                requestResponseLog.response = JsonConvert.SerializeObject(returnResponse);
                requestResponseLog.participantid = "";
                requestResponseLog.reqtype = "get_team";
                requestResponseLog.reqdate = reqDate;
                requestResponseLog.rspdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                requestResponseLogRepository.LOG_DB_ApiRequestResponseLog(requestResponseLog);
            }
            return returnResponse;
        }

        [HttpPost("close_task")]
        public async Task<object> close_task(JObject jsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to add user." };
            try
            {
                returnResponse = commonRepository.CommonRepositoryAsync(JsonConvert.SerializeObject(jsonRequest), "fun_update_close_task");
            }
            catch (Exception ex)
            {

                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "ManageTaskController",
                    sourcepagemethod = "close_task",
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
                RequestResponseLog requestResponseLog = new RequestResponseLog();
                requestResponseLog.request = JsonConvert.SerializeObject(jsonRequest);
                requestResponseLog.response = JsonConvert.SerializeObject(returnResponse);
                requestResponseLog.participantid = "";
                requestResponseLog.reqtype = "close_task";
                requestResponseLog.reqdate = reqDate;
                requestResponseLog.rspdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                requestResponseLogRepository.LOG_DB_ApiRequestResponseLog(requestResponseLog);
            }
            return returnResponse;
        }

        [HttpPost("Reopen")]
        public async Task<object> Reopen(JObject jsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to add user." };
            try
            {
                returnResponse = commonRepository.CommonRepositoryAsync(JsonConvert.SerializeObject(jsonRequest), "fun_update_task_and_parent_status");

                // ✅ Send mail only if response is success
                if (returnResponse.ResponseCode == "00" && returnResponse.RspData is JObject rspDataObj)
                {
                    string toEmail = rspDataObj?["emailid"]?.ToString();
                    string documentPath = rspDataObj?["docpath"]?.ToString();
                    string username = rspDataObj?["username"]?.ToString();
                    string taskname = rspDataObj?["taskname"]?.ToString();

                    if (!string.IsNullOrWhiteSpace(toEmail) && !string.IsNullOrWhiteSpace(documentPath))
                    {
                        string subject = "Task Fail - QCC";
                        string body = $"<p>Dear {username},</p><p>The task <strong>{taskname}</strong> has been failed by QC Team. The task has been reopened and assigned to you for development/Issue resolving.  Please find the attached document for your reference.</p>";

                        // ✅ Send mail from separate service class
                        MailService mailService = new MailService();
                        mailService.SendQccSuccessEmail(toEmail, subject, body, documentPath);
                    }
                }
            }
            catch (Exception ex)
            {

                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "ManageTaskController",
                    sourcepagemethod = "Reopen",
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
                RequestResponseLog requestResponseLog = new RequestResponseLog();
                requestResponseLog.request = JsonConvert.SerializeObject(jsonRequest);
                requestResponseLog.response = JsonConvert.SerializeObject(returnResponse);
                requestResponseLog.participantid = "";
                requestResponseLog.reqtype = "Reopen";
                requestResponseLog.reqdate = reqDate;
                requestResponseLog.rspdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                requestResponseLogRepository.LOG_DB_ApiRequestResponseLog(requestResponseLog);
            }
            return returnResponse;
        }

        [HttpPost("Downloadfile")]
        public async Task<object> Downloadfile(JObject jsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to add user." };
            try
            {
                returnResponse = commonRepository.CommonRepositoryAsync(JsonConvert.SerializeObject(jsonRequest), "fun_getdownloaddetails_api");
            }
            catch (Exception ex)
            {

                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "ManageTaskController",
                    sourcepagemethod = "Downloadfile",
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
                RequestResponseLog requestResponseLog = new RequestResponseLog();
                requestResponseLog.request = JsonConvert.SerializeObject(jsonRequest);
                requestResponseLog.response = JsonConvert.SerializeObject(returnResponse);
                requestResponseLog.participantid = "";
                requestResponseLog.reqtype = "Downloadfile";
                requestResponseLog.reqdate = reqDate;
                requestResponseLog.rspdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                requestResponseLogRepository.LOG_DB_ApiRequestResponseLog(requestResponseLog);
            }
            return returnResponse;
        }
    }
}
