using ProdAndServManagement.Interfaces;
using ProdAndServManagement.Packages;
using System.Runtime.Serialization;

namespace ProdAndServManagement.AbstractClasses
{
    [DataContract]
    internal abstract class AbstractObjectBuilder<T> : IPackageable
    {
        [DataMember(Name = "ID")]
        public uint ID { get; set; }
        [DataMember(Name = "Name")]
        public string? Name { get; set; }
        [DataMember(Name = "InternalCode")]
        public string? InternalCode { get; set; }
        [DataMember(Name = "Price")]
        public decimal? Price { get; set; }

        protected AbstractObjectBuilder(uint id, string name, string internalCode, decimal price)
        {
            ID = id;
            Name = name;
            InternalCode = internalCode;
            Price = price;
        }

        protected AbstractObjectBuilder() { }

        public virtual bool CanAddToPackage(Package? package, object? obj)
        {
            if (package == null || package.GetObjectCount(obj) > 0)
                return false;

            return true;
        }

        public abstract string Description();

        public abstract override bool Equals(object? obj);

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
