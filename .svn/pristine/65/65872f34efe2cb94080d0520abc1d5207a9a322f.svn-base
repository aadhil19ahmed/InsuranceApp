using System;
using System.Text.RegularExpressions;

namespace ACIA.Helper.Validations
{
    public static class Validator
    {
        public static bool Validate(object capturedValue, string regex)
        {
            bool validZipCode = true;
            if(capturedValue == null)
            {
                return false;
            }
            else if((!Regex.Match((string)capturedValue, Convert(regex)).Success))
            {
                validZipCode = false;
            }
            return validZipCode;
        }

        private static string Convert(string regex)
        {
            switch (regex)
            {
                case "email":
                    return @"^\d{5}(?:[-\s]\d{4})?$";
                default:
                    return string.Empty;
            }
        }
    }
}
