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

namespace WBS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly DBInsert dBInsert;
        private readonly ICommonRepository commonRepository;
        private readonly IClientRepository clientRepository;
        private readonly IRequestResponseLogRepository requestResponseLogRepository;

        public ClientController(DBInsert dBInsert, ICommonRepository commonRepository,IClientRepository clientRepository, IRequestResponseLogRepository requestResponseLogRepository)
        {

            this.dBInsert = dBInsert;
            this.commonRepository = commonRepository;
            this.clientRepository = clientRepository;
            this.requestResponseLogRepository = requestResponseLogRepository;
        }
        string reqDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        [HttpPost("Client")]
       
        public async Task<object> Client(IFormFile file)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to process file." };
            try
            {
                // Validate file
                if (file == null || file.Length == 0)
                {
                    returnResponse.ResponseCode = "01";
                    returnResponse.ResponseMessage = "Invalid or empty file.";
                    return returnResponse;
                }

                // Convert CSV to JSON
                string jsonContent;
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    var csvData = await reader.ReadToEndAsync();
                    jsonContent = CsvHelper.ConvertCsvToJson(csvData); // ✅ Call correctly
                }

                // Process the JSON request
                returnResponse = clientRepository.ClientAsync(jsonContent);
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "ClientController",
                    sourcepagemethod = "Client",
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
                // Always log request and response
                //request = "File Upload: " + file.FileName,
                RequestResponseLog requestResponseLog = new RequestResponseLog();
                requestResponseLog.request = JsonConvert.SerializeObject(file.FileName);
                requestResponseLog.response = JsonConvert.SerializeObject(returnResponse);
                requestResponseLog.participantid = "";
                requestResponseLog.reqtype = "Client";
                requestResponseLog.reqdate = reqDate;
                requestResponseLog.rspdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                requestResponseLogRepository.LOG_DB_ApiRequestResponseLog(requestResponseLog);
            }

            return returnResponse;
        }


        [HttpPost("GetClientList")]

        public async Task<IActionResult> GetClientList(JObject JsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to process request." };
            try
            {
                String FunctionName = "fn_get_client";
                String connName = "DBConstr";

                returnResponse = commonRepository.CommonAsync(Convert.ToString(JsonRequest),FunctionName, connName);
                    }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "ClientController",
                    sourcepagemethod = "GetClientList",
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

        [HttpPost("DeleteSPOC")]

        public async Task<IActionResult> DeleteSPOC(JObject JsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to process request." };
            try
            {
                String FunctionName = "fn_delete_clientspoc";
                String connName = "DBConstr";

                returnResponse = commonRepository.CommonAsync(Convert.ToString(JsonRequest), FunctionName, connName);
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "ClientController",
                    sourcepagemethod = "GetClientList",
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


        [HttpPost("SaveClientList")]

        public async Task<IActionResult> SaveClientList(JObject JsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to process request." };
            try
            {
                String FunctionName = "fn_save_client";
                String connName = "DBConstr";

                returnResponse = commonRepository.CommonAsync(Convert.ToString(JsonRequest), FunctionName, connName);
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "ClientController",
                    sourcepagemethod = "GetClientList",
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

        [HttpPost("InsertSPOC")]

        public async Task<object> InsertSPOC(JObject jsonrequest)
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
                returnResponse = clientRepository.InsertSpocAsync(JsonConvert.SerializeObject(jsonrequest));
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "ClientController",
                    sourcepagemethod = "SPOC",
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
                requestResponseLog.reqtype = "SPOC";
                requestResponseLog.reqdate = reqDate;
                requestResponseLog.rspdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                requestResponseLogRepository.LOG_DB_ApiRequestResponseLog(requestResponseLog);
            }

            return returnResponse;
        }



        [HttpPost("FetchSPOCList")]

        public async Task<object> FetchSPOCList(JObject jsonrequest)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to process file." };

            try
            {
                // Process the JSON request
                returnResponse = clientRepository.FetchSpocAsync(JsonConvert.SerializeObject(jsonrequest));
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "ClientController",
                    sourcepagemethod = "FetchSPOC",
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
                requestResponseLog.reqtype = "FetchSPOC";
                requestResponseLog.reqdate = reqDate;
                requestResponseLog.rspdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                requestResponseLogRepository.LOG_DB_ApiRequestResponseLog(requestResponseLog);
            }

            return returnResponse;
        }

        //[HttpPost("FetchSPOCList")]

        //public async Task<object> FetchSPOCList(JObject jsonrequest)
        //{
        //    ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to process file." };

        //    try
        //    {
        //        // Process the JSON request
        //        returnResponse = commonRepository.CommonAsync(JsonConvert.SerializeObject(jsonrequest), "fun_get_clientspoc_list", "DBConstr");
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorLog errorLog = new ErrorLog
        //        {
        //            sourcepage = "ClientController",
        //            sourcepagemethod = "FetchSPOC",
        //            message = ex.Message,
        //            stacktrace = ex.StackTrace,
        //            param = jsonrequest.ToString(),
        //            errortype = "Controller",
        //            checkedcomment = "",
        //            checkedby = "",
        //        };

        //        dBInsert.FunTmsErrorLog(errorLog);
        //        returnResponse.ResponseMessage = "Something went wrong";
        //    }
        //    finally
        //    {
        //        // Always log request and response
        //        //request = "File Upload: " + file.FileName,
        //        RequestResponseLog requestResponseLog = new RequestResponseLog();
        //        requestResponseLog.request = JsonConvert.SerializeObject(jsonrequest);
        //        requestResponseLog.response = JsonConvert.SerializeObject(returnResponse);
        //        requestResponseLog.participantid = "";
        //        requestResponseLog.reqtype = "FetchSPOC";
        //        requestResponseLog.reqdate = reqDate;
        //        requestResponseLog.rspdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        //        requestResponseLogRepository.LOG_DB_ApiRequestResponseLog(requestResponseLog);
        //    }

        //    return returnResponse;
        //}

        [HttpPost("UpdateSPOC")]

        public async Task<object> UpdateSPOC(JObject jsonrequest)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to process file." };

            try
            {


                // Process the JSON request
                returnResponse = clientRepository.UpdateSpocAsync(JsonConvert.SerializeObject(jsonrequest));
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "ClientController",
                    sourcepagemethod = "UpdateSPOC",
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
                requestResponseLog.reqtype = "UpdateSPOC";
                requestResponseLog.reqdate = reqDate;
                requestResponseLog.rspdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                requestResponseLogRepository.LOG_DB_ApiRequestResponseLog(requestResponseLog);
            }

            return returnResponse;
        }


        [HttpPost("GetClientDropdown")]

        public async Task<IActionResult> GetClientDropdown(JObject JsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to process request." };
            try
            {
                String FunctionName = "fuc_getclientdropdown";
                String connName = "DBConstr";

                returnResponse = commonRepository.CommonAsync(Convert.ToString(JsonRequest), FunctionName, connName);
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "ClientController",
                    sourcepagemethod = "GetClientList",
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
    }
}
