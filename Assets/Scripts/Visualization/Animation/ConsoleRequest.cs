using System.Collections;
using UnityEngine;
using Visualization.UI;

namespace Visualization.Animation
{
    public abstract class ConsoleRequest
    {
        protected string WriteText;
        public bool Done { get; set; }

        public ConsoleRequest(string textToWrite)
        {
            this.WriteText = textToWrite;
            this.Done = false;
        }

        public abstract void PerformRequest();
    }
}