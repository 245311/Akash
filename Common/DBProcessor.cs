using Dapper;
using System.Data;
using Microsoft.AspNetCore.Connections;
using WBS_API.Model;
using WBS_API.Infrastructure;

namespace WBS_API.Common
{
    public class DBProcessor
    {
        DBConnectionFactory dBConnectionFactory = new DBConnectionFactory(DBConnectionFactory.DBConnType.PostgrSql);
        private IConfiguration configuration;
        public DBProcessor(IConfiguration configuration)
        {
            this.configuration = configuration;
        }


        public string Execute(string DBParameterName, string JsonRequest, string FunctionName, out string ResposneCode, out string ResposneMessage, out string ResponseString, string connName = "DBConstr")
        {
            ResposneCode = "115";
            ResposneMessage = "Error while establish db connection";

            string JsonResponse = string.Empty;
            ResponseString = string.Empty;
            ReturnResponse returnResponse = new ReturnResponse();
            try
            {

                var dp = new DynamicParameters();
                dp.Add(DBParameterName, JsonRequest, DbType.String, ParameterDirection.Input);

                // Corrected syntax for output parameters
                dp.Add("rspcode", dbType: DbType.String, direction: ParameterDirection.Output);
                dp.Add("rspmsg", dbType: DbType.String, direction: ParameterDirection.Output);
                dp.Add("rspjson", dbType: DbType.String, direction: ParameterDirection.Output);
                dp.Add("rspstring", dbType: DbType.String, direction: ParameterDirection.Output);

                string connectionStirng = configuration.GetSection($"ConnectionStrings:{connName}").Value;
                using IDbConnection con = dBConnectionFactory.GetDbConnection(connectionStirng);

                returnResponse = con.Query<ReturnResponse>(FunctionName, dp, commandType: CommandType.StoredProcedure).FirstOrDefault();
                ResposneCode = dp.Get<string>("rspcode");
                ResposneMessage = dp.Get<string>("rspmsg");
                if (ResposneCode == "00")
                {
                    JsonResponse = dp.Get<string>("rspjson");
                    ResponseString = dp.Get<string>("rspstring");
                }
                con.Close();
            }
            catch (Exception ex)
            {
                ResposneCode = "116";
                ResposneMessage = "Failed to esatablish a database connection";

            }
            return JsonResponse;
        }
    }
}
