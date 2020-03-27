using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace AnimationControl.Tests
{
    [TestClass]
    public class EXECommandQueryRelateTests
    {
        [TestMethod]
        public void Execute_Normal_01()
        {
            //Set up the execution space
            Animation Animation = new Animation();

            Animation.ExecutionSpace.SpawnClass("Observer");
            Animation.ExecutionSpace.SpawnClass("Subject");
            String Rel1Name = Animation.RelationshipSpace.SpawnRelationship("Observer", "Subject").RelationshipName;

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "s"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("o", "s", Rel1Name));
            Boolean ExecutionSuccess = Animation.Execute();


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

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            List<(String, String, String, String, String)> ActualRels = TestUtil.CreateRelatedVariableTupples(Animation, Animation.SuperScope);

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedRels, ActualRels);
        }
        [TestMethod]
        public void Execute_Normal_02()
        {
            //Set up the execution space
            Animation Animation = new Animation();

            Animation.ExecutionSpace.SpawnClass("Observer");
            Animation.ExecutionSpace.SpawnClass("Subject");
            String Rel1Name = Animation.RelationshipSpace.SpawnRelationship("Observer", "Subject").RelationshipName;

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "s"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("s", "o", Rel1Name));
            Boolean ExecutionSuccess = Animation.Execute();


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

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            List<(String, String, String, String, String)> ActualRels = TestUtil.CreateRelatedVariableTupples(Animation, Animation.SuperScope);

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedRels, ActualRels);
        }
        [TestMethod]
        public void Execute_Normal_03()
        {
            //Set up the execution space
            Animation Animation = new Animation();

            Animation.ExecutionSpace.SpawnClass("Observer");
            Animation.ExecutionSpace.SpawnClass("Subject");
            String Rel1Name = Animation.RelationshipSpace.SpawnRelationship("Observer", "Subject").RelationshipName;

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o2"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o3"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "s"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("o1", "s", Rel1Name));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("o2", "s", Rel1Name));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("o3", "s", Rel1Name));
            Boolean ExecutionSuccess = Animation.Execute();


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

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            List<(String, String, String, String, String)> ActualRels = TestUtil.CreateRelatedVariableTupples(Animation, Animation.SuperScope);

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedRels, ActualRels);
        }
        [TestMethod]
        public void Execute_Normal_04()
        {
            //Set up the execution space
            Animation Animation = new Animation();

            Animation.ExecutionSpace.SpawnClass("Observer");
            Animation.ExecutionSpace.SpawnClass("Subject");
            String Rel1Name = Animation.RelationshipSpace.SpawnRelationship("Observer", "Subject").RelationshipName;

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "s"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o2"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("o2", "s", Rel1Name));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o3"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("o1", "s", Rel1Name));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("s", "o3", Rel1Name));
            Boolean ExecutionSuccess = Animation.Execute();

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

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            List<(String, String, String, String, String)> ActualRels = TestUtil.CreateRelatedVariableTupples(Animation, Animation.SuperScope);

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedRels, ActualRels);
        }
        [TestMethod]
        public void Execute_Normal_05()
        {
            //Set up the execution space
            Animation Animation = new Animation();

            Animation.ExecutionSpace.SpawnClass("Observer");
            Animation.ExecutionSpace.SpawnClass("Subject");
            Animation.ExecutionSpace.SpawnClass("Client");
            String Rel1Name = Animation.RelationshipSpace.SpawnRelationship("Observer", "Subject").RelationshipName;
            String Rel2Name = Animation.RelationshipSpace.SpawnRelationship("Client", "Subject").RelationshipName;

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o2"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o3"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "s"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Client", "c1"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Client", "c2"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("o1", "s", Rel1Name));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("o2", "s", Rel1Name));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("o3", "s", Rel1Name));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("s", "c1", Rel2Name));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("c2", "s", Rel2Name));
            Boolean ExecutionSuccess = Animation.Execute();

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

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            List<(String, String, String, String, String)> ActualRels = TestUtil.CreateRelatedVariableTupples(Animation, Animation.SuperScope);

            Assert.IsTrue(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedRels, ActualRels);
        }
        [TestMethod]
        public void Execute_Bad_01()
        {
            //Set up the execution space
            Animation Animation = new Animation();

            Animation.ExecutionSpace.SpawnClass("Observer");
            Animation.ExecutionSpace.SpawnClass("Subject");
            String Rel1Name = Animation.RelationshipSpace.SpawnRelationship("Observer", "Subject").RelationshipName;

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "s"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("o", "s", Rel1Name));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("o", "s", Rel1Name));
            Boolean ExecutionSuccess = Animation.Execute();


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

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            List<(String, String, String, String, String)> ActualRels = TestUtil.CreateRelatedVariableTupples(Animation, Animation.SuperScope);

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedRels, ActualRels);
        }
        [TestMethod]
        public void Execute_Bad_02()
        {
            //Set up the execution space
            Animation Animation = new Animation();

            Animation.ExecutionSpace.SpawnClass("Observer");
            Animation.ExecutionSpace.SpawnClass("Subject");
            String Rel1Name = Animation.RelationshipSpace.SpawnRelationship("Observer", "Subject").RelationshipName;

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "s"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("x", "s", Rel1Name));
            Boolean ExecutionSuccess = Animation.Execute();

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

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            List<(String, String, String, String, String)> ActualRels = TestUtil.CreateRelatedVariableTupples(Animation, Animation.SuperScope);

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedRels, ActualRels);
        }
        [TestMethod]
        public void Execute_Bad_03()
        {
            //Set up the execution space
            Animation Animation = new Animation();

            Animation.ExecutionSpace.SpawnClass("Observer");
            Animation.ExecutionSpace.SpawnClass("Subject");
            String Rel1Name = Animation.RelationshipSpace.SpawnRelationship("Observer", "Subject").RelationshipName;

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "s"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("o", "x", Rel1Name));
            Boolean ExecutionSuccess = Animation.Execute();

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

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            List<(String, String, String, String, String)> ActualRels = TestUtil.CreateRelatedVariableTupples(Animation, Animation.SuperScope);

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedRels, ActualRels);
        }
        [TestMethod]
        public void Execute_Bad_04()
        {
            //Set up the execution space
            Animation Animation = new Animation();

            Animation.ExecutionSpace.SpawnClass("Observer");
            Animation.ExecutionSpace.SpawnClass("Subject");
            String Rel1Name = Animation.RelationshipSpace.SpawnRelationship("Observer", "Subject").RelationshipName;

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "s"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("s", "s", Rel1Name));
            Boolean ExecutionSuccess = Animation.Execute();

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

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            List<(String, String, String, String, String)> ActualRels = TestUtil.CreateRelatedVariableTupples(Animation, Animation.SuperScope);

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedRels, ActualRels);
        }
        [TestMethod]
        public void Execute_Bad_05()
        {
            //Set up the execution space
            Animation Animation = new Animation();

            Animation.ExecutionSpace.SpawnClass("Observer");
            Animation.ExecutionSpace.SpawnClass("Subject");
            Animation.ExecutionSpace.SpawnClass("Client");
            String Rel1Name = Animation.RelationshipSpace.SpawnRelationship("Observer", "Subject").RelationshipName;
            String Rel2Name = Animation.RelationshipSpace.SpawnRelationship("Client", "Subject").RelationshipName;

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "s"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Client", "c"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("c", "s", Rel1Name));
            Boolean ExecutionSuccess = Animation.Execute();

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

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            List<(String, String, String, String, String)> ActualRels = TestUtil.CreateRelatedVariableTupples(Animation, Animation.SuperScope);

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedRels, ActualRels);
        }
        [TestMethod]
        public void Execute_Bad_06()
        {
            //Set up the execution space
            Animation Animation = new Animation();

            Animation.ExecutionSpace.SpawnClass("Observer");
            Animation.ExecutionSpace.SpawnClass("Subject");
            Animation.ExecutionSpace.SpawnClass("Client");
            String Rel1Name = Animation.RelationshipSpace.SpawnRelationship("Observer", "Subject").RelationshipName;
            String Rel2Name = Animation.RelationshipSpace.SpawnRelationship("Client", "Subject").RelationshipName;

            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Observer", "o"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Subject", "s"));
            Animation.SuperScope.AddCommand(new EXECommandQueryCreate("Client", "c"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("o", "c", Rel2Name));
            Boolean ExecutionSuccess = Animation.Execute();

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

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            List<(String, String, String, String, String)> ActualRels = TestUtil.CreateRelatedVariableTupples(Animation, Animation.SuperScope);

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedRels, ActualRels);
        }
        [TestMethod]
        public void Execute_Bad_07()
        {
            //Set up the execution space
            Animation Animation = new Animation();

            Animation.ExecutionSpace.SpawnClass("Observer");
            Animation.ExecutionSpace.SpawnClass("Subject");
            Animation.ExecutionSpace.SpawnClass("Client");
            String Rel1Name = Animation.RelationshipSpace.SpawnRelationship("Observer", "Subject").RelationshipName;
            String Rel2Name = Animation.RelationshipSpace.SpawnRelationship("Client", "Subject").RelationshipName;

            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("o", "c", Rel2Name));
            Boolean ExecutionSuccess = Animation.Execute();

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

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            List<(String, String, String, String, String)> ActualRels = TestUtil.CreateRelatedVariableTupples(Animation, Animation.SuperScope);

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedRels, ActualRels);
        }
        [TestMethod]
        public void Execute_Bad_08()
        {
            //Set up the execution space
            Animation Animation = new Animation();

            Animation.ExecutionSpace.SpawnClass("Observer");
            Animation.ExecutionSpace.SpawnClass("Subject");
            Animation.ExecutionSpace.SpawnClass("Client");
            String Rel1Name = Animation.RelationshipSpace.SpawnRelationship("Observer", "Subject").RelationshipName;
            String Rel2Name = Animation.RelationshipSpace.SpawnRelationship("Client", "Subject").RelationshipName;

            Animation.SuperScope.AddVariable(new EXEPrimitiveVariable("o", "15"));
            Animation.SuperScope.AddVariable(new EXEPrimitiveVariable("c", "15"));
            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("o", "c", Rel2Name));
            Boolean ExecutionSuccess = Animation.Execute();

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

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            List<(String, String, String, String, String)> ActualRels = TestUtil.CreateRelatedVariableTupples(Animation, Animation.SuperScope);

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedRels, ActualRels);
        }
        [TestMethod]
        public void Execute_Bad_09()
        {
            //Set up the execution space
            Animation Animation = new Animation();

            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("o", "c", "R1"));
            Boolean ExecutionSuccess = Animation.Execute();

            Dictionary<string, int> ExpectedInstanceDBHist = new Dictionary<string, int>()
            {
            };
            Dictionary<string, string> ExpectedScopeVars = new Dictionary<string, string>()
            {
            };
            List<(String, String, String, String, String)> ExpectedRels = new List<(String, String, String, String, String)>(new (String, String, String, String, String)[] {
            });

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            List<(String, String, String, String, String)> ActualRels = TestUtil.CreateRelatedVariableTupples(Animation, Animation.SuperScope);

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedRels, ActualRels);
        }
        [TestMethod]
        public void Execute_Bad_10()
        {
            //Set up the execution space
            Animation Animation = new Animation();

            Animation.ExecutionSpace.SpawnClass("Observer");
            Animation.ExecutionSpace.SpawnClass("Subject");
            Animation.ExecutionSpace.SpawnClass("Client");

            Animation.SuperScope.AddCommand(new EXECommandQueryRelate("o", "c", "R1"));
            Boolean ExecutionSuccess = Animation.Execute();

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

            Dictionary<string, int> ActualInstanceDBHist = Animation.ExecutionSpace.ProduceInstanceHistogram();
            Dictionary<string, string> ActualScopeVars = Animation.SuperScope.GetRefStateDictRecursive();
            List<(String, String, String, String, String)> ActualRels = TestUtil.CreateRelatedVariableTupples(Animation, Animation.SuperScope);

            Assert.IsFalse(ExecutionSuccess);
            CollectionAssert.AreEquivalent(ExpectedInstanceDBHist, ActualInstanceDBHist);
            CollectionAssert.AreEquivalent(ExpectedScopeVars, ActualScopeVars);
            CollectionAssert.AreEquivalent(ExpectedRels, ActualRels);
        }
    }
}