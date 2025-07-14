using WBS_API.Common;
using WBS_API.Interface;
using WBS_API.Model;

namespace WBS_API.Repositories
{
    public class ClientRepository: IClientRepository
    {
        private readonly DBProcessorService dBProcessorService;
        private readonly IErrorLogRepository errorLogRepository;

        public ClientRepository(DBProcessorService dBProcessorService, IErrorLogRepository errorLogRepository)
        {
            this.dBProcessorService = dBProcessorService;
            this.errorLogRepository = errorLogRepository;
        }

        public ReturnResponse ClientAsync(string jsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse() { ResponseCode = "118", ResponseMessage = "Failed to process request." };
            try
            {
                // Call the function using DB Processor
                returnResponse = dBProcessorService.Process("jsonrequest", jsonRequest, "fun_insert_update_client_api");

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
                    sourcepage = "ClientRepository",
                    sourcepagemethod = "ClientAsync",
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

        public ReturnResponse InsertSpocAsync(string jsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse() { ResponseCode = "118", ResponseMessage = "Failed to process request." };
            try
            {
                // Call the function using DB Processor
                returnResponse = dBProcessorService.Process("jsonrequest", jsonRequest, "fun_insert_client_spoc_api");

                return returnResponse ?? new ReturnResponse
                {
                    ResponseCode = "02",
                    ResponseMessage = "Unknown error occurred while Adding SPOC"
                };
            }
            catch (Exception ex)
            {
                var errorLog = new ErrorLog
                {
                    sourcepage = "ClientRepository",
                    sourcepagemethod = "SPOCAsync",
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


        public ReturnResponse FetchSpocAsync(string jsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse() { ResponseCode = "118", ResponseMessage = "Failed to process request." };
            try
            {
                // Call the function using DB Processor
                returnResponse = dBProcessorService.Process("jsonrequest", jsonRequest, "fun_get_clientspoc_details_api");

                return returnResponse ?? new ReturnResponse
                {
                    ResponseCode = "02",
                    ResponseMessage = "Unknown error occurred while Adding SPOC"
                };
            }
            catch (Exception ex)
            {
                var errorLog = new ErrorLog
                {
                    sourcepage = "ClientRepository",
                    sourcepagemethod = "FetchSPOCAsync",
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

        public ReturnResponse UpdateSpocAsync(string jsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse() { ResponseCode = "118", ResponseMessage = "Failed to process request." };
            try
            {
                // Call the function using DB Processor
                returnResponse = dBProcessorService.Process("jsonrequest", jsonRequest, "fun_update_clientspoc_details_api");

                return returnResponse ?? new ReturnResponse
                {
                    ResponseCode = "02",
                    ResponseMessage = "Unknown error occurred while Adding SPOC"
                };
            }
            catch (Exception ex)
            {
                var errorLog = new ErrorLog
                {
                    sourcepage = "ClientRepository",
                    sourcepagemethod = "UpdateSpocAsync",
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
