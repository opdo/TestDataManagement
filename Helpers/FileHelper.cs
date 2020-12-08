using Newtonsoft.Json;
using System.IO;

namespace TestDataManagement.Helpers
{
    public class FileHelper
    {
        public static T ReadJson<T>(string content)
        {
            return JsonConvert.DeserializeObject<T>(content);
        }

        public static void SaveJson(string filepath, object json)
        {
            // Parse to string
            var jsonToString = JsonConvert.SerializeObject(json, Formatting.Indented);

            // Open and save file
            File.WriteAllText(filepath, jsonToString);
        }

        public static T ReadJsonFromFile<T>(string filepath)
        {
            var content = File.ReadAllText(filepath);
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}
