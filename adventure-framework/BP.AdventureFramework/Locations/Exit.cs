using System;
using System.Xml;
using BP.AdventureFramework.Interaction;

namespace AdventureFramework.Locations
{
    /// <summary>
    /// Represents an exit from a GameLocation
    /// </summary>
    public class Exit : ExaminableObject
    {
        #region Properties

        /// <summary>
        /// Get the direcion of the exit
        /// </summary>
        public ECardinalDirection Direction
        {
            get { return direction; }
            protected set { direction = value; }
        }

        /// <summary>
        /// Get or set the direcion of the exit
        /// </summary>
        private ECardinalDirection direction;

        /// <summary>
        /// Get if this Exit is locked
        /// </summary>
        public bool IsLocked
        {
            get { return isLocked; }
            protected set { isLocked = value; }
        }

        /// <summary>
        /// Get if this Exit is locked
        /// </summary>
        private bool isLocked;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the Exit class
        /// </summary>
        protected Exit()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Exit class
        /// </summary>
        /// <param name="direction">The direction of the exit</param>
        public Exit(ECardinalDirection direction)
        {
            // set direction
            Direction = direction;

            // set unlocked
            IsLocked = false;

            // generate description
            Description = GenerateDescription();
        }

        /// <summary>
        /// Initializes a new instance of the Exit class
        /// </summary>
        /// <param name="direction">The direction of the exit</param>
        /// <param name="isLocked">If this exit is locked</param>
        public Exit(ECardinalDirection direction, bool isLocked)
        {
            // set direction
            Direction = direction;

            // set if locked
            IsLocked = isLocked;

            // generate description
            Description = GenerateDescription();
        }

        /// <summary>
        /// Initializes a new instance of the Exit class
        /// </summary>
        /// <param name="direction">The direction of the exit</param>
        /// <param name="isLocked">If this exit is locked</param>
        /// <param name="description">A description of this exit</param>
        public Exit(ECardinalDirection direction, bool isLocked, Description description)
        {
            // set direction
            Direction = direction;

            // set if locked
            IsLocked = isLocked;

            // set description
            Description = description;
        }

        /// <summary>
        /// generate a description for this exit
        /// </summary>
        /// <returns>The completed Description</returns>
        protected virtual Description GenerateDescription()
        {
            // set description
            return new ConditionalDescription(string.Format("The exit {0} is locked", direction.ToString().ToLower()), string.Format("The exit {0} is unlocked", direction.ToString().ToLower()), () => { return IsLocked; });
        }

        /// <summary>
        /// Set if this exit is locked
        /// </summary>
        public void Unlock()
        {
            // unlock
            IsLocked = false;
        }

        #region XmlSerialization

        /// <summary>
        /// Handle writing of Xml for this Exit
        /// </summary>
        /// <param name="writer">The XmlWriter to write Xml with</param>
        protected override void OnWriteXml(XmlWriter writer)
        {
            // write start element
            writer.WriteStartElement("Exit");

            // write direction
            writer.WriteAttributeString("Direction", Direction.ToString());

            // write if locked
            writer.WriteAttributeString("IsLocked", IsLocked.ToString());

            // write base
            base.OnWriteXml(writer);

            // write end element
            writer.WriteEndElement();
        }

        /// <summary>
        /// Handle reading of Xml for this Exit
        /// </summary>
        /// <param name="node">The node to read Xml from</param>
        protected override void OnReadXmlNode(XmlNode node)
        {
            // get direction
            Direction = (ECardinalDirection)Enum.Parse(typeof(ECardinalDirection), GetAttribute(node, "Direction").Value);

            // get if locked
            IsLocked = bool.Parse(GetAttribute(node, "IsLocked").Value);

            // read base
            base.OnReadXmlNode(GetNode(node, "ExaminableObject"));
        }

        #endregion

        #endregion
    }
}