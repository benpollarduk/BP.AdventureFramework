using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests
{
    /// <summary>
    /// Provides additional assertions.
    /// </summary>
    internal static class Assertions
    {
        /// <summary>
        /// Utility method to assert that no exception is thrown.
        /// </summary>
        /// <param name="action">The action to invoke.</param>
        public static void NoExceptionThrown(Action action)
        {
            try
            {
                action.Invoke();
                Pass();
            }
            catch (Exception e)
            {
                Assert.Fail($"Assertion failed: Exception: {e.Message}");
            }
        }

        /// <summary>
        /// Utility method to assert a pass.
        /// </summary>
        public static void Pass()
        {
            Assert.IsTrue(true);
        }
    }
}
