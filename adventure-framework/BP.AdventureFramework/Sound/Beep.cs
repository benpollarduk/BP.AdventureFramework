using System;

namespace AdventureFramework.Sound
{
    /// <summary>
    /// Represents a single beep of the motherboard
    /// </summary>
    [Serializable]
    public struct Beep : IBeep
    {
        #region StaticProperties

        /// <summary>
        /// Get the frequency between octaves
        /// </summary>
        public const int FrequencyChangeBetweenOctaves = 262;

        #endregion

        #region Properties

        /// <summary>
        /// Get or set the duration of this note in milliseconds
        /// </summary>
        private int duration;

        /// <summary>
        /// Get or set the frequency of the note that will be played
        /// </summary>
        private int frequency;

        #endregion

        #region Methods        

        /// <summary>
        /// Initializes a new instance of the Beep struct with a standard duration of 250ms
        /// </summary>
        /// <param name="frequency">Specify the frequency of the note of this FrequencyBeep</param>
        public Beep(int frequency)
        {
            // set note
            this.frequency = frequency;

            // set duration
            duration = 250;
        }

        /// <summary>
        /// Initializes a new instance of the Beep struct
        /// </summary>
        /// <param name="frequency">Specify the frequency of the note of this FrequencyBeep</param>
        /// <param name="duration">Specify the duration of this FrequencyBeep</param>
        public Beep(int frequency, int duration)
        {
            // set note
            this.frequency = frequency;

            // set duration
            this.duration = duration;
        }

        /// <summary>
        /// Get this Beep as a string
        /// </summary>
        /// <returns>This Beep displayed as a string</returns>
        public override string ToString()
        {
            // return as Hz and duration
            return string.Format("{0}{1} Hz for {2}{3} ms", frequency, getWhiteSpace(5 - frequency.ToString().Length), duration, getWhiteSpace(5 - duration.ToString().Length));
        }

        /// <summary>
        /// Get a portion of whitespace
        /// </summary>
        /// <param name="length">The length (in characters) of the whitespace</param>
        /// <returns>A whitespace string</returns>
        private string getWhiteSpace(int length)
        {
            // hold whitespace
            var ws = string.Empty;

            // itterate all whitespace
            for (var index = 0; index < length; index++)
                // add white
                ws += " ";

            // return whitespace
            return ws;
        }

        #endregion

        #region IBeep Members

        /// <summary>
        /// Get or set the duration in ms
        /// </summary>
        public int Duration
        {
            get { return duration; }
            set { duration = value; }
        }

        /// <summary>
        /// Get or set the frequency in htZ
        /// </summary>
        public int Frequency
        {
            get { return frequency; }
            set { frequency = value; }
        }

        #endregion
    }
}