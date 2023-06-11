using System.Runtime.Serialization;

namespace ProdAndServManagement.AbstractClasses
{
    [DataContract]
    internal abstract class AbstractObjectManagerBuilder<TObject> where TObject : AbstractObjectBuilder<TObject>
    {

        public AbstractObjectManagerBuilder(List<TObject>? objects, uint objectCounter, uint managerId)
        {
            this.objects = objects;
            this.objectCounter = objectCounter;
            this.managerId = managerId;
        }

        public AbstractObjectManagerBuilder() { }

        [DataMember(Name = "ObjectsArray")]
        public List<TObject>? objects;
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

        public List<TObject> GetObjects()
        {
            if (this.objects == null) return new List<TObject>();

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
                return "For "+ typeof(TObject) +" manager with ID: " + managerId +
                    "\nWe don't have any products!\n";
            }

            string objectManagerElements = "[\n";

            foreach(TObject? obj in objects)
            {
                objectManagerElements += obj.Description() + (obj == objects.Last() ? "\n" : ",\n");
            }

            objectManagerElements += "\n]";

            string classTypeName = typeof(TObject).Name.ToLower();

            return "For "+classTypeName+" manager with ID: " + managerId +
                    "\nWe got "+classTypeName+"s: " + objectManagerElements +
                    "\nTotal "+classTypeName+"s: " + objectCounter;
        }

        //this used to be a override of Equals(object obj)
        //not anymore because serialization uses this automatically and sends loop of objects as TObject
        //if you need to have this overriden from the main class don't forget to handle where TObject is of type List<TObject>
        public bool Equals(TObject? obj) 
        {

            objects = GetObjects();

            return objects.Any(objectToCompare => objectToCompare.Equals(obj));
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
