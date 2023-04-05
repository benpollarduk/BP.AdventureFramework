using System;
using System.Diagnostics;
using System.Threading;

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
        private static bool hasBeenCancelled;

        #endregion

        #region StaticMethods

        /// <summary>
        /// Play many Beeps
        /// </summary>
        /// <param name="beeps">The beeps to play</param>
        /// <param name="cycles">The amount of times the beeps should be repeated. Use -1 for inifinity</param>
        public static void PlayBeeps(IBeep[] beeps, int cycles)
        {
            try
            {
                // play the song the set number of times
                for (var index = 0; index < cycles; index++)
                {
                    // play each note from generated song
                    foreach (var currentNote in beeps)
                    {
                        // play note
                        PlayBeep(currentNote.Frequency, currentNote.Duration);

                        // if cancelled
                        if (hasBeenCancelled) break;
                    }

                    // if cancelled
                    if (hasBeenCancelled) break;
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
                PlayBeeps(chain.Beeps, 1);
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
        public static void PlayChain(Chain chain, int cycles, ESyncModes mode)
        {
            // select mode
            switch (mode)
            {
                case ESyncModes.Async:
                    {
                        // play on thread pool
                        ThreadPool.QueueUserWorkItem(obj =>
                        {
                            // play the chain
                            PlayBeeps(chain.Beeps, cycles);
                        });

                        break;
                    }
                case ESyncModes.Sync:
                    {
                        // play the chain
                        PlayBeeps(chain.Beeps, cycles);

                        break;
                    }
                default:
                    {
                        throw new NotImplementedException();
                    }
            }
        }

        /// <summary>
        /// Play a note for a set duration of one whole beat
        /// </summary>
        /// <param name="note">The note to play</param>
        public static void PlayNote(EConsoleNote note)
        {
            // play note for set duration
            PlayNote(note, ENoteDuration.Whole);
        }

        /// <summary>
        /// Play a note for a specified duration
        /// </summary>
        /// <param name="note">The note to play</param>
        /// <param name="duration">The duration of the note to play</param>
        public static void PlayNote(EConsoleNote note, ENoteDuration duration)
        {
            // play
            PlayBeep((int)note, (int)duration);
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
                PlayBeep(note.Frequency, note.Duration);
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
        public static void PlayNotes(Note[] notes, int cycles)
        {
            // create array
            var beeps = new IBeep[notes.Length];

            // itterate all notes
            for (var index = 0; index < notes.Length; index++)
                // set element
                beeps[index] = notes[index];

            // play
            PlayBeeps(beeps, cycles);
        }

        /// <summary>
        /// Play a beep for a set duration of 250ms
        /// </summary>
        /// <param name="frequency">The frequency of the beep to play</param>
        public static void PlayBeep(int frequency)
        {
            // play note for set duration
            PlayBeep(frequency, 250);
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
                PlayBeep(beep.Frequency, beep.Duration);
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
        public static void PlayBeep(int frequency, int duration)
        {
            // only play if using sounds
            if (UseSounds)
            {
                // reset to not been cancelled
                hasBeenCancelled = false;

                // if above 0
                if (frequency > 0)
                    // play the note
                    Console.Beep(frequency, duration);
                else
                    // rest
                    Thread.Sleep(duration);
            }
        }

        /// <summary>
        /// Cancel any playing beep
        /// </summary>
        public static void Cancel()
        {
            // set cancelled
            hasBeenCancelled = true;

            // do inaudible clearence beep
            Console.Beep(37, 0);
        }

        #endregion
    }
}