using System;
using System.IO;

namespace PasswordStrengthDLL
{
    public class Constants
    {
        public enum Strength
        {
            Blank,
            VeryWeak,
            Weak,
            Medium,
            Strong,
            VeryStrong
        };
        public const int LEVENSHTEIN_THRESHOLD = 1;
    }
}
