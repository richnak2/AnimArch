using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;
using ParserRuleContext = Antlr4.Runtime.ParserRuleContext;

namespace AnimationControl.OAL
{
    public class OALVisitor2:OALBaseVisitor<object>
    {
        public EXEScope e;
        public OALVisitor2()
        {
            this.e = new EXEScope();
            //e.AddCommand(new EXECommandQueryCreate("a","a"));

        }


        public override object VisitExeCommandQueryCreate([NotNull] OALParser.ExeCommandQueryCreateContext context)
        {
            //Console.WriteLine("test: " + context.ChildCount);
            String ClassName = null;
            String InstanceName = null;

            foreach (IParseTree child in context.children)
            {

                //Console.WriteLine(child.GetType().ToString());
                //Console.WriteLine(child.GetText());

                if (child.GetType().ToString().Contains("KeyLetter"))
                {
                    ClassName = child.GetText();
                    Console.WriteLine(ClassName);
                }

                if (child.GetType().ToString().Contains("InstanceHandle"))
                {
                    InstanceName = child.GetText();
                    Console.WriteLine(InstanceName);
                }
                
            }
            e.AddCommand(new EXECommandQueryCreate(ClassName, InstanceName));
            return null;
        }

    }
}
