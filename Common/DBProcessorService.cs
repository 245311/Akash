using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using WBS_API.DAL;
using WBS_API.Model;

namespace WBS_API.Common
{
    public class DBProcessorService
    {
        private readonly IConfiguration configuration;
        private readonly DBProcessor dBProcessor;
        //private readonly IErrorLogRepository errorLogRepository;
        private readonly DBInsert dBInsert;
        public DBProcessorService(IConfiguration configuration, DBProcessor dBProcessor)
        {
            this.configuration = configuration;
            this.dBProcessor = dBProcessor;
            //this.errorLogRepository = errorLogRepository;
            this.dBInsert = dBInsert;
        }



        public ReturnResponse Process(string DBParameterName, string JsonRequest, string FunctionName, string connName = "DBConstr")
        {
            ReturnResponse returnResponse = new ReturnResponse() { ResponseCode = "113", ResponseMessage = "Error while processing the request" };
            string JsonResposne = string.Empty;

            try
            {
                JsonResposne = dBProcessor.Execute(DBParameterName, JsonRequest, FunctionName, out string ResponseCode, out string ResponseMessage, out string RspString, connName);

                returnResponse.ResponseCode = ResponseCode;
                returnResponse.ResponseMessage = ResponseMessage;

                if (returnResponse.ResponseCode == "00")
                {
                    if (!string.IsNullOrEmpty(JsonResposne))
                    {
                        returnResponse.RspData = JsonConvert.DeserializeObject<JToken>(JsonResposne);
                    }
                }
            }
            catch (Exception ex)
            {
                //ERROR LOG
                ErrorLog errorLog = new ErrorLog();
                errorLog.sourcepage = "DBProcessorService";
                errorLog.sourcepagemethod = "Process";
                errorLog.message = ex.Message;
                errorLog.stacktrace = ex.StackTrace;
                errorLog.param = "";
                errorLog.errortype = "Controller";
                errorLog.checkedcomment = "";
                errorLog.checkedby = "";
                //errorLogRepository.ApiErrorLog(errorLog);
                dBInsert.FunTmsErrorLog(errorLog);
                returnResponse.ResponseMessage = "Something went wrong";
                returnResponse.ResponseCode = "114";

                ////Logger.Log(Convert.ToString(ex.Message) + ",ClassName : DBProcessorService | MethodName: Process()", "", Convert.ToInt32(Logtype.Error));
            }
            return returnResponse;
        }

        public ReturnResponse Process1(string DBParameterName, string JsonRequest, string FunctionName, string connName = "DBConstr")
        {
            ReturnResponse returnResponse = new ReturnResponse() { ResponseCode = "113", ResponseMessage = "Error while processing the request" };
            string JsonResposne = string.Empty;

            try
            {
                JsonResposne = dBProcessor.Execute(DBParameterName, JsonRequest, FunctionName, out string ResponseCode, out string ResponseMessage, out string RspString, connName);

                returnResponse.ResponseCode = ResponseCode;
                returnResponse.ResponseMessage = ResponseMessage;

                if (returnResponse.ResponseCode == "00")
                {
                    if (!string.IsNullOrEmpty(JsonResposne))
                    {
                        var varjson = JsonConvert.DeserializeObject<JObject>(JsonResposne);
                        returnResponse.RspData = varjson["taskdetails"];
                        returnResponse.RspJson = varjson["remarks"];
                    }
                }
            }
            catch (Exception ex)
            {
                //ERROR LOG
                ErrorLog errorLog = new ErrorLog();
                errorLog.sourcepage = "DBProcessorService";
                errorLog.sourcepagemethod = "Process";
                errorLog.message = ex.Message;
                errorLog.stacktrace = ex.StackTrace;
                errorLog.param = "";
                errorLog.errortype = "Controller";
                errorLog.checkedcomment = "";
                errorLog.checkedby = "";
                //errorLogRepository.ApiErrorLog(errorLog);
                dBInsert.FunTmsErrorLog(errorLog);
                returnResponse.ResponseMessage = "Something went wrong";
                returnResponse.ResponseCode = "114";

                ////Logger.Log(Convert.ToString(ex.Message) + ",ClassName : DBProcessorService | MethodName: Process()", "", Convert.ToInt32(Logtype.Error));
            }
            return returnResponse;
        }

    }
}
