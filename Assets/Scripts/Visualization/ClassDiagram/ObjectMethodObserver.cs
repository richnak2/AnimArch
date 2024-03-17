using System;
using System.Collections.Generic;
using UnityEngine;
using Visualisation.Animation;

namespace Visualization.ClassDiagram
{

public class ObjectMethodObserver : Observer
{
        public ObjectMethodObserver(HighlightSubject subject) : base(subject)
        {
        }

        public override void Update()
    {
        ObjectMethodHighlightSubject classHighlightSubject = Subject as ObjectMethodHighlightSubject;
        if (classHighlightSubject.HighlightInt == 1) {
            Animation.Animation.Instance.HighlightInstancesMethod(classHighlightSubject.InvocationInfo,true);
            Animation.Animation.Instance.HighlightObjectMethod(classHighlightSubject.InvocationInfo.CalledMethod.Name, classHighlightSubject.InvocationInfo.CalledObject.UniqueID,true);
        }
        else if (classHighlightSubject.HighlightInt == 0) {
            Animation.Animation.Instance.HighlightInstancesMethod(classHighlightSubject.InvocationInfo,false);
            Animation.Animation.Instance.HighlightObjectMethod(classHighlightSubject.InvocationInfo.CalledMethod.Name, classHighlightSubject.InvocationInfo.CalledObject.UniqueID,false);
        }
        
    }

}



}
