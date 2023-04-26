using System;
using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Utils.Generation.Simple;

namespace BP.AdventureFramework.Utils.Generation
{
    /// <summary>
    /// Represents a class for generating games.
    /// </summary>
    public sealed class GameGenerator
    {
        #region Properties

        /// <summary>
        /// Get the identifier.
        /// </summary>
        private Identifier Identifier { get; }

        /// <summary>
        /// Get the description.
        /// </summary>
        private Description Description { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the GameGenerator class.
        /// </summary>
        /// <param name="identifier">An identifier for the region.</param>
        /// <param name="description">A description for the region.</param>
        public GameGenerator(string identifier, string description)
        {
        }

        /// <summary>
        /// Initializes a new instance of the OverworldMaker class.
        /// </summary>
        /// <param name="identifier">An identifier for the region.</param>
        /// <param name="description">A description for the region.</param>
        public GameGenerator(Identifier identifier, Description description)
        {
            Identifier = identifier;
            Description = description;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Generate an OverworldMaker.
        /// </summary>
        /// <param name="options">The generation options.</param>
        /// <param name="theme">The theme.</param>
        /// <param name="seed">The seed used for generation.</param>
        /// <returns>The generated overworld maker.</returns>
        public OverworldMaker Generate(GameGenerationOptions options, ITheme theme, out int seed)
        {
            var seedGenerator = new Random();
            seed = seedGenerator.Next(0, int.MaxValue);
            return Generate(seed, options, theme);
        }

        /// <summary>
        /// Generate an OverworldMaker.
        /// </summary>
        /// <param name="seed">The see to use for generation.</param>
        /// <param name="options">The generation options.</param>
        /// <param name="theme">The theme.</param>
        /// <returns>The created overworld maker.</returns>
        public OverworldMaker Generate(int seed, GameGenerationOptions options, ITheme theme)
        {
            return Generate(new Random(seed),
                new RegionGenerator(), 
                new RoomGenerator(new ExaminableGenerator(theme.RoomNouns, theme.RoomAdjectives, new DescriptionGenerator(), false)),
                new ItemGenerator(new ExaminableGenerator(theme.TakeableItemNouns, theme.TakeableItemAdjectives, new DescriptionGenerator(), false), true),
                new ItemGenerator(new ExaminableGenerator(theme.NonTakeableItemNouns, theme.NonTakeableItemAdjectives, new DescriptionGenerator(), false), false),
                options);
        }

        /// <summary>
        /// Generate an OverworldMaker.
        /// </summary>
        /// <param name="generator">The generator.</param>
        /// <param name="regionGenerator">The region generator.</param>
        /// <param name="roomGenerator">The room generator.</param>
        /// <param name="takeableItemGenerator">The item generator for takeable items.</param>
        /// <param name="nonTakeableItemGenerator">The item generator for non-takeable items.</param>
        /// <param name="options">The generation options.</param>
        /// <returns>The generated overworld maker.</returns>
        private OverworldMaker Generate(Random generator, IRegionGenerator regionGenerator, IRoomGenerator roomGenerator, IItemGenerator takeableItemGenerator, IItemGenerator nonTakeableItemGenerator, GameGenerationOptions options)
        {
            var regions = new RegionMaker[generator.Next((int)options.MinimumRegions, (int)options.MaximumRegions)];

            for (var i = 0; i < regions.Length; i++)
                regions[i] = regionGenerator.GenerateRegion(generator, roomGenerator, takeableItemGenerator, nonTakeableItemGenerator, options);

            return new OverworldMaker(Identifier, Description, regions);
        }

        #endregion
    }
}
