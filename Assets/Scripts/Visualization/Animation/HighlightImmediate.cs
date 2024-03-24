using System;
using System.Collections.Generic;
using OALProgramControl;
using UnityEngine;

namespace Visualization.ClassDiagram
{

public class HighlightImmediate : HighlightEdgeState
{

    protected static HighlightImmediate instance = null;
    public static HighlightImmediate GetInstance()
    {
        if (instance == null)
        {
            instance = new HighlightImmediate();
        }
        return instance;
    }
    public override void Highligt(MethodInvocationInfo Call)
    {
        Animation.Animation a = Animation.Animation.Instance;
        a.HighlightEdge(Call.Relation?.RelationshipName, true, Call);
    }

}

}