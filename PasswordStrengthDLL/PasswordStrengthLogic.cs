using MinimumEditDistance;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PasswordStrengthDLL
{
    public static class PasswordStrengthLogic
    {
        /// <summary>
        /// Singleton List of similar passords
        /// </summary>
        private static List<string> _LstSimilarPasswords;
        public static List<string> GetSimilarPasswordList
        {
            get
            {
                if (_LstSimilarPasswords == null)
                {
                    _LstSimilarPasswords = new List<string>();
                }
                return _LstSimilarPasswords;
            }
            set
            {
                _LstSimilarPasswords = value;
            }
        }


        /// <summary>
        /// Main function that checks the password strength 
        /// </summary>
        /// <param name="pass"> a password to check</param>
        /// <returns>Return ENUM with password strength from Blank and Very Weak to Very Strong</returns>
        public static Constants.Strength CheckStrength(string pass)
        {
            // if the password is blank return the Blank Strength
            if (string.IsNullOrEmpty(pass))
            {
                GetSimilarPasswordList = null;
                return Constants.Strength.Blank;
            }
            if (pass.Length < 8)
            {
                GetSimilarPasswordList = null;
                return Constants.Strength.TooShort;
            }

            string PasswordListPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "passwordList.txt");
            int passwordScore = 0;

            passwordScore += HasDigit(pass, out bool boolHasDigit);
            passwordScore += HasLowerLetter(pass, out bool boolHasLowerLetter);
            passwordScore += HasSpecialCharacter(pass, out bool boolHasSpecialCharacter);
            passwordScore += HasUpperLetter(pass, out bool boolHasUpperLetter);

            Console.WriteLine($"Basic Validation:");

            ColoredConsoleWrite((passwordScore == 100 ? ConsoleColor.Green : ConsoleColor.Red), $"password score: {passwordScore} / 100");
            Console.Write("Digit included :");
            ColoredConsoleWrite((boolHasDigit ? ConsoleColor.Green : ConsoleColor.Red), $" {(boolHasDigit ? "Yes" : "No")}");
            Console.Write("Lower Letter included :");
            ColoredConsoleWrite((boolHasLowerLetter ? ConsoleColor.Green : ConsoleColor.Red), $" {(boolHasLowerLetter ? "Yes" : "No")}");
            Console.Write("Special Character included :");
            ColoredConsoleWrite((boolHasSpecialCharacter ? ConsoleColor.Green : ConsoleColor.Red), $" {(boolHasSpecialCharacter ? "Yes" : "No")}");
            Console.Write("Upper Letter included :");
            ColoredConsoleWrite((boolHasUpperLetter ? ConsoleColor.Green : ConsoleColor.Red), $" {(boolHasUpperLetter ? "Yes" : "No")}");

            if (passwordScore != 100)
            {
                Console.WriteLine($"Pasword '{pass}' must pass the basic tests before the Advanced qualifications");
                GetSimilarPasswordList = null;
                return Constants.Strength.VeryWeak;
            }
            Console.WriteLine($"Advanced Validation:");
            Console.WriteLine("Please wait...");
            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);

            var fileStream = new FileStream(PasswordListPath, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    var cost = LevenshteinMinimumEditDistance(line, pass);
                    if (cost < 4)
                    {
                        Console.WriteLine($"cost {cost} => {pass} is similar to {line} ||==> {line} added to list.  List Size - {GetSimilarPasswordList.Count}");
                        Console.WriteLine($"");
                        GetSimilarPasswordList.Add(line);
                    }
                }
            }
            int count = GetSimilarPasswordList.Count;
            if (count == 0)
            {
                return PrintFinalResult(count, pass, Constants.Strength.VeryStrong, ConsoleColor.Green);
            }
            else if (count > 0 && count < 4)
            {
                return PrintFinalResult(count, pass, Constants.Strength.Strong, ConsoleColor.Yellow);
            }
            else if (count > 3 && count < 10)
            {
                return PrintFinalResult(count, pass, Constants.Strength.Medium, ConsoleColor.DarkYellow);
            }
            else
            {
                return PrintFinalResult(count, pass, Constants.Strength.Weak, ConsoleColor.Red);
            }
        }

        /// <summary>
        /// Priunt the final result in the console , and return the Strength to the client
        /// </summary>
        /// <param name="count">the amount of passwords</param>
        /// <param name="pass">the password string</param>
        /// <param name="strength">the returned strength</param>
        /// <param name="consoleColor">the color of the strength</param>
        /// <returns>Strength</returns>
        private static Constants.Strength PrintFinalResult(int count, string pass, Constants.Strength strength, ConsoleColor consoleColor)
        {
            if (count > 0)
            {
                ColoredConsoleWrite(ConsoleColor.DarkCyan, $"found {count} similar passwords ");
            }
            else
            {
                ColoredConsoleWrite(ConsoleColor.Green, $"Couldnt find any similar passwords");
            }
            GetSimilarPasswordList = null;
            ColoredConsoleWrite(consoleColor, $"passsword {pass} is {strength.ToString()}");
            return strength;
        }

        /// <summary>
        /// Write to console in a specific Color
        /// </summary>
        /// <param name="color"> the color</param>
        /// <param name="text"> the text </param>
        public static void ColoredConsoleWrite(ConsoleColor color, string text)
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = originalColor;
        }

        /// <summary>
        /// a dynamic Function invoker for Improving Code Redundancy
        /// </summary>
        /// <param name="method"> the method which we like to call</param>
        /// <param name="pass"> the password we would like to check</param>
        /// <param name="has">  an out parameter that indicates if the method succeeded</param>
        /// <returns> return the score for the specific Test</returns>
        private static int FunctionInvoke(Func<string, bool> method, string pass, out bool has)
        {
            has = false;
            if (method(pass))
            {
                has = true;
                return 25;
            }
            return 0;
        }

        /// <summary>
        /// Checks if a string has a digits
        /// </summary>
        /// <param name="pass">string to check</param>
        /// <returns>if a match then gives a 10 point score</returns>
        private static int HasDigit(string pass, out bool has)
        {
            return FunctionInvoke(Helper.RgxHasDigit.IsMatch, pass, out has);
        }

        /// <summary>
        /// Checks if a string has a Lower case letter
        /// </summary>
        /// <param name="pass">string to check</param>
        /// <returns>if a match then gives a 10 point score</returns>
        private static int HasLowerLetter(string pass, out bool has)
        {
            return FunctionInvoke(Helper.RgxHasLowerLetter.IsMatch, pass, out has);
        }

        /// <summary>
        /// Checks if a string has a upper case letter
        /// </summary>
        /// <param name="pass">string to check</param>
        /// <returns>if a match then gives a 10 point score</returns>
        private static int HasUpperLetter(string pass, out bool has)
        {
            return FunctionInvoke(Helper.RgxHasUpperLetter.IsMatch, pass, out has);
        }

        /// <summary>
        /// Checks if a string has any Special character such as @, $, % etc included
        /// </summary>
        /// <param name="pass">string to check</param>
        /// <returns>if a match then gives a 10 point score</returns>
        private static int HasSpecialCharacter(string pass, out bool has)
        {
            return FunctionInvoke(Helper.RgxHasSpecialCharacter, pass, out has);
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
