namespace BP.AdventureFramework.Utilities
{
    /// <summary>
    /// Represents any object that is a template for an asset.
    /// </summary>
    /// <typeparam name="T">The type of asset being templated.</typeparam>
    public interface IAssetTemplate<out T>
    {
        /// <summary>
        /// Instantiate a new instance of the templated asset.
        /// </summary>
        /// <returns>The asset.</returns>
        T Instantiate();
    }
}
