using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    public class EXERelationshipSelection
    {
        public String StartingVariable { get; }
        private List<EXERelationshipLink> RelationshipSpecification { get; }
        public EXERelationshipSelection(String StartingVariable)
        {
            this.StartingVariable = StartingVariable;
            this.RelationshipSpecification = new List<EXERelationshipLink>();
        }
        public void AddRelationshipLink(EXERelationshipLink RelationshipLink)
        {
            this.RelationshipSpecification.Add(RelationshipLink);
        }

        public List<long> Evaluate(CDRelationshipPool RelationshipSpace, EXEScope Scope)
        {
            List<long> Result = null;
            EXEReferenceHandle StartVariable = Scope.FindReferenceHandleByName(this.StartingVariable);
            if (StartVariable != null && this.RelationshipSpecification.Any())
            {
                List<long> CurrentIds = StartVariable.GetReferencedIds();
                String CurrentClass = StartVariable.ClassName;
                foreach(EXERelationshipLink RelationshipLink in this.RelationshipSpecification)
                {
                    if (!CurrentIds.Any())
                    {
                        break;
                    }
                    CurrentIds = RelationshipLink.RetrieveIds(CurrentIds, CurrentClass, RelationshipSpace);
                    CurrentClass = RelationshipLink.ClassName;
                }
                Result = CurrentIds;
            }
            return Result;
        }
        public String GetLastClassName()
        {
            String Result = null;
            if (this.RelationshipSpecification != null && this.RelationshipSpecification.Any())
            {
                Result = this.RelationshipSpecification.Last().ClassName;
            }
            return Result;
        }
    }
}
