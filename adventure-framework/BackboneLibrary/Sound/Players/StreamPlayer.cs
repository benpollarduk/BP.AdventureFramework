using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Reflection;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace AdventureFramework.Sound.Players
{
    /// <summary>
    /// Play a sound file as a stream
    /// </summary>
    public abstract class StreamPlayer : SoundPlayer
    {
        #region DLLImports

        [DllImport("WinMM.dll")]
        private static extern bool PlaySound(string fname, int Mod, int flag);

        [DllImport("Winmm.dll")]
        private static extern bool PlaySound(byte[] data, IntPtr hMod, UInt32 dwFlags);

        #endregion

        #region StaticMethods

        /// <summary>
        /// Play a sound file. The resource will be played asychronously
        /// </summary>
        /// <param name="fileName">The full file name of the file to play</param>
        public static void PlayWav(String fileName)
        {
            // play stream
            StreamPlayer.PlayWav(fileName, (UInt32)(ESoundFlags.SND_FILENAME | ESoundFlags.SND_ASYNC));
        }

        /// <summary>
        /// Play a sound file
        /// </summary>
        /// <param name="fileName">The full file name of the file to play</param>
        /// <param name="flags">The flags to use when playing the sound</param>
        public static void PlayWav(String fileName, UInt32 flags)
        {
            // play stream
            StreamPlayer.PlayWav(fileName, flags, ESyncModes.Sync);
        }
 
        /// <summary>
        /// Play a sound file
        /// </summary>
        /// <param name="fileName">The full file name of the file to play</param>
        /// <param name="flags">The flags to use when playing the sound</param>
        /// <param name="preSendMode">The sync mode to use prior to sending the sound</param>
        public static void PlayWav(String fileName, UInt32 flags, ESyncModes preSendMode)
        {
            try
            {
                // if file exists
                if (File.Exists(fileName))
                {
                    // hold sound stream
                    MemoryStream soundStream = null;

                    // open file
                    using (FileStream fStream = new FileStream(fileName, FileMode.Open))
                    {
                        // create array for all bytes of wav
                        Byte[] wavBytes = new Byte[fStream.Length];

                        // read bytes
                        fStream.Read(wavBytes, 0, (Int32)fStream.Length);

                        // load bytes from buffer
                        soundStream = new MemoryStream(wavBytes);
                    }

                    // play stream
                    StreamPlayer.PlayStream(soundStream, flags, preSendMode);
                }
                else
                {
                    // throw exception
                    throw new FileNotFoundException("File " + fileName + " could not be found");
                }
            }
            catch (Exception e)
            {
                // display in debug
                Debug.WriteLine("Exception caught playing wav: {0}", e.Message);
            }
        }

        /// <summary>
        /// Play a sound file from a resource. The resource will be played asychronously
        /// </summary>
        /// <param name="assembly">The assembly the sound file belongs to</param>
        /// <param name="fileName">The file name of the stream, including it's extension. Any directories that the resource lies in should be specified with '.' instead of '/' e.g. .DirectoryName.File.wav</param>
        public static void PlayResourceStream(Assembly assembly, String fileName)
        {
            // play
            StreamPlayer.PlayResourceStream(assembly, fileName, (UInt32)(ESoundFlags.SND_ASYNC | ESoundFlags.SND_MEMORY));
        }

        /// <summary>
        /// Play a sound file from a resource
        /// </summary>
        /// <param name="assembly">The assembly the sound file belongs to</param>
        /// <param name="fileName">The file name of the stream, including it's extension. Any directories that the resource lies in should be specified with '.' instead of '/' e.g. .DirectoryName.File.wav</param>
        /// <param name="flags">The flags to use when playing the sound</param>
        public static void PlayResourceStream(Assembly assembly, String fileName, UInt32 flags)
        {
            // play
            StreamPlayer.PlayResourceStream(assembly, fileName, flags, ESyncModes.Sync);
        }

        /// <summary>
        /// Play a sound file from a resource
        /// </summary>
        /// <param name="assembly">The assembly the sound file belongs to</param>
        /// <param name="fileName">The file name of the stream, including it's extension. Any directories that the resource lies in should be specified with '.' instead of '/' e.g. .DirectoryName.File.wav</param>
        /// <param name="flags">The flags to use when playing the sound</param>
        /// <param name="preSendMode">The sync mode to use prior to sending the sound</param>
        public static void PlayResourceStream(Assembly assembly, String fileName, UInt32 flags, ESyncModes preSendMode)
        {
            try
            {
                // all embedded sounds must be set to "embeded resource" under their property

                // get the resource name
                String uriName = assembly.GetName().Name.ToString() + ".Sounds." + fileName;

                // get stream from resources
                Stream soundStream = assembly.GetManifestResourceStream(uriName);

                // play
                StreamPlayer.PlayStream(soundStream, flags, preSendMode);
            }
            catch (Exception e)
            {
                // display in debug
                Debug.WriteLine("Exception caught playing resource stream: {0}", e.Message);
            }
        }

        /// <summary>
        /// Play a stream. The stream will be played asychronously
        /// </summary>
        /// <param name="stream">The stream to play</param>
        public static void PlayStream(Stream stream)
        {
            // play a stream
            StreamPlayer.PlayStream(stream, (UInt32)(ESoundFlags.SND_ASYNC | ESoundFlags.SND_MEMORY));
        }

        /// <summary>
        /// Play a stream
        /// </summary>
        /// <param name="stream">The stream to play</param>
        /// <param name="flags">The flags to use when playing the sound</param>
        public static void PlayStream(Stream stream, UInt32 flags)
        {
            try
            {
                // only play if using sounds
                if (SoundPlayer.UseSounds)
                {
                    // create byte array to stream length
                    Byte[] streamAsByteArray = new Byte[stream.Length];

                    // set array from stream
                    stream.Read(streamAsByteArray, 0, (Int32)streamAsByteArray.Length);

                    // play byte array stream using logical inclusive or to specify flags
                    StreamPlayer.PlaySound(streamAsByteArray, IntPtr.Zero, flags);
                }
            }
            catch (Exception e)
            {
                // display in debug
                Debug.WriteLine("Exception caught playing stream: {0}", e.Message);
            }
        }

        /// <summary>
        /// Play a stream
        /// </summary>
        /// <param name="stream">The stream to play</param>
        /// <param name="flags">The flags to use when playing the sound</param>
        /// <param name="preSendMode">The sync mode to use prior to sending the sound</param>
        public static void PlayStream(Stream stream, UInt32 flags, ESyncModes preSendMode)
        {
            // switch mode
            switch (preSendMode)
            {
                case (ESyncModes.Sync):
                    {
                        // play sound in sync from memory
                        StreamPlayer.PlayStream(stream, (UInt32)(ESoundFlags.SND_MEMORY | ESoundFlags.SND_SYNC));

                        break;
                    }
                case (ESyncModes.Async):
                    {
                        // play the resource on a thread pool thread - this is so that it is ASYNC without scratching
                        ThreadPool.QueueUserWorkItem(delegate(object state) { StreamPlayer.PlayStream(stream, (UInt32)(ESoundFlags.SND_MEMORY | ESoundFlags.SND_SYNC)); });
                       
                        break;
                    }
                default: { throw new NotImplementedException(); }
            }
        }

        #endregion
    }

    /// <summary>
    /// Enumeration of sound flags
    /// </summary>
    [Flags]
    public enum ESoundFlags
    {
        /// <summary>
        /// Play synchronously (default)
        /// </summary>
        SND_SYNC = 0x0000, 
        /// <summary>
        /// Play asynchronously
        /// </summary>
        SND_ASYNC = 0x0001,  
        /// <summary>
        /// Silence (default) if sound not found
        /// </summary>
        SND_NODEFAULT = 0x0002,   
        /// <summary>
        /// Stream points to a memory file
        /// </summary>
        SND_MEMORY = 0x0004,
        /// <summary>
        /// Loop the sound until next sndPlaySound
        /// </summary>
        SND_LOOP = 0x0008,   
        /// <summary>
        /// Don't stop any currently playing sound
        /// </summary>
        SND_NOSTOP = 0x0010,
        /// <summary>
        /// Don't wait if the driver is busy
        /// </summary>
        SND_NOWAIT = 0x00002000, 
        /// <summary>
        /// Name is a registry alias
        /// </summary>
        SND_ALIAS = 0x00010000,
        /// <summary>
        /// Alias is a predefined ID
        /// </summary>
        SND_ALIAS_ID = 0x00110000,  
        /// <summary>
        /// Name is file name
        /// </summary>
        SND_FILENAME = 0x00020000,  
        /// <summary>
        /// Name is resource name or atom 
        /// </summary>
        SND_RESOURCE = 0x00040004 
    }

    /// <summary>
    /// Enumeration of sync modes
    /// </summary>
    public enum ESyncModes
    {
        /// <summary>
        /// Asynchronous
        /// </summary>
        Async = 0,
        /// <summary>
        /// Sychronous
        /// </summary>
        Sync
    }
}
