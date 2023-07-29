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

foreach(ProductManager productManager in productManagers)
{
    Console.WriteLine(productManager.ObjectManagerDescription());
}

List<Product> products = productApplication.GetObjectsFromXml();

foreach(Product product in products)
{
    Console.WriteLine(product.Description());
}

pathToXmlFile = Path.Combine(rootDirectory,"Services.xml");
pathToJsonFile = Path.Combine(rootDirectory, "ServiceManagers.json");
Application<ServiceManager, Service> serviceApplication = new Application<ServiceManager, Service>(pathToXmlFile, pathToJsonFile);
List<ServiceManager> serviceManagers = serviceApplication.GetObjectManagersFromJson();

foreach(ServiceManager serviceManager in serviceManagers)
{
    Console.WriteLine(serviceManager.ObjectManagerDescription());
}

pathToXmlFile = Path.Combine(rootDirectory, "Packages.xml");
pathToJsonFile = Path.Combine(rootDirectory, "PackageManagers.json");
Application<PackageManager, Package> packageApplication = new Application<PackageManager, Package>(pathToXmlFile, pathToJsonFile);
List<PackageManager> packageManagers = packageApplication.GetObjectManagersFromJson();

foreach(PackageManager packageManager in packageManagers)
{
    Console.WriteLine(packageManager.ObjectManagerDescription());
}

List<Package> packages = packageApplication.GetObjectsFromXml();
foreach(Package package in packages)
{
    Console.WriteLine(package.Description());
}

//just test purpouses -> mostly to test application functionality for xml serialization and deserialization along with json
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

// Assign products and services to packages
int productIndex = 0;
int serviceIndex = 0;

foreach (Package tempPackage in tempPackages)
{
    packageApplication.addObject(tempPackage);

    // Add a product to the package
    if (productIndex < tempProducts.Count)
    {
        Product tempProduct = tempProducts[productIndex];
        productApplication.addObject(tempProduct);

        if (tempProduct.CanAddToPackage(tempPackage))
        {
            tempPackage.AddObjectToPackage(tempProduct);
            tempPackage.AddToPackagePrice(tempProduct.Price);
            productIndex++;
        }
    }

    // Add a service to the package
    if (serviceIndex < tempServices.Count)
    {
        Service tempService = tempServices[serviceIndex];
        serviceApplication.addObject(tempService);

        if (tempService.CanAddToPackage(tempPackage))
        {
            tempPackage.AddObjectToPackage(tempService);
            tempPackage.AddToPackagePrice(tempService.Price);
            serviceIndex++;
        }
    }
}

productApplication.addManager(tempProductManager);
serviceApplication.addManager(tempServiceManager);
packageApplication.addManager(tempPackageManager);


//descriere manageri
Console.WriteLine("Descriere manager produse: ");
Console.WriteLine(tempProductManager.ObjectManagerDescription());

Console.WriteLine("Descriere manager servicii: ");
Console.WriteLine(tempServiceManager.ObjectManagerDescription());

Console.WriteLine("Descriere manager pachete: ");
Console.WriteLine(tempPackageManager.ObjectManagerDescription());


//serialize lists of products, services, packages as xml
Console.WriteLine("Rezultate serializare xml-json");
Console.WriteLine(Convert.ToString(productApplication.SetObjectManagersToJson()) + " - " + Convert.ToString(productApplication.SetObjectsToXml()));
Console.WriteLine(Convert.ToString(serviceApplication.SetObjectManagersToJson()) + " - " + Convert.ToString(serviceApplication.SetObjectsToXml()));
Console.WriteLine(Convert.ToString(packageApplication.SetObjectManagersToJson()) + " - " + Convert.ToString(packageApplication.SetObjectsToXml()));