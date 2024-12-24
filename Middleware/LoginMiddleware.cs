using System.Text;

namespace CloudDrive.Middleware;

public class LoginMiddleware
{
    private readonly RequestDelegate _next;

    public LoginMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    private async Task<string> GetStringFromStream(Stream stream)
    {
        StringBuilder sb = new StringBuilder();
        byte[] buffer = new byte[2048];
        int bytesRead;

        while((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
        {
            int rest;
            while(
                bytesRead != buffer.Length 
                && (rest = await stream.ReadAsync(buffer, bytesRead, buffer.Length - bytesRead)) > 0
                )
            {
                bytesRead += rest;
            }
            sb.Append(
                Encoding.ASCII.GetString(buffer, 0, bytesRead)
            );
        }

        // Reset stream position
        stream.Position = 0;

        return sb.ToString();
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

    public async Task InvokeAsync(HttpContext context)
    {
        // Validate endpoint
        string endpoint = context.GetEndpoint()?.ToString() ?? "";
        if(!ValidEndpoint(endpoint))
        {

        }
        
        await _next(context);
    }
}