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
        public List<EXEClassHandle> ClassVariables;
        public EXEScope SuperScope;
        public List<EXECommandInterface> Commands;

        public String OALCode { get; set; }
        public String ScopeType { get; set; }

        // "Method" Specific
        public String OwningClassName { get; set; }
        public String OwningInstanceName { get; set; }
        public String MethodName { get; set; }

        // "If" and "While" specific
        public String ConditionOAL { get; set; }

        // "For each" specific
        public EXEClassHandle Iterator { get; set; }
        public List<EXEClassHandle> Iterable { get; set; }
        public String IteratorName { get; set; }
        public string IterableName { get; set; }

        //
        public const String ScopeTypeNameMethod = "method";
        public const String ScopeTypeNameGlobal = "global";
        public const String ScopeTypeNameIf = "if";
        public const String ScopeTypeNameElif = "elif";
        public const String ScopeTypeNameElse = "else";
        public const String ScopeTypeNameWhile = "while";
        public const String ScopeTypeNameForeach = "foreach";

        private static Dictionary<String, String> ControlEndings = new Dictionary<String, String>();

        //Add constructors specific for each scope type
        public EXEScope()
        {
            this.ScopeType = "";
            this.PrimitiveVariables = new List<EXEPrimitiveVariable>();
            this.ClassVariables = new List<EXEClassHandle>();
            this.SuperScope = null;
            this.Commands = new List<EXECommandInterface>();

            this.OwningClassName = "";
            this.OwningInstanceName = "";
            this.MethodName = "";

            this.ConditionOAL = "";

            this.Iterator = null;
            this.IteratorName = "";
            this.Iterable = null;
            this.IterableName = "";
        }
        public EXEScope(String ScopeType)
        {
            this.ScopeType = ScopeType;
            this.PrimitiveVariables = new List<EXEPrimitiveVariable>();
            this.ClassVariables = new List<EXEClassHandle>();
            this.SuperScope = null;
            this.Commands = new List<EXECommandInterface>();

            this.OwningClassName = "";
            this.OwningInstanceName = "";
            this.MethodName = "";

            this.ConditionOAL = "";

            this.Iterator = null;
            this.IteratorName = "";
            this.Iterable = null;
            this.IterableName = "";
        }
        private void FillControlEndingsDict() {
            if (ControlEndings != null)
            {
                return;
            }

            ControlEndings.Add(ScopeTypeNameIf, "endif");
            ControlEndings.Add(ScopeTypeNameWhile, "endwhile");
            ControlEndings.Add(ScopeTypeNameForeach, "endfor");
        }
        public void AddCommand(EXECommandInterface Command)
        {
            this.Commands.Add(Command);
        }

        public Boolean IsMyEnding(String OALCode)
        {
            Boolean Result = false;
            if (ControlEndings.ContainsKey(this.ScopeType))
            {
                Result = this.ScopeType.Equals(OALCode);
            }
            return Result;
        }
        public String PrintSelf(Boolean IsTopLevel)
        {
            StringBuilder SelfPrintBuilder = new StringBuilder();
            String Indentation = IsTopLevel ? "" : "\t";


            SelfPrintBuilder.Append(Indentation);
            if (this.ConditionOAL != "")
            {
                SelfPrintBuilder.Append("\nif/for ");
                SelfPrintBuilder.Append(this.ConditionOAL);
                SelfPrintBuilder.Append("CONDITIONEND\n");
            }
            else if (this.IterableName != "")
            {
                SelfPrintBuilder.Append("for ");
                SelfPrintBuilder.Append(this.IteratorName);
                SelfPrintBuilder.Append(" in ");
                SelfPrintBuilder.Append(this.IterableName);
                SelfPrintBuilder.Append("\n");
            }
            foreach (EXECommandInterface Command in this.Commands)
            {
                SelfPrintBuilder.Append(Indentation);
                SelfPrintBuilder.Append(Command.PrintSelf(false));
            }

            return SelfPrintBuilder.ToString();
        }

        public Boolean Execute(OALAnimationRepresentation ExecutionSpace, EXEScope Scope)
        {
            return false;
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
