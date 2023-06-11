using ProdAndServManagement.Packages;

namespace ProdAndServManagement.Interfaces
{
    internal interface IPackageable
    {
        bool CanAddToPackage(Package? package);
        string Description();
    }
}
