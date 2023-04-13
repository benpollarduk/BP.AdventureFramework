using System;
using System.Collections.Generic;
using System.Linq;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Interaction;

namespace BP.AdventureFramework.Locations
{
    /// <summary>
    /// Represents an entire overworld.
    /// </summary>
    public class Overworld : GameLocation
    {
        #region Fields

        private Region currentRegion;

        #endregion

        #region Properties

        /// <summary>
        /// Get the Regions in this Overworld.
        /// </summary>
        public List<Region> Regions { get; } = new List<Region>();

        /// <summary>
        /// Get the current Region.
        /// </summary>
        public Region CurrentRegion
        {
            get { return currentRegion ?? (Regions.Count > 0 ? Regions[0] : null); }
            protected set { currentRegion = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Overworld class.
        /// </summary>
        /// <param name="identifier">The identifier for this Overworld.</param>
        /// <param name="description">A description of this Overworld.</param>
        public Overworld(Identifier identifier, Description description)
        {
            Identifier = identifier;
            Description = description;
        }

        /// <summary>
        /// Create a Region in this Overworld.
        /// </summary>
        /// <param name="region">The Region to create.</param>
        /// <param name="columnInOverworld">The column of the Region with this Overworld.</param>
        /// <param name="rowInOverworld">The row of the Region within this Overworld.</param>
        public bool CreateRegion(Region region, int columnInOverworld, int rowInOverworld)
        {
            var addable = true;
            region.Column = columnInOverworld;
            region.Row = rowInOverworld;

            foreach (var r in Regions)
            {
                if (r.Column != region.Column || r.Row != region.Row)
                    continue;

                addable = false;
                break;
            }

            if (addable)
                Regions.Add(region);

            return addable;
        }

        /// <summary>
        /// Create a Region in this Overworld.
        /// </summary>
        /// <param name="region">The Region to create.</param>
        /// <param name="relativeLocation">The direction this Region lies in relative to the last Region created.</param>
        public bool CreateRegion(Region region, CardinalDirection relativeLocation)
        {
            var addable = true;
            region.Column = Regions.Count > 0 ? Regions[Regions.Count - 1].Column : 0;
            region.Row = Regions.Count > 0 ? Regions[Regions.Count - 1].Row : 0;

            if (Regions.Any())
            {
                switch (relativeLocation)
                {
                    case CardinalDirection.East:
                        region.Column++;
                        break;
                    case CardinalDirection.North:
                        region.Row++;
                        break;
                    case CardinalDirection.South:
                        region.Row--;
                        break;
                    case CardinalDirection.West:
                        region.Column--;
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
            
            foreach (var r in Regions)
            {
                if (r.Column != region.Column || r.Row != region.Row) 
                    continue;

                addable = false;
                break;
            }
            if (addable)
                Regions.Add(region);

            return addable;
        }

        /// <summary>
        /// Move to a specified Region.
        /// </summary>
        /// <param name="regionName">The name of the Region to move to.</param>
        /// <returns>If a move was successful.</returns>
        public bool MoveRegion(string regionName)
        {
            var matches = Regions.Where(regionName.EqualsExaminable).ToArray();

            // move region
            if (matches.Any())
                return MoveRegion(matches.ElementAt(0));

            return false;
        }

        /// <summary>
        /// Move to a different Region.
        /// </summary>
        /// <param name="region">The Region to move to.</param>
        /// <returns>If a move was successful</returns>
        public bool MoveRegion(Region region)
        {
            if (!Regions.Contains(region)) 
                return false;

            if (TryGetDirectionOfAdjoiningRegion(CurrentRegion, region, out var direction))
            {
                CurrentRegion = region;
                CurrentRegion.MovedInto(direction);
            }
            else
            {
                CurrentRegion = region;
                CurrentRegion.MovedInto(null);
            }

            return true;
        }

        /// <summary>
        /// Move to a region in a specified direction.
        /// </summary>
        /// <param name="direction">The direction to move in.</param>
        /// <returns>If a move was successful.</returns>
        public bool MoveRegion(CardinalDirection direction)
        {
            if (!HasAdjoiningRegion(direction)) 
                return false;

            CurrentRegion = GetAdjoiningRegion(direction);
            CurrentRegion.MovedInto(direction);
            return true;
        }

        /// <summary>
        /// Get if the Overworld.CurrentRegion property has an adjoining Region.
        /// </summary>
        /// <param name="direction">The direction to check.</param>
        /// <returns>True if there is an adjoining region in the direction specified.</returns>
        public virtual bool HasAdjoiningRegion(CardinalDirection direction)
        {
            switch (direction)
            {
                case CardinalDirection.East:
                    return Regions.Any(r => r.Column == CurrentRegion.Column + 1 && r.Row == CurrentRegion.Row);
                case CardinalDirection.North:
                    return Regions.Any(r => r.Column == CurrentRegion.Column && r.Row == CurrentRegion.Row + 1);
                case CardinalDirection.South:
                    return Regions.Any(r => r.Column == CurrentRegion.Column && r.Row == CurrentRegion.Row - 1);
                case CardinalDirection.West:
                    return Regions.Any(r => r.Column == CurrentRegion.Column - 1 && r.Row == CurrentRegion.Row);
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Get an adjoining Region.
        /// </summary>
        /// <param name="direction">The direction of the adjoining Region.</param>
        /// <returns>The adjoining Region, if there is one.</returns>
        public virtual Region GetAdjoiningRegion(CardinalDirection direction)
        {
            if (!HasAdjoiningRegion(direction)) 
                return null;

            int columnToLookFor;
            int rowToLookFor;

            switch (direction)
            {
                case CardinalDirection.East:
                    columnToLookFor = CurrentRegion.Column + 1;
                    rowToLookFor = CurrentRegion.Row;
                    break;
                case CardinalDirection.North:
                    columnToLookFor = CurrentRegion.Column;
                    rowToLookFor = CurrentRegion.Row + 1;
                    break;
                case CardinalDirection.South:
                    columnToLookFor = CurrentRegion.Column;
                    rowToLookFor = CurrentRegion.Row - 1;
                    break;
                case CardinalDirection.West:
                    columnToLookFor = CurrentRegion.Column - 1;
                    rowToLookFor = CurrentRegion.Row;
                    break;
                default:
                    throw new NotImplementedException();
            }

            foreach (var region in Regions)
            {
                if (region.Column == columnToLookFor &&region.Row == rowToLookFor)
                    return region;
            }

            return null;
        }

        /// <summary>
        /// Set the Region to start in.
        /// </summary>
        /// <param name="region">The Region to start in.</param>
        public void SetStartRegion(Region region)
        {
            CurrentRegion = region;
        }

        /// <summary>
        /// Set the Region to start in.
        /// </summary>
        /// <param name="index">The index of Region to start in.</param>
        public void SetStartRegion(int index)
        {
            CurrentRegion = Regions[index];
        }

        /// <summary>
        /// Handle examination this Overworld.
        /// </summary>
        /// <returns>The result of this examination.</returns>
        public override ExaminationResult Examime()
        {
            return new ExaminationResult(Description.GetDescription());
        }

        /// <summary>
        /// Try and get the direction of an adjoining region.
        /// </summary>
        /// <param name="sourceRegion">The source region.</param>
        /// <param name="destinationRegion">The destination region.</param>
        /// <param name="direction">The direction the destinationRegion lies in relative to the sourceRegion.</param>
        /// <returns>True if the Region's connect, false if they don't connect.</returns>
        public bool TryGetDirectionOfAdjoiningRegion(Region sourceRegion, Region destinationRegion, out CardinalDirection direction)
        {
            if (sourceRegion == null || destinationRegion == null)
            {
                direction = CardinalDirection.North;
                return false;
            }

            if (sourceRegion.Column == destinationRegion.Column && sourceRegion.Row == destinationRegion.Row - 1)
            {
                direction = CardinalDirection.North;
                return true;
            }

            if (sourceRegion.Column == destinationRegion.Column && sourceRegion.Row == destinationRegion.Row + 1)
            {
                direction = CardinalDirection.South;
                return true;
            }

            if (sourceRegion.Column == destinationRegion.Column - 1 && sourceRegion.Row == destinationRegion.Row)
            {
                direction = CardinalDirection.East;
                return true;
            }

            if (sourceRegion.Column == destinationRegion.Column + 1 && sourceRegion.Row == destinationRegion.Row)
            {
                direction = CardinalDirection.West;
                return true;
            }

            direction = CardinalDirection.North;
            return false;
        }

        /// <summary>
        /// Register all child properties of this Overworld that are ITransferableDelegation.
        /// </summary>
        /// <param name="children">A list containing all the ITransferableDelegation properties of this Overworld.</param>
        public override void RegisterTransferableChildren(ref List<ITransferableDelegation> children)
        {
            foreach (var r in Regions)
            {
                children.Add(r);
                r.RegisterTransferableChildren(ref children);
            }

            base.RegisterTransferableChildren(ref children);
        }

        #endregion
    }
}