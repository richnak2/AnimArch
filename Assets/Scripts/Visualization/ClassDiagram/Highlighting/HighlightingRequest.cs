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

        public HighlightingRequest(MethodInvocationInfo call)
        {
            this.Done = false;
            this.callInfo = call;
        }

        public bool IsDone()
        {
            return Done;
        }

        public abstract IEnumerator PerformRequest();
    }
}