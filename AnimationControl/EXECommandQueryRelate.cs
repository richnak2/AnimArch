using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    public class EXECommandQueryRelate : EXECommand
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
        // SetUloh3
        // Create a relationship instance (between two variables pointing to class instances)
        // Based on class names get the CDRelationship from RelationshipSpace
        // Based on variable names get the instance ids from Scope.ReferencingVariables
        // Create relationship between the given instance ids (CDRelationship.CreateRelationship) and return result of it
        // create unit tests for this - (successfull creation / relationship already exists/such variables do not exist in Scope). Keep the naming convention from other unit tests
        // use methods you are supposed to implement as Set Uloh 3
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
