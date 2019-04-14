using MinimumEditDistance;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
            string PasswordListPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "passwordList.txt");
            int passwordScore = 0;

            passwordScore += HasDigit(pass);
            passwordScore += HasLowerLetter(pass);
            passwordScore += HasSpecialCharacter(pass);
            passwordScore += HasUpperLetter(pass);


            var fileStream = new FileStream(PasswordListPath, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    var cost = LevenshteinMinimumEditDistance(line, pass);
                    if (cost < 4)
                    {
                        Console.WriteLine($"cost {cost} => {pass} is similar to {line}");
                    }
                }
            }

        }

    
        /// <summary>
        /// Checks if a string has a digits
        /// </summary>
        /// <param name="pass">string to check</param>
        /// <returns>if a match then gives a 10 point score</returns>
        private static int HasDigit(string pass)
        {
            return (Helper.RgxHasDigit.IsMatch(pass) ? 10 : 0);

        }

        /// <summary>
        /// Checks if a string has a Lower case letter
        /// </summary>
        /// <param name="pass">string to check</param>
        /// <returns>if a match then gives a 10 point score</returns>
        private static int HasLowerLetter(string pass)
        {
            return (Helper.RgxHasLowerLetter.IsMatch(pass) ? 10 : 0);

        }

        /// <summary>
        /// Checks if a string has a upper case letter
        /// </summary>
        /// <param name="pass">string to check</param>
        /// <returns>if a match then gives a 10 point score</returns>
        private static int HasUpperLetter(string pass)
        {
            return (Helper.RgxHasUpperLetter.IsMatch(pass) ? 10 : 0);
        }

        /// <summary>
        /// Checks if a string has any Special character such as @, $, % etc included
        /// </summary>
        /// <param name="pass">string to check</param>
        /// <returns>if a match then gives a 10 point score</returns>
        private static int HasSpecialCharacter(string pass)
        {
            return (Helper.RgxHasSpecialCharacter.IsMatch(pass) ? 10 : 0);
        }

        /// <summary>
        /// A function that uses the Levenstein algorithm using two strings and a substitution cost
        /// </summary>
        /// <param name="str1">First string to compare</param>
        /// <param name="str2">Second string to compare</param>
        /// <param name="cost">Substitution Cost</param>
        /// <returns>the distance between the two strings</returns>
        private static int LevenshteinMinimumEditDistance(string str1, string str2)
        {
            return Levenshtein.CalculateDistance(str1, str2, Constants.LEVENSHTEIN_THRESHOLD);
        }
    }
}
