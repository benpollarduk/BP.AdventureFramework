using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

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
        public const Int32 FrequencyChangeBetweenOctaves = 262;

        #endregion

        #region Properties

        /// <summary>
        /// Get or set the duration of this note in milliseconds
        /// </summary>
        private Int32 duration;

        /// <summary>
        /// Get or set the frequency of the note that will be played
        /// </summary>
        private Int32 frequency;

        #endregion

        #region Methods        

        /// <summary>
        /// Initializes a new instance of the Beep struct with a standard duration of 250ms
        /// </summary>
        /// <param name="frequency">Specify the frequency of the note of this FrequencyBeep</param>
        public Beep(Int32 frequency)
        {
            // set note
            this.frequency = frequency;

            // set duration
            this.duration = 250;
        }

        /// <summary>
        /// Initializes a new instance of the Beep struct
        /// </summary>
        /// <param name="frequency">Specify the frequency of the note of this FrequencyBeep</param>
        /// <param name="duration">Specify the duration of this FrequencyBeep</param>
        public Beep(Int32 frequency, Int32 duration)
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
            return String.Format("{0}{1} Hz for {2}{3} ms", frequency, this.getWhiteSpace(5 - frequency.ToString().Length), duration, this.getWhiteSpace(5 - duration.ToString().Length));
        }

        /// <summary>
        /// Get a portion of whitespace
        /// </summary>
        /// <param name="length">The length (in characters) of the whitespace</param>
        /// <returns>A whitespace string</returns>
        private string getWhiteSpace(Int32 length)
        {
            // hold whitespace
            String ws = String.Empty;

            // itterate all whitespace
            for (Int32 index = 0; index < length; index++)
            {
                // add white
                ws += " ";
            }

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
            get
            {
                return this.duration;
            }
            set
            {
                this.duration = value;
            }
        }

        /// <summary>
        /// Get or set the frequency in htZ
        /// </summary>
        public int Frequency
        {
            get
            {
                return this.frequency;
            }
            set
            {
                this.frequency = value;
            }
        }

        #endregion
    }
}
