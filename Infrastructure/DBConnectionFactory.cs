using Npgsql;
using System.Data;

namespace WBS_API.Infrastructure
{
    public class DBConnectionFactory
    {
        public enum DBConnType
        {
            PostgrSql,
            Oracle,
            SqlServer,
            MySql
        }

        DBConnType connType;

        public DBConnectionFactory(DBConnType dBConnType)
        {
            connType = dBConnType;
        }

        public IDbConnection GetDbConnection(string connectionString)
        {
            IDbConnection conn = null;

            string conStr = Base64EncodeDecode.DecodeString(connectionString);
            //string conStr = Base64E.DecodeString(connectionString);
            try
            {

                switch (connType)
                {
                    case DBConnType.PostgrSql:
                        conn = new NpgsqlConnection(conStr);
                        conn.Open();
                        return conn;

                    case DBConnType.Oracle:
                        break;

                    case DBConnType.SqlServer:
                        break;

                    case DBConnType.MySql:
                        break;

                    default:
                        conn = new NpgsqlConnection(conStr);
                        conn.Open();
                        return conn;
                }
            }
            catch (Exception ex)
            {
                ////Logger.Log(Convert.ToString(ex.Message) + ",ClassName : DBConnectionFactory | MethodName: GetDbConnection()", "", Convert.ToInt32(Logtype.Error));
            }
            return conn;
        }
    }
}
