using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    class EXEScopeForEach : EXEScope
    {
        public EXEReferencingVariable Iterator { get; set; }
        public EXEReferencingSetVariable Iterable { get; set; }
        public String IteratorName { get; set; }
        public string IterableName { get; set; }


    }
}
