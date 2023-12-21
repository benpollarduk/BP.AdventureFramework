namespace BP.AdventureFramework.Assets.Interaction
{
    /// <summary>
    /// Represents any object that can interact with an item.
    /// </summary>
    public interface IInteractWithItem
    {
        /// <summary>
        /// Interact with an item.
        /// </summary>
        /// <param name="item">The item to interact with.</param>
        /// <returns>The result of the interaction.</returns>
        InteractionResult Interact(Item item);
    }
}