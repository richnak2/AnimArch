using UnityEditor;
using UnityEngine;

namespace OALProgramControl
{
    public class EXEASTNodeAccessChainContext
    {
        public bool CreateVariableIfItDoesNotExist { get; set; }
        public string VariableCreationType { get; set; }
        public EXEValueBase CurrentValue { get; set; }
        public string CurrentAccessChain { get; set; }

        public EXEASTNodeAccessChainContext Clone()
        {
            return new EXEASTNodeAccessChainContext()
            {
                CreateVariableIfItDoesNotExist = this.CreateVariableIfItDoesNotExist,
                VariableCreationType = this.VariableCreationType,
                CurrentValue = this.CurrentValue,
                CurrentAccessChain = this.CurrentAccessChain
            };
        }
    }
}