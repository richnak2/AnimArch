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

        private bool Over;

        public HighlightingScheduler()
        {
            requestQueues = new Dictionary<int, Queue<HighlightingRequest>>();
            this.Over = false;
        }

        private IEnumerator QueueLoop(Queue<HighlightingRequest> queue)
        {
            HighlightingRequest currentRequest;
            while (true)
            {
                yield return new WaitUntil(() => queue.Any() || Over);

                if (Over) { break; }

                currentRequest = queue.Dequeue();
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
                Animation.Instance.StartCoroutine(QueueLoop(newQueue));
            }
            this.requestQueues[request.threadId].Enqueue(request);
        }

        public void Terminate()
        {
            // this.Over = true;
        }
    }
}