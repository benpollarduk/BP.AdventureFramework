using System;
using System.Linq;
using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Utilities;

namespace BP.AdventureFramework.Extensions
{
    /// <summary>
    /// Provides extension methods for strings.
    /// </summary>
    public static class StringExtensions
    {
        #region Extensions

        /// <summary>
        /// Returns this string as a Description.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>This string as a description.</returns>
        public static Description ToDescription(this string value)
        {
            return new Description(value);
        }

        /// <summary>
        /// Returns this string as an Identifier.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>This string as an identifier.</returns>
        public static Identifier ToIdentifier(this string value)
        {
            return new Identifier(value);
        }

        /// <summary>
        /// Determine if this string equals an identifier.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="identifier">The identifier.</param>
        /// <returns>True if this string equals the identifier, else false.</returns>
        public static bool EqualsIdentifier(this string value, Identifier identifier)
        {
            return identifier.Equals(value);
        }

        /// <summary>
        /// Determine if this string equals an IExaminable.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="examinable">The examinable.</param>
        /// <returns>True if this string equals the identifier, else false.</returns>
        public static bool EqualsExaminable(this string value, IExaminable examinable)
        {
            return examinable.Identifier.Equals(value);
        }

        /// <summary>
        /// Get if a word is plural.
        /// </summary>
        /// <param name="word">The word to check.</param>
        /// <returns>True if the word is plural.</returns>
        public static bool IsPlural(this string word)
        {
            if (string.IsNullOrEmpty(word))
                return false;

            var space = Convert.ToChar(" ");
            var lastWord = word.TrimEnd(space).Split(space).Last();
            return lastWord.EndsWith("S", StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// Get an objectifier for a word.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <returns>The objectifier.</returns>
        public static string GetObjectifier(this string word)
        {
            if (string.IsNullOrEmpty(word))
                return string.Empty;

            var space = Convert.ToChar(" ");
            var firstWord = word.TrimStart(space).Split(space).First();
            var lastWord = word.TrimEnd(space).Split(space).Last();

            if (IsPlural(lastWord))
                return "some";
            if (IsVowel(firstWord[0].ToString()) && !firstWord.StartsWith("U", StringComparison.CurrentCultureIgnoreCase))
                return "an";

            return "a";
        }

        /// <summary>
        /// Get if a character is a vowel.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>True if the character is a vowel.</returns>
        public static bool IsVowel(this string value)
        {
            if (value.Length != 1)
                return false;

            var vowels = new[] { "A", "E", "I", "O", "U" };
            return vowels.Any(x => x.InsensitiveEquals(value));
        }

        /// <summary>
        /// Ensure this string is a finished sentence, ending in either ?, ! or .
        /// </summary>
        /// <param name="value">The string to finish.</param>
        /// <returns>The finished string.</returns>
        public static string EnsureFinishedSentence(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            if (value.EndsWith(".") || value.EndsWith("!") || value.EndsWith("?"))
                return value;

            if (value.EndsWith(","))
                return value.Substring(0, value.Length - 1) + ".";

            return value + ".";
        }

        /// <summary>
        /// Ensure this string is not a finished sentence, ending in either ?, ! or .
        /// </summary>
        /// <param name="value">The string to ensure isn't finished finish.</param>
        /// <returns>The unfinished string.</returns>
        public static string RemoveSentenceEnd(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            if (value.EndsWith(".") || value.EndsWith("!") || value.EndsWith("?"))
                return value.Remove(value.Length - 1);

            return value;
        }

        /// <summary>
        /// Convert a string to sentence case.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The word in sentence case.</returns>
        public static string ToSentenceCase(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            if (value.Length == 1)
                return value.ToUpper();

            var first = value.Substring(0, 1).ToUpper();
            var rest = value.Substring(1, value.Length - 1);

            return $"{first}{rest}";
        }

        /// <summary>
        /// Determine the number of lines in this string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The number of lines in the string.</returns>
        public static int LineCount(this string value)
        {
            return value?.Split(new[] { StringUtilities.Newline }, StringSplitOptions.RemoveEmptyEntries).Length ?? 0;
        }

        /// <summary>
        /// Convert a string to speech.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The value in sentence case.</returns>
        public static string ToSpeech(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return "\"\"";

            if (!value.StartsWith("\""))
                value = "\"" + value;

            if (!value.EndsWith("\""))
                value = value + "\"";

            return value;
        }

        /// <summary>
        /// Returns a value indicating whether a specified substring occurs within this string. This is not case sensitive.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="subString">The string to seek.</param>
        /// <returns>True if the value parameter occurs within this string, or if value is the empty string (""); otherwise, false.</returns>
        public static bool CaseInsensitiveContains(this string value, string subString)
        {
            var valueUpper = value.ToUpper();
            return valueUpper.Contains(subString.ToUpper());
        }

        /// <summary>
        /// Compare this string to another, with no case sensitivity.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="other">The other value.</param>
        /// <returns>The number of lines in the string.</returns>
        internal static bool InsensitiveEquals(this string value, string other)
        {
            return value?.Equals(other, StringComparison.InvariantCultureIgnoreCase) ?? false;
        }

        /// <summary>
        /// Add a sentence to this string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="other">The other value.</param>
        /// <returns>The concatenated string.</returns>
        internal static string AddSentence(this string value, string other)
        {
            if (string.IsNullOrEmpty(value))
                return other;

            if (string.IsNullOrEmpty(other))
                return value;

            return $"{value} {other}";
        }

        /// <summary>
        /// Ensure this string starts with a lower case character.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The modified string.</returns>
        internal static string StartWithLower(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            if (value.Length == 1)
                return value.ToLower();

            return $"{value.Substring(0, 1).ToLower()}{value.Substring(1)}";
        }

        #endregion
    }
}