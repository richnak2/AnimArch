using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AnimArch.Extensions
{
    public static class StringExtension
    {
        public static String Last(this String Value)
        {
            return String.IsNullOrEmpty(Value) ? null : Value[Value.Length - 1].ToString();
        }

        private static readonly Regex sWhitespace = new Regex(@"\s+");
        public static string ReplaceWhitespace(this string input, string replacement="")
        {
            return sWhitespace.Replace(input, replacement);
        }

        // https://stackoverflow.com/a/2641383
        public static List<int> AllIndexesOf(this string str, string value)
        {
            if (String.IsNullOrEmpty(value))
                throw new ArgumentException("the string to find may not be empty", "value");
            List<int> indexes = new List<int>();
            for (int index = 0; ; index += 1)
            {
                index = str.IndexOf(value, index);
                if (index == -1)
                    return indexes;
                indexes.Add(index);
            }
        }
    }
}
