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

        public CDClass SpawnClass(String Name, List<String> AttributeNames, List<String> MethodNames)
        {
            long NewClassID = this.GenerateClassID();
            CDClass NewClass = new CDClass(NewClassID, Name, AttributeNames, MethodNames);
            this.ClassPool.Add(NewClass);
            return NewClass;
        }

        public (List<String>, List<String>, List<String>) CreateDefinedMethodsTupple()
        {
            (List<String>, List<String>, List<String>) Tupples = (new List<String>(), new List<String>(), new List<String>());
            foreach (CDClass Class in this.ClassPool)
            {
                foreach (CDMethod Method in Class.Methods)
                {
                    Tupples.Item1.Add(Class.Name);
                    Tupples.Item2.Add(Method.Name);
                    Tupples.Item3.Add(Method.OALCode);
                }
            }
            return Tupples;
        }

        private long GenerateClassID()
        {
            long NewClassID = this.ClassIDPrefix;
            this.ClassIDPrefix++;

            return NewClassID;
        }
    }
}
