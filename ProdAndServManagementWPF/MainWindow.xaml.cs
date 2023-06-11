using System.Windows;
using ProdAndServManagementWPF.Windows;

namespace WpfApplication
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ManagersButton_Click(object sender, RoutedEventArgs e)
        {
            // Open the ManagersWindow
            ManagersWindow managersWindow = new ManagersWindow();
            managersWindow.Show();
        }

        private void ObjectsButton_Click(object sender, RoutedEventArgs e)
        {
            // Open the ObjectsWindow
            ObjectsWindow objectsWindow = new ObjectsWindow();
            objectsWindow.Show();
        }

        private void PackagesButton_Click(object sender, RoutedEventArgs e)
        {
            // Open the PackagesWindow
            PackagesWindow packagesWindow = new PackagesWindow();
            packagesWindow.Show();
        }
    }
}
