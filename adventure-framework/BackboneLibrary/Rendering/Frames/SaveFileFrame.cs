using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using AdventureFramework.IO;
using AdventureFramework.Interaction;
using AdventureFramework.Structure;

namespace AdventureFramework.Rendering.Frames
{
    /// <summary>
    /// Represents a frame for saving files
    /// </summary>
    public class SaveFileFrame : FileIOFrame
    {
        #region Methods

        /// <summary>
        /// Initializes a new insatnce of the SaveFileFrame class
        /// </summary>
        protected SaveFileFrame()
        {
            // get default info
            this.DefaultDirectory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);

            // set the extension
            this.Extension = "xml";
        }

        /// <summary>
        /// Initializes a new insatnce of the SaveFileFrame class
        /// </summary>
        /// <param name="directory">Specify the default directory</param>
        /// <param name="extension">The file extension (excluding the '.')</param>
        public SaveFileFrame(String directory, String extension)
        {
            // set directory
            this.DefaultDirectory = new DirectoryInfo(directory);

            // set extension
            this.Extension = extension;
        }

        /// <summary>
        /// Initializes a new insatnce of the SaveFileFrame class
        /// </summary>
        /// <param name="directory">Specify the default directory</param>
        /// <param name="extension">The file extension (excluding the '.')</param>
        public SaveFileFrame(DirectoryInfo directory, String extension)
        {
            // set directory
            this.DefaultDirectory = directory;

            // set extension
            this.Extension = extension;
        }

        /// <summary>
        /// Build this SaveFileFrame into a text based display
        /// </summary>
        /// <param name="width">Specify the width of the Frame</param>
        /// <param name="height">Specify the height of the Frame</param>
        /// <param name="drawer">The FrameDrawer to draw the Frame with</param>
        /// <returns>A string representing the Frame</returns>
        public override string BuildFrame(int width, int height, FrameDrawer drawer)
        {
            // create builder
            StringBuilder builder = new StringBuilder();

            // get all files
            List<FileInfo> files = new List<FileInfo>(this.DefaultDirectory.GetFiles(String.Format("*.{0}", this.Extension), SearchOption.TopDirectoryOnly));

            // sort lines by used by
            files.Sort(new Comparison<FileInfo>((FileInfo a, FileInfo b) =>
                {
                    // check times
                    if (a.LastAccessTime < b.LastAccessTime)
                    {
                        // older
                        return 1;
                    }
                    else if (a.LastAccessTime > b.LastAccessTime)
                    {
                        // newer
                        return -1;
                    }
                    else
                    {
                        // the same
                        return 0;
                    }
                }));

            // clear all files
            this.DeterminedFiles.Clear();

            // populate dictionary
            for (Int32 index = 0; index < files.Count; index++)
            {
                // add file
                this.DeterminedFiles.Add(index + 1, files[index]);
            }

            // create devider
            String devider = drawer.ConstructDevider(width);

            // hold blank area
            String blankArea = drawer.ConstructPaddedArea(width, 1);

            // hold desired lines in bottom area
            Int32 desiredLinesInBottomArea = 4;

            // add devider
            builder.Append(devider);

            // add title
            builder.Append(drawer.ConstructCentralisedString("Save", width));

            // add devider
            builder.Append(devider);
           
            // hold lines so far
            Int32 linesSoFar;

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
                Int32 startIndex = this.IndexOfFirstDisplayedFile;

                // itterate all save files
                for (Int32 index = startIndex; index < files.Count; index++)
                {
                    // if enough room
                    if (linesSoFar + 5 + (index - startIndex) < height)
                    {
                        // get name
                        String name = files[index].Name.Substring(0, files[index].Name.LastIndexOf("."));

                        // if name is excessive
                        if (name.Length > 20)
                        {
                            // remove all next characters
                            name = name.Remove(20);
                        }

                        // add file
                        builder.Append(drawer.ConstructWrappedPaddedString(String.Format("{0}. {1} {2} {3} {4}", index + 1, name, drawer.ConstructWhitespaceString(25 - name.Length + (2 - (index + 1).ToString().Length)), files[index].LastWriteTime.ToShortDateString(), files[index].LastWriteTime.ToShortTimeString()), width));

                        // update last file name
                        this.IndexOfLastDisplayedFile = index;
                    }
                    else
                    {
                        // write last bit
                        builder.Append(drawer.ConstructWrappedPaddedString(String.Format("1 - {0}...", files.Count), width));

                        // break all itteration
                        break;
                    }
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
                builder.Append(drawer.ConstructWrappedPaddedString("Enter file index/new file name to save:", width));

                // set left
                this.CursorLeft = 42;
            }
            else
            {
                // set text input line
                builder.Append(drawer.ConstructWrappedPaddedString("Enter a new file name to save:", width));

                // set left
                this.CursorLeft = 33;
            }

            // set cursor position
            this.CursorTop = height - 3;

            // add devider without newline
            builder.Append(devider.Replace("\n", ""));

            // return concatonated string
            return builder.ToString();
        }

        #endregion
    }
}
