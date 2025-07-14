using Dapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Data;
using System.Runtime.Intrinsics.Arm;
using WBS_API.Common;
using WBS_API.Infrastructure;
using WBS_API.Interface;
using WBS_API.Model;
using WBS_API.Infrastructure;
using static WBS_API.Infrastructure.DBConnectionFactory;
using Microsoft.AspNetCore.Connections;

namespace WBS_API.Repositories
{
    public class CommonRepository : ICommonRepository
    {
        DBConnectionFactory dBConnectionFactory = new DBConnectionFactory(DBConnectionFactory.DBConnType.PostgrSql);

        private readonly DBProcessorService dBProcessorService;
        private readonly IErrorLogRepository errorLogRepository;
        private readonly IConfiguration configuration;

        public CommonRepository(IConfiguration configuration,DBProcessorService dBProcessorService, IErrorLogRepository errorLogRepository)
        {
            this.configuration = configuration;
            this.dBProcessorService = dBProcessorService;
            this.errorLogRepository = errorLogRepository;
        }

        public ReturnResponse CommonAsync(string JsonRequest, String FunctionName,string connName)
        {
            ReturnResponse returnResponse = new ReturnResponse() { ResponseCode = "118", ResponseMessage = "Failed to process request." };
            try
            {
                var dp = new DynamicParameters();

                // Call the function using DB Processor
                // returnResponse = dBProcessorService.Process("jsonrequest", jsonRequest, "fun_insert_update_client_api");

                // Corrected syntax for output parameters
               // dp.Add("p_json_request", value: JsonConvert.SerializeObject(JsonRequest), dbType: DbType.String);
                dp.Add("p_json_request", value:  JsonRequest, dbType: DbType.String);
                dp.Add("ResponseCode", dbType: DbType.String, direction: ParameterDirection.Output);
                dp.Add("ResponseMessage", dbType: DbType.String, direction: ParameterDirection.Output);
                dp.Add("rsplist", dbType: DbType.String, direction: ParameterDirection.Output);

                string connectionStirng = configuration.GetSection($"ConnectionStrings:{connName}").Value;
                using IDbConnection con = dBConnectionFactory.GetDbConnection(connectionStirng);
                returnResponse = con.Query<ReturnResponse>(FunctionName, dp, commandType: CommandType.StoredProcedure).FirstOrDefault();

                con.Close();
            }
            catch (Exception ex)
            {
                var errorLog = new ErrorLog
                {
                    sourcepage = "CommonRepository",
                    sourcepagemethod = "CommonAsync",
                    message = ex.Message,
                    stacktrace = ex.StackTrace,
                    param = JsonRequest,
                    errortype = "Repository",
                    checkedcomment = "",
                    checkedby = ""
                };

                errorLogRepository.ApiErrorLog(errorLog);


                returnResponse.ResponseCode = "02";
                returnResponse.ResponseMessage = "Failed to Process Request ";

            }
            return returnResponse;
        }
        public ReturnResponse CommonRepositoryAsync(string jsonRequest, string FunctionName)
        {
            ReturnResponse returnResponse = new ReturnResponse() { ResponseCode = "118", ResponseMessage = "Failed to process request." };
            try
            {
                // Call the function using DB Processor
                //returnResponse = dBProcessorService.Process("jsonrequest", jsonRequest, "fun_get_project_details_api");
                returnResponse = dBProcessorService.Process("jsonrequest", jsonRequest, FunctionName);

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
                    sourcepage = "CommonRepository",
                    sourcepagemethod = "CommonRepositoryAsync",
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

        public ReturnResponse CommonRepositoryAsync1(string jsonRequest, string FunctionName)
        {
            ReturnResponse returnResponse = new ReturnResponse() { ResponseCode = "118", ResponseMessage = "Failed to process request." };
            try
            {
                // Call the function using DB Processor
                //returnResponse = dBProcessorService.Process("jsonrequest", jsonRequest, "fun_get_project_details_api");
                returnResponse = dBProcessorService.Process1("jsonrequest", jsonRequest, FunctionName);

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
                    sourcepage = "CommonRepository",
                    sourcepagemethod = "CommonRepositoryAsync",
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

        public ReturnResponse GetTimesheetList(string jsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse() { ResponseCode = "118", ResponseMessage = "Failed to process request." };
            try
            {
                returnResponse = dBProcessorService.Process("jsonrequest", jsonRequest, "fun_gettimesheetlist_api_v2");

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
                    sourcepage = "CommonRepository",
                    sourcepagemethod = "GetTimesheetList",
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

        public ReturnResponse GetNestedTimesheetList(string jsonRequest)
        {
            ReturnResponse returnResponse = new ReturnResponse() { ResponseCode = "118", ResponseMessage = "Failed to process request." };
            try
            {
                returnResponse = dBProcessorService.Process("jsonrequest", jsonRequest, "fun_getnestedtimesheetlist_api");

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
                    sourcepage = "CommonRepository",
                    sourcepagemethod = "GetTimesheetList",
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

    }
}
