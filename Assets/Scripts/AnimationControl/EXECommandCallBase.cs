using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AnimArch.Extensions;
using UnityEngine;
using Object = System.Object;

namespace OALProgramControl
{
    public abstract class EXECommandCallBase : EXECommand
    {
        public readonly EXEASTNodeMethodCall MethodCall;

        public EXECommandCallBase EvaluateMethodCall()
        {
            
        }

        public MethodCallRecord CallerMethodInfo
        {
            get
            {
                EXEScopeMethod TopScope = (EXEScopeMethod) GetTopLevelScope();
                return TopScope.MethodDefinition;
            }
        }

        public EXECommandCallBase(EXEASTNodeAccessChain methodAccessChain, EXEASTNodeMethodCall methodCall)
        {
            this.InstanceName = InstanceName;
            this.AttributeName = AttributeName;
            this.CalledMethod = MethodName;
            this.Parameters = Parameters;
        }

        protected override EXEExecutionResult Execute(OALProgram OALProgram)
        {
            EXEReferencingVariable Reference = this.SuperScope.FindReferencingVariableByName(this.InstanceName);

            if (Reference == null)
            {
                return Error("XEC1030", ErrorMessage.VariableNotFound(this.InstanceName, this.SuperScope));
            }

            CDClass Class = OALProgram.ExecutionSpace.getClassByName(Reference.ClassName);

            if (Class == null)
            {
                return Error("XEC1031", ErrorMessage.ClassNotFound(Reference.ClassName, OALProgram));
            }

            Class = Class.GetInstanceClassByIDRecursiveDownward(Reference.ReferencedInstanceId);

            if (Class == null)
            {
                return Error("XEC1032", ErrorMessage.InstanceNotFoundRecursive(Reference.ReferencedInstanceId, Class));
            }

            long CalledID = Reference.ReferencedInstanceId;

            if (this.AttributeName != null)
            {
                CDAttribute Attribute = Class.GetAttributeByName(this.AttributeName);

                if (Attribute == null)
                {
                    return Error("XEC1033", ErrorMessage.AttributeNotFoundOnClass(this.AttributeName, Class));
                }

                CDClass AtrributeClass = OALProgram.ExecutionSpace.getClassByName(Attribute.Type);

                if (AtrributeClass == null)
                {
                    return Error("XEC1034", ErrorMessage.ClassNotFound(Attribute.Type, OALProgram));
                }

                CalledID = long.Parse(Class.GetInstanceByID(Reference.ReferencedInstanceId).State[Attribute.Name]);
                Class = AtrributeClass.GetInstanceClassByIDRecursiveDownward(CalledID);
            }

            this.CalledClass = Class.Name;

            CDMethod Method = Class.GetMethodByName(this.CalledMethod);

            if (Method == null)
            {
                return Error("XEC1035", ErrorMessage.MethodNotFoundOnClass(this.CalledMethod, Class));
            }

            EXEScopeMethod MethodCode = Method.ExecutableCode;

            if (MethodCode == null)
            {
                return Success();
            }

            MethodCode.SetSuperScope(null);
            MethodCode.CommandStack = this.CommandStack;
            this.CommandStack.Enqueue(MethodCode);
            MethodCode.AddVariable(new EXEReferencingVariable(EXETypes.SelfReferenceName, CalledClass, CalledID)); //

            for (int i = 0; i < this.Parameters.Count; i++)
            {
                CDParameter Parameter = Method.Parameters[i];
                if
                (
                    !(
                        Parameter.Type != null
                        &&
                        this.Parameters[i].IsReference().Implies
                        (
                            Object.Equals(Parameter.Type,
                                this.SuperScope.DetermineVariableType(this.Parameters[i].AccessChain(),
                                    OALProgram.ExecutionSpace))
                        )
                    )
                    &&
                    !(
                        OALProgram.Instance.ExecutionSpace.ClassExists(Parameter.Type)
                        ||
                        OALProgram.Instance.ExecutionSpace.ClassExists(
                            Parameter.Type.Substring(0, Parameter.Type.Length - 2))
                    )
                )
                {
                    return Error("XEC1036", ErrorMessage.UnresolvedParameterValue(Class.Name, Method.Name, Parameter.Name, this.Parameters[i].ToCode()));
                }

                if (EXETypes.IsPrimitive(Parameter.Type))
                {
                    String Value = this.Parameters[i].Evaluate(this.SuperScope, OALProgram.ExecutionSpace);

                    if (!EXETypes.IsValidValue(Value, Parameter.Type))
                    {
                        return Error("XEC1037", ErrorMessage.InvalidParameterValue(Class.Name, Method.Name, Parameter.Name, Parameter.Type, Value));
                    }

                    MethodCode.AddVariable(new EXEPrimitiveVariable(Parameter.Name, Value, Parameter.Type));
                }
                else if ("[]".Equals(Parameter.Type.Substring(Parameter.Type.Length - 2, 2)))
                {
                    string className = Parameter.Type.Substring(0, Parameter.Type.Length - 2);
                    CDClass ClassDefinition = OALProgram.ExecutionSpace.getClassByName(className);
                    if (ClassDefinition == null)
                    {
                        return Error("XEC1038", ErrorMessage.ClassNotFound(className, OALProgram)); ;
                    }

                    String Values = this.Parameters[i].Evaluate(this.SuperScope, OALProgram.ExecutionSpace);

                    if (!EXETypes.IsValidReferenceValue(Values, Parameter.Type))
                    {
                        return Error("XEC1039", ErrorMessage.InvalidReference(string.Format("Parameter '{0}' of method '{1}' of class '{2}'", Parameter.Name, Method.Name, Class.Name), Values));
                    }

                    long[] IDs = String.Empty.Equals(Values)
                        ? new long[] { }
                        : Values.Split(',').Select(id => long.Parse(id)).ToArray();

                    CDClassInstance ClassInstance;
                    foreach (long ID in IDs)
                    {
                        ClassInstance = ClassDefinition.GetInstanceByID(ID);
                        if (ClassInstance == null)
                        {
                            return Error("XEC1040", ErrorMessage.InstanceNotFoundRecursive(ID, ClassDefinition));
                        }
                    }

                    EXEReferencingSetVariable CreatedSetVariable =
                        new EXEReferencingSetVariable(Parameter.Name, ClassDefinition.Name);

                    foreach (long ID in IDs)
                    {
                        CreatedSetVariable.AddReferencingVariable(
                            new EXEReferencingVariable("", ClassDefinition.Name, ID));
                    }

                    MethodCode.AddVariable(CreatedSetVariable);
                }
                else if (!String.IsNullOrEmpty(Parameter.Type))
                {
                    CDClass ClassDefinition = OALProgram.ExecutionSpace.getClassByName(Parameter.Type);
                    if (ClassDefinition == null)
                    {
                        return Error("XEC1041", ErrorMessage.ClassNotFound(Parameter.Type, OALProgram));
                    }

                    string Value = Parameters[i].Evaluate(this.SuperScope, OALProgram.ExecutionSpace);

                    if (!EXETypes.IsValidReferenceValue(Value, Parameter.Type))
                    {
                        return Error("XEC1042", ErrorMessage.InvalidReference(string.Format("Parameter '{0}' of method '{1}' of class '{2}'", Parameter.Name, Method.Name, Class.Name), Value));
                    }

                    long ID = long.Parse(Value);

                    CDClassInstance ClassInstance = ClassDefinition.GetInstanceByIDRecursiveDownward(ID);
                    if (ClassInstance == null)
                    {
                        return Error("XEC1043", ErrorMessage.InstanceNotFoundRecursive(ID, ClassDefinition));
                    }

                    MethodCode.AddVariable(new EXEReferencingVariable(Parameter.Name, ClassDefinition.Name, ID));
                }
            }

            return Success();
        }

        public OALCall CreateOALCall()
        {
            MethodCallRecord _CallerMethodInfo = this.CallerMethodInfo;
            CDRelationship _RelationshipInfo = CallRelationshipInfo(_CallerMethodInfo.ClassName, this.CalledClass);
            bool IsSelfCall = string.Equals(this.CalledClass, _CallerMethodInfo.ClassName);
            return new OALCall
            (
                _CallerMethodInfo.ClassName,
                _CallerMethodInfo.MethodName,
                IsSelfCall ? null : _RelationshipInfo?.RelationshipName,
                this.CalledClass,
                this.CalledMethod,
                SuperScope.VariableNameExists(InstanceName) ?
                    SuperScope.FindReferencingVariableByName(InstanceName).ReferencedInstanceId : -1,
                IsSelfCall
            );
        }

        public override String ToCodeSimple()
        {
            return new StringBuilder()
                .Append(InstanceName)
                .Append(AttributeName == null ? string.Empty : ("." + AttributeName))
                .Append(".")
                .Append(CalledMethod)
                .Append("(")
                .AppendJoin(", ", Parameters.Select(parameter => parameter.ToCode()))
                .Append(")")
                .ToString();
        }

        private CDRelationship CallRelationshipInfo(string CallerMethod, string CalledMethod)
        {
            return OALProgram.Instance.RelationshipSpace.GetRelationshipByClasses(CallerMethod, CalledMethod);
        }

        public override EXECommand CreateClone()
        {
            return new EXECommandCallBase(InstanceName, AttributeName, CalledMethod, Parameters);
        }
    }
}
