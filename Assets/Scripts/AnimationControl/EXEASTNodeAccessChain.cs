using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace OALProgramControl
{
    public class EXEASTNodeAccessChain : EXEASTNodeBase
    {
        public EXEASTNodeAccessChainElement FirstElement { get; private set; }
        public EXEASTNodeAccessChainElement LastElement { get; private set; }
        public bool CreateVariableIfItDoesNotExist;

        public EXEASTNodeAccessChain()
        {
            this.FirstElement = null;
            this.LastElement = null;
            this.CreateVariableIfItDoesNotExist = false;
        }

        public override EXEExecutionResult Evaluate(EXEScope currentScope, OALProgram currentProgramInstance, EXEASTNodeAccessChainContext valueContext = null)
        {
            if (this.FirstElement == null)
            {
                return EXEExecutionResult.Error("Access chain with no elements.", "XEC2000", null);
            }

            return FirstElement.Evaluate(currentScope, currentProgramInstance);
        }

        public void AddElement(EXEASTNodeBase elementValue)
        {
            EXEASTNodeAccessChainElement element = new EXEASTNodeAccessChainElement(elementValue);

            if (this.FirstElement == null)
            {
                element.PreviousNode = null;
                element.NextNode = null;
                this.FirstElement = element;
                this.LastElement = element;
                return;
            }

            element.PreviousNode = this.LastElement;
            element.NextNode = null;
            this.LastElement.NextNode = element;
        }

        public override void PrintPretty(string indent, bool last)
        {
            throw new System.NotImplementedException();
        }

        public override string ToCode()
        {
            throw new System.NotImplementedException();
        }
    }
}
