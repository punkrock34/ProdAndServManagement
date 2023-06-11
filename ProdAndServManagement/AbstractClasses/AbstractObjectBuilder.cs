using System.Runtime.Serialization;

namespace ProdAndServManagement.AbstractClasses
{
    [DataContract]
    internal abstract class AbstractObjectBuilder<TObject>
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
        public abstract string Description();

        public abstract bool Equals(TObject? obj);

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
