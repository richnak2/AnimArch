using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    public class EXEClassHandle
    {
        public String Name { get; set; }
        public CDClass Class { get; set; }

        public EXEClassHandle(String Name, CDClass Class)
        {
            this.Name = Name;
            this.Class = Class;
        }
    }
}
