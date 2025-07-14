using WBS_API.Common;
using WBS_API.Interface;
using WBS_API.Model;

namespace WBS_API.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DBProcessorService dBProcessorService;
        private readonly IErrorLogRepository errorLogRepository;

        public ProjectRepository(DBProcessorService dBProcessorService, IErrorLogRepository errorLogRepository)
        {
            this.dBProcessorService = dBProcessorService;
            this.errorLogRepository = errorLogRepository;
        }

        public ReturnResponse ProjectAsync(string jsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse() { ResponseCode = "118", ResponseMessage = "Failed to process request." };
            try
            {
                // Call the function using DB Processor
                returnResponse = dBProcessorService.Process("jsonrequest", jsonRequest, "fun_insert_update_project_api");

                return returnResponse ?? new ReturnResponse
                {
                    ResponseCode = "02",
                    ResponseMessage = "Unknown error occurred while Adding client"
                };
            }
            catch (Exception ex)
            {
                var errorLog = new ErrorLog
                {
                    sourcepage = "ProjectRepository",
                    sourcepagemethod = "ProjectAsync",
                    message = ex.Message,
                    stacktrace = ex.StackTrace,
                    param = jsonRequest,
                    errortype = "Repository",
                    checkedcomment = "",
                    checkedby = ""
                };

                errorLogRepository.ApiErrorLog(errorLog);


                returnResponse.ResponseCode = "02";
                returnResponse.ResponseMessage = "Failed to Add User: ";

            }
            return returnResponse;
        }

        public ReturnResponse FetchProjectAsync(string jsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse() { ResponseCode = "118", ResponseMessage = "Failed to process request." };
            try
            {
                // Call the function using DB Processor
                returnResponse = dBProcessorService.Process("jsonrequest", jsonRequest, "fun_get_project_details_api");

                return returnResponse ?? new ReturnResponse
                {
                    ResponseCode = "02",
                    ResponseMessage = "Unknown error occurred while Adding client"
                };
            }
            catch (Exception ex)
            {
                var errorLog = new ErrorLog
                {
                    sourcepage = "ProjectRepository",
                    sourcepagemethod = "FetchProjectAsync",
                    message = ex.Message,
                    stacktrace = ex.StackTrace,
                    param = jsonRequest,
                    errortype = "Repository",
                    checkedcomment = "",
                    checkedby = ""
                };

                errorLogRepository.ApiErrorLog(errorLog);


                returnResponse.ResponseCode = "02";
                returnResponse.ResponseMessage = "Failed to Add User: ";

            }
            return returnResponse;
        }

        public ReturnResponse GetClientNameList(string jsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse() { ResponseCode = "118", ResponseMessage = "Failed to process request." };
            try
            {
                returnResponse = dBProcessorService.Process("jsonrequest", jsonRequest, "fun_getclientnamelist_api");

                return returnResponse ?? new ReturnResponse
                {
                    ResponseCode = "02",
                    ResponseMessage = "Unknown error occurred while fetching role"
                };
            }
            catch (Exception ex)
            {
                var errorLog = new ErrorLog
                {
                    sourcepage = "ProjectRepository",
                    sourcepagemethod = "GetClientNameList",
                    message = ex.Message,
                    stacktrace = ex.StackTrace,
                    param = jsonRequest,
                    errortype = "Repository",
                    checkedcomment = "",
                    checkedby = ""
                };

                errorLogRepository.ApiErrorLog(errorLog);


                returnResponse.ResponseCode = "02";
                returnResponse.ResponseMessage = "Failed to fetch client name: ";

            }
            return returnResponse;
        }

        public ReturnResponse GetProjectMasterDataList(string jsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse() { ResponseCode = "118", ResponseMessage = "Failed to process request." };
            try
            {
                returnResponse = dBProcessorService.Process("jsonrequest", jsonRequest, "fun_getprojectmasterdatalist_api");

                return returnResponse ?? new ReturnResponse
                {
                    ResponseCode = "02",
                    ResponseMessage = "Unknown error occurred while fetching role"
                };
            }
            catch (Exception ex)
            {
                var errorLog = new ErrorLog
                {
                    sourcepage = "ProjectRepository",
                    sourcepagemethod = "GetClientNameList",
                    message = ex.Message,
                    stacktrace = ex.StackTrace,
                    param = jsonRequest,
                    errortype = "Repository",
                    checkedcomment = "",
                    checkedby = ""
                };

                errorLogRepository.ApiErrorLog(errorLog);


                returnResponse.ResponseCode = "02";
                returnResponse.ResponseMessage = "Failed to fetch client name: ";

            }
            return returnResponse;
        }

        public ReturnResponse SaveProjectMasterData(string jsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse() { ResponseCode = "118", ResponseMessage = "Failed to process request." };
            try
            {
                returnResponse = dBProcessorService.Process("jsonrequest", jsonRequest, "fun_saveprojectmasterdata_api");

                return returnResponse ?? new ReturnResponse
                {
                    ResponseCode = "02",
                    ResponseMessage = "Unknown error occurred while updating User"
                };
            }
            catch (Exception ex)
            {
                var errorLog = new ErrorLog
                {
                    sourcepage = "UserRepository",
                    sourcepagemethod = "UpdatePermission",
                    message = ex.Message,
                    stacktrace = ex.StackTrace,
                    param = jsonRequest,
                    errortype = "Repository",
                    checkedcomment = "",
                    checkedby = ""
                };

                errorLogRepository.ApiErrorLog(errorLog);


                returnResponse.ResponseCode = "02";
                returnResponse.ResponseMessage = "Failed to Add project: ";

            }
            return returnResponse;
        }

        public ReturnResponse FunGetprojectListById(string jsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse() { ResponseCode = "118", ResponseMessage = "Failed to process request." };
            try
            {
                returnResponse = dBProcessorService.Process("jsonrequest", jsonRequest, "fun_getprojectmasterlistbyid_api");

                return returnResponse ?? new ReturnResponse
                {
                    ResponseCode = "02",
                    ResponseMessage = "Unknown error occurred while fetching role"
                };
            }
            catch (Exception ex)
            {
                var errorLog = new ErrorLog
                {
                    sourcepage = "ProjectRepository",
                    sourcepagemethod = "GetClientNameList",
                    message = ex.Message,
                    stacktrace = ex.StackTrace,
                    param = jsonRequest,
                    errortype = "Repository",
                    checkedcomment = "",
                    checkedby = ""
                };

                errorLogRepository.ApiErrorLog(errorLog);


                returnResponse.ResponseCode = "02";
                returnResponse.ResponseMessage = "Failed to fetch client name: ";

            }
            return returnResponse;
        }

        public ReturnResponse EditProjectMasterData(string jsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse() { ResponseCode = "118", ResponseMessage = "Failed to process request." };
            try
            {
                returnResponse = dBProcessorService.Process("jsonrequest", jsonRequest, "fun_editprojectmasterdata_api");

                return returnResponse ?? new ReturnResponse
                {
                    ResponseCode = "02",
                    ResponseMessage = "Unknown error occurred while updating User"
                };
            }
            catch (Exception ex)
            {
                var errorLog = new ErrorLog
                {
                    sourcepage = "UserRepository",
                    sourcepagemethod = "EditProjectMasterData",
                    message = ex.Message,
                    stacktrace = ex.StackTrace,
                    param = jsonRequest,
                    errortype = "Repository",
                    checkedcomment = "",
                    checkedby = ""
                };

                errorLogRepository.ApiErrorLog(errorLog);


                returnResponse.ResponseCode = "02";
                returnResponse.ResponseMessage = "Failed to Add project: ";

            }
            return returnResponse;
        }
    }
}
