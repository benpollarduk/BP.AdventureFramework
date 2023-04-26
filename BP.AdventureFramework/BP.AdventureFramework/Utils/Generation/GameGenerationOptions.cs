namespace BP.AdventureFramework.Utils.Generation
{
    /// <summary>
    /// Provides options for generating games.
    /// </summary>
    public sealed class GameGenerationOptions
    {
        /// <summary>
        /// Get the minimum regions.
        /// </summary>
        public uint MinimumRegions { get; set; } = 1;
        /// <summary>
        /// Get the maximum regions.
        /// </summary>
        public uint MaximumRegions { get; set; } = 1;
        /// <summary>
        /// Get or set the complexity of the regions - higher numbers are more complex, lower numbers are less complex.
        /// </summary>
        public uint RegionComplexity { get; set; } = 5;
        /// <summary>
        /// Get the minimum rooms.
        /// </summary>
        public uint MinimumRooms { get; set; } = 10;
        /// <summary>
        /// Get the maximum rooms.
        /// </summary>
        public uint MaximumRooms { get; set; } = 40;
        /// <summary>
        /// Get the ratio of rooms to items.
        /// </summary>
        public double RoomToItemRatio { get; set; } = 0.5;
    }
}
