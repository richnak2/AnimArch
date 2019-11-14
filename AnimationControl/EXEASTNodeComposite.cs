using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    public class EXEASTNodeComposite : EXEASTNode
    {
        public String Operation { get; set; }
        public List<EXEASTNode> Operands { get; }

        public EXEASTNodeComposite(String Operation)
        {
            this.Operation = Operation;
            this.Operands = new List<EXEASTNode>();
        }

        public void AddOperand(EXEASTNode Operand)
        {
            this.Operands.Add(Operand);
        }

        public new String Evaluate(EXEScope Scope)
        {
            //TODO
            return null;
        }
    }
}
