using System.Data.Common;
using CloudDrive.DataAccess.Lib;
using CloudDrive.DataAccess.Models;
using Dapper;

namespace CloudDrive.DataAccess.Controllers;

public class FileDataController
{
    private DbConnection _connection;

    public FileDataController()
    {
        _connection = DbConnectionHandler.CreateDbConnection();
    }

    private void DoCommand(string sql, object[] parameters)
    {
        _connection.Open();
        _connection.Execute(sql, parameters);
        _connection.Close();
    }

    private List<T> DoQuery<T>(string sql, object parameters)
    {
        _connection.Open();
        List<T> list = _connection.Query<T>(sql, parameters).AsList();
        _connection.Close();
        return list;
    }

    public void CreateFile(int folderId, string fileName)
    {
        string sql = "INSERT INTO file_table"
                    + " (folderid, file_name)"
                    + " VALUES ( @folderid, @file_name );";
        object[] parameters = { new { folderid = folderId, file_name = fileName } };

       DoCommand(sql, parameters); 
    }

    public void DeleteFile(int folderId, int fileId)
    {
        string sql = "DELETE FROM file_table"
                    + " WHERE folderid = @folderid AND id = @id;";
        object[] parameters = { new { folderid = folderId, id = fileId } };

        DoCommand(sql, parameters);
    }

    public void DeleteFilesInFolder(int folderId)
    {
        string sql = "DELETE FROM file_table"
                    + " WHERE folderid = @folderid;";
        object[] parameters = { new { folderid = folderId } };

        DoCommand(sql, parameters);    
    }

    public void UpdateFile(int fileId, int folderId, string name)
    {
        string sql = "UPDATE file_table"
                    + " SET folderid = @folderid, file_name = @file_name"
                    + " WHERE id = @id;";
        object[] parameters = { new { id = fileId, folderid = folderId, file_name = name } };

        DoCommand(sql, parameters);
    }

    public bool ContainsFileName(int folderId, string name)
    {
        string sql = "SELECT * FROM file_table"
                    + " WHERE folderid = @folderid AND file_name = @file_name;";

        return DoQuery<FileModel>(sql, new { folderid = folderId, file_name = name } )
                .AsList()
                .Count != 0;
    }

    public FileModel? GetFile(int folderId, int fileId)
    {
        string sql = "SELECT * FROM file_table"
                    + " WHERE folderid = @folderid AND id = @id;";
        
        object parameters = new { folderid = folderId, id = fileId };

        return DoQuery<FileModel>(sql, parameters).FirstOrDefault();
    }

    public FileModel? GetFile(int folderId, string fileName)
    {
        string sql = "SELECT * FROM file_table"
                    + " WHERE folderid = @folderid AND file_name = @file_name;";
        object parameters = new { folderid = folderId, file_name = fileName };

        return DoQuery<FileModel>(sql, parameters).FirstOrDefault();
    }

    public List<FileModel> GetFilesInFolder(int folderId)
    {
        string sql = "SELECT * FROM file_table"
                    + " WHERE folderid = @folderid;";

        return DoQuery<FileModel>(sql, new { folderid = folderId });
    }
}
