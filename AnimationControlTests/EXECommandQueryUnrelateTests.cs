using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace OALProgramControl.Tests
{
    [TestClass]
    public class EXECommandQueryUnrelateTests
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
            OALProgram.SuperScope.AddCommand(new EXECommandQueryUnrelate("o", "s", Rel1Name));
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
            OALProgram.SuperScope.AddCommand(new EXECommandQueryRelate("o", "s", Rel1Name));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryUnrelate("s", "o", Rel1Name));
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

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "s"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryRelate("o", "s", Rel1Name));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryUnrelate("o", "s", Rel1Name));
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
        public void Execute_Normal_04()
        {
            //Set up the execution space
            OALProgram OALProgram = new OALProgram();

            OALProgram.ExecutionSpace.SpawnClass("Observer");
            OALProgram.ExecutionSpace.SpawnClass("Subject");
            String Rel1Name = OALProgram.RelationshipSpace.SpawnRelationship("Observer", "Subject").RelationshipName;

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "s"));
            for (int i = 0; i < 1000; i++)
            {
                OALProgram.SuperScope.AddCommand(new EXECommandQueryRelate("o", "s", Rel1Name));
                OALProgram.SuperScope.AddCommand(new EXECommandQueryUnrelate("o", "s", Rel1Name));
            }
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
        public void Execute_Normal_05()
        {
            //Set up the execution space
            OALProgram OALProgram = new OALProgram();

            OALProgram.ExecutionSpace.SpawnClass("Observer");
            OALProgram.ExecutionSpace.SpawnClass("Subject");
            String Rel1Name = OALProgram.RelationshipSpace.SpawnRelationship("Observer", "Subject").RelationshipName;

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "s"));
            for (int i = 0; i < 1000; i++)
            {
                OALProgram.SuperScope.AddCommand(new EXECommandQueryRelate("o", "s", Rel1Name));
                OALProgram.SuperScope.AddCommand(new EXECommandQueryUnrelate("o", "s", Rel1Name));
            }
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

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedRels, ActualRels);
        }
        [TestMethod]
        public void Execute_Normal_06()
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
            OALProgram.SuperScope.AddCommand(new EXECommandQueryRelate("o", "s", Rel1Name));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryRelate("c", "s", Rel2Name));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryUnrelate("o", "s", Rel1Name));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryUnrelate("c", "s", Rel2Name));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryRelate("o", "s", Rel1Name));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryRelate("c", "s", Rel2Name));
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 1},
                { "Subject", 1},
                { "Client", 1},
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "o", "Observer"},
                { "s", "Subject"},
                { "c", "Client"}
            };
            List<(String, String, String, String, String)> ExpectedRels = new List<(String, String, String, String, String)>(new (String, String, String, String, String)[] {
                (Rel1Name, "Observer", "o", "Subject", "s"),
                (Rel2Name, "Client", "c", "Subject", "s")
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
        public void Execute_Normal_07()
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
            OALProgram.SuperScope.AddCommand(new EXECommandQueryRelate("o", "s", Rel1Name));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryRelate("c", "s", Rel2Name));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryUnrelate("o", "s", Rel1Name));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryRelate("o", "s", Rel1Name));
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 1},
                { "Subject", 1},
                { "Client", 1},
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "o", "Observer"},
                { "s", "Subject"},
                { "c", "Client"}
            };
            List<(String, String, String, String, String)> ExpectedRels = new List<(String, String, String, String, String)>(new (String, String, String, String, String)[] {
                (Rel1Name, "Observer", "o", "Subject", "s"),
                (Rel2Name, "Client", "c", "Subject", "s")
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
        public void Execute_Normal_08()
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
            OALProgram.SuperScope.AddCommand(new EXECommandQueryRelate("o", "s", Rel1Name));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryRelate("c", "s", Rel2Name));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryUnrelate("o", "s", Rel1Name));
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 1},
                { "Subject", 1},
                { "Client", 1},
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "o", "Observer"},
                { "s", "Subject"},
                { "c", "Client"}
            };
            List<(String, String, String, String, String)> ExpectedRels = new List<(String, String, String, String, String)>(new (String, String, String, String, String)[] {
                (Rel2Name, "Client", "c", "Subject", "s")
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
        public void Execute_Normal_09()
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
            OALProgram.SuperScope.AddCommand(new EXECommandQueryRelate("o", "s", Rel1Name));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryRelate("c", "s", Rel2Name));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryUnrelate("o", "s", Rel1Name));
            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 1},
                { "Subject", 1},
                { "Client", 1},
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "o", "Observer"},
                { "s", "Subject"},
                { "c", "Client"}
            };
            List<(String, String, String, String, String)> ExpectedRels = new List<(String, String, String, String, String)>(new (String, String, String, String, String)[] {
                (Rel2Name, "Client", "c", "Subject", "s")
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
        public void Execute_Normal_10()
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
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "s1"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "s2"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "s3"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Client", "c1"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Client", "c2"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Client", "c3"));

            List<EXECommandQueryRelate> RelateCommands = new List<EXECommandQueryRelate>(new EXECommandQueryRelate[] {
                new EXECommandQueryRelate("o1", "s1", Rel1Name),
                new EXECommandQueryRelate("o1", "s2", Rel1Name),
                new EXECommandQueryRelate("o1", "s3", Rel1Name),
                new EXECommandQueryRelate("o2", "s1", Rel1Name),
                new EXECommandQueryRelate("o2", "s2", Rel1Name),
                new EXECommandQueryRelate("o2", "s3", Rel1Name),
                new EXECommandQueryRelate("o3", "s1", Rel1Name),
                new EXECommandQueryRelate("o3", "s2", Rel1Name),
                new EXECommandQueryRelate("o3", "s3", Rel1Name),
                new EXECommandQueryRelate("c1", "s1", Rel2Name),
                new EXECommandQueryRelate("c2", "s1", Rel2Name),
                new EXECommandQueryRelate("c3", "s1", Rel2Name),
                new EXECommandQueryRelate("c1", "s2", Rel2Name),
                new EXECommandQueryRelate("c2", "s2", Rel2Name),
                new EXECommandQueryRelate("c3", "s2", Rel2Name),
                new EXECommandQueryRelate("c1", "s3", Rel2Name),
                new EXECommandQueryRelate("c2", "s3", Rel2Name),
                new EXECommandQueryRelate("c3", "s3", Rel2Name)
            });
            List<EXECommandQueryUnrelate> UnrelateCommands = new List<EXECommandQueryUnrelate>(new EXECommandQueryUnrelate[] {
                new EXECommandQueryUnrelate("o1", "s1", Rel1Name),
                new EXECommandQueryUnrelate("o1", "s2", Rel1Name),
                new EXECommandQueryUnrelate("o1", "s3", Rel1Name),
                new EXECommandQueryUnrelate("o2", "s1", Rel1Name),
                new EXECommandQueryUnrelate("o2", "s2", Rel1Name),
                new EXECommandQueryUnrelate("o2", "s3", Rel1Name),
                new EXECommandQueryUnrelate("o3", "s1", Rel1Name),
                new EXECommandQueryUnrelate("o3", "s2", Rel1Name),
                new EXECommandQueryUnrelate("o3", "s3", Rel1Name),
                new EXECommandQueryUnrelate("c1", "s1", Rel2Name),
                new EXECommandQueryUnrelate("c2", "s1", Rel2Name),
                new EXECommandQueryUnrelate("c3", "s1", Rel2Name),
                new EXECommandQueryUnrelate("c1", "s2", Rel2Name),
                new EXECommandQueryUnrelate("c2", "s2", Rel2Name),
                new EXECommandQueryUnrelate("c3", "s2", Rel2Name),
                new EXECommandQueryUnrelate("c1", "s3", Rel2Name),
                new EXECommandQueryUnrelate("c2", "s3", Rel2Name),
                new EXECommandQueryUnrelate("c3", "s3", Rel2Name)
            });

            for (int i = 0; i < 1000; i++)
            {
                TestUtil.ShuffleList<EXECommandQueryRelate>(RelateCommands);
                TestUtil.ShuffleList<EXECommandQueryUnrelate>(UnrelateCommands);

                foreach (EXECommandQueryRelate Command in RelateCommands)
                {
                    OALProgram.SuperScope.AddCommand(Command);
                }
                foreach (EXECommandQueryUnrelate Command in UnrelateCommands)
                {
                    OALProgram.SuperScope.AddCommand(Command);
                }
            }


            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 3},
                { "Subject", 3},
                { "Client", 3},
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "o1", "Observer"},
                { "o2", "Observer"},
                { "o3", "Observer"},
                { "s1", "Subject"},
                { "s2", "Subject"},
                { "s3", "Subject"},
                { "c1", "Client"},
                { "c2", "Client"},
                { "c3", "Client"}
            };
            List<(String, String, String, String, String)> ExpectedRels = new List<(String, String, String, String, String)>(new (String, String, String, String, String)[] {
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
        public void Execute_Normal_11()
        {
            //Set up the execution space
            OALProgram OALProgram = new OALProgram();

            OALProgram.ExecutionSpace.SpawnClass("Observer");
            OALProgram.ExecutionSpace.SpawnClass("Subject");
            OALProgram.ExecutionSpace.SpawnClass("Client");
            String Rel1Name = OALProgram.RelationshipSpace.SpawnRelationship("Observer", "Subject").RelationshipName;
            String Rel2Name = OALProgram.RelationshipSpace.SpawnRelationship("Client", "Subject").RelationshipName;

            EXEScope SubScope = new EXEScope();
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o1"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o2"));
            SubScope.AddCommand(new EXECommandQueryCreate("Observer", "o3"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "s1"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "s2"));
            SubScope.AddCommand(new EXECommandQueryCreate("Subject", "s3"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Client", "c1"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Client", "c2"));
            SubScope.AddCommand(new EXECommandQueryCreate("Client", "c3"));

            List<EXECommandQueryRelate> RelateCommands = new List<EXECommandQueryRelate>(new EXECommandQueryRelate[] {
                new EXECommandQueryRelate("o1", "s1", Rel1Name),
                new EXECommandQueryRelate("o1", "s2", Rel1Name),
                new EXECommandQueryRelate("o1", "s3", Rel1Name),
                new EXECommandQueryRelate("o2", "s1", Rel1Name),
                new EXECommandQueryRelate("o2", "s2", Rel1Name),
                new EXECommandQueryRelate("o2", "s3", Rel1Name),
                new EXECommandQueryRelate("o3", "s1", Rel1Name),
                new EXECommandQueryRelate("o3", "s2", Rel1Name),
                new EXECommandQueryRelate("o3", "s3", Rel1Name),
                new EXECommandQueryRelate("c1", "s1", Rel2Name),
                new EXECommandQueryRelate("c2", "s1", Rel2Name),
                new EXECommandQueryRelate("c3", "s1", Rel2Name),
                new EXECommandQueryRelate("c1", "s2", Rel2Name),
                new EXECommandQueryRelate("c2", "s2", Rel2Name),
                new EXECommandQueryRelate("c3", "s2", Rel2Name),
                new EXECommandQueryRelate("c1", "s3", Rel2Name),
                new EXECommandQueryRelate("c2", "s3", Rel2Name),
                new EXECommandQueryRelate("c3", "s3", Rel2Name)
            });
            List<EXECommandQueryUnrelate> UnrelateCommands = new List<EXECommandQueryUnrelate>(new EXECommandQueryUnrelate[] {
                new EXECommandQueryUnrelate("o1", "s1", Rel1Name),
                new EXECommandQueryUnrelate("o1", "s2", Rel1Name),
                new EXECommandQueryUnrelate("o1", "s3", Rel1Name),
                new EXECommandQueryUnrelate("o2", "s1", Rel1Name),
                new EXECommandQueryUnrelate("o2", "s2", Rel1Name),
                new EXECommandQueryUnrelate("o2", "s3", Rel1Name),
                new EXECommandQueryUnrelate("o3", "s1", Rel1Name),
                new EXECommandQueryUnrelate("o3", "s2", Rel1Name),
                new EXECommandQueryUnrelate("o3", "s3", Rel1Name),
                new EXECommandQueryUnrelate("c1", "s1", Rel2Name),
                new EXECommandQueryUnrelate("c2", "s1", Rel2Name),
                new EXECommandQueryUnrelate("c3", "s1", Rel2Name),
                new EXECommandQueryUnrelate("c1", "s2", Rel2Name),
                new EXECommandQueryUnrelate("c2", "s2", Rel2Name),
                new EXECommandQueryUnrelate("c3", "s2", Rel2Name),
                new EXECommandQueryUnrelate("c1", "s3", Rel2Name),
                new EXECommandQueryUnrelate("c2", "s3", Rel2Name),
                new EXECommandQueryUnrelate("c3", "s3", Rel2Name)
            });

            for (int i = 0; i < 1000; i++)
            {
                TestUtil.ShuffleList<EXECommandQueryRelate>(RelateCommands);
                TestUtil.ShuffleList<EXECommandQueryUnrelate>(UnrelateCommands);

                foreach (EXECommandQueryRelate Command in RelateCommands)
                {
                    SubScope.AddCommand(Command);
                }
                foreach (EXECommandQueryUnrelate Command in UnrelateCommands)
                {
                    SubScope.AddCommand(Command);
                }
            }
            OALProgram.SuperScope.AddCommand(SubScope);

            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 3},
                { "Subject", 3},
                { "Client", 3},
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "o1", "Observer"},
                { "o2", "Observer"},
                { "s1", "Subject"},
                { "s2", "Subject"},
                { "c1", "Client"},
                { "c2", "Client"}
            };
            List<(String, String, String, String, String)> ExpectedRels = new List<(String, String, String, String, String)>(new (String, String, String, String, String)[] {
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
        public void Execute_Normal_12()
        {
            //Set up the execution space
            OALProgram OALProgram = new OALProgram();

            OALProgram.ExecutionSpace.SpawnClass("Observer");
            OALProgram.ExecutionSpace.SpawnClass("Subject");
            OALProgram.ExecutionSpace.SpawnClass("Client");
            String Rel1Name = OALProgram.RelationshipSpace.SpawnRelationship("Observer", "Subject").RelationshipName;
            String Rel2Name = OALProgram.RelationshipSpace.SpawnRelationship("Client", "Subject").RelationshipName;

            EXEScope SubScope = new EXEScope();
            SubScope.AddCommand(new EXECommandQueryCreate("Observer", "o1"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o2"));
            SubScope.AddCommand(new EXECommandQueryCreate("Observer", "o3"));
            SubScope.AddCommand(new EXECommandQueryCreate("Subject", "s1"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "s2"));
            SubScope.AddCommand(new EXECommandQueryCreate("Subject", "s3"));
            SubScope.AddCommand(new EXECommandQueryCreate("Client", "c1"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Client", "c2"));
            SubScope.AddCommand(new EXECommandQueryCreate("Client", "c3"));

            List<EXECommandQueryRelate> RelateCommands = new List<EXECommandQueryRelate>(new EXECommandQueryRelate[] {
                new EXECommandQueryRelate("o1", "s1", Rel1Name),
                new EXECommandQueryRelate("o1", "s2", Rel1Name),
                new EXECommandQueryRelate("o1", "s3", Rel1Name),
                new EXECommandQueryRelate("o2", "s1", Rel1Name),
                new EXECommandQueryRelate("o2", "s2", Rel1Name),
                new EXECommandQueryRelate("o2", "s3", Rel1Name),
                new EXECommandQueryRelate("o3", "s1", Rel1Name),
                new EXECommandQueryRelate("o3", "s2", Rel1Name),
                new EXECommandQueryRelate("o3", "s3", Rel1Name),
                new EXECommandQueryRelate("c1", "s1", Rel2Name),
                new EXECommandQueryRelate("c2", "s1", Rel2Name),
                new EXECommandQueryRelate("c3", "s1", Rel2Name),
                new EXECommandQueryRelate("c1", "s2", Rel2Name),
                new EXECommandQueryRelate("c2", "s2", Rel2Name),
                new EXECommandQueryRelate("c3", "s2", Rel2Name),
                new EXECommandQueryRelate("c1", "s3", Rel2Name),
                new EXECommandQueryRelate("c2", "s3", Rel2Name),
                new EXECommandQueryRelate("c3", "s3", Rel2Name)
            });
            List<EXECommandQueryUnrelate> UnrelateCommands = new List<EXECommandQueryUnrelate>(new EXECommandQueryUnrelate[] {
                new EXECommandQueryUnrelate("o1", "s1", Rel1Name),
                new EXECommandQueryUnrelate("o1", "s2", Rel1Name),
                new EXECommandQueryUnrelate("o1", "s3", Rel1Name),
                new EXECommandQueryUnrelate("o2", "s1", Rel1Name),
                new EXECommandQueryUnrelate("o2", "s2", Rel1Name),
                new EXECommandQueryUnrelate("o2", "s3", Rel1Name),
                new EXECommandQueryUnrelate("o3", "s1", Rel1Name),
                new EXECommandQueryUnrelate("o3", "s2", Rel1Name),
                new EXECommandQueryUnrelate("o3", "s3", Rel1Name),
                new EXECommandQueryUnrelate("c1", "s1", Rel2Name),
                new EXECommandQueryUnrelate("c2", "s1", Rel2Name),
                new EXECommandQueryUnrelate("c3", "s1", Rel2Name),
                new EXECommandQueryUnrelate("c1", "s2", Rel2Name),
                new EXECommandQueryUnrelate("c2", "s2", Rel2Name),
                new EXECommandQueryUnrelate("c3", "s2", Rel2Name),
                new EXECommandQueryUnrelate("c1", "s3", Rel2Name),
                new EXECommandQueryUnrelate("c2", "s3", Rel2Name),
                new EXECommandQueryUnrelate("c3", "s3", Rel2Name)
            });

            for (int i = 0; i < 1000; i++)
            {
                TestUtil.ShuffleList<EXECommandQueryRelate>(RelateCommands);
                TestUtil.ShuffleList<EXECommandQueryUnrelate>(UnrelateCommands);

                foreach (EXECommandQueryRelate Command in RelateCommands)
                {
                    SubScope.AddCommand(Command);
                }
                foreach (EXECommandQueryUnrelate Command in UnrelateCommands)
                {
                    SubScope.AddCommand(Command);
                }
            }
            TestUtil.ShuffleList<EXECommandQueryRelate>(RelateCommands);
            foreach (EXECommandQueryRelate Command in RelateCommands)
            {
                SubScope.AddCommand(Command);
            }
            OALProgram.SuperScope.AddCommand(SubScope);

            Boolean ExecutionSuccess = OALProgram.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
                { "Observer", 3},
                { "Subject", 3},
                { "Client", 3},
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
                { "o2", "Observer"},
                { "s2", "Subject"},
                { "c2", "Client"}
            };
            List<(String, String, String, String, String)> ExpectedRels = new List<(String, String, String, String, String)>(new (String, String, String, String, String)[] {
                (Rel1Name, "Observer", "o1", "Subject", "s1"),
                (Rel1Name, "Observer", "o1", "Subject", "s2"),
                (Rel1Name, "Observer", "o1", "Subject", "s3"),
                (Rel1Name, "Observer", "o2", "Subject", "s1"),
                (Rel1Name, "Observer", "o2", "Subject", "s2"),
                (Rel1Name, "Observer", "o2", "Subject", "s3"),
                (Rel1Name, "Observer", "o3", "Subject", "s1"),
                (Rel1Name, "Observer", "o3", "Subject", "s2"),
                (Rel1Name, "Observer", "o3", "Subject", "s3"),
                (Rel2Name, "Client", "c1", "Subject", "s1"),
                (Rel2Name, "Client", "c1", "Subject", "s2"),
                (Rel2Name, "Client", "c1", "Subject", "s3"),
                (Rel2Name, "Client", "c2", "Subject", "s1"),
                (Rel2Name, "Client", "c2", "Subject", "s2"),
                (Rel2Name, "Client", "c2", "Subject", "s3"),
                (Rel2Name, "Client", "c3", "Subject", "s1"),
                (Rel2Name, "Client", "c3", "Subject", "s2"),
                (Rel2Name, "Client", "c3", "Subject", "s3"),
            });

            Dictionary<string, int> ActualInstanceDBHist = OALProgram.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = OALProgram.SuperScope.GetRefStateDictRecursive();
            List<(String, String, String, String, String)> ActualRels = TestUtil.CreateRelatedVariableTupples(OALProgram, SubScope);

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
            OALProgram.SuperScope.AddCommand(new EXECommandQueryUnrelate("o", "s", Rel1Name));
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
        public void Execute_Bad_02()
        {
            //Set up the execution space
            OALProgram OALProgram = new OALProgram();

            OALProgram.ExecutionSpace.SpawnClass("Observer");
            OALProgram.ExecutionSpace.SpawnClass("Subject");
            String Rel1Name = OALProgram.RelationshipSpace.SpawnRelationship("Observer", "Subject").RelationshipName;

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "s"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryRelate("o", "s", Rel1Name));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryUnrelate("o", "s", Rel1Name));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryUnrelate("o", "s", Rel1Name));
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
            OALProgram.SuperScope.AddCommand(new EXECommandQueryRelate("o", "s", Rel1Name));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryUnrelate("c", "s", Rel1Name));
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
        public void Execute_Bad_04()
        {
            //Set up the execution space
            OALProgram OALProgram = new OALProgram();

            OALProgram.ExecutionSpace.SpawnClass("Observer");
            OALProgram.ExecutionSpace.SpawnClass("Subject");
            String Rel1Name = OALProgram.RelationshipSpace.SpawnRelationship("Observer", "Subject").RelationshipName;

            OALProgram.SuperScope.AddVariable(new EXEPrimitiveVariable("c", "80"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "s"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryRelate("o", "s", Rel1Name));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryUnrelate("c", "s", Rel1Name));
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
        public void Execute_Bad_05()
        {
            //Set up the execution space
            OALProgram OALProgram = new OALProgram();

            OALProgram.ExecutionSpace.SpawnClass("Observer");
            OALProgram.ExecutionSpace.SpawnClass("Subject");

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "s"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryUnrelate("o", "s", "R1"));
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
        public void Execute_Bad_06()
        {
            //Set up the execution space
            OALProgram OALProgram = new OALProgram();

            OALProgram.ExecutionSpace.SpawnClass("Observer");
            OALProgram.ExecutionSpace.SpawnClass("Subject");

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "s"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryUnrelate("o", "s", "R1"));
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
        public void Execute_Bad_07()
        {
            //Set up the execution space
            OALProgram OALProgram = new OALProgram();

            OALProgram.SuperScope.AddCommand(new EXECommandQueryUnrelate("o", "s", "R1"));
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
        public void Execute_Bad_08()
        {
            //Set up the execution space
            OALProgram OALProgram = new OALProgram();

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "s"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryDelete("o"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryDelete("s"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryUnrelate("o", "s", "R1"));
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
        public void Execute_Bad_09()
        {
            //Set up the execution space
            OALProgram OALProgram = new OALProgram();

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "s"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryDelete("o"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryUnrelate("o", "s", "R1"));
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

            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "s"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryDelete("s"));
            OALProgram.SuperScope.AddCommand(new EXECommandQueryUnrelate("o", "s", "R1"));
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
    }
}