namespace CloudDrive.Controllers.Lib;

public static class BodyReader
{
    public static async Task<string> GetStringFromStream(Stream stream)
    {
        using(StreamReader reader = new StreamReader(stream))
        {
            return await reader.ReadToEndAsync();
        }
    }
}