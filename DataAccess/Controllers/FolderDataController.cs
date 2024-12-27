using System.Data.Common;
using CloudDrive.DataAccess.Lib;
using Dapper;

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
        string sql = "INSERT INTO folder_table"
                    + " (ownerid, folder_name, parentid)"
                    + "VALUES ( @ownerid, @folder_name, @parentid );";

        object[] parameters = { new { ownerid = userId, folder_name = folderName, parentid = parentId } };
        
        _connection.Open();
        _connection.Execute(sql, parameters);
        _connection.Close();
    }

    public void DeleteFolder(int userId, int folderId)
    {
        string sql = "DELETE FROM folder_table"
                    + " WHERE ownerid = @ownerid AND folderid = @folderid;";
        
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

    public void DeleteAllContents(int userId, int folderId)
    {
        // TODO
    }
}