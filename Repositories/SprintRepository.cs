using WBS_API.Common;
using WBS_API.Interface;
using WBS_API.Model;

namespace WBS_API.Repositories
{
    public class SprintRepository : ISprintRepository
    {

        private readonly DBProcessorService dBProcessorService;
        private readonly IErrorLogRepository errorLogRepository;

        public SprintRepository(DBProcessorService dBProcessorService, IErrorLogRepository errorLogRepository)
        {
            this.dBProcessorService = dBProcessorService;
            this.errorLogRepository = errorLogRepository;
        }

        public ReturnResponse AddSprintAsync(string jsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse() { ResponseCode = "118", ResponseMessage = "Failed to process request." };
            try
            {
                // Call the function using DB Processor
                returnResponse = dBProcessorService.Process("jsonrequest", jsonRequest, "fun_insert_sprint_api");

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
                    sourcepage = "SprintRepository",
                    sourcepagemethod = "AddSprintAsync",
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


        public ReturnResponse FetchSprintAsync(string jsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse() { ResponseCode = "118", ResponseMessage = "Failed to process request." };
            try
            {
                // Call the function using DB Processor
                returnResponse = dBProcessorService.Process("jsonrequest", jsonRequest, "fun_get_sprint_details_api");

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
                    sourcepage = "SprintRepository",
                    sourcepagemethod = "FetchSprintAsync",
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
