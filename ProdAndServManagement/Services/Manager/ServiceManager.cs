using ProdAndServManagement.AbstractClasses;
using ProdAndServManagement.Interfaces;
using System.Globalization;
using System.Runtime.Serialization;

namespace ProdAndServManagement.Services.Manager
{
    [DataContract(Name = "ServiceManager")]
    internal class ServiceManager : AbstractObjectManagerBuilder<Service>, IFilterable<Service>
    {

        public ServiceManager(List<Service>? objects, uint objectCounter, uint managerId) : base(objects, objectCounter, managerId)
        {

        }

        public ServiceManager() { }

        public void AddNewService(string? Name, string? InternalCode, string? Price)
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(InternalCode) || string.IsNullOrEmpty(Price))
            {
                Console.WriteLine("Invalid service, Required parameters are: Name, InternalCode");
                return;
            }

            if (!Decimal.TryParse(Price, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal price))
            {
                Console.WriteLine("Price is invalid, accepted inputs: 'xx.xx'");
                return;
            }

            objects = GetObjects();

            uint randId = GenerateRandomId();

            //generate random Id untill we find one unique
            while (objects.Where(s => s.ID == randId).Any()) randId = GenerateRandomId();

            Service newService = new Service(randId, Name, InternalCode, price);

            if (!Equals(newService))
            {
                objects.Add(newService);
                objectCounter = Convert.ToUInt32(objects.Count());
                Console.WriteLine($"New Service with internal code {newService.InternalCode} added sucessfully!");
                return;
            }

        }

    }
}
