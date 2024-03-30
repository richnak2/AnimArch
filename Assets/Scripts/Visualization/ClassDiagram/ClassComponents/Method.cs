using System;
using System.Collections.Generic;
using UnityEngine;

namespace Visualization.ClassDiagram.ClassComponents
{
    [Serializable]
    public class Method
    {
        public string Id;
        public string Name;
        public string ReturnValue;
        public MethodHighlightSubject HighlightSubject { get; private set;}
        public ObjectHighlightSubject HighlightObjectSubject { get; private set;}
        public List<string> arguments;
        public Method(string name, string id, string returnValue, List<string> arguments)
        {
            Name = name;
            Id = id;
            ReturnValue = returnValue;
            this.arguments = arguments;
            this.HighlightSubject = new MethodHighlightSubject();
            this.HighlightObjectSubject = new ObjectHighlightSubject();
        }

        public Method(string name, string id, string returnValue)
        {
            Name = name;
            Id = id;
            ReturnValue = returnValue;
            this.HighlightSubject = new MethodHighlightSubject();
            this.HighlightObjectSubject = new ObjectHighlightSubject();
        }
        public Method() { 
            this.HighlightSubject = new MethodHighlightSubject();
            this.HighlightObjectSubject = new ObjectHighlightSubject();
        }

        protected bool Equals(Method other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Method)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name);
        }
    }
}
