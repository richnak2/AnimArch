using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using OALProgramControl;
using UnityEngine;

namespace Visualization.ClassDiagram
{

public class CallerObjectHighlightSubject : HighlightSubject
{
    public MethodInvocationInfo InvocationInfo {get; set;}

    public CallerObjectHighlightSubject()
    {
        CallerObjectHighlightObserver o = new CallerObjectHighlightObserver(this);
        this.Attach(o);
    }

}


}
