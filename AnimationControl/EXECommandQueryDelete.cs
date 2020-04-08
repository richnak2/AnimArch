using System;

namespace AnimationControl
{
    public class EXECommandQueryDelete : EXECommand
    {
        public String VariableName { get; }

        public EXECommandQueryDelete(String VariableName)
        {
            this.VariableName = VariableName;
        }

        public override bool Execute(Animation Animation, EXEScope Scope)
        {
            bool Result = false;
            EXEReferencingVariable Variable = Scope.FindReferencingVariableByName(this.VariableName);
            if (Variable != null)
            {
                bool DestructionSuccess = Animation.ExecutionSpace.DestroyInstance(Variable.ClassName, Variable.ReferencedInstanceId);
                if(DestructionSuccess)
                {
                    Result = Scope.UnsetReferencingVariables(Variable.ClassName, Variable.ReferencedInstanceId);
                }
            }
            return Result;
        }
        public override string ToCodeSimple()
        {
            return "delete object instance " + this.VariableName;
        }
    }
}
