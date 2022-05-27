using Microsoft.AspNetCore.Identity;

namespace SchoolAssistant.Logic.Help
{
    public static class PasswordHelper
    {
        public static char[] AllowedCharacters => (UPPER_CASE + LOWER_CASE + DIGITS + NON_ALPHANUMERIC).ToCharArray();

        private const string UPPER_CASE = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string LOWER_CASE = "abcdefghijklmnopqrstuvwxyz";
        private const string DIGITS = "0123456789";
        private const string NON_ALPHANUMERIC = "!@$?_-";

        public static void ApplyDefaultOptionsTo(PasswordOptions opts)
        {
            // TODO: Password configuration here
        }

        public static string GenerateRandom()
        {
            var opts = new PasswordOptions();
            ApplyDefaultOptionsTo(opts);
            return GenerateRandom(opts);
        }

        /// <summary>
        /// Generates a Random Password
        /// respecting the given strength requirements.
        /// Taken from https://stackoverflow.com/a/46229180/17758185
        /// </summary>
        /// <param name="opts">A valid PasswordOptions object
        /// containing the password strength requirements.</param>
        /// <returns>A random password</returns>
        public static string GenerateRandom(PasswordOptions opts)
        {
            if (opts is null) throw new ArgumentNullException("Password options must not be null");

            string[] randomChars = new[] {
                UPPER_CASE,
                LOWER_CASE,
                DIGITS,
                NON_ALPHANUMERIC
            };

            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            if (opts.RequireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);

            if (opts.RequireLowercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

            if (opts.RequireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);

            if (opts.RequireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);

            for (int i = chars.Count; i < opts.RequiredLength
                || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }
    }
}
