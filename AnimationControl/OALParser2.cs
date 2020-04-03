﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    public class OALParser2
    {
        static void Main(string[] args)
        {

        }

        public static List<String> ArithmeticOperatorList = new List<String>(new String[] { "+", "-", "*", "/", "%" });
        // Do not forget that each operator ("<=") needs to be sooner than its substrings ("<")
        public static List<String> LogicalOperatorList = new List<String>(new String[] { "==", "!=", "<=", ">=", "and", "or", "not", "<", ">" });
        public static String AccessOperator = ".";
        public static String AssignmentOperator = "=";
        public static List<String> AllOperators = new List<String>(new String[] { "-", "+", "*", "/", "%", "==", "!=", "<", ">", "<=", ">=", "and", "or", "not", ".", "=" });
        public static List<List<String>> LeveledOperators = new List<List<String>>(new List<String>[] {
            new List<String> (new String[] { "and", "or"}),
            new List<String> (new String[] { "not"}),
            new List<String> (new String[] { "==", "!="}),
            new List<String> (new String[] { "<=", ">=", "<", ">" }),
            new List<String> (new String[] { "+", "-"}),
            new List<String> (new String[] { "*", "/", "%"}),
            new List<String> (new String[] { AccessOperator})
        });
        public static List<String> UnaryOperators = new List<String>(new String[] { "not", "empty", "not_empty", "cardinality" });
        public static int LongestOperatorLength = 3;
        private static List<String> Keywords = new List<String>(new String[]
            {
             "create", "object", "instance", "of",
            "relate", "to", "across", 
            "select", "any", "many", "from", "instances", "where", "related", "by",
            "if", "elif", "else", "end",
            "par", "thread",
            "while",
            "for", "each", "in",
            "break", "continue"
            });
        private static List<String> SpecialChars = new List<String>(new String[] { "[", "]", "->", ";", "(", ")" });
        public List<String> Tokenize(String Code)
        {
            List<String> Tokens = new List<String>();
            String CurrentCommand = "";
            Boolean InString = false;
            foreach (char c in Code)
            {
                if (InString)
                {
                    Console.WriteLine(c + " : " + CurrentCommand + " : " + CurrentCommand.Last());
                    if (c == '"' && CurrentCommand.Last() != '\\')
                    {
                        InString = false;
                        CurrentCommand += c;
                        Tokens.Add(CurrentCommand);
                        CurrentCommand = "";
                    }
                    else
                    {
                        CurrentCommand += c;
                    }
                    continue;
                }

                if (c == '"')
                {
                    if (CurrentCommand.Any())
                    {
                        Tokens.Add(CurrentCommand);
                        CurrentCommand = "";
                    }

                    CurrentCommand += c;
                    InString = true;
                    continue;
                }

                if(IsBracket(c))
                {
                    if (CurrentCommand.Any())
                    {
                        Tokens.Add(CurrentCommand);
                        CurrentCommand = "";
                    }

                    Tokens.Add(c.ToString());

                    continue;
                }
              
                if (Char.IsWhiteSpace(c))
                {
                    if (!"".Equals(CurrentCommand))
                    {
                        Tokens.Add(CurrentCommand);
                        CurrentCommand = "";
                    }
                    continue;
                }

                if (!CurrentCommand.Any())
                {
                    CurrentCommand += c;
                    continue;
                }

                if (IsNameChar(c) != IsNameChar(CurrentCommand.Last()))
                {
                    Tokens.Add(CurrentCommand);
                    CurrentCommand = c.ToString();
                    continue;
                }
                else if (IsNameChar(c) == IsNameChar(CurrentCommand.Last()))
                {
                    CurrentCommand += c;
                    continue;
                }

                throw new Exception("Uknown token");
            }

            if (!"".Equals(CurrentCommand))
            {
                Tokens.Add(CurrentCommand);
            }

            return Tokens;
        }
        private Boolean IsBracket(char c)
        {
            return c == '(' || c == ')' || c == '[' || c == ']';
        }
        private Boolean IsNameChar(char c)
        {
            return Char.IsLetterOrDigit(c) || c == '_';
        }
    }
}
