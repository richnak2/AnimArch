using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace OALProgramControl.Tests
{
    [TestClass]
    public class EXECommandQueryRelateTests
    {
        [TestMethod]
        public void Execute_Normal_01()
        {
            //Set up the execution space
            OALProgram OALProgram = new OALProgram();

            OALProgram.ExecutionSpace.SpawnClass("Observer");
            OALProgram.ExecutionSpace.SpawnClass("Subject");
            String Rel1Name = OALProgram.RelationshipSpace.SpawnRelationship("Observer", "Subject").RelationshipName;

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "s"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryRelate("o", "s", Rel1Name));
            Boolean ExecutionSuccess = OALProgram.Execute();


            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 1},
                { "Subject", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "o", "Observer"},
                { "s", "Subject"}
            };
            List<(String, String, String, String, String)> ExpectedRels = new List<(String, String, String, String, String)>(new (String, String, String, String, String)[] {
                (Rel1Name, "Observer", "o", "Subject", "s")
            });

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            List<(String, String, String, String, String)> ActualRels = TestUtil.CreateRelatedVariableTupples(OALProgram, OALProgram.SuperScope);

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedRels, ActualRels);
        }
        [TestMethod]
        public void Execute_Normal_02()
        {
            //Set up the execution space
            OALProgram OALProgram = new OALProgram();

            OALProgram.ExecutionSpace.SpawnClass("Observer");
            OALProgram.ExecutionSpace.SpawnClass("Subject");
            String Rel1Name = OALProgram.RelationshipSpace.SpawnRelationship("Observer", "Subject").RelationshipName;

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "s"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryRelate("s", "o", Rel1Name));
            Boolean ExecutionSuccess = OALProgram.Execute();


            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 1},
                { "Subject", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "o", "Observer"},
                { "s", "Subject"}
            };
            List<(String, String, String, String, String)> ExpectedRels = new List<(String, String, String, String, String)>(new (String, String, String, String, String)[] {
                (Rel1Name, "Observer", "o", "Subject", "s")
            });

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            List<(String, String, String, String, String)> ActualRels = TestUtil.CreateRelatedVariableTupples(OALProgram, OALProgram.SuperScope);

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedRels, ActualRels);
        }
        [TestMethod]
        public void Execute_Normal_03()
        {
            //Set up the execution space
            OALProgram OALProgram = new OALProgram();

            OALProgram.ExecutionSpace.SpawnClass("Observer");
            OALProgram.ExecutionSpace.SpawnClass("Subject");
            String Rel1Name = OALProgram.RelationshipSpace.SpawnRelationship("Observer", "Subject").RelationshipName;

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o1"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o2"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o3"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "s"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryRelate("o1", "s", Rel1Name));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryRelate("o2", "s", Rel1Name));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryRelate("o3", "s", Rel1Name));
            Boolean ExecutionSuccess = OALProgram.Execute();


            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 3},
                { "Subject", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "o1", "Observer"},
                { "o2", "Observer"},
                { "o3", "Observer"},
                { "s", "Subject"}
            };
            List<(String, String, String, String, String)> ExpectedRels = new List<(String, String, String, String, String)>(new (String, String, String, String, String)[] {
                (Rel1Name, "Observer", "o1", "Subject", "s"),
                (Rel1Name, "Observer", "o2", "Subject", "s"),
                (Rel1Name, "Observer", "o3", "Subject", "s")
            });

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            List<(String, String, String, String, String)> ActualRels = TestUtil.CreateRelatedVariableTupples(OALProgram, OALProgram.SuperScope);

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedRels, ActualRels);
        }
        [TestMethod]
        public void Execute_Normal_04()
        {
            //Set up the execution space
            OALProgram OALProgram = new OALProgram();

            OALProgram.ExecutionSpace.SpawnClass("Observer");
            OALProgram.ExecutionSpace.SpawnClass("Subject");
            String Rel1Name = OALProgram.RelationshipSpace.SpawnRelationship("Observer", "Subject").RelationshipName;

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "s"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o1"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o2"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryRelate("o2", "s", Rel1Name));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o3"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryRelate("o1", "s", Rel1Name));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryRelate("s", "o3", Rel1Name));
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 3},
                { "Subject", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "o1", "Observer"},
                { "o2", "Observer"},
                { "o3", "Observer"},
                { "s", "Subject"}
            };
            List<(String, String, String, String, String)> ExpectedRels = new List<(String, String, String, String, String)>(new (String, String, String, String, String)[] {
                (Rel1Name, "Observer", "o1", "Subject", "s"),
                (Rel1Name, "Observer", "o2", "Subject", "s"),
                (Rel1Name, "Observer", "o3", "Subject", "s")
            });

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            List<(String, String, String, String, String)> ActualRels = TestUtil.CreateRelatedVariableTupples(OALProgram, OALProgram.SuperScope);

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedRels, ActualRels);
        }
        [TestMethod]
        public void Execute_Normal_05()
        {
            //Set up the execution space
            OALProgram OALProgram = new OALProgram();

            OALProgram.ExecutionSpace.SpawnClass("Observer");
            OALProgram.ExecutionSpace.SpawnClass("Subject");
            OALProgram.ExecutionSpace.SpawnClass("Client");
            String Rel1Name = OALProgram.RelationshipSpace.SpawnRelationship("Observer", "Subject").RelationshipName;
            String Rel2Name = OALProgram.RelationshipSpace.SpawnRelationship("Client", "Subject").RelationshipName;

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o1"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o2"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o3"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "s"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Client", "c1"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Client", "c2"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryRelate("o1", "s", Rel1Name));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryRelate("o2", "s", Rel1Name));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryRelate("o3", "s", Rel1Name));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryRelate("s", "c1", Rel2Name));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryRelate("c2", "s", Rel2Name));
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 3},
                { "Subject", 1},
                { "Client", 2}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "o1", "Observer"},
                { "o2", "Observer"},
                { "o3", "Observer"},
                { "s", "Subject"},
                { "c1", "Client"},
                { "c2", "Client"}
            };
            List<(String, String, String, String, String)> ExpectedRels = new List<(String, String, String, String, String)>(new (String, String, String, String, String)[] {
                (Rel1Name, "Observer", "o1", "Subject", "s"),
                (Rel1Name, "Observer", "o2", "Subject", "s"),
                (Rel1Name, "Observer", "o3", "Subject", "s"),
                (Rel2Name, "Client", "c1", "Subject", "s"),
                (Rel2Name, "Client", "c2", "Subject", "s")
            });

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            List<(String, String, String, String, String)> ActualRels = TestUtil.CreateRelatedVariableTupples(OALProgram, OALProgram.SuperScope);

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedRels, ActualRels);
        }
        [TestMethod]
        public void Execute_Bad_01()
        {
            //Set up the execution space
            OALProgram OALProgram = new OALProgram();

            OALProgram.ExecutionSpace.SpawnClass("Observer");
            OALProgram.ExecutionSpace.SpawnClass("Subject");
            String Rel1Name = OALProgram.RelationshipSpace.SpawnRelationship("Observer", "Subject").RelationshipName;

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "s"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryRelate("o", "s", Rel1Name));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryRelate("o", "s", Rel1Name));
            Boolean ExecutionSuccess = OALProgram.Execute();


            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 1},
                { "Subject", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "o", "Observer"},
                { "s", "Subject"}
            };
            List<(String, String, String, String, String)> ExpectedRels = new List<(String, String, String, String, String)>(new (String, String, String, String, String)[] {
                (Rel1Name, "Observer", "o", "Subject", "s")
            });

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            List<(String, String, String, String, String)> ActualRels = TestUtil.CreateRelatedVariableTupples(OALProgram, OALProgram.SuperScope);

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedRels, ActualRels);
        }
        [TestMethod]
        public void Execute_Bad_02()
        {
            //Set up the execution space
            OALProgram OALProgram = new OALProgram();

            OALProgram.ExecutionSpace.SpawnClass("Observer");
            OALProgram.ExecutionSpace.SpawnClass("Subject");
            String Rel1Name = OALProgram.RelationshipSpace.SpawnRelationship("Observer", "Subject").RelationshipName;

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "s"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryRelate("x", "s", Rel1Name));
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 1},
                { "Subject", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "o", "Observer"},
                { "s", "Subject"}
            };
            List<(String, String, String, String, String)> ExpectedRels = new List<(String, String, String, String, String)>(new (String, String, String, String, String)[] {
            });

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            List<(String, String, String, String, String)> ActualRels = TestUtil.CreateRelatedVariableTupples(OALProgram, OALProgram.SuperScope);

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedRels, ActualRels);
        }
        [TestMethod]
        public void Execute_Bad_03()
        {
            //Set up the execution space
            OALProgram OALProgram = new OALProgram();

            OALProgram.ExecutionSpace.SpawnClass("Observer");
            OALProgram.ExecutionSpace.SpawnClass("Subject");
            String Rel1Name = OALProgram.RelationshipSpace.SpawnRelationship("Observer", "Subject").RelationshipName;

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "s"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryRelate("o", "x", Rel1Name));
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 1},
                { "Subject", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "o", "Observer"},
                { "s", "Subject"}
            };
            List<(String, String, String, String, String)> ExpectedRels = new List<(String, String, String, String, String)>(new (String, String, String, String, String)[] {
            });

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            List<(String, String, String, String, String)> ActualRels = TestUtil.CreateRelatedVariableTupples(OALProgram, OALProgram.SuperScope);

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedRels, ActualRels);
        }
        [TestMethod]
        public void Execute_Bad_04()
        {
            //Set up the execution space
            OALProgram OALProgram = new OALProgram();

            OALProgram.ExecutionSpace.SpawnClass("Observer");
            OALProgram.ExecutionSpace.SpawnClass("Subject");
            String Rel1Name = OALProgram.RelationshipSpace.SpawnRelationship("Observer", "Subject").RelationshipName;

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "s"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryRelate("s", "s", Rel1Name));
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 1},
                { "Subject", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "o", "Observer"},
                { "s", "Subject"}
            };
            List<(String, String, String, String, String)> ExpectedRels = new List<(String, String, String, String, String)>(new (String, String, String, String, String)[] {
            });

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            List<(String, String, String, String, String)> ActualRels = TestUtil.CreateRelatedVariableTupples(OALProgram, OALProgram.SuperScope);

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedRels, ActualRels);
        }
        [TestMethod]
        public void Execute_Bad_05()
        {
            //Set up the execution space
            OALProgram OALProgram = new OALProgram();

            OALProgram.ExecutionSpace.SpawnClass("Observer");
            OALProgram.ExecutionSpace.SpawnClass("Subject");
            OALProgram.ExecutionSpace.SpawnClass("Client");
            String Rel1Name = OALProgram.RelationshipSpace.SpawnRelationship("Observer", "Subject").RelationshipName;
            String Rel2Name = OALProgram.RelationshipSpace.SpawnRelationship("Client", "Subject").RelationshipName;

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "s"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Client", "c"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryRelate("c", "s", Rel1Name));
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 1},
                { "Subject", 1},
                { "Client", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "o", "Observer"},
                { "s", "Subject"},
                { "c", "Client"}
            };
            List<(String, String, String, String, String)> ExpectedRels = new List<(String, String, String, String, String)>(new (String, String, String, String, String)[] {
            });

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            List<(String, String, String, String, String)> ActualRels = TestUtil.CreateRelatedVariableTupples(OALProgram, OALProgram.SuperScope);

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedRels, ActualRels);
        }
        [TestMethod]
        public void Execute_Bad_06()
        {
            //Set up the execution space
            OALProgram OALProgram = new OALProgram();

            OALProgram.ExecutionSpace.SpawnClass("Observer");
            OALProgram.ExecutionSpace.SpawnClass("Subject");
            OALProgram.ExecutionSpace.SpawnClass("Client");
            String Rel1Name = OALProgram.RelationshipSpace.SpawnRelationship("Observer", "Subject").RelationshipName;
            String Rel2Name = OALProgram.RelationshipSpace.SpawnRelationship("Client", "Subject").RelationshipName;

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "s"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Client", "c"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryRelate("o", "c", Rel2Name));
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 1},
                { "Subject", 1},
                { "Client", 1}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "o", "Observer"},
                { "s", "Subject"},
                { "c", "Client"}
            };
            List<(String, String, String, String, String)> ExpectedRels = new List<(String, String, String, String, String)>(new (String, String, String, String, String)[] {
            });

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            List<(String, String, String, String, String)> ActualRels = TestUtil.CreateRelatedVariableTupples(OALProgram, OALProgram.SuperScope);

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedRels, ActualRels);
        }
        [TestMethod]
        public void Execute_Bad_07()
        {
            //Set up the execution space
            OALProgram OALProgram = new OALProgram();

            OALProgram.ExecutionSpace.SpawnClass("Observer");
            OALProgram.ExecutionSpace.SpawnClass("Subject");
            OALProgram.ExecutionSpace.SpawnClass("Client");
            String Rel1Name = OALProgram.RelationshipSpace.SpawnRelationship("Observer", "Subject").RelationshipName;
            String Rel2Name = OALProgram.RelationshipSpace.SpawnRelationship("Client", "Subject").RelationshipName;

            OALProgram.SuperScope.AddCommand(new EXECommandQueryRelate("o", "c", Rel2Name));
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 0},
                { "Subject", 0},
                { "Client", 0}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
            };
            List<(String, String, String, String, String)> ExpectedRels = new List<(String, String, String, String, String)>(new (String, String, String, String, String)[] {
            });

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            List<(String, String, String, String, String)> ActualRels = TestUtil.CreateRelatedVariableTupples(OALProgram, OALProgram.SuperScope);

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedRels, ActualRels);
        }
        [TestMethod]
        public void Execute_Bad_08()
        {
            //Set up the execution space
            OALProgram OALProgram = new OALProgram();

            OALProgram.ExecutionSpace.SpawnClass("Observer");
            OALProgram.ExecutionSpace.SpawnClass("Subject");
            OALProgram.ExecutionSpace.SpawnClass("Client");
            String Rel1Name = OALProgram.RelationshipSpace.SpawnRelationship("Observer", "Subject").RelationshipName;
            String Rel2Name = OALProgram.RelationshipSpace.SpawnRelationship("Client", "Subject").RelationshipName;

            OALProgram.SuperScope.AddVariable(new EXEPrimitiveVariable("o", "15"));
            OALProgram.SuperScope.AddVariable(new EXEPrimitiveVariable("c", "15"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryRelate("o", "c", Rel2Name));
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 0},
                { "Subject", 0},
                { "Client", 0}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
            };
            List<(String, String, String, String, String)> ExpectedRels = new List<(String, String, String, String, String)>(new (String, String, String, String, String)[] {
            });

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            List<(String, String, String, String, String)> ActualRels = TestUtil.CreateRelatedVariableTupples(OALProgram, OALProgram.SuperScope);

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedRels, ActualRels);
        }
        [TestMethod]
        public void Execute_Bad_09()
        {
            //Set up the execution space
            OALProgram OALProgram = new OALProgram();

            OALProgram.SuperScope.AddCommand(new EXECommandQueryRelate("o", "c", "R1"));
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
            };
            List<(String, String, String, String, String)> ExpectedRels = new List<(String, String, String, String, String)>(new (String, String, String, String, String)[] {
            });

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            List<(String, String, String, String, String)> ActualRels = TestUtil.CreateRelatedVariableTupples(OALProgram, OALProgram.SuperScope);

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedRels, ActualRels);
        }
        [TestMethod]
        public void Execute_Bad_10()
        {
            //Set up the execution space
            OALProgram OALProgram = new OALProgram();

            OALProgram.ExecutionSpace.SpawnClass("Observer");
            OALProgram.ExecutionSpace.SpawnClass("Subject");
            OALProgram.ExecutionSpace.SpawnClass("Client");

            OALProgram.SuperScope.AddCommand(new EXECommandQueryRelate("o", "c", "R1"));
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 0},
                { "Subject", 0},
                { "Client", 0}
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
            };
            List<(String, String, String, String, String)> ExpectedRels = new List<(String, String, String, String, String)>(new (String, String, String, String, String)[] {
            });

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            List<(String, String, String, String, String)> ActualRels = TestUtil.CreateRelatedVariableTupples(OALProgram, OALProgram.SuperScope);

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedRels, ActualRels);
        }
    }
}