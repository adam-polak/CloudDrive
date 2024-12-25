using System.Data.Common;
using Microsoft.Data.SqlClient;

namespace CloudDrive.DataAccess.Lib;

public static class DbConnectionHandler
{
    public static DbConnection CreateDbConnection()
    {
        string? connectionString = Environment.GetEnvironmentVariable("ConnectionString");
        if(connectionString == null)
        {
            // Get connection string from json file
            return new SqlConnection("");
        } else return new SqlConnection(connectionString);
    }
}