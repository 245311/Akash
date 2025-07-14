using WBS_API.Common;
using WBS_API.Interface;
using WBS_API.Model;

namespace WBS_API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DBProcessorService dBProcessorService;
        private readonly IErrorLogRepository errorLogRepository;

        public UserRepository(DBProcessorService dBProcessorService,IErrorLogRepository errorLogRepository)
        {
            this.dBProcessorService = dBProcessorService;
            this.errorLogRepository = errorLogRepository;   
        }

        public ReturnResponse AddUserAsync(string jsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse() { ResponseCode = "118", ResponseMessage = "Failed to process request." };
            try
            {
                // Call the function using DB Processor
                returnResponse = dBProcessorService.Process("jsonrequest", jsonRequest, "fun_add_user_api");

                return returnResponse ?? new ReturnResponse
                {
                    ResponseCode = "02",
                    ResponseMessage = "Unknown error occurred while Adding User"
                };
            }
            catch (Exception ex)
            {
                var errorLog = new ErrorLog
                {
                    sourcepage = "UserRepository",
                    sourcepagemethod = "AddUserAsync",
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

        public ReturnResponse GetUserAsync(string jsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse() { ResponseCode = "118", ResponseMessage = "Failed to process request." };
            try
            {
                // Call the function using DB Processor
                returnResponse = dBProcessorService.Process("jsonrequest", jsonRequest, "fun_get_all_users_api");

                return returnResponse ?? new ReturnResponse
                {
                    ResponseCode = "02",
                    ResponseMessage = "Unknown error occurred while fetching User"
                };
            }
            catch (Exception ex)
            {
                var errorLog = new ErrorLog
                {
                    sourcepage = "UserRepository",
                    sourcepagemethod = "GetUserAsync",
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


        public ReturnResponse GetUserIdAsync(string jsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse() { ResponseCode = "118", ResponseMessage = "Failed to process request." };
            try
            {
                // Call the function using DB Processor
                returnResponse = dBProcessorService.Process("jsonrequest", jsonRequest, "fun_get_user_by_id_api");

                return returnResponse ?? new ReturnResponse
                {
                    ResponseCode = "02",
                    ResponseMessage = "Unknown error occurred while fetching  User id"
                };
            }
            catch (Exception ex)    
            {
                var errorLog = new ErrorLog
                {
                    sourcepage = "UserRepository",
                    sourcepagemethod = "GetUserIDAsync",
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


        public ReturnResponse UpdateUserAsync(string jsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse() { ResponseCode = "118", ResponseMessage = "Failed to process request." };
            try
            {
                // Call the function using DB Processor
                returnResponse = dBProcessorService.Process("jsonrequest", jsonRequest, "fun_update_user_api");

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
                    sourcepagemethod = "UpdateUserAsync",
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
        public ReturnResponse DeleteUserAsync(string jsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse() { ResponseCode = "118", ResponseMessage = "Failed to process request." };
            try
            {
                // Call the function using DB Processor
                returnResponse = dBProcessorService.Process("jsonrequest", jsonRequest, "fun_deleteuser");

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
                    sourcepagemethod = "DeleteUserAsync",
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

        public ReturnResponse TeamAsync(string jsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse() { ResponseCode = "118", ResponseMessage = "Failed to process request." };
            try
            {
                // Call the function using DB Processor
                returnResponse = dBProcessorService.Process("jsonrequest", jsonRequest, "fun_get_all_teams_api");

                return returnResponse ?? new ReturnResponse
                {
                    ResponseCode = "02",
                    ResponseMessage = "Unknown error occurred while fetching team"
                };
            }
            catch (Exception ex)
            {
                var errorLog = new ErrorLog
                {
                    sourcepage = "UserRepository",
                    sourcepagemethod = "TeamAsync",
                    message = ex.Message,
                    stacktrace = ex.StackTrace,
                    param = jsonRequest,
                    errortype = "Repository",
                    checkedcomment = "",
                    checkedby = ""
                };

                errorLogRepository.ApiErrorLog(errorLog);


                returnResponse.ResponseCode = "02";
                returnResponse.ResponseMessage = "Failed to fetch Team: ";

            }
            return returnResponse;
        }

        public ReturnResponse RoleAsync(string jsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse() { ResponseCode = "118", ResponseMessage = "Failed to process request." };
            try
            {
                // Call the function using DB Processor
                returnResponse = dBProcessorService.Process("jsonrequest", jsonRequest, "fun_get_all_role_names_api");

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
                    sourcepage = "UserRepository",
                    sourcepagemethod = "RoleAsync",
                    message = ex.Message,
                    stacktrace = ex.StackTrace,
                    param = jsonRequest,
                    errortype = "Repository",
                    checkedcomment = "",
                    checkedby = ""
                };

                errorLogRepository.ApiErrorLog(errorLog);


                returnResponse.ResponseCode = "02";
                returnResponse.ResponseMessage = "Failed to fetch Team: ";

            }
            return returnResponse;
        }

        public ReturnResponse GetRoleList(string jsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse() { ResponseCode = "118", ResponseMessage = "Failed to process request." };
            try
            {
                returnResponse = dBProcessorService.Process("jsonrequest", jsonRequest, "fun_getrolelist_api");

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
                    sourcepage = "UserRepository",
                    sourcepagemethod = "RoleAsync",
                    message = ex.Message,
                    stacktrace = ex.StackTrace,
                    param = jsonRequest,
                    errortype = "Repository",
                    checkedcomment = "",
                    checkedby = ""
                };

                errorLogRepository.ApiErrorLog(errorLog);


                returnResponse.ResponseCode = "02";
                returnResponse.ResponseMessage = "Failed to fetch Team: ";

            }
            return returnResponse;
        }

        public ReturnResponse GetPermissionList(string jsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse() { ResponseCode = "118", ResponseMessage = "Failed to process request." };
            try
            {
                returnResponse = dBProcessorService.Process("jsonrequest", jsonRequest, "fun_getpermissionlist_api");

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
                    sourcepage = "UserRepository",
                    sourcepagemethod = "RoleAsync",
                    message = ex.Message,
                    stacktrace = ex.StackTrace,
                    param = jsonRequest,
                    errortype = "Repository",
                    checkedcomment = "",
                    checkedby = ""
                };

                errorLogRepository.ApiErrorLog(errorLog);


                returnResponse.ResponseCode = "02";
                returnResponse.ResponseMessage = "Failed to fetch Team: ";

            }
            return returnResponse;
        }

        public ReturnResponse UpdatePermission(string jsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse() { ResponseCode = "118", ResponseMessage = "Failed to process request." };
            try
            {
                returnResponse = dBProcessorService.Process("jsonrequest", jsonRequest, "fun_updatepermission_api");

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
                returnResponse.ResponseMessage = "Failed to Add User: ";

            }
            return returnResponse;
        }

        public ReturnResponse SkillSetAsync(string jsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse() { ResponseCode = "118", ResponseMessage = "Failed to process request." };
            try
            {
                // Call the function using DB Processor
                returnResponse = dBProcessorService.Process("jsonrequest", jsonRequest, "fun_get_all_skillsets_api");

                return returnResponse ?? new ReturnResponse
                {
                    ResponseCode = "02",
                    ResponseMessage = "Unknown error occurred while fetching skillset"
                };
            }       
            catch (Exception ex)
            {
                var errorLog = new ErrorLog
                {
                    sourcepage = "UserRepository",
                    sourcepagemethod = "SkillSetAsync",
                    message = ex.Message,
                    stacktrace = ex.StackTrace,
                    param = jsonRequest,
                    errortype = "Repository",
                    checkedcomment = "",
                    checkedby = ""
                };

                errorLogRepository.ApiErrorLog(errorLog);


                returnResponse.ResponseCode = "02";
                returnResponse.ResponseMessage = "Failed to fetch Team: ";

            }
            return returnResponse;
        }


        public ReturnResponse LocationAsync(string jsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse() { ResponseCode = "118", ResponseMessage = "Failed to process request." };
            try
            {
                // Call the function using DB Processor
                returnResponse = dBProcessorService.Process("jsonrequest", jsonRequest, "fun_get_all_locations_api");

                return returnResponse ?? new ReturnResponse
                {
                    ResponseCode = "02",
                    ResponseMessage = "Unknown error occurred while fetching location"
                };
            }
            catch (Exception ex)
            {
                var errorLog = new ErrorLog
                {
                    sourcepage = "UserRepository",
                    sourcepagemethod = "LocationAsync",
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
    }
}
