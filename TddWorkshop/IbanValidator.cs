using System;

namespace TddWorkshop
{
    public static class IbanValidator
    {
        public static bool IsValid(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            if (!input.StartsWith("NL"))
            {
                return false;
            }
            
            return true;
        }
    }
}