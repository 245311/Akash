using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using WBS_API.DAL;
using WBS_API.Interface;
using WBS_API.Model;
using WBS_API.Utility;
using WBS_API.Repositories;

namespace WBS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CRTaskController : Controller
    {
        private readonly DBInsert dBInsert;
        private readonly ICommonRepository commonRepository;
        private readonly IRequestResponseLogRepository requestResponseLogRepository;

        public CRTaskController(DBInsert dBInsert, ICommonRepository commonRepository, IRequestResponseLogRepository requestResponseLogRepository)
        {

            this.dBInsert = dBInsert;
            this.commonRepository = commonRepository;
            this.requestResponseLogRepository = requestResponseLogRepository;
        }
        string reqDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        [HttpPost("CRTask")]
        public async Task<object> CRTask([FromForm] int spid, [FromForm] IFormFile file, [FromForm] string createdBy=null)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to process file." };

            try
            {
                // Validate input
                if (file == null || file.Length == 0)
                {
                    returnResponse.ResponseCode = "01";
                    returnResponse.ResponseMessage = "Invalid or empty file.";
                    return returnResponse;
                }
                // Try to extract createdBy from form if it's null
                if (string.IsNullOrEmpty(createdBy))
                {
                    // Look for the parameter in different form fields
                    // This accounts for potential case sensitivity or formatting issues
                    foreach (var key in Request.Form.Keys)
                    {
                        if (key.Equals("createdBy", StringComparison.OrdinalIgnoreCase) ||
                            key.Equals("created_by", StringComparison.OrdinalIgnoreCase) ||
                            key.Equals("LoggedInUserId", StringComparison.OrdinalIgnoreCase))
                        {
                            createdBy = Request.Form[key];
                            break;
                        }
                    }

                    // If still null, check Request.Form directly for "createdBy"
                    if (string.IsNullOrEmpty(createdBy) && Request.Form.ContainsKey("createdBy"))
                    {
                        createdBy = Request.Form["createdBy"];
                    }

                    // If still null, try lowercase
                    if (string.IsNullOrEmpty(createdBy) && Request.Form.ContainsKey("createdby"))
                    {
                        createdBy = Request.Form["createdby"];
                    }
                }

                // If still null after all these attempts, return an error
                if (string.IsNullOrEmpty(createdBy))
                {
                    return new ReturnResponse
                    {
                        ResponseCode = "01",
                        ResponseMessage = "createdBy parameter is required but was not found in the request."
                    };
                }

                // Read CSV data
                string csvData;
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    csvData = await reader.ReadToEndAsync();
                }

                // Convert CSV to JSON string (ensure ConvertCsvToJson returns a JSON array as a string)
                string csvJsonString = CsvHelper.ConvertCsvToJson(csvData);

                // 🔹 Log JSON output to verify the structure
                Console.WriteLine($"CSV Converted JSON: {csvJsonString}");

                // ✅ Check if JSON is an array, if not, wrap it into an array
                if (!csvJsonString.TrimStart().StartsWith("["))
                {
                    csvJsonString = "[" + csvJsonString + "]";  // Wrap in an array if needed
                }

                // Deserialize the JSON string into List<object>
                List<object> csvList = JsonConvert.DeserializeObject<List<object>>(csvJsonString)
                                       ?? throw new InvalidCastException("Failed to convert CSV to List<object>");

                // Create JSON request
                var jsonRequest = new
                {
                    spid = spid,
                    csvfile = csvList,  // ✅ Guaranteed to be an array
                    createdBy = createdBy
                };

                // Serialize and send request
                string jsonRequestString = JsonConvert.SerializeObject(jsonRequest);
                // Process the JSON request
                returnResponse = commonRepository.CommonRepositoryAsync(jsonRequestString, "fun_process_CRtask_api");
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "CRTaskController",
                    sourcepagemethod = "CRTask",
                    message = ex.Message,
                    stacktrace = ex.StackTrace,
                    param = file.FileName,
                    errortype = "Controller",
                    checkedcomment = "",
                    checkedby = "",
                };

                dBInsert.FunTmsErrorLog(errorLog);
                returnResponse.ResponseMessage = "Something went wrong";
            }
            finally
            {
                // Log request and response
                RequestResponseLog requestResponseLog = new RequestResponseLog
                {
                    request = JsonConvert.SerializeObject(new { spid, fileName = file.FileName }),
                    response = JsonConvert.SerializeObject(returnResponse),
                    participantid = "",
                    reqtype = "CRTask",
                    reqdate = reqDate,
                    rspdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")
                };

                requestResponseLogRepository.LOG_DB_ApiRequestResponseLog(requestResponseLog);
            }

            return returnResponse;
        }

        [HttpPost("get_subproject")]

        public async Task<object> get_subproject(JObject jsonrequest)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to process file." };

            try
            {


                // Process the JSON request
                returnResponse = commonRepository.CommonRepositoryAsync(JsonConvert.SerializeObject(jsonrequest), "fun_get_subproject_details_fordropdown_api");
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "CRTaskController",
                    sourcepagemethod = "get_subproject",
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
                requestResponseLog.reqtype = "get_subproject";
                requestResponseLog.reqdate = reqDate;
                requestResponseLog.rspdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                requestResponseLogRepository.LOG_DB_ApiRequestResponseLog(requestResponseLog);
            }

            return returnResponse;
        }

        [HttpPost("get_subproject_description")]

        public async Task<object> get_subproject_description(JObject jsonrequest)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to process file." };

            try
            {


                // Process the JSON request
                returnResponse = commonRepository.CommonRepositoryAsync(JsonConvert.SerializeObject(jsonrequest), "fun_get_task_details_api");
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "CRTaskController",
                    sourcepagemethod = "get_subproject_description",
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
                requestResponseLog.reqtype = "get_subproject_description";
                requestResponseLog.reqdate = reqDate;
                requestResponseLog.rspdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                requestResponseLogRepository.LOG_DB_ApiRequestResponseLog(requestResponseLog);
            }

            return returnResponse;
        }


        [HttpPost("update_subproject_description")]

        public async Task<object> update_subproject_description(JObject jsonrequest)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to process file." };

            try
            {


                // Process the JSON request
                returnResponse = commonRepository.CommonRepositoryAsync(JsonConvert.SerializeObject(jsonrequest), "fun_update_task_details_api");
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "CRTaskController",
                    sourcepagemethod = "update_subproject_description",
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
                requestResponseLog.reqtype = "update_subproject_description";
                requestResponseLog.reqdate = reqDate;
                requestResponseLog.rspdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                requestResponseLogRepository.LOG_DB_ApiRequestResponseLog(requestResponseLog);
            }

            return returnResponse;
        }

        [HttpPost("get_subproject_description_by_id")]
        //not completeed yet
        public async Task<object> get_subproject_description_by_id(JObject jsonrequest)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to process file." };

            try
            {


                // Process the JSON request
                returnResponse = commonRepository.CommonRepositoryAsync(JsonConvert.SerializeObject(jsonrequest), "fun_get_task_details_by_spid_api");
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "CRTaskController",
                    sourcepagemethod = "get_subproject_description",
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
                requestResponseLog.reqtype = "get_subproject_description";
                requestResponseLog.reqdate = reqDate;
                requestResponseLog.rspdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                requestResponseLogRepository.LOG_DB_ApiRequestResponseLog(requestResponseLog);
            }

            return returnResponse;
        }
    }
}
