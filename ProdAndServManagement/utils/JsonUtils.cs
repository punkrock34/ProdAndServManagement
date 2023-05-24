using Newtonsoft.Json;
using System.Text.Json;

namespace ProdAndServManagement.utils
{
    public static class JsonUtils
    {
        public static void SerializeAsJson<T>(T obj, string filePath, bool prettySerialize = false)
        {
            Formatting formatting = prettySerialize ? Formatting.Indented : Formatting.None; // https://www.newtonsoft.com/json/help/html/T_Newtonsoft_Json_Formatting.htm

            using (StreamWriter streamWriter = new StreamWriter(filePath))
            {
                string json = JsonConvert.SerializeObject(obj, formatting);
                streamWriter.Write(json);
            }
        }


        public static T? DeserializeFromJson<T>(string filePath)
        {
            using (StreamReader streamReader = new StreamReader(filePath))
            {
                try
                {
                    string json = streamReader.ReadToEnd();
                    return JsonConvert.DeserializeObject<T>(json);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error while trying to deserialize object!");
                    return default(T);
                }
            }
        }

        public static JsonDocument LoadJsonDocument(string jsonPath)
        {
            string json = File.ReadAllText(jsonPath);
            return JsonDocument.Parse(json);
        }

    }
}
