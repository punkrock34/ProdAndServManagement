using System.Runtime.Serialization;

namespace ProdAndServManagement.AbstractClasses
{
    [DataContract]
    internal abstract class AbstractObjectManagerBuilder<T> where T : AbstractObjectBuilder<T>
    {

        public AbstractObjectManagerBuilder(List<T>? objects, uint objectCounter, uint managerId)
        {
            this.objects = objects;
            this.objectCounter = objectCounter;
            this.managerId = managerId;
        }

        public AbstractObjectManagerBuilder() { }

        [DataMember(Name = "Objects")]
        public List<T>? objects;
        [DataMember(Name = "ObjectCounter")]
        public uint objectCounter;
        [DataMember(Name = "managerID")]
        public uint managerId;

        protected Random random = new Random();

        public uint GetObjectCounter()
        {
            return this.objectCounter;
        }

        public void SetObjectCounter(int objectCounter)
        {
            if(objectCounter < uint.MinValue)
            {
                Console.WriteLine("Counter can't be smaller than 0");
                return;
            }

            this.objectCounter = (uint)objectCounter;
        }

        public List<T> GetObjects()
        {
            if (this.objects == null) return new List<T>();

            return this.objects;
        }

        public uint GenerateRandomId()
        {
            if(random == null)
            {
                random = new Random();
            }

            uint randomId = (uint)random.Next(1 << 30);

            return randomId;
        }

        public string ObjectManagerDescription()
        {
            if (objects == null || objectCounter == 0)
            {
                return "For "+ typeof(T) +" manager with ID: " + managerId +
                    "\nWe don't have any products!\n";
            }

            string objectManagerElements = "[\n";

            foreach(T? obj in objects)
            {
                objectManagerElements += obj.Description() + (obj == objects.Last() ? "\n" : ",\n");
            }

            objectManagerElements += "\n]";

            string classTypeName = typeof(T).Name.ToLower();

            return "For "+classTypeName+" manager with ID: " + managerId +
                    "\nWe got "+classTypeName+"s: " + objectManagerElements +
                    "\nTotal "+classTypeName+"s: " + objectCounter;
        }

        public override bool Equals(object? obj)
        {
            T? tempObject = obj as T; 

            objects = GetObjects();

            return objects.Any(objectToCompare => objectToCompare.Equals(obj));
        }
    }
}
