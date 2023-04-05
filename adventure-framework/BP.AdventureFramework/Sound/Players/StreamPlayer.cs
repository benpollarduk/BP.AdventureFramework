using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;

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
        private static extern bool PlaySound(byte[] data, IntPtr hMod, uint dwFlags);

        #endregion

        #region StaticMethods

        /// <summary>
        /// Play a sound file. The resource will be played asychronously
        /// </summary>
        /// <param name="fileName">The full file name of the file to play</param>
        public static void PlayWav(string fileName)
        {
            // play stream
            PlayWav(fileName, (uint)(ESoundFlags.SND_FILENAME | ESoundFlags.SND_ASYNC));
        }

        /// <summary>
        /// Play a sound file
        /// </summary>
        /// <param name="fileName">The full file name of the file to play</param>
        /// <param name="flags">The flags to use when playing the sound</param>
        public static void PlayWav(string fileName, uint flags)
        {
            // play stream
            PlayWav(fileName, flags, ESyncModes.Sync);
        }

        /// <summary>
        /// Play a sound file
        /// </summary>
        /// <param name="fileName">The full file name of the file to play</param>
        /// <param name="flags">The flags to use when playing the sound</param>
        /// <param name="preSendMode">The sync mode to use prior to sending the sound</param>
        public static void PlayWav(string fileName, uint flags, ESyncModes preSendMode)
        {
            try
            {
                // if file exists
                if (File.Exists(fileName))
                {
                    // hold sound stream
                    MemoryStream soundStream = null;

                    // open file
                    using (var fStream = new FileStream(fileName, FileMode.Open))
                    {
                        // create array for all bytes of wav
                        var wavBytes = new byte[fStream.Length];

                        // read bytes
                        fStream.Read(wavBytes, 0, (int)fStream.Length);

                        // load bytes from buffer
                        soundStream = new MemoryStream(wavBytes);
                    }

                    // play stream
                    PlayStream(soundStream, flags, preSendMode);
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
        public static void PlayResourceStream(Assembly assembly, string fileName)
        {
            // play
            PlayResourceStream(assembly, fileName, (uint)(ESoundFlags.SND_ASYNC | ESoundFlags.SND_MEMORY));
        }

        /// <summary>
        /// Play a sound file from a resource
        /// </summary>
        /// <param name="assembly">The assembly the sound file belongs to</param>
        /// <param name="fileName">The file name of the stream, including it's extension. Any directories that the resource lies in should be specified with '.' instead of '/' e.g. .DirectoryName.File.wav</param>
        /// <param name="flags">The flags to use when playing the sound</param>
        public static void PlayResourceStream(Assembly assembly, string fileName, uint flags)
        {
            // play
            PlayResourceStream(assembly, fileName, flags, ESyncModes.Sync);
        }

        /// <summary>
        /// Play a sound file from a resource
        /// </summary>
        /// <param name="assembly">The assembly the sound file belongs to</param>
        /// <param name="fileName">The file name of the stream, including it's extension. Any directories that the resource lies in should be specified with '.' instead of '/' e.g. .DirectoryName.File.wav</param>
        /// <param name="flags">The flags to use when playing the sound</param>
        /// <param name="preSendMode">The sync mode to use prior to sending the sound</param>
        public static void PlayResourceStream(Assembly assembly, string fileName, uint flags, ESyncModes preSendMode)
        {
            try
            {
                // all embedded sounds must be set to "embeded resource" under their property

                // get the resource name
                var uriName = assembly.GetName().Name + ".Sounds." + fileName;

                // get stream from resources
                var soundStream = assembly.GetManifestResourceStream(uriName);

                // play
                PlayStream(soundStream, flags, preSendMode);
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
            PlayStream(stream, (uint)(ESoundFlags.SND_ASYNC | ESoundFlags.SND_MEMORY));
        }

        /// <summary>
        /// Play a stream
        /// </summary>
        /// <param name="stream">The stream to play</param>
        /// <param name="flags">The flags to use when playing the sound</param>
        public static void PlayStream(Stream stream, uint flags)
        {
            try
            {
                // only play if using sounds
                if (UseSounds)
                {
                    // create byte array to stream length
                    var streamAsByteArray = new byte[stream.Length];

                    // set array from stream
                    stream.Read(streamAsByteArray, 0, streamAsByteArray.Length);

                    // play byte array stream using logical inclusive or to specify flags
                    PlaySound(streamAsByteArray, IntPtr.Zero, flags);
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
        public static void PlayStream(Stream stream, uint flags, ESyncModes preSendMode)
        {
            // switch mode
            switch (preSendMode)
            {
                case ESyncModes.Sync:
                    {
                        // play sound in sync from memory
                        PlayStream(stream, (uint)(ESoundFlags.SND_MEMORY | ESoundFlags.SND_SYNC));

                        break;
                    }
                case ESyncModes.Async:
                    {
                        // play the resource on a thread pool thread - this is so that it is ASYNC without scratching
                        ThreadPool.QueueUserWorkItem(delegate { PlayStream(stream, (uint)(ESoundFlags.SND_MEMORY | ESoundFlags.SND_SYNC)); });

                        break;
                    }
                default:
                    {
                        throw new NotImplementedException();
                    }
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