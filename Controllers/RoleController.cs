using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WBS_API.DAL;
using WBS_API.Interface;
using WBS_API.Model;

namespace WBS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly DBInsert dBInsert;
        private readonly IUserRepository userRepository;
        private readonly IRequestResponseLogRepository requestResponseLogRepository;

        public RoleController(DBInsert dBInsert, IUserRepository userRepository, IRequestResponseLogRepository requestResponseLogRepository)
        {

            this.dBInsert = dBInsert;
            this.userRepository = userRepository;
            this.requestResponseLogRepository = requestResponseLogRepository;

        }

        string reqDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");


        [HttpPost("Role")]
        public async Task<object> Role(JObject jsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to fetch role." };


            try
            {

                returnResponse = userRepository.RoleAsync(JsonConvert.SerializeObject(jsonRequest));
            }
            catch (Exception ex)
            {

                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "UserController",
                    sourcepagemethod = "Role",
                    message = ex.Message,
                    stacktrace = ex.StackTrace,
                    param = jsonRequest.ToString(),
                    errortype = "Controller",
                    checkedcomment = "",
                    checkedby = "",
                    //user_id = Convert.ToString(loggedUserDetails.Appuserid)
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


        [HttpPost("AddRole")]
        public async Task<object> AddRole(JObject jsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to add role." };


            try
            {

                returnResponse = userRepository.RoleAsync(JsonConvert.SerializeObject(jsonRequest));
            }
            catch (Exception ex)
            {

                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "RoleController",
                    sourcepagemethod = "AddRole",
                    message = ex.Message,
                    stacktrace = ex.StackTrace,
                    param = jsonRequest.ToString(),
                    errortype = "Controller",
                    checkedcomment = "",
                    checkedby = "",
                    //user_id = Convert.ToString(loggedUserDetails.Appuserid)
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
        [HttpPost("GetRoleList")]
        public async Task<object> GetRoleList(JObject jsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to add user." };
            try
            {
                returnResponse = userRepository.GetRoleList(JsonConvert.SerializeObject(jsonRequest));
            }
            catch (Exception ex)
            {

                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "UserController",
                    sourcepagemethod = "GetRoleList",
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


    }
}
