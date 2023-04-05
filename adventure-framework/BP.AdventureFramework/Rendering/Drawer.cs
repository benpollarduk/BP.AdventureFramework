using System;

namespace AdventureFramework.Rendering
{
    /// <summary>
    /// A class for drawing strings
    /// </summary>
    public abstract class Drawer
    {
        #region StaticMethods

        /// <summary>
        /// Extract the next word from a string
        /// </summary>
        /// <param name="input">The input string</param>
        /// <returns>The extracted work</returns>
        private static string extractNextWordFromString(ref string input)
        {
            // create word
            var word = string.Empty;

            // get character for space
            var space = Convert.ToChar(" ");

            // trim preceeding space
            input = input.TrimStart(space);

            // find space
            for (var index = 0; index < input.Length; index++)
                // if not a space
                if (input[index] != space)
                    // add character
                    word += input[index];
                else
                    // break itteration
                    break;

            // remove the word from input
            input = input.Remove(0, word.Length);

            // trim whitespace
            word = word.Trim();

            // return
            return word;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Get or set the character used for left boundaries
        /// </summary>
        public char LeftBoundaryCharacter
        {
            get { return leftBoundaryCharacter; }
            set { leftBoundaryCharacter = value; }
        }

        /// <summary>
        /// Get or set the character used for left boundaries
        /// </summary>
        private char leftBoundaryCharacter = Convert.ToChar("|");

        /// <summary>
        /// Get or set the character used for right boundaries
        /// </summary>
        public char RightBoundaryCharacter
        {
            get { return rightBoundaryCharacter; }
            set { rightBoundaryCharacter = value; }
        }

        /// <summary>
        /// Get or set the character used for right boundaries
        /// </summary>
        private char rightBoundaryCharacter = Convert.ToChar("|");

        #endregion

        #region Methods

        /// <summary>
        /// Construct a padded area
        /// </summary>
        /// <param name="width">The width of the padded area</param>
        /// <param name="height">The height of the padded area</param>
        /// <returns>A constructed padded area</returns>
        public virtual string ConstructPaddedArea(int width, int height)
        {
            return ConstructPaddedArea(LeftBoundaryCharacter, RightBoundaryCharacter, width, height);
        }

        /// <summary>
        /// Construct a padded area
        /// </summary>
        /// <param name="leftBoundary">The left boundary string</param>
        /// <param name="rightBoundary">The right boundary string</param>
        /// <param name="width">The width of the padded area</param>
        /// <param name="height">The height of the padded area</param>
        /// <returns>A constructed padded area</returns>
        public virtual string ConstructPaddedArea(char leftBoundary, char rightBoundary, int width, int height)
        {
            // hold padded area
            var paddedArea = string.Empty;

            // pad white space
            for (var index = 0; index < height; index++)
                // construct
                paddedArea += constructWhiteSpaceWithBoundaryDevider(width, leftBoundary, rightBoundary);

            // return area
            return paddedArea;
        }

        /// <summary>
        /// Construct a deviding horizontal line
        /// </summary>
        /// <param name="width">The width of the devider</param>
        /// <param name="leftBoundary">The left boundary character</param>
        /// <param name="deviderString">The deviding character</param>
        /// <param name="rightBoundary">The right boundary character</param>
        /// <returns>A constructed devider</returns>
        public virtual string ConstructDevider(int width, char leftBoundary, char deviderString, char rightBoundary)
        {
            // if width is too small
            if (width <= 0)
                // throw exception
                throw new ArgumentException("The width parameter must be greater than 0");

            // the left character
            var devider = leftBoundary.ToString();

            // itterate width
            for (var index = 0; index < width - 3; index++)
                // add deviding character
                devider += deviderString;

            // add right boundary
            devider += rightBoundary;

            // add new line
            devider += "\n";

            // return
            return devider;
        }

        /// <summary>
        /// Construct a wrapped padded string, ready for display
        /// </summary>
        /// <param name="displayString">The string to pad</param>
        /// <param name="width">The desired overall width of the padded string</param>
        /// <returns>A padded string</returns>
        public virtual string ConstructWrappedPaddedString(string displayString, int width)
        {
            // construct
            return ConstructWrappedPaddedString(displayString, width, LeftBoundaryCharacter, RightBoundaryCharacter, false);
        }

        /// <summary>
        /// Construct a deviding horizontal line
        /// </summary>
        /// <param name="width">The width of the devider</param>
        /// <param name="leftBoundary">The left boundary character</param>
        /// <param name="rightBoundary">The right boundary character</param>
        /// <returns>A constructed devider</returns>
        private string constructWhiteSpaceWithBoundaryDevider(int width, char leftBoundary, char rightBoundary)
        {
            // get a devider
            var devider = leftBoundary.ToString();

            // itterate width
            for (var index = 0; index < width - 3; index++)
                // add whitespace
                devider += " ";

            // add right boundary
            devider += rightBoundary;

            // add new line
            devider += "\n";

            // return
            return devider;
        }

        /// <summary>
        /// Construct a string made of whitespace
        /// </summary>
        /// <param name="width">The width of the whitespace</param>
        /// <returns>A string constructed of whitespace</returns>
        public virtual string ConstructWhitespaceString(int width)
        {
            // get a devider
            var whiteSpace = string.Empty;

            // itterate width
            for (var index = 0; index < width; index++)
                // add deviding character
                whiteSpace += " ";

            // return
            return whiteSpace;
        }

        /// <summary>
        /// Construct a unboardered, centralised string
        /// </summary>
        /// <param name="displayString">The string to pad</param>
        /// <param name="width">The desired overall width of the padded string</param>
        /// <returns>A padded string</returns>
        public string ConstructUnboarderedCentralisedString(string displayString, int width)
        {
            // if width is too small
            if (width <= 0)
                // throw exception
                throw new ArgumentException("The width parameter must be greater than 0");

            // if too large
            if (displayString.Length > width)
                // throw exception
                throw new ArgumentException("The length of the displayString parameter is greater than the width paramater");

            // hold constructed string
            var constructedString = string.Empty;

            // hold start position
            var startPosition = width / 2 - displayString.Length / 2;

            // add white space
            constructedString += ConstructWhitespaceString(startPosition - 1);

            // add string
            constructedString += displayString;

            // add right buffer
            constructedString += ConstructWhitespaceString(width - constructedString.Length);

            // add end
            constructedString += "\n";

            // return
            return constructedString;
        }

        /// <summary>
        /// Construct a centralised string, ready for display
        /// </summary>
        /// <param name="displayString">The string to pad</param>
        /// <param name="width">The desired overall width of the padded string</param>
        /// <param name="leftBoundary">The left boundary character</param>
        /// <param name="rightBoundary">The right boundary character</param>
        /// <returns>A padded string</returns>
        public virtual string ConstructCentralisedString(string displayString, int width, char leftBoundary, char rightBoundary)
        {
            // if width is too small
            if (width <= 0)
                // throw exception
                throw new ArgumentException("The width parameter must be greater than 0");

            // if short enough
            if (displayString.Length + 2 < width)
            {
                // hold constructed string
                var constructedString = leftBoundary.ToString();

                // hold start position
                var startPosition = width / 2 - displayString.Length / 2;

                // add white space
                constructedString += ConstructWhitespaceString(startPosition - 1);

                // add string
                constructedString += displayString;

                // add right buffer
                constructedString += ConstructWhitespaceString(width - 1 - constructedString.Length - 1);

                // add right string
                constructedString += rightBoundary;

                // add end
                constructedString += "\n";

                // return
                return constructedString;
            }

            // wrap and centralise
            return ConstructWrappedPaddedString(displayString, width, leftBoundary, rightBoundary, true);
        }

        /// <summary>
        /// Construct a centralised string, ready for display
        /// </summary>
        /// <param name="displayString">The string to pad</param>
        /// <param name="width">The desired overall width of the padded string</param>
        /// <returns>A padded string</returns>
        public virtual string ConstructCentralisedString(string displayString, int width)
        {
            return ConstructCentralisedString(displayString, width, LeftBoundaryCharacter, RightBoundaryCharacter);
        }


        /// <summary>
        /// Construct a wrapped padded string, ready for display
        /// </summary>
        /// <param name="displayString">The string to pad</param>
        /// <param name="width">The desired overall width of the padded string</param>
        /// <param name="centralise">Specify if the string should be centralised</param>
        /// <returns>A padded string</returns>
        public virtual string ConstructWrappedPaddedString(string displayString, int width, bool centralise)
        {
            // construct
            return ConstructWrappedPaddedString(displayString, width, LeftBoundaryCharacter, RightBoundaryCharacter, centralise);
        }

        /// <summary>
        /// Construct a wrapped padded string, ready for display
        /// </summary>
        /// <param name="displayString">The string to pad</param>
        /// <param name="width">The desired overall width of the padded string</param>
        /// <param name="leftBoundary">The left boundary character</param>
        /// <param name="rightBoundary">The right boundary character</param>
        /// <param name="centralise">set if the string should be centralised</param>
        /// <returns>A padded string</returns>
        public virtual string ConstructWrappedPaddedString(string displayString, int width, char leftBoundary, char rightBoundary, bool centralise)
        {
            // if width is too small
            if (width <= 0)
                // throw exception
                throw new ArgumentException("The width parameter must be greater than 0");

            // create a wrapped string
            var wrappedString = string.Empty;

            // hold freespace allocation
            var availableTextSpace = width - 4;

            // if wrapping
            if (displayString.Length > availableTextSpace)
            {
                // hold a chunk of the string
                var chunk = string.Empty;

                // hold word
                var word = string.Empty;

                // while something left
                while (displayString.Length > 0)
                {
                    // while stil room
                    while (chunk.Length <= availableTextSpace)
                    {
                        // if a lingering word
                        if (!string.IsNullOrEmpty(word))
                            // add word
                            chunk += word;

                        // get word
                        word = extractNextWordFromString(ref displayString) + " ";

                        // if the word fits onto the end of the chunk
                        if (chunk.Length + word.Length <= availableTextSpace)
                        {
                            // add word
                            chunk += word;

                            // clear word
                            word = string.Empty;
                        }
                        else
                        {
                            // break itteration
                            break;
                        }
                    }

                    // add wrapped line
                    wrappedString += leftBoundary + " " + chunk + ConstructWhitespaceString(availableTextSpace - chunk.Length) + rightBoundary + "\n";

                    // clear chunk
                    chunk = string.Empty;
                }

                // if a lingering word
                if (!string.IsNullOrEmpty(word) &&
                    word != " ")
                {
                    // if centralising
                    if (centralise)
                        // add centralised string
                        wrappedString += ConstructCentralisedString(word, width, leftBoundary, rightBoundary);
                    else
                        // add wrapped line with just word in it
                        wrappedString += leftBoundary + " " + word + ConstructWhitespaceString(availableTextSpace - word.Length) + rightBoundary + "\n";
                }
            }
            else
            {
                // if centralising
                if (centralise)
                    // add centralised string
                    wrappedString += ConstructCentralisedString(displayString, width, leftBoundary, rightBoundary);
                else
                    // construct string
                    wrappedString = leftBoundary + " " + displayString + ConstructWhitespaceString(availableTextSpace - displayString.Length) + rightBoundary + "\n";
            }

            // return the wrapped string
            return wrappedString;
        }

        /// <summary>
        /// Determine the amount of lines in a string
        /// </summary>
        /// <param name="input">The input to determine the amount of lines from</param>
        /// <returns>The amount of lines in the string</returns>
        public int DetermineLinesInString(string input)
        {
            // hold occurences
            var occurences = 0;

            // itterate input
            for (var index = 0; index < input.Length - 1; index++)
                // if newline found
                if (input.Substring(index, 1) == "\n")
                    // add occurence
                    occurences++;

            // if some data in input
            if (!string.IsNullOrEmpty(input))
                // if not a new line at the end
                if (input[input.Length - 1] != (char)13)
                    // add additional occurence
                    occurences++;

            // return
            return occurences;
        }

        #endregion
    }
}