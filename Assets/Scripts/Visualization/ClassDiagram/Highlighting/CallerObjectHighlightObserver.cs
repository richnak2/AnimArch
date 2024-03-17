using System;
using System.Collections.Generic;
using UnityEngine;
using Visualisation.Animation;

namespace Visualization.ClassDiagram
{

public class CallerObjectHighlightObserver : HighlightObserver
{
        public CallerObjectHighlightObserver(HighlightSubject subject) : base(subject)
        {
        }

        public override void Update()
        {
            CallerObjectHighlightSubject s = Subject as CallerObjectHighlightSubject;
            Animation.Animation a = Animation.Animation.Instance;

            if (s.HighlightInt == 1)
            {
                a.HighlightInstancesMethod(s.InvocationInfo,true);
                a.HighlightObjects(s.InvocationInfo,true);
            }
            else if (s.HighlightInt == 0)
            {
                a.HighlightInstancesMethod(s.InvocationInfo,false);
                a.HighlightObjects(s.InvocationInfo,false);
            }
        }

}



}
