using System;
using System.Collections.Generic;
using System.Linq;
using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Locations;

namespace BP.AdventureFramework.Utils.Generation.Simple
{
    /// <summary>
    /// Provides a region generator.
    /// </summary>
    internal sealed class RegionGenerator : IRegionGenerator
    {
        #region StaticMethods

        /// <summary>
        /// Generate items.
        /// </summary>
        /// <param name="generator">The generator.</param>
        /// <param name="itemGenerator">The item generator.</param>
        /// <param name="options">The generation options.</param>
        /// <param name="roomCount">The number of rooms in the region.</param>
        /// <returns>The generated items.</returns>
        internal static Item[] GenerateItems(Random generator, IItemGenerator itemGenerator, GameGenerationOptions options, int roomCount)
        {
            var count = Math.Ceiling(roomCount * options.RoomToItemRatio);
            var items = new List<Item>();

            for (var i = 0; i < count; i++)
            {
                var item = itemGenerator.Generate(generator);

                if (item == null)
                    continue;

                items.Add(item);
            }
                

            return items.ToArray();
        }

        /// <summary>
        /// Generate items.
        /// </summary>
        /// <param name="generator">The generator.</param>
        /// <param name="rooms">The rooms.</param>
        /// <param name="items">The items.</param>
        internal static void PopulateRooms(Random generator, Room[] rooms, Item[] items)
        {
            if (items == null || items.Length == 0 || rooms == null || rooms.Length == 0)
                return;

            foreach (var item in items)
            {
                var roomIndex = generator.Next(0, rooms.Length);
                rooms[roomIndex].AddItem(item);
            }
        }

        #endregion

        #region Implementation of IRegionGenerator

        /// <summary>
        /// Generate a region.
        /// </summary>
        /// <param name="generator">The generator.</param>
        /// <param name="roomGenerator">The room generator.</param>
        /// <param name="takeableItemGenerator">The item generator for takeable items.</param>
        /// <param name="nonTakeableItemGenerator">The item generator for non-takeable items.</param>
        /// <param name="options">The generation options.</param>
        /// <returns>The generated region maker.</returns>
        public RegionMaker GenerateRegion(Random generator, IRoomGenerator roomGenerator, IItemGenerator takeableItemGenerator, IItemGenerator nonTakeableItemGenerator, GameGenerationOptions options)
        {
            var regionMaker = new RegionMaker("Region", "Generated Region.");
            roomGenerator.GenerateRooms(regionMaker, generator, options);

            if (takeableItemGenerator != null)
            {
                var items = GenerateItems(generator, takeableItemGenerator, options, regionMaker.GetRoomPositions().Length);
                PopulateRooms(generator, regionMaker.GetRoomPositions().Select(x => x.Room).ToArray(), items);
            }

            if (nonTakeableItemGenerator != null)
            {
                var items = GenerateItems(generator, nonTakeableItemGenerator, options, regionMaker.GetRoomPositions().Length);
                PopulateRooms(generator, regionMaker.GetRoomPositions().Select(x => x.Room).ToArray(), items);
            }

            return regionMaker;
        }

        #endregion
    }
}
