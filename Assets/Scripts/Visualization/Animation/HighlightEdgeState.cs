using System;
using System.Collections.Generic;
using OALProgramControl;
using UnityEngine;

namespace Visualization.ClassDiagram
{

public abstract class HighlightEdgeState
{
    public abstract void Highligt(MethodInvocationInfo Call);
    protected HighlightEdgeState() {}
}

}
