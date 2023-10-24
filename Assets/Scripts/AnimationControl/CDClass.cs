using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OALProgramControl
{
    public class CDClass
    {
        public string Name { get; set; }
        public List<CDAttribute> Attributes { get; }
        public List<CDMethod> Methods { get; }
        public List<CDClassInstance> Instances { get; }
        private CDClass _SuperClass { get; set; }
        public CDClass SuperClass
        {
            get { return _SuperClass; }
            set
            {
                _SuperClass = value;
                if (_SuperClass != null)
                {
                    SuperClass.SubClasses.Add(this);
                }
            }
        }
        public List<CDClass> SubClasses { get; }
        public readonly CDClassPool OwningClassPool;

        public CDClass(String Name, CDClassPool owningClassPool)
        {
            this.Name = Name;

            this.Attributes = new List<CDAttribute>();

            this.Methods = new List<CDMethod>();

            this.Instances = new List<CDClassInstance>();

            this.SuperClass = null;

            this.SubClasses = new List<CDClass>();

            this.OwningClassPool = owningClassPool;
        }

        public CDClassInstance CreateClassInstance()
        {
            long NewInstanceID = EXEInstanceIDSeed.GetInstance().GenerateID();

            CDClassInstance Instance = new CDClassInstance(NewInstanceID, this.Attributes, this);
            this.Instances.Add(Instance);

            return Instance;
        }

        public bool DestroyInstance(long InstanceId)
        {
            bool Result = false;

            foreach (CDClassInstance classInstance in this.Instances.Where(instance => instance.UniqueID == InstanceId))
            {
                classInstance.Destroy();
            }

            int RemovedCount = this.Instances.RemoveAll(x => x.UniqueID == InstanceId);
            if (RemovedCount == 1)
            {
                Result = true;
            }
            else if (RemovedCount > 1)
            {
                throw new Exception("We removed more than 1 instance of class " + this.Name + " with given ID (" +
                                    InstanceId.ToString() + "), something must have gone terribly wrong");
            }
            else if (RemovedCount == 0)
            {
                throw new Exception("We removed 0 instances of class " + this.Name + " with given ID (" +
                                    InstanceId.ToString() + "), something must have gone terribly wrong");
            }

            return Result;
        }

        public Boolean AddMethod(CDMethod newMethod)
        {
            if (GetAttributeByName(newMethod.Name) != null)
            {
                return false;
            }

            if (MethodExists(newMethod.Name))
            {
                return false;
            }

            this.Methods.Add(newMethod);

            return true;
        }

        public Boolean AddAttribute(CDAttribute NewAttribute)
        {
            if (GetAttributeByName(NewAttribute.Name) != null)
            {
                return false;
            }

            if (MethodExists(NewAttribute.Name))
            {
                return false;
            }

            this.Attributes.Add(NewAttribute);

            return true;
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

        public CDMethod GetMethodByName(String MethodName)
        {
            CDMethod Result = null;

            foreach (CDMethod Method in this.Methods)
            {
                if (Method.Name.Equals(MethodName))
                {
                    Result = Method;
                    break;
                }
            }

            return Result;
        }

        public int InstanceCount()
        {
            return this.Instances.Count;
        }

        public CDClassInstance GetInstanceByID(long id)
        {
            CDClassInstance Result = null;
            foreach (CDClassInstance ClassInstance in this.Instances)
            {
                if (ClassInstance.UniqueID == id)
                {
                    Result = ClassInstance;
                    break;
                }
            }

            return Result;
        }

        public CDClassInstance GetInstanceByIDRecursiveDownward(long id)
        {
            CDClassInstance Result = GetInstanceByID(id);

            if (Result != null)
            {
                return Result;
            }

            foreach (CDClass SubClass in this.SubClasses)
            {
                Result = SubClass.GetInstanceByIDRecursiveDownward(id);

                if (Result != null)
                {
                    return Result;
                }
            }

            return null;
        }

        public CDClass GetInstanceClassByIDRecursiveDownward(long id)
        {
            CDClassInstance Instance = GetInstanceByID(id);

            if (Instance != null)
            {
                return this;
            }

            CDClass Result = null;

            foreach (CDClass SubClass in this.SubClasses)
            {
                Result = SubClass.GetInstanceClassByIDRecursiveDownward(id);

                if (Result != null)
                {
                    return Result;
                }
            }

            return null;
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

        public bool CanBeAssignedTo(CDClass TargetClass)
        {
            if (TargetClass == null)
            {
                return false;
            }

            CDClass CurrentClass = this;

            while (CurrentClass != null)
            {
                if (CurrentClass == TargetClass)
                {
                    return true;
                }

                CurrentClass = CurrentClass.SuperClass;
            }

            return false;
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

        public List<long> GetAllInstanceIDs()
        {
            return this.Instances.Select(x => x.UniqueID).ToList();
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