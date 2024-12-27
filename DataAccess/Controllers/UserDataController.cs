using System.Data.Common;
using System.Security.Cryptography;
using CloudDrive.DataAccess.Lib;
using CloudDrive.DataAccess.Models;
using Dapper;

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
        string sql = "SELECT * FROM user_table WHERE username = @username;";

        _connection.Open();
        List<User> users = _connection.Query<User>(sql, new { username = username }).ToList();
        _connection.Close();

        return users.Count() == 1;
    }

    public bool CorrectLogin(string username, string password)
    {
        string sql = "SELECT * FROM user_table WHERE username = @username AND password = @password;";

        _connection.Open();
        List<User> users = _connection.Query<User>(sql, new { username = username, password = password }).ToList();
        _connection.Close();

        return users.Count() == 1;
    }

    public bool ContainsLoginKey(string loginKey)
    {
        string sql = "SELECT * FROM user_table WHERE loginkey = @loginKey;";

        _connection.Open();
        List<User> users = _connection.Query<User>(sql, new { loginKey = loginKey }).ToList();
        _connection.Close();

        return users.Count() == 1;
    }

    public void CreateUser(string username, string password)
    {
        // Setup SQL string
        string sql = "INSERT INTO user_table (username, password, loginkey) VALUES (@username, @password, @loginkey);";
        object[] parameters = { new { username = username, password = password, loginkey = LoginKey.Create() }};
        
        // Open connection, and execute command
        _connection.Open();
        _connection.Execute(sql, parameters);
        _connection.Close();
    }

    public void UpdateLoginKey(string username, string loginKey)
    {
        string sql = "UPDATE user_table SET loginKey = @loginKey WHERE username = @username;";
        object[] parameters = { new { username = username, loginKey = loginKey } };

        _connection.Open();
        _connection.Execute(sql, parameters);
        _connection.Close();
    }

    public string? LoginToUser(string username, string password)
    {
        if(!CorrectLogin(username, password)) return null;
        
        string loginKey = LoginKey.Create();
        while(ContainsLoginKey(loginKey))
        {
            loginKey = LoginKey.Create();
        }

        UpdateLoginKey(username, loginKey);

        return loginKey;
    }

    public void DeleteUser(int id)
    {
        string sql = "DELETE FROM user_table WHERE id = @id;";

        _connection.Open();
        _connection.Execute(sql, new { id = id });
        _connection.Close();
    }

    public User GetUser(string loginKey)
    {
        string sql = "SELECT * FROM user_table WHERE loginkey = @loginkey;";

        _connection.Open();
        User user = _connection.Query<User>(sql, new { loginkey = loginKey }).First();
        _connection.Close();
        return user;
    }

    public int GetUserId(string username, string password)
    {
        string sql = "SELECT * FROM user_table WHERE username = @username AND password = @password;";
        
        _connection.Open();
        User user = _connection.Query<User>(sql, new { username = username, password = password }).First();
        _connection.Close();
        return user.Id;
    }

    public int GetUserId(string loginKey)
    {
        return GetUser(loginKey).Id;
    }

    public string GetUsername(string loginKey)
    {
        return GetUser(loginKey).Username;
    }

    public void GetUsernames()
    {
        // TODO
    }
}

static class LoginKey
{
    public static string Create()
    {
        using(RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            byte[] bytes = new byte[32];
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
    }
}