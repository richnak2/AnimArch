﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AnimArch.Extensions;
using OALProgramControl;
using UnityEngine;
using Visualization.Animation;
using Visualization.ClassDiagram.ClassComponents;
using Visualization.ClassDiagram.ComponentsInDiagram;
using Visualization.ClassDiagram.Relations;
using Attribute = Visualization.ClassDiagram.ClassComponents.Attribute;
using Object = UnityEngine.Object;

namespace Visualization.ClassDiagram.Editors
{
    public class MainEditor
    {
        protected IVisualEditor _visualEditor;

        public MainEditor(IVisualEditor visualEditor)
        {
            _visualEditor = visualEditor;
        }

        public enum Source
        {
            RPC,
            Editor,
            Loader
        }
        
        public virtual void CreateNode(Class newClass)
        {
            var newCdClass = CDEditor.CreateNode(newClass);
            newClass.Name = newCdClass.Name;

            var classGo = _visualEditor.CreateNode(newClass);

            if (newClass.Left == 0) 
                newClass = ParsedEditor.UpdateNodeGeometry(newClass, classGo);

            var classInDiagram = new ClassInDiagram
                { ParsedClass = newClass, ClassInfo = newCdClass, VisualObject = classGo };
            DiagramPool.Instance.ClassDiagram.Classes.Add(classInDiagram);
        }

        public virtual void UpdateNodeName(string oldName, string newName)
        {
            var classInDiagram = DiagramPool.Instance.ClassDiagram.FindClassByName(oldName);
            if (classInDiagram == null)
                return;

            classInDiagram.ParsedClass.Name = newName;
            classInDiagram.ClassInfo.Name = newName;
            classInDiagram.VisualObject.name = newName;

            _visualEditor.UpdateNode(classInDiagram.VisualObject);

            foreach (var relationInDiagram in DiagramPool.Instance.ClassDiagram.Relations)
            {
                if (relationInDiagram.ParsedRelation.FromClass == oldName)
                {
                    relationInDiagram.ParsedRelation.FromClass = newName;
                    relationInDiagram.ParsedRelation.SourceModelName = newName;
                    relationInDiagram.RelationInfo.FromClass = newName;
                }

                if (relationInDiagram.ParsedRelation.ToClass == oldName)
                {
                    relationInDiagram.ParsedRelation.ToClass = newName;
                    relationInDiagram.ParsedRelation.TargetModelName = newName;
                    relationInDiagram.RelationInfo.ToClass = newName;
                }
            }

            foreach (var otherClassInDiagram in DiagramPool.Instance.ClassDiagram.Classes)
            {
                var attributesWithOldNameAsType = new List<Attribute>(
                    otherClassInDiagram.ParsedClass.Attributes
                        .Where(x => x.Type.Contains(oldName)));
                foreach (var attribute in attributesWithOldNameAsType)
                {
                    var formerArray = attribute.Type.Contains("[]");
                    var formerType = Regex.Replace( attribute.Type, "[\\[\\]\\n]", "");
                    if (formerType != oldName)
                        continue;

                    var newAttribute = new Attribute
                    {
                        Id = attribute.Id,
                        Name = attribute.Name,
                        Type = newName + (formerArray ? "[]" : "")
                    };
                    UpdateAttribute(otherClassInDiagram.ParsedClass.Name, attribute.Name, newAttribute);
                }


                var methodsWithOldNameAsType = new List<Method>(
                    otherClassInDiagram.ParsedClass.Methods
                        .Where(x => x.ReturnValue == oldName ||
                                    x.arguments.Any(arg => arg.Split(" ")[0].Contains(oldName))));
                foreach (var method in methodsWithOldNameAsType)
                {
                    var newMethod = new Method
                    {
                        Id = method.Id,
                        Name = method.Name,
                        arguments = method.arguments,
                        ReturnValue = method.ReturnValue
                    };
                    
                    var returnType = Regex.Replace( method.ReturnValue, "[\\[\\]\\n]", "");
                    if (returnType == oldName)
                    {
                        newMethod.ReturnValue =  newName + (method.ReturnValue.Contains("[]") ? "[]" : "");
                    }

                    for (var i = 0; i < newMethod.arguments.Count; i++)
                    {
                        if (!newMethod.arguments[i].Contains(oldName))
                            continue;
                        var attributeType = newMethod.arguments[i].Split(" ")[0];
                        var attributeName = newMethod.arguments[i].Split(" ")[1];
                        var formerArray = attributeType.Contains("[]");
                        attributeType = Regex.Replace(attributeType, "[\\[\\]\\n]", "");
                        if (attributeType == oldName)
                        {
                            newMethod.arguments[i] = newName + (formerArray ? "[]" : "") + " " + attributeName;
                        }
                    }

                    UpdateMethod(otherClassInDiagram.ParsedClass.Name, method.Name, newMethod);
                }
            }
        }

        public virtual void UpdateNodeGeometry(string name)
        {
            var classInDiagram = DiagramPool.Instance.ClassDiagram.FindClassByName(name);
            classInDiagram.ParsedClass =
                ParsedEditor.UpdateNodeGeometry(classInDiagram.ParsedClass, classInDiagram.VisualObject);
        }

        public virtual void AddAttribute(string targetClass, Attribute attribute)
        {
            var classInDiagram = DiagramPool.Instance.ClassDiagram.FindClassByName(targetClass);
            if (classInDiagram == null) return;

            if (DiagramPool.Instance.ClassDiagram.FindAttributeByName(targetClass, attribute.Name) != null)
            {
                CDEditor.AddAttribute(classInDiagram, attribute);
                _visualEditor.AddAttribute(classInDiagram, attribute);
                return;
            }

            ParsedEditor.AddAttribute(classInDiagram, attribute);
            CDEditor.AddAttribute(classInDiagram, attribute);
            _visualEditor.AddAttribute(classInDiagram, attribute);
        }

        public virtual void UpdateAttribute(string targetClass, string oldAttribute, Attribute newAttribute)
        {
            var classInDiagram = DiagramPool.Instance.ClassDiagram.FindClassByName(targetClass);
            if (classInDiagram == null)
                return;

            if (DiagramPool.Instance.ClassDiagram.FindAttributeByName(targetClass, oldAttribute) == null)
                return;

            ParsedEditor.UpdateAttribute(classInDiagram, oldAttribute, newAttribute);
            CDEditor.UpdateAttribute(classInDiagram, oldAttribute, newAttribute);
            _visualEditor.UpdateAttribute(classInDiagram, oldAttribute, newAttribute);
        }

        public virtual void AddMethod(string targetClass, Method method)
        {
            var classInDiagram = DiagramPool.Instance.ClassDiagram.FindClassByName(targetClass);
            if (classInDiagram == null)
                return;

            classInDiagram.ParsedClass.Methods ??= new List<Method>();

            if (DiagramPool.Instance.ClassDiagram.FindMethodByName(targetClass, method.Name) != null)
            {
                CDEditor.AddMethod(classInDiagram, method);
                _visualEditor.AddMethod(classInDiagram, method);
                return;
            }

            ParsedEditor.AddMethod(classInDiagram, method);
            CDEditor.AddMethod(classInDiagram, method);
            _visualEditor.AddMethod(classInDiagram, method);
        }

        public virtual void UpdateMethod(string targetClass, string oldMethod, Method newMethod)
        {
            var classInDiagram = DiagramPool.Instance.ClassDiagram.FindClassByName(targetClass);
            if (classInDiagram == null)
                return;

            if (DiagramPool.Instance.ClassDiagram.FindMethodByName(targetClass, oldMethod) == null)
                return;

            ParsedEditor.UpdateMethod(classInDiagram, oldMethod, newMethod);
            CDEditor.UpdateMethod(classInDiagram, oldMethod, newMethod);
            _visualEditor.UpdateMethod(classInDiagram, oldMethod, newMethod);
        }

        public virtual void CreateRelation(Relation relation)
        {
            relation.FromClass = relation.SourceModelName.Replace(" ", "_");
            relation.ToClass = relation.TargetModelName.Replace(" ", "_");

            var cdRelation = CDEditor.CreateRelation(relation);
            var relationGo = _visualEditor.CreateRelation(relation);

            var relationInDiagram = new RelationInDiagram
                { ParsedRelation = relation, RelationInfo = cdRelation, VisualObject = relationGo };
            DiagramPool.Instance.ClassDiagram.AddRelation(relationInDiagram);
            DiagramPool.Instance.ClassDiagram.graph.UpdateGraph();
        }

        public virtual void DeleteRelation(GameObject relation)
        {
            var relationInDiagram = DiagramPool.Instance.ClassDiagram.Relations
                .Find(x => x.VisualObject.Equals(relation));

            CDEditor.DeleteRelation(relationInDiagram);
            _visualEditor.DeleteRelation(relationInDiagram);

            DiagramPool.Instance.ClassDiagram.Relations.Remove(relationInDiagram);
        }

        private void DeleteNodeFromRelations(ClassInDiagram classInDiagram)
        {
            new List<RelationInDiagram>(
                    DiagramPool.Instance.ClassDiagram.Relations
                .Where(x => x.ParsedRelation.FromClass == classInDiagram.ParsedClass.Name
                            || x.ParsedRelation.ToClass == classInDiagram.ParsedClass.Name))
                .ForEach(x => DeleteRelation(x.VisualObject));
        }

        public virtual void DeleteNode(string className)
        {
            var classInDiagram = DiagramPool.Instance.ClassDiagram.FindClassByName(className);
            if (classInDiagram == null)
                return;

            DeleteNodeFromRelations(classInDiagram);

            CDEditor.DeleteNode(classInDiagram);
            _visualEditor.DeleteNode(classInDiagram);

            DiagramPool.Instance.ClassDiagram.Classes.Remove(classInDiagram);
        }

        public virtual void DeleteAttribute(string className, string attributeName)
        {
            var classInDiagram = DiagramPool.Instance.ClassDiagram.FindClassByName(className);
            if (classInDiagram == null)
                return;

            if (DiagramPool.Instance.ClassDiagram.FindAttributeByName(className, attributeName) == null)
                return;

            ParsedEditor.DeleteAttribute(classInDiagram, attributeName);
            CDEditor.DeleteAttribute(classInDiagram, attributeName);
            _visualEditor.DeleteAttribute(classInDiagram, attributeName);
        }

        public virtual void DeleteMethod(string className, string methodName)
        {
            var classInDiagram = DiagramPool.Instance.ClassDiagram.FindClassByName(className);
            if (classInDiagram == null)
                return;

            if (DiagramPool.Instance.ClassDiagram.FindMethodByName(className, methodName) == null)
                return;

            ParsedEditor.DeleteMethod(classInDiagram, methodName);
            CDEditor.DeleteMethod(classInDiagram, methodName);
            _visualEditor.DeleteMethod(classInDiagram, methodName);
        }

        public void ClearDiagram()
        {
            // Get rid of already rendered classes in diagram.
            if (DiagramPool.Instance.ClassDiagram.Classes != null)
            {
                foreach (var Class in DiagramPool.Instance.ClassDiagram.Classes) Object.Destroy(Class.VisualObject);

                DiagramPool.Instance.ClassDiagram.Classes.Clear();
            }

            // Get rid of already rendered relations in diagram.
            if (DiagramPool.Instance.ClassDiagram.Relations != null)
            {
                foreach (var relation in DiagramPool.Instance.ClassDiagram.Relations)
                    Object.Destroy(relation.VisualObject);

                DiagramPool.Instance.ClassDiagram.Relations.Clear();
            }

            if (DiagramPool.Instance.ClassDiagram.graph != null)
            {
                Object.Destroy(DiagramPool.Instance.ClassDiagram.graph.gameObject);
                DiagramPool.Instance.ClassDiagram.graph = null;
            }

            DiagramPool.Instance.ClassDiagram.graph = null;
            Animation.Animation.Instance.CurrentProgramInstance.Reset();
            AnimationData.Instance.RemoveAnimation();
        }
    }
}
