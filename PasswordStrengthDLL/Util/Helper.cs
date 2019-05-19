using MinimumEditDistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PasswordStrengthDLL
{
    public static class Helper
    {
        public static Regex RgxHasDigit = new Regex(@"\d");
        public static Regex RgxHasUpperLetter = new Regex(@"[A-Z]");
        public static Regex RgxHasLowerLetter = new Regex(@"[a-z]");
        public static bool RgxHasSpecialCharacter(string pass)
        {
            for (int i = 0; i < pass.Length; i++)
            {
                if (!Char.IsLetterOrDigit(pass[i]))
                {
                    return true;
                }
            }
            return false;
        }

        //CHANGE
        public static void ASD()
        {
            Levenshtein.CalculateDistance("a", "b", 1);
        }
    }

}
