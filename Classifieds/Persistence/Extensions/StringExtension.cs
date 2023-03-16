using System.Text.RegularExpressions;
using System;

namespace Classifieds.Persistence.Extensions
{
    public static class StringExtension
    {       
        public static string StripHTML(this string input)
        {
            return Regex.Replace(input, "<.*?>", String.Empty);
        }

        public static string IntoParagraph(this string input)
        {
            return "<p>" + input + "<p>";
        }
    }
}
