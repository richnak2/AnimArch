using System;
using System.Collections.Generic;
using System.Text;

namespace AnimationControl
{
    public class EXEScope : EXECommand
    {
        public List<EXEPrimitiveVariable> PrimitiveVariables;
        public List<EXEReferencingVariable> ReferencingVariables;
        public List<EXEReferencingSetVariable> SetReferencingVariables;
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
