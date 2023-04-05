using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Diagnostics;

namespace AdventureFramework.Rendering
{
    /// <summary>
    /// Represents a store for ingame graphics
    /// </summary>
    public static class InGameGraphics
    {
        #region StaticProperties

        /// <summary>
        /// Get the user defined graphics
        /// </summary>
        public static Dictionary<String, Bitmap> UserDefinedGraphics
        {
            get { return InGameGraphics.userDefinedGraphics; }
            private set { InGameGraphics.userDefinedGraphics = new Dictionary<String, Bitmap>(); }
        }
        
        /// <summary>
        /// Get or set the user defined graphics
        /// </summary>
        private static Dictionary<String, Bitmap> userDefinedGraphics = new Dictionary<String,Bitmap>();

        #endregion

        #region StaticMethods

        /// <summary>
        /// Buffer all graphics from the default folder into system memory
        /// </summary>
        /// <returns>True is the buffer was sucsessful</returns>
        public static Boolean BufferGraphics()
        {
            // create path
            String path = AppDomain.CurrentDomain.BaseDirectory + "Graphics\\";

            // check directory
            if (!Directory.Exists(path))
            {
                // create so that it is there
                Directory.CreateDirectory(path);
            }

            // create dictionary for all files
            Dictionary<String, Bitmap> bmpDict = new Dictionary<String, Bitmap>();

            try
            {
                // get all files
                List<String> allBmpFiles = new List<String>(Directory.GetFiles(path, "*.bmp", SearchOption.AllDirectories));
                allBmpFiles.AddRange(Directory.GetFiles(path, "*.jpg", SearchOption.AllDirectories));
                allBmpFiles.AddRange(Directory.GetFiles(path, "*.jpeg", SearchOption.AllDirectories));
                allBmpFiles.AddRange(Directory.GetFiles(path, "*.gif", SearchOption.AllDirectories));
                allBmpFiles.AddRange(Directory.GetFiles(path, "*.png", SearchOption.AllDirectories));
                allBmpFiles.AddRange(Directory.GetFiles(path, "*.tif", SearchOption.AllDirectories));

                // now check files
                foreach (String bmpPath in allBmpFiles)
                {
                    // add new element, using its name as the key
                    bmpDict.Add(bmpPath.Substring(bmpPath.LastIndexOf("\\") + 1).Replace(bmpPath.Substring(bmpPath.LastIndexOf(".")), ""), Bitmap.FromFile(bmpPath) as Bitmap);
                }
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
                InGameGraphics.userDefinedGraphics = bmpDict;
            }

            // pass
            return true;
        }

        #endregion
    }
}
