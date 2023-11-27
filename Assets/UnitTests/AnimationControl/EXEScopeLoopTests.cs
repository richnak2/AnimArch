using Assets.Scripts.AnimationControl.OAL;
using NUnit.Framework;
using OALProgramControl;
using System.Collections.Generic;
using System.Linq;

namespace Assets.UnitTests.AnimationControl
{
    public class EXEScopeLoopTests : StandardTest
    {
        [Test]
        public void HappyDay_01_While()
        {
            CommandTest Test = new CommandTest();

            // Arrange
            string _methodSourceCode = "x = 0;\r\ni = 0;\r\nwhile(i < 5)\r\nx = x + 2;\r\ni = i + 1;\r\nend while;";

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
                    .ExpectVariable("x", new EXEValueInt(10))
                    .ExpectVariable("i", new EXEValueInt(5))
                    .ExpectVariable("self", new EXEValueReference(methodScope.OwningObject));

            Test.PerformAssertion();
        }

        [Test]
        public void HappyDay_02_Foreach_01_CheckByLocalVariable()
        {
            CommandTest Test = new CommandTest();

            // Arrange
            string _methodSourceCode = "create list children of Class1;\r\nadd self to children;\r\ncreate object instance brother of Class1;\r\nadd brother to children;\r\ni = 0;\r\nfor each child in children\r\ni = i + 1;\r\nend for;";

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

            EXEValueArray expectedChildrenValue = new EXEValueArray("Class1[]");
            expectedChildrenValue.Elements = new List<EXEValueBase>();
            expectedChildrenValue.WasInitialized = true;
            expectedChildrenValue.AppendElement(new EXEValueReference(methodScope.OwningObject), programInstance.ExecutionSpace);
            expectedChildrenValue.AppendElement(new EXEValueReference(owningClass.Instances.Last()), programInstance.ExecutionSpace);

            Test.Variables
                    .ExpectVariable("i", new EXEValueInt(2))
                    .ExpectVariable("self", new EXEValueReference(methodScope.OwningObject))
                    .ExpectVariable("brother", new EXEValueReference(owningClass.Instances.Last()))
                    .ExpectVariable("child", new EXEValueReference(owningClass.Instances.Last()))
                    .ExpectVariable("children", expectedChildrenValue);

            Test.PerformAssertion();
        }

        [Test]
        public void HappyDay_02_Foreach_02_CheckBySum()
        {
            CommandTest Test = new CommandTest();

            // Arrange
            string _methodSourceCode = "create list children of Class1;\r\nself.att1 = 5;\r\nadd self to children;\r\ncreate object instance brother of Class1;\r\nbrother.att1 = 7;\r\nadd brother to children;\r\ni = 0;\r\nfor each child in children\r\ni = i + child.att1;\r\nend for;";

            OALProgram programInstance = new OALProgram();
            CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

            CDAttribute att = new CDAttribute("att1", "integer");
            owningClass.AddAttribute(att);

            CDMethod owningMethod = new CDMethod(owningClass, "Method1", "");
            owningClass.AddMethod(owningMethod);

            // Act
            EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
            owningMethod.ExecutableCode = methodScope;
            programInstance.SuperScope = methodScope;

            EXEExecutionResult _executionResult = PerformExecution(programInstance);

            // Assert
            Test.Declare(methodScope, _executionResult);

            EXEValueArray expectedChildrenValue = new EXEValueArray("Class1[]");
            expectedChildrenValue.Elements = new List<EXEValueBase>();
            expectedChildrenValue.WasInitialized = true;
            expectedChildrenValue.AppendElement(new EXEValueReference(methodScope.OwningObject), programInstance.ExecutionSpace);
            expectedChildrenValue.AppendElement(new EXEValueReference(owningClass.Instances.Last()), programInstance.ExecutionSpace);

            Test.Variables
                    .ExpectVariable("i", new EXEValueInt(12))
                    .ExpectVariable("self", new EXEValueReference(methodScope.OwningObject))
                    .ExpectVariable("brother", new EXEValueReference(owningClass.Instances.Last()))
                    .ExpectVariable("child", new EXEValueReference(owningClass.Instances.Last()))
                    .ExpectVariable("children", expectedChildrenValue);

            Test.PerformAssertion();
        }

        [Test]
        public void HappyDay_02_Foreach_03_CheckByMultipleCommands()
        {
            CommandTest Test = new CommandTest();

            // Arrange
            string _methodSourceCode = "create list children of Class1;\r\nself.att1 = 5;\r\nadd self to children;\r\ncreate object instance brother of Class1;\r\nbrother.att1 = 7;\r\nadd brother to children;\r\ni = 0;\r\nj = 1;\r\nfor each child in children\r\ni = i + child.att1;\r\nj = j * child.att1;\r\nend for;";

            OALProgram programInstance = new OALProgram();
            CDClass owningClass = programInstance.ExecutionSpace.SpawnClass("Class1");

            CDAttribute att = new CDAttribute("att1", "integer");
            owningClass.AddAttribute(att);

            CDMethod owningMethod = new CDMethod(owningClass, "Method1", "");
            owningClass.AddMethod(owningMethod);

            // Act
            EXEScopeMethod methodScope = OALParserBridge.Parse(_methodSourceCode);
            owningMethod.ExecutableCode = methodScope;
            programInstance.SuperScope = methodScope;

            EXEExecutionResult _executionResult = PerformExecution(programInstance);

            // Assert
            Test.Declare(methodScope, _executionResult);

            EXEValueArray expectedChildrenValue = new EXEValueArray("Class1[]");
            expectedChildrenValue.Elements = new List<EXEValueBase>();
            expectedChildrenValue.WasInitialized = true;
            expectedChildrenValue.AppendElement(new EXEValueReference(methodScope.OwningObject), programInstance.ExecutionSpace);
            expectedChildrenValue.AppendElement(new EXEValueReference(owningClass.Instances.Last()), programInstance.ExecutionSpace);

            Test.Variables
                    .ExpectVariable("i", new EXEValueInt(12))
                    .ExpectVariable("j", new EXEValueInt(35))
                    .ExpectVariable("self", new EXEValueReference(methodScope.OwningObject))
                    .ExpectVariable("brother", new EXEValueReference(owningClass.Instances.Last()))
                    .ExpectVariable("child", new EXEValueReference(owningClass.Instances.Last()))
                    .ExpectVariable("children", expectedChildrenValue);

            Test.PerformAssertion();
        }
    }
}