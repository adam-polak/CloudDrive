using System.Data.Common;
using CloudDrive.Lib;
using Microsoft.Data.SqlClient;

namespace CloudDrive.DataAccess.Lib;

public static class DbConnectionHandler
{
    public static DbConnection CreateDbConnection()
    {
        string? connectionString = Environment.GetEnvironmentVariable("ConnectionString");
        if(connectionString == null) {
            return new SqlConnection(JsonInfo.GetJsonDevVariable("DatabaseConnectionString"));
        } else {
            return new SqlConnection(connectionString);
        }
    }
}