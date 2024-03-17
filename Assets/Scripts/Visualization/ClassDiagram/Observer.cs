using System;
using System.Collections.Generic;
using UnityEngine;

namespace Visualization.ClassDiagram
{

public abstract class Observer
{
    protected HighlightSubject Subject {set; get;}

    public Observer(HighlightSubject subject) {
        this.Subject = subject;
    }
    public abstract void Update();

}



}
