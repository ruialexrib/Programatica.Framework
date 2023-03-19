using System;
using System.Linq;

namespace Programatica.Framework.Core.Adapter
{
    /// <summary>
    /// Provides methods to evaluate password strength and enforce password policies.
    /// </summary>
    public class SecurityAdapter : ISecurityAdapter
    {
        /// <summary>
        /// Initializes a new instance of the SecurityAdapter class.
        /// </summary>
        public SecurityAdapter() : base()
        { }

        /// <summary>
        /// Generic method to retrieve password strength: use this for general purpose scenarios, 
        /// i.e. when you don't have a strict policy to follow.
        /// </summary>
        /// <param name="password">The password to be evaluated.</param>
        /// <returns>The strength of the password as a value of the PasswordStrength enum.</returns>

        public PasswordStrength GetPasswordStrength(string password)
        {
            int score = 0;
            if (String.IsNullOrEmpty(password) || String.IsNullOrEmpty(password.Trim())) return PasswordStrength.Blank;
            if (!HasMinimumLength(password, 5)) return PasswordStrength.VeryWeak;
            if (HasMinimumLength(password, 8)) score++;
            if (HasUpperCaseLetter(password) && HasLowerCaseLetter(password)) score++;
            if (HasDigit(password)) score++;
            if (HasSpecialChar(password)) score++;
            return (PasswordStrength)score;
        }

        /// <summary>
        /// Sample password policy implementation:
        /// - minimum 8 characters
        /// - at lease one UC letter
        /// - at least one LC letter
        /// - at least one non-letter char (digit OR special char)
        /// </summary>
        /// <returns>True if the password meets the criteria for a strong password, false otherwise</returns>
        public bool IsStrongPassword(string password)
        {
            return HasMinimumLength(password, 8)
                && HasUpperCaseLetter(password)
                && HasLowerCaseLetter(password)
                && (HasDigit(password) || HasSpecialChar(password));
        }


        /// <summary>
        /// Sample password policy implementation following the Microsoft.AspNetCore.Identity.PasswordOptions standard.
        /// </summary>
        /// <returns>Returns true if the password meets the specified criteria, otherwise false.</returns>
        public bool IsValidPassword(
            string password,
            int requiredLength,
            int requiredUniqueChars,
            bool requireNonAlphanumeric,
            bool requireLowercase,
            bool requireUppercase,
            bool requireDigit)
        {
            if (!HasMinimumLength(password, requiredLength)) return false;
            if (!HasMinimumUniqueChars(password, requiredUniqueChars)) return false;
            if (requireNonAlphanumeric && !HasSpecialChar(password)) return false;
            if (requireLowercase && !HasLowerCaseLetter(password)) return false;
            if (requireUppercase && !HasUpperCaseLetter(password)) return false;
            if (requireDigit && !HasDigit(password)) return false;
            return true;
        }

        #region Helper Methods

        /// <summary>
        /// Determines if a given password meets the minimum length requirement.
        /// </summary>
        /// <param name="password">The password to be checked.</param>
        /// <param name="minLength">The minimum length required for the password.</param>
        /// <returns>True if the password meets the minimum length requirement, false otherwise.</returns>
        public bool HasMinimumLength(string password, int minLength)
        {
            return password.Length >= minLength;
        }

        /// <summary>
        /// Checks if the password has a minimum number of unique characters.
        /// </summary>
        /// <param name="password">The password to check.</param>
        /// <param name="minUniqueChars">The minimum number of unique characters the password must have.</param>
        /// <returns>True if the password has at least the minimum number of unique characters, false otherwise.</returns>

        public bool HasMinimumUniqueChars(string password, int minUniqueChars)
        {
            return password.Distinct().Count() >= minUniqueChars;
        }

        /// <summary>
        /// Returns TRUE if the password has at least one digit.
        /// </summary>
        /// <param name="password">The password string to check.</param>
        /// <returns>TRUE if the password has at least one digit; otherwise, FALSE.</returns>
        public bool HasDigit(string password)
        {
            return password.Any(c => char.IsDigit(c));
        }

        /// <summary>
        /// Returns TRUE if the password has at least one special character
        /// </summary>
        /// <param name="password">The password to check for special characters</param>
        /// <returns>True if the password has at least one special character, False otherwise</returns>
        public bool HasSpecialChar(string password)
        {
            // return password.Any(c => char.IsPunctuation(c)) || password.Any(c => char.IsSeparator(c)) || password.Any(c => char.IsSymbol(c));
            return password.IndexOfAny("!@#$%^&*?_~-£().,".ToCharArray()) != -1;
        }

        /// <summary>
        /// Returns TRUE if the password has at least one uppercase letter
        /// </summary>
        /// <param name="password">The password to check.</param>
        /// <returns>True if the password has at least one uppercase letter; otherwise, false.</returns>
        public bool HasUpperCaseLetter(string password)
        {
            return password.Any(c => char.IsUpper(c));
        }

        /// <summary>
        /// Returns TRUE if the password has at least one lowercase letter
        /// </summary>
        /// <param name="password">The password string to check</param>
        /// <returns>A boolean indicating whether the password has at least one lowercase 
        public bool HasLowerCaseLetter(string password)
        {
            return password.Any(c => char.IsLower(c));
        }
        #endregion
    }
}
