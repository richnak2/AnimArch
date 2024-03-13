using UnityEditor;
using UnityEngine;

namespace OALProgramControl
{
    public interface IReturnCollector
    {
        void CollectReturn(EXEValueBase returnedValue);
        MethodInvocatorInfo GetOriginatorData();
    }
}