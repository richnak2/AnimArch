using UnityEngine;

namespace Visualization.UI
{
    public class PatternCatalogueLeaf : PatternCatalogueComponent
    {
        public string LeafName;
        public string LeafPath{get; set;}
        public PatternCatalogueLeaf(string path, string name) : base(path,name)
        {
            LeafName = name;
            LeafPath = path;
        }
        public override PatternCatalogueComponent GetComponent()
        {
            return this;
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