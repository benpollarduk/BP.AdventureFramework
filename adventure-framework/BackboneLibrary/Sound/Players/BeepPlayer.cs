using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace AdventureFramework.Sound.Players
{
    /// <summary>
    /// A class that allows advanced control of the System.Console.Beep() method
    /// </summary>
    public abstract class BeepPlayer : SoundPlayer
    {
        #region StaticProperties

        /// <summary>
        /// Get or set if playing has been cancelled
        /// </summary>
        private static Boolean hasBeenCancelled = false;

        #endregion

        #region StaticMethods

        /// <summary>
        /// Play many Beeps
        /// </summary>
        /// <param name="beeps">The beeps to play</param>
        /// <param name="cycles">The amount of times the beeps should be repeated. Use -1 for inifinity</param>
        public static void PlayBeeps(IBeep[] beeps, Int32 cycles)
        {
            try
            {
                // play the song the set number of times
                for (Int32 index = 0; index < cycles; index++)
                {
                    // play each note from generated song
                    foreach (IBeep currentNote in beeps)
                    {
                        // play note
                        BeepPlayer.PlayBeep(currentNote.Frequency, currentNote.Duration);

                        // if cancelled
                        if (BeepPlayer.hasBeenCancelled)
                        {
                            break;
                        }
                    }

                    // if cancelled
                    if (BeepPlayer.hasBeenCancelled)
                    {
                        break;
                    }
                }
            }
            catch (NullReferenceException nREx)
            {
                // display in debug
                Debug.WriteLine("Exception caught playing beep: {0}", nREx.Message);
            }
        }

        /// <summary>
        /// Play a Chain
        /// </summary>
        /// <param name="chain">The chain to play</param>
        public static void PlayChain(Chain chain)
        {
            try
            {
                // play the chain
                BeepPlayer.PlayBeeps(chain.Beeps, 1);
            }
            catch (NullReferenceException nREx)
            {
                // display in debug
                Debug.WriteLine("Exception caught playing beep: {0}", nREx.Message);
            }
        }

        /// <summary>
        /// Play a Chain
        /// </summary>
        /// <param name="chain">The song to play</param>
        /// <param name="cycles">The amount of times the chain should be repeated. Use -1 for inifinity</param>
        /// <param name="mode">The sync mode to use</param>
        public static void PlayChain(Chain chain, Int32 cycles, ESyncModes mode)
        {
            // select mode
            switch (mode)
            {
                case (ESyncModes.Async):
                    {
                        // play on thread pool
                        ThreadPool.QueueUserWorkItem(new WaitCallback((Object obj) =>
                        {
                            // play the chain
                            BeepPlayer.PlayBeeps(chain.Beeps, cycles);
                        }));

                        break;
                    }
                case (ESyncModes.Sync):
                    {
                        // play the chain
                        BeepPlayer.PlayBeeps(chain.Beeps, cycles);

                        break;
                    }
                default: { throw new NotImplementedException(); }
            }
        }

        /// <summary>
        /// Play a note for a set duration of one whole beat
        /// </summary>
        /// <param name="note">The note to play</param>
        public static void PlayNote(EConsoleNote note)
        {
            // play note for set duration
            BeepPlayer.PlayNote(note, ENoteDuration.Whole);
        }

        /// <summary>
        /// Play a note for a specified duration
        /// </summary>
        /// <param name="note">The note to play</param>
        /// <param name="duration">The duration of the note to play</param>
        public static void PlayNote(EConsoleNote note, ENoteDuration duration)
        {
            // play
            BeepPlayer.PlayBeep((Int32)note, (Int32)duration);
        }

        /// <summary>
        /// Play a note for a specified duration
        /// </summary>
        /// <param name="note">The note to play</param>
        public static void PlayNote(Note note)
        {
            try
            {
                // play
                BeepPlayer.PlayBeep(note.Frequency, note.Duration);
            }
            catch (NullReferenceException nREx)
            {
                // display in debug
                Debug.WriteLine("Exception caught playing beep: {0}", nREx.Message);
            }
        }

        /// <summary>
        /// Play many Notes
        /// </summary>
        /// <param name="notes">The notes to play</param>
        /// <param name="cycles">The amount of times the notes should be repeated. Use -1 for inifinity</param>
        public static void PlayNotes(Note[] notes, Int32 cycles)
        {
            // create array
            IBeep[] beeps = new IBeep[notes.Length];

            // itterate all notes
            for (Int32 index = 0; index < notes.Length; index++)
            {
                // set element
                beeps[index] = notes[index] as IBeep;
            }

            // play
            BeepPlayer.PlayBeeps(beeps, cycles);
        }

        /// <summary>
        /// Play a beep for a set duration of 250ms
        /// </summary>
        /// <param name="frequency">The frequency of the beep to play</param>
        public static void PlayBeep(Int32 frequency)
        {
            // play note for set duration
            BeepPlayer.PlayBeep(frequency, 250);
        }

        /// <summary>
        /// Play a beep for a specified duration
        /// </summary>
        /// <param name="beep">The beep to play</param>
        public static void PlayBeep(IBeep beep)
        {
            try
            {
                // play
                BeepPlayer.PlayBeep(beep.Frequency, beep.Duration);
            }
            catch (NullReferenceException nREx)
            {
                // display in debug
                Debug.WriteLine("Exception caught playing beep: {0}", nREx.Message);
            }
        }

        /// <summary>
        /// Play a beep for a specified duration
        /// </summary>
        /// <param name="frequency">The frequency of the beep to play</param>
        /// <param name="duration">The duration of the beep to play</param>
        public static void PlayBeep(Int32 frequency, Int32 duration)
        {
            // only play if using sounds
            if (SoundPlayer.UseSounds)
            {
                // reset to not been cancelled
                BeepPlayer.hasBeenCancelled = false;

                // if above 0
                if (frequency > 0)
                {
                    // play the note
                    Console.Beep(frequency, duration);
                }
                else
                {
                    // rest
                    Thread.Sleep(duration);
                }
            }
        }

        /// <summary>
        /// Cancel any playing beep
        /// </summary>
        public static void Cancel()
        {
            // set cancelled
            BeepPlayer.hasBeenCancelled = true;

            // do inaudible clearence beep
            Console.Beep(37, 0);
        }

        #endregion
    }
}
