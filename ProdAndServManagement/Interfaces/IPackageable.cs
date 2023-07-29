using ProdAndServManagement.Packages;

namespace ProdAndServManagement.Interfaces
{
    internal interface IPackageable
    {
        bool CanAddToPackage(Package? package);

        bool AddToPackagePrice(Package package);

        bool RemoveFromPackagePrice(Package package);

        string Description();
    }
}
