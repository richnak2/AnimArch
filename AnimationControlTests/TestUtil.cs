using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnimationControl.Tests
{
    class TestUtil
    {
        public static List<(String, String, String, String, String)> CreateRelatedVariableTupples(Animation Animation, EXEScope Scope)
        {
            List<(String, String, String, String, String)> Result = new List<(String, String, String, String, String)>();
            List<(String, long, long)> RelTupplesID = Animation.RelationshipSpace.GetAllRelationshipsTupples();

            List<(String, String)> Inst1Tupples;
            List<(String, String)> Inst2Tupples;

            foreach ((String, long, long) RelTupple in RelTupplesID)
            {
                if (RelTupple.Item2 == int.MinValue || RelTupple.Item3 == int.MinValue)
                {
                    continue;
                }

                Inst1Tupples = Scope.GetReferencingVariablesByIDRecursive(RelTupple.Item2);
                Inst2Tupples = Scope.GetReferencingVariablesByIDRecursive(RelTupple.Item3);

                foreach ((String, String) Var1 in Inst1Tupples)
                {
                    foreach ((String, String) Var2 in Inst2Tupples)
                    {
                        if (Var1.CompareTo(Var2) <= 0)
                        {
                            Result.Add((RelTupple.Item1, Var1.Item1, Var1.Item2, Var2.Item1, Var2.Item2));
                        }
                        else
                        {
                            Result.Add((RelTupple.Item1, Var2.Item1, Var2.Item2, Var1.Item1, Var1.Item2));
                        }
                    }
                }
            }

            return Result;
        }
        public static void ShuffleList<T>(List<T> List)
        {
            Random random = new Random();
            int n = List.Count();
            int i;
            T temp;
            while (n > 1)
            {
                n--;
                i = random.Next(n + 1);
                temp = List[i];
                List[i] = List[n];
                List[n] = temp;
            }
        }
    }
}
