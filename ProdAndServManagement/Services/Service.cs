
using ProdAndServManagement.AbstractClasses;
using ProdAndServManagement.Packages;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace ProdAndServManagement.Services
{
    [DataContract(Name = "Service")]
    internal class Service : AbstractObjectBuilder<Service>
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

        public override bool Equals(object? obj)
        {
            Service? service = obj as Service;

            return service.Name == this.Name && service.InternalCode == this.InternalCode;
            
        }

        public override bool CanAddToPackage(Package? package, object? obj)
        {
            if(package == null || package.GetObjectCount(obj) > 1) {
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
