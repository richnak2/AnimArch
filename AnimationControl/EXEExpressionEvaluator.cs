using System;
using System.Collections.Generic;
using System.Linq;

namespace AnimationControl
{
    public class EXEExpressionEvaluator
    {

        // SetUloh1
        // Here you get operator and operands, and you need to return the result. Check the unit tests to see what this is about
        public String Evaluate(String Operator, List<String> Operands)
        {
           if (!this.CanBeEvaluated(Operator, Operands)) return null;
      

            //get variable type
            String VariableType = EXETypes.DetermineVariableType(null, Operands[0]);


            switch (Operator)
            {
                case "+":
                    //true if Operands are integers
                    if (String.Equals(EXETypes.IntegerTypeName, VariableType))
                    {
                        //convert to list of int
                        List<int> IntList = Operands.Select(int.Parse).ToList();
                        //add numbers in list and return result in string
                        return IntList.Aggregate((a, x) => a + x).ToString();
                    }
                    //true if Operands are real numbers
                    if (String.Equals(EXETypes.RealTypeName, VariableType))
                    {
                        //convert to list of real numbers
                        List<double> DoubleList = Operands.Select(double.Parse).ToList();
                        //add numbers in list and return result in string
                        return DoubleList.Aggregate((a, x) => a + x).ToString();
                    }
                    if (String.Equals(EXETypes.StringTypeName, VariableType))
                    {
                        //add string in list and return result in string
                        //TODO
                       // return Operands.Aggregate((a, x) => a + x).ToString();
                       //concate
                    }
                    break;
                case "-":
                    //true if Operands are integers
                    if (String.Equals(EXETypes.IntegerTypeName, VariableType))
                    {
                        //convert to list of int
                        List<int> IntList = Operands.Select(int.Parse).ToList();
                        //sub numbers in list and return result in string
                        return IntList.Aggregate((a, x) => a - x).ToString();
                    }
                    //true if Operands are real numbers
                    if (String.Equals(EXETypes.RealTypeName, VariableType))
                    {
                        //convert to list of real numbers
                        List<double> DoubleList = Operands.Select(double.Parse).ToList();
                        //sub numbers in list and return result in string
                        return DoubleList.Aggregate((a, x) => a - x).ToString();
                    }
                    if (String.Equals(EXETypes.StringTypeName, VariableType))
                    {
                        //add string in list and return result in string
                        //TODO
                        // return Operands.Aggregate((a, x) => a + x).ToString();
                        //
                    }
                    break;
                case "*":
                    //true if Operands are integers
                    if (String.Equals(EXETypes.IntegerTypeName, VariableType))
                    {
                        //convert to list of int
                        List<int> IntList = Operands.Select(int.Parse).ToList();
                        //multiply numbers in list and return result in string
                        return IntList.Aggregate((a, x) => a * x).ToString();
                    }
                    //true if Operands are real numbers
                    if (String.Equals(EXETypes.RealTypeName, VariableType))
                    {
                        //convert to list of real numbers
                        List<double> DoubleList = Operands.Select(double.Parse).ToList();
                        //multiply numbers in list and return result in string
                        return DoubleList.Aggregate((a, x) => a * x).ToString();
                    }
                   
                    break;
                case "/":
                    //true if Operands are integers
                    if (String.Equals(EXETypes.IntegerTypeName, VariableType))
                    {
                        //convert to list of int
                        List<int> IntList = Operands.Select(int.Parse).ToList();
                        //divide numbers in list and return result in string
                        return IntList.Aggregate((a, x) => a / x).ToString();
                    }
                    //true if Operands are real numbers
                    if (String.Equals(EXETypes.IntegerTypeName, VariableType))
                    {
                        //convert to list of real numbers
                        List<double> DoubleList = Operands.Select(double.Parse).ToList();
                        //divide numbers in list and return result in string
                        return DoubleList.Aggregate((a, x) => a / x).ToString();
                    }
                   
                    break;
                case "%":
                    //true if Operands are integers
                    if (String.Equals(EXETypes.IntegerTypeName, VariableType))
                    {
                        //convert to list of int
                        List<int> IntList = Operands.Select(int.Parse).ToList();
                        //divide numbers in list and return result in string
                        return IntList.Aggregate((a, x) => a % x).ToString();
                    }
                    //true if Operands are real numbers
                    if (String.Equals(EXETypes.IntegerTypeName, VariableType))
                    {
                        //convert to list of real numbers
                        List<double> DoubleList = Operands.Select(double.Parse).ToList();
                        //divide numbers in list and return result in string
                        return DoubleList.Aggregate((a, x) => a % x).ToString();
                    }
                    
                    break;
                case "==":
                    //true if Operands are integers
                    if (String.Equals(EXETypes.IntegerTypeName, VariableType))
                    {
                        //convert to list of int
                        List<int> IntList = Operands.Select(int.Parse).ToList();
                        //compare 2 numbers in list (equals) and return result in string
                        return (int.Equals(IntList[0], IntList[1]) ? EXETypes.BooleanTrue : EXETypes.BooleanFalse);
                        
                    }
                    //true if Operands are real numbers
                    if (String.Equals(EXETypes.RealTypeName, VariableType))
                    {
                        //convert to list of real numbers
                        List<double> DoubleList = Operands.Select(double.Parse).ToList();
                        //compare 2 numbers in list (equals) and return result in string
                        return (Double.Equals(DoubleList[0], DoubleList[1]) ? EXETypes.BooleanTrue : EXETypes.BooleanFalse);
                    }
                    if (String.Equals(EXETypes.StringTypeName, VariableType))
                    {
                        //compare 2 string in list (equals) and return result in string
                        return (String.Equals(Operands[0], Operands[1]) ? EXETypes.BooleanTrue : EXETypes.BooleanFalse);
                    }
                    //true if Operands are boolean
                    //only 2 operands
                    if (String.Equals(EXETypes.BooleanTypeName, VariableType))
                    {
                        //convert to list of int
                        List<bool> BooleanList = Operands.Select(bool.Parse).ToList();
                        //compare 2 boolean in list (equals) and return result in string
                        return (Boolean.Equals(BooleanList[0],BooleanList[1]) ? EXETypes.BooleanTrue : EXETypes.BooleanFalse);
                    }
                    break;
                case "!=":
                    //true if Operands are integers
                    if (String.Equals(EXETypes.IntegerTypeName, VariableType))
                    {
                        //convert to list of int
                        List<int> IntList = Operands.Select(int.Parse).ToList();
                        //compare numbers in list (not equals) and return result in string
                        return EXETypes.BooleanTrue;
                    }
                    //true if Operands are real numbers
                    if (String.Equals(EXETypes.RealTypeName, VariableType))
                    {
                        //convert to list of real numbers
                        List<double> DoubleList = Operands.Select(double.Parse).ToList();
                        //compare numbers in list (not equals) and return result in string
                        return EXETypes.BooleanTrue;
                    }
                    if (String.Equals(EXETypes.StringTypeName, VariableType))
                    {
                        //add string in list and return result in string
                        //TODO
                        // return Operands.Aggregate((a, x) => a + x).ToString();
                    }
                    //true if Operands are boolean
                    if (String.Equals(EXETypes.BooleanTypeName, VariableType))
                    {
                        //convert to list of int
                        List<bool> IntList = Operands.Select(bool.Parse).ToList();
                        //and booleans
                        Console.WriteLine(IntList);
                        //return TRUE or FALSE
                        return EXETypes.BooleanTrue;
                    }
                    break;
                    //only 2 operands
                case "<":
                    //true if Operands are integers
                    if (String.Equals(EXETypes.IntegerTypeName, VariableType))
                    {
                        //convert to list of int
                        List<int> IntList = Operands.Select(int.Parse).ToList();
                        //compare numbers in list (less then) and return result in string
                        return EXETypes.BooleanTrue;
                    }
                    //true if Operands are real numbers
                    if (String.Equals(EXETypes.RealTypeName, VariableType))
                    {
                        //convert to list of real numbers
                        List<double> DoubleList = Operands.Select(double.Parse).ToList();
                        //compare numbers in list (less then) and return result in string
                        return EXETypes.BooleanTrue;
                    }
                    if (String.Equals(EXETypes.StringTypeName, VariableType))
                    {
                        //add string in list and return result in string
                        //TODO
                        // return Operands.Aggregate((a, x) => a + x).ToString();
                    }
                    break;
                case ">":
                    //true if Operands are integers
                    if (String.Equals(EXETypes.IntegerTypeName, VariableType))
                    {
                        //convert to list of int
                        List<int> IntList = Operands.Select(int.Parse).ToList();
                        //compare numbers in list (greater then) and return result in string
                        return EXETypes.BooleanTrue;
                    }
                    //true if Operands are real numbers
                    if (String.Equals(EXETypes.RealTypeName, VariableType))
                    {
                        //convert to list of real numbers
                        List<double> DoubleList = Operands.Select(double.Parse).ToList();
                        //compare numbers in list (greater then) and return result in string
                        return EXETypes.BooleanTrue;
                    }
                    if (String.Equals(EXETypes.StringTypeName, VariableType))
                    {
                        //add string in list and return result in string
                        //TODO
                        // return Operands.Aggregate((a, x) => a + x).ToString();
                    }
                    break;
                case "<=":
                    //true if Operands are integers
                    if (String.Equals(EXETypes.IntegerTypeName, VariableType))
                    {
                        //convert to list of int
                        List<int> IntList = Operands.Select(int.Parse).ToList();
                        //compare numbers in list (less then or equals to) and return result in string
                        return EXETypes.BooleanTrue;
                    }
                    //true if Operands are real numbers
                    if (String.Equals(EXETypes.RealTypeName, VariableType))
                    {
                        //convert to list of real numbers
                        List<double> DoubleList = Operands.Select(double.Parse).ToList();
                        //compare numbers in list (less then or equals to) and return result in string
                        return EXETypes.BooleanTrue;
                    }
                    if (String.Equals(EXETypes.StringTypeName, VariableType))
                    {
                        //add string in list and return result in string
                        //TODO
                        // return Operands.Aggregate((a, x) => a + x).ToString();
                    }
                    break;
                case ">=":
                    //true if Operands are integers
                    if (String.Equals(EXETypes.IntegerTypeName, VariableType))
                    {
                        //convert to list of int
                        List<int> IntList = Operands.Select(int.Parse).ToList();
                        //compare numbers in list (greater then or equals to) and return result in string
                        return EXETypes.BooleanTrue;
                    }
                    //true if Operands are real numbers
                    if (String.Equals(EXETypes.RealTypeName, VariableType))
                    {
                        //convert to list of real numbers
                        List<double> DoubleList = Operands.Select(double.Parse).ToList();
                        //compare numbers in list (greater then or equals to) and return result in string
                        return EXETypes.BooleanTrue;
                    }
                    if (String.Equals(EXETypes.StringTypeName, VariableType))
                    {
                        //add string in list and return result in string
                        //TODO
                        // return Operands.Aggregate((a, x) => a + x).ToString();
                    }
                    break;
                case "and":
                    //true if Operands are boolean
                    if (String.Equals(EXETypes.BooleanTypeName, VariableType))
                    {
                        //convert to list of int
                        List<bool> BooleanList = Operands.Select(bool.Parse).ToList();
                        //and booleans
                        //return TRUE or FALSE
                        return (BooleanList[0] && BooleanList[1]) ? EXETypes.BooleanTrue : EXETypes.BooleanFalse;
                    }
                    break;
                case "or":
                    //true if Operands are boolean
                    if (String.Equals(EXETypes.BooleanTypeName, VariableType))
                    {
                        //convert to list of int
                        List<bool> BooleanList = Operands.Select(bool.Parse).ToList();
                        return (BooleanList[0] || BooleanList[1]) ? EXETypes.BooleanTrue : EXETypes.BooleanFalse;

                    }
                    break;
                    //only 1 operand
                case "not":
                    //true if Operands are boolean
                    Console.WriteLine(VariableType);
                    if (String.Equals(EXETypes.BooleanTypeName, VariableType))
                    {
                        //return TRUE or FALSE
                        return (String.Equals(Operands[0], EXETypes.BooleanTrue) ? EXETypes.BooleanFalse : EXETypes.BooleanTrue) ;
                    }
                    break;
            }


            throw new NotImplementedException();
        }
        //What's the difference between "FAIL" and "false"?
        public Boolean CanBeEvaluated(String Operator, List<String> Params)
        {
            String Result = this.IsValid(Operator, Params.ToArray());

            Boolean CanBeEvaluated = EXETypes.BooleanTrue.Equals(Result) ? true : false;

            return CanBeEvaluated;
        }
        // Potentially refactor this
        private String IsValid(string oper, string[] param)
        {
            int p1;
            double p2;
            bool b1 = int.TryParse(param[0], out p1);
            bool b2 = double.TryParse(param[0], out p2);

            if (b1 || b2)
            {
                if (param.Length < 2)
                {
                    return "FAIL";
                }
                if (oper == ">" || oper == "<" || oper == "<=" || oper == ">=" || oper == "+" ||
                   oper == "-" || oper == "/" || oper == "*" || oper == "==" || oper == "!=")
                {
                    foreach (string paramx in param)
                    {
                        if (!int.TryParse(paramx, out p1) && !double.TryParse(paramx, out p2))
                        {
                            return "FAIL";
                        }
                    }
                    double res;
                    switch (oper)
                    {
                        case ">":
                            double.TryParse(param[0], out res);
                            for (int i = 1; i < param.Length; i++)
                            {
                                double.TryParse(param[i], out p2);
                                if (!(res > p2))
                                {
                                    return EXETypes.BooleanFalse;
                                }
                                res = p2;
                            }
                            return EXETypes.BooleanTrue;
                        case "<":
                            double.TryParse(param[0], out res);
                            for (int i = 1; i < param.Length; i++)
                            {
                                double.TryParse(param[i], out p2);
                                if (!(res < p2))
                                {
                                    return EXETypes.BooleanFalse;
                                }
                                res = p2;
                            }
                            return EXETypes.BooleanTrue;
                        case "<=":
                            double.TryParse(param[0], out res);
                            for (int i = 1; i < param.Length; i++)
                            {
                                double.TryParse(param[i], out p2);
                                if (!(res <= p2))
                                {
                                    return EXETypes.BooleanFalse;
                                }
                                res = p2;
                            }
                            return EXETypes.BooleanTrue;
                        case "==":
                            double.TryParse(param[0], out res);
                            for (int i = 1; i < param.Length; i++)
                            {
                                double.TryParse(param[i], out p2);
                                if (!(res == p2))
                                {
                                    return EXETypes.BooleanFalse;
                                }
                                res = p2;
                            }
                            return EXETypes.BooleanTrue;
                        case "!=":
                            double.TryParse(param[0], out res);
                            for (int i = 1; i < param.Length; i++)
                            {
                                double.TryParse(param[i], out p2);
                                if (!(res != p2))
                                {
                                    return EXETypes.BooleanFalse;
                                }
                                res = p2;
                            }
                            return EXETypes.BooleanTrue;
                        case ">=":
                            double.TryParse(param[0], out res);
                            for (int i = 1; i < param.Length; i++)
                            {
                                double.TryParse(param[i], out p2);
                                if (!(res >= p2))
                                {
                                    return EXETypes.BooleanFalse;
                                }
                                res = p2;
                            }
                            return EXETypes.BooleanTrue;
                        case "+":
                            double.TryParse(param[0], out res);

                            for (int i = 1; i < param.Length; i++)
                            {
                                double.TryParse(param[i], out p2);
                                res += p2;
                            }
                            return res.ToString();
                        case "-":
                            double.TryParse(param[0], out res);
                            for (int i = 1; i < param.Length; i++)
                            {
                                double.TryParse(param[i], out p2);
                                res -= p2;
                            }
                            return res.ToString();
                        case "/":
                            double.TryParse(param[0], out res);
                            for (int i = 1; i < param.Length; i++)
                            {
                                double.TryParse(param[i], out p2);
                                res /= p2;
                            }
                            return res.ToString();
                        case "*":
                            double.TryParse(param[0], out res);
                            for (int i = 1; i < param.Length; i++)
                            {
                                double.TryParse(param[i], out p2);
                                res *= p2;
                            }
                            return res.ToString();

                        default:
                            return "FAIL";

                    }
                }
                else if (oper == "%")
                {
                    foreach (string paramx in param)
                    {
                        if (!int.TryParse(paramx, out p1))
                        {
                            return "FAIL";
                        }
                    }
                    int res;
                    int.TryParse(param[0], out res);
                    for (int i = 1; i < param.Length; i++)
                    {
                        int.TryParse(param[i], out p1);
                        res %= p1;
                    }
                    return res.ToString();
                }
                return "FAIL2";
            }
            else
            {
                foreach (string paramx in param)
                {
                    if (int.TryParse(paramx, out p1) || double.TryParse(paramx, out p2))
                    {
                        return "FAIL";
                    }
                }
            }

            b1 = param[0][0] == '\"';
            b2 = param[0][param[0].Length - 1] == '\"';

            if (b1 && b2)
            {
                if (param.Length < 2)
                {
                    return "FAIL";
                }
                if (oper == ">" || oper == "<" || oper == "<=" || oper == ">=" || oper == "==" || oper == "!=")
                {
                    foreach (string paramx in param)
                    {
                        if (!(paramx[paramx.Length - 1] == '\"') || !(paramx[0] == '\"'))
                        {
                            return "FAIL";
                        }
                    }
                    switch (oper)
                    {
                        case "<":
                            for (int i = 1; i < param.Length; i++)
                            {
                                if (!((param[i - 1]).Length < (param[i]).Length))
                                {
                                    return EXETypes.BooleanFalse;
                                }
                            }
                            return EXETypes.BooleanTrue;
                        case ">":
                            for (int i = 1; i < param.Length; i++)
                            {
                                if (!((param[i - 1]).Length > (param[i]).Length))
                                {
                                    return EXETypes.BooleanFalse;
                                }
                            }
                            return EXETypes.BooleanTrue;
                        case "<=":
                            for (int i = 1; i < param.Length; i++)
                            {
                                if (!((param[i - 1]).Length <= (param[i]).Length))
                                {
                                    return EXETypes.BooleanFalse;
                                }
                            }
                            return EXETypes.BooleanTrue;
                        case ">=":
                            for (int i = 1; i < param.Length; i++)
                            {
                                if (!((param[i - 1]).Length >= (param[i]).Length))
                                {
                                    return EXETypes.BooleanFalse;
                                }
                            }
                            return EXETypes.BooleanTrue;
                        case "==":
                            for (int i = 1; i < param.Length; i++)
                            {
                                if (!(param[i - 1] == param[i]))
                                {
                                    return EXETypes.BooleanFalse;
                                }
                            }
                            return EXETypes.BooleanTrue;
                        case "!=":
                            for (int i = 1; i < param.Length; i++)
                            {
                                if (!(param[i - 1] != param[i]))
                                {
                                    return EXETypes.BooleanFalse;
                                }
                            }
                            return EXETypes.BooleanTrue;
                        default:
                            return "FAIL";
                    }
                }
                return "FAIL";
            }
            else
            {
                foreach (string paramx in param)
                {
                    if (paramx[paramx.Length - 1] == '\"' || paramx[0] == '\"')
                    {
                        return "FAIL";
                    }
                }
            }


            bool.TryParse(param[0], out b1);

            if (bool.TryParse((param[0]).ToLower(), out _))
            {


                //"not" have only 1 parameter
                if (oper.ToLower() == "not")
                {
                    if (param.Length != 1) return EXETypes.BooleanFalse;
                    return (Boolean.TryParse(param[0], out _)) ? EXETypes.BooleanTrue : EXETypes.BooleanFalse  ;

                }

                if (oper == "==" || oper == "!=" || oper.ToLower() == "and" || oper.ToLower() == "or" || oper.ToLower() == "xor" || oper.ToLower() == "xnor")
                {
                    if (param.Length != 2) return EXETypes.BooleanFalse;

                    foreach (var value in param)
                    {
                        if (!Boolean.TryParse(value, out _)) return EXETypes.BooleanFalse;
                    }
                    return EXETypes.BooleanTrue;

                }
            }

            //if (DateTime.TryParseExact(param1, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture,System.Globalization.DateTimeStyles.NoCurrentDateDefault  , out DateTime datetime) && 
            //   DateTime.TryParseExact(param2, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture,System.Globalization.DateTimeStyles.NoCurrentDateDefault  , out datetime))
            //{
            //	return true;
            //}		
            return "FAIL";
        }
    }
}
