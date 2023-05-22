using ProdAndServManagement.AbstractClasses;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ProdAndServManagement.Products
{
    [DataContract(Name = "Product")]
    internal class Product : AbstractObjectBuilder<Product>
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

        public override bool Equals(object? obj)
        {
            Product? product = obj as Product;

            return product.Name == this.Name && product.InternalCode == this.InternalCode;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
