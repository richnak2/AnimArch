using UnityEditor;
using UnityEngine;

namespace OALProgramControl
{
    public class EXEASTNodeAccessChainContext
    {
        public bool CreateVariableIfItDoesNotExist { get; set; }
        public EXEValueBase CurrentValue { get; set; }
        public string CurrentAccessChain { get; set; }
    }
}