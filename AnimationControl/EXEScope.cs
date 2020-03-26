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

        public Animation Animation;

        public EXEScope()
        {
            this.PrimitiveVariables = new List<EXEPrimitiveVariable>();
            this.ReferencingVariables = new List<EXEReferencingVariable>();
            this.SetReferencingVariables = new List<EXEReferencingSetVariable>();
            this.SuperScope = null;
            this.Commands = new List<EXECommand>();

            this.Animation = null;
        }

        public Dictionary<String, String> GetStateDictRecursive()
        {
            Dictionary<String, String> Result = new Dictionary<String, String>();
            EXEScope CurrentScope = this;
            while (CurrentScope != null)
            {
                foreach (EXEPrimitiveVariable CurrentVariable in CurrentScope.PrimitiveVariables)
                {
                    Result[CurrentVariable.Name] = CurrentVariable.Value;
                }

                CurrentScope = CurrentScope.SuperScope;
            }

            return Result;
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
        public EXEReferenceHandle FindReferenceHandleByName(String Name)
        {
            EXEReferenceHandle Result = FindReferencingVariableByName(Name);
            if (Result == null)
            {
                Result = FindSetReferencingVariableByName(Name);
            }
            return Result;
        }
        public EXEReferencingVariable FindReferencingVariableByName(String Name)
        {
            EXEReferencingVariable Result = null;
            EXEScope CurrentScope = this;

            while (CurrentScope != null) {
                foreach (EXEReferencingVariable CurrentVariable in CurrentScope.ReferencingVariables)
                {
                    if (String.Equals(CurrentVariable.Name, Name)){
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
        public EXEReferencingSetVariable FindSetReferencingVariableByName(String Name)
        {

            EXEScope CurrentScope = this;

            while (CurrentScope != null)
            {

                foreach (EXEReferencingSetVariable ReferencingSetVariable in CurrentScope.SetReferencingVariables)
                {
                    if (String.Equals(ReferencingSetVariable.Name, Name))
                    {
                        return ReferencingSetVariable;
                    }
                }
                CurrentScope = CurrentScope.SuperScope;
            }
            return null;
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
                if (Variable.ClassName == ClassName && Variable.ReferencedInstanceId == InstanceID)
                {
                    Variable.ReferencedInstanceId = -1;
                }
            }
            foreach (EXEReferencingSetVariable SetVariable in this.SetReferencingVariables)
            {
                if (SetVariable.ClassName != ClassName)
                {
                    continue;
                }

                foreach (EXEReferencingVariable Variable in SetVariable.GetReferencingVariables())
                {
                    if (Variable.ReferencedInstanceId == InstanceID)
                    {
                        Variable.ReferencedInstanceId = -1;
                    }
                }
            }
            if (this.SuperScope != null)
            {
                Result &= this.SuperScope.UnsetReferencingVariables(ClassName, InstanceID);
            }

            return Result;
        }

        public int VariablePrimitiveCount()
        {
            return this.PrimitiveVariables.Count;
        }
        public int ValidVariableReferencingCountRecursive()
        {
            int Result = 0;
            foreach (EXEReferencingVariable Var in this.ReferencingVariables)
            {
                if (Var.IsInitialized())
                {
                    ++Result;
                }
            }
            if (this.SuperScope != null)
            {
                Result += this.SuperScope.ValidVariableReferencingCountRecursive();
            }
            return Result;
        }
        public int VariableReferencingCount()
        {
            return this.ReferencingVariables.Count;
        }
        public int VariableSetReferencingCount()
        {
            return this.SetReferencingVariables.Count;
        }

        public Boolean DestroyReferencingVariable(String VariableName)
        {
            Boolean Result = false;

            int i = 0;
            foreach (EXEReferencingVariable CurrentVariable in this.ReferencingVariables)
            {
                if (String.Equals(CurrentVariable.Name, VariableName))
                {
                    this.ReferencingVariables.RemoveAt(i);
                    Result = true;
                    break;
                }

                i++;
            }

            return Result;
        }

        //"Scope" param is ignored here, because this class is a scope
        new public Boolean Execute(Animation Animation, EXEScope Scope)
        {
            this.Animation = Animation;

            Boolean Success = false;
            foreach(EXECommand Command in this.Commands)
            {
                Success = Command.SynchronizedExecute(Animation, this);
                if (!Success)
                {
                    break;
                }
            }

            return Success;
        }
        new public Boolean SynchronizedExecute(Animation Animation, EXEScope Scope)
        {
            Boolean Success = this.Execute(Animation, Scope);
            return Success;
        }
        public void RequestNextStep()
        {
            this.Animation.RequestNextStep();
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
