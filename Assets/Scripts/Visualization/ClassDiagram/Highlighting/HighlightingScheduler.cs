using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Msagl.Core.DataStructures;
using UnityEngine;

namespace Visualization.Animation
{
    public class HighlightingScheduler
    {
        private readonly Queue<HighlightingRequest> requestQueue;

        private readonly List<HighlightingRequest> currentRequests;
        private bool Over;

        public HighlightingScheduler()
        {
            this.requestQueue = new Queue<HighlightingRequest>();
            this.currentRequests = new List<HighlightingRequest>();
            this.Over = false;
        }

        private void scheduleRequest(HighlightingRequest requestToSchedule)
        {
            currentRequests.Add(requestToSchedule);
            Animation.Instance.StartCoroutine(requestToSchedule.PerformRequest());
        }

        public IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitUntil(() => requestQueue.Any() || Over);

                if (Over) { break; }

                HighlightingRequest request = requestQueue.Peek();
                if (currentRequests.Count != 0)
                {
                    if (request.GetType() == currentRequests.First().GetType())
                    {
                        scheduleRequest(requestQueue.Dequeue());
                    }
                    else
                    {
                        foreach (HighlightingRequest r in currentRequests)
                        {
                            yield return new WaitUntil(() => r.IsDone());
                        }
                        currentRequests.Clear();
                    }
                }
                else
                {
                    scheduleRequest(requestQueue.Dequeue());
                }

            }

            yield return new WaitForFixedUpdate();
        }

        public void Enqueue(HighlightingCallFunctionRequest request)
        {
            this.requestQueue.Enqueue(request);
        }
        public void Enqueue(HighlightingCreateObjectRequest request)
        {
            this.requestQueue.Enqueue(request);
        }

        public void Terminate()
        {
            this.Over = true;
        }
    }
}