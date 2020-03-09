using System;
using System.Collections.Generic;
using System.Globalization;
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
            if (Operand == null)
            {
                return;
            }

            this.Operands.Add(Operand);
        }

        public String GetNodeValue()
        {
            return this.Operation;
        }
        public String Evaluate(EXEScope Scope)
        {
            String Result = null;
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();

            // If we just calculate with ints, reals, bools, strings
            if (Evaluator.IsSimpleOperator(this.Operation))
            {
                List<String> EvaluatedOperands = new List<String>();
                foreach (EXEASTNode Operand in this.Operands)
                {
                    EvaluatedOperands.Add(Operand.Evaluate(Scope));
                }

                Result = Evaluator.Evaluate(this.Operation, EvaluatedOperands);
                if (EXETypes.RealTypeName.Equals(EXETypes.DetermineVariableType("", Result)))
                {
                    Result = FormatDouble(Result);//FormatDouble(Result);
                }
            }
            

            return Result;
        }

        public void PrintPretty(string indent, bool last)
        {
            Console.Write(indent);
            if (last)
            {
                Console.Write("\\+");
                indent += "  ";
            }
            else
            {
                Console.Write("|+");
                indent += "| ";
            }
            Console.WriteLine(this.Operation);

            for (int i = 0; i < this.Operands.Count; i++)
                this.Operands[i].PrintPretty(indent, i == this.Operands.Count - 1);
        }

        private String FormatDouble(String Double)
        {
            String Temp = decimal.Parse(Double).ToString("G29");
            String Result = "";
            for (int i = 0; i < Temp.Length; i++)
            {
                Result += Temp[i] == ',' ? '.' : Temp[i];
            }

            return Result;
        }
    }
}
