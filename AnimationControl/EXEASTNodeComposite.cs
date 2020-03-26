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
        public String Evaluate(EXEScope Scope, CDClassPool ExecutionSpace)
        {
            String Result = null;
            EXEExpressionEvaluator Evaluator = new EXEExpressionEvaluator();
            EXEEvaluatorHandleOperators HandleEvaluator = new EXEEvaluatorHandleOperators();
            EXEReferenceEvaluator AccessEvaluator = new EXEReferenceEvaluator();

            // If we just calculate with ints, reals, bools, strings
            if (Evaluator.IsSimpleOperator(this.Operation))
            {
                List<String> EvaluatedOperands = new List<String>();
                foreach (EXEASTNode Operand in this.Operands)
                {
                    EvaluatedOperands.Add(Operand.Evaluate(Scope, ExecutionSpace));
                }

                //If we are returning real number, let's format it so that we don't have trouble with precision
                Result = Evaluator.Evaluate(this.Operation, EvaluatedOperands);
                Console.WriteLine(this.Operation);
                Console.WriteLine(Result);
                if (EXETypes.RealTypeName.Equals(EXETypes.DetermineVariableType("", Result)))
                {
                    Result = FormatDouble(Result);
                }
            }
            // If we have handle operators
            else if (HandleEvaluator.IsHandleOperator(this.Operation))
            {
                Result = HandleEvaluator.Evaluate(this.Operation, this.Operands.Select(x => ((EXEASTNodeLeaf)x).GetNodeValue()).ToList(), Scope);
            }
            // If we have access operator - we either access attribute or have decimal number. There are always 2 operands
            else if (".".Equals(this.Operation) && this.Operands.Count == 2)
            {
                if (EXETypes.IntegerTypeName.Equals(EXETypes.DetermineVariableType("", this.Operands[0].GetNodeValue()))
                    && EXETypes.IntegerTypeName.Equals(EXETypes.DetermineVariableType("", this.Operands[1].GetNodeValue()))
                )
                {
                    Result = this.Operands[0].GetNodeValue() + "." + this.Operands[1].GetNodeValue();
                }
                else if (EXETypes.ReferenceTypeName.Equals(EXETypes.DetermineVariableType("", this.Operands[0].GetNodeValue()))
                    && EXETypes.ReferenceTypeName.Equals(EXETypes.DetermineVariableType("", this.Operands[1].GetNodeValue()))
                )
                {
                    Result = AccessEvaluator.EvaluateAttributeValue(this.Operands[0].GetNodeValue(), this.Operands[1].GetNodeValue(), Scope, ExecutionSpace);
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
