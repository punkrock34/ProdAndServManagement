
using ProdAndServManagement.AbstractClasses;
using ProdAndServManagement.Interfaces;
using ProdAndServManagement.Packages;
using ProdAndServManagement.Products;
using System.Runtime.Serialization;

namespace ProdAndServManagement.Services
{
    [DataContract(Name = "Service"), KnownType(typeof(Service))]
    internal class Service : AbstractObjectBuilder<Service>, IPackageable
    {

        public Service(uint id, string name, string internalCode, decimal price) : base(id, name, internalCode, price)
        {
        }

        public Service()
        {
        }


        public override string Description()
        {
            return "\t{\"ID\": " + ID + ", \"Name\": \"" + Name + "\", \"InternalCode\": \"" + InternalCode + "\", \"Price\": \"" + Price + "\"}";
        }

        public override bool Equals(Service? obj) => obj != null && obj.Name == Name && obj.InternalCode == InternalCode;

        public override bool CanAddToPackage(Package? package)
        {
            if(package == null || package.GetObjectCount(this) == 2) {
                return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
