using System.Xml;
using BP.AdventureFramework.Interaction;

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
        public int Row
        {
            get { return row; }
            set { row = value; }
        }

        /// <summary>
        /// Get or set the row of this Room within it's parent location
        /// </summary>
        private int row;

        /// <summary>
        /// Get or set the column of this Room within it's parent location
        /// </summary>
        public int Column
        {
            get { return column; }
            set { column = value; }
        }

        /// <summary>
        /// Get or set the column of this Room within it's parent location
        /// </summary>
        private int column;

        /// <summary>
        /// Get if this GameLocation has been visited
        /// </summary>
        public bool HasBeenVisited
        {
            get { return hasBeenVisited; }
            protected set { hasBeenVisited = value; }
        }

        /// <summary>
        /// Get or set if this GameLocation has been visited
        /// </summary>
        private bool hasBeenVisited;

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
        public GameLocation(int columnWithinParnt, int rowWithinParent)
        {
            // set column
            Column = columnWithinParnt;

            // set row
            Row = rowWithinParent;
        }

        /// <summary>
        /// Handle movement into this GameLocation
        /// </summary>
        /// <param name="fromDirection">The direction movement into this GameLocation is from. Use null if there should be no direction</param>
        public virtual void OnMovedInto(ECardinalDirection? fromDirection)
        {
            // visited
            HasBeenVisited = true;
        }

        #region XmlSerialization

        /// <summary>
        /// Handle writing of Xml for this GameLocation
        /// </summary>
        /// <param name="writer">The XmlWriter to write Xml with</param>
        protected override void OnWriteXml(XmlWriter writer)
        {
            // write start element
            writer.WriteStartElement("GameLocation");

            // write column attribute
            writer.WriteAttributeString("Column", Column.ToString());

            // write row attribute
            writer.WriteAttributeString("Row", Row.ToString());

            // write visited attribute
            writer.WriteAttributeString("HasBeenVisited", HasBeenVisited.ToString());

            // write base
            base.OnWriteXml(writer);

            // write end element
            writer.WriteEndElement();
        }

        /// <summary>
        /// Handle reading of Xml for this GameLocation
        /// </summary>
        /// <param name="node">The node to read Xml from</param>
        protected override void OnReadXmlNode(XmlNode node)
        {
            // get column
            Column = int.Parse(GetAttribute(node, "Column").Value);

            // get row
            Row = int.Parse(GetAttribute(node, "Row").Value);

            // get if visited
            HasBeenVisited = bool.Parse(GetAttribute(node, "HasBeenVisited").Value);

            // read base
            base.OnReadXmlNode(GetNode(node, "ExaminableObject"));
        }

        #endregion

        #endregion
    }
}