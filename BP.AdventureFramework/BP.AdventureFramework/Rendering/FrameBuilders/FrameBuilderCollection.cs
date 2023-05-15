namespace BP.AdventureFramework.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a collection of all of the frame builders required to run a game.
    /// </summary>
    public class FrameBuilderCollection
    {
        #region Properties

        /// <summary>
        /// Get the builder to use for title frames.
        /// </summary>
        public ITitleFrameBuilder TitleFrameBuilder { get; }

        /// <summary>
        /// Get the builder to use for scene frames.
        /// </summary>
        public ISceneFrameBuilder SceneFrameBuilder { get; }

        /// <summary>
        /// Get the builder to use for region map frames.
        /// </summary>
        public IRegionMapFrameBuilder RegionMapFrameBuilder { get; }

        /// <summary>
        /// Get the builder to use for help frames.
        /// </summary>
        public IHelpFrameBuilder HelpFrameBuilder { get; }

        /// <summary>
        /// Get the builder to use for completion frames.
        /// </summary>
        public ICompletionFrameBuilder CompletionFrameBuilder { get; }

        /// <summary>
        /// Get the builder to use for game over frames.
        /// </summary>
        public IGameOverFrameBuilder GameOverFrameBuilder { get; }

        /// <summary>
        /// Get the builder to use for about frames.
        /// </summary>
        public IAboutFrameBuilder AboutFrameBuilder { get; }

        /// <summary>
        /// Get the builder to use for transition frames.
        /// </summary>
        public ITransitionFrameBuilder TransitionFrameBuilder { get; }

        /// <summary>
        /// Get the builder to use for conversation frames.
        /// </summary>
        public IConversationFrameBuilder ConversationFrameBuilder { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the FrameBuilderCollection class.
        /// </summary>
        /// <param name="titleFrameBuilder">The builder to use for building title frames.</param>
        /// <param name="sceneFrameBuilder">The builder to use for building scene frames.</param>
        /// <param name="regionMapFrameBuilder">The builder to use for building region map frames.</param>
        /// <param name="helpFrameBuilder">The builder to use for building help frames.</param>
        /// <param name="completionFrameBuilder">The builder to use for building completion frames.</param>
        /// <param name="gameOverFrameBuilder">The builder to use for building game over frames.</param>
        /// <param name="aboutFrameBuilder">The builder to use for building about frames.</param>
        /// <param name="transitionFrameBuilder">The builder to use for building transition frames.</param>
        /// <param name="conversationFrameBuilder">The builder to use for building conversation frames.</param>
        public FrameBuilderCollection(ITitleFrameBuilder titleFrameBuilder, ISceneFrameBuilder sceneFrameBuilder, IRegionMapFrameBuilder regionMapFrameBuilder, IHelpFrameBuilder helpFrameBuilder, ICompletionFrameBuilder completionFrameBuilder, IGameOverFrameBuilder gameOverFrameBuilder, IAboutFrameBuilder aboutFrameBuilder, ITransitionFrameBuilder transitionFrameBuilder, IConversationFrameBuilder conversationFrameBuilder)
        {
            TitleFrameBuilder = titleFrameBuilder;
            SceneFrameBuilder = sceneFrameBuilder;
            RegionMapFrameBuilder = regionMapFrameBuilder;
            HelpFrameBuilder = helpFrameBuilder;
            CompletionFrameBuilder = completionFrameBuilder;
            GameOverFrameBuilder = gameOverFrameBuilder;
            AboutFrameBuilder = aboutFrameBuilder;
            TransitionFrameBuilder = transitionFrameBuilder;
            ConversationFrameBuilder = conversationFrameBuilder;
        }

        #endregion
    }
}
