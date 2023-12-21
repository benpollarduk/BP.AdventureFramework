using BP.AdventureFramework.Rendering.FrameBuilders.Appenders;
using BP.AdventureFramework.Rendering.FrameBuilders.Appenders.Legacy;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Rendering.FrameBuilders.Appenders.Legacy
{
    [TestClass]
    public class LegacyConversationFrameBuilder_Tests
    {
        [TestMethod]
        public void GivenDefaults_WhenBuild_ThenNoException()
        {
            var builder = new LegacyConversationFrameBuilder(new LineStringBuilder());

            builder.Build(string.Empty, null, null, 80, 50);

            Assert.IsTrue(true);
        }
    }
}
