using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Rendering;
using BP.AdventureFramework.Rendering.FrameBuilders.Appenders;
using BP.AdventureFramework.Rendering.FrameBuilders.Appenders.Legacy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Rendering.FrameBuilders.Appenders.Legacy
{
    [TestClass]
    public class LegacyRoomMapBuilder_Tests
    {
        [TestMethod]
        public void GivenDefaultValues_WhenBuildRoomMap_ThenNotEmptyOrNull()
        {
            var builder = new LegacyRoomMapBuilder();
            var room = new Room(string.Empty, string.Empty);

            var result = builder.BuildRoomMap(new LineStringBuilder(), room, ViewPoint.NoView, KeyType.None, 10);

            Assert.IsFalse(string.IsNullOrEmpty(result));
        }
    }
}
