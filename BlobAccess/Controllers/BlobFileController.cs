using Azure.Storage.Blobs;
using CloudDrive.BlobAccess.Lib;
using CloudDrive.DataAccess.Models;

namespace CloudDrive.BlobAccess.Controllers;

public class BlobFileController
{
    private BlobContainerClient _blobClient;
    
    public BlobFileController()
    {
        _blobClient = BlobConnectionHandler.CreateBlobConnection();
    }

    public void UploadFile(FileModel file, string json)
    {
        // TODO
    }

    public void DeleteFile(FileModel file)
    {
        // TODO
    }

    public string GetFile(FileModel file)
    {
        // TODO
        return "";
    }
}