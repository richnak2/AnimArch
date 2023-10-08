using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OALProgramControl
{
    public class EXECommandQuerySelectRelatedBy : EXECommand
    {
        public String Cardinality { get; set; }
        public String VariableName { get; set; }
        public String AttributeName { get; set; }
        public EXEASTNode WhereCondition { get; set; }
        public EXERelationshipSelection RelationshipSelection {get; set;}

        public EXECommandQuerySelectRelatedBy(String Cardinality, String VariableName, String AttributeName, EXEASTNode WhereCondition, EXERelationshipSelection RelationshipSelection)
        {
            this.Cardinality = Cardinality;
            this.VariableName = VariableName;
            this.AttributeName = AttributeName;
            this.WhereCondition = WhereCondition;
            this.RelationshipSelection = RelationshipSelection;
        }
        // SetUloh2
        protected override EXEExecutionResult Execute(OALProgram OALProgram)
        {
            //Select instances of given class that match the criteria and assign them to variable with given name
            // ClassName tells us which class we are interested in
            // Cardinality tells us whether we want one random instance (matching the criteria) or all of them
            // "Many" - we create variable EXEReferencingSetVariable, "Any" - we create variable EXEReferencingVariable
            // Variable name tells us how to name the newly created referencing variable
            // Where condition tells us which instances to select from all instances of the class (just do EXEASTNode.Evaluate and return true if the result "true" and false for "false")
            // When making unit tests, do not use the "where" causule yet, because its evaluation is not yet implemented
            // If relationship selection does not exists, this is problem

            if (this.RelationshipSelection == null)
            {
                return Error(ErrorMessage.NoRelationshipSelectionOnSelectByRelationship());
            }

            string className = this.RelationshipSelection.GetLastClassName();
            CDClass Class = OALProgram.ExecutionSpace.getClassByName(className);
            if (Class == null)
            {
                return Error(ErrorMessage.ClassNotFound(className, OALProgram));
            }
            
            CDClassInstance ClassInstance = null; // This is important if we have AttributeName

            if (this.AttributeName == null)
            {
                // We need to check, if the variable already exists, it must be of corresponding type
                if (SuperScope.VariableNameExists(this.VariableName))
                {

                    EXEReferencingVariable refVariable = SuperScope.FindReferencingVariableByName(this.VariableName);

                    if
                    (
                        EXECommandQuerySelect.CardinalityAny.Equals(this.Cardinality)
                        && className != refVariable.ClassName
                    )
                    {
                        return Error(ErrorMessage.InvalidAssignment("", className, this.VariableName, refVariable.ClassName));
                    }

                    EXEReferencingSetVariable setVariable = SuperScope.FindSetReferencingVariableByName(this.VariableName);

                    if
                    (
                        EXECommandQuerySelect.CardinalityMany.Equals(this.Cardinality)
                        &&
                        className != setVariable.ClassName
                    )
                    {
                        return Error(ErrorMessage.InvalidAssignment("", className, this.VariableName, refVariable.ClassName));
                    }
                }
            }
            else
            {
                // If we have AttributeName, VariableName must be reference
                EXEReferencingVariable Variable = SuperScope.FindReferencingVariableByName(this.VariableName);
                if (Variable == null)
                {
                    return Error(ErrorMessage.VariableNotFound(this.VariableName, this.SuperScope));
                }

                CDClass VariableClass = OALProgram.ExecutionSpace.getClassByName(Variable.ClassName);
                if (VariableClass == null)
                {
                    return Error(ErrorMessage.ClassNotFound(Variable.ClassName, OALProgram));
                }

                CDAttribute Attribute = VariableClass.GetAttributeByName(this.AttributeName);
                if (Attribute == null)
                {
                    return Error(ErrorMessage.AttributeNotFoundOnClass(this.AttributeName, VariableClass));
                }

                // We need to check the corresponding type of attribute
                if ("[]".Equals(Attribute.Type.Substring(Attribute.Type.Length - 2, 2)))
                {
                    if (!Attribute.Type.Equals(className + "[]"))
                    {
                        return Error(ErrorMessage.AssignNewListToVariableHoldingListOfAnotherType(this.VariableName + "." + this.AttributeName, Attribute.Type.Replace("[]", ""), className));
                    }

                    if (EXECommandQuerySelect.CardinalityAny.Equals(this.Cardinality))
                    {
                        return Error(ErrorMessage.SelectingAnyIntoAnArray());
                    }
                }
                else
                {
                    if (!Attribute.Type.Equals(className))
                    {
                        return Error(ErrorMessage.InvalidAssignment("irrelevant", className, this.VariableName + "." + this.AttributeName, Attribute.Type));
                    }

                    if (EXECommandQuerySelect.CardinalityMany.Equals(this.Cardinality))
                    {
                        return Error(ErrorMessage.SelectingManyIntoAReference());
                    }
                }

                // Get instance representing VariableName, important in assignment section at end of this Execute method
                ClassInstance = VariableClass.GetInstanceByID(Variable.ReferencedInstanceId);
                if (ClassInstance == null)
                {
                    return Error(ErrorMessage.InstanceNotFound(Variable.ReferencedInstanceId, VariableClass));
                }
            }

            // Evaluate relationship selection. If it fails, execution fails too
            List<long> SelectedIds = this.RelationshipSelection.Evaluate(OALProgram, SuperScope);
            if (SelectedIds == null)
            {
                return Error(ErrorMessage.FailedRetrievingAllClassInstanceIds(Class));
            }

            // Now let's evaluate the condition
            if (this.WhereCondition != null && SelectedIds.Any())
            {
                String TempSelectedVarName = "selected";

                EXEReferencingVariable SelectedVar = new EXEReferencingVariable(TempSelectedVarName, this.RelationshipSelection.GetLastClassName(), -1);
                EXEExecutionResult addVariableResult = SuperScope.AddVariable(SelectedVar);
                addVariableResult.OwningCommand = this;
                if (!addVariableResult.IsSuccess)
                {
                    return addVariableResult;
                }

                List<long> ResultIds = new List<long>();
                foreach (long Id in SelectedIds)
                {
                    SelectedVar.ReferencedInstanceId = Id;
                    String ConditionResult = this.WhereCondition.Evaluate(SuperScope, OALProgram.ExecutionSpace);
                    //Console.Write(Id + " : " + ConditionResult);

                    if(!EXETypes.IsValidValue(ConditionResult, EXETypes.BooleanTypeName))
                    {
                        SuperScope.DestroyReferencingVariable(TempSelectedVarName);
                        return Error(ErrorMessage.InvalidValueForType(string.Format("Condition in WHERE clause evaluated to '{0}'", ConditionResult), EXETypes.BooleanTypeName));
                    }

                    if (EXETypes.BooleanTrue.Equals(ConditionResult))
                    {
                        ResultIds.Add(Id);
                    }
                }

                SelectedIds = ResultIds;
                SuperScope.DestroyReferencingVariable(TempSelectedVarName);
            }

            // Now we have ids of selected instances. Let's assign them to a variable
            if (EXECommandQuerySelect.CardinalityMany.Equals(this.Cardinality))
            {
                if (this.AttributeName == null)
                {
                    EXEReferencingSetVariable Variable = SuperScope.FindSetReferencingVariableByName(this.VariableName);
                    if (Variable == null)
                    {
                        Variable = new EXEReferencingSetVariable(this.VariableName, this.RelationshipSelection.GetLastClassName());
                        EXEExecutionResult addVariableResult = SuperScope.AddVariable(Variable);
                        addVariableResult.OwningCommand = this;
                        if (!addVariableResult.IsSuccess)
                        {
                            return addVariableResult;
                        }
                    }

                    Variable.ClearVariables();

                    foreach (long Id in SelectedIds)
                    {
                        Variable.AddReferencingVariable(new EXEReferencingVariable("", Variable.ClassName, Id));
                    }
                }
                else
                {
                    String Result = String.Join(",", SelectedIds);

                    EXEExecutionResult setAttributeResult = ClassInstance.SetAttribute(this.AttributeName, Result);
                    setAttributeResult.OwningCommand = this;
                    if (!setAttributeResult.IsSuccess)
                    {
                        return setAttributeResult;
                    }
                }
            }
            else if (EXECommandQuerySelect.CardinalityAny.Equals(this.Cardinality))
            {
                long ResultId = SelectedIds.Any() ? SelectedIds[0] : -1;

                if (this.AttributeName == null)
                {
                    EXEReferencingVariable Variable = SuperScope.FindReferencingVariableByName(this.VariableName);

                    if (Variable == null)
                    {
                        Variable = new EXEReferencingVariable(this.VariableName, this.RelationshipSelection.GetLastClassName(), ResultId);
                        EXEExecutionResult addVariableResult = SuperScope.AddVariable(Variable);
                        addVariableResult.OwningCommand = this;
                        if (!addVariableResult.IsSuccess)
                        {
                            return addVariableResult;
                        }
                    }
                    else
                    {
                        Variable.ReferencedInstanceId = ResultId;
                    }
                }
                else
                {
                    EXEExecutionResult setAttributeResult = ClassInstance.SetAttribute(this.AttributeName, ResultId.ToString());
                    setAttributeResult.OwningCommand = this;
                    if (!setAttributeResult.IsSuccess)
                    {
                        return setAttributeResult;
                    }
                }
            }
            else
            {
                return Error(ErrorMessage.UnknownSelectCardinality(this.Cardinality));
            }

            return Success();
        }
        public override string ToCodeSimple()
        {
            string prefix = "select " + this.Cardinality + " " + (this.AttributeName == null ? this.VariableName : (this.VariableName + "." + this.AttributeName)) + " related by ";
            string relationLink = this.RelationshipSelection.ToCode();
            string sufix = this.WhereCondition == null ? "" : (" where ") + this.WhereCondition.ToCode();

            return prefix + relationLink + sufix;
        }

        public override EXECommand CreateClone()
        {
            return new EXECommandQuerySelectRelatedBy(Cardinality, VariableName, AttributeName, WhereCondition, RelationshipSelection);
        }
    }
}
