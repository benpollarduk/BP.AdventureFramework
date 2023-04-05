using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace AdventureFramework.Sound
{
    /// <summary>
    /// Represents a chain made out of a collection of Beeps
    /// </summary>
    [Serializable]
    public class Chain
    {
        #region Properties

        /// <summary>
        /// Get the Beep's that make up this chain
        /// </summary>
        public IBeep[] Beeps
        {
            get { return beeps.ToArray<IBeep>(); }
            protected set { beeps = new List<IBeep>(value); }
        }

        /// <summary>
        /// Get the Beep's that make up this chain
        /// </summary>
        private List<IBeep> beeps = new List<IBeep>();

        /// <summary>
        /// Get the total duration of this Chain
        /// </summary>
        public TimeSpan Duration
        {
            get
            {
                // hold running length in ms
                long lengthInMS = 0;

                // itterate each beep
                foreach (Beep b in Beeps)
                    // add length
                    lengthInMS += b.Duration;

                // return new timespan
                return TimeSpan.FromMilliseconds(lengthInMS);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the Chain class
        /// </summary>
        private Chain()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Chain class
        /// </summary>
        /// <param name="beeps">The beeps that make up this chain</param>
        public Chain(params IBeep[] beeps)
        {
            // set beeps
            this.beeps = new List<IBeep>(beeps);
        }

        /// <summary>
        /// Add a new beep to the end of the chain
        /// </summary>
        /// <param name="beep">The beep to add</param>
        public void AddBeep(IBeep beep)
        {
            // add
            beeps.Add(beep);
        }

        /// <summary>
        /// Add multiple new beeps to the end of the chain
        /// </summary>
        /// <param name="beeps">The beeps to add</param>
        public void AddRange(IBeep[] beeps)
        {
            // add beeps
            this.beeps.AddRange(beeps);
        }

        /// <summary>
        /// Remove the last beep in this chain
        /// </summary>
        public void RemoveLast()
        {
            // remove last
            beeps.RemoveAt(beeps.Count - 1);
        }

        /// <summary>
        /// Remove a range of beeps from this chain
        /// </summary>
        /// <param name="index">The index of the first beep to remove</param>
        /// <param name="count">The amount of beeps to remove from the index</param>
        public void RemoveRange(int index, int count)
        {
            // remove range
            beeps.RemoveRange(index, count);
        }

        /// <summary>
        /// Clear all beeps from this song
        /// </summary>
        public void Clear()
        {
            // clear all
            beeps.Clear();
        }

        /// <summary>
        /// Serialize this Chain to a file
        /// </summary>
        /// <param name="fullPath">The full path of the file to serialize to</param>
        /// <returns>True is the file is seralized correctly</returns>
        public bool SerializeToFile(string fullPath)
        {
            try
            {
                // create serializer
                var serializer = new BinaryFormatter();

                // create stream
                using (Stream stream = File.Open(fullPath, FileMode.Create))
                {
                    // serialize
                    serializer.Serialize(stream, this);
                }
            }
            catch (Exception e)
            {
                // display in debug
                Debug.WriteLine("Exception caught serializing the Chain: {0}", e.Message);

                // fail
                return false;
            }

            // pass
            return true;
        }

        #endregion
    }
}