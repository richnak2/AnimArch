using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    class EXECommandQueryRelate : EXECommand
    {
        public String Variable1Name { get; }
        public String Variable2Name { get; }

        public String RelationshipName { get; }

        public String Class1Name { get; }
        public String Class2Name { get; }


        public EXECommandQueryRelate(String Variable1Name, String Variable2Name, String RelationshipName, String Class1Name, String Class2Name)
        {
            this.Variable1Name = Variable1Name;
            this.Variable2Name = Variable2Name;
            this.RelationshipName = RelationshipName;
            this.Class1Name = Class1Name;
            this.Class2Name = Class2Name;
        }

        new public bool Execute(CDClassPool ExecutionSpace, CDRelationshipPool RelationshipSpace, EXEScope Scope)
        {
            //Create a relationship between given class instances -> will affect RelationshipSpace.
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
