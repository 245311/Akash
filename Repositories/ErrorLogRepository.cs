using Dapper;
using Microsoft.AspNetCore.Connections.Features;
using System.Data;
using WBS_API.Infrastructure;
using WBS_API.Interface;
using WBS_API.Model;

namespace WBS_API.Repositories
{
    public class ErrorLogRepository:IErrorLogRepository
    {
        DBConnectionFactory dBConnectionFactory = new DBConnectionFactory(DBConnectionFactory.DBConnType.PostgrSql);
        private readonly IConfiguration _configuration;

        public ErrorLogRepository()
        {
        }

        public ErrorLogRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public ReturnResponseDownloadAPI ApiErrorLog(ErrorLog errorLog)
        {
            string connName = "DBConstr";
            ReturnResponseDownloadAPI returnStatus = new ReturnResponseDownloadAPI() { ResponseCode = "02", ResponseMessage = "Failed to add error log." };
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
                dp.Add("inuserid", value: "", dbType: DbType.String);
                dp.Add("rspcode", dbType: DbType.String, direction: ParameterDirection.Output);
                dp.Add("rspmsg", dbType: DbType.String, direction: ParameterDirection.Output);
                string connectionStirng = _configuration.GetSection($"ConnectionStrings:{connName}").Value;
                using IDbConnection con = dBConnectionFactory.GetDbConnection(connectionStirng);
                returnStatus = con.Query<ReturnResponseDownloadAPI>("fun_wbs_error_log", dp, commandType: CommandType.StoredProcedure).FirstOrDefault();
                returnStatus.ResponseCode = dp.Get<string>("rspcode");
                returnStatus.ResponseMessage = dp.Get<string>("rspmsg");
                con.Close();
            }
            catch (Exception ex)
            {
                returnStatus.ResponseCode = "02"; returnStatus.ResponseMessage = "Failed to add error log.";
            }
            return returnStatus;
        }
    }
}
