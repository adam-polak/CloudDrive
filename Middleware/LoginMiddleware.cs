using System.Text;

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

    private int GetUidFromStream(Stream stream)
    {
        int uid = -1;
        int i;
        while((i = stream.ReadByte()) != -1)
        {
            if((char)i == ':')
            {
                bool start = false;
                string s = "";
                while((i = stream.ReadByte()) != -1)
                {
                    char c = (char)i;
                    if(start) {
                        if(c == ' ') break;
                        s.Append(c);
                    } else if(c == ' ') continue;
                    else start = true;
                }

                try
                {
                    return int.Parse(s);
                } catch {
                    return -1;
                }
            }
        }

        return uid;
    }

    private async Task RespondBadUserKey(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.StatusCode = 401; // Unauthorized
        await context.Response.WriteAsync("Invalid user key");
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Validate endpoint
        string endpoint = context.GetEndpoint()?.ToString() ?? "";
        if(!ValidEndpoint(endpoint))
        {
            using(MemoryStream memoryStream = new MemoryStream())
            {
                await context.Request.Body.CopyToAsync(memoryStream);
                memoryStream.Position = 0;
                context.Request.Body = memoryStream;
                
                int uid = GetUidFromStream(memoryStream);

                if(uid == -1) {
                    // invalid uid in body
                    await RespondBadUserKey(context);
                    return;
                }
                
                // TODO
                // check that key is valid in db
            }
        }
        
        // All is good, proceed with next task
        await _next(context);
    }
}