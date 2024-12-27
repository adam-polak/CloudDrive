using System.Data.Common;
using CloudDrive.DataAccess.Lib;

namespace CloudDrive.DataAccess.Controllers;

public class FolderDataController
{
    private DbConnection _connection;

    public FolderDataController()
    {
        _connection = DbConnectionHandler.CreateDbConnection();
    }

    public void CreateFolder(int userId, int parentId, string folderName)
    {
        // TODO
    }

    public void DeleteFolder(int userId, int folderId)
    {
        // TODO
    }

    public void ChangeFolderName(int userId, int folderId, string folderName)
    {
        // TODO
    }
}