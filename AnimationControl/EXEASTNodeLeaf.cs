using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    public class EXEASTNodeLeaf : EXEASTNode
    {
        public Boolean IsVariableReference;
        public Boolean IsClassReference;
        public Boolean IsRelationshipReference;
        public String Value { get; set; }

        public EXEASTNodeLeaf(String Value, Boolean IsVariableReference, Boolean IsClassReference, Boolean IsRelationshipReference)
        {
            this.Value = Value;
            this.IsVariableReference = IsVariableReference;
            this.IsClassReference = IsClassReference;
            this.IsRelationshipReference = IsRelationshipReference;
        }

        public new String Evaluate(EXEScope Scope)
        {
            //TODO
            return null;
        }
    }
}
