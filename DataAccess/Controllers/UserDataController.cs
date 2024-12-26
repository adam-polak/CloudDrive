using System.Data.Common;
using CloudDrive.DataAccess.Lib;

namespace CloudDrive.DataAccess.Controllers;

public class UserDataController
{
    private DbConnection _connection;

    public UserDataController()
    {
        _connection = DbConnectionHandler.CreateDbConnection();
    }

    public bool ContainsUsername(string username)
    {
        // TODO
        return false;
    }

    public bool CorrectLogin(string username, string password)
    {
        // TODO
        return false;
    }

    public bool CorrectLogin(int key)
    {
        _connection.Open();
        _connection.Close();
        return false;
    }

    public int CreateUser(string username, string password)
    {
        // TODO
            // return uid
        return -1;
    }

    public void DeleteUser(int uid)
    {
        // TODO
    }

    public void UpdatePassword(string password)
    {
        // TODO
    }
}