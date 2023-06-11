using ProdAndServManagement.AbstractClasses;
using ProdAndServManagement.Interfaces;
using System.Runtime.Serialization;

namespace ProdAndServManagement.Packages
{
    [DataContract(Name = "Package")]
    internal class Package : AbstractObjectBuilder<Package>
    {
        [DataMember(Name = "Category")]
        string Category { get; set; }

        [DataMember(Name = "Packageables")]
        List<IPackageable>? packageablesObjects;

        public Package(uint id, string name, string internalCode, decimal price, string category) : base(id, name, internalCode, price)
        {
            Category = category;
        }

        public List<IPackageable> GetPackageables()
        {
            if(packageablesObjects == null) return new List<IPackageable>();

            return packageablesObjects;
        }

        public int GetObjectCount(object? packageType)
        {
            if(packageType == null) return 0;

            Type type = packageType.GetType();

            packageablesObjects = GetPackageables();

            return packageablesObjects.Count(x => x.GetType() == type);
        }

        public void AddObjectToPackage(IPackageable packageable)
        {
            packageablesObjects = GetPackageables();
            packageablesObjects.Add(packageable);
        }

        public override string Description()
        {
            string? packageDescription = "{\n\"" + ID + "\":[\n";

            packageablesObjects = GetPackageables();

            foreach (IPackageable packageableObject in packageablesObjects)
            {
                packageDescription += packageableObject.Description() + (packageablesObjects.Last() == packageableObject ? "\n" : ",\n");
            }

            packageDescription += "],\n";

            packageDescription += "\"PackageName\":\"" + Name + "\",\n";
            packageDescription += "\"PackageInternalCode\":\"" + InternalCode + "\",\n";
            packageDescription += "\"PackageCategory\":\"" + Category + "\"\n}";

            return packageDescription;
        }

        public override bool Equals(Package? obj) => Name == obj.Name && Category == obj.Category;

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
