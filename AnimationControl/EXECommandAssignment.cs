﻿using System;

namespace AnimationControl
{
    public class EXECommandAssignment : EXECommand
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

        public override Boolean Execute(Animation Animation, EXEScope Scope)
        {
            Boolean Result = false;

            String AssignedValue = this.AssignedExpression.Evaluate(Scope, Animation.ExecutionSpace);
            Console.WriteLine("Assigning value: " + AssignedValue);
            // If we are assigning to a variable
            if (this.AttributeName == null)
            {
                EXEPrimitiveVariable Variable = Scope.FindPrimitiveVariableByName(this.VariableName);
                // If the variable doesnt exist, we simply create it
                if (Variable == null)
                {
                    Console.WriteLine("Creating new var" + this.VariableName);
                    Result = Scope.AddVariable(new EXEPrimitiveVariable(this.VariableName, AssignedValue));
                }
                //If variable exists and its type is UNDEFINED
                else if (EXETypes.UnitializedName.Equals(Variable.Type))
                {
                    Console.WriteLine("Assigning to unitialized var" + this.VariableName);
                    Result = Variable.AssignValue(Variable.Name, AssignedValue);
                }
                // If the variable exists and is primitive
                else if (!EXETypes.ReferenceTypeName.Equals(Variable.Type))
                {
                    Console.WriteLine("Assigning to initialized var" + this.VariableName);
                    // If the types don't match, this fails and returns false
                    Result = Variable.AssignValue("", AssignedValue);
                }
                // Variable exists and is not primitive. What to do, what to do?
                // We do nothing, we CANNOT ASIGN TO HANDLES!!!
            }
            // We are assigning to an attribute of a variable
            else
            {
                EXEReferenceEvaluator RefEvaluator = new EXEReferenceEvaluator();
                Result = RefEvaluator.SetAttributeValue(this.VariableName, this.AttributeName, Scope, Animation.ExecutionSpace, AssignedValue);
            }

            return Result;
        }
    }
}
