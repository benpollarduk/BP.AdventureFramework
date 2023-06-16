using BP.AdventureFramework.Rendering.FrameBuilders.Appenders;
using BP.AdventureFramework.Rendering.FrameBuilders.Appenders.Legacy;
using BP.AdventureFramework.Rendering.FrameBuilders.Grid;
using BP.AdventureFramework.Rendering.FrameBuilders.Grid.Color;

namespace BP.AdventureFramework.Rendering.FrameBuilders
{
    /// <summary>
    /// Provides a container from frame builder collections.
    /// </summary>
    public static class FrameBuilderCollections
    {
        /// <summary>
        /// Get the legacy frame builder collection.
        /// </summary>
        public static FrameBuilderCollection Legacy
        {
            get
            {
                var stringLayoutBuilder = new LineStringBuilder();

                return new FrameBuilderCollection(
                    new LegacyTitleFrameBuilder(stringLayoutBuilder),
                    new LegacySceneFrameBuilder(stringLayoutBuilder, new LegacyRoomMapBuilder()),
                    new LegacyRegionMapFrameBuilder(stringLayoutBuilder, new LegacyRegionMapBuilder()),
                    new LegacyHelpFrameBuilder(stringLayoutBuilder),
                    new LegacyCompletionFrameBuilder(stringLayoutBuilder), 
                    new LegacyGameOverFrameBuilder(stringLayoutBuilder), 
                    new LegacyAboutFrameBuilder(stringLayoutBuilder),
                    new LegacyTransitionFrameBuilder(stringLayoutBuilder),
                    new LegacyConversationFrameBuilder(stringLayoutBuilder));
            }
        }
        
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
                    new ConsolidatedColorSceneFrameBuilder(gridLayoutBuilder, new ColorRoomMapBuilder()), 
                    new ColorRegionMapFrameBuilder(gridLayoutBuilder, new ColorRegionMapBuilder()),
                    new ColorHelpFrameBuilder(gridLayoutBuilder),
                    new ColorCompletionFrameBuilder(gridLayoutBuilder),
                    new ColorGameOverFrameBuilder(gridLayoutBuilder),
                    new ColorAboutFrameBuilder(gridLayoutBuilder),
                    new ColorTransitionFrameBuilder(gridLayoutBuilder),
                    new ColorConversationFrameBuilder(gridLayoutBuilder));
            }
        }

        /// <summary>
        /// Get the simple frame builder collection.
        /// </summary>
        public static FrameBuilderCollection Simple
        {
            get
            {
                var gridLayoutBuilder = new GridStringBuilder();

                return new FrameBuilderCollection(
                    new ColorTitleFrameBuilder(gridLayoutBuilder),
                    new SimpleColorSceneFrameBuilder(gridLayoutBuilder), 
                    new ColorRegionMapFrameBuilder(gridLayoutBuilder, new ColorRegionMapBuilder()),
                    new ColorHelpFrameBuilder(gridLayoutBuilder),
                    new ColorCompletionFrameBuilder(gridLayoutBuilder),
                    new ColorGameOverFrameBuilder(gridLayoutBuilder),
                    new ColorAboutFrameBuilder(gridLayoutBuilder),
                    new ColorTransitionFrameBuilder(gridLayoutBuilder),
                    new SimpleColorConversationFrameBuilder(gridLayoutBuilder));
            }
        }
    }
}
