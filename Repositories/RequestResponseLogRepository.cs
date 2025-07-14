using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using WBS_API.Infrastructure;
using WBS_API.Interface;
using WBS_API.Model;

namespace WBS_API.Repositories
{
    public class RequestResponseLogRepository:IRequestResponseLogRepository
    {
        DBConnectionFactory dBConnectionFactory = new DBConnectionFactory(DBConnectionFactory.DBConnType.PostgrSql);
        private readonly IConfiguration _configuration;

        public RequestResponseLogRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public ReturnResponseDownloadAPI ApiRequestResponseLog(RequestResponseLog requestResponseLog)
        {
            ReturnResponseDownloadAPI returnStatus = new ReturnResponseDownloadAPI() { ResponseCode = "02", ResponseMessage = "Failed to add request response." };
            string connName = "DBConstr";
            try
            {

                var dp = new DynamicParameters();
                dp.Add("inrequest", value: requestResponseLog.request, dbType: DbType.String);
                dp.Add("inresponse", value: requestResponseLog.response, dbType: DbType.String);
                dp.Add("increated_by", value: requestResponseLog.serial_no, dbType: DbType.String);
                dp.Add("inparticipantid", value: requestResponseLog.participantid, dbType: DbType.String);
                dp.Add("inreqtype", value: requestResponseLog.reqtype, dbType: DbType.String);
                dp.Add("inreqdate", value: requestResponseLog.reqdate, dbType: DbType.String);
                dp.Add("inrspdate", value: requestResponseLog.rspdate, dbType: DbType.String);
                dp.Add("rspcode", dbType: DbType.String, direction: ParameterDirection.Output);
                dp.Add("rspmsg", dbType: DbType.String, direction: ParameterDirection.Output);

                string connectionStirng = _configuration.GetSection($"ConnectionStrings:{connName}").Value;
                using IDbConnection con = dBConnectionFactory.GetDbConnection(connectionStirng);

                returnStatus = con.Query<ReturnResponseDownloadAPI>("funsaverequest_responselog", dp, commandType: CommandType.StoredProcedure).FirstOrDefault();
                returnStatus.ResponseCode = dp.Get<string>("rspcode");
                returnStatus.ResponseMessage = dp.Get<string>("rspmsg");
                con.Close();
            }
            catch (Exception ex)
            {
                returnStatus.ResponseCode = "02"; returnStatus.ResponseMessage = "Failed to add request response.";
            }
            return returnStatus;
        }

        public ReturnResponseDownloadAPI LOG_DB_ApiRequestResponseLog(RequestResponseLog requestResponseLog)
        {
            ReturnResponseDownloadAPI returnStatus = new ReturnResponseDownloadAPI() { ResponseCode = "02", ResponseMessage = "Failed to add request response." };
            string connName = "DBConstr";
            try
            {
                int b = 1, m = 0, a = b / m;
                var dp = new DynamicParameters();
                //dp.Add(DBParameterName, JsonRequest, DbType.String, ParameterDirection.Input);
                dp.Add("inrequest", value: requestResponseLog.request, dbType: DbType.String);
                dp.Add("inresponse", value: requestResponseLog.response, dbType: DbType.String);
                dp.Add("increated_by", value: requestResponseLog.serial_no, dbType: DbType.String);
                dp.Add("inparticipantid", value: requestResponseLog.participantid, dbType: DbType.String);
                dp.Add("inreqtype", value: requestResponseLog.reqtype, dbType: DbType.String);
                dp.Add("inreqdate", value: requestResponseLog.reqdate, dbType: DbType.String);
                dp.Add("inrspdate", value: requestResponseLog.rspdate, dbType: DbType.String);
                dp.Add("rspcode", dbType: DbType.String, direction: ParameterDirection.Output);
                dp.Add("rspmsg", dbType: DbType.String, direction: ParameterDirection.Output);

                string connectionStirng = _configuration.GetSection($"ConnectionStrings:{connName}").Value;
                using IDbConnection con = dBConnectionFactory.GetDbConnection(connectionStirng);

                returnStatus = con.Query<ReturnResponseDownloadAPI>("funsaverequest_responselog", dp, commandType: CommandType.StoredProcedure).FirstOrDefault();
                returnStatus.ResponseCode = dp.Get<string>("rspcode");
                returnStatus.ResponseMessage = dp.Get<string>("rspmsg");
                con.Close();

                //using (IDbConnection con = new ConnectionFactory(_configuration).OpenConnection())
                //{
                //    con.Query("funsaverequest_responselog", dp, commandType: CommandType.StoredProcedure);
                //    con.Close();
                //    con?.Dispose();

                //}
            }
            catch (Exception ex)
            {
                returnStatus.ResponseCode = "02"; returnStatus.ResponseMessage = "Failed to add request response.";
            }
            return returnStatus;
        }
    }
}
