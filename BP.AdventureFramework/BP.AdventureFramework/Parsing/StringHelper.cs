using System;
using System.Linq;

namespace BP.AdventureFramework.Parsing
{
    /// <summary>
    /// Provides a helper class for strings.
    /// </summary>
    public static class StringHelper
    {
        #region StaticProperties

        private static string[] Vowels { get; } = { "A", "E", "I", "O", "U" };

        #endregion

        #region StaticMethods

        /// <summary>
        /// Get an objectifier for a word.
        /// </summary>
        /// <param name="word">The word.</param>
        /// <returns>The objectifier.</returns>
        public static string GetObjectifier(string word)
        {
            if (string.IsNullOrEmpty(word))
                throw new ArgumentException("Parameter 'word' must have a value");

            if (IsPlural(word))
                return "some";
            if (IsVowel(word[0]) && word[0].ToString().ToUpper() != "U")
                return "an";

            return "a";
        }

        /// <summary>
        /// Get if a character is a vowel.
        /// </summary>
        /// <param name="c">The character to check.</param>
        /// <returns>True if the character is a vowel.</returns>
        public static bool IsVowel(char c)
        {
            return Vowels.Any(x => x.Equals(c.ToString(), StringComparison.CurrentCultureIgnoreCase));
        }

        /// <summary>
        /// Get if a word is plural.
        /// </summary>
        /// <param name="word">The word to check.</param>
        /// <returns>True if the word is plural.</returns>
        public static bool IsPlural(string word)
        {
            if (string.IsNullOrEmpty(word))
                throw new ArgumentException("Parameter 'word' must have a value");

            word = word.Trim(Convert.ToChar(" "));

            if (word.Contains(" "))
                word = word.Substring(0, word.IndexOf(" ", StringComparison.Ordinal));

            return word.Substring(word.Length - 1).ToUpper() == "S";
        }

        #endregion
    }
}
