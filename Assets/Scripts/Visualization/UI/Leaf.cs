using UnityEngine;

namespace Visualization.UI
{
    public class Leaf : Component
    {
        public string LeafName;
        public Leaf(string name) : base(name)
        {
            LeafName = name;
        }
        public override void Operation()
        {
            //TODO: nacitanie animace/diagramu atd...
            Debug.Log("Leaf operation");
        } 
        public override string GetName()
        {
            return LeafName;
        }
    }
}