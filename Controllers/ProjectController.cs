using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WBS_API.DAL;
using WBS_API.Interface;
using WBS_API.Model;
using WBS_API.Repositories;
using WBS_API.Utility;

namespace WBS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly DBInsert dBInsert;
        private readonly IProjectRepository projectRepository;
        private readonly IRequestResponseLogRepository requestResponseLogRepository;

        public ProjectController(DBInsert dBInsert, IProjectRepository projectRepository, IRequestResponseLogRepository requestResponseLogRepository)
        {

            this.dBInsert = dBInsert;
            this.projectRepository = projectRepository;
            this.requestResponseLogRepository = requestResponseLogRepository;
        }
        string reqDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        [HttpPost("Project")]

        public async Task<object> Project(IFormFile file)
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
                returnResponse = projectRepository.ProjectAsync(jsonContent);
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "ProjectController",
                    sourcepagemethod = "Project",
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
                requestResponseLog.reqtype = "project";
                requestResponseLog.reqdate = reqDate;
                requestResponseLog.rspdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                requestResponseLogRepository.LOG_DB_ApiRequestResponseLog(requestResponseLog);
            }

            return returnResponse;
        }


        [HttpPost("FetchProject")]

        public async Task<object> FetchProject(JObject jsonrequest)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to process file." };

            try
            {
               
                // Process the JSON request
                returnResponse = projectRepository.FetchProjectAsync(JsonConvert.SerializeObject(jsonrequest));
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "ProjectController",
                    sourcepagemethod = "Project",
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
                requestResponseLog.reqtype = "project";
                requestResponseLog.reqdate = reqDate;
                requestResponseLog.rspdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                requestResponseLogRepository.LOG_DB_ApiRequestResponseLog(requestResponseLog);
            }

            return returnResponse;
        }

        [HttpPost("GetClientNameList")]
        public async Task<object> GetClientNameList(JObject jsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to add user." };
            try
            {
                returnResponse = projectRepository.GetClientNameList(JsonConvert.SerializeObject(jsonRequest));
            }
            catch (Exception ex)
            {

                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "ProjectController",
                    sourcepagemethod = "GetClientNameList",
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
                requestResponseLog.reqtype = "";
                requestResponseLog.reqdate = reqDate;
                requestResponseLog.rspdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                requestResponseLogRepository.LOG_DB_ApiRequestResponseLog(requestResponseLog);
            }
            return returnResponse;
        }

        [HttpPost("GetProjectMasterDataList")]
        public async Task<object> GetProjectMasterDataList(JObject jsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to add user." };
            try
            {
                returnResponse = projectRepository.GetProjectMasterDataList(JsonConvert.SerializeObject(jsonRequest));
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

        [HttpPost("SaveProjectMasterData")]
        public async Task<object> SaveProjectMasterData(JObject jsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to add user." };
            try
            {
                returnResponse = projectRepository.SaveProjectMasterData(JsonConvert.SerializeObject(jsonRequest));
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

        [HttpPost("FunGetprojectListById")]
        public async Task<object> FunGetprojectListById(JObject jsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to add user." };
            try
            {
                returnResponse = projectRepository.FunGetprojectListById(JsonConvert.SerializeObject(jsonRequest));
            }
            catch (Exception ex)
            {

                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "SubProjectController",
                    sourcepagemethod = "FunGetprojectListById",
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

        [HttpPost("EditProjectMasterData")]
        public async Task<object> EditProjectMasterData(JObject jsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse { ResponseCode = "01", ResponseMessage = "Failed to add user." };
            try
            {
                returnResponse = projectRepository.EditProjectMasterData(JsonConvert.SerializeObject(jsonRequest));
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog
                {
                    sourcepage = "UserController",
                    sourcepagemethod = "EditProjectMasterData",
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
    }
}
