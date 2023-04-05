using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventureFramework.Sound
{
    /// <summary>
    /// Represents a musical note
    /// </summary>
    public struct Note : IBeep
    {
        #region Properties

        /// <summary>
        /// Get or set the duration of this Note
        /// </summary>
        public ENoteDuration NoteDuration
        {
            get { return this.noteDuration; }
            set { this.noteDuration = value; }
        }

        /// <summary>
        /// Get or set the duration of this Note
        /// </summary>
        private ENoteDuration noteDuration;

        /// <summary>
        /// Get or set the note that will be played
        /// </summary>
        public EConsoleNote MusicalNote
        {
            get { return this.musicalNote; }
            set { this.musicalNote = value; }
        }

        /// <summary>
        /// Get or set the note that will be played
        /// </summary>
        private EConsoleNote musicalNote;

        #endregion

        #region Methods        

        /// <summary>
        /// Initializes a new instance of the Note struct with a standard duration of a whole note
        /// </summary>
        /// <param name="note">Specify the note of this Note</param>
        public Note(EConsoleNote note)
        {
            // set note
            this.musicalNote = note;

            // set duration
            this.noteDuration = ENoteDuration.Whole;
        }

        /// <summary>
        /// Initializes a new instance of the Note struct
        /// </summary>
        /// <param name="note">Specify the frequency of the note of this Note</param>
        /// <param name="duration">Specify the duration of this Note</param>
        public Note(EConsoleNote note, ENoteDuration duration)
        {
            // set note
            this.musicalNote = note;

            // set duration
            this.noteDuration = duration;
        }

        /// <summary>
        /// Get this Note as a string
        /// </summary>
        /// <returns>This Note as a string</returns>
        public override string ToString()
        {
            // return as note and duration
            return String.Format("{0} {1}", this.musicalNote, this.Duration);
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
                return (Int32)this.NoteDuration;
            }
            set
            {
                this.NoteDuration = (ENoteDuration)value;
            }
        }

        /// <summary>
        /// Get or set the frequency in htZ
        /// </summary>
        public int Frequency
        {
            get
            {
                return (Int32)this.MusicalNote;
            }
            set
            {
                this.MusicalNote = (EConsoleNote)value;
            }
        }

        #endregion
    }

    /// <summary>
    /// Enumeration of console notes in the second ocatve 
    /// </summary>
    public enum EConsoleNote
    {
        /// <summary>
        /// A musical rest
        /// </summary>
        Rest = 0,
        /// <summary>
        /// Note C in the 2nd octave
        /// </summary>
        C2 = 261, // 261.6
        /// <summary>
        /// Note C# in the 2nd octave
        /// </summary>
        Cs2 = 277, // 277.2
        /// <summary>
        /// Note D in the 2nd octave
        /// </summary>
        D2 = 293, // 293.7
        /// <summary>
        /// Note D# in the 2nd octave
        /// </summary>
        Ds2 = 311, // 311.1
        /// <summary>
        /// Note E in the 2nd octave
        /// </summary>
        E2 = 329, // 329.6
        /// <summary>
        /// Note F in the 2nd octave
        /// </summary>
        F2 = 349, // 349.2
        /// <summary>
        /// Note F# in the 2nd octave
        /// </summary>
        Fs2 = 370, // 370.0
        /// <summary>
        /// Note G in the 2nd octave
        /// </summary>
        G = 392, // 392.0
        /// <summary>
        /// Note G# in the 2nd octave
        /// </summary>
        Gs2 = 415, // 415.3
        /// <summary>
        /// Note A in the 2nd octave
        /// </summary>
        A = 440, // 440.0
        /// <summary>
        /// Note A# in the 2nd octave
        /// </summary>
        As2 = 466, // 466.2
        /// <summary>
        /// Note B in the 2nd octave
        /// </summary>
        B = 493, // 493.9
        /// <summary>
        /// Note C in the 3rd octave
        /// </summary>
        C3 = 523 // 523.2
    }

    /// <summary>
    /// Enueration of not durations, based on 120 bpm
    /// </summary>
    public enum ENoteDuration
    {
        /// <summary>
        /// A whole note
        /// </summary>
        Whole = 500,
        /// <summary>
        /// A half note
        /// </summary>
        Half = ENoteDuration.Whole / 2,
        /// <summary>
        /// A quater note
        /// </summary>
        Quater = ENoteDuration.Half / 2,
        /// <summary>
        /// An eigth note
        /// </summary>
        Eighth = ENoteDuration.Quater / 2,
        /// <summary>
        /// A sixteenth note
        /// </summary>
        Sixteenth = ENoteDuration.Eighth / 2,
        /// <summary>
        /// A eighth . note
        /// </summary>
        EighthDot = ENoteDuration.Eighth + ENoteDuration.Sixteenth,
        /// <summary>
        /// A quater . note
        /// </summary>
        QuaterDot = ENoteDuration.Quater + ENoteDuration.Eighth,
        /// <summary>
        /// A sixteenth . note
        /// </summary>
        SixteenthDot = ENoteDuration.Sixteenth + (ENoteDuration.Sixteenth / 2)
    }

}
