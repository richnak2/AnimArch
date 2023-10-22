using UnityEditor;
using UnityEngine;

namespace OALProgramControl
{
    public enum LoopInterruptionRequest : int
    {
        DoNotInterrupt = 0,
        Continue = 1,
        Break =2
    }
}