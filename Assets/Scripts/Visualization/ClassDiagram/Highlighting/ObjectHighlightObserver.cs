using System;
using System.Collections.Generic;
using UnityEngine;
using Visualisation.Animation;

namespace Visualization.ClassDiagram
{

public class ObjectHighlightObserver : HighlightObserver
{
        public ObjectHighlightObserver(HighlightSubject subject) : base(subject)
        {
        }

        public override void Update()
        {
            ObjectHighlightSubject s = Subject as ObjectHighlightSubject;
            Animation.Animation a = Animation.Animation.Instance;

            if (s.HighlightInt == 1)
            {
                a.HighlightObjectMethod(s.InvocationInfo.CalledMethod.Name, s.InvocationInfo.CalledObject.UniqueID,true);
                a.HighlightObject(s.InvocationInfo.CalledObject.UniqueID,true);
            }
            else if (s.HighlightInt == 0)
            {
                a.HighlightObjectMethod(s.InvocationInfo.CalledMethod.Name, s.InvocationInfo.CalledObject.UniqueID,false);
                a.HighlightObject(s.InvocationInfo.CalledObject.UniqueID,false);
            }

        }

}

}
