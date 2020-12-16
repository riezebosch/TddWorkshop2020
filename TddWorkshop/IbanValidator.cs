using System;
using System.Linq;

namespace TddWorkshop
{
    public static class IbanValidator
    {
        public static bool IsValid(string input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            input = input.Replace(" ", "");
            
            return 
                !string.IsNullOrEmpty(input) 
                && input.StartsWith("NL")
                && int.TryParse(input.Substring(2, 2), out _)
                && new[] { "INGB", "RABO" }.Contains(input.Substring(4, 4));
        }
    }
}