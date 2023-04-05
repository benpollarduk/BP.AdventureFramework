namespace BP.AdventureFramework.Interaction
{
    /// <summary>
    /// Represnts any object that can interact with an item
    /// </summary>
    public interface IInteractWithItem
    {
        /// <summary>
        /// Interact with an item
        /// </summary>
        /// <param name="item">The item to interact with</param>
        /// <returns>The result of the interaction</returns>
        InteractionResult Interact(Item item);
    }
}