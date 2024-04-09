using System.Collections.Generic;
using UnityEngine;

namespace Visualization.UI
{
    public class Composite : Component
    {
        private List<Component> children = new List<Component>();
        public string CompositeName;
        public Composite(string name) : base(name)
        {
            CompositeName = name;
        }
        public override void Add(Component component)
        {
            children.Add(component);
        }
        public override void Remove(Component component)
        {
            children.Remove(component);
        }
        public override Composite GetComposite()
        {
            return this;
        }
        public override void Operation()
        {
            foreach (Component child in children)
            {
                child.Operation();
            }
        }
        public override Component GetChild(int index)
        {
            return children[index];
        }
        public override string GetName()
        {
            return CompositeName;
        }
        public override List<Component> GetChildren()
        {
            return children;
        }
    }
}