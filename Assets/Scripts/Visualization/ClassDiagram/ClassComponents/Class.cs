using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace Visualization.ClassDiagram.ClassComponents
{
    [Serializable]
    public class Class
    {
        public string Name;
        [FormerlySerializedAs("XmiId")] public string Id;
        public string Visibility;
        public string NameSpc;
        public string Geometry;
        public float Left;
        public float Right;
        public float Top;
        public float Bottom;
        public string Type;
        public List<Attribute> Attributes;
        public List<Method> Methods;
        public ClassHighlightSubject HighlightSubject { get; private set;}
        public ObjectHighlightSubject HighlightSubjectObjects { get; private set;}

        public Class()
        {
            this.HighlightSubject = new ClassHighlightSubject();
            this.HighlightSubjectObjects = new ObjectHighlightSubject();
        }

        public Class(string id)
        {
            Name = "NewClass_" + id;
            Id = id;
            Type = "uml:Class";
            Attributes = new List<Attribute>();
            Methods = new List<Method>();
            this.HighlightSubject = new ClassHighlightSubject();
            this.HighlightSubjectObjects = new ObjectHighlightSubject();

        }
        public Class(string name, string id)
        {
            Name = name;
            Id = id;
            Type = "uml:Class";
            Attributes = new List<Attribute>();
            Methods = new List<Method>();
            this.HighlightSubject = new ClassHighlightSubject();
            this.HighlightSubjectObjects = new ObjectHighlightSubject();
        }

        protected bool Equals(Class other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Class)obj);
        }

        public override int GetHashCode()
        {
            return (Id != null ? Id.GetHashCode() : 0);
        }
    }
}
