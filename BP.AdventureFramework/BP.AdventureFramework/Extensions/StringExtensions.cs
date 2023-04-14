﻿using System;
using System.Linq;
using BP.AdventureFramework.Assets;

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

            word = word.Trim(Convert.ToChar(" "));

            if (word.Contains(" "))
                word = word.Substring(0, word.IndexOf(" ", StringComparison.Ordinal));

            return word.Substring(word.Length - 1).ToUpper() == "S";
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

            if (IsPlural(word))
                return "some";
            if (IsVowel(word[0].ToString()) && word[0].ToString().ToUpper() != "U")
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
            return vowels.Any(x => x.Equals(value, StringComparison.CurrentCultureIgnoreCase));
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

        #endregion
    }
}
