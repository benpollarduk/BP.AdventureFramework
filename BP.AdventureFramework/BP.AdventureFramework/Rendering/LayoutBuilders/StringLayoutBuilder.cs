using System;

namespace BP.AdventureFramework.Rendering.LayoutBuilders
{
    /// <summary>
    /// Provides a builder for laying out strings.
    /// </summary>
    public class StringLayoutBuilder : IStringLayoutBuilder
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the StringLayoutBuilder class.
        /// </summary>
        /// <param name="leftBoundaryCharacter">The character to use for left boundaries.</param>
        /// <param name="rightBoundaryCharacter">The character to use for right boundaries.</param>
        /// <param name="horizontalDividerCharacter">The character to use for horizontal dividers.</param>
        public StringLayoutBuilder(char leftBoundaryCharacter = (char)124, char rightBoundaryCharacter = (char)124, char horizontalDividerCharacter = (char)45)
        {
            LeftBoundaryCharacter = leftBoundaryCharacter;
            RightBoundaryCharacter = rightBoundaryCharacter;
            HorizontalDividerCharacter = horizontalDividerCharacter;
        }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Extract the next word from a string. This will remove the word from the input string.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>The extracted work.</returns>
        private static string ExtractNextWordFromString(ref string input)
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
        /// Build a horizontal divider.
        /// </summary>
        /// <param name="width">The width of the divider.</param>
        /// <param name="leftBoundary">The left boundary character.</param>
        /// <param name="rightBoundary">The right boundary character.</param>
        /// <param name="lineTerminator">The string to use for line termination.</param>
        /// <returns>The divider.</returns>
        private static string BuildWhiteSpaceWithBoundaryDivider(int width, char leftBoundary, char rightBoundary, string lineTerminator)
        {
            var divider = leftBoundary.ToString();

            for (var index = 0; index < width - 2; index++)
                divider += " ";

            divider += rightBoundary;
            divider += lineTerminator;

            return divider;
        }

        #endregion

        #region Implementation of IStringLayoutBuilder

        /// <summary>
        /// Get or set the character used for left boundary.
        /// </summary>
        public char LeftBoundaryCharacter { get; set; }

        /// <summary>
        /// Get or set the character used for right boundary.
        /// </summary>
        public char RightBoundaryCharacter { get; set; }

        /// <summary>
        /// Get or set the character used for horizontal dividers.
        /// </summary>
        public char HorizontalDividerCharacter { get; set; }

        /// <summary>
        /// Get or set the line terminator.
        /// </summary>
        public string LineTerminator { get; set; } = Environment.NewLine;

        /// <summary>
        /// Build a horizontal divider.
        /// </summary>
        /// <param name="width">The width of the divider.</param>
        /// <returns>The divider.</returns>
        public string BuildHorizontalDivider(int width)
        {
            if (width <= 0)
                throw new ArgumentException("The width parameter must be greater than 0.");

            var divider = LeftBoundaryCharacter.ToString();

            for (var index = 0; index < width - 2; index++)
                divider += HorizontalDividerCharacter;

            divider += RightBoundaryCharacter;
            divider += LineTerminator;
            return divider;
        }

        /// <summary>
        /// Build a padded area.
        /// </summary>
        /// <param name="width">The width of the padded area.</param>
        /// <param name="height">The height of the padded area.</param>
        /// <returns>A padded area.</returns>
        public string BuildPaddedArea(int width, int height)
        {
            var paddedArea = string.Empty;

            for (var index = 0; index < height; index++)
                paddedArea += BuildWhiteSpaceWithBoundaryDivider(width, LeftBoundaryCharacter, RightBoundaryCharacter, LineTerminator);

            return paddedArea;
        }

        /// <summary>
        /// Build a wrapped padded string.
        /// </summary>
        /// <param name="value">The string to pad.</param>
        /// <param name="width">The overall width of the padded string.</param>
        /// <param name="centralise">True if the string should be centralised.</param>
        /// <returns>The padded string.</returns>
        public string BuildWrappedPadded(string value, int width, bool centralise)
        {
            if (width <= 0)
                throw new ArgumentException("The width parameter must be greater than 0.");

            var wrappedString = string.Empty;
            var availableTextSpace = width - 3;

            if (value.Length > availableTextSpace)
            {
                var chunk = string.Empty;
                var word = string.Empty;

                while (value.Length > 0)
                {
                    while (chunk.Length <= availableTextSpace)
                    {
                        if (!string.IsNullOrEmpty(word))
                            chunk += word;

                        word = ExtractNextWordFromString(ref value) + " ";

                        if (chunk.Length + word.Length <= availableTextSpace)
                        {
                            chunk += word;
                            word = string.Empty;
                        }
                        else
                        {
                            break;
                        }
                    }

                    wrappedString += LeftBoundaryCharacter + " " + chunk + BuildWhitespace(availableTextSpace - chunk.Length) + RightBoundaryCharacter + LineTerminator;
                    chunk = string.Empty;
                }

                if (string.IsNullOrEmpty(word) || word == " ")
                    return wrappedString;

                if (centralise)
                    wrappedString += BuildCentralised(word, width);
                else
                    wrappedString += LeftBoundaryCharacter + " " + word + BuildWhitespace(availableTextSpace - word.Length) + RightBoundaryCharacter + LineTerminator;
            }
            else
            {
                if (centralise)
                    wrappedString += BuildCentralised(value, width);
                else
                    wrappedString = LeftBoundaryCharacter + " " + value + BuildWhitespace(availableTextSpace - value.Length) + RightBoundaryCharacter + LineTerminator;
            }

            return wrappedString;
        }

        /// <summary>
        /// Build a string made of whitespace.
        /// </summary>
        /// <param name="width">The width of the whitespace.</param>
        /// <returns>The whitespace string.</returns>
        public string BuildWhitespace(int width)
        {
            var whiteSpace = string.Empty;

            for (var index = 0; index < width; index++)
                whiteSpace += " ";

            return whiteSpace;
        }

        /// <summary>
        /// Build a centralised string.
        /// </summary>
        /// <param name="value">The string to centralise.</param>
        /// <param name="width">The overall width of the string.</param>
        /// <returns>The centralised string.</returns>
        public string BuildCentralised(string value, int width)
        {
            if (width <= 0)
                throw new ArgumentException("The width parameter must be greater than 0.");

            if (value.Length + 2 >= width)
                return BuildWrappedPadded(value, width, true);

            var constructedString = LeftBoundaryCharacter.ToString();
            var startPosition = width / 2 - value.Length / 2;
            constructedString += BuildWhitespace(startPosition - 1);
            constructedString += value;
            constructedString += BuildWhitespace(width - 1 - constructedString.Length);
            constructedString += RightBoundaryCharacter;
            constructedString += LineTerminator;

            return constructedString;
        }

        #endregion
    }
}
