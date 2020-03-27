using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{

    public class CDClass
    {
        public string Name { get; set; }
        private List<CDAttribute> Attributes { get; }
        private List<CDMethod> Methods { get; }
        private List<CDClassInstance> Instances { get; }
        private long ClassIDPrefix;
        private long InstanceIDSeed;

        public CDClass(long ClassIDPrefix, String Name)
        {
            this.ClassIDPrefix = ClassIDPrefix;
            this.InstanceIDSeed = 0;
            this.Name = Name;

            this.Attributes = new List<CDAttribute>();

            this.Methods = new List<CDMethod>();

            this.Instances = new List<CDClassInstance>();
        }

        public CDClassInstance CreateClassInstance()
        {
            long NewInstanceID = ConstructNewInstanceUniqueID();
            this.InstanceIDSeed++;

            CDClassInstance Instance = new CDClassInstance(NewInstanceID, this.Attributes);
            this.Instances.Add(Instance);

            return Instance;
        }
        private long ConstructNewInstanceUniqueID()
        {
            int DigitCount = this.InstanceIDSeed.ToString().Length;
            long NewInstanceID = ClassIDPrefix * PowerOf(10, DigitCount) + InstanceIDSeed;

            return NewInstanceID;
        }

        public bool DestroyInstance(long InstanceId)
        {
            bool Result = false;

            int RemovedCount = this.Instances.RemoveAll(x => x.UniqueID == InstanceId);
            if (RemovedCount == 1)
            {
                Result = true;
            }
            else if (RemovedCount > 1)
            {
                throw new Exception("We removed more than 1 instance of class " + this.Name + " with given ID (" + InstanceId.ToString() + "), something must have gone terribly wrong");
            }

            return Result;
        }

        public Boolean AddMethod(CDMethod NewMethod)
        {
            Boolean Result = true;
            foreach (CDMethod Method in this.Methods)
            {
                if (Method.Name == NewMethod.Name)
                {
                    Result = false;
                    break;
                }
            }
            if (Result)
            {
                this.Methods.Add(NewMethod);
            }

            return Result;
        }

        public Boolean AddAttribute(CDAttribute NewAttribute)
        {
            Boolean Result = true;
            foreach (CDAttribute Attribute in this.Attributes)
            {
                if (Attribute.Name == NewAttribute.Name)
                {
                    Result = false;
                    break;
                }
            }
            if (Result)
            {
                this.Attributes.Add(NewAttribute);
            }

            return Result;
        }

        public Boolean MethodExists(String MethodName)
        {
            Boolean Result = false;

            foreach (CDMethod Method in this.Methods)
            {
                if (Method.Name.Equals(MethodName))
                {
                    Result = true;
                    break;
                }
            }

            return Result;
        }

        public int InstanceCount()
        {
            return this.Instances.Count;
        }

        public CDClassInstance GetInstanceByID (long id)
        {
            CDClassInstance Result = null;
            foreach (CDClassInstance ClassInstance in this.Instances)
            {
                if (ClassInstance.UniqueID == id)
                {
                    Result = ClassInstance;
                }
            }
            return Result;
        }

        public CDAttribute GetAttributeByName(String Name)
        {
            CDAttribute Result = null;
            foreach (CDAttribute Attribute in this.Attributes)
            {
                if (String.Equals(Attribute.Name, Name))
                {
                    Result = Attribute;
                    break;
                }
            }
            return Result;
        }
        
        private int PowerOf(int x, int power)
        {
            int result = 1;
            for (int i = 0; i < power; i++)
            {
                result *= x;
            }

            return result;
        }

        public void AppendToInstanceDatabase(Dictionary<String, long> InstanceDatabase)
        {
            foreach (CDClassInstance Instance in this.Instances)
            {
                InstanceDatabase.Add(this.Name, Instance.UniqueID);
            }
        }

    }
}
