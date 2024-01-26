namespace BP.AdventureFramework.Assets.Interaction
{
    /// <summary>
    /// Represents the callback for interacting with objects.
    /// </summary>
    /// <param name="item">The item to interact with.</param>
    /// <returns>The result of the interaction.</returns>
    public delegate InteractionResult InteractionCallback(Item item);
}