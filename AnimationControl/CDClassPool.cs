using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    public class CDClassPool
    {
        private long ClassIDPrefix;
        public List<CDClass> ClassPool { get; }

        public CDClassPool()
        {
            this.ClassIDPrefix = 1000;
            this.ClassPool = new List<CDClass>();
        }

        // SetUloh2
        public CDClassInstance GetClassInstanceById(String ClassName, long Id)
        {
            throw new NotImplementedException();
        }
        public CDClass SpawnClass(String Name)
        {
            long NewClassID = this.GenerateClassID();
            CDClass NewClass = new CDClass(NewClassID, Name);
            this.ClassPool.Add(NewClass);
            return NewClass;
        }

        public Boolean ClassesExist(List<String> ClassNames)
        {
            Boolean Result = true;

            foreach (String ClassName in ClassNames)
            {
                if (!ClassExists(ClassName))
                {
                    Result = false;
                }
            }

            return Result;
        }

        public Boolean ClassExists(String ClassName)
        {
            Boolean Result = false;

            foreach (CDClass Class in this.ClassPool)
            {
                if (Class.Name.Equals(ClassName))
                {
                    Result = true;
                    break;
                }
            }

            return Result;
        }

        public CDClass getClassByName(String ClassName)
        {
            CDClass Result = null;

            foreach (CDClass Class in this.ClassPool)
            {
                if (Class.Name.Equals(ClassName))
                {
                    Result = Class;
                    break;
                }
            }

            return Result;
        }

        public Boolean MethodExists(String ClassName, String MethodName)
        {
            if (!ClassExists(ClassName))
            {
                return false;
            }

            CDClass SearchedClass = null;

            foreach (CDClass Class in this.ClassPool)
            {
                if (Class.Name.Equals(ClassName))
                {
                    SearchedClass = Class;
                    break;
                }
            }

            if (SearchedClass == null)
            {
                return false;
            }

            return SearchedClass.MethodExists(MethodName);
        }

        private long GenerateClassID()
        {
            long NewClassID = this.ClassIDPrefix;
            this.ClassIDPrefix++;

            return NewClassID;
        }

        public bool DestroyInstance(String ClassName, long InstanceId)
        {
            bool Result = false;
            CDClass Class = this.getClassByName(ClassName);
            if (Class != null)
            {
                Result = Class.DestroyInstance(InstanceId);
            }

            return Result;
        }
    }
}