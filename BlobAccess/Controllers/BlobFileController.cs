using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
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

    private string GetBlobName(FileModel file)
    {
        return $"{file.FolderId}_{file.Id}.json";
    }

    public async Task UploadFileAsync(FileModel file, string json)
    {
        string blobName = GetBlobName(file);

        BlobClient client = _blobClient.GetBlobClient(blobName);

        await client.UploadAsync(json);
    }

    public async Task UploadFileAsync(FileModel file, Stream stream)
    {
        string blobName = GetBlobName(file);

        BlobClient client = _blobClient.GetBlobClient(blobName);

        await client.UploadAsync(stream);
    }

    public async Task DeleteFile(FileModel file)
    {
        string blobName = GetBlobName(file);

        await _blobClient.DeleteBlobIfExistsAsync(blobName);
    }

    public async Task<string> GetContentsAsString(FileModel file)
    {
        string blobName = GetBlobName(file);
        
        BlobClient client = _blobClient.GetBlobClient(blobName);

        BlobDownloadResult downloadResult = await client.DownloadContentAsync();

        return downloadResult.ToString() ?? "";
    }
}