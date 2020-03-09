using System;
using System.Collections.Generic;
using System.Text;

namespace AnimationControl
{
    public class EXEScope : EXECommand
    {
        private List<EXEPrimitiveVariable> PrimitiveVariables;
        private List<EXEReferencingVariable> ReferencingVariables;
        private List<EXEReferencingSetVariable> SetReferencingVariables;
        public EXEScope SuperScope;
        public List<EXECommand> Commands;

        public String OALCode;

        public EXEScope()
        {
            this.PrimitiveVariables = new List<EXEPrimitiveVariable>();
            this.ReferencingVariables = new List<EXEReferencingVariable>();
            this.SetReferencingVariables = new List<EXEReferencingSetVariable>();
            this.SuperScope = null;
            this.Commands = new List<EXECommand>();
        }

        public bool AddVariable(EXEPrimitiveVariable Variable)
        {
            bool Result = false;

            if (!VariableNameExists(Variable.Name))
            {
                this.PrimitiveVariables.Add(Variable);
                Result = true;
            }

            return Result;
        }
        public bool AddVariable(EXEReferencingVariable Variable)
        {
            bool Result = false;

            if (!VariableNameExists(Variable.Name))
            {
                this.ReferencingVariables.Add(Variable);
                Result = true;
            }

            return Result;
        }
        public bool AddVariable(EXEReferencingSetVariable Variable)
        {
            bool Result = false;

            if (!VariableNameExists(Variable.Name))
            {
                this.SetReferencingVariables.Add(Variable);
                Result = true;
            }

            return Result;
        }

        public bool VariableNameExists(String VariableName)
        {
            bool Result = false;
            if (FindPrimitiveVariableByName(VariableName) != null)
            {
                Result = true;
            }
            else if (FindReferencingVariableByName(VariableName) != null)
            {
                Result = true;
            }
            else if (FindSetReferencingVariableByName(VariableName) != null)
            {
                Result = true;
            }

            return Result;
        }

        // SetUloh1 - this method is done. Do the same with two similar methods below it
        public EXEPrimitiveVariable FindPrimitiveVariableByName(String Name)
        {
            EXEPrimitiveVariable Result = null;
            EXEScope CurrentScope = this;
            while (CurrentScope != null)
            {
                foreach (EXEPrimitiveVariable CurrentVariable in CurrentScope.PrimitiveVariables)
                {
                    if (CurrentVariable.Name == Name)
                    {
                        Result = CurrentVariable;
                        break;
                    }
                }

                if (Result != null)
                {
                    break;
                }

                CurrentScope = CurrentScope.SuperScope;
            }

            return Result;
        }
        public EXEReferencingVariable FindReferencingVariableByName(String Name)
        {
            throw new NotImplementedException();        
        }
        public EXEReferencingSetVariable FindSetReferencingVariableByName(String Name)
        {
            throw new NotImplementedException();
        }

        public Boolean EvaluateCondition()
        {
            return true;
        }

        public void AddCommand(EXECommand Command)
        {
            this.Commands.Add(Command);
        }

        new public String PrintSelf(Boolean IsTopLevel)
        {
            StringBuilder SelfPrintBuilder = new StringBuilder();
            String Indentation = IsTopLevel ? "" : "\t";


            SelfPrintBuilder.Append(Indentation);
            foreach (EXECommand Command in this.Commands)
            {
                SelfPrintBuilder.Append(Indentation);
                SelfPrintBuilder.Append(Command.PrintSelf(false));
            }

            return SelfPrintBuilder.ToString();
        }

        public bool UnsetReferencingVariables(String ClassName, long InstanceID)
        {
            bool Result = true;
            foreach (EXEReferencingVariable Variable in this.ReferencingVariables)
            {
                if (Variable.Name == ClassName && Variable.ReferencedInstanceId == InstanceID)
                {
                    Variable.ReferencedInstanceId = -1;
                }
            }
            if (this.SuperScope != null)
            {
                Result &= this.SuperScope.UnsetReferencingVariables(ClassName, InstanceID);
            }

            return Result;
        }

        //"Scope" param is ignored here, because this class is a scope
        new public Boolean Execute(CDClassPool ExecutionSpace, CDRelationshipPool RelationshipSpace, EXEScope Scope)
        {
            Boolean Success = false;
            foreach(EXECommand Command in this.Commands)
            {
                Success = Command.Execute(ExecutionSpace, RelationshipSpace, this);
                if (!Success)
                {
                    break;
                }
            }

            return Success;
        }

        new public String GetCode()
        {
            return this.OALCode;
        }

        new public void PrintAST()
        {
            foreach (EXECommand Command in this.Commands)
            {
                Command.PrintAST();
            }
        }
    }
}
