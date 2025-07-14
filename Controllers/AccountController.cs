using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using WBS_API.DAL;
using WBS_API.Model;
using WBS_API.Repositories;
using WBS_API.Interface;

namespace WBS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly DBInsert dBInsert;
        private readonly IRequestResponseLogRepository requestResponseLogRepository;

        public AccountController(DBInsert dBInsert,IRequestResponseLogRepository requestResponseLogRepository)
        {
            this.dBInsert = dBInsert;
            this.requestResponseLogRepository = requestResponseLogRepository;
        }
        string reqDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        [HttpPost("Login")]
        public async Task<object> Login(JObject jsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Unable to process" };
            try
            {
                if (jsonRequest == null || jsonRequest.ToString() == "{}")
                {
                    returnResponse.ResponseCode = "01";
                    returnResponse.ResponseMessage = "jsonRequest cannot be null or empty";
                    return returnResponse;
                }

                returnResponse = dBInsert.FunPreLogin(JsonConvert.SerializeObject(jsonRequest));

                
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "AccountController",
                    sourcepagemethod = "Login",
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
