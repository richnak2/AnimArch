using System;
using System.Collections.Generic;
using UnityEngine;
using Visualisation.Animation;

namespace Visualization.ClassDiagram
{

public class ObjectObserver : Observer
{
        public ObjectObserver(HighlightSubject subject) : base(subject)
        {
        }

        public override void Update()
    {
        ObjectHighlightSubject classHighlightSubject = Subject as ObjectHighlightSubject;
        Debug.LogErrorFormat("subject {0}, cats {1}, dogs {2}", Subject, classHighlightSubject, Subject.GetType());
        if (classHighlightSubject.HighlightInt == 1) {
            Animation.Animation.Instance.HighlightObjects(classHighlightSubject.InvocationInfo,true);
            Animation.Animation.Instance.HighlightObject(classHighlightSubject.InvocationInfo.CalledObject.UniqueID,true);
        }
        else if (classHighlightSubject.HighlightInt == 0) {
            Animation.Animation.Instance.HighlightObjects(classHighlightSubject.InvocationInfo,false);
            Animation.Animation.Instance.HighlightObject(classHighlightSubject.InvocationInfo.CalledObject.UniqueID,false);
        }
        
    }

}



}
