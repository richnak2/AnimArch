using Assets.Scripts.AnimationControl.OAL;
using NUnit.Framework;
using OALProgramControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.UnitTests.AnimationControl
{
    public class EXECommandArrayStringMethodTests
    {
        [TestFixture]
        public class Contains_string : StandardTest
        {
            [Test]
            public void HappyDay_01_ElementContained()
            {
                CommandTest Test = new CommandTest();

                // Arrange
                string _methodSourceCode = "create list x of string {\"a\", \"b\", \"c\"};\ny = \"b\";\nz = x.Contains(y);";

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
                        .ExpectVariable
                        (
                            "x",
                            new EXEValueArray("string[]")
                            {
                                Elements = new List<EXEValueBase>()
                                {
                                new EXEValueString("\"a\""),
                                new EXEValueString("\"b\""),
                                new EXEValueString("\"c\"")
                                }
                            }
                        )
                        .ExpectVariable("y", new EXEValueString("\"b\""))
                        .ExpectVariable("z", new EXEValueBool(true))
                        .ExpectVariable("self", methodScope.OwningObject);

                Test.PerformAssertion();
            }

            [Test]
            public void HappyDay_02_ElementNotContained()
            {
                CommandTest Test = new CommandTest();

                // Arrange
                string _methodSourceCode = "create list x of string {\"a\", \"b\", \"c\"};\ny = \"d\";\nz = x.Contains(y);";

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
                        .ExpectVariable
                        (
                            "x",
                            new EXEValueArray("string[]")
                            {
                                Elements = new List<EXEValueBase>()
                                {
                                new EXEValueString("\"a\""),
                                new EXEValueString("\"b\""),
                                new EXEValueString("\"c\"")
                                }
                            }
                        )
                        .ExpectVariable("y", new EXEValueString("\"d\""))
                        .ExpectVariable("z", new EXEValueBool(false))
                        .ExpectVariable("self", methodScope.OwningObject);

                Test.PerformAssertion();
            }

            [Test]
            public void HappyDay_03_ArrayEmpty()
            {
                CommandTest Test = new CommandTest();

                // Arrange
                string _methodSourceCode = "create list x of string;\ny = \"d\";\nz = x.Contains(y);";

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
                        .ExpectVariable
                        (
                            "x",
                            new EXEValueArray("string[]")
                            {
                                Elements = new List<EXEValueBase>()
                            }
                        )
                        .ExpectVariable("y", new EXEValueString("\"d\""))
                        .ExpectVariable("z", new EXEValueBool(false))
                        .ExpectVariable("self", methodScope.OwningObject);

                Test.PerformAssertion();
            }
        }

        [TestFixture]
        public class Contains_int : StandardTest
        {
            [Test]
            public void HappyDay_01_ElementContained()
            {
                CommandTest Test = new CommandTest();

                // Arrange
                string _methodSourceCode = "create list x of integer {1, 2, 3};\ny = 3;\nz = x.Contains(y);";

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
                        .ExpectVariable
                        (
                            "x",
                            new EXEValueArray("integer[]")
                            {
                                Elements = new List<EXEValueBase>()
                                {
                                    new EXEValueInt(1),
                                    new EXEValueInt(2),
                                    new EXEValueInt(3)
                                }
                            }
                        )
                        .ExpectVariable("y", new EXEValueInt(3))
                        .ExpectVariable("z", new EXEValueBool(true))
                        .ExpectVariable("self", methodScope.OwningObject);

                Test.PerformAssertion();
            }

            [Test]
            public void HappyDay_02_ElementNotContained()
            {
                CommandTest Test = new CommandTest();

                // Arrange
                string _methodSourceCode = "create list x of integer {1, 2, 3};\ny = 4;\nz = x.Contains(y);";

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
                        .ExpectVariable
                        (
                            "x",
                            new EXEValueArray("integer[]")
                            {
                                Elements = new List<EXEValueBase>()
                                {
                                    new EXEValueInt(1),
                                    new EXEValueInt(2),
                                    new EXEValueInt(3)
                                }
                            }
                        )
                        .ExpectVariable("y", new EXEValueInt(4))
                        .ExpectVariable("z", new EXEValueBool(false))
                        .ExpectVariable("self", methodScope.OwningObject);

                Test.PerformAssertion();
            }

            [Test]
            public void HappyDay_03_ArrayEmpty()
            {
                CommandTest Test = new CommandTest();

                // Arrange
                string _methodSourceCode = "create list x of integer;\ny = 4;\nz = x.Contains(y);";

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
                        .ExpectVariable
                        (
                            "x",
                            new EXEValueArray("integer[]")
                            {
                                Elements = new List<EXEValueBase>()
                            }
                        )
                        .ExpectVariable("y", new EXEValueInt(4))
                        .ExpectVariable("z", new EXEValueBool(false))
                        .ExpectVariable("self", methodScope.OwningObject);

                Test.PerformAssertion();
            }
        }


        [TestFixture]
        public class Count_string : StandardTest
        {
            [Test]
            public void HappyDay_01_Filled()
            {
                CommandTest Test = new CommandTest();

                // Arrange
                string _methodSourceCode = "create list x of string {\"a\", \"b\", \"c\"};\ny = x.Count();";

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
                        .ExpectVariable
                        (
                            "x",
                            new EXEValueArray("string[]")
                            {
                                Elements = new List<EXEValueBase>()
                                {
                                new EXEValueString("\"a\""),
                                new EXEValueString("\"b\""),
                                new EXEValueString("\"c\"")
                                }
                            }
                        )
                        .ExpectVariable("y", new EXEValueInt(3))
                        .ExpectVariable("self", methodScope.OwningObject);

                Test.PerformAssertion();
            }

            [Test]
            public void HappyDay_02_Empty()
            {
                CommandTest Test = new CommandTest();

                // Arrange
                string _methodSourceCode = "create list x of string;\ny = x.Count();";

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
                        .ExpectVariable
                        (
                            "x",
                            new EXEValueArray("string[]")
                            {
                                Elements = new List<EXEValueBase>()
                            }
                        )
                        .ExpectVariable("y", new EXEValueInt(0))
                        .ExpectVariable("self", methodScope.OwningObject);

                Test.PerformAssertion();
            }
        }


        [TestFixture]
        public class Count_int : StandardTest
        {
            [Test]
            public void HappyDay_01_Filled()
            {
                CommandTest Test = new CommandTest();

                // Arrange
                string _methodSourceCode = "create list x of integer {1, 2, 3};\ny = x.Count();";

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
                        .ExpectVariable
                        (
                            "x",
                            new EXEValueArray("integer[]")
                            {
                                Elements = new List<EXEValueBase>()
                                {
                                    new EXEValueInt(1),
                                    new EXEValueInt(2),
                                    new EXEValueInt(3)
                                }
                            }
                        )
                        .ExpectVariable("y", new EXEValueInt(3))
                        .ExpectVariable("self", methodScope.OwningObject);

                Test.PerformAssertion();
            }

            [Test]
            public void HappyDay_02_Empty()
            {
                CommandTest Test = new CommandTest();

                // Arrange
                string _methodSourceCode = "create list x of integer;\ny = x.Count();";

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
                        .ExpectVariable
                        (
                            "x",
                            new EXEValueArray("integer[]")
                            {
                                Elements = new List<EXEValueBase>()
                            }
                        )
                        .ExpectVariable("y", new EXEValueInt(0))
                        .ExpectVariable("self", methodScope.OwningObject);

                Test.PerformAssertion();
            }
        }
    }
}
