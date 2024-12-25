using CloudDrive.DataAccess.Controllers;
using Microsoft.Extensions.Primitives;

namespace CloudDrive.Middleware;

public class LoginMiddleware
{
    private readonly RequestDelegate _next;

    public LoginMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    private bool ValidEndpoint(string endpoint)
    {
        switch(endpoint)
        {
            case "":
            case "login":
            case "createuser":
                return true;
            default:
                return false;
        }
    }

    private async Task RespondBadLoginKey(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.StatusCode = 401; // Unauthorized
        await context.Response.WriteAsync("Invalid login key");
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Validate endpoint
        string endpoint = context.GetEndpoint()?.ToString() ?? "";
        if(!ValidEndpoint(endpoint))
        {
            bool isValid = false;
            if(context.Request.Query.TryGetValue("loginkey", out StringValues s))
            {
                if(int.TryParse(s.ToString(), out int loginKey))
                {
                    UserController userController = new UserController();

                    // Validate login key
                    isValid = userController.CorrectLogin(loginKey);
                }
            }

            if(!isValid)
            {
                await RespondBadLoginKey(context);
                return;
            }
        }
        
        // All is good, proceed with next task
        await _next(context);
    }
}