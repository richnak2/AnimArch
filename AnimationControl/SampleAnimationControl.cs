using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    class SampleAnimationControl
    {
        public void AnimateDistributors()
        {
            CDClassPool cp = new CDClassPool();
            CDRelationshipPool rp = new CDRelationshipPool();

            cp.SpawnClass("Outer Control Class");
            cp.SpawnClass("Order");
            cp.SpawnClass("Careful Distributor");
            cp.SpawnClass("Regular Distributor");
            cp.SpawnClass("Messenger");
            cp.SpawnClass("Line Item");

            CDRelationship R1 = rp.SpawnRelationship("Outer Control Class", "Order");
            CDRelationship R2 = rp.SpawnRelationship("Careful Distributor", "Order");
            CDRelationship R3 = rp.SpawnRelationship("Regular Distributor", "Order");
            CDRelationship R4 = rp.SpawnRelationship("Messenger", "Order");

            EXEScope AnimationSuperscope = new EXEScope();

            EXECommandQueryCreate CreateQuery1 = new EXECommandQueryCreate("Outer Control Class", "ooc");
            EXECommandQueryCreate CreateQuery2 = new EXECommandQueryCreate("Order", "order");
            EXECommandQueryCreate CreateQuery3 = new EXECommandQueryCreate("Careful Distributor", "car_distrib");
            EXECommandQueryCreate CreateQuery4 = new EXECommandQueryCreate("Regular Distributor", "reg_distrib");
            EXECommandQueryCreate CreateQuery5 = new EXECommandQueryCreate("Messenger", "messenger");
            AnimationSuperscope.AddCommand(CreateQuery1);
            AnimationSuperscope.AddCommand(CreateQuery2);
            AnimationSuperscope.AddCommand(CreateQuery3);
            AnimationSuperscope.AddCommand(CreateQuery4);
            AnimationSuperscope.AddCommand(CreateQuery5);
            for (int i = 0; i < 3; i++)
            {
                AnimationSuperscope.AddCommand(new EXECommandQueryCreate("Line Item", "li" + i));
            }

            EXECommandQueryRelate RelQuery1 = new EXECommandQueryRelate("ooc", "order", R1.RelationshipName);
            EXECommandQueryRelate RelQuery2 = new EXECommandQueryRelate("car_distrib", "order", R2.RelationshipName);
            EXECommandQueryRelate RelQuery3 = new EXECommandQueryRelate("reg_distrib", "order", R3.RelationshipName);
            EXECommandQueryRelate RelQuery4 = new EXECommandQueryRelate("messenger", "order", R4.RelationshipName);
            AnimationSuperscope.AddCommand(RelQuery1);
            AnimationSuperscope.AddCommand(RelQuery2);
            AnimationSuperscope.AddCommand(RelQuery3);
            AnimationSuperscope.AddCommand(RelQuery4);

            EXECommandCall Call1 = new EXECommandCall("ooc", "InitiateDispatch", R1.RelationshipName, "order", "dispatch");
            EXECommandQuerySelect SelectSetQuery = new EXECommandQuerySelect(EXECommandQuerySelect.CardinalityMany, "Line Item", "line_items");
            AnimationSuperscope.AddCommand(Call1);
            AnimationSuperscope.AddCommand(SelectSetQuery);

            EXEScopeForEach ForEachCommand = new EXEScopeForEach("current_li", "line_items");
           // EXEScopeCondition IfCommand = new EXEScopeCondition(new EXEASTNodeLeaf(EXETypes.BooleanTrue));
        }
    }
}