using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    public class CDAttribute
    {
        public String name { get; }
        public String type { get; }
        public Boolean IsMockedByCompiler { get; }
        public CDAttribute(String name, String type)
        {
            this.name = name;
            this.type = type;
            this.IsMockedByCompiler = false;
        }
        public CDAttribute(String name, String type, Boolean IsMockedByCompiler)
        {
            this.name = name;
            this.type = type;
            this.IsMockedByCompiler = IsMockedByCompiler;
        }
    }
}
