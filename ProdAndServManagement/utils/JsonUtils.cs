using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text.Json;

namespace ProdAndServManagement.utils
{
    public static class JsonUtils
    {
        public static void SerializeAsJson<T>(T obj, string filePath)
        {
            DataContractJsonSerializerSettings settings = new DataContractJsonSerializerSettings()
            {
                UseSimpleDictionaryFormat = true,
                KnownTypes = GeneralUtils.GetKnownTypes(),
                EmitTypeInformation = EmitTypeInformation.AsNeeded,

            };

            DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(T), settings);

            using (StreamWriter streamWriter = new StreamWriter(filePath))
            {
                try
                {
                    dataContractJsonSerializer.WriteObject(streamWriter.BaseStream, obj);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error while trying to serialize JSON object of type: " + typeof(T).Name.ToString());
                }
            }
        }


        public static T? DeserializeFromJson<T>(string filePath)
        {
            DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(T), GeneralUtils.GetKnownTypes());

            using (StreamReader streamReader = new StreamReader(filePath))
            {
                try
                {
                    return (T)dataContractJsonSerializer.ReadObject(streamReader.BaseStream);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error while trying to deserialize JSON object of type: " + typeof(T).Name.ToString());
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
