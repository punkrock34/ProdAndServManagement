using System.Runtime.Serialization;
using System.Xml;

namespace ProdAndServManagement.utils
{
    public static class XmlUtils
    {

        public static XmlDocument LoadXmlDocument(string xmlPath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);
            return xmlDoc;
        }

        public static void SerializeAsXml<T>(T obj, string filePath, bool prettySerialize = false)
        {
            DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(T));

            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings
            {
                Indent = prettySerialize, // Enable indentation if prettySerialize is true
                IndentChars = "\t" // Set the indentation character(s) to use, such as tab (\t)
            };

            using (XmlWriter xmlWriter = XmlWriter.Create(filePath, xmlWriterSettings))
            {
                dataContractSerializer.WriteObject(xmlWriter, obj);
            }
        }


        public static T? DeserializeFromXml<T>(string filePath)
        {
            DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(T));

            using (XmlReader xmlReader = XmlReader.Create(filePath))
            {
                try
                {
                    return (T)dataContractSerializer.ReadObject(xmlReader);
                }catch(Exception ex)
                {
                    Console.WriteLine("Error while trying to deserialize object!");
                    return default(T);
                }
            }

        }

        public static string? GetXmlNodeInnerTextByTag(XmlNode node, string tagName)
        {
            XmlNode? childNode = node[tagName];
            return childNode?.InnerText;
        }

        public static XmlNodeList? GetXmlInnerNodeList(XmlNode node, string tagName)
        {
            XmlNodeList? childNodeList = node.SelectNodes("descendant::" + tagName);
            if(childNodeList == null)
            {
                Console.WriteLine("Invalid tagName!");
                return null;
            }

            return childNodeList;
        }

        public static XmlNodeList? GetXmlInnerNodesByXPath(XmlDocument xmlDocument,string tagName)
        {
            if(xmlDocument == null || string.IsNullOrEmpty(tagName))
            {
                Console.WriteLine("Invalid xmlDocument or tagName!");
                return null;
            }

            XmlNodeList? nodes = xmlDocument.SelectNodes("descendant::" + tagName);

            if(nodes == null)
            {
                Console.WriteLine("Invalid tagName!");
                return null;
            }

            return nodes;

        }

    }
}
