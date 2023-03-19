namespace Programatica.Framework.Core.Adapter
{
    /// <summary>
    /// Defines methods for validating password strength and complexity.
    /// </summary>
    public interface ISecurityAdapter 
    {
        /// <summary>
        /// Determines the strength of a password.
        /// </summary>
        /// <param name="password">The password to check.</param>
        /// <returns>The strength level of the password.</returns>
        PasswordStrength GetPasswordStrength(string password);

        /// <summary>
        /// Determines whether a password is strong enough.
        /// </summary>
        /// <param name="password">The password to check.</param>
        /// <returns>True if the password is strong enough; otherwise, false.</returns>
        bool IsStrongPassword(string password);

        /// <summary>
        /// Determines whether a password meets the specified complexity requirements.
        /// </summary>
        /// <param name="password">The password to check.</param>
        /// <param name="requiredLength">The required minimum length of the password.</param>
        /// <param name="requiredUniqueChars">The required number of unique characters in the password.</param>
        /// <param name="requireNonAlphanumeric">Whether to require at least one non-alphanumeric character.</param>
        /// <param name="requireLowercase">Whether to require at least one lowercase letter.</param>
        /// <param name="requireUppercase">Whether to require at least one uppercase letter.</param>
        /// <param name="requireDigit">Whether to require at least one digit.</param>
        /// <returns>True if the password meets the specified complexity requirements; otherwise, false.</returns>
        bool IsValidPassword(
            string password,
            int requiredLength,
            int requiredUniqueChars,
            bool requireNonAlphanumeric,
            bool requireLowercase,
            bool requireUppercase,
            bool requireDigit);
    }

    /// <summary>
    /// Specifies the strength level of a password.
    /// </summary>
    public enum PasswordStrength
    {
        /// <summary>
        /// Blank Password (empty and/or space chars only)
        /// </summary>
        Blank = 0,
        /// <summary>
        /// Either too short (less than 5 chars), one-case letters only or digits only
        /// </summary>
        VeryWeak = 1,
        /// <summary>
        /// At least 5 characters, one strong condition met (>= 8 chars with 1 or more UC letters, LC letters, digits & special chars)
        /// </summary>
        Weak = 2,
        /// <summary>
        /// At least 5 characters, two strong conditions met (>= 8 chars with 1 or more UC letters, LC letters, digits & special chars)
        /// </summary>
        Medium = 3,
        /// <summary>
        /// At least 8 characters, three strong conditions met (>= 8 chars with 1 or more UC letters, LC letters, digits & special chars)
        /// </summary>
        Strong = 4,
        /// <summary>
        /// At least 8 characters, all strong conditions met (>= 8 chars with 1 or more UC letters, LC letters, digits & special chars)
        /// </summary>
        VeryStrong = 5
    }
}
