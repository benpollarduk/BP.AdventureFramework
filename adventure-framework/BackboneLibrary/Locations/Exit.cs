using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventureFramework.Interaction;
using AdventureFramework.IO;
using System.Xml.Serialization;

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
            get { return this.direction; }
            protected set { this.direction = value; }
        }

        /// <summary>
        /// Get or set the direcion of the exit
        /// </summary>
        private ECardinalDirection direction;

        /// <summary>
        /// Get if this Exit is locked
        /// </summary>
        public Boolean IsLocked
        {
            get { return this.isLocked; }
            protected set { this.isLocked = value; }
        }

        /// <summary>
        /// Get if this Exit is locked
        /// </summary>
        private Boolean isLocked = false;

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
            this.Direction = direction;

            // set unlocked
            this.IsLocked = false;

            // generate description
            this.Description = this.GenerateDescription();
        }

        /// <summary>
        /// Initializes a new instance of the Exit class
        /// </summary>
        /// <param name="direction">The direction of the exit</param>
        /// <param name="isLocked">If this exit is locked</param>
        public Exit(ECardinalDirection direction, Boolean isLocked)
        {
            // set direction
            this.Direction = direction;

            // set if locked
            this.IsLocked = isLocked;

            // generate description
            this.Description = this.GenerateDescription();
        }

        /// <summary>
        /// Initializes a new instance of the Exit class
        /// </summary>
        /// <param name="direction">The direction of the exit</param>
        /// <param name="isLocked">If this exit is locked</param>
        /// <param name="description">A description of this exit</param>
        public Exit(ECardinalDirection direction, Boolean isLocked, Description description)
        {
            // set direction
            this.Direction = direction;

            // set if locked
            this.IsLocked = isLocked;

            // set description
            this.Description = description;
        }

        /// <summary>
        /// generate a description for this exit
        /// </summary>
        /// <returns>The completed Description</returns>
        protected virtual Description GenerateDescription()
        {
            // set description
            return new ConditionalDescription(String.Format("The exit {0} is locked", direction.ToString().ToLower()), String.Format("The exit {0} is unlocked", direction.ToString().ToLower()), new Condition(() => { return this.IsLocked; }));
        }

        /// <summary>
        /// Set if this exit is locked
        /// </summary>
        public void Unlock()
        {
            // unlock
            this.IsLocked = false;
        }

        #region XmlSerialization

        /// <summary>
        /// Handle writing of Xml for this Exit
        /// </summary>
        /// <param name="writer">The XmlWriter to write Xml with</param>
        protected override void OnWriteXml(System.Xml.XmlWriter writer)
        {
            // write start element
            writer.WriteStartElement("Exit");

            // write direction
            writer.WriteAttributeString("Direction", this.Direction.ToString());

            // write if locked
            writer.WriteAttributeString("IsLocked", this.IsLocked.ToString());

            // write base
            base.OnWriteXml(writer);

            // write end element
            writer.WriteEndElement();
        }

        /// <summary>
        /// Handle reading of Xml for this Exit
        /// </summary>
        /// <param name="node">The node to read Xml from</param>
        protected override void OnReadXmlNode(System.Xml.XmlNode node)
        {
            // get direction
            this.Direction = (ECardinalDirection)Enum.Parse(typeof(ECardinalDirection), XMLSerializableObject.GetAttribute(node, "Direction").Value);

            // get if locked
            this.IsLocked = Boolean.Parse(XMLSerializableObject.GetAttribute(node, "IsLocked").Value);

            // read base
            base.OnReadXmlNode(XMLSerializableObject.GetNode(node, "ExaminableObject"));
        }

        #endregion

        #endregion
    }
}
