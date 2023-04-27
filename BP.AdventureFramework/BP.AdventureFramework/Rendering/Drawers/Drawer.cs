using System;

namespace BP.AdventureFramework.Rendering.Drawers
{
    /// <summary>
    /// A class for drawing strings on a console window.
    /// </summary>
    public class Drawer
    {
        #region Properties

        /// <summary>
        /// Get or set the character used for left boundaries.
        /// </summary>
        public char LeftBoundaryCharacter { get; set; } = Convert.ToChar("|");

        /// <summary>
        /// Get or set the character used for right boundaries.
        /// </summary>
        public char RightBoundaryCharacter { get; set; } = Convert.ToChar("|");

        /// <summary>
        /// Get or set the character used for dividers.
        /// </summary>
        public char DividerCharacter { get; set; } = Convert.ToChar("-");

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Drawer class.
        /// </summary>
        /// <param name="leftBoundaryCharacter">The character to use for left boundaries.</param>
        /// <param name="rightBoundaryCharacter">The character to use for right boundaries.</param>
        /// <param name="dividingCharacter">The character to use for dividers.</param>
        public Drawer(char leftBoundaryCharacter = (char)124, char rightBoundaryCharacter = (char)124, char dividingCharacter = (char)45)
        {
            LeftBoundaryCharacter = leftBoundaryCharacter;
            RightBoundaryCharacter = rightBoundaryCharacter;
            DividerCharacter = dividingCharacter;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Construct a dividing horizontal line.
        /// </summary>
        /// <param name="width">The width of the divider.</param>
        /// <returns>A constructed divider.</returns>
        public string ConstructDivider(int width)
        {
            return ConstructDivider(width, LeftBoundaryCharacter, DividerCharacter, RightBoundaryCharacter);
        }

        /// <summary>
        /// Construct a padded area.
        /// </summary>
        /// <param name="width">The width of the padded area.</param>
        /// <param name="height">The height of the padded area.</param>
        /// <returns>A constructed padded area.</returns>
        public string ConstructPaddedArea(int width, int height)
        {
            return ConstructPaddedArea(LeftBoundaryCharacter, RightBoundaryCharacter, width, height);
        }

        /// <summary>
        /// Construct a padded area.
        /// </summary>
        /// <param name="leftBoundary">The left boundary string.</param>
        /// <param name="rightBoundary">The right boundary string.</param>
        /// <param name="width">The width of the padded area.</param>
        /// <param name="height">The height of the padded area.</param>
        /// <returns>A constructed padded area.</returns>
        public string ConstructPaddedArea(char leftBoundary, char rightBoundary, int width, int height)
        {
            var paddedArea = string.Empty;

            for (var index = 0; index < height; index++)
                paddedArea += ConstructWhiteSpaceWithBoundaryDivider(width, leftBoundary, rightBoundary);

            return paddedArea;
        }

        /// <summary>
        /// Construct a dividing horizontal line.
        /// </summary>
        /// <param name="width">The width of the divider.</param>
        /// <param name="leftBoundary">The left boundary character.</param>
        /// <param name="dividerString">The dividing character.</param>
        /// <param name="rightBoundary">The right boundary character.</param>
        /// <returns>A constructed divider.</returns>
        public string ConstructDivider(int width, char leftBoundary, char dividerString, char rightBoundary)
        {
            if (width <= 0)
                throw new ArgumentException("The width parameter must be greater than 0.");

            var divider = leftBoundary.ToString();

            for (var index = 0; index < width - 3; index++)
                divider += dividerString;

            divider += rightBoundary;
            divider += Environment.NewLine;
            return divider;
        }

        /// <summary>
        /// Construct a wrapped padded string, ready for display.
        /// </summary>
        /// <param name="displayString">The string to pad.</param>
        /// <param name="width">The desired overall width of the padded string.</param>
        /// <returns>A padded string.</returns>
        public string ConstructWrappedPaddedString(string displayString, int width)
        {
            return ConstructWrappedPaddedString(displayString, width, LeftBoundaryCharacter, RightBoundaryCharacter, false);
        }

        /// <summary>
        /// Construct a dividing horizontal line.
        /// </summary>
        /// <param name="width">The width of the divider.</param>
        /// <param name="leftBoundary">The left boundary character.</param>
        /// <param name="rightBoundary">The right boundary character.</param>
        /// <returns>A constructed divider.</returns>
        private string ConstructWhiteSpaceWithBoundaryDivider(int width, char leftBoundary, char rightBoundary)
        {
            var divider = leftBoundary.ToString();

            for (var index = 0; index < width - 3; index++)
                divider += " ";

            divider += rightBoundary;
            divider += Environment.NewLine;

            return divider;
        }

        /// <summary>
        /// Construct a string made of whitespace.
        /// </summary>
        /// <param name="width">The width of the whitespace.</param>
        /// <returns>A string constructed of whitespace.</returns>
        public string ConstructWhitespaceString(int width)
        {
            var whiteSpace = string.Empty;

            for (var index = 0; index < width; index++)
                whiteSpace += " ";

            return whiteSpace;
        }

        /// <summary>
        /// Construct a centralised string, ready for display.
        /// </summary>
        /// <param name="displayString">The string to pad.</param>
        /// <param name="width">The desired overall width of the padded string.</param>
        /// <param name="leftBoundary">The left boundary character.</param>
        /// <param name="rightBoundary">The right boundary character.</param>
        /// <returns>A padded string.</returns>
        public string ConstructCentralisedString(string displayString, int width, char leftBoundary, char rightBoundary)
        {
            if (width <= 0)
                throw new ArgumentException("The width parameter must be greater than 0.");

            if (displayString.Length + 2 >= width) 
                return ConstructWrappedPaddedString(displayString, width, leftBoundary, rightBoundary, true);

            var constructedString = leftBoundary.ToString();
            var startPosition = width / 2 - displayString.Length / 2;
            constructedString += ConstructWhitespaceString(startPosition - 1);
            constructedString += displayString;
            constructedString += ConstructWhitespaceString(width - 1 - constructedString.Length - 1);
            constructedString += rightBoundary;
            constructedString += Environment.NewLine;

            return constructedString;
        }

        /// <summary>
        /// Construct a centralised string, ready for display.
        /// </summary>
        /// <param name="displayString">The string to pad.</param>
        /// <param name="width">The desired overall width of the padded string.</param>
        /// <returns>A padded string.</returns>
        public string ConstructCentralisedString(string displayString, int width)
        {
            return ConstructCentralisedString(displayString, width, LeftBoundaryCharacter, RightBoundaryCharacter);
        }

        /// <summary>
        /// Construct a wrapped padded string, ready for display.
        /// </summary>
        /// <param name="displayString">The string to pad.</param>
        /// <param name="width">The desired overall width of the padded string.</param>
        /// <param name="centralise">Specify if the string should be centralised.</param>
        /// <returns>A padded string.</returns>
        public virtual string ConstructWrappedPaddedString(string displayString, int width, bool centralise)
        {
            return ConstructWrappedPaddedString(displayString, width, LeftBoundaryCharacter, RightBoundaryCharacter, centralise);
        }

        /// <summary>
        /// Construct a wrapped padded string, ready for display.
        /// </summary>
        /// <param name="displayString">The string to pad.</param>
        /// <param name="width">The desired overall width of the padded string.</param>
        /// <param name="leftBoundary">The left boundary character.</param>
        /// <param name="rightBoundary">The right boundary character.</param>
        /// <param name="centralise">set if the string should be centralised.</param>
        /// <returns>A padded string</returns>
        public string ConstructWrappedPaddedString(string displayString, int width, char leftBoundary, char rightBoundary, bool centralise)
        {
            if (width <= 0)
                throw new ArgumentException("The width parameter must be greater than 0.");

            var wrappedString = string.Empty;
            var availableTextSpace = width - 4;

            if (displayString.Length > availableTextSpace)
            {
                var chunk = string.Empty;
                var word = string.Empty;

                while (displayString.Length > 0)
                {
                    while (chunk.Length <= availableTextSpace)
                    {
                        if (!string.IsNullOrEmpty(word))
                            chunk += word;

                        word = ExtractNextWordFromString(ref displayString) + " ";

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

                    wrappedString += leftBoundary + " " + chunk + ConstructWhitespaceString(availableTextSpace - chunk.Length) + rightBoundary + Environment.NewLine;
                    chunk = string.Empty;
                }

                if (string.IsNullOrEmpty(word) || word == " ") 
                    return wrappedString;
                
                if (centralise)
                    wrappedString += ConstructCentralisedString(word, width, leftBoundary, rightBoundary);
                else
                    wrappedString += leftBoundary + " " + word + ConstructWhitespaceString(availableTextSpace - word.Length) + rightBoundary + Environment.NewLine;
            }
            else
            {
                if (centralise)
                    wrappedString += ConstructCentralisedString(displayString, width, leftBoundary, rightBoundary);
                else
                    wrappedString = leftBoundary + " " + displayString + ConstructWhitespaceString(availableTextSpace - displayString.Length) + rightBoundary + Environment.NewLine;
            }

            return wrappedString;
        }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Extract the next word from a string.
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

        #endregion
    }
}