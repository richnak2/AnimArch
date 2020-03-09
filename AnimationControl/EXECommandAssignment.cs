using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    class EXECommandAssignment : EXECommand
    {
        private String VariableName { get; }
        private String AttributeName { get; }
        private EXEASTNode AssignedExpression { get; }

        public EXECommandAssignment(String VariableName, EXEASTNode AssignedExpression)
        {
            this.VariableName = VariableName;
            this.AttributeName = null;
            this.AssignedExpression = AssignedExpression;
        }
        public EXECommandAssignment(String VariableName, String AttributeName, EXEASTNode AssignedExpression)
        {
            this.VariableName = VariableName;
            this.AttributeName = AttributeName;
            this.AssignedExpression = AssignedExpression;
        }

        new public Boolean Execute(CDClassPool ExecutionSpace, CDRelationshipPool RelationshipSpace, EXEScope Scope)
        {
            Boolean Result = false;

            String AssignedValue = this.AssignedExpression.Evaluate(Scope, ExecutionSpace);
            // If we are assigning to a variable
            if (this.AttributeName == null)
            {
                EXEPrimitiveVariable Variable = Scope.FindPrimitiveVariableByName(this.VariableName);
                // If the variable doesnt exist, we simply create it
                if (Variable == null)
                {
                    Scope.AddVariable(new EXEPrimitiveVariable(this.VariableName, AssignedValue));
                    Result = true;
                }
                // If the variable exists and is primitive
                else if (!EXETypes.ReferenceTypeName.Equals(Variable.Type))
                {
                    // If the types don't match, this fails and returns false
                    Result = Variable.AssignValue("", AssignedValue);
                }
                // Variable exists and is not primitive. What to do, what to do?
            }
            // We are assigning to an attribute of a variable
            else
            {
                EXEReferenceEvaluator RefEvaluator = new EXEReferenceEvaluator();
                Result = RefEvaluator.SetAttributeValue(this.VariableName, this.AttributeName, Scope, ExecutionSpace, AssignedValue);
            }

            return Result;
        }
    }
}
