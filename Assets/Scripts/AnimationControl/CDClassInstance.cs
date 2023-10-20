using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OALProgramControl
{

    public class CDClassInstance
    {
        public long UniqueID { get; }
        // Attributes
        public Dictionary<string, EXEValueBase> State { get; }
        public readonly CDClass OwningClass;

        public CDClassInstance(long UniqueID, List<CDAttribute> attributes, CDClass owningClass)
        {
           this.State = new Dictionary<string, EXEValueBase>();

            foreach (CDAttribute Attribute in attributes)
            {
                this.State.Add(Attribute.Name, Tuple mu);
            }

            this.UniqueID = UniqueID;
            this.State.Add(EXETypes.UniqueIDAttributeName, new EXEValueInt(UniqueID.ToString()));

            this.OwningClass = owningClass;
        }

        public EXEValueBase GetAttributeValue(String name)
        {
            EXEValueBase Result = null;
            if (this.State.ContainsKey(name))
            {
                Result = this.State[name];
            }

            return Result;
        }
    }
}
