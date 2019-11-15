using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    public class Program
    {
        static void Main(string[] args)
        {

            /*OALAnimationRepresentation Animation = new OALAnimationRepresentation();

            Animation.ExecutionNameSpace.SpawnClass("Observer", new List<String>(), new List<String>(new String[] { "update", "init", "addComment" }));
            Animation.ExecutionNameSpace.SpawnClass("Subject", new List<String>(), new List<String>(new String[] { "attach", "update", "notifyObservers" }));
            Animation.RelationshipSpace.Add(new CDRelationship("Observer", "Subject"));
            Boolean success;

            success = Animation.AddCallToAnimation("Observer", "init", "Subject", "attach");
            Console.WriteLine(success);
            success = Animation.AddCallToAnimation("Observer", "init", "Subject", "attach");
            Console.WriteLine(success);
            success = Animation.AddCallToAnimation("Observer", "init", "Subject", "attach");
            Console.WriteLine(success);

            success = Animation.AddCallToAnimation("Observer", "addComment", "Subject", "update");
            Console.WriteLine(success);
            success = Animation.AddCallToAnimation("Observer", "addComment", "Subject", "notifyObservers");
            Console.WriteLine(success);
            
            success = Animation.AddCallToAnimation("Subject", "notifyObservers", "Observer", "update");
            Console.WriteLine(success);
            success = Animation.AddCallToAnimation("Subject", "notifyObservers", "Observer", "update");
            Console.WriteLine(success);
            success = Animation.AddCallToAnimation("Subject", "notifyObservers", "Observer", "update");
            Console.WriteLine(success);*/

            Console.WriteLine("\n\n\n!!!!!!!!!!!!!!!!!!!!!!!");

            OALCommandParser OCP = new OALCommandParser();

            // Tokenize top level brackets did not expect something not in brackets can be on the same level
            // Perhaps put each chunk into identificator of top level commands?
            String command = "a.b = (15000 + 47) * 9";
            EXEASTNode AST = OCP.ConstructAST(command);
            AST.PrintPretty("", true);

            Console.ReadLine();
        }
    }
}
