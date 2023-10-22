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

        public EXEASTNodeAccessChain() : base()
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

            if (this.FirstElement == this.LastElement)
            {
                valueContext = valueContext ?? new EXEASTNodeAccessChainContext();
                valueContext.CreateVariableIfItDoesNotExist = true;
            }
            
            return FirstElement.Evaluate(currentScope, currentProgramInstance, valueContext);
        }

        public IEnumerable<EXEASTNodeAccessChainElement> GetElements()
        {
            EXEASTNodeAccessChainElement currentElement = this.FirstElement;

            while (currentElement != null)
            {
                yield return currentElement;

                currentElement = currentElement.NextNode;
            }
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

        public override EXEASTNodeBase Clone()
        {
            EXEASTNodeAccessChain result = new EXEASTNodeAccessChain();

            foreach (EXEASTNodeAccessChainElement chainElement in this.GetElements())
            {
                result.AddElement(chainElement.NodeValue.Clone());
            }

            return result;
        }
    }
}
