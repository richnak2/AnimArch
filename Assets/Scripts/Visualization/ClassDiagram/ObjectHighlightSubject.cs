using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using OALProgramControl;
using UnityEngine;

namespace Visualization.ClassDiagram
{

public class ObjectHighlightSubject : HighlightSubject
{
    public MethodInvocationInfo InvocationInfo {get; set;}

    public ObjectHighlightSubject() {
        ObjectObserver o = new ObjectObserver(this);
        this.Attach(o);
    }

}


}
