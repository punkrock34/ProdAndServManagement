using ProdAndServManagement.AbstractClasses;
using ProdAndServManagement.Interfaces;
using ProdAndServManagement.Packages;
using System.Runtime.Serialization;

namespace ProdAndServManagement.Products
{
    [DataContract(Name = "Product"), KnownType(typeof(Product))]
    internal class Product : AbstractObjectBuilder<Product>, IPackageable
    {
        [DataMember(Name = "Producer")]
        public string? Producer { get; set; }

        public Product(uint id, string name, string internalCode, decimal price, string producer) : base(id, name, internalCode, price)
        {
            Producer = producer;
        }

        public Product()
        {
        }

        public override string Description()
        {
            return "\t{\"ID\": " + ID + ", \"Name\": \"" + Name + "\", \"InternalCode\": \"" + InternalCode + "\", \"Producer\": \"" + Producer + "\", \"Price\":  \"" + Price + "\"}";
        }

        public override bool Equals(Product? obj) => obj != null && obj.Name == Name && obj.InternalCode == InternalCode;

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

    }
}
