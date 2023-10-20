using System;
using System.Collections.Generic;
using System.Linq;

namespace OALProgramControl
{
    public class EXEScope : EXECommand
    {
        protected readonly List<EXEVariable> LocalVariables;
        public List<EXECommand> Commands { get; protected set; }

        public String OALCode;

        public EXEScope()
        {
            this.LocalVariables = new List<EXEVariable>();
            this.SuperScope = null;
            this.Commands = new List<EXECommand>();
        }

        public IEnumerable<EXEScope> ScopesToTop()
        {
            EXEScope currentScope = this;

            while (currentScope != null)
            {
                yield return currentScope;
                currentScope = currentScope.SuperScope;
            }
        }

        public Dictionary<string, string> AllDeclaredVariables()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            PrimitiveVariables.ForEach(variable => result.Add(variable.Name, variable.Type));
            ReferencingVariables.ForEach(variable => result.Add(variable.Name, variable.ClassName));
            SetReferencingVariables.ForEach(variable => result.Add(variable.Name, variable.ClassName));

            return result;
        }

        public EXEScope(EXEScope SuperScope, EXECommand[] Commands)
        {
            this.PrimitiveVariables = new List<EXEPrimitiveVariable>();
            this.ReferencingVariables = new List<EXEReferencingVariable>();
            this.SetReferencingVariables = new List<EXEReferencingSetVariable>();

            this.SetSuperScope(SuperScope);

            this.Commands = new List<EXECommand>();
            foreach (EXECommand Command in Commands)
            {
                this.AddCommand(Command);
            }
        }

        protected override EXEExecutionResult Execute(OALProgram OALProgram)
        {
            AddCommandsToStack(this.Commands);
            return Success();
        }

        protected void AddCommandsToStack(List<EXECommand> Commands)
        {
            Commands.ForEach(command => { command.SetSuperScope(this); command.CommandStack = this.CommandStack; });
            this.CommandStack.Enqueue(Commands);
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

                foreach (EXEReferencingSetVariable CurrentVariable in CurrentScope.SetReferencingVariables)
                {
                    Result[CurrentVariable.Name + "[" + CurrentVariable.ValidVariableCount() + "]"] =
                        CurrentVariable.ClassName;
                }

                CurrentScope = CurrentScope.SuperScope;
            }

            return Result;
        }

        public Dictionary<String, String> GetRefStateAttrsDictRecursive(CDClassPool ExecutionSpace, String VarName)
        {
            Dictionary<String, String> Result = new Dictionary<String, String>();
            EXEReferencingVariable Var = this.FindReferencingVariableByName(VarName);
            if (Var == null)
            {
                return Result;
            }

            CDClassInstance Inst = Var.RetrieveReferencedClassInstance(ExecutionSpace);
            if (Inst == null)
            {
                return Result;
            }

            foreach (var Attribute in Inst.GetStateWithoutID())
            {
                Result[VarName + "." + Attribute.Key] = Attribute.Value;
            }

            return Result;
        }

        public Dictionary<String, String> GetRefStateAttrsDictRecursive(CDClassPool ExecutionSpace)
        {
            Dictionary<String, String> Result = new Dictionary<String, String>();
            EXEScope CurrentScope = this;
            while (CurrentScope != null)
            {
                foreach (EXEReferencingVariable Var in CurrentScope.ReferencingVariables)
                {
                    CDClassInstance Inst = Var.RetrieveReferencedClassInstance(ExecutionSpace);
                    if (Inst == null)
                    {
                        continue;
                    }

                    foreach (var Attribute in Inst.GetStateWithoutID())
                    {
                        Result[Var.Name + "." + Attribute.Key] = Attribute.Value;
                    }
                }

                CurrentScope = CurrentScope.SuperScope;
            }

            return Result;
        }

        public Dictionary<String, String> GetSetRefStateAttrsDictRecursive(CDClassPool ExecutionSpace, String VarName)
        {
            Dictionary<String, String> Result = new Dictionary<String, String>();

            EXEReferencingSetVariable SetVar = this.FindSetReferencingVariableByName(VarName);
            if (SetVar == null)
            {
                return Result;
            }

            int i = 0;
            Console.WriteLine(VarName + " has cardinality " + SetVar.GetReferencingVariables().Count());
            foreach (EXEReferencingVariable Var in SetVar.GetReferencingVariables())
            {
                CDClassInstance Inst = Var.RetrieveReferencedClassInstance(ExecutionSpace);
                if (Inst == null)
                {
                    i++;
                    continue;
                }

                foreach (var Attribute in Inst.GetStateWithoutID())
                {
                    Result[VarName + "[" + i + "]." + Attribute.Key] = Attribute.Value;
                }

                i++;
            }

            return Result;
        }

        public Dictionary<String, String> GetAllHandleStateAttrsDictRecursive(CDClassPool ExecutionSpace)
        {
            Dictionary<String, String> Result = new Dictionary<String, String>();
            Dictionary<String, String> Temp;

            foreach (EXEReferencingVariable Var in this.ReferencingVariables)
            {
                Temp = this.GetRefStateAttrsDictRecursive(ExecutionSpace, Var.Name);
                Temp.ToList().ForEach(x => Result.Add(x.Key, x.Value));
            }

            foreach (EXEReferencingSetVariable Var in this.SetReferencingVariables)
            {
                Temp = this.GetSetRefStateAttrsDictRecursive(ExecutionSpace, Var.Name);
                Temp.ToList().ForEach(x => Result.Add(x.Key, x.Value));
            }

            if (this.SuperScope != null)
            {
                Temp = this.SuperScope.GetAllHandleStateAttrsDictRecursive(ExecutionSpace);
                Temp.ToList().ForEach(x => Result.Add(x.Key, x.Value));
            }

            return Result;
        }

        public EXEExecutionResult AddVariable(EXEVariable variable)
        {
            EXEExecutionResult Result;

            if (!VariableExists(variable.Name))
            {
                this.LocalVariables.Add(variable);
                Result = EXEExecutionResult.Success();
            }
            else
            {
                Result = EXEExecutionResult.Error("XEC1180", ErrorMessage.CreatingExistingVariable(variable.Name));
            }

            return Result;
        }

        public bool VariableExists(String seekedVariableName)
        {
            return FindVariable(seekedVariableName) != null;
        }

        public EXEVariable FindVariable(String seekedVariableName)
        {
            EXEVariable result = null;

            foreach (EXEScope scope in ScopesToTop())
            {
                result = scope.LocalVariables.Where(variable => string.Equals(seekedVariableName, variable.Name)).FirstOrDefault();

                if (result != null)
                {
                    break;
                }
            }

            return result;
        }

        // SetUloh1 - this method is done. Do the same with two similar methods below it

        public void AddCommand(EXECommand Command)
        {
            this.Commands.Add(Command);
            if (Command.IsComposite())
            {
                ((EXEScope) Command).SetSuperScope(this);
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
                SetVariable.UnsetVariables(InstanceID);
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

        public int NonEmptyVariableSetReferencingCountRecursive()
        {
            int Result = 0;
            foreach (EXEReferencingSetVariable Var in this.SetReferencingVariables)
            {
                if (Var.IsNotEmpty())
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

        public List<(String, String)> GetReferencingVariablesByIDRecursive(long ID)
        {
            List<(String, String)> Vars = new List<(String, String)>();
            foreach (EXEReferencingVariable Var in this.ReferencingVariables)
            {
                if (Var.ReferencedInstanceId == ID)
                {
                    Vars.Add((Var.ClassName, Var.Name));
                }
            }

            if (this.SuperScope != null)
            {
                Vars = Vars.Concat(this.SuperScope.GetReferencingVariablesByIDRecursive(ID)).ToList();
            }

            return Vars;
        }

        public void ClearVariables()
        {
            this.PrimitiveVariables.Clear();
            this.ReferencingVariables.Clear();
            this.SetReferencingVariables.Clear();
        }

        public void ClearVariablesRecursive()
        {
            this.ClearVariables();

            foreach (EXECommand Command in this.Commands)
            {
                if (Command is EXEScope)
                {
                    ((EXEScope) Command).ClearVariablesRecursive();
                }
            }
        }

        public String DetermineVariableType(List<String> AccesChain, CDClassPool ClassPool)
        {
            if (AccesChain == null || !AccesChain.Any())
            {
                return null;
            }

            EXEPrimitiveVariable PrimitiveVariable = FindPrimitiveVariableByName(AccesChain[0]);
            if (PrimitiveVariable != null)
            {
                return AccesChain.Count > 1 ? null : PrimitiveVariable.Type;
            }

            EXEReferencingSetVariable SetVariable = FindSetReferencingVariableByName(AccesChain[0]);
            if (SetVariable != null)
            {
                return AccesChain.Count > 1 ? null : SetVariable.Type;
            }

            EXEReferencingVariable ReferencingVariable = FindReferencingVariableByName(AccesChain[0]);
            if (ReferencingVariable == null)
            {
                return null;
            }

            if (AccesChain.Count == 1)
            {
                return ReferencingVariable.ClassName;
            }

            CDClass Class = ClassPool.getClassByName(ReferencingVariable.ClassName);
            if (Class == null)
            {
                return null;
            }

            CDAttribute Attribute = Class.GetAttributeByName(AccesChain[1]);
            if (Attribute == null)
            {
                return null;
            }

            return Attribute.Type;
        }

        public override String ToCode(String Indent = "")
        {
            String Result = "";
            foreach (EXECommand Command in this.Commands)
            {
                Result += Command.ToCode(Indent);
            }

            return Result;
        }

        public override string ToFormattedCode(string Indent = "")
        {
            String Result = "";
            foreach (EXECommand Command in this.Commands)
            {
                Result += Command.ToFormattedCode(Indent);
            }
            return Result;
        }

        public override EXECommand CreateClone()
        {
            EXEScope Clone = CreateDuplicateScope();
            Clone.OALCode = this.OALCode;
            Clone.Commands = this.Commands.Select(x => x.CreateClone()).ToList();

            return Clone;
        }

        protected virtual EXEScope CreateDuplicateScope()
        {
            return new EXEScope();
        }
    }
}