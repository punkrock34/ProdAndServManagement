using ProdAndServManagement.Products.Manager;
using ProdAndServManagement.Products;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WpfApplication;

namespace ProdAndServManagementWPF.Pages
{
    /// <summary>
    /// Interaction logic for ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        private DataGrid dataGrid = null;

        public ProductPage()
        {
            InitializeComponent();

            if (Application.Current.MainWindow is MainWindow mainWindow)
            {
                // Now you can access the lists from the MainWindow
                List<ProductManager> productManagers = mainWindow.productManagers;
                List<Product> products = mainWindow.products;

                // Get dataGrid reference from the XAML
                dataGrid = this.ProductDataGrid;

                // Set the ItemsSource of the dataGrid to the products list
                dataGrid.ItemsSource = products;
            }
        }
    }
}
