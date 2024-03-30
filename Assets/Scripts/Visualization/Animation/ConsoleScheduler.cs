using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Visualization.Animation
{
    public class ConsoleScheduler
    {
        private Queue<ConsoleRequest> RequestQueue;
        private bool Over;

        public ConsoleScheduler()
        {
            this.RequestQueue = new Queue<ConsoleRequest>();
            this.Over = false;
        }

        public IEnumerator Start()
        {
            ConsoleRequest currentRequest;
            while (true)
            {
                yield return new WaitUntil(() => RequestQueue.Any() || Over);

                if (Over) { break; }

                currentRequest = RequestQueue.Dequeue();
                currentRequest.PerformRequest();

                yield return new WaitUntil(() => currentRequest.Done);
            }
        }

        public void Enqueue(ConsoleRequest request)
        {
            this.RequestQueue.Enqueue(request);
        }

        public void Terminate()
        {
            this.Over = true;
        }
    }
}