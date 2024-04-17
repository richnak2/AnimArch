using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Msagl.Core.DataStructures;
using UnityEngine;

namespace Visualization.Animation
{
    public class HighlightingScheduler
    {
        private readonly Dictionary<int, Queue<HighlightingRequest>> requestQueues;
        private bool wantsToterminate;
        private int runningThreads;

        public HighlightingScheduler()
        {
            requestQueues = new Dictionary<int, Queue<HighlightingRequest>>();
            wantsToterminate = false;
            runningThreads = 0;
        }

        public bool IsOver()
        {
            return wantsToterminate && runningThreads == 0;
        }

        private IEnumerator QueueLoop(Queue<HighlightingRequest> queue)
        {
            HighlightingRequest currentRequest;
            while (true)
            {
                yield return new WaitUntil(() => queue.Any() || IsOver());

                currentRequest = queue.Dequeue();
                if (currentRequest == null)
                {
                    this.runningThreads--;
                    break;
                }
                Animation.Instance.StartCoroutine(currentRequest.PerformRequest());

                yield return new WaitUntil(() => currentRequest.IsDone());
            }
        }

        public void Enqueue(HighlightingRequest request)
        {
            if (!this.requestQueues.ContainsKey(request.threadId))
            {
                Queue<HighlightingRequest> newQueue = new Queue<HighlightingRequest>();
                this.requestQueues.Add(request.threadId, newQueue);
                this.runningThreads++;
                Animation.Instance.StartCoroutine(QueueLoop(newQueue));
            }
            this.requestQueues[request.threadId].Enqueue(request);
        }

        public void Terminate()
        {
            wantsToterminate = true;
            foreach(var pair in this.requestQueues) {
                pair.Value.Enqueue(null);
            }
            
        }
    }
}