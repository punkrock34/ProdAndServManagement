
using ProdAndServManagement.Products;
using ProdAndServManagement.Services;
using System.Net;

namespace ProdAndServManagement.utils
{
    public static class GeneralUtils
    {

        public static bool CheckIfFileValid(string? file)
        {
            if (string.IsNullOrEmpty(file)) return false;
            return true;
        }

        //for file we broke it into two because we usually create an empty file at startup of the application for storing the objects, and we need to check if the file actually exists when trying to download it
        public static bool CheckIfFileExists(string? file)
        {
            if (!CheckIfFileValid(file)) return false;
            if (!File.Exists(file)) return false;
            return true;
        }

        public static bool CheckIfFolderExists(string? folder)
        {
            if (string.IsNullOrEmpty(folder)) return false;
            if (!Directory.Exists(folder)) return false;

            return true;
        }

        public static List<Type> GetKnownTypes()
        {
            return new List<Type>
            {
                typeof(Product),
                typeof(Service)
                // Add other known types here
            };
        }

        public static void DownloadFile(string filePath, string downloadPath)
        {
            using (WebClient client = new WebClient())
            {
                try
                {
                    if(!CheckIfFolderExists(downloadPath))
                    {
                        throw new Exception($"Could not find directory for download {downloadPath}");
                    }

                    if(!CheckIfFileExists(filePath))
                    {
                        throw new Exception($"Could not download file {filePath}");
                    }

                    string downloadFilePath = Path.Combine(downloadPath, Path.GetFileName(filePath));

                    client.DownloadFile(filePath, downloadFilePath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error while trying to download file: " + ex.Message);
                }
            }

        }

        public static string GetTempDownload(string fileExt)
        {
            string TempDownloadFileName = "serialized_data_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + $".{fileExt}";
            string TempDownloadFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), TempDownloadFileName);

            return TempDownloadFilePath;
        }

    }
}
