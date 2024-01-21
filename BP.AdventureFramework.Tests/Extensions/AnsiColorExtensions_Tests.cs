using System;
using BP.AdventureFramework.Extensions;
using BP.AdventureFramework.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Extensions
{
    [TestClass]
    public class AnsiColorExtensions_Tests
    {
        [TestMethod]
        public void GivenWhite_WhenToConsoleColor_ThenWhite()
        {
            var result = AnsiColor.White.ToConsoleColor();
            
            Assert.AreEqual(ConsoleColor.White, result);
        }

        [TestMethod]
        public void GivenBlack_WhenToConsoleColor_ThenBlack()
        {
            var result = AnsiColor.Black.ToConsoleColor();

            Assert.AreEqual(ConsoleColor.Black, result);
        }

        [TestMethod]
        public void GivenGray_WhenToConsoleColor_ThenGray()
        {
            var result = AnsiColor.Gray.ToConsoleColor();

            Assert.AreEqual(ConsoleColor.Gray, result);
        }

        [TestMethod]
        public void GivenDarkGray_WhenToConsoleColor_ThenDarkGray()
        {
            var result = AnsiColor.DarkGray.ToConsoleColor();

            Assert.AreEqual(ConsoleColor.DarkGray, result);
        }

        [TestMethod]
        public void GivenBlue_WhenToConsoleColor_ThenBlue()
        {
            var result = AnsiColor.Blue.ToConsoleColor();

            Assert.AreEqual(ConsoleColor.Blue, result);
        }

        [TestMethod]
        public void GivenRed_WhenToConsoleColor_ThenRed()
        {
            var result = AnsiColor.Red.ToConsoleColor();

            Assert.AreEqual(ConsoleColor.Red, result);
        }

        [TestMethod]
        public void GivenGreen_WhenToConsoleColor_ThenGreen()
        {
            var result = AnsiColor.Green.ToConsoleColor();

            Assert.AreEqual(ConsoleColor.Green, result);
        }

        [TestMethod]
        public void GivenYellow_WhenToConsoleColor_ThenYellow()
        {
            var result = AnsiColor.Yellow.ToConsoleColor();

            Assert.AreEqual(ConsoleColor.Yellow, result);
        }
    }
}
