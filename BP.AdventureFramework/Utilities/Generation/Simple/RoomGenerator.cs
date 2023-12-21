﻿using System;
using System.Collections.Generic;
using System.Linq;
using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Extensions;

namespace BP.AdventureFramework.Utilities.Generation.Simple
{
    /// <summary>
    /// Provides a room generator.
    /// </summary>
    internal sealed class RoomGenerator : IRoomGenerator
    {
        #region Properties

        /// <summary>
        /// Get or set the examinable generator.
        /// </summary>
        private IExaminableGenerator ExaminableGenerator { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the RoomGenerator class.
        /// <param name="examinableGenerator">A generator to use for generating examinables.</param>
        /// </summary>
        public RoomGenerator(IExaminableGenerator examinableGenerator)
        {
            ExaminableGenerator = examinableGenerator;
        }

        #endregion

        #region StaticMethods

        /// <summary>
        /// Resolve all required exits for rooms.
        /// </summary>
        /// <param name="roomPositions">The room positions.</param>
        internal static void ResolveRequiredExits(RoomPosition[] roomPositions)
        {
            for (var i = 0; i < roomPositions.Length; i++)
            {
                if (i == 0)
                    continue;

                var last = roomPositions[i - 1];
                var current = roomPositions[i];
                Direction? enteredFrom = null;

                if (last.X < current.X)
                    enteredFrom = Direction.West;
                else if (last.X > current.X)
                    enteredFrom = Direction.East;
                else if (last.Y > current.Y)
                    enteredFrom = Direction.North;
                else if (last.Y < current.Y)
                    enteredFrom = Direction.South;

                if (enteredFrom == null)
                    continue;

                var exitTo = enteredFrom.Value.Inverse();

                if (!last.Room.FindExit(exitTo, true, out _)) 
                    last.Room.AddExit(new Exit(exitTo));

                if (!current.Room.FindExit(enteredFrom.Value, true, out _))
                    current.Room.AddExit(new Exit(enteredFrom.Value));
            }
        }

        /// <summary>
        /// Resolve all possible exits for rooms.
        /// </summary>
        /// <param name="roomPositions">The room positions.</param>
        internal static void ResolveAllPossibleExits(RoomPosition[] roomPositions)
        {
            foreach (var roomPosition in roomPositions)
            {
                foreach (var direction in new[] { Direction.North, Direction.East, Direction.South, Direction.West, Direction.Up, Direction.Down })
                {
                    Region.NextPosition(roomPosition.X, roomPosition.Y, roomPosition.Z, direction, out var nextX, out var nextY, out var nextZ);

                    if (roomPositions.Any(x => x.IsAtPosition(nextX, nextY, nextZ)) && !roomPosition.Room.FindExit(direction, true, out _))
                        roomPosition.Room.AddExit(new Exit(direction));
                }
            }
        }

        /// <summary>
        /// Get the positions that rooms will be laid out in.
        /// </summary>
        /// <param name="count">The number of rooms.</param>
        /// <param name="generator">The generator.</param>
        /// <returns>A collection of room positions.</returns>
        internal static List<RoomPosition> GetRoomPositions(int count, Random generator)
        {
            return GetRoomPositions(count, generator, new RoomPosition[0]);
        }

        /// <summary>
        /// Get the positions that rooms will be laid out in.
        /// </summary>
        /// <param name="count">The number of rooms.</param>
        /// <param name="generator">The generator.</param>
        /// <param name="existingRoomPositions">Any existing room positions.</param>
        /// <returns>A collection of room positions.</returns>
        internal static List<RoomPosition> GetRoomPositions(int count, Random generator, RoomPosition[] existingRoomPositions)
        {
            var lastX = 0;
            var lastY = 0;
            var lastZ = 0;

            var positions = new List<RoomPosition>();
            positions.AddRange(existingRoomPositions);

            for (var i = 0; i < count; i++)
            {
                if (i == 0)
                {
                    // this may be a branch off an existing collection of room
                    if (positions.Any())
                    {
                        // branch off of a random room
                        var last = positions.ElementAt(generator.Next(0, positions.Count));
                        lastX = last.X;
                        lastY = last.Y;
                    }
                    else
                    {
                        // add first room to region at random location
                        lastX = generator.Next(0, count);
                        lastY = generator.Next(0, count);
                        lastZ = generator.Next(0, count);
                    }
                }
                else
                {
                    if (!TryGetNextRoomLocation(lastX, lastY, lastZ, positions, generator, out var x, out var y, out var z))
                        break;

                    lastX = x;
                    lastY = y;
                    lastZ = z;
                }

                positions.Add(new RoomPosition(null, lastX, lastY, lastZ));
            }

            return positions;
        }

        /// <summary>
        /// Try and get the next room location.
        /// </summary>
        /// <param name="startX">The start X location.</param>
        /// <param name="startY">The start Y location.</param>
        /// <param name="startZ">The start Z location.</param>
        /// <param name="positions">The positions.</param>
        /// <param name="generator">The generator.</param>
        /// <param name="x">The next X location.</param>
        /// <param name="y">The next Y location.</param>
        /// <param name="z">The next Z location.</param>
        /// <returns>True if the next room location can be determined, else false.</returns>
        internal static bool TryGetNextRoomLocation(int startX, int startY, int startZ, List<RoomPosition> positions, Random generator, out int x, out int y, out int z)
        {
            var directions = new List<Direction> { Direction.North, Direction.East, Direction.South, Direction.West, Direction.Up, Direction.Down };

            while (directions.Any())
            {
                var nextIndex = generator.Next(0, directions.Count);
                var nextDirection = directions.ElementAt(nextIndex);
                directions.RemoveAt(nextIndex);

                Region.NextPosition(startX, startY, startZ, nextDirection, out var nextX, out var nextY, out var nextZ);

                if (positions.Any(p => p.IsAtPosition(nextX, nextY, nextZ)))
                    continue;

                x = nextX;
                y = nextY;
                z = nextZ;
                return true;
            }

            x = startX;
            y = startY;
            z = startZ;
            return false;
        }

        /// <summary>
        /// Generate a Room.
        /// </summary>
        /// <param name="generator">The generator.</param>
        /// <param name="examinableGenerator">The examinable generator.</param>
        /// <returns>The generated room.</returns>
        internal static Room GenerateRoom(Random generator, IExaminableGenerator examinableGenerator)
        {
            var examinable = examinableGenerator.Generate(generator);
            return new Room(examinable?.Identifier ?? new Identifier(string.Empty), examinable?.Description ?? new Description(string.Empty));
        }

        #endregion

        #region Implementation of IRoomGenerator

        /// <summary>
        /// Generate the rooms.
        /// </summary>
        /// <param name="regionMaker">The region maker.</param>
        /// <param name="generator">The generator.</param>
        /// <param name="options">The game generation options.</param>
        public void GenerateRooms(RegionMaker regionMaker, Random generator, GameGenerationOptions options)
        {
            for (uint i = 1; i <= options.RegionComplexity; i++)
            {
                var min = options.MinimumRooms / options.RegionComplexity;
                var max = options.MaximumRooms / options.RegionComplexity;
                var count = generator.Next((int)min, (int)max);
                var positions = GetRoomPositions(count, generator, regionMaker.GetRoomPositions());

                foreach (var p in positions)
                    regionMaker[p.X, p.Y, p.Z] = GenerateRoom(generator, ExaminableGenerator);
            }

            ResolveAllPossibleExits(regionMaker.GetRoomPositions());
        }

        #endregion
    }
}
