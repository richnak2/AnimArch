using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    public class EXECommand : EXECommandInterface
    {
        public String OALCode { get; set; }
        public String CommandType { get; set; }

        public EXECommand(String OALCode)
        {
            this.OALCode = OALCode;
        }
        public String PrintSelf(Boolean IsTopLevel)
        {
            return this.OALCode;
        }
    }
}
