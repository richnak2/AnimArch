using System;
using System.Collections.Generic;
using UnityEngine;

namespace Visualization.ClassDiagram
{

public abstract class HighlightObserver
{
    protected HighlightSubject Subject {set; get;}

    public HighlightObserver(HighlightSubject subject)
    {
        this.Subject = subject;
    }
    public abstract void Update();

}



}
