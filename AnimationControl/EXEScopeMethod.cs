using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    public class EXEScopeMethod : EXEScope
    {
        public static String ScopeTypeName = "method";

        public EXEScopeMethod(String OwningClassName, String OwningInstanceName, String MethodName) : base(ScopeTypeName)
        {
            this.OwningClassName = OwningClassName;
            this.OwningInstanceName = OwningInstanceName;
            this.MethodName = MethodName;
        }
    }
}
