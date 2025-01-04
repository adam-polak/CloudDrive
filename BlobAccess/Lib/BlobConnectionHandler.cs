using Azure.Storage.Blobs;
using CloudDrive.Lib;

namespace CloudDrive.BlobAccess.Lib;

public static class BlobConnectionHandler
{
    public static BlobContainerClient CreateBlobConnection()
    {
        string? connectionString = Environment.GetEnvironmentVariable("BlobConnectionString");
        if(connectionString == null) {
            return new BlobContainerClient(JsonInfo.GetJsonDevVariable("BlobConnectionString"), JsonInfo.GetJsonDevVariable("BlobContainerName"));
        } else {
            return new BlobContainerClient(connectionString, Environment.GetEnvironmentVariable("BlobContainerName"));
        }
    }
}