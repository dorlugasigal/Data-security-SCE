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
        /// <summary>
        /// Main function that checks the password strength 
        /// </summary>
        /// <param name="pass"> a password to check</param>
        /// <returns>Return ENUM with password strength from Blank and Very Weak to Very Strong</returns>
        public static Constants.Strength CheckStrength(string pass)
        {
            if (string.IsNullOrEmpty(pass))
            {
                return Constants.Strength.Blank;
            }
            else if (HasDigit(pass))
            {
                return Constants.Strength.VeryStrong;
            }
            else
            {
                return Constants.Strength.VeryWeak;
            }
        }


        /// <summary>
        /// Checks if a string has a digits
        /// </summary>
        /// <param name="pass">string to check</param>
        /// <returns>True od False</returns>
        private static bool HasDigit(string pass)
        {
            return Helper.RgxHasDigit.IsMatch(pass);
        }

        /// <summary>
        /// Checks if a string has a Lower case letter
        /// </summary>
        /// <param name="pass">string to check</param>
        /// <returns>True od False</returns>
        private static bool HasLowerLetter(string pass)
        {
            return Helper.RgxHasLowerLetter.IsMatch(pass);
        }

        /// <summary>
        /// Checks if a string has a upper case letter
        /// </summary>
        /// <param name="pass">string to check</param>
        /// <returns>True od False</returns>
        private static bool HasUpperLetter(string pass)
        {
            return Helper.RgxHasUpperLetter.IsMatch(pass);
        }

        /// <summary>
        /// Checks if a string has any Special character such as @, $, % etc included
        /// </summary>
        /// <param name="pass">string to check</param>
        /// <returns>True od False</returns>
        private static bool HasSpecialCharacter(string pass)
        {
            return Helper.RgxHasSpecialCharacter.IsMatch(pass);
        }
    }
}
