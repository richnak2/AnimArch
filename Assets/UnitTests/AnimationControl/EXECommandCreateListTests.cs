using System.Linq;
using NUnit.Framework;
using OALProgramControl;
using Assets.Scripts.AnimationControl.OAL;
using System;
using System.Collections.Generic;

namespace Assets.UnitTests.AnimationControl
{
    public class EXECommandCreateListTests : StandardTest
    {
        //TODO create tests for different types of lists (int, integer, long, long int, long integer)
        [Test]
        public void HappyDay_01_AssignToLocalVariable_01_Literal_01_Int()
        {
            CommandTest Test = new CommandTest();

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

            EXEExecutionResult _executionResult = PerformExecution(programInstance);

            // Assert
            Test.Declare(methodScope, _executionResult);

            Test.Variables
                    .ExpectVariable("x", new EXEValueInt(5))
                    .ExpectVariable("self", new EXEValueReference(methodScope.OwningObject));

            Test.PerformAssertion();
        }
    }
}