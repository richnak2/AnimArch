using UnityEditor;
using UnityEngine;

namespace OALProgramControl
{
    public enum EEvaluationState : int
    {
        NotYetEvaluated = 0,
        IsBeingEvaluated = 1,
        HasBeenEvaluated = 2
    }
}