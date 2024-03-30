﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace OALProgramControl
{
    public class EXEScope : EXECommand
    {
        protected readonly List<EXEVariable> LocalVariables;
        public List<EXEVariable> Variables
        {
            get
            {
                return this.LocalVariables.Select(x => x).ToList();
            }
        }
        public List<EXECommand> Commands { get; protected set; }

        public String OALCode;

        public EXEScope() : base()
        {
            this.LocalVariables = new List<EXEVariable>();
            this.Commands = new List<EXECommand>();
        }

        public EXEScope(EXEScope SuperScope, EXECommand[] Commands)
        {
            this.LocalVariables = new List<EXEVariable>();

            this.SetSuperScope(SuperScope);

            this.Commands = new List<EXECommand>();
            foreach (EXECommand Command in Commands)
            {
                this.AddCommand(Command);
            }
        }

        public override IEnumerable<EXEScope> ScopesToTop()
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

            this.LocalVariables.ForEach(variable => result.Add(variable.Name, variable.Value.TypeName));

            return result;
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

        public virtual EXEExecutionResult AddVariable(EXEVariable variable)
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

        public void AddCommand(EXECommand Command)
        {
            this.Commands.Add(Command);
            Command.SetSuperScope(this);
        }

        public override Boolean IsComposite()
        {
            return true;
        }

        public void ClearVariables()
        {
            this.LocalVariables.Clear();
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

        public override void Accept(Visitor v)
        {
            v.VisitExeScope(this);
        }

        public override EXECommand CreateClone()
        {
            EXEScope Clone = CreateDuplicateScope();
            
            Clone.OALCode = this.OALCode;
            Clone.CommandStack = this.CommandStack;

            foreach (EXECommand command in this.Commands.Select(command => command.CreateClone()))
            {
                Clone.AddCommand(command);
            }

            return Clone;
        }

        protected virtual EXEScope CreateDuplicateScope()
        {
            return new EXEScope();
        }
    }
}