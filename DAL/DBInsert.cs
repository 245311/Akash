using Dapper;
using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.Configuration;
using System.Data;
using WBS_API.Common;
using WBS_API.Infrastructure;
using WBS_API.Model;

namespace WBS_API.DAL
{
    public class DBInsert
    {
        DBConnectionFactory dBConnectionFactory = new DBConnectionFactory(DBConnectionFactory.DBConnType.PostgrSql);
        private readonly IConfiguration _configuration;
        private readonly DBProcessorService dBProcessorService;


        public DBInsert(IConfiguration configuration, DBProcessorService dBProcessorService)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.dBProcessorService = dBProcessorService;

        }

        public ReturnResponse FunTmsErrorLog(ErrorLog errorLog, string connName = "DBConstr")
        {
            ReturnResponse returnStatus = new ReturnResponse() { ResponseCode = "05", ResponseMessage = "Something went wrong" };
            try
            {
                var dp = new DynamicParameters();
                dp.Add("insourcepage", value: errorLog.sourcepage, dbType: DbType.String);
                dp.Add("insourcepagemethod", value: errorLog.sourcepagemethod, dbType: DbType.String);
                dp.Add("inmessage", value: errorLog.message, dbType: DbType.String);
                dp.Add("instacktrace", value: errorLog.stacktrace, dbType: DbType.String);
                dp.Add("inparams", value: errorLog.param, dbType: DbType.String);
                dp.Add("inerrortype", value: errorLog.errortype, dbType: DbType.String);
                dp.Add("incheckedcomment", value: errorLog.checkedcomment, dbType: DbType.String);
                dp.Add("incheckedby", value: errorLog.checkedby, dbType: DbType.String);
                dp.Add("inuserid", value: "0", dbType: DbType.String);
                dp.Add("rspcode", dbType: DbType.String, direction: ParameterDirection.Output);
                dp.Add("rspmsg", dbType: DbType.String, direction: ParameterDirection.Output);
                //using IDbConnection con = new ConnectionFactory(_configuration).OpenConnection();
                string connectionStirng = _configuration.GetSection($"ConnectionStrings:{connName}").Value;
                using IDbConnection con = dBConnectionFactory.GetDbConnection(connectionStirng);


                returnStatus = con.Query<ReturnResponse>("fun_wbs_error_log", dp, commandType: CommandType.StoredProcedure).FirstOrDefault();

                returnStatus.ResponseCode = dp.Get<string>("rspcode");
                returnStatus.ResponseMessage = dp.Get<string>("rspmsg");
                con.Close();
            }
            catch (Exception ex)
            {

                returnStatus.ResponseCode = "06"; returnStatus.ResponseMessage = "Failed to add Error Log.";
            }
            return returnStatus;
        }

        public ReturnResponse FunPreLogin(string jsonrequest)
        {
            ReturnResponse returnStatus = new ReturnResponse() { ResponseCode = "05", ResponseMessage = "Something went wrong" };
            try
            {

                returnStatus = dBProcessorService.Process("jsonrequest", jsonrequest, "fun_login_api_v1");
            }
            catch (Exception ex)
            {
                ErrorLog errorLog = new ErrorLog();
                errorLog.sourcepage = "AccountRepository";
                errorLog.sourcepagemethod = "FunPreLogin";
                errorLog.message = ex.Message;
                errorLog.stacktrace = ex.StackTrace;
                errorLog.param = "inusername";
                errorLog.errortype = "Repository";
                errorLog.checkedcomment = "";
                errorLog.checkedby = "";
                FunTmsErrorLog(errorLog);
                returnStatus.ResponseCode = "06"; returnStatus.ResponseMessage = "Something went wrong";
            }
            return returnStatus;
        }

    }
}
