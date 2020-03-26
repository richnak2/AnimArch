using System;

namespace AnimationControl
{
    public class EXEASTNodeLeaf : EXEASTNode
    {
        private String Value { get; }

        public EXEASTNodeLeaf(String Value)
        {
            this.Value = Value;
        }

        public String GetNodeValue()
        {
            return this.Value;
        }
        public String Evaluate(EXEScope Scope, CDClassPool ExecutionSpace)
        {
            String Result = null;
            // If we have simple value, e.g. 5, 3.14, "hi Madelyn", we are good
            if (!EXETypes.ReferenceTypeName.Equals(EXETypes.DetermineVariableType("", this.Value)))
            {
                Result = this.Value;
            }
            // We got here because we have a variable name, the variable is of primitive value
            else if(EXETypes.ReferenceTypeName.Equals(EXETypes.DetermineVariableType("", this.Value)))
            {
                EXEPrimitiveVariable ThisVariable = Scope.FindPrimitiveVariableByName(this.Value);
                if(ThisVariable != null)
                {
                    Result = ThisVariable.Value;
                }
            }

            Console.WriteLine("Operand: " + this.Value);
            Console.WriteLine("Result: " + Result);

            return Result;
        }

        //https://stackoverflow.com/questions/1649027/how-do-i-print-out-a-tree-structure
        public void PrintPretty(string indent, bool last)
        {
            Console.Write(indent);
            if (last)
            {
                Console.Write("\\-");
                indent += "  ";
            }
            else
            {
                Console.Write("|-");
                indent += "| ";
            }
            Console.WriteLine(this.Value);
        }
    }
}
