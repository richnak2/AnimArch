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

            OALAnimationRepresentation Animation = new OALAnimationRepresentation();

            CDClass ObserverClass = Animation.ExecutionNameSpace.SpawnClass("Observer", new List<String>(), new List<String>(new String[] { "update", "init", "addComment" }));
            CDClass SubjectClass = Animation.ExecutionNameSpace.SpawnClass("Subject", new List<String>(), new List<String>(new String[] { "attach", "update", "notifyObservers" }));
            CDRelationship OSRel = new CDRelationship("Observer", "Subject");
            String OSRelName = OSRel.RelationshipName;
            Animation.RelationshipSpace.Add(OSRel);
            Boolean success;

            success = Animation.AddCallToAnimation("Observer", "init", OSRelName, "Subject", "attach");
           // Console.WriteLine(success);
            success = Animation.AddCallToAnimation("Observer", "init", OSRelName, "Subject", "attach");
           // Console.WriteLine(success);
            success = Animation.AddCallToAnimation("Observer", "init", OSRelName, "Subject", "attach");
           // Console.WriteLine(success);

            success = Animation.AddCallToAnimation("Observer", "addComment", OSRelName, "Subject", "update");
            //Console.WriteLine(success);
            success = Animation.AddCallToAnimation("Observer", "addComment", OSRelName, "Subject", "notifyObservers");
           // Console.WriteLine(success);
            
            success = Animation.AddCallToAnimation("Subject", "notifyObservers", OSRelName, "Observer", "update");
           // Console.WriteLine(success);
            success = Animation.AddCallToAnimation("Subject", "notifyObservers", OSRelName, "Observer", "update");
            //Console.WriteLine(success);
            success = Animation.AddCallToAnimation("Subject", "notifyObservers", OSRelName, "Observer", "update");
           // Console.WriteLine(success);

            //Console.WriteLine("\n\n\n!!!!!!!!!!!!!!!!!!!!!!!");
            String Code = Animation.TranslateToMainCode();
            OALParser Parser = new OALParser();

            EXEScope SuperScope = Parser.DecomposeOALFragment(Code);
            SuperScope.Parse(null);

           /* int i = 1;
            foreach (EXECommandInterface Command in SuperScope.Commands)
            {
                Console.WriteLine(i.ToString());
                ++i;

                Command.PrintAST();
            }*/


           /* OALCommandParser OCP = new OALCommandParser();

            // Tokenize top level brackets did not expect something not in brackets can be on the same level
            // Perhaps put each chunk into identificator of top level commands?
            String command = "4 * 5 + 4 + 8 + 9 + (14 * (2) - 6 * 88 + (5 * (6))) + 78 * 2";
            EXEASTNode AST = OCP.ConstructAST(command);
            AST.PrintPretty("", true);*/

            Console.ReadLine();
        }
    }
}
