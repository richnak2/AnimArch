using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    public class EXECommandQueryCreate : EXECommand
    {
        private String ReferencingVariableName { get; set; }
        private String ClassName { get; set; }

        public EXECommandQueryCreate(String ClassNamem, String ReferencingVariableName)
        {
            this.ReferencingVariableName = ReferencingVariableName;
            this.ClassName = ClassName;
        }

        public EXECommandQueryCreate(String ClassName)
        {
            this.ReferencingVariableName = "";
            this.ClassName = ClassName;
        }

        // SetUloh2
        new public bool Execute(CDClassPool ExecutionSpace, CDRelationshipPool RelationshipSpace, EXEScope Scope)
        {
            //Create an instance of given class -> will affect ExecutionSpace.
            //If ReferencingVariableName is provided (is not ""), create a referencing variable pointing to this instance -> will affect scope
            throw new NotImplementedException();
        }

        //Ignore all methods below this comment
        new public string GetCode()
        {
            throw new NotImplementedException();
        }

        new public void PrintAST()
        {
            throw new NotImplementedException();
        }

        new public string PrintSelf(bool IsTopLevel)
        {
            throw new NotImplementedException();
        }
    }
}
