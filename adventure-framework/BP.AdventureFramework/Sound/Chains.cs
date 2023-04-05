using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace AdventureFramework.Sound
{
    /// <summary>
    /// A library of predefined FX Chains
    /// </summary>
    public static class Chains
    {
        #region StaticMethods

        /// <summary>
        /// Buffer all user defined chains from the default folder into system memory
        /// </summary>
        /// <returns>True is the buffer was sucsessful</returns>
        public static bool BufferUserDefinedChains()
        {
            // create path
            var path = AppDomain.CurrentDomain.BaseDirectory + "Chains\\";

            // check directory
            if (!Directory.Exists(path))
                // create so that it is there
                Directory.CreateDirectory(path);

            // create dictionary for all files
            var chainDict = new Dictionary<string, Chain>();

            try
            {
                // create deserializer
                var deserializer = new BinaryFormatter();

                // now check files
                foreach (var binPath in Directory.GetFiles(path, "*.bin", SearchOption.AllDirectories))
                    // create reader
                    using (var reader = new StreamReader(binPath))
                    {
                        // add new element, using its name as the key
                        chainDict.Add(binPath.Substring(binPath.LastIndexOf("\\") + 1).Replace(".bin", ""), (Chain)deserializer.Deserialize(reader.BaseStream));
                    }
            }
            catch (Exception e)
            {
                // display in debug
                Debug.WriteLine("Exception caught buffering chains: {0}", e.Message);

                // fail
                return false;
            }
            finally
            {
                // set to whatever was buffered
                userDefinedChains = chainDict;
            }

            // pass
            return true;
        }

        #endregion

        #region StaticProperties

        /// <summary>
        /// Get an Attention FX
        /// </summary>
        public static Chain Attention = new Chain(new Beep(260, 100));

        /// <summary>
        /// Get the user defined chains
        /// </summary>
        public static Dictionary<string, Chain> UserDefinedChains
        {
            get { return userDefinedChains; }
            private set { userDefinedChains = new Dictionary<string, Chain>(); }
        }

        /// <summary>
        /// Get or set the user defined chains
        /// </summary>
        private static Dictionary<string, Chain> userDefinedChains = new Dictionary<string, Chain>();

        #endregion
    }
}