using System.Data.Common;
using CloudDrive.DataAccess.Lib;
using CloudDrive.DataAccess.Models;
using Dapper;

namespace CloudDrive.DataAccess.Controllers;

public class FolderDataController
{
    private DbConnection _connection;

    public FolderDataController()
    {
        _connection = DbConnectionHandler.CreateDbConnection();
    }

    public void CreateRootFolder(int userId)
    {
        CreateFolder(userId, 0, "root");
    }

    public void CreateFolder(int userId, int parentId, string folderName)
    {
        string sql = "INSERT INTO folder_table"
                    + " (ownerid, folder_name, parentid)"
                    + " VALUES ( @ownerid, @folder_name, @parentid );";

        object[] parameters = { new { ownerid = userId, folder_name = folderName, parentid = parentId } };
        
        _connection.Open();
        _connection.Execute(sql, parameters);
        _connection.Close();
    }

    public bool ContainsFolder(int parentId, string folderName)
    {
        string sql = "SELECT * FROM folder_table WHERE folder_name = @foldername AND parentid = @parentid;";

        _connection.Open();
        List<Folder> folders = _connection.Query<Folder>(sql, new { foldername = folderName, parentid = parentId }).ToList();
        _connection.Close();
        return folders.Count == 1;
    }

    public void DeleteFolder(int userId, int folderId)
    {
        string sql = "DELETE FROM folder_table"
                    + " WHERE ownerid = @ownerid AND id = @folderid;";
        
        object[] parameters = { new { ownerid = userId, folderid = folderId } };
        
        _connection.Open();
        _connection.Execute(sql, parameters);
        _connection.Close();
    }

    public void ChangeFolderName(int userId, int folderId, string folderName)
    {
        string sql = "UPDATE folder_table"
                    + " SET folder_name = @foldername"
                    + " WHERE ownerid = @ownerid AND parentid = @folderid;";
        
        object[] parameters = { new { foldername = folderName, ownerid = userId, parentid = folderId } };

        _connection.Open();
        _connection.Execute(sql, parameters);
        _connection.Close();
    }

    public Folder GetFolder(int userId, int folderId)
    {
        string sql = "SELECT * FROM folder_table"
                    + " WHERE ownerid = @ownerid AND id = @id";

        _connection.Open();
        Folder folder = _connection.Query<Folder>(sql, new { ownerid = userId, id = folderId }).First();
        _connection.Close();

        return folder;
    }

    public Folder? GetRootFolder(int userId)
    {
        string sql = "SELECT * FROM folder_table"
                    + " WHERE ownerid = @ownerid AND parentid = 0;";
        
        _connection.Open();
        List<Folder> list = _connection.Query<Folder>(sql, new { ownerid = userId }).AsList();
        _connection.Close();
        return list.FirstOrDefault();
    }

    public List<Folder> GetNestedFolders(int userId, int folderId)
    {
        string sql = "SELECT * FROM folder_table"
                    + " WHERE ownerid = @ownerId AND parentid = @parentId";
        
        _connection.Open();
        List<Folder> folders = _connection.Query<Folder>(sql, new { ownerId = userId, parentId = folderId } ).ToList();
        _connection.Close();

        return folders;
    }
 
}