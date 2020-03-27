using System;
using System.Collections.Generic;
using System.Linq;

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

        public Dictionary<String, String> GetRefStateDictRecursive()
        {
            Dictionary<String, String> Result = new Dictionary<String, String>();
            EXEScope CurrentScope = this;
            while (CurrentScope != null)
            {
                foreach (EXEReferencingVariable CurrentVariable in CurrentScope.ReferencingVariables)
                {
                    Result[CurrentVariable.Name] = CurrentVariable.ClassName;
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
            if (Command.IsComposite())
            {
                ((EXEScope)Command).SuperScope = this;
            }
        }
        public override Boolean IsComposite()
        {
            return true;
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
        public override Boolean Execute(Animation Animation, EXEScope Scope)
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
        public override Boolean SynchronizedExecute(Animation Animation, EXEScope Scope)
        {
            Boolean Success = this.Execute(Animation, Scope);
            return Success;
        }

        public List<(String, String)> GetReferencingVariablesByIDRecursive(long ID)
        {
            List<(String, String)> Vars = new List<(String, String)>();
            foreach (EXEReferencingVariable Var in this.ReferencingVariables)
            {
                if (Var.ReferencedInstanceId == ID)
                {
                    Vars.Add((Var.ClassName ,Var.Name));
                }
            }
            if (this.SuperScope != null)
            {
                Vars = Vars.Concat(this.SuperScope.GetReferencingVariablesByIDRecursive(ID)).ToList();
            }
            return Vars;
        }
    }
}
