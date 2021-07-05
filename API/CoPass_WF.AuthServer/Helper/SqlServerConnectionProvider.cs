using Microsoft.Data.SqlClient;
using System.Data;

namespace CoPass_WF.AuthServer.Helper
{
    public interface ISqlServerConnectionProvider
    {
        public IDbConnection GetDbConnection();
    }

    public class SqlServerConnectionProvider: ISqlServerConnectionProvider
    {
        private readonly string _connectionString;

        public SqlServerConnectionProvider(string connectionString)
        {
            _connectionString = connectionString;
        }
 

        public IDbConnection GetDbConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
