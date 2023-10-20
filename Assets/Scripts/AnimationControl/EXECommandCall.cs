using UnityEditor;
using UnityEngine;

namespace OALProgramControl
{
    public class EXECommandCall : EXECommandCallBase
    {
        public readonly EXEASTNodeAccessChain MethodAccessChain;

        // V EXECUTE - evaluujem access chain a potom vyrobim EXECommandCallPreevaluated - a ten sa aj bude animovat
        // Tento sa nebude animovat ani v CD, ani jeho src code
    }
}