using UnityEngine;

namespace Visualization.UI
{
    public abstract class Mediator : MonoBehaviour
    {
        public abstract void OnClicked(GameObject element);
    }
}