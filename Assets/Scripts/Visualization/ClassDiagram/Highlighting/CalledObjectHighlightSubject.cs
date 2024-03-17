using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using OALProgramControl;
using UnityEngine;

namespace Visualization.ClassDiagram
{

public class CalledObjectHighlightSubject : HighlightSubject
{
    public MethodInvocationInfo InvocationInfo {get; set;}

    public CalledObjectHighlightSubject()
    {
        CalledObjectHighlightObserver o = new CalledObjectHighlightObserver(this);
        this.Attach(o);
    }

}


}
