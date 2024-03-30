using System.Collections;
using UnityEditor;
using UnityEngine;
using Visualization.UI;

namespace Visualization.Animation
{
    public class ConsoleRequestWrite : ConsoleRequest
    {
        public ConsoleRequestWrite(string textToWrite) : base(textToWrite) { }

        public override void PerformRequest()
        {
            ConsolePanel.Instance.YieldOutput(WriteText);
        }
    }
}