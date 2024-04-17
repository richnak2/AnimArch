using System;
using System.Collections;
using OALProgramControl;
using UnityEngine;
using Visualization.ClassDiagram.ClassComponents;
using Visualization.ClassDiagram.ComponentsInDiagram;
using Visualization.UI;

namespace Visualization.Animation
{
    public class HighlightingTerminateRequest : HighlightingRequest
    {
        private Func<bool> terminateThread;

        public HighlightingTerminateRequest(MethodInvocationInfo call, int threadId, Func<bool> terminateThread) : base(call, threadId)
        {
            this.terminateThread = terminateThread;
        }

        public override IEnumerator PerformRequest()
        {
            terminateThread();
            return null;
        }
    }
}