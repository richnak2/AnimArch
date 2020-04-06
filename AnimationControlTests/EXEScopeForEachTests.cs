using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnimationControl;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimationControl.Tests
{
    [TestClass()]
    public class EXEScopeForEachTests
    {
        [TestMethod]
        public void EXEScopeLoopWhile_Normal_01()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Observer");

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer"));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityMany, "Observer", "observers"));
            Animation.SuperScope.AddCommand(
                new EXEScopeForEach
                (
                   Animation.SuperScope,
                   new EXECommand[]
                   {
                   },
                   "observer",
                   "observers"
                )
            );

            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
            };
            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "observer", "Observer"},
                { "observers[]", "Observer"},
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
            };
            int ExpectedValidRefVarCount = 0;
            int ExpectedValidSetRefVarCount = 0;

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();
            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetSetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "observers");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = Animation.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
            Assert.AreEqual(ExpectedValidSetRefVarCount, ActualValidSetRefVarCount);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void EXEScopeLoopWhile_Normal_02()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Observer");

            Animation.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            Animation.SuperScope.AddCommand(new EXEScopeLoopWhile
            (
                Animation.SuperScope,
                new EXECommand[]
                {
                    new EXECommandQueryCreate("Observer"),
                    new EXECommandAssignment(
                        "x",
                        new EXEASTNodeComposite(
                            "+",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("x"),
                                new EXEASTNodeLeaf("1")
                            }
                        )
                    )
                },
                new EXEASTNodeComposite
                (
                    "<",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeLeaf("x"),
                        new EXEASTNodeLeaf("10")
                    }
                )
            ));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityMany, "Observer", "observers"));
            Animation.SuperScope.AddCommand(
                new EXEScopeForEach
                (
                   Animation.SuperScope,
                   new EXECommand[]
                   {
                        new EXECommandQueryDelete("observer")
                   },
                   "observer",
                   "observers"
                )
            );

            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
                {"x", "10"}
            };
            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 0}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "observer", "Observer"},
                { "observers[]", "Observer"},
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
            };
            int ExpectedValidRefVarCount = 0;
            int ExpectedValidSetRefVarCount = 0;

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();
            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetSetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "observers");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = Animation.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
            Assert.AreEqual(ExpectedValidSetRefVarCount, ActualValidSetRefVarCount);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
        [TestMethod]
        public void EXEScopeLoopWhile_Normal_03()
        {
            Animation Animation = new Animation();
            Animation.ExecutionSpace.SpawnClass("Observer");
            Animation.ExecutionSpace.SpawnClass("Subject");
            Animation.RelationshipSpace.SpawnRelationship("Observer", "Subject");

            Animation.SuperScope.AddCommand(new EXECommandAssignment("x", new EXEASTNodeLeaf("0")));
            Animation.SuperScope.AddCommand(new EXEScopeLoopWhile
            (
                Animation.SuperScope,
                new EXECommand[]
                {
                    new EXECommandQueryCreate("Subject", "s"),
                    new EXECommandAssignment("y", new EXEASTNodeLeaf("0")),
                    new EXEScopeLoopWhile
                    (
                        Animation.SuperScope,
                        new EXECommand[]
                        {
                            new EXECommandQueryCreate("Observer", "o"),
                            new EXECommandQueryRelate("o", "s", "R1"),
                            new EXECommandAssignment(
                                "y",
                                new EXEASTNodeComposite(
                                    "+",
                                    new EXEASTNode[]
                                    {
                                        new EXEASTNodeLeaf("y"),
                                        new EXEASTNodeLeaf("1")
                                    }
                                )
                            )
                        },
                        new EXEASTNodeComposite
                        (
                            "<",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("y"),
                                new EXEASTNodeLeaf("x")
                            }
                        )
                    ),
                    new EXECommandAssignment(
                        "x",
                        new EXEASTNodeComposite(
                            "+",
                            new EXEASTNode[]
                            {
                                new EXEASTNodeLeaf("x"),
                                new EXEASTNodeLeaf("1")
                            }
                        )
                    )
                },
                new EXEASTNodeComposite
                (
                    "<",
                    new EXEASTNode[]
                    {
                        new EXEASTNodeLeaf("x"),
                        new EXEASTNodeLeaf("10")
                    }
                )
            ));
            Animation.SuperScope.AddCommand(new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityMany, "Subject", "subjects"));
            Animation.SuperScope.AddCommand(
                new EXEScopeForEach
                (
                   Animation.SuperScope,
                   new EXECommand[]
                   {
                        new EXECommandQuerySelectRelatedBy
                        (
                            EXECommandQuerySelect.CardinalityMany,
                            "observers",
                            null,
                            new EXERelationshipSelection
                            (
                                "subject",
                                new EXERelationshipLink[]
                                {
                                    new EXERelationshipLink("R1", "Observer")
                                }
                            )
                        ),
                        new EXEScopeForEach
                        (
                            Animation.SuperScope,
                            new EXECommand[]
                            {
                                new EXECommandQueryUnrelate("o", "s", "R1"),
                            },
                            "observer",
                            "observers"
                        )
                   },
                   "subject",
                   "subjects"
                )
            );

            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<String, String> ExpectedPrimitiveVarState = new Dictionary<string, string> {
                {"x", "10"}
            };
            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Subject", 10},
                { "Observer", 45}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
            };
            Dictionary<String, String> ExpectedCreatedVarState = new Dictionary<String, String>()
            {
            };
            int ExpectedValidRefVarCount = 0;
            int ExpectedValidSetRefVarCount = 0;

            Dictionary<String, String> ActualPrimitiveVarState = Animation.SuperScope.GetStateDictRecursive();
            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            Dictionary<String, String> ActualCreatedVarState = Animation.SuperScope.GetSetRefStateAttrsDictRecursive(Animation.ExecutionSpace, "observers");
            int ActualValidRefVarCount = Animation.SuperScope.ValidVariableReferencingCountRecursive();
            int ActualValidSetRefVarCount = Animation.SuperScope.NonEmptyVariableSetReferencingCountRecursive();

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedCreatedVarState, ActualCreatedVarState);
            Assert.AreEqual(ExpectedValidRefVarCount, ActualValidRefVarCount);
            Assert.AreEqual(ExpectedValidSetRefVarCount, ActualValidSetRefVarCount);
            CollectionAssert.AreEquivalent(ExpectedPrimitiveVarState, ActualPrimitiveVarState);
        }
    }
}