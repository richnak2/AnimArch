using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Msagl.Core.DataStructures;
using UnityEngine;

namespace Visualization.Animation
{
    public class HighlightingScheduler
    {
        private enum ThreadStatus {
            RUNNING = 0,
            TERMINATED = 1
        };

        private struct ThreadQueue {
            public Queue<HighlightingRequest> queue;
            public ThreadStatus status;
        };

        private Dictionary<int, ThreadQueue> requestQueues;
        private bool wantsToterminate;

        public HighlightingScheduler()
        {
            requestQueues = new Dictionary<int, ThreadQueue>();
            wantsToterminate = false;
        }

        public bool IsOver()
        {
            return wantsToterminate && this.requestQueues.Values.All((q) => q.status == ThreadStatus.TERMINATED);
        }

        private IEnumerator QueueLoop(Queue<HighlightingRequest> queue)
        {
            HighlightingRequest currentRequest;
            while (true)
            {
                yield return new WaitUntil(() => queue.Any() || IsOver());

                if (IsOver())
                {
                    break;
                }

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
                this.requestQueues.Add(request.threadId, new ThreadQueue { 
                    queue = newQueue,
                    status = ThreadStatus.RUNNING
                });

                Animation.Instance.StartCoroutine(QueueLoop(newQueue));
            }
            this.requestQueues[request.threadId].queue.Enqueue(request);
        }

        public void Terminate()
        {
            wantsToterminate = true;
            foreach(var pair in this.requestQueues) {
                pair.Value.queue.Enqueue(new HighlightingTerminateRequest(null, pair.Key, () => {
                    ThreadQueue q = requestQueues[pair.Key];
                    q.status = ThreadStatus.TERMINATED;
                    requestQueues[pair.Key] = q;
                    return true;
                }));
            }
            
        }
    }
}