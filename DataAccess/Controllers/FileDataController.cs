using System.Data.Common;
using CloudDrive.DataAccess.Lib;
using CloudDrive.DataAccess.Models;

namespace CloudDrive.DataAccess.Controllers;

public class FileDataController
{
    private DbConnection _connection;

    public FileDataController()
    {
        _connection = DbConnectionHandler.CreateDbConnection();
    }

    public void CreateFile(int folderId, string fileName)
    {
        // TOOD
    }

    public void DeleteFile(int folderId, int fileId)
    {
        // TODO
    }

    public void DeleteFilesInFolder(int folderId)
    {
        // TOOD
    }

    public void UpdateFile(int fileId, int folderId, string name)
    {
        // TODO
    }

    public FileModel GetFile(int folderId, int fileId)
    {
        // TODO

        return null;
    }

    public List<FileModel> GetFilesInFolder(int folderId)
    {
        // TODO
        return [];
    }
}
