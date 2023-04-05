using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventureFramework.Rendering.Frames
{
    /// <summary>
    /// Represents a frame for loading files
    /// </summary>
    public class LoadFileFrame : FileIOFrame
    {
        #region Methods

        /// <summary>
        /// Initializes a new insatnce of the LoadFileFrame class
        /// </summary>
        protected LoadFileFrame()
        {
            // get default info
            DefaultDirectory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);

            // set the extension
            Extension = "xml";
        }

        /// <summary>
        /// Initializes a new insatnce of the LoadFileFrame class
        /// </summary>
        /// <param name="directory">Specify the default directory</param>
        /// <param name="extension">The file extension (excluding the '.')</param>
        public LoadFileFrame(string directory, string extension)
        {
            // set directory
            DefaultDirectory = new DirectoryInfo(directory);

            // set extension
            Extension = extension;
        }

        /// <summary>
        /// Initializes a new insatnce of the LoadFileFrame class
        /// </summary>
        /// <param name="directory">Specify the default directory</param>
        /// <param name="extension">The file extension (excluding the '.')</param>
        public LoadFileFrame(DirectoryInfo directory, string extension)
        {
            // set directory
            DefaultDirectory = directory;

            // set extension
            Extension = extension;
        }

        /// <summary>
        /// Build this LoadFileFrame into a text based display
        /// </summary>
        /// <param name="width">Specify the width of the Frame</param>
        /// <param name="height">Specify the height of the Frame</param>
        /// <param name="drawer">The FrameDrawer to draw the Frame with</param>
        /// <returns>A string representing the Frame</returns>
        public override string BuildFrame(int width, int height, FrameDrawer drawer)
        {
            // create builder
            var builder = new StringBuilder();

            // get all files
            var files = new List<FileInfo>(DefaultDirectory.GetFiles(string.Format("*.{0}", Extension), SearchOption.TopDirectoryOnly));

            // sort lines by used by
            files.Sort((a, b) =>
            {
                // check times
                if (a.LastAccessTime < b.LastAccessTime)
                    // older
                    return 1;
                if (a.LastAccessTime > b.LastAccessTime)
                    // newer
                    return -1;
                return 0;
            });

            // clear all files
            DeterminedFiles.Clear();

            // populate dictionary
            for (var index = 0; index < files.Count; index++)
                // add file
                DeterminedFiles.Add(index + 1, files[index]);

            // create devider
            var devider = drawer.ConstructDevider(width);

            // hold blank area
            var blankArea = drawer.ConstructPaddedArea(width, 1);

            // hold desired lines in bottom area
            var desiredLinesInBottomArea = 4;

            // add devider
            builder.Append(devider);

            // add title
            builder.Append(drawer.ConstructCentralisedString("Load", width));

            // add devider
            builder.Append(devider);

            // hold lines so far
            int linesSoFar;

            // if some files
            if (files.Count > 0)
            {
                // add command
                builder.Append(drawer.ConstructWrappedPaddedString("Existing files", width, true));

                // add whitespace
                builder.Append(blankArea);

                // determine lines so far
                linesSoFar = drawer.DetermineLinesInString(builder.ToString());

                // hold index of start
                var startIndex = IndexOfFirstDisplayedFile;

                // itterate all save files
                for (var index = startIndex; index < files.Count; index++)
                    // if enough room
                    if (linesSoFar + 5 + (index - startIndex) < height)
                    {
                        // get name
                        var name = files[index].Name.Substring(0, files[index].Name.LastIndexOf("."));

                        // if name is excessive
                        if (name.Length > 20)
                            // remove all next characters
                            name = name.Remove(20);

                        // add file
                        builder.Append(drawer.ConstructWrappedPaddedString(string.Format("{0}. {1} {2} {3} {4}", index + 1, name, drawer.ConstructWhitespaceString(25 - name.Length + (2 - (index + 1).ToString().Length)), files[index].LastWriteTime.ToShortDateString(), files[index].LastWriteTime.ToShortTimeString()), width));

                        // update last file name
                        IndexOfLastDisplayedFile = index;
                    }
                    else
                    {
                        // write last bit
                        builder.Append(drawer.ConstructWrappedPaddedString(string.Format("1 - {0}...", files.Count), width));

                        // break all itteration
                        break;
                    }
            }
            else
            {
                // display blank
                builder.Append(drawer.ConstructPaddedArea(width, (height - desiredLinesInBottomArea - drawer.DetermineLinesInString(builder.ToString())) / 2));

                // display no files
                builder.Append(drawer.ConstructWrappedPaddedString("There are no save files", width, true));
            }

            // hold lines so far
            linesSoFar = drawer.DetermineLinesInString(builder.ToString());

            // append
            builder.Append(drawer.ConstructPaddedArea(width, height - linesSoFar - desiredLinesInBottomArea));

            // add devider
            builder.Append(devider);

            // if some files
            if (files.Count > 0)
            {
                // set text input line
                builder.Append(drawer.ConstructWrappedPaddedString("Enter file index/file name to load:", width));

                // set left
                CursorLeft = 38;
            }
            else
            {
                // set text input line
                builder.Append(drawer.ConstructWrappedPaddedString("", width));

                // don except input
                AcceptsInput = false;

                // hide cursor
                ShowCursor = false;

                // set left
                CursorLeft = 3;
            }

            // set cursor position
            CursorTop = height - 3;

            // add devider without newline
            builder.Append(devider.Replace("\n", ""));

            // return concatonated string
            return builder.ToString();
        }

        #endregion
    }
}