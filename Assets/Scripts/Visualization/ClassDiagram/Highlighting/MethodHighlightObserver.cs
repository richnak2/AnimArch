using System;
using System.Collections.Generic;
using UnityEngine;
using Visualisation.Animation;

namespace Visualization.ClassDiagram
{

public class MethodHighlightObserver : HighlightObserver
{
        public MethodHighlightObserver(HighlightSubject subject) : base(subject)
        {
        }

        public override void Update()
        {
            MethodHighlightSubject methodHighlightSubject = Subject as MethodHighlightSubject;
            Animation.Animation a = Animation.Animation.Instance;

            if (methodHighlightSubject.HighlightInt == 1)
            {
                a.HighlightMethod(methodHighlightSubject.ClassName, methodHighlightSubject.MethodName, true);
            }
            else if (methodHighlightSubject.HighlightInt == 0)
            {
                a.HighlightMethod(methodHighlightSubject.ClassName, methodHighlightSubject.MethodName, false);
            }
        }

}



}
