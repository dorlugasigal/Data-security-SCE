using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PasswordStrengthDLL
{
    public static class PasswordStrengthLogic
    {
        public static Constants.Strength CheckStrength(string pass)
        {
            if (HasDigit(pass))
            {
                return Constants.Strength.VeryStrong;
            }
            else
            {
                return Constants.Strength.VeryWeak;
            }
        }

        private static bool HasDigit(string pass)
        {
            return Constants.RgxHasDigit.IsMatch(pass);
        }
    }
}
