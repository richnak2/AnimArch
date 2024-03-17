using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using OALProgramControl;
using UnityEngine;

namespace Visualization.ClassDiagram
{

public class MethodHighlightSubject : HighlightSubject
{
    public string ClassName {set; get;}
    public string MethodName {set; get;}

    public MethodHighlightSubject() {
        MethodObserver o = new MethodObserver(this);
        this.Attach(o);
    }

}


}
