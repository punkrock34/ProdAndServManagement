using ProdAndServManagement.Packages;

namespace ProdAndServManagement.Interfaces
{
    internal interface IPackageable
    {
        bool CanAddToPackage(Package? package, object? obj);
        string Description();
    }
}
