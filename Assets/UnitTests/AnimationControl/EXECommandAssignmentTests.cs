using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using OALProgramControl;
using Assets.Scripts.AnimationControl.OAL;
using UnityEngine;
using UnityEngine.TestTools;

public class EXECommandAssignmentTests
{
    [Test]
    public void HappyDay_01_AssignToLocalVariableNew_01_Literal_01_Int()
    {
        // Arrange
        string _methodSourceCode = "x = 5;";

        OALProgram programInstance = new OALProgram();
        CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

        CDMethod owningMethod = new CDMethod(owningClass, "Method1", "");
        owningClass.AddMethod(owningMethod);

        // Act
        EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
        owningMethod.ExecutableCode = methodScope;
        programInstance.SuperScope = methodScope;

        EXEExecutionResult _executionResult = methodScope.PerformExecution(programInstance);

        // Assert
        Assert.IsTrue(_executionResult.IsSuccess);
    }
}
