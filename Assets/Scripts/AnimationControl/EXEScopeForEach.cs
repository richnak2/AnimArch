using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OALProgramControl
{
    public class EXEScopeForEach : EXEScope
    {
        public String IteratorName { get; set; }
        public String IterableName { get; set; }
        public String IterableAttributeName { get; set; }
        private int IterableIndex;

        public EXEScopeForEach(String Iterator, String Iterable, String IterableAttribute) : base()
        {
            this.IteratorName = Iterator;
            this.IterableName = Iterable;
            this.IterableAttributeName = IterableAttribute;
            int IterableIndex = 0;
        }

        public EXEScopeForEach(EXEScope SuperScope, EXECommand[] Commands, String Iterator, String Iterable,
            String IterableAttribute) : base(SuperScope, Commands)
        {
            this.IteratorName = Iterator;
            this.IterableName = Iterable;
            this.IterableAttributeName = IterableAttribute;
            int IterableIndex = 0;
        }

        protected override EXEExecutionResult Execute(OALProgram OALProgram)
        {
            EXEReferencingVariable IteratorVariable = this.FindReferencingVariableByName(this.IteratorName);
            EXEReferencingSetVariable IterableVariable = this.FindSetReferencingVariableByName(this.IterableName);

            String IterableVariableClassName = "";
            int ReferencingVariablesCount = 0;
            List<long> ReferencingVariablesIDs = new List<long>(); // This is important if we have IterableAttributeName

            if (this.IterableAttributeName == null)
            {
                // We cannot iterate over not existing reference set
                if (IterableVariable == null)
                {
                    return Error("XEC1150", ErrorMessage.VariableNotFound(this.IterableName, this.SuperScope));
                }

                IterableVariableClassName = IterableVariable.ClassName;
                ReferencingVariablesCount = IterableVariable.GetReferencingVariables().Count;
            }
            else
            {
                // If we have IterableAttributeName, IterableName must be reference and not reference set 
                EXEReferencingVariable Variable = SuperScope.FindReferencingVariableByName(this.IterableName);
                if (Variable == null)
                {
                    return Error("XEC1151", ErrorMessage.VariableNotFound(this.IterableName, this.SuperScope));
                }

                CDClass VariableClass = OALProgram.ExecutionSpace.getClassByName(Variable.ClassName);
                if (VariableClass == null)
                {
                    return Error("XEC1152", ErrorMessage.ClassNotFound(Variable.ClassName, OALProgram));
                }

                CDAttribute Attribute = VariableClass.GetAttributeByName(this.IterableAttributeName);
                if (Attribute == null)
                {
                    return Error("XEC1153", ErrorMessage.AttributeNotFoundOnClass(this.IterableAttributeName, VariableClass));
                }

                // We cannot iterate over reference that is not a set
                if (!"[]".Equals(Attribute.Type.Substring(Attribute.Type.Length - 2, 2)))
                {
                    return Error("XEC1154", ErrorMessage.IsNotIterable(Attribute.Type));
                }

                IterableVariableClassName = Attribute.Type.Substring(0, Attribute.Type.Length - 2);

                // Get instance representing IterableName
                CDClassInstance ClassInstance = VariableClass.GetInstanceByID(Variable.ReferencedInstanceId);
                if (ClassInstance == null)
                {
                    return Error("XEC1155", ErrorMessage.InstanceNotFound(Variable.ReferencedInstanceId, VariableClass));
                }

                String Values = ClassInstance.GetAttributeValue(this.IterableAttributeName);
                if (!EXETypes.IsValidReferenceValue(Values, Attribute.Type))
                {
                    return Error("XEC1156", ErrorMessage.InvalidReference(this.IterableName + "." + this.IterableAttributeName, Values));
                }

                if (!String.Empty.Equals(Values))
                {
                    ReferencingVariablesIDs = Values.Split(',').Select(id => long.Parse(id)).ToList();
                }

                ReferencingVariablesCount = ReferencingVariablesIDs.Count;
            }

            // If iterator already exists and its class does not match the iterable class, we cannot do this
            if (IteratorVariable != null && !IteratorVariable.ClassName.Equals(IterableVariableClassName))
            {
                return Error("XEC1157", ErrorMessage.IterableAndIteratorTypeMismatch(this.IterableName + (this.IterableAttributeName == null ? "" : ("." + this.IterableAttributeName)), IterableVariableClassName, this.IteratorName, IteratorVariable.ClassName));
            }

            // If iterator name is already taken for another variable, we quit again. Otherwise we create the iterator variable
            if (IteratorVariable == null)
            {
                IteratorVariable = new EXEReferencingVariable(this.IteratorName, IterableVariableClassName, -1);
                EXEExecutionResult variableAddResult = this.GetSuperScope().AddVariable(IteratorVariable);
                variableAddResult.OwningCommand = this;
                if (!variableAddResult.IsSuccess)
                {
                    return variableAddResult;
                }
            }

            if (IterableIndex < ReferencingVariablesCount)
            {
                if (this.IterableAttributeName == null)
                {
                    EXEReferencingVariable CurrentItem = IterableVariable.GetReferencingVariables()[IterableIndex];
                    IteratorVariable.ReferencedInstanceId = CurrentItem.ReferencedInstanceId;
                }
                else
                {
                    IteratorVariable.ReferencedInstanceId = ReferencingVariablesIDs[IterableIndex];
                }

                IterableIndex++;
                OALProgram.CommandStack.Enqueue(this);
                AddCommandsToStack(this.Commands);
                this.ClearVariables();
            }

            return Success();
        }

        public override String ToCode(String Indent = "")
        {
            return FormatCode(Indent, false);
        }
        public override string ToFormattedCode(string Indent = "")
        {
            return FormatCode(Indent, IsActive);
        }
        private string FormatCode(String Indent, bool Highlight)
        {
            String Result
                =
                HighlightCodeIf
                (
                    Highlight,
                    Indent + "for each " + this.IteratorName + " in "
                    + (this.IterableAttributeName == null ? this.IterableName : (this.IterableName + "." + this.IterableAttributeName))
                    + "\n"
                );
            foreach (EXECommand Command in this.Commands)
            {
                Result += Command.ToFormattedCode(Indent + "\t");
            }
            Result += HighlightCodeIf(Highlight, Indent + "end for;\n");
            return Result;
        }

        protected override EXEScope CreateDuplicateScope()
        {
            return new EXEScopeForEach(IteratorName, IterableName, IterableAttributeName);
        }
    }
}