using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    public class EXEScope : EXECommandInterface
    {
        public List<EXEPrimitiveVariable> PrimitiveVariables;
        public List<EXEReferencingVariable> ReferencingVariables;
        public List<EXEReferencingSetVariable> SetReferencingVariables;
        public EXEScope SuperScope;
        public List<EXECommandInterface> Commands;

        public String OALCode;

        public EXEScope()
        {
            this.PrimitiveVariables = new List<EXEPrimitiveVariable>();
            this.ReferencingVariables = new List<EXEReferencingVariable>();
            this.SetReferencingVariables = new List<EXEReferencingSetVariable>();
            this.SuperScope = null;
            this.Commands = new List<EXECommandInterface>();
        }

        public Boolean EvaluateCondition()
        {
            return true;
        }

        public void AddCommand(EXECommandInterface Command)
        {
            this.Commands.Add(Command);
        }

        public String PrintSelf(Boolean IsTopLevel)
        {
            StringBuilder SelfPrintBuilder = new StringBuilder();
            String Indentation = IsTopLevel ? "" : "\t";


            SelfPrintBuilder.Append(Indentation);
            foreach (EXECommandInterface Command in this.Commands)
            {
                SelfPrintBuilder.Append(Indentation);
                SelfPrintBuilder.Append(Command.PrintSelf(false));
            }

            return SelfPrintBuilder.ToString();
        }

        public Boolean Execute(OALAnimationRepresentation ExecutionSpace, EXEScope Scope)
        {
            Scope = this;

            throw new NotImplementedException();
        }

        public String GetCode()
        {
            return this.OALCode;
        }

        public void Parse(EXEScope SuperScope)
        {
            Console.WriteLine("ExeScope.Parse");
            OALParser Parser = new OALParser();
            Parser.DecomposeOALFragment(this);
            this.SuperScope = SuperScope;

            foreach (EXECommandInterface Command in this.Commands)
            {
                Command.Parse(this);
            }
        }

        public void PrintAST()
        {
            foreach (EXECommandInterface Command in this.Commands)
            {
                Command.PrintAST();
            }
        }
    }
}
