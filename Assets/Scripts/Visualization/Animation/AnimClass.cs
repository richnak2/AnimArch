using System.Collections.Generic;
using UnityEngine;

namespace Visualization.Animation
{
    [System.Serializable]
    public class AnimClass  //Filip
    {
        [SerializeField]
        public string Name;
        [SerializeField]
        public string SuperClass; //od ktorej dedi
        [SerializeField]
        public List<string> Attributes;
        [SerializeField]
        public List<AnimMethod> Methods;

        public AnimClass(string Name, string SuperClass, List<string> Attributes, List<AnimMethod> Methods)
        {
            this.Name = Name;
            this.SuperClass = SuperClass;
            this.Attributes = Attributes;
            this.Methods = Methods;
        }
    }
}
