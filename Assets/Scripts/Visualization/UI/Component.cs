using UnityEngine;

namespace Visualization.UI
{
    class Component : MonoBehaviour
    {
        public string ComponentName;

        public Component(string name)
        {
            ComponentName = name;
        }
        public virtual Composite GetComposite()
        {
            return null;
        }
        public virtual void Operation(){}
        public virtual void Add(Component component){}
        public virtual void Remove(Component component){}
        public virtual Component GetChild(int index)
        {
            return null;
        }
    }
}