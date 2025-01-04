using Azure.Storage.Blobs;
using CloudDrive.BlobAccess.Lib;

namespace CloudDrive.BlobAccess.Controllers;

public class BlobFileController
{
    private BlobContainerClient _blobClient;
    
    public BlobFileController()
    {
        _blobClient = BlobConnectionHandler.CreateBlobConnection();
    }
}