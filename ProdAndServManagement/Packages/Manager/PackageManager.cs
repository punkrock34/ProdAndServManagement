using ProdAndServManagement.AbstractClasses;
using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Xml;

namespace ProdAndServManagement.Packages.Manager
{
    [DataContract]
    internal class PackageManager : AbstractObjectManagerBuilder<Package>
    {
        public PackageManager(List<Package>? objects, uint packageCounter, uint managerId) : base(objects, packageCounter, managerId)
        {
        }

        public void AddNewPackage(string? Name, string? InternalCode, string? Price, string? Category)
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(InternalCode) || string.IsNullOrEmpty(Category) || string.IsNullOrEmpty(Price))
            {
                Console.WriteLine("Invalid package, required parameters are: Name, InternalCode, Category, Price");
                return;
            }

            if (!Decimal.TryParse(Price, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal price))
            {
                Console.WriteLine("Price is invalid, accepted inputs: 'xx.xx'");
                return;
            }

            objects = GetObjects();
               
            uint randId = GenerateRandomId();

            while (objects.Where(p => p.ID == randId).Any()) randId = GenerateRandomId();

            Package newPackage = new Package(randId, Name, InternalCode, price, Category);

            if(!Equals(newPackage)) {
                objects.Add(newPackage);
                objectCounter = Convert.ToUInt32(objects.Count());
                Console.WriteLine($"New Package with internal code ${newPackage.InternalCode} added sucessfully!");
                return;
            }

        }
    }
}
