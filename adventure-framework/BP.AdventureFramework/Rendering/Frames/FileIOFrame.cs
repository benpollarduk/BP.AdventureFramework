using System.Collections.Generic;
using System.IO;
using BP.AdventureFramework.Interaction;

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
            get { return defaultDirectory; }
            set { defaultDirectory = value; }
        }

        /// <summary>
        /// Get or set default directroy
        /// </summary>
        private DirectoryInfo defaultDirectory;

        /// <summary>
        /// Get or set file extension to use
        /// </summary>
        public string Extension
        {
            get { return extension; }
            set { extension = value; }
        }

        /// <summary>
        /// Get or set the file extension to use
        /// </summary>
        private string extension;

        /// <summary>
        /// Get or set the determined files from file generation
        /// </summary>
        protected Dictionary<int, FileInfo> DeterminedFiles
        {
            get { return determinedFiles; }
            set { determinedFiles = value; }
        }

        /// <summary>
        /// Get or set the determined files from file generation
        /// </summary>
        private Dictionary<int, FileInfo> determinedFiles = new Dictionary<int, FileInfo>();

        /// <summary>
        /// Get or set the index if the first displayed file
        /// </summary>
        protected int IndexOfFirstDisplayedFile
        {
            get { return indexOfFirstDisplayedFile; }
            set { indexOfFirstDisplayedFile = value; }
        }

        /// <summary>
        /// Get or set the index if the first displayed file
        /// </summary>
        private int indexOfFirstDisplayedFile;

        /// <summary>
        /// Get or set the index if the last displayed file
        /// </summary>
        protected int IndexOfLastDisplayedFile
        {
            get { return indexOfLastDisplayedFile; }
            set { indexOfLastDisplayedFile = value; }
        }

        /// <summary>
        /// Get or set the index if the last displayed file
        /// </summary>
        private int indexOfLastDisplayedFile;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the FileIOFrame class
        /// </summary>
        protected FileIOFrame()
        {
            // accepts input
            AcceptsInput = true;
        }

        /// <summary>
        /// Try and determine a valid file name
        /// </summary>
        /// <param name="fileIndex">The index of the file</param>
        /// <param name="fileName">The constructed file name</param>
        /// <returns>True if a file name could be determined</returns>
        public virtual Decision TryDetermineValidFileName(int fileIndex, out string fileName)
        {
            // if contains file index
            if (DeterminedFiles.ContainsKey(fileIndex))
            {
                // maybe in next/previous session
                if (fileIndex < IndexOfFirstDisplayedFile + 1 ||
                    fileIndex > IndexOfLastDisplayedFile + 1)
                {
                    // if propper index
                    if (fileIndex > 0)
                    {
                        // if in range
                        if (fileIndex < determinedFiles.Count)
                            // set index
                            IndexOfFirstDisplayedFile = fileIndex - 1;
                        else
                            // set index
                            IndexOfFirstDisplayedFile = determinedFiles.Count - 1;
                    }
                    else
                    {
                        // set index
                        IndexOfFirstDisplayedFile = 0;
                    }

                    // invalidate
                    Invalidate();

                    // set no file name
                    fileName = string.Empty;

                    // pass
                    return new Decision(EReactionToInput.SelfContainedReaction);
                }

                // set file name
                fileName = determinedFiles[fileIndex].FullName;

                // pass
                return new Decision(EReactionToInput.CouldReact);
            }

            // set empty
            fileName = string.Empty;

            // fail
            return new Decision(EReactionToInput.CouldntReact, "File index was invalid");
        }

        /// <summary>
        /// Try and determine a valid file name
        /// </summary>
        /// <param name="newFileName">The new file name</param>
        /// <param name="fileName">The constructed file name</param>
        /// <returns>True if a file name could be determined</returns>
        public virtual Decision TryDetermineValidFileName(string newFileName, out string fileName)
        {
            // itterate illegal characters
            foreach (var s in GameSave.ILLEGAL_FILE_HANDLING_CHARACTERS)
                // if contains illegal character
                if (newFileName.Contains(s))
                {
                    // set file name
                    fileName = string.Empty;

                    // fail
                    return new Decision(EReactionToInput.CouldntReact, "File name cannot contain the character " + s);
                }

            // file too short
            if (newFileName.Length > 20)
            {
                // set file name
                fileName = string.Empty;

                // fail
                return new Decision(EReactionToInput.CouldntReact, "File names cannot exceed 20 chacarters");
            }

            if (newFileName.Length == 0)
            {
                // set file name
                fileName = string.Empty;

                // fail
                return new Decision(EReactionToInput.CouldntReact, "File names must have atleast one character");
            }

            if (newFileName.Substring(0, 1) == " ")
            {
                // set file name
                fileName = string.Empty;

                // fail
                return new Decision(EReactionToInput.CouldntReact, "File names cannot start with a space");
            }

            // set new name
            fileName = DefaultDirectory.FullName + newFileName + "." + Extension;

            // pass
            return new Decision(EReactionToInput.CouldReact);
        }

        #endregion
    }
}