using System.Linq;
using NUnit.Framework;
using OALProgramControl;
using Assets.Scripts.AnimationControl.OAL;
using System;
using System.Collections.Generic;

namespace Assets.UnitTests.AnimationControl
{
    public class EXECommandReturnTests : StandardTest
    {
        [Test]
        public void HappyDay_01_RootLevel_01_OneCommandAfter()
        {
            CommandTest Test = new CommandTest();

            // Arrange
            string _methodSourceCode = "x = 5;\nreturn 2;\nx = 6;";

            OALProgram programInstance = new OALProgram();
            CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

            CDMethod owningMethod = new CDMethod(owningClass, "Method1", "integer");
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
                    .ExpectVariable("self", methodScope.OwningObject);

            Test.PerformAssertion();
        }

        [Test]
        public void HappyDay_01_RootLevel_02_ManyCommandsAfter()
        {
            CommandTest Test = new CommandTest();

            // Arrange
            string _methodSourceCode = "x = 5;\nreturn 2;\nx = 6;\nx = 7;\nif(x > 8)\n\tx = 25;\nend if;";

            OALProgram programInstance = new OALProgram();
            CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

            CDMethod owningMethod = new CDMethod(owningClass, "Method1", "integer");
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
                    .ExpectVariable("self", methodScope.OwningObject);

            Test.PerformAssertion();
        }

        [Test]
        public void HappyDay_02_Nested_01_OneCommandAfter()
        {
            CommandTest Test = new CommandTest();

            // Arrange
            string _methodSourceCode = "x = 5;\nif (x == 5)\n\treturn 2;\nend if;\nx = 6;";

            OALProgram programInstance = new OALProgram();
            CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

            CDMethod owningMethod = new CDMethod(owningClass, "Method1", "integer");
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
                    .ExpectVariable("self", methodScope.OwningObject);

            Test.PerformAssertion();
        }
    }
}