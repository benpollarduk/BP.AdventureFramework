using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;

namespace BP.AdventureFramework.Rendering
{
    /// <summary>
    /// Represents a store for in game graphics.
    /// </summary>
    public static class InGameGraphics
    {
        #region StaticProperties

        /// <summary>
        /// Get the user defined graphics.
        /// </summary>
        public static Dictionary<string, Bitmap> UserDefinedGraphics { get; private set; } = new Dictionary<string, Bitmap>();

        #endregion

        #region StaticMethods

        /// <summary>
        /// Buffer all graphics from the default folder into system memory.
        /// </summary>
        /// <returns>True is the buffer was successful.</returns>
        public static bool BufferGraphics()
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Graphics");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var bmpDict = new Dictionary<string, Bitmap>();

            try
            {
                var allBmpFiles = new List<string>(Directory.GetFiles(path, "*.bmp", SearchOption.AllDirectories));
                allBmpFiles.AddRange(Directory.GetFiles(path, "*.jpg", SearchOption.AllDirectories));
                allBmpFiles.AddRange(Directory.GetFiles(path, "*.jpeg", SearchOption.AllDirectories));
                allBmpFiles.AddRange(Directory.GetFiles(path, "*.gif", SearchOption.AllDirectories));
                allBmpFiles.AddRange(Directory.GetFiles(path, "*.png", SearchOption.AllDirectories));
                allBmpFiles.AddRange(Directory.GetFiles(path, "*.tif", SearchOption.AllDirectories));

                foreach (var bmpPath in allBmpFiles)
                    bmpDict.Add(bmpPath.Substring(bmpPath.LastIndexOf("\\", StringComparison.Ordinal) + 1).Replace(bmpPath.Substring(bmpPath.LastIndexOf(".", StringComparison.Ordinal)), ""), Image.FromFile(bmpPath) as Bitmap);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception caught buffering graphics: {0}", e.Message);
                return false;
            }
            finally
            {
                UserDefinedGraphics = bmpDict;
            }

            return true;
        }

        #endregion
    }
}