using PasswordStrengthDLL;
using Strength = PasswordStrengthDLL.Constants.Strength;

namespace DemoConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                System.Console.Write("Insert a password to check:  ");
                string pass = System.Console.ReadLine();
                Strength strength = PasswordStrengthLogic.CheckStrength(pass);
                switch (strength)
                {
                    case Strength.Blank:
                        System.Console.WriteLine("Your password is Blank");
                        break;
                    case Strength.VeryWeak:
                        System.Console.WriteLine("Your password is Very Weak");
                        break;
                    case Strength.Weak:
                        System.Console.WriteLine("Your password is Weak");
                        break;
                    case Strength.Medium:
                        System.Console.WriteLine("Your password is Medium");
                        break;
                    case Strength.Strong:
                        System.Console.WriteLine("Your password is Strong");
                        break;
                    case Strength.VeryStrong:
                        System.Console.WriteLine("Your password is Very Strong");
                        break;
                }
            }
        }
    }
}
