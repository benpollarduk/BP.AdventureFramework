using System;

namespace BP.AdventureFramework.Utilities
{
    /// <summary>
    /// Provides a helper class for string interpretation.
    /// </summary>
    internal static class StringUtilities
    {
        /// <summary>
        /// Extract the next word from a string. This will remove the word from the input string.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>The extracted work.</returns>
        internal static string ExtractNextWordFromString(ref string input)
        {
            var word = string.Empty;
            var space = Convert.ToChar(" ");
            input = input.TrimStart(space);

            foreach (var t in input)
            {
                if (t != space)
                    word += t;
                else
                    break;
            }

            input = input.Remove(0, word.Length);
            word = word.Trim();
            return word;
        }
        
        /// <summary>
        /// Cut a line from a paragraph.
        /// </summary>
        /// <param name="paragraph">The paragraph.</param>
        /// <param name="maxWidth">The max line length.</param>
        /// <returns>The line cut from the paragraph.</returns>
        internal static string CutLineFromParagraph(ref string paragraph, int maxWidth)
        {
            var chunk = string.Empty;

            while (chunk.Length < maxWidth)
            {
                var word = ExtractNextWordFromString(ref paragraph);

                if (string.IsNullOrEmpty(word))
                    break;

                if (chunk.Length + word.Length <= maxWidth)
                {
                    if (!string.IsNullOrEmpty(paragraph))
                        word += " ";

                    chunk += word;
                }
                else
                {
                    paragraph = word + " " + paragraph;
                    break;
                }
            }

            return chunk.TrimEnd();
        }
    }
}
