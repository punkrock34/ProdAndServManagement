using ProdAndServManagement.AbstractClasses;
using ProdAndServManagement.utils;
using System.Globalization;
using System.Runtime.Serialization;
using System.Xml;

namespace ProdAndServManagement.Products.Manager
{
    [DataContract(Name = "ProductManager")]
    internal class ProductManager : AbstractObjectManagerBuilder<Product>
    {
        public ProductManager(List<Product>? objects, uint objectCounter, uint managerId) : base(objects, objectCounter, managerId)
        {
           
        }

        public ProductManager() { }

        public void AddNewProduct(string? Name, string? InternalCode, string? Producer, string? Price)
        {
            if(string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(InternalCode) || string.IsNullOrEmpty(Producer) || string.IsNullOrEmpty(Price)) {
                Console.WriteLine("Invalid product, required parameters are: Name, InternalCode, Producer");
                return;
            }

            if (!Decimal.TryParse(Price, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal price))
            {
                Console.WriteLine("Price is invalid, accepted inputs: 'xx.xx'");
                return;
            }

            objects = GetObjects();

            uint randId = GenerateRandomId();

            //generate random Id untill we find one unique;
            while(objects.Where(p => p.ID == randId).Any()) randId = GenerateRandomId();

            Product newProduct = new Product(randId, Name, InternalCode, price, Producer);

           if(!Equals(newProduct))
            {
                objects.Add(newProduct);
                objectCounter = Convert.ToUInt32(objects.Count());
                Console.WriteLine($"New Product with internal code ${newProduct.InternalCode} added sucessfully!");
                return;
            }
            else
            {
                Console.WriteLine("Object already exists!");
            }

        }

    }
}
