using Newtonsoft.Json;

namespace CloudDrive.Lib;

public class JsonInfo
{
    public static string GetJsonDevVariable(string key)
    {
        string ans = "";
        bool found = false;
        using(JsonTextReader reader = new JsonTextReader(new StreamReader("secrets.json")))
        {
            while(!found && reader.Read())
            {
                if(reader.TokenType.ToString().Equals("PropertyName"))
                {
                    string checkKey = reader.Value != null ? reader.Value.ToString() ?? "" : "";
                    if(!checkKey.Equals(key)) continue;
                    reader.Read();
                    ans = reader.Value != null ? reader.Value.ToString() ?? "" : "";
                    found = true;
                    break;
                }
            }
        }
        
        return ans;
    }
}