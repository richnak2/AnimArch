using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using OALProgramControl;
using UnityEngine;

namespace Visualization.ClassDiagram
{

public class ClassHighlightSubject : HighlightSubject
{
    public string ClassName {set; get;}
    public MethodInvocationInfo InvocationInfo {get; set;}

    public ClassHighlightSubject()
    {
        ClassHighlightObserver o = new ClassHighlightObserver(this);
        this.Attach(o);
    }

}


}
