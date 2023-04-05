using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventureFramework.Rendering
{
    /// <summary>
    /// A class for drawing strings
    /// </summary>
    public abstract class Drawer
    {
        #region Properties

        /// <summary>
        /// Get or set the character used for left boundaries
        /// </summary>
        public Char LeftBoundaryCharacter
        {
            get { return this.leftBoundaryCharacter; }
            set { this.leftBoundaryCharacter = value; }
        }

        /// <summary>
        /// Get or set the character used for left boundaries
        /// </summary>
        private Char leftBoundaryCharacter = Convert.ToChar("|");

        /// <summary>
        /// Get or set the character used for right boundaries
        /// </summary>
        public Char RightBoundaryCharacter
        {
            get { return this.rightBoundaryCharacter; }
            set { this.rightBoundaryCharacter = value; }
        }

        /// <summary>
        /// Get or set the character used for right boundaries
        /// </summary>
        private Char rightBoundaryCharacter = Convert.ToChar("|");

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the Drawer class
        /// </summary>
        protected Drawer()
        {
        }

        /// <summary>
        /// Construct a padded area
        /// </summary>
        /// <param name="width">The width of the padded area</param>
        /// <param name="height">The height of the padded area</param>
        /// <returns>A constructed padded area</returns>
        public virtual String ConstructPaddedArea(Int32 width, Int32 height)
        {
            return this.ConstructPaddedArea(this.LeftBoundaryCharacter, this.RightBoundaryCharacter, width, height);
        }

        /// <summary>
        /// Construct a padded area
        /// </summary>
        /// <param name="leftBoundary">The left boundary string</param>
        /// <param name="rightBoundary">The right boundary string</param>
        /// <param name="width">The width of the padded area</param>
        /// <param name="height">The height of the padded area</param>
        /// <returns>A constructed padded area</returns>
        public virtual String ConstructPaddedArea(Char leftBoundary, Char rightBoundary, Int32 width, Int32 height)
        {
            // hold padded area
            String paddedArea = String.Empty;

            // pad white space
            for (Int32 index = 0; index < height; index++)
            {
                // construct
                paddedArea += this.constructWhiteSpaceWithBoundaryDevider(width, leftBoundary, rightBoundary);
            }

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
        public virtual String ConstructDevider(Int32 width, Char leftBoundary, Char deviderString, Char rightBoundary)
        {
            // if width is too small
            if (width <= 0)
            {
                // throw exception
                throw new ArgumentException("The width parameter must be greater than 0");
            }

            // the left character
            String devider = leftBoundary.ToString();

            // itterate width
            for (Int32 index = 0; index < width - 3; index++)
            {
                // add deviding character
                devider += deviderString;
            }

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
        public virtual String ConstructWrappedPaddedString(String displayString, Int32 width)
        {
            // construct
            return this.ConstructWrappedPaddedString(displayString, width, this.LeftBoundaryCharacter, this.RightBoundaryCharacter, false);
        }

        /// <summary>
        /// Construct a deviding horizontal line
        /// </summary>
        /// <param name="width">The width of the devider</param>
        /// <param name="leftBoundary">The left boundary character</param>
        /// <param name="rightBoundary">The right boundary character</param>
        /// <returns>A constructed devider</returns>
        private String constructWhiteSpaceWithBoundaryDevider(Int32 width, Char leftBoundary, Char rightBoundary)
        {
            // get a devider
            String devider = leftBoundary.ToString();

            // itterate width
            for (Int32 index = 0; index < width - 3; index++)
            {
                // add whitespace
                devider += " ";
            }

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
        public virtual String ConstructWhitespaceString(Int32 width)
        {
            // get a devider
            String whiteSpace = String.Empty;

            // itterate width
            for (Int32 index = 0; index < width; index++)
            {
                // add deviding character
                whiteSpace += " ";
            }

            // return
            return whiteSpace;
        }

        /// <summary>
        /// Construct a unboardered, centralised string
        /// </summary>
        /// <param name="displayString">The string to pad</param>
        /// <param name="width">The desired overall width of the padded string</param>
        /// <returns>A padded string</returns>
        public String ConstructUnboarderedCentralisedString(String displayString, Int32 width)
        {
            // if width is too small
            if (width <= 0)
            {
                // throw exception
                throw new ArgumentException("The width parameter must be greater than 0");
            }

            // if too large
            if (displayString.Length > width)
            {                
                // throw exception
                throw new ArgumentException("The length of the displayString parameter is greater than the width paramater");
            }

            // hold constructed string
            String constructedString = String.Empty;

            // hold start position
            Int32 startPosition = (width / 2) - (displayString.Length / 2);

            // add white space
            constructedString += this.ConstructWhitespaceString(startPosition - 1);

            // add string
            constructedString += displayString;

            // add right buffer
            constructedString += this.ConstructWhitespaceString(width - constructedString.Length);

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
        public virtual String ConstructCentralisedString(String displayString, Int32 width, Char leftBoundary, Char rightBoundary)
        {
            // if width is too small
            if (width <= 0)
            {
                // throw exception
                throw new ArgumentException("The width parameter must be greater than 0");
            }

            // if short enough
            if (displayString.Length + 2 < width)
            {
                // hold constructed string
                String constructedString = leftBoundary.ToString();

                // hold start position
                Int32 startPosition = (width / 2) - (displayString.Length / 2);

                // add white space
                constructedString += this.ConstructWhitespaceString(startPosition - 1);

                // add string
                constructedString += displayString;

                // add right buffer
                constructedString += this.ConstructWhitespaceString(width - 1 - constructedString.Length - 1);

                // add right string
                constructedString += rightBoundary;

                // add end
                constructedString += "\n";

                // return
                return constructedString;
            }
            else
            {
                // wrap and centralise
                return this.ConstructWrappedPaddedString(displayString, width, leftBoundary, rightBoundary, true);
            }
        }

        /// <summary>
        /// Construct a centralised string, ready for display
        /// </summary>
        /// <param name="displayString">The string to pad</param>
        /// <param name="width">The desired overall width of the padded string</param>
        /// <returns>A padded string</returns>
        public virtual String ConstructCentralisedString(String displayString, Int32 width)
        {
            return this.ConstructCentralisedString(displayString, width, this.LeftBoundaryCharacter, this.RightBoundaryCharacter);
        }


        /// <summary>
        /// Construct a wrapped padded string, ready for display
        /// </summary>
        /// <param name="displayString">The string to pad</param>
        /// <param name="width">The desired overall width of the padded string</param>
        /// <param name="centralise">Specify if the string should be centralised</param>
        /// <returns>A padded string</returns>
        public virtual String ConstructWrappedPaddedString(String displayString, Int32 width, Boolean centralise)
        {
            // construct
            return this.ConstructWrappedPaddedString(displayString, width, this.LeftBoundaryCharacter, this.RightBoundaryCharacter, centralise);
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
        public virtual String ConstructWrappedPaddedString(String displayString, Int32 width, Char leftBoundary, Char rightBoundary, Boolean centralise)
        {
            // if width is too small
            if (width <= 0)
            {
                // throw exception
                throw new ArgumentException("The width parameter must be greater than 0");
            }

            // create a wrapped string
            String wrappedString = String.Empty;

            // hold freespace allocation
            Int32 availableTextSpace = width - 4;

            // if wrapping
            if (displayString.Length > availableTextSpace)
            {
                // hold a chunk of the string
                String chunk = String.Empty;

                // hold word
                String word = String.Empty;

                // while something left
                while (displayString.Length > 0)
                {
                    // while stil room
                    while (chunk.Length <= availableTextSpace)
                    {
                        // if a lingering word
                        if (!String.IsNullOrEmpty(word))
                        {
                            // add word
                            chunk += word;
                        }

                        // get word
                        word = Drawer.extractNextWordFromString(ref displayString) + " ";

                        // if the word fits onto the end of the chunk
                        if (chunk.Length + word.Length <= availableTextSpace)
                        {
                            // add word
                            chunk += word;

                            // clear word
                            word = String.Empty;
                        }
                        else
                        {
                            // break itteration
                            break;
                        }
                    }

                    // add wrapped line
                    wrappedString += leftBoundary + " " + chunk + this.ConstructWhitespaceString(availableTextSpace - chunk.Length) + rightBoundary + "\n";

                    // clear chunk
                    chunk = String.Empty;
                }

                // if a lingering word
                if ((!String.IsNullOrEmpty(word)) &&
                    (word != " "))
                {
                    // if centralising
                    if (centralise)
                    {
                        // add centralised string
                        wrappedString += this.ConstructCentralisedString(word, width, leftBoundary, rightBoundary);
                    }
                    else
                    {
                        // add wrapped line with just word in it
                        wrappedString += leftBoundary + " " + word + this.ConstructWhitespaceString(availableTextSpace - word.Length) + rightBoundary + "\n";
                    }
                }
            }
            else
            {
                // if centralising
                if (centralise)
                {
                    // add centralised string
                    wrappedString += this.ConstructCentralisedString(displayString, width, leftBoundary, rightBoundary);
                }
                else
                {
                    // construct string
                    wrappedString = leftBoundary + " " + displayString + this.ConstructWhitespaceString(availableTextSpace - displayString.Length) + rightBoundary + "\n";
                }
            }

            // return the wrapped string
            return wrappedString;
        }

        /// <summary>
        /// Determine the amount of lines in a string
        /// </summary>
        /// <param name="input">The input to determine the amount of lines from</param>
        /// <returns>The amount of lines in the string</returns>
        public Int32 DetermineLinesInString(String input)
        {
            // hold occurences
            Int32 occurences = 0;

            // itterate input
            for (Int32 index = 0; index < input.Length - 1; index++)
            {
                // if newline found
                if (input.Substring(index, 1) == "\n")
                {
                    // add occurence
                    occurences++;
                }
            }

            // if some data in input
            if (!String.IsNullOrEmpty(input))
            {
                // if not a new line at the end
                if (input[input.Length - 1] != (Char)13)
                {
                    // add additional occurence
                    occurences++;
                }
            }

            // return
            return occurences;
        }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Extract the next word from a string
        /// </summary>
        /// <param name="input">The input string</param>
        /// <returns>The extracted work</returns>
        private static String extractNextWordFromString(ref String input)
        {
            // create word
            String word = String.Empty;

            // get character for space
            Char space = Convert.ToChar(" ");

            // trim preceeding space
            input = input.TrimStart(space);

            // find space
            for (Int32 index = 0; index < input.Length; index++)
            {
                // if not a space
                if (input[index] != space)
                {
                    // add character
                    word += input[index];
                }
                else
                {
                    // break itteration
                    break;
                }
            }

            // remove the word from input
            input = input.Remove(0, word.Length);

            // trim whitespace
            word = word.Trim();

            // return
            return word;
        }

        #endregion
    }
}
