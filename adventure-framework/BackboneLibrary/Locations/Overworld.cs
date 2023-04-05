using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventureFramework.Interaction;
using System.Xml.Serialization;
using System.Xml;
using AdventureFramework.IO;

namespace AdventureFramework.Locations
{
    /// <summary>
    /// Represents an entire overworld
    /// </summary>
    public class Overworld : GameLocation
    {
        #region Properies

        /// <summary>
        /// Get the Regions in this Overworld
        /// </summary>
        public Region[] Regions
        {
            get { return this.regions.ToArray<Region>(); }
        }

        /// <summary>
        /// Get or set the Regions in this Overworld
        /// </summary>
        private List<Region> regions = new List<Region>();

        /// <summary>
        /// Get the current Region
        /// </summary>
        public Region CurrentRegion
        {
            get { return this.currentRegion ?? (this.regions.Count > 0 ? this.regions[0] : null); }
            protected set { this.currentRegion = value; }
        }

        /// <summary>
        /// Get or set the current Region
        /// </summary>
        private Region currentRegion;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the Overworld class
        /// </summary>
        protected Overworld()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Overworld class
        /// </summary>
        /// <param name="name">The name of this Overworld</param>
        /// <param name="description">A description of this Overworld</param>
        public Overworld(String name, String description)
        {
            // set name
            this.name = name;

            // set description
            this.description = new Description(description);
        }

        /// <summary>
        /// Initializes a new instance of the Overworld class
        /// </summary>
        /// <param name="name">The name of this Overworld</param>
        /// <param name="description">A description of this Overworld</param>
        public Overworld(String name, Description description)
        {
            // set name
            this.name = name;

            // set description
            this.description = description;
        }

        /// <summary>
        /// Create a Region in this Overworld
        /// </summary>
        /// <param name="region">The Region to create</param>
        /// <param name="columnInOverworld">The column of the Region with this Overworld</param>
        /// <param name="rowInOverworld">The row of the Region within this Overworld</param>
        public Boolean CreateRegion(Region region, Int32 columnInOverworld, Int32 rowInOverworld)
        {
            // hold if ok to add
            Boolean isOkToAdd = true;

            // set column
            region.Column = columnInOverworld;

            // set row
            region.Row = rowInOverworld;

            // itterate regions
            foreach (Region r in this.Regions)
            {
                // if collided
                if ((r.Column == region.Column) &&
                    (r.Row == region.Row))
                {
                    // not ok
                    isOkToAdd = false;

                    break;
                }
            }

            // if ok to add
            if (isOkToAdd)
            {
                // add region
                this.regions.Add(region);
            }

            // return result
            return isOkToAdd;
        }

        /// <summary>
        /// Create a Region in this Overworld
        /// </summary>
        /// <param name="region">The Region to create</param>
        /// <param name="relativeLocation">The direction this Region lies in relative to the last Region created</param>
        public Boolean CreateRegion(Region region, ECardinalDirection relativeLocation)
        {
            // hold if ok to add
            Boolean isOkToAdd = true;

            // set column
            region.Column = this.Regions.Length > 0 ? this.Regions[this.Regions.Length - 1].Column : 0;

            // set row
            region.Row = this.Regions.Length > 0 ? this.Regions[this.Regions.Length - 1].Row : 0;

            // if a previously added room
            if (this.Regions.Length > 0)
            {
                // select direction
                switch (relativeLocation)
                {
                    case (ECardinalDirection.East):
                        {
                            // set column
                            region.Column++;

                            break;
                        }
                    case (ECardinalDirection.North):
                        {
                            // set row
                            region.Row++;

                            break;
                        }
                    case (ECardinalDirection.South):
                        {
                            // set row
                            region.Row--;

                            break;
                        }
                    case (ECardinalDirection.West):
                        {
                            // set column
                            region.Column--;

                            break;
                        }
                    default: { throw new NotImplementedException(); }
                }
            }

            // itterate regions
            foreach (Region r in this.Regions)
            {
                // if collided
                if ((r.Column == region.Column) &&
                    (r.Row == region.Row))
                {
                    // not ok
                    isOkToAdd = false;

                    break;
                }
            }

            // if ok to add
            if (isOkToAdd)
            {
                // add region
                this.regions.Add(region);
            }

            // return result
            return isOkToAdd;
        }

        /// <summary>
        /// Move to a specified Region
        /// </summary>
        /// <param name="regionName">The name of the Region to move to</param>
        /// <returns>If a move was sucsessful</returns>
        public Boolean MoveRegion(String regionName)
        {
            // get matches of name
            IEnumerable<Region> matches = this.Regions.Where<Region>((Region r) => r.Name == regionName);

            // move region
            if (matches.Count<Region>() > 0)
            {
                // move region
                return this.MoveRegion(matches.ElementAt<Region>(0));
            }
            else
            {
                // fail
                return false;
            }
        }

        /// <summary>
        /// Move to a different Region
        /// </summary>
        /// <param name="region">The Region to move to</param>
        /// <returns>If a move was sucsessful</returns>
        public Boolean MoveRegion(Region region)
        {
            // check if contained
            if (this.Regions.Contains<Region>(region))
            {
                // hold direction
                ECardinalDirection direction;

                // if direction could be gotten
                if (this.TryGetDirectionOfAdjoiningRegion(this.CurrentRegion, region, out direction))
                {
                    // change region
                    this.CurrentRegion = region;

                    // handle movement into
                    this.CurrentRegion.OnMovedInto(direction);
                }
                else
                {
                    // change region
                    this.CurrentRegion = region;

                    // do null, but still moved into
                    this.CurrentRegion.OnMovedInto(null);
                }

                // moved
                return true;
            }
            else
            {
                // fail
                return false;
            }
        }

        /// <summary>
        /// Move to a region in a specified direction
        /// </summary>
        /// <param name="direction">The direction to move in</param>
        /// <returns>If a move was sucsessful</returns>
        public Boolean MoveRegion(ECardinalDirection direction)
        {
            // check region
            if (this.HasAdjoiningRegion(direction))
            {
                // move to the current region
                this.CurrentRegion = this.GetAdjoiningRegion(direction);

                // handle moved into
                this.CurrentRegion.OnMovedInto(direction);

                // pass
                return true;
            }
            else
            {
                // fail
                return false;
            }
        }

        /// <summary>
        /// Get if the Overworld.CurrentRegion prperty has an adjoining Region
        /// </summary>
        /// <param name="direction">The direction to check</param>
        /// <returns>True if there is an adjoining region in the direction specified</returns>
        public virtual Boolean HasAdjoiningRegion(ECardinalDirection direction)
        {
            // select direction
            switch (direction)
            {
                case (ECardinalDirection.East):
                    {
                        // see if there is a region there
                        return this.Regions.Where<Region>((Region r) =>
                            {
                                // check column and row
                                return ((r.Column == this.CurrentRegion.Column + 1) && (r.Row == this.CurrentRegion.Row));
                            }).Count<Region>() > 0;
                    }
                case (ECardinalDirection.North):
                    {
                        // see if there is a region there
                        return this.Regions.Where<Region>((Region r) =>
                        {
                            // check column and row
                            return ((r.Column == this.CurrentRegion.Column ) && (r.Row == this.CurrentRegion.Row + 1));
                        }).Count<Region>() > 0;
                    }
                case (ECardinalDirection.South):
                    {
                        // see if there is a region there
                        return this.Regions.Where<Region>((Region r) =>
                        {
                            // check column and row
                            return ((r.Column == this.CurrentRegion.Column ) && (r.Row == this.CurrentRegion.Row - 1));
                        }).Count<Region>() > 0;
                    }
                case (ECardinalDirection.West):
                    {
                        // see if there is a region there
                        return this.Regions.Where<Region>((Region r) =>
                        {
                            // check column and row
                            return ((r.Column == this.CurrentRegion.Column - 1) && (r.Row == this.CurrentRegion.Row));
                        }).Count<Region>() > 0;
                    }
                default: { throw new NotImplementedException(); }
            }
        }

        /// <summary>
        /// Get an adjoining Region
        /// </summary>
        /// <param name="direction">The direction of the adjoining Region</param>
        /// <returns>The adjoining Region, if there is one</returns>
        public virtual Region GetAdjoiningRegion(ECardinalDirection direction)
        {
            // if a move is valid
            if (this.HasAdjoiningRegion(direction))
            {
                // hold column to look for
                Int32 columnToLookFor;

                // hold row to look for
                Int32 rowToLookFor;

                // select direction
                switch (direction)
                {
                    case (ECardinalDirection.East):
                        {
                            // set column
                            columnToLookFor = this.CurrentRegion.Column + 1;

                            // set row
                            rowToLookFor = this.CurrentRegion.Row;

                            break;
                        }
                    case (ECardinalDirection.North):
                        {
                            // set column
                            columnToLookFor = this.CurrentRegion.Column;

                            // set row
                            rowToLookFor = this.CurrentRegion.Row + 1;

                            break;
                        }
                    case (ECardinalDirection.South):
                        {
                            // set column
                            columnToLookFor = this.CurrentRegion.Column;

                            // set row
                            rowToLookFor = this.CurrentRegion.Row - 1;

                            break;
                        }
                    case (ECardinalDirection.West):
                        {
                            // set column
                            columnToLookFor = this.CurrentRegion.Column - 1;

                            // set row
                            rowToLookFor = this.CurrentRegion.Row;

                            break;
                        }
                    default: { throw new NotImplementedException(); }
                }

                // itterate all regions
                foreach (Region region in this.Regions)
                {
                    // if found
                    if ((region.Column == columnToLookFor) &&
                        (region.Row == rowToLookFor))
                    {
                        // return the region
                        return region;
                    }
                }

                // no region found
                return null;
            }
            else
            {
                // no region
                return null;
            }
        }

        /// <summary>
        /// Set the Region to start in
        /// </summary>
        /// <param name="region">The Region to start in</param>
        public void SetStartRegion(Region region)
        {
            // set region
            this.CurrentRegion = region;
        }

        /// <summary>
        /// Set the Region to start in
        /// </summary>
        /// <param name="index">The index of Region to start in</param>
        public void SetStartRegion(Int32 index)
        {
            // set region
            this.CurrentRegion = this.Regions[index];
        }

        /// <summary>
        /// Handle examination this Overworld
        /// </summary>
        /// <returns>The result of this examination</returns>
        protected override ExaminationResult OnExamined()
        {
            return new ExaminationResult(this.Description.GetDescription());
        }

        /// <summary>
        /// Try and get the direction of an adjoining region
        /// </summary>
        /// <param name="sourceRegion">The source region</param>
        /// <param name="destinationRegion">The destination region</param>
        /// <param name="direction">The direction the destinationRegion lies in relative to the sourceRegion</param>
        /// <returns>True if the Region's connect, false if they don't connect</returns>
        public Boolean TryGetDirectionOfAdjoiningRegion(Region sourceRegion, Region destinationRegion, out ECardinalDirection direction)
        {
            // if either region is null
            if ((sourceRegion == null) || (destinationRegion == null))
            {
                // set default
                direction = ECardinalDirection.North;

                // fail
                return false;
            }

            // check all directions
            if ((sourceRegion.Column == destinationRegion.Column) && (sourceRegion.Row == destinationRegion.Row - 1))
            {
                // north
                direction = ECardinalDirection.North;

                // pass
                return true;
            }
            else if ((sourceRegion.Column == destinationRegion.Column) && (sourceRegion.Row == destinationRegion.Row + 1))
            {
                // south
                direction = ECardinalDirection.South;

                // pass
                return true;
            }
            else if ((sourceRegion.Column == destinationRegion.Column - 1) && (sourceRegion.Row == destinationRegion.Row))
            {
                // east
                direction = ECardinalDirection.East;

                // pass
                return true;
            }
            else if ((sourceRegion.Column == destinationRegion.Column + 1) && (sourceRegion.Row == destinationRegion.Row))
            {
                // west
                direction = ECardinalDirection.West;

                // pass
                return true;
            }
            else
            {
                // default
                direction = ECardinalDirection.North;

                // fail
                return false;
            }
        }

        /// <summary>
        /// Register all child properties of this Overworld that are ITransferableDelegation
        /// </summary>
        /// <param name="children">A list containing all the ITransferableDelegation properties of this Overworld</param>
        protected override void OnRegisterTransferableChildren(ref List<ITransferableDelegation> children)
        {
            // itterate all regions
            foreach (Region r in this.Regions)
            {
                // add
                children.Add(r);

                // register children
                r.RegisterTransferableChildren(ref children);
            }

            base.OnRegisterTransferableChildren(ref children);
        }

        #region XmlSerialization

        /// <summary>
        /// Handle writing of Xml for this Overworld
        /// </summary>
        /// <param name="writer">The XmlWriter to write Xml with</param>
        protected override void OnWriteXml(System.Xml.XmlWriter writer)
        {
            // write start element
            writer.WriteStartElement("Overworld");

            // write current region
            writer.WriteAttributeString("CurrentRegion", this.CurrentRegion.Name);
            
            // write regions
            writer.WriteStartElement("Regions");
            
            // itterate all regions
            for (Int32 index = 0; index < this.Regions.Length; index++)
            {
                // write
                this.Regions[index].WriteXml(writer);
            }

            // write end regions
            writer.WriteEndElement();

            // write base
            base.OnWriteXml(writer);

            // write end overworld
            writer.WriteEndElement();
        }

        /// <summary>
        /// Handle reading of Xml for this Overworld
        /// </summary>
        /// <param name="node">The node to read Xml from</param>
        protected override void OnReadXmlNode(System.Xml.XmlNode node)
        {
            // get regions
            XmlNode regionsNode = XMLSerializableObject.GetNode(node, "Regions");

            // itterate regions
            for (Int32 index = 0; index < regionsNode.ChildNodes.Count; index++)
            {
                // get region node
                XmlNode regionElementNode = regionsNode.ChildNodes[index];

                // setup region
                this.regions[index].ReadXmlNode(regionsNode.ChildNodes[index]);
            }

            // get current region
            Region[] regions = this.regions.Where<Region>((Region r) => r.Name == XMLSerializableObject.GetAttribute(node, "CurrentRegion").Value).ToArray<Region>();

            // set current to first match
            this.CurrentRegion = regions[0];

            // read base
            base.OnReadXmlNode(XMLSerializableObject.GetNode(node, "GameLocation"));
        }

        #endregion

        #endregion
    }
}
