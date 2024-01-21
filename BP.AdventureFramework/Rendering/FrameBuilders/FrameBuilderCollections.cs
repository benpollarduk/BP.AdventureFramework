using BP.AdventureFramework.Rendering.FrameBuilders.Color;

namespace BP.AdventureFramework.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a container from frame builder collections.
    /// </summary>
    public static class FrameBuilderCollections {
        
        /// <summary>
        /// Get the default frame builder collection.
        /// </summary>
        public static FrameBuilderCollection Default
        {
            get
            {
                var gridLayoutBuilder = new GridStringBuilder();

                return new FrameBuilderCollection(
                    new ColorTitleFrameBuilder(gridLayoutBuilder),
                    new ColorSceneFrameBuilder(gridLayoutBuilder, new ColorRoomMapBuilder()), 
                    new ColorRegionMapFrameBuilder(gridLayoutBuilder, new ColorRegionMapBuilder()),
                    new ColorHelpFrameBuilder(gridLayoutBuilder),
                    new ColorCompletionFrameBuilder(gridLayoutBuilder),
                    new ColorGameOverFrameBuilder(gridLayoutBuilder),
                    new ColorAboutFrameBuilder(gridLayoutBuilder),
                    new ColorTransitionFrameBuilder(gridLayoutBuilder),
                    new ColorConversationFrameBuilder(gridLayoutBuilder));
            }
        }
    }
}
