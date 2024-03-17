using System;
using System.Collections.Generic;
using UnityEngine;
using Visualisation.Animation;

namespace Visualization.ClassDiagram
{

public class MethodObserver : Observer
{
        public MethodObserver(HighlightSubject subject) : base(subject)
        {
        }

        public override void Update()
    {
        MethodHighlightSubject methodHighlightSubject = Subject as MethodHighlightSubject;
        if (methodHighlightSubject.HighlightInt == 1) {
            Animation.Animation.Instance.HighlightMethod(methodHighlightSubject.ClassName, methodHighlightSubject.MethodName, true);
        }
        else if (methodHighlightSubject.HighlightInt == 0) {
            Animation.Animation.Instance.HighlightMethod(methodHighlightSubject.ClassName, methodHighlightSubject.MethodName, false);
        }
        
    }

}



}
