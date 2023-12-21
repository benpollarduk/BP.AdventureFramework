using BP.AdventureFramework.Assets.Locations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Assets.Locations
{
    [TestClass]
    public class Exit_Tests
    {
        [TestMethod]
        public void GivenLockedExit_WhenUnlock_ThenIsLockedIsTrue()
        {
            var exit = new Exit(Direction.Down, true);

            exit.Unlock();

            Assert.IsFalse(exit.IsLocked);
        }

        [TestMethod]
        public void GivenUnlockedExit_WhenLock_ThenIsLockedIsFalse()
        {
            var exit = new Exit(Direction.Down);

            exit.Lock();

            Assert.IsTrue(exit.IsLocked);
        }
    }
}
