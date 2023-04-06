namespace BP.AdventureFramework.Rendering
{
    /// <summary>
    /// Enumeration of region map modes.
    /// </summary>
    public enum RegionMapMode
    {
        /// <summary>
        /// Shows rooms at a detailed level.
        /// </summary>
        Detailed = 0,
        /// <summary>
        /// Shows rooms as one character, which allows larger maps to be displayed in a limited area.
        /// </summary>
        Undetailed,
        /// <summary>
        /// Dynamic region map - uses detailed if there is room, else map will be undetailed.
        /// </summary>
        Dynamic
    }
}