using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using AdventureFramework.IO;
using AdventureFramework.Interaction;

namespace AdventureFramework.Rendering.Frames
{
    /// <summary>
    /// Represents a Frame for File IO
    /// </summary>
    public abstract class FileIOFrame : Frame
    {
        #region Properties

        /// <summary>
        /// Get or set default directroy
        /// </summary>
        public DirectoryInfo DefaultDirectory
        {
            get { return this.defaultDirectory; }
            set { this.defaultDirectory = value; }
        }

        /// <summary>
        /// Get or set default directroy
        /// </summary>
        private DirectoryInfo defaultDirectory;

        /// <summary>
        /// Get or set file extension to use
        /// </summary>
        public String Extension
        {
            get { return this.extension; }
            set { this.extension = value; }
        }

        /// <summary>
        /// Get or set the file extension to use
        /// </summary>
        private String extension;

        /// <summary>
        /// Get or set the determined files from file generation
        /// </summary>
        protected Dictionary<Int32, FileInfo> DeterminedFiles
        {
            get { return this.determinedFiles; }
            set { this.determinedFiles = value; }
        }

        /// <summary>
        /// Get or set the determined files from file generation
        /// </summary>
        private Dictionary<Int32, FileInfo> determinedFiles = new Dictionary<Int32, FileInfo>();

        /// <summary>
        /// Get or set the index if the first displayed file
        /// </summary>
        protected Int32 IndexOfFirstDisplayedFile
        {
            get { return this.indexOfFirstDisplayedFile; }
            set { this.indexOfFirstDisplayedFile = value; }
        }

        /// <summary>
        /// Get or set the index if the first displayed file
        /// </summary>
        private Int32 indexOfFirstDisplayedFile = 0;

        /// <summary>
        /// Get or set the index if the last displayed file
        /// </summary>
        protected Int32 IndexOfLastDisplayedFile
        {
            get { return this.indexOfLastDisplayedFile; }
            set { this.indexOfLastDisplayedFile = value; }
        }

        /// <summary>
        /// Get or set the index if the last displayed file
        /// </summary>
        private Int32 indexOfLastDisplayedFile = 0;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the FileIOFrame class
        /// </summary>
        protected FileIOFrame()
        {
            // accepts input
            this.AcceptsInput = true;
        }

        /// <summary>
        /// Try and determine a valid file name
        /// </summary>
        /// <param name="fileIndex">The index of the file</param>
        /// <param name="fileName">The constructed file name</param>
        /// <returns>True if a file name could be determined</returns>
        public virtual Decision TryDetermineValidFileName(Int32 fileIndex, out String fileName)
        {
            // if contains file index
            if (this.DeterminedFiles.ContainsKey(fileIndex))
            {
                // maybe in next/previous session
                if ((fileIndex < this.IndexOfFirstDisplayedFile + 1) ||
                    (fileIndex > this.IndexOfLastDisplayedFile + 1))
                {
                    // if propper index
                    if (fileIndex > 0)
                    {
                        // if in range
                        if (fileIndex < this.determinedFiles.Count)
                        {
                            // set index
                            this.IndexOfFirstDisplayedFile = fileIndex - 1;
                        }
                        else
                        {
                            // set index
                            this.IndexOfFirstDisplayedFile = this.determinedFiles.Count - 1;
                        }
                    }
                    else
                    {
                        // set index
                        this.IndexOfFirstDisplayedFile = 0;
                    }

                    // invalidate
                    this.Invalidate();

                    // set no file name
                    fileName = String.Empty;

                    // pass
                    return new Decision(EReactionToInput.SelfContainedReaction);
                }
                else
                {
                    // set file name
                    fileName = this.determinedFiles[fileIndex].FullName;

                    // pass
                    return new Decision(EReactionToInput.CouldReact);
                }
            }
            else
            {
                // set empty
                fileName = String.Empty;

                // fail
                return new Decision(EReactionToInput.CouldntReact, "File index was invalid");
            }
        }

        /// <summary>
        /// Try and determine a valid file name
        /// </summary>
        /// <param name="newFileName">The new file name</param>
        /// <param name="fileName">The constructed file name</param>
        /// <returns>True if a file name could be determined</returns>
        public virtual Decision TryDetermineValidFileName(String newFileName, out String fileName)
        {
            // itterate illegal characters
            foreach (String s in GameSave.ILLEGAL_FILE_HANDLING_CHARACTERS)
            {
                // if contains illegal character
                if (newFileName.Contains(s))
                {
                    // set file name
                    fileName = String.Empty;

                    // fail
                    return new Decision(EReactionToInput.CouldntReact, "File name cannot contain the character " + s);
                }
            }

            // file too short
            if (newFileName.Length > 20)
            {
                // set file name
                fileName = String.Empty;

                // fail
                return new Decision(EReactionToInput.CouldntReact, "File names cannot exceed 20 chacarters");
            }
            else if (newFileName.Length == 0)
            {
                // set file name
                fileName = String.Empty;

                // fail
                return new Decision(EReactionToInput.CouldntReact, "File names must have atleast one character");
            }
            else if (newFileName.Substring(0, 1) == " ")
            {
                // set file name
                fileName = String.Empty;

                // fail
                return new Decision(EReactionToInput.CouldntReact, "File names cannot start with a space");
            }
            else
            {
                // set new name
                fileName = this.DefaultDirectory.FullName + newFileName + "." + this.Extension;

                // pass
                return new Decision(EReactionToInput.CouldReact);
            }
        }

        #endregion
    }
}
