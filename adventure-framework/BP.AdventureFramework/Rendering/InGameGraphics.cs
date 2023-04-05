using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;

namespace AdventureFramework.Rendering
{
    /// <summary>
    /// Represents a store for ingame graphics
    /// </summary>
    public static class InGameGraphics
    {
        #region StaticMethods

        /// <summary>
        /// Buffer all graphics from the default folder into system memory
        /// </summary>
        /// <returns>True is the buffer was sucsessful</returns>
        public static bool BufferGraphics()
        {
            // create path
            var path = AppDomain.CurrentDomain.BaseDirectory + "Graphics\\";

            // check directory
            if (!Directory.Exists(path))
                // create so that it is there
                Directory.CreateDirectory(path);

            // create dictionary for all files
            var bmpDict = new Dictionary<string, Bitmap>();

            try
            {
                // get all files
                var allBmpFiles = new List<string>(Directory.GetFiles(path, "*.bmp", SearchOption.AllDirectories));
                allBmpFiles.AddRange(Directory.GetFiles(path, "*.jpg", SearchOption.AllDirectories));
                allBmpFiles.AddRange(Directory.GetFiles(path, "*.jpeg", SearchOption.AllDirectories));
                allBmpFiles.AddRange(Directory.GetFiles(path, "*.gif", SearchOption.AllDirectories));
                allBmpFiles.AddRange(Directory.GetFiles(path, "*.png", SearchOption.AllDirectories));
                allBmpFiles.AddRange(Directory.GetFiles(path, "*.tif", SearchOption.AllDirectories));

                // now check files
                foreach (var bmpPath in allBmpFiles)
                    // add new element, using its name as the key
                    bmpDict.Add(bmpPath.Substring(bmpPath.LastIndexOf("\\") + 1).Replace(bmpPath.Substring(bmpPath.LastIndexOf(".")), ""), Image.FromFile(bmpPath) as Bitmap);
            }
            catch (Exception e)
            {
                // display in debug
                Debug.WriteLine("Exception caught buffering graphics: {0}", e.Message);

                // fail
                return false;
            }
            finally
            {
                // set to whatever was buffered
                userDefinedGraphics = bmpDict;
            }

            // pass
            return true;
        }

        #endregion

        #region StaticProperties

        /// <summary>
        /// Get the user defined graphics
        /// </summary>
        public static Dictionary<string, Bitmap> UserDefinedGraphics
        {
            get { return userDefinedGraphics; }
            private set { userDefinedGraphics = new Dictionary<string, Bitmap>(); }
        }

        /// <summary>
        /// Get or set the user defined graphics
        /// </summary>
        private static Dictionary<string, Bitmap> userDefinedGraphics = new Dictionary<string, Bitmap>();

        #endregion
    }
}