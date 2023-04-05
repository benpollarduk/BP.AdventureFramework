using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using BP.AdventureFramework.Interaction;

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
            get { return regions.ToArray<Region>(); }
        }

        /// <summary>
        /// Get or set the Regions in this Overworld
        /// </summary>
        private readonly List<Region> regions = new List<Region>();

        /// <summary>
        /// Get the current Region
        /// </summary>
        public Region CurrentRegion
        {
            get { return currentRegion ?? (regions.Count > 0 ? regions[0] : null); }
            protected set { currentRegion = value; }
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
        public Overworld(string name, string description)
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
        public Overworld(string name, Description description)
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
        public bool CreateRegion(Region region, int columnInOverworld, int rowInOverworld)
        {
            // hold if ok to add
            var isOkToAdd = true;

            // set column
            region.Column = columnInOverworld;

            // set row
            region.Row = rowInOverworld;

            // itterate regions
            foreach (var r in Regions)
                // if collided
                if (r.Column == region.Column &&
                    r.Row == region.Row)
                {
                    // not ok
                    isOkToAdd = false;

                    break;
                }

            // if ok to add
            if (isOkToAdd)
                // add region
                regions.Add(region);

            // return result
            return isOkToAdd;
        }

        /// <summary>
        /// Create a Region in this Overworld
        /// </summary>
        /// <param name="region">The Region to create</param>
        /// <param name="relativeLocation">The direction this Region lies in relative to the last Region created</param>
        public bool CreateRegion(Region region, ECardinalDirection relativeLocation)
        {
            // hold if ok to add
            var isOkToAdd = true;

            // set column
            region.Column = Regions.Length > 0 ? Regions[Regions.Length - 1].Column : 0;

            // set row
            region.Row = Regions.Length > 0 ? Regions[Regions.Length - 1].Row : 0;

            // if a previously added room
            if (Regions.Length > 0)
                // select direction
                switch (relativeLocation)
                {
                    case ECardinalDirection.East:
                        {
                            // set column
                            region.Column++;

                            break;
                        }
                    case ECardinalDirection.North:
                        {
                            // set row
                            region.Row++;

                            break;
                        }
                    case ECardinalDirection.South:
                        {
                            // set row
                            region.Row--;

                            break;
                        }
                    case ECardinalDirection.West:
                        {
                            // set column
                            region.Column--;

                            break;
                        }
                    default:
                        {
                            throw new NotImplementedException();
                        }
                }

            // itterate regions
            foreach (var r in Regions)
                // if collided
                if (r.Column == region.Column &&
                    r.Row == region.Row)
                {
                    // not ok
                    isOkToAdd = false;

                    break;
                }

            // if ok to add
            if (isOkToAdd)
                // add region
                regions.Add(region);

            // return result
            return isOkToAdd;
        }

        /// <summary>
        /// Move to a specified Region
        /// </summary>
        /// <param name="regionName">The name of the Region to move to</param>
        /// <returns>If a move was sucsessful</returns>
        public bool MoveRegion(string regionName)
        {
            // get matches of name
            var matches = Regions.Where(r => r.Name == regionName);

            // move region
            if (matches.Count() > 0)
                // move region
                return MoveRegion(matches.ElementAt(0));
            return false;
        }

        /// <summary>
        /// Move to a different Region
        /// </summary>
        /// <param name="region">The Region to move to</param>
        /// <returns>If a move was sucsessful</returns>
        public bool MoveRegion(Region region)
        {
            // check if contained
            if (Regions.Contains(region))
            {
                // hold direction
                ECardinalDirection direction;

                // if direction could be gotten
                if (TryGetDirectionOfAdjoiningRegion(CurrentRegion, region, out direction))
                {
                    // change region
                    CurrentRegion = region;

                    // handle movement into
                    CurrentRegion.OnMovedInto(direction);
                }
                else
                {
                    // change region
                    CurrentRegion = region;

                    // do null, but still moved into
                    CurrentRegion.OnMovedInto(null);
                }

                // moved
                return true;
            }

            // fail
            return false;
        }

        /// <summary>
        /// Move to a region in a specified direction
        /// </summary>
        /// <param name="direction">The direction to move in</param>
        /// <returns>If a move was sucsessful</returns>
        public bool MoveRegion(ECardinalDirection direction)
        {
            // check region
            if (HasAdjoiningRegion(direction))
            {
                // move to the current region
                CurrentRegion = GetAdjoiningRegion(direction);

                // handle moved into
                CurrentRegion.OnMovedInto(direction);

                // pass
                return true;
            }

            // fail
            return false;
        }

        /// <summary>
        /// Get if the Overworld.CurrentRegion prperty has an adjoining Region
        /// </summary>
        /// <param name="direction">The direction to check</param>
        /// <returns>True if there is an adjoining region in the direction specified</returns>
        public virtual bool HasAdjoiningRegion(ECardinalDirection direction)
        {
            // select direction
            switch (direction)
            {
                case ECardinalDirection.East:
                    {
                        // see if there is a region there
                        return Regions.Where(r =>
                        {
                            // check column and row
                            return r.Column == CurrentRegion.Column + 1 && r.Row == CurrentRegion.Row;
                        }).Count() > 0;
                    }
                case ECardinalDirection.North:
                    {
                        // see if there is a region there
                        return Regions.Where(r =>
                        {
                            // check column and row
                            return r.Column == CurrentRegion.Column && r.Row == CurrentRegion.Row + 1;
                        }).Count() > 0;
                    }
                case ECardinalDirection.South:
                    {
                        // see if there is a region there
                        return Regions.Where(r =>
                        {
                            // check column and row
                            return r.Column == CurrentRegion.Column && r.Row == CurrentRegion.Row - 1;
                        }).Count() > 0;
                    }
                case ECardinalDirection.West:
                    {
                        // see if there is a region there
                        return Regions.Where(r =>
                        {
                            // check column and row
                            return r.Column == CurrentRegion.Column - 1 && r.Row == CurrentRegion.Row;
                        }).Count() > 0;
                    }
                default:
                    {
                        throw new NotImplementedException();
                    }
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
            if (HasAdjoiningRegion(direction))
            {
                // hold column to look for
                int columnToLookFor;

                // hold row to look for
                int rowToLookFor;

                // select direction
                switch (direction)
                {
                    case ECardinalDirection.East:
                        {
                            // set column
                            columnToLookFor = CurrentRegion.Column + 1;

                            // set row
                            rowToLookFor = CurrentRegion.Row;

                            break;
                        }
                    case ECardinalDirection.North:
                        {
                            // set column
                            columnToLookFor = CurrentRegion.Column;

                            // set row
                            rowToLookFor = CurrentRegion.Row + 1;

                            break;
                        }
                    case ECardinalDirection.South:
                        {
                            // set column
                            columnToLookFor = CurrentRegion.Column;

                            // set row
                            rowToLookFor = CurrentRegion.Row - 1;

                            break;
                        }
                    case ECardinalDirection.West:
                        {
                            // set column
                            columnToLookFor = CurrentRegion.Column - 1;

                            // set row
                            rowToLookFor = CurrentRegion.Row;

                            break;
                        }
                    default:
                        {
                            throw new NotImplementedException();
                        }
                }

                // itterate all regions
                foreach (var region in Regions)
                    // if found
                    if (region.Column == columnToLookFor &&
                        region.Row == rowToLookFor)
                        // return the region
                        return region;

                // no region found
                return null;
            }

            // no region
            return null;
        }

        /// <summary>
        /// Set the Region to start in
        /// </summary>
        /// <param name="region">The Region to start in</param>
        public void SetStartRegion(Region region)
        {
            // set region
            CurrentRegion = region;
        }

        /// <summary>
        /// Set the Region to start in
        /// </summary>
        /// <param name="index">The index of Region to start in</param>
        public void SetStartRegion(int index)
        {
            // set region
            CurrentRegion = Regions[index];
        }

        /// <summary>
        /// Handle examination this Overworld
        /// </summary>
        /// <returns>The result of this examination</returns>
        protected override ExaminationResult OnExamined()
        {
            return new ExaminationResult(Description.GetDescription());
        }

        /// <summary>
        /// Try and get the direction of an adjoining region
        /// </summary>
        /// <param name="sourceRegion">The source region</param>
        /// <param name="destinationRegion">The destination region</param>
        /// <param name="direction">The direction the destinationRegion lies in relative to the sourceRegion</param>
        /// <returns>True if the Region's connect, false if they don't connect</returns>
        public bool TryGetDirectionOfAdjoiningRegion(Region sourceRegion, Region destinationRegion, out ECardinalDirection direction)
        {
            // if either region is null
            if (sourceRegion == null || destinationRegion == null)
            {
                // set default
                direction = ECardinalDirection.North;

                // fail
                return false;
            }

            // check all directions
            if (sourceRegion.Column == destinationRegion.Column && sourceRegion.Row == destinationRegion.Row - 1)
            {
                // north
                direction = ECardinalDirection.North;

                // pass
                return true;
            }

            if (sourceRegion.Column == destinationRegion.Column && sourceRegion.Row == destinationRegion.Row + 1)
            {
                // south
                direction = ECardinalDirection.South;

                // pass
                return true;
            }

            if (sourceRegion.Column == destinationRegion.Column - 1 && sourceRegion.Row == destinationRegion.Row)
            {
                // east
                direction = ECardinalDirection.East;

                // pass
                return true;
            }

            if (sourceRegion.Column == destinationRegion.Column + 1 && sourceRegion.Row == destinationRegion.Row)
            {
                // west
                direction = ECardinalDirection.West;

                // pass
                return true;
            }

            // default
            direction = ECardinalDirection.North;

            // fail
            return false;
        }

        /// <summary>
        /// Register all child properties of this Overworld that are ITransferableDelegation
        /// </summary>
        /// <param name="children">A list containing all the ITransferableDelegation properties of this Overworld</param>
        protected override void OnRegisterTransferableChildren(ref List<ITransferableDelegation> children)
        {
            // itterate all regions
            foreach (var r in Regions)
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
        protected override void OnWriteXml(XmlWriter writer)
        {
            // write start element
            writer.WriteStartElement("Overworld");

            // write current region
            writer.WriteAttributeString("CurrentRegion", CurrentRegion.Name);

            // write regions
            writer.WriteStartElement("Regions");

            // itterate all regions
            for (var index = 0; index < Regions.Length; index++)
                // write
                Regions[index].WriteXml(writer);

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
        protected override void OnReadXmlNode(XmlNode node)
        {
            // get regions
            var regionsNode = GetNode(node, "Regions");

            // itterate regions
            for (var index = 0; index < regionsNode.ChildNodes.Count; index++)
            {
                // get region node
                var regionElementNode = regionsNode.ChildNodes[index];

                // setup region
                this.regions[index].ReadXmlNode(regionsNode.ChildNodes[index]);
            }

            // get current region
            var regions = this.regions.Where(r => r.Name == GetAttribute(node, "CurrentRegion").Value).ToArray();

            // set current to first match
            CurrentRegion = regions[0];

            // read base
            base.OnReadXmlNode(GetNode(node, "GameLocation"));
        }

        #endregion

        #endregion
    }
}