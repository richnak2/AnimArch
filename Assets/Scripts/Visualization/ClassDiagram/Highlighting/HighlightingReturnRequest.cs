using System.Collections;
using OALProgramControl;
using UnityEngine;
using Visualization.ClassDiagram.ClassComponents;
using Visualization.ClassDiagram.ComponentsInDiagram;
using Visualization.UI;

namespace Visualization.Animation
{
    public class HighlightingReturnRequest : HighlightingRequest
    {
        public HighlightingReturnRequest(MethodInvocationInfo call, int threadId) : base(call, threadId)
        {}

        public override IEnumerator PerformRequest()
        {
            float timeModifier = 1f;
            Animation a = Animation.Instance;

            Class called = a.classDiagram.FindClassByName(callInfo.CalledMethod.OwningClass.Name).ParsedClass;
            Method calledMethod = a.classDiagram.FindMethodByName(callInfo.CalledMethod.OwningClass.Name, callInfo.CalledMethod.Name);
            RelationInDiagram relation = a.classDiagram.FindEdgeInfo(callInfo.Relation?.RelationshipName);
            Animation.assignCallInfoToAllHighlightSubjects(called, calledMethod, relation, callInfo, callInfo.CalledMethod);

            if (relation != null)
            {
                yield return new WaitUntil(() => relation.HighlightSubject.finishedFlag.IsDrawingFinished());
            }

            calledMethod.HighlightSubject.DecrementHighlightLevel();
            calledMethod.HighlightObjectSubject.DecrementHighlightLevel();
            called.HighlightSubject.DecrementHighlightLevel();
            relation?.HighlightSubject.DecrementHighlightLevel();

            if (a.standardPlayMode)
            {
                yield return new WaitForSeconds(AnimationData.Instance.AnimSpeed * timeModifier);
            }

            Done = true;
        }
    }
}