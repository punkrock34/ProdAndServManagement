// See https://aka.ms/new-console-template for more information
using ProdAndServManagement;
using ProdAndServManagement.Packages;
using ProdAndServManagement.Packages.Manager;
using ProdAndServManagement.Products;
using ProdAndServManagement.Products.Manager;
using ProdAndServManagement.Services;
using ProdAndServManagement.Services.Manager;




string pathToXmlFile = "";
string pathToJsonFile = "";

pathToXmlFile = "input\\Products.xml";
pathToJsonFile = "input\\ProductManagers.json";
Application<ProductManager, Product> productApplication = new Application<ProductManager, Product>(pathToXmlFile, pathToJsonFile);

pathToXmlFile = "input\\Services.xml";
pathToJsonFile = "input\\ServiceManagers.json";
Application<ServiceManager, Service> serviceApplication = new Application<ServiceManager, Service>(pathToXmlFile, pathToJsonFile);

pathToXmlFile = "input\\Packages.xml";
pathToJsonFile = "input\\PackageManagers.json";
Application<PackageManager, Package> packageApplication = new Application<PackageManager, Package>(pathToXmlFile, pathToJsonFile);






//just test purpouses
List<Product> products = productApplication.GetObjectsFromXml();

List<Service> services = serviceApplication.GetObjectsFromXml();

List<Package> packages = packageApplication.GetObjectsFromXml();

ProductManager tempProductManager = new ProductManager(null, 0, 1);
ServiceManager tempServiceManager = new ServiceManager(null, 0, 1);
PackageManager tempPackageManager = new PackageManager(null, 0, 1);

tempProductManager.AddNewProduct("Product 1", "0000001", "Producer 1", "19.99");
tempProductManager.AddNewProduct("Product 2", "0000002", "Producer 1", "29.00");

tempServiceManager.AddNewService("Service 1", "0000001", "25.99");
tempServiceManager.AddNewService("Service 2", "0000002", "49.99");

tempPackageManager.AddNewPackage("Package 1", "0000001", "129.99", "Category Producer 1 under 30");
tempPackageManager.AddNewPackage("Package 2", "0000002", "50.00", "Category Producer 1 under 50");

List<Product> tempProducts = tempProductManager.GetObjects();
List<Service> tempServices = tempServiceManager.GetObjects();
List<Package> tempPackages = tempPackageManager.GetObjects();

foreach(Package tempPackage in tempPackages)
{
    packageApplication.addObject(tempPackage);
    foreach(Product tempProduct in tempProducts)
    {
        productApplication.addObject(tempProduct);
        if (tempPackage.CanAddToPackage(tempPackage, tempProduct))
        {
            tempPackage.AddObjectToPackage(tempProduct);
        }
    }

    foreach(Service tempService in tempServices)
    {
        serviceApplication.addObject(tempService);
        if (tempPackage.CanAddToPackage(tempPackage, tempService))
        {
            tempPackage.AddObjectToPackage(tempService);
        }
    }
}

productApplication.addManager(tempProductManager);
serviceApplication.addManager(tempServiceManager);
packageApplication.addManager(tempPackageManager);



//serialize lists of products, services, packages as xml
Console.WriteLine(Convert.ToString(productApplication.SetObjectManagersToJson()) + " - " + Convert.ToString(productApplication.SetObjectsToXml()));
Console.WriteLine(Convert.ToString(serviceApplication.SetObjectManagersToJson()) + " - " + Convert.ToString(serviceApplication.SetObjectsToXml()));
Console.WriteLine(Convert.ToString(packageApplication.SetObjectManagersToJson()) + " - " + Convert.ToString(packageApplication.SetObjectsToXml()));