using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using OALProgramControl;
using UnityEngine;

namespace Visualization.ClassDiagram
{

public class EdgeHighlightSubject : HighlightSubject
{
    public MethodInvocationInfo InvocationInfo {get; set;}
    public Animation.Animation.EdgesDrawingFinishedFlag finishedFlag;

    public EdgeHighlightSubject()
    {
        EdgeHighlightObserver o = new EdgeHighlightObserver(this);
        this.Attach(o);
    }

}

}
