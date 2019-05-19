using PasswordStrengthDLL;
using Strength = PasswordStrengthDLL.Constants.Strength;
using System;
namespace DemoConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Insert a password to check:  ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                string pass = Console.ReadLine();
                Console.ResetColor();

                Strength strength = PasswordStrengthLogic.CheckStrength(pass);
                switch (strength)
                {
                    case Strength.TooShort:
                        Console.WriteLine("Your password has less than 8 characters");
                        break;
                    case Strength.Blank:
                        Console.WriteLine("Your password is Blank");
                        break;
                    case Strength.VeryWeak:
                        Console.WriteLine("Your password is Very Weak");
                        break;
                    case Strength.Weak:
                        Console.WriteLine("Your password is Weak");
                        break;
                    case Strength.Medium:
                        Console.WriteLine("Your password is Medium");
                        break;
                    case Strength.Strong:
                        Console.WriteLine("Your password is Strong");
                        break;
                    case Strength.VeryStrong:
                        Console.WriteLine("Your password is Very Strong");
                        break;
                }
                Console.ResetColor();
                Console.WriteLine("\nEnter any key to try another password");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
