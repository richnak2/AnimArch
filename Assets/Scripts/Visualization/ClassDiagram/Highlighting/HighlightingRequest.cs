using System.Collections;
using OALProgramControl;
using UnityEngine;
using Visualization.UI;

namespace Visualization.Animation
{
    public abstract class HighlightingRequest
    {
        protected bool Done { get; set; }
        protected MethodInvocationInfo callInfo;
        public int threadId {get; protected set; }

        public HighlightingRequest(MethodInvocationInfo call, int threadId)
        {
            this.Done = false;
            this.callInfo = call;
            this.threadId = threadId;
        }

        public bool IsDone()
        {
            return Done;
        }

        public abstract IEnumerator PerformRequest();
    }
}