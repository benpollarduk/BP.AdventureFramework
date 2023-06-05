using System;
using System.Collections.Generic;
using System.Linq;
using BP.AdventureFramework.Assets.Locations;

namespace BP.AdventureFramework.Rendering.FrameBuilders.Grid.Color
{
    /// <summary>
    /// Provides a color room map builder.
    /// </summary>
    public sealed class ColorRoomMapBuilder : IRoomMapBuilder
    {
        #region Properties

        /// <summary>
        /// Get or set the character used for representing a locked exit.
        /// </summary>
        public char LockedExit { get; set; } = Convert.ToChar("x");

        /// <summary>
        /// Get or set the character used for representing there is an item or a character in the room.
        /// </summary>
        public char ItemOrCharacterInRoom { get; set; } = Convert.ToChar("?");

        /// <summary>
        /// Get or set the character to use for vertical boundaries.
        /// </summary>
        public char VerticalBoundary { get; set; } = Convert.ToChar("|");

        /// <summary>
        /// Get or set the character to use for horizontal boundaries.
        /// </summary>
        public char HorizontalBoundary { get; set; } = Convert.ToChar("-");

        /// <summary>
        /// Get or set the character to use for vertical exit borders.
        /// </summary>
        public char VerticalExitBorder { get; set; } = Convert.ToChar("|");

        /// <summary>
        /// Get or set the character to use for horizontal exit borders.
        /// </summary>
        public char HorizontalExitBorder { get; set; } = Convert.ToChar("-");

        /// <summary>
        /// Get or set the character to use for corners.
        /// </summary>
        public char Corner { get; set; } = Convert.ToChar("+");

        /// <summary>
        /// Get or set the padding between the key and the map.
        /// </summary>
        public int KeyPadding { get; set; } = 6;
        
        /// <summary>
        /// Get or set the room boundary color.
        /// </summary>
        public RenderColor BoundaryColor { get; set; } = RenderColor.DarkGray;

        /// <summary>
        /// Get or set the item or character color.
        /// </summary>
        public RenderColor ItemOrCharacterColor { get; set; } = RenderColor.Blue;

        /// <summary>
        /// Get or set the locked exit color.
        /// </summary>
        public RenderColor LockedExitColor { get; set; } = RenderColor.Red;

        /// <summary>
        /// Get or set the visited exit color.
        /// </summary>
        public RenderColor VisitedExitColor { get; set; } = RenderColor.Yellow;

        /// <summary>
        /// Get or set the unvisited exit color.
        /// </summary>
        public RenderColor UnvisitedExitColor { get; set; } = RenderColor.Green;

        #endregion

        #region Methods

        /// <summary>
        /// Draw the north border.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="viewPoint">The viewpoint from the room.</param>
        /// <param name="gridStringBuilder">The builder to use for the map.</param>
        /// <param name="startX">The start position, x.</param>
        /// <param name="startY">The start position, x.</param>
        private void DrawNorthBorder(Room room, ViewPoint viewPoint, GridStringBuilder gridStringBuilder, int startX, int startY)
        {
            gridStringBuilder.SetCell(startX, startY, Corner, BoundaryColor);
            gridStringBuilder.SetCell(startX + 1, startY, HorizontalBoundary, BoundaryColor);

            if (room.HasLockedExitInDirection(Direction.North))
            {
                gridStringBuilder.SetCell(startX + 2, startY, VerticalExitBorder, BoundaryColor);
                gridStringBuilder.SetCell(startX + 4, startY, LockedExit, LockedExitColor);
                gridStringBuilder.SetCell(startX + 6, startY, VerticalExitBorder, BoundaryColor);
            }
            else if (room.HasUnlockedExitInDirection(Direction.North))
            {
                gridStringBuilder.SetCell(startX + 2, startY, VerticalExitBorder, BoundaryColor);

                if (viewPoint[Direction.North]?.HasBeenVisited ?? false)
                    gridStringBuilder.SetCell(startX + 4, startY, Convert.ToChar("n"), VisitedExitColor);
                else
                    gridStringBuilder.SetCell(startX + 4, startY, Convert.ToChar("N"), UnvisitedExitColor);

                gridStringBuilder.SetCell(startX + 6, startY, VerticalExitBorder, BoundaryColor);
            }
            else
            {
                gridStringBuilder.SetCell(startX + 2, startY, HorizontalBoundary, BoundaryColor);
                gridStringBuilder.SetCell(startX + 3, startY, HorizontalBoundary, BoundaryColor);
                gridStringBuilder.SetCell(startX + 4, startY, HorizontalBoundary, BoundaryColor);
                gridStringBuilder.SetCell(startX + 5, startY, HorizontalBoundary, BoundaryColor);
                gridStringBuilder.SetCell(startX + 6, startY, HorizontalBoundary, BoundaryColor);
            }

            gridStringBuilder.SetCell(startX + 7, startY, HorizontalBoundary, BoundaryColor);
            gridStringBuilder.SetCell(startX + 8, startY, Corner, BoundaryColor);
        }

        /// <summary>
        /// Draw the south border.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="viewPoint">The viewpoint from the room.</param>
        /// <param name="gridStringBuilder">The builder to use for the map.</param>
        /// <param name="startX">The start position, x.</param>
        /// <param name="startY">The start position, x.</param>
        private void DrawSouthBorder(Room room, ViewPoint viewPoint, GridStringBuilder gridStringBuilder, int startX, int startY)
        {
            gridStringBuilder.SetCell(startX, startY + 6, Corner, BoundaryColor);
            gridStringBuilder.SetCell(startX + 1, startY + 6, HorizontalBoundary, BoundaryColor);

            if (room.HasLockedExitInDirection(Direction.South))
            {
                gridStringBuilder.SetCell(startX + 2, startY + 6, VerticalExitBorder, BoundaryColor);
                gridStringBuilder.SetCell(startX + 4, startY + 6, LockedExit, LockedExitColor);
                gridStringBuilder.SetCell(startX + 6, startY + 6, VerticalExitBorder, BoundaryColor);
            }
            else if (room.HasUnlockedExitInDirection(Direction.South))
            {
                gridStringBuilder.SetCell(startX + 2, startY + 6, VerticalExitBorder, BoundaryColor);

                if (viewPoint[Direction.South]?.HasBeenVisited ?? false)
                    gridStringBuilder.SetCell(startX + 4, startY + 6, Convert.ToChar("s"), VisitedExitColor);
                else
                    gridStringBuilder.SetCell(startX + 4, startY + 6, Convert.ToChar("S"), UnvisitedExitColor);

                gridStringBuilder.SetCell(startX + 6, startY + 6, VerticalExitBorder, BoundaryColor);
            }
            else
            {
                gridStringBuilder.SetCell(startX + 2, startY + 6, HorizontalBoundary, BoundaryColor);
                gridStringBuilder.SetCell(startX + 3, startY + 6, HorizontalBoundary, BoundaryColor);
                gridStringBuilder.SetCell(startX + 4, startY + 6, HorizontalBoundary, BoundaryColor);
                gridStringBuilder.SetCell(startX + 5, startY + 6, HorizontalBoundary, BoundaryColor);
                gridStringBuilder.SetCell(startX + 6, startY + 6, HorizontalBoundary, BoundaryColor);
            }

            gridStringBuilder.SetCell(startX + 7, startY + 6, HorizontalBoundary, BoundaryColor);
            gridStringBuilder.SetCell(startX + 8, startY + 6, Corner, BoundaryColor);
        }

        /// <summary>
        /// Draw the east border.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="viewPoint">The viewpoint from the room.</param>
        /// <param name="gridStringBuilder">The builder to use for the map.</param>
        /// <param name="startX">The start position, x.</param>
        /// <param name="startY">The start position, x.</param>
        private void DrawEastBorder(Room room, ViewPoint viewPoint, GridStringBuilder gridStringBuilder, int startX, int startY)
        {
            gridStringBuilder.SetCell(startX + 8, startY + 1, VerticalBoundary, BoundaryColor);

            if (room.HasLockedExitInDirection(Direction.East))
            {
                gridStringBuilder.SetCell(startX + 8, startY + 2, HorizontalExitBorder, BoundaryColor);
                gridStringBuilder.SetCell(startX + 8, startY + 3, LockedExit, LockedExitColor);
                gridStringBuilder.SetCell(startX + 8, startY + 4, HorizontalExitBorder, BoundaryColor);
            }
            else if (room.HasUnlockedExitInDirection(Direction.East))
            {
                gridStringBuilder.SetCell(startX + 8, startY + 2, HorizontalExitBorder, BoundaryColor);

                if (viewPoint[Direction.East]?.HasBeenVisited ?? false)
                    gridStringBuilder.SetCell(startX + 8, startY + 3, Convert.ToChar("e"), VisitedExitColor);
                else
                    gridStringBuilder.SetCell(startX + 8, startY + 3, Convert.ToChar("E"), UnvisitedExitColor);

                gridStringBuilder.SetCell(startX + 8, startY + 4, HorizontalExitBorder, BoundaryColor);
            }
            else
            {
                gridStringBuilder.SetCell(startX + 8, startY + 2, VerticalBoundary, BoundaryColor);
                gridStringBuilder.SetCell(startX + 8, startY + 3, VerticalBoundary, BoundaryColor);
                gridStringBuilder.SetCell(startX + 8, startY + 4, VerticalBoundary, BoundaryColor);
            }

            gridStringBuilder.SetCell(startX + 8, startY + 5, VerticalExitBorder, BoundaryColor);
        }

        /// <summary>
        /// Draw the west border.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="viewPoint">The viewpoint from the room.</param>
        /// <param name="gridStringBuilder">The builder to use for the map.</param>
        /// <param name="startX">The start position, x.</param>
        /// <param name="startY">The start position, x.</param>
        private void DrawWestBorder(Room room, ViewPoint viewPoint, GridStringBuilder gridStringBuilder, int startX, int startY)
        {
            gridStringBuilder.SetCell(startX, startY + 1, VerticalBoundary, BoundaryColor);

            if (room.HasLockedExitInDirection(Direction.West))
            {
                gridStringBuilder.SetCell(startX, startY + 2, HorizontalExitBorder, BoundaryColor);
                gridStringBuilder.SetCell(startX, startY + 3, LockedExit, LockedExitColor);
                gridStringBuilder.SetCell(startX, startY + 4, HorizontalExitBorder, BoundaryColor);
            }
            else if (room.HasUnlockedExitInDirection(Direction.West))
            {
                gridStringBuilder.SetCell(startX, startY + 2, HorizontalExitBorder, BoundaryColor);

                if (viewPoint[Direction.West]?.HasBeenVisited ?? false)
                    gridStringBuilder.SetCell(startX, startY + 3, Convert.ToChar("w"), VisitedExitColor);
                else
                    gridStringBuilder.SetCell(startX, startY + 3, Convert.ToChar("W"), UnvisitedExitColor);

                gridStringBuilder.SetCell(startX, startY + 4, HorizontalExitBorder, BoundaryColor);
            }
            else
            {
                gridStringBuilder.SetCell(startX, startY + 2, VerticalBoundary, BoundaryColor);
                gridStringBuilder.SetCell(startX, startY + 3, VerticalBoundary, BoundaryColor);
                gridStringBuilder.SetCell(startX, startY + 4, VerticalBoundary, BoundaryColor);
            }

            gridStringBuilder.SetCell(startX, startY + 5, VerticalExitBorder, BoundaryColor);
        }

        /// <summary>
        /// Draw the up exit.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="viewPoint">The viewpoint from the room.</param>
        /// <param name="gridStringBuilder">The builder to use for the map.</param>
        /// <param name="startX">The start position, x.</param>
        /// <param name="startY">The start position, x.</param>
        private void DrawUpExit(Room room, ViewPoint viewPoint, GridStringBuilder gridStringBuilder, int startX, int startY)
        {
            if (room.HasLockedExitInDirection(Direction.Up))
            {
                gridStringBuilder.SetCell(startX + 2, startY + 2, LockedExit, LockedExitColor);
            }
            else if (room.HasUnlockedExitInDirection(Direction.Up))
            {
                if (viewPoint[Direction.Up]?.HasBeenVisited ?? false)
                    gridStringBuilder.SetCell(startX + 2, startY + 2, Convert.ToChar("u"), VisitedExitColor);
                else
                    gridStringBuilder.SetCell(startX + 2, startY + 2, Convert.ToChar("U"), UnvisitedExitColor);
            }
        }

        /// <summary>
        /// Draw the down exit.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="viewPoint">The viewpoint from the room.</param>
        /// <param name="gridStringBuilder">The builder to use for the map.</param>
        /// <param name="startX">The start position, x.</param>
        /// <param name="startY">The start position, x.</param>
        private void DrawDownExit(Room room, ViewPoint viewPoint, GridStringBuilder gridStringBuilder, int startX, int startY)
        {
            if (room.HasLockedExitInDirection(Direction.Down))
            {
                gridStringBuilder.SetCell(startX + 6, startY + 2, LockedExit, LockedExitColor);
            }
            else if (room.HasUnlockedExitInDirection(Direction.Down))
            {
                if (viewPoint[Direction.Down]?.HasBeenVisited ?? false)
                    gridStringBuilder.SetCell(startX + 6, startY + 2, Convert.ToChar("d"), VisitedExitColor);
                else
                    gridStringBuilder.SetCell(startX + 6, startY + 2, Convert.ToChar("D"), UnvisitedExitColor);
            }
        }

        /// <summary>
        /// Draw the item or character.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="gridStringBuilder">The builder to use for the map.</param>
        /// <param name="startX">The start position, x.</param>
        /// <param name="startY">The start position, x.</param>
        private void DrawItemOrCharacter(Room room, GridStringBuilder gridStringBuilder, int startX, int startY)
        {
            if (room.Items.Any(x => x.IsPlayerVisible) || room.Characters.Any(x => x.IsPlayerVisible))
                gridStringBuilder.SetCell(startX + 4, startY + 3, ItemOrCharacterInRoom, ItemOrCharacterColor);
        }

        /// <summary>
        /// Draw the key.
        /// </summary>
        /// <param name="room">The room.</param>
        /// <param name="viewPoint">The viewpoint from the room.</param>
        /// <param name="key">The key type.</param>
        /// <param name="gridStringBuilder">The builder to use for the map.</param>
        /// <param name="startX">The start position, x.</param>
        /// <param name="startY">The start position, x.</param>
        /// <param name="endX">The end position, x.</param>
        /// <param name="endY">The end position, x.</param>
        private void DrawKey(Room room, ViewPoint viewPoint, KeyType key, GridStringBuilder gridStringBuilder, int startX, int startY, out int endX, out int endY)
        {
            var keyLines = new Dictionary<string, RenderColor>();
            var lockedExitString = $"{LockedExit} = Locked Exit";
            var notVisitedExitString = "N/E/S/W/U/D = Unvisited";
            var visitedExitString = "n/e/s/w/u/d = Visited";
            var itemsString = $"{ItemOrCharacterInRoom} = Item(s) or Character(s) in Room";

            switch (key)
            {
                case KeyType.Dynamic:

                    if (room.Exits.Where(x => x.IsPlayerVisible).Any(x => x.IsLocked))
                        keyLines.Add(lockedExitString, LockedExitColor);

                    if (viewPoint.AnyNotVisited)
                        keyLines.Add(notVisitedExitString, UnvisitedExitColor);

                    if (viewPoint.AnyVisited)
                        keyLines.Add(visitedExitString, VisitedExitColor);

                    if (room.EnteredFrom.HasValue)
                        keyLines.Add($"{room.EnteredFrom.Value.ToString().ToLower().Substring(0, 1)} = Entrance", VisitedExitColor);

                    if (room.Items.Any(x => x.IsPlayerVisible) || room.Characters.Any(x => x.IsPlayerVisible))
                        keyLines.Add(itemsString, ItemOrCharacterColor);

                    break;

                case KeyType.Full:

                    keyLines.Add(lockedExitString, LockedExitColor);
                    keyLines.Add(notVisitedExitString, UnvisitedExitColor);
                    keyLines.Add(visitedExitString, VisitedExitColor);
                    keyLines.Add(itemsString, ItemOrCharacterColor);

                    break;

                case KeyType.None:
                    break;
                default:
                    throw new NotImplementedException();
            }

            endX = startX + 8;
            endY = startY;

            if (!keyLines.Any())
                return;

            var startKeyX = endX + KeyPadding;
            var maxWidth = keyLines.Max(x => x.Key.Length) + startKeyX + 1;

            foreach (var keyLine in keyLines)
                gridStringBuilder.DrawWrapped(keyLine.Key, startKeyX, endY + 1, maxWidth, keyLine.Value, out endX, out endY);

        }

        #endregion

        #region Implementation of IRoomMapBuilder

        /// <summary>
        /// Build a map for a room.
        /// </summary>
        /// <param name="gridStringBuilder">The string builder to use.</param>
        /// <param name="room">The room.</param>
        /// <param name="viewPoint">The viewpoint from the room.</param>
        /// <param name="key">The key type.</param>
        /// <param name="startX">The start position, x.</param>
        /// <param name="startY">The start position, x.</param>
        /// <param name="endX">The end position, x.</param>
        /// <param name="endY">The end position, x.</param>
        public void BuildRoomMap(GridStringBuilder gridStringBuilder, Room room, ViewPoint viewPoint, KeyType key, int startX, int startY, out int endX, out int endY)
        {
            /*
             * *-| N |-*
             * |       |
             * - U   D -
             * W   ?   E
             * -       -
             * |       |
             * *-| S |-*
             */

            DrawNorthBorder(room, viewPoint, gridStringBuilder, startX, startY);
            DrawSouthBorder(room, viewPoint, gridStringBuilder, startX, startY);
            DrawEastBorder(room, viewPoint, gridStringBuilder, startX, startY);
            DrawWestBorder(room, viewPoint, gridStringBuilder, startX, startY);
            DrawUpExit(room, viewPoint, gridStringBuilder, startX, startY);
            DrawDownExit(room, viewPoint, gridStringBuilder, startX, startY);
            DrawItemOrCharacter(room, gridStringBuilder, startX, startY);
            DrawKey(room, viewPoint, key, gridStringBuilder, startX, startY, out endX, out endY);

            if (endY < startY + 6)
                endY = startY + 6;
        }

        #endregion
    }
}
