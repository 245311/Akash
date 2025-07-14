using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using WBS_API.DAL;
using WBS_API.Interface;
using WBS_API.Model;
using WBS_API.Repositories;
using WBS_API.Utility;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Http.HttpResults;
using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Spreadsheet;

namespace WBS_API.Controllers
{
    [ApiController]

    [Route("api/[controller]")]
    public class MyTaskController : ControllerBase
    {
        private readonly DBInsert dBInsert;
        private readonly ICommonRepository commonRepository;
        private readonly IClientRepository clientRepository;
        private readonly IRequestResponseLogRepository requestResponseLogRepository;

        public MyTaskController(DBInsert dBInsert, ICommonRepository commonRepository, IClientRepository clientRepository, IRequestResponseLogRepository requestResponseLogRepository)
        {

            this.dBInsert = dBInsert;
            this.commonRepository = commonRepository;
            this.clientRepository = clientRepository;
            this.requestResponseLogRepository = requestResponseLogRepository;
        }
        string reqDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");




        [HttpPost("GetTaskList")]

        public async Task<IActionResult> GetTaskList(JObject JsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to process request." };
            try
            {
                String FunctionName = "fun_getmytasklist_v1";
                String connName = "DBConstr";

                returnResponse = commonRepository.CommonAsync(Convert.ToString(JsonRequest), FunctionName, connName);
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "MyTaskController",
                    sourcepagemethod = "GetTaskList",
                    message = ex.Message,
                    stacktrace = ex.StackTrace,
                    param = Convert.ToString(JsonRequest),
                    errortype = "Controller",
                    checkedcomment = "",
                    checkedby = "",
                };

                dBInsert.FunTmsErrorLog(errorLog);
                returnResponse.ResponseMessage = "Something went wrong";
            }


            return Ok(returnResponse);

        }


        [HttpPost("UpdateTaskProgress")]

        public async Task<IActionResult> UpdateTaskProgress(JObject JsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to process request." };
            try
            {
                //String FunctionName = "fun_updatetaskprogress_v2";
                String FunctionName = "fun_updatetaskprogress_v2";
                String connName = "DBConstr";

                returnResponse = commonRepository.CommonAsync(Convert.ToString(JsonRequest), FunctionName, connName);
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "MyTaskController",
                    sourcepagemethod = "UpdateTaskProgress",
                    message = ex.Message,
                    stacktrace = ex.StackTrace,
                    param = Convert.ToString(JsonRequest),
                    errortype = "Controller",
                    checkedcomment = "",
                    checkedby = "",
                };

                dBInsert.FunTmsErrorLog(errorLog);
                returnResponse.ResponseMessage = "Something went wrong";
            }


            return Ok(returnResponse);

        }

        [HttpPost("LogTime")]

        public async Task<IActionResult> LogTime(JObject JsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to process request." };
            try
            {
                String FunctionName = "fun_insertlogtime";
                String connName = "DBConstr";

                returnResponse = commonRepository.CommonAsync(Convert.ToString(JsonRequest), FunctionName, connName);
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "MyTaskController",
                    sourcepagemethod = "UpdateTaskProgress",
                    message = ex.Message,
                    stacktrace = ex.StackTrace,
                    param = Convert.ToString(JsonRequest),
                    errortype = "Controller",
                    checkedcomment = "",
                    checkedby = "",
                };

                dBInsert.FunTmsErrorLog(errorLog);
                returnResponse.ResponseMessage = "Something went wrong";
            }


            return Ok(returnResponse);

        }

        [HttpPost("GetTaskType")]
        public async Task<IActionResult> GetTaskType(JObject jsonrequest)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to process file." };
            try
            {
                String FunctionName = "fun_gettasktype";
                String connName = "DBConstr";
                // Process the JSON request
                returnResponse = commonRepository.CommonAsync(Convert.ToString(jsonrequest), FunctionName, connName);
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

            return Ok(returnResponse);
        }

        [HttpPost("GetSubProject")]
        public async Task<IActionResult> GetSubProjectOnClientId(JObject jsonrequest)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to process file." };
            try
            {
                String FunctionName = "fun_getsubprojectonClient";
                String connName = "DBConstr";
                // Process the JSON request
                returnResponse = commonRepository.CommonAsync(Convert.ToString(jsonrequest), FunctionName, connName);
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

            return Ok(returnResponse);
        }
    }
}
