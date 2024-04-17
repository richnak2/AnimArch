using System.Collections;
using OALProgramControl;
using UnityEngine;
using Visualization.ClassDiagram.ClassComponents;
using Visualization.ClassDiagram.ComponentsInDiagram;
using Visualization.UI;

namespace Visualization.Animation
{
    public class HighlightingCallFunctionRequest : HighlightingRequest
    {
        public HighlightingCallFunctionRequest(MethodInvocationInfo call, int threadId) : base(call, threadId)
        {}

        public override IEnumerator PerformRequest()
        {
            ClassDiagram.Diagrams.ClassDiagram classDiagram = Animation.Instance.classDiagram;
            Class called = classDiagram.FindClassByName(callInfo.CalledMethod.OwningClass.Name).ParsedClass;
            Method calledMethod = classDiagram.FindMethodByName(callInfo.CalledMethod.OwningClass.Name, callInfo.CalledMethod.Name);
            RelationInDiagram relation = classDiagram.FindEdgeInfo(callInfo.Relation?.RelationshipName);

            Animation.assignCallInfoToAllHighlightSubjects(called, calledMethod, relation, callInfo, callInfo.CalledMethod);

            if (relation != null)
            {
                yield return new WaitUntil(() => relation.HighlightSubject.finishedFlag.IsDrawingFinished());
                relation?.HighlightSubject.IncrementHighlightLevel();
                yield return new WaitUntil(() => relation.HighlightSubject.finishedFlag.IsDrawingFinished());
            }
            calledMethod.HighlightObjectSubject.IncrementHighlightLevel();
            called.HighlightSubject.IncrementHighlightLevel();
            calledMethod.HighlightSubject.IncrementHighlightLevel();
            yield return new WaitForSeconds(AnimationData.Instance.AnimSpeed * 1.25f);

            Done = true;
        }
    }
}