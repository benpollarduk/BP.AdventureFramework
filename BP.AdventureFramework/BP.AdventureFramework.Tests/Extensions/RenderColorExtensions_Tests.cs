using System;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Rendering.FrameBuilders.Grid.Color;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Extensions
{
    [TestClass]
    public class RenderColorExtensions_Tests
    {
        [TestMethod]
        public void GivenWhite_WhenToConsoleColor_ThenWhite()
        {
            var result = RenderColor.White.ToConsoleColor();
            
            Assert.AreEqual(ConsoleColor.White, result);
        }

        [TestMethod]
        public void GivenBlack_WhenToConsoleColor_ThenBlack()
        {
            var result = RenderColor.Black.ToConsoleColor();

            Assert.AreEqual(ConsoleColor.Black, result);
        }

        [TestMethod]
        public void GivenGray_WhenToConsoleColor_ThenGray()
        {
            var result = RenderColor.Gray.ToConsoleColor();

            Assert.AreEqual(ConsoleColor.Gray, result);
        }

        [TestMethod]
        public void GivenDarkGray_WhenToConsoleColor_ThenDarkGray()
        {
            var result = RenderColor.DarkGray.ToConsoleColor();

            Assert.AreEqual(ConsoleColor.DarkGray, result);
        }

        [TestMethod]
        public void GivenBlue_WhenToConsoleColor_ThenBlue()
        {
            var result = RenderColor.Blue.ToConsoleColor();

            Assert.AreEqual(ConsoleColor.Blue, result);
        }

        [TestMethod]
        public void GivenRed_WhenToConsoleColor_ThenRed()
        {
            var result = RenderColor.Red.ToConsoleColor();

            Assert.AreEqual(ConsoleColor.Red, result);
        }

        [TestMethod]
        public void GivenGreen_WhenToConsoleColor_ThenGreen()
        {
            var result = RenderColor.Green.ToConsoleColor();

            Assert.AreEqual(ConsoleColor.Green, result);
        }

        [TestMethod]
        public void GivenYellow_WhenToConsoleColor_ThenYellow()
        {
            var result = RenderColor.Yellow.ToConsoleColor();

            Assert.AreEqual(ConsoleColor.Yellow, result);
        }
    }
}
