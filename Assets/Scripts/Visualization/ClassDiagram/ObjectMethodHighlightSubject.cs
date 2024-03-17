using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using OALProgramControl;
using UnityEngine;

namespace Visualization.ClassDiagram
{

public class ObjectMethodHighlightSubject : HighlightSubject
{
    public MethodInvocationInfo InvocationInfo {get; set;}

    public ObjectMethodHighlightSubject() {
        ObjectMethodObserver o = new ObjectMethodObserver(this);
        this.Attach(o);
    }

}


}
