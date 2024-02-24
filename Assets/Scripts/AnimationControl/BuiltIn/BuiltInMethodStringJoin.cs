using OALProgramControl;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.AnimationControl.BuiltIn
{
    public class BuiltInMethodStringJoin : BuiltInMethodString
    {
        public override EXEExecutionResult Evaluate(EXEValueString owningObject, List<EXEValueBase> parameters)
        {
            EXEValueArray list = parameters[0] as EXEValueArray;

            IEnumerable<EXEValueBase> nonStringValues = list.Elements.Where(element => element as EXEValueString == null);

            if (nonStringValues.Any())
            {
                return EXEExecutionResult.Error(string.Format("The provided list needs to contains string values only. Instead, it contains '{0}' of type '{1}'.", nonStringValues.First().ToObjectDiagramText(), nonStringValues.First().TypeName), "XEC2050");
            }

            string delimiter = owningObject.Value;
            IEnumerable<string> items = list.Elements.Select(element => (element as EXEValueString).Value);

            string valueResult = string.Format("\"{0}\"", string.Join(delimiter, items));

            EXEExecutionResult result = EXEExecutionResult.Success();
            result.ReturnedOutput = new EXEValueString(valueResult);
            return result;
        }
    }
}