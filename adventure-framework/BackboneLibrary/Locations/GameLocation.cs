using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventureFramework.Interaction;
using System.Xml.Serialization;
using AdventureFramework.IO;

namespace AdventureFramework.Locations
{
    /// <summary>
    /// Represents any game location
    /// </summary>
    public class GameLocation : ExaminableObject
    {
        #region Properties

        /// <summary>
        /// Get or set the row of this Room within it's parent location
        /// </summary>
        public Int32 Row
        {
            get { return this.row; }
            set { this.row = value; }
        }

        /// <summary>
        /// Get or set the row of this Room within it's parent location
        /// </summary>
        private Int32 row;

        /// <summary>
        /// Get or set the column of this Room within it's parent location
        /// </summary>
        public Int32 Column
        {
            get { return this.column; }
            set { this.column = value; }
        }

        /// <summary>
        /// Get or set the column of this Room within it's parent location
        /// </summary>
        private Int32 column;

        /// <summary>
        /// Get if this GameLocation has been visited
        /// </summary>
        public Boolean HasBeenVisited
        {
            get { return this.hasBeenVisited; }
            protected set { this.hasBeenVisited = value; }
        }

        /// <summary>
        /// Get or set if this GameLocation has been visited
        /// </summary>
        private Boolean hasBeenVisited = false;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the GameLocation class
        /// </summary>
        public GameLocation()
        {
        }

        /// <summary>
        /// Initializes a new instance of the GameLocation class
        /// </summary>
        /// <param name="columnWithinParnt">The column this GameLocation resides within its parent</param>
        /// <param name="rowWithinParent">The row this GameLocation resides within it's parent</param>
        public GameLocation(Int32 columnWithinParnt, Int32 rowWithinParent)
        {
            // set column
            this.Column = columnWithinParnt;

            // set row
            this.Row = rowWithinParent;
        }

        /// <summary>
        /// Handle movement into this GameLocation
        /// </summary>
        /// <param name="fromDirection">The direction movement into this GameLocation is from. Use null if there should be no direction</param>
        public virtual void OnMovedInto(ECardinalDirection? fromDirection)
        {
            // visited
            this.HasBeenVisited = true;
        }

        #region XmlSerialization

        /// <summary>
        /// Handle writing of Xml for this GameLocation
        /// </summary>
        /// <param name="writer">The XmlWriter to write Xml with</param>
        protected override void OnWriteXml(System.Xml.XmlWriter writer)
        {
            // write start element
            writer.WriteStartElement("GameLocation");
            
            // write column attribute
            writer.WriteAttributeString("Column", this.Column.ToString());
            
            // write row attribute
            writer.WriteAttributeString("Row", this.Row.ToString());

            // write visited attribute
            writer.WriteAttributeString("HasBeenVisited", this.HasBeenVisited.ToString());

            // write base
            base.OnWriteXml(writer);

            // write end element
            writer.WriteEndElement();
        }

        /// <summary>
        /// Handle reading of Xml for this GameLocation
        /// </summary>
        /// <param name="node">The node to read Xml from</param>
        protected override void OnReadXmlNode(System.Xml.XmlNode node)
        {
            // get column
            this.Column = Int32.Parse(XMLSerializableObject.GetAttribute(node, "Column").Value);

            // get row
            this.Row = Int32.Parse(XMLSerializableObject.GetAttribute(node, "Row").Value);

            // get if visited
            this.HasBeenVisited = Boolean.Parse(XMLSerializableObject.GetAttribute(node, "HasBeenVisited").Value);

            // read base
            base.OnReadXmlNode(XMLSerializableObject.GetNode(node, "ExaminableObject"));
        }

        #endregion

        #endregion
    }
}
