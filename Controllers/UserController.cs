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
    public class UserController : ControllerBase
    {
        private readonly DBInsert dBInsert;
        private readonly IUserRepository userRepository;
        private readonly IRequestResponseLogRepository requestResponseLogRepository;

        public UserController(DBInsert dBInsert, IUserRepository userRepository, IRequestResponseLogRepository requestResponseLogRepository)
        {

            this.dBInsert = dBInsert;
            this.userRepository = userRepository;
            this.requestResponseLogRepository = requestResponseLogRepository;

        }

        string reqDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");



        [HttpPost("AddUser")]
        public async Task<object> AddUser(JObject jsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to add user." };


            try
            {
                // Validate request payload
                if (jsonRequest == null || !jsonRequest.HasValues)
                {
                    returnResponse.ResponseCode = "01";
                    returnResponse.ResponseMessage = "Invalid request.";
                    return returnResponse;
                }
                returnResponse = userRepository.AddUserAsync(JsonConvert.SerializeObject(jsonRequest));
            }
            catch (Exception ex)
            {

                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "UserController",
                    sourcepagemethod = "AddUser",
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
                requestResponseLog.reqtype = "AddRole";
                requestResponseLog.reqdate = reqDate;
                requestResponseLog.rspdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                requestResponseLogRepository.LOG_DB_ApiRequestResponseLog(requestResponseLog);
            }

            return returnResponse;

        }


        [HttpPost("GetUser")]
        public async Task<object> GetUser(JObject jsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to add user." };
            try
            {
                returnResponse = userRepository.GetUserAsync(JsonConvert.SerializeObject(jsonRequest));
            }
            catch (Exception ex)
            {

                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "UserController",
                    sourcepagemethod = "GetUser",
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
                requestResponseLog.reqtype = "GetUser";
                requestResponseLog.reqdate = reqDate;
                requestResponseLog.rspdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                requestResponseLogRepository.LOG_DB_ApiRequestResponseLog(requestResponseLog);
            }


            return returnResponse;

        }


        [HttpPost("GetUserId")]
        public async Task<object> GetUserId(JObject jsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to add user." };


            try
            {


                returnResponse = userRepository.GetUserIdAsync(JsonConvert.SerializeObject(jsonRequest));
            }
            catch (Exception ex)
            {

                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "UserController",
                    sourcepagemethod = "GetUserId",
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
                requestResponseLog.reqtype = "GetUserId";
                requestResponseLog.reqdate = reqDate;
                requestResponseLog.rspdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                requestResponseLogRepository.LOG_DB_ApiRequestResponseLog(requestResponseLog);
            }
            return returnResponse;

        }


        [HttpPost("UpdateUser")]
        public async Task<object> UpdateUser(JObject jsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to add user." };


            try
            {
                returnResponse = userRepository.UpdateUserAsync(JsonConvert.SerializeObject(jsonRequest));
            }
            catch (Exception ex)
            {

                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "UserController",
                    sourcepagemethod = "UpdateUser",
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
                requestResponseLog.reqtype = "UpdateUser";
                requestResponseLog.reqdate = reqDate;
                requestResponseLog.rspdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                requestResponseLogRepository.LOG_DB_ApiRequestResponseLog(requestResponseLog);
            }
            return returnResponse;

        }

        [HttpPost("DeleteUser")]
        public async Task<object> DeleteUser(JObject jsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to delete user." };


            try
            {
                returnResponse = userRepository.DeleteUserAsync(JsonConvert.SerializeObject(jsonRequest));
            }
            catch (Exception ex)
            {

                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "UserController",
                    sourcepagemethod = "DeleteUser",
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
                requestResponseLog.reqtype = "UpdateUser";
                requestResponseLog.reqdate = reqDate;
                requestResponseLog.rspdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                requestResponseLogRepository.LOG_DB_ApiRequestResponseLog(requestResponseLog);
            }
            return returnResponse;

        }

        [HttpPost("Team")]
        public async Task<object> Team(JObject jsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to add user." };


            try
            {

                //JObject jsonRequest = new JObject(); // Empty JSON request, as no parameters are needed.
                returnResponse = userRepository.TeamAsync(JsonConvert.SerializeObject(jsonRequest));
            }
            catch (Exception ex)
            {

                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "UserController",
                    sourcepagemethod = "Team",
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
                requestResponseLog.reqtype = "Team";
                requestResponseLog.reqdate = reqDate;
                requestResponseLog.rspdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                requestResponseLogRepository.LOG_DB_ApiRequestResponseLog(requestResponseLog);
            }
            return returnResponse;
        }

        [HttpPost("Role")]
        public async Task<object> Role(JObject jsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to add user." };


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

        [HttpPost("GetPermissionList")]
        public async Task<object> GetPermissionList(JObject jsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to add user." };
            try
            {
                returnResponse = userRepository.GetPermissionList(JsonConvert.SerializeObject(jsonRequest));
            }
            catch (Exception ex)
            {

                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "UserController",
                    sourcepagemethod = "GetPermissionList",
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

        [HttpPost("UpdatePermission")]
        public async Task<object> UpdatePermission(JObject jsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to add user." };
            try
            {
                returnResponse = userRepository.UpdatePermission(JsonConvert.SerializeObject(jsonRequest));
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "UserController",
                    sourcepagemethod = "UpdateUser",
                    message = ex.Message,
                    stacktrace = ex.StackTrace,
                    param = jsonRequest.ToString(),
                    errortype = "Controller",
                    checkedcomment = "",
                    checkedby = "",
                };
                dBInsert.FunTmsErrorLog(errorLog);
            }
            return returnResponse;
        }

        [HttpPost("SkillSet")]
        public async Task<object> SkillSet(JObject jsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to add user." };


            try
            {

                //JObject jsonRequest = new JObject(); // Empty JSON request, as no parameters are needed.
                returnResponse = userRepository.SkillSetAsync(JsonConvert.SerializeObject(jsonRequest));
            }
            catch (Exception ex)
            {

                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "UserController",
                    sourcepagemethod = "SkillSet",
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
                requestResponseLog.reqtype = "SkillSet";
                requestResponseLog.reqdate = reqDate;
                requestResponseLog.rspdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                requestResponseLogRepository.LOG_DB_ApiRequestResponseLog(requestResponseLog);
            }
            return returnResponse;
        }

        [HttpPost("Location")]
        public async Task<object> Location(JObject jsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to add user." };


            try
            {


                returnResponse = userRepository.LocationAsync(JsonConvert.SerializeObject(jsonRequest));
            }
            catch (Exception ex)
            {

                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "UserController",
                    sourcepagemethod = "Location",
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
                requestResponseLog.reqtype = "Location";
                requestResponseLog.reqdate = reqDate;
                requestResponseLog.rspdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                requestResponseLogRepository.LOG_DB_ApiRequestResponseLog(requestResponseLog);
            }
            return returnResponse;

        }

    }
}
