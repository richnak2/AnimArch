using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimationControl
{
    public class EXEParseUtil
    {
        public static String RemoveWhitespace(String String)
        {
            StringBuilder FilteredStringBuilder = new StringBuilder();
            foreach (char c in String)
            {
                if (!Char.IsWhiteSpace(c))
                {
                    FilteredStringBuilder.Append(c);
                }
            }
            return FilteredStringBuilder.ToString();
        }

        public static String SqueezeWhiteSpace(String String)
        {
            int FirstNonWSIndex = 0;
            while (FirstNonWSIndex < String.Length && Char.IsWhiteSpace(String[FirstNonWSIndex]))
            {
                FirstNonWSIndex++;
            }
            int LastNonWSIndex = String.Length - 1;
            while (FirstNonWSIndex > 0 && Char.IsWhiteSpace(String[LastNonWSIndex]))
            {
                LastNonWSIndex--;
            }
            // This means that the string is only whitespce
            if (LastNonWSIndex == 0 && FirstNonWSIndex != 0)
            {
                return "";
            }
            String TrimmedString = String.Substring(FirstNonWSIndex, LastNonWSIndex - FirstNonWSIndex + 1);

            StringBuilder FilteredStringBuilder = new StringBuilder();
            int WhitespaceCount = 0;
            foreach (char c in TrimmedString)
            {
                if (!Char.IsWhiteSpace(c))
                {
                    WhitespaceCount = 0;
                    FilteredStringBuilder.Append(c);
                }
                else
                {
                    if (WhitespaceCount == 0)
                    {
                        FilteredStringBuilder.Append(' ');
                    }
                    WhitespaceCount++;
                }
            }
            return FilteredStringBuilder.ToString();
        }
    }
}
