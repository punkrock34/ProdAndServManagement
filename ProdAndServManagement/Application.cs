using ProdAndServManagement.AbstractClasses;
using ProdAndServManagement.utils;

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

        public void AddManager(TManager manager)
        {
            managerList.Add(manager);
        }

        public bool RemoveManager(TManager manager)
        {
            return managerList.Remove(manager);
        }

        public void AddObject(TObject @object)
        {
            objectList.Add(@object);
        }

        public bool RemoveObject(TObject @object)
        {
            return objectList.Remove(@object);
        }

        public List<TObject> GetObjectsFromXml()
        {
            if (!GeneralUtils.CheckIfFileValid(PathToXmlFile))
                return objectList;

            if (!File.Exists(PathToXmlFile)) return objectList;

            try
            {
                List<TObject> tempObjects = XmlUtils.DeserializeFromXml<List<TObject>>(PathToXmlFile);

                if (tempObjects == null) return objectList;

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

            foreach (TManager manager in managerList)
            {
                List<TObject> objects = manager.GetObjects();
                foreach (TObject @object in objects)
                {
                    if(objectList.Any(o => !o.Equals(@object))) continue;
                    objectList.Add(@object);
                }
            }

            try
            {
                XmlUtils.SerializeAsXml(objectList, PathToXmlFile);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while trying to serialize XML");
                Console.WriteLine(ex.ToString());
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

                if (tempManagers == null) return managerList;

                HashSet<uint> existingManagerIds = new HashSet<uint>(managerList.Select(m => m.managerId));

                foreach (TManager tempManager in tempManagers)
                {
                    if (existingManagerIds.Contains(tempManager.managerId)) continue;

                    managerList.Add(tempManager);
                    existingManagerIds.Add(tempManager.managerId);
                }

                return managerList;

            }
            catch
            {
                Console.WriteLine("Error while trying to deserialize JSON");
                return managerList;
            }
        }

        public bool SetObjectManagersToJson()
        {
            if (!GeneralUtils.CheckIfFileValid(PathToJsonFile))
                return false;

            foreach (TManager manager in managerList)
            {
                List<TObject> objects = manager.GetObjects();
                foreach (TObject @object in objects)
                {
                    if (objectList.Any(o => !o.Equals(@object))) continue;
                    objectList.Add(@object);
                }
            }

            try
            {
                JsonUtils.SerializeAsJson(managerList, PathToJsonFile);
                return true;
            }
            catch
            {
                Console.WriteLine("Error while trying to serialize JSON");
                return false;
            }
        }

        public bool SetObjectsToTempDownloadFile(List<TObject> objectList, string DownloadPath, string FileExtDownload = "xml")
        {
            string TempDownloadFilePath = GeneralUtils.GetTempDownload(FileExtDownload);

            try
            {
                XmlUtils.SerializeAsXml(objectList, TempDownloadFilePath, prettySerialize: true);
                GeneralUtils.DownloadFile(TempDownloadFilePath, DownloadPath);

                File.Delete(TempDownloadFilePath);
                return true;
            }
            catch
            {
                Console.WriteLine("Error while trying to download File");
                return false;
            }
        }

        public bool SetObjectManagersToTempDownloadFile(List<TManager> managerList, string DownloadPath, string FileExtDownload = "json")
        {
            string TempDownloadFilePath = GeneralUtils.GetTempDownload(FileExtDownload);

            try
            {
                JsonUtils.SerializeAsJson(managerList, TempDownloadFilePath, prettySerialize: true);
                GeneralUtils.DownloadFile(TempDownloadFilePath, DownloadPath);

                File.Delete(TempDownloadFilePath); 
                return true;
            }
            catch
            {
                Console.WriteLine("Error while trying to download File");
                return false;
            }
        }

    }
}
