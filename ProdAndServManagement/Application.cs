using ProdAndServManagement.AbstractClasses;
using ProdAndServManagement.Packages;
using ProdAndServManagement.Packages.Manager;
using ProdAndServManagement.Products;
using ProdAndServManagement.Products.Manager;
using ProdAndServManagement.Services;
using ProdAndServManagement.Services.Manager;
using ProdAndServManagement.utils;
using System.Xml;

namespace ProdAndServManagement
{
    internal class Application<TManager, TObject> where TManager : AbstractObjectManagerBuilder<TObject> where TObject : AbstractObjectBuilder<TObject>
    {
        List<TManager> managerList { get; set; }
        List<TObject> objectList { get; set; }

        public string? PathToXmlFile { get; set; }

        public string? PathToJsonFile { get; set; }

        public Application(string pathToXmlFile, string pathToJsonFile)
        {
            managerList = new List<TManager>();
            objectList = new List<TObject>();
            PathToXmlFile = pathToXmlFile;
            PathToJsonFile = pathToJsonFile;
        }

        public void addManager(TManager manager)
        {
            managerList.Add(manager);
        }

        public bool removeManager(TManager manager)
        {
            return managerList.Remove(manager);
        }

        public void addObject(TObject @object)
        {
            objectList.Add(@object);
        }

        public bool removeObject(TObject @object)
        {
            return objectList.Remove(@object);
        }

        public List<TObject> GetObjectsFromXml()
        {
            if (!GeneralUtils.CheckIfFileValid(PathToXmlFile))
                return objectList;

            try
            {
                List<TObject> tempObjects = XmlUtils.DeserializeFromXml<List<TObject>>(PathToXmlFile);

                if(tempObjects == null) return objectList;

                foreach (TObject tempObject in tempObjects)
                {

                    if (objectList.Any(o => o.Equals(tempObject))) continue;

                    objectList.Add(tempObject);
                }

                return objectList;
            }
            catch
            {
                Console.WriteLine("Error while trying to deserialize XML");
                return objectList;
            }
        }

        public bool SetObjectsToXml()
        {
            if (!GeneralUtils.CheckIfFileValid(PathToXmlFile))
                return false;

            try
            {
                
                XmlUtils.SerializeAsXml(objectList, PathToXmlFile, true);
                return true;
            }
            catch
            {
                Console.WriteLine("Error while trying to serialize XML");
                return false;
            }
        }

        public List<TManager> GetObjectManagersFromJson()
        {
            if (!GeneralUtils.CheckIfFileValid(PathToJsonFile))
                return managerList;

            try
            {
                List<TManager>? tempManagers = JsonUtils.DeserializeFromJson<List<TManager>>(PathToJsonFile);

                if(tempManagers == null) return managerList;

                HashSet<uint> existingManagerIds = new HashSet<uint>(managerList.Select(m => m.managerId));

                foreach (TManager tempManager in tempManagers)
                {
                    if (existingManagerIds.Contains(tempManager.managerId)) continue;

                    managerList.Add(tempManager);
                    existingManagerIds.Add(tempManager.managerId);
                }

                return managerList;

            } catch {
                Console.WriteLine("Error while trying to deserialize JSON");
                return managerList;
            }
        }

        public bool SetObjectManagersToJson()
        {
            if (!GeneralUtils.CheckIfFileValid(PathToJsonFile))
                return false;

            try
            {
                JsonUtils.SerializeAsJson(objectList, PathToJsonFile, true);
                return true;
            }
            catch
            {
                Console.WriteLine("Error while trying to serialize JSON");
                return false;
            }
        }

    }
}
