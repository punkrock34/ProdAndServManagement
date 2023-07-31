// See https://aka.ms/new-console-template for more information
using ProdAndServManagement;
using ProdAndServManagement.Packages;
using ProdAndServManagement.Packages.Manager;
using ProdAndServManagement.Products;
using ProdAndServManagement.Products.Manager;
using ProdAndServManagement.Services;
using ProdAndServManagement.Services.Manager;

string rootDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Data\\";

string pathToXmlFile = Path.Combine(rootDirectory, "Products.xml");
string pathToJsonFile = Path.Combine(rootDirectory, "ProductManagers.json");

Application<ProductManager, Product> productApplication = new Application<ProductManager, Product>(pathToXmlFile, pathToJsonFile);
List<ProductManager> productManagers = productApplication.GetObjectManagersFromJson();
List<Product> products = productApplication.GetObjectsFromXml();

pathToXmlFile = Path.Combine(rootDirectory, "Services.xml");
pathToJsonFile = Path.Combine(rootDirectory, "ServiceManagers.json");
Application<ServiceManager, Service> serviceApplication = new Application<ServiceManager, Service>(pathToXmlFile, pathToJsonFile);
List<ServiceManager> serviceManagers = serviceApplication.GetObjectManagersFromJson();
List<Service> services = serviceApplication.GetObjectsFromXml();

pathToXmlFile = Path.Combine(rootDirectory, "Packages.xml");
pathToJsonFile = Path.Combine(rootDirectory, "PackageManagers.json");
Application<PackageManager, Package> packageApplication = new Application<PackageManager, Package>(pathToXmlFile, pathToJsonFile);
List<PackageManager> packageManagers = packageApplication.GetObjectManagersFromJson();
List<Package> packages = packageApplication.GetObjectsFromXml();
