using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    public class CDAttribute
    {
        public String name { get; set; }
        public String type { get; set; }

        public CDAttribute(String name, String type)
        {
            this.name = name;
            this.type = type;
        }
    }
}
