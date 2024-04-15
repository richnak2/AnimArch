using System.Collections;
using System.Collections.Generic;
using OALProgramControl;
using UnityEngine;
using Visualization.ClassDiagram.ClassComponents;
using Visualization.ClassDiagram.ComponentsInDiagram;
using Visualization.UI;

namespace Visualization.Animation
{
    public class HighlightingCreateObjectRequest : HighlightingRequest
    {
        public HighlightingCreateObjectRequest(MethodInvocationInfo call) : base(call)
        {}

        public override IEnumerator PerformRequest()
        {
            Animation a = Animation.Instance;
            ObjectCreationInfo info = callInfo as ObjectCreationInfo;
            int step = 0;
            float speedPerAnim = AnimationData.Instance.AnimSpeed;
            float timeModifier = 1f;
            IEnumerable<RelationInDiagram> relationsOfClass = a.classDiagram.FindRelationsByClass(info.CalledObject.OwningClass.Name);

            foreach (RelationInDiagram rel in relationsOfClass)
            {
                yield return new WaitUntil(() => rel.HighlightSubject.finishedFlag.IsUnhighlightingFinished());
            }
            Class highlightedClass = a.classDiagram.FindClassByName(info.CalledObject.OwningClass.Name).ParsedClass;
            highlightedClass.HighlightSubject.ClassName = highlightedClass.Name;
            while (step < 7)
            {
                switch (step)
                {
                    case 0:
                        highlightedClass.HighlightSubject.IncrementHighlightLevel();
                        break;
                    case 1:
                        // yield return StartCoroutine(AnimateFillInterGraph(relation));
                        timeModifier = 0f;
                        break;
                    case 3:
                        // relation.Show(); // TODO
                        // relation.Highlight();
                        timeModifier = 1f;
                        break;
                    case 2:
                        a.objectDiagram.ShowObject(info.CreatedObject);
                        timeModifier = 0.5f;
                        break;
                    case 6:
                        highlightedClass.HighlightSubject.DecrementHighlightLevel();
                        // relation.UnHighlight();
                        timeModifier = 1f;
                        break;
                }

                step++;
                yield return new WaitForSeconds(AnimationData.Instance.AnimSpeed * timeModifier);
            }

            a.objectDiagram.AddRelation(info.CallerObject, info.CalledObject, "ASSOCIATION");

            Done = true;
        }
    }
}