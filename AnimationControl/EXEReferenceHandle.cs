using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    public class EXEReferenceHandle
    {
        public String Name { get; }
        public String ClassName { get; }
        public EXEReferenceHandle(String Name, String ClassName)
        {
            this.Name = Name;
            this.ClassName = ClassName;
        }
        public List<long> GetReferencedIds()
        {
            return new List<long>();
        }
    }
}
