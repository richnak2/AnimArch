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
            Console.WriteLine(success);

            /*Console.WriteLine("Declaration:");
            Console.WriteLine(Animation.DeclarationPartCode);

            Console.WriteLine("Declaration:");
            Console.WriteLine(Animation.DeclarationPartCode);

            Console.WriteLine("Execution:");
            Console.WriteLine(Animation.ExecutionPartCode);

            Console.WriteLine("Method Definitions:");
            foreach (CDClass Class in Animation.ExecutionNameSpace.ClassPool)
            {
                foreach (CDMethod Method in Class.Methods)
                {
                    Console.WriteLine("Definition of " + Class.Name + "." + Method.Name +" :");
                    Console.WriteLine(Method.OALCode);
                }
            }*/

            Console.WriteLine("------------------------------");
            Console.WriteLine(Animation.TranslateToCode());

            Console.WriteLine("------------------------------");
            OALParser op = new OALParser();
            //op.ParseOALFragment(Animation.TranslateToCode(), "global");
            //Console.WriteLine(op.FilterOutComments("a.b();\nb.c();//prd\n//Ahoooj\nend if;\nc.d();"));


            EXEScope es = op.DecomposeOALFragment(Animation.TranslateToCode());
            Console.WriteLine("------------------------------");
            Console.WriteLine(es.PrintSelf(true));
            //Console.WriteLine(op.SqueezeWhiteSpace("    x kp( )  |\n"));

            Console.ReadLine();

        }
    }
}
