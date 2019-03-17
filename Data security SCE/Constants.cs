using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PasswordStrengthDLL
{
    public class Constants
    {
        public enum Strength
        {
            VeryWeak,
            Weak,
            Medium,
            Strong,
            VeryStrong
        };

        public static Regex RgxHasDigit = new Regex(@"\d");
    }
}
