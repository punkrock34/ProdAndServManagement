using System.Windows;
using ProdAndServManagementWPF.Pages;
using ProdAndServManagement.Packages;
using ProdAndServManagement.Packages.Manager;
using ProdAndServManagement.Products;
using ProdAndServManagement.Products.Manager;
using ProdAndServManagement.Services;
using ProdAndServManagement.Services.Manager;
using System.Collections.Generic;
using System.IO;
using ProdAndServManagement;

namespace WpfApplication
{
    public partial class MainWindow : Window
    {
        private readonly HomePage homePage = new HomePage();
        private readonly PackagePage packagePage = new PackagePage();
        private readonly ServicePage servicePage = new ServicePage();
        private readonly ProductPage productPage = new ProductPage();

        internal List<ProductManager> productManagers = new List<ProductManager>();
        internal List<Product> products = new List<Product>();

        internal List<ServiceManager> serviceManagers = new List<ServiceManager>();
        internal List<Service> services = new List<Service>();

        internal List<PackageManager> packageManagers = new List<PackageManager>();
        internal List<Package> packages = new List<Package>();

        public MainWindow()
        {
            InitializeComponent();
            MainContentFrame.NavigationService.Navigate(homePage);

            string rootDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\Data\\";

            string pathToXmlFile = Path.Combine(rootDirectory, "Products.xml");
            string pathToJsonFile = Path.Combine(rootDirectory, "ProductManagers.json");

            Application<ProductManager, Product> productApplication = new Application<ProductManager, Product>(pathToXmlFile, pathToJsonFile);
            productManagers = productApplication.GetObjectManagersFromJson();
            products = productApplication.GetObjectsFromXml();

            pathToXmlFile = Path.Combine(rootDirectory, "Services.xml");
            pathToJsonFile = Path.Combine(rootDirectory, "ServiceManagers.json");
            Application<ServiceManager, Service> serviceApplication = new Application<ServiceManager, Service>(pathToXmlFile, pathToJsonFile);
            serviceManagers = serviceApplication.GetObjectManagersFromJson();
            services = serviceApplication.GetObjectsFromXml();

            pathToXmlFile = Path.Combine(rootDirectory, "Packages.xml");
            pathToJsonFile = Path.Combine(rootDirectory, "PackageManagers.json");
            Application<PackageManager, Package> packageApplication = new Application<PackageManager, Package>(pathToXmlFile, pathToJsonFile);
            packageManagers = packageApplication.GetObjectManagersFromJson();
            packages = packageApplication.GetObjectsFromXml();
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            MainContentFrame.NavigationService.Navigate(homePage);
        }

        private void PackagesButton_Click(object sender, RoutedEventArgs e)
        {
            MainContentFrame.NavigationService.Navigate(packagePage);
        }

        private void ServicesButton_Click(object sender, RoutedEventArgs e)
        {
            MainContentFrame.NavigationService.Navigate(servicePage);
        }

        private void ProductsButton_Click(object sender, RoutedEventArgs e)
        {
            MainContentFrame.NavigationService.Navigate(productPage);
        }
    }
}
