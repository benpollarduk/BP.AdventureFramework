using BP.AdventureFramework.Assets;
using BP.AdventureFramework.Assets.Characters;
using BP.AdventureFramework.Assets.Locations;
using BP.AdventureFramework.Logic;
using BP.AdventureFramework.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BP.AdventureFramework.Tests.Logic
{
    [TestClass]
    public class Game_Tests
    {
        [TestMethod]
        public void GivenEmptyRoom_WhenGetAllPlayerVisibleExaminables_ThenNotNull()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var room = new Room(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = room;
            var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(string.Empty, string.Empty, string.Empty, x => overworldMaker.Make(), () => new PlayableCharacter(string.Empty, string.Empty), x => new CompletionCheckResult(false, string.Empty, string.Empty)).Invoke();

            var result = game.GetAllPlayerVisibleExaminables();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GivenEmptyRoom_WhenGetAllPlayerVisibleExaminables_Then4Examinables()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var room = new Room(string.Empty, string.Empty);
            regionMaker[0, 0, 0] = room;
            var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(string.Empty, string.Empty, string.Empty, x => overworldMaker.Make(), () => new PlayableCharacter(string.Empty, string.Empty), x => new CompletionCheckResult(false, string.Empty, string.Empty)).Invoke();

            var result = game.GetAllPlayerVisibleExaminables();

            Assert.AreEqual(4, result.Length);
        }

        [TestMethod]
        public void GivenRoomWithOnePlayerVisibleItem_WhenGetAllPlayerVisibleExaminables_Then5Examinables()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var item = new Item(string.Empty, string.Empty) { IsPlayerVisible = true };
            var room = new Room(string.Empty, string.Empty,null, item);
            regionMaker[0, 0, 0] = room;
            var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(string.Empty, string.Empty, string.Empty, x => overworldMaker.Make(), () => new PlayableCharacter(string.Empty, string.Empty), x => new CompletionCheckResult(false, string.Empty, string.Empty)).Invoke();

            var result = game.GetAllPlayerVisibleExaminables();

            Assert.AreEqual(5, result.Length);
        }

        [TestMethod]
        public void GivenRoomWithOnePlayerInvisibleItem_WhenGetAllPlayerVisibleExaminables_Then4Examinables()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var item = new Item(string.Empty, string.Empty) { IsPlayerVisible = false };
            var room = new Room(string.Empty, string.Empty, null, item);
            regionMaker[0, 0, 0] = room;
            var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(string.Empty, string.Empty, string.Empty, x => overworldMaker.Make(), () => new PlayableCharacter(string.Empty, string.Empty), x => new CompletionCheckResult(false, string.Empty, string.Empty)).Invoke();

            var result = game.GetAllPlayerVisibleExaminables();

            Assert.AreEqual(4, result.Length);
        }

        [TestMethod]
        public void GivenRoomWithOnePlayerVisibleExit_WhenGetAllPlayerVisibleExaminables_Then5Examinables()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var exit = new Exit(Direction.Down) { IsPlayerVisible = true };
            var room = new Room(string.Empty, string.Empty, exit);
            regionMaker[0, 0, 0] = room;
            var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(string.Empty, string.Empty, string.Empty, x => overworldMaker.Make(), () => new PlayableCharacter(string.Empty, string.Empty), x => new CompletionCheckResult(false, string.Empty, string.Empty)).Invoke();

            var result = game.GetAllPlayerVisibleExaminables();

            Assert.AreEqual(5, result.Length);
        }

        [TestMethod]
        public void GivenRoomWithOnePlayerInvisibleExit_WhenGetAllPlayerVisibleExaminables_Then4Examinables()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var exit = new Exit(Direction.Down) { IsPlayerVisible = false };
            var room = new Room(string.Empty, string.Empty, exit);
            regionMaker[0, 0, 0] = room;
            var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(string.Empty, string.Empty, string.Empty, x => overworldMaker.Make(), () => new PlayableCharacter(string.Empty, string.Empty), x => new CompletionCheckResult(false, string.Empty, string.Empty)).Invoke();

            var result = game.GetAllPlayerVisibleExaminables();

            Assert.AreEqual(4, result.Length);
        }

        [TestMethod]
        public void GivenRoomWithOnePlayerVisibleNPC_WhenGetAllPlayerVisibleExaminables_Then5Examinables()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var npc = new NonPlayableCharacter(string.Empty, string.Empty) { IsPlayerVisible = true };
            var room = new Room(string.Empty, string.Empty);
            room.AddCharacter(npc);
            regionMaker[0, 0, 0] = room;
            var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(string.Empty, string.Empty, string.Empty, x => overworldMaker.Make(), () => new PlayableCharacter(string.Empty, string.Empty), x => new CompletionCheckResult(false, string.Empty, string.Empty)).Invoke();

            var result = game.GetAllPlayerVisibleExaminables();

            Assert.AreEqual(5, result.Length);
        }

        [TestMethod]
        public void GivenRoomWithOnePlayerInvisibleNPC_WhenGetAllPlayerVisibleExaminables_Then4Examinables()
        {
            var regionMaker = new RegionMaker(string.Empty, string.Empty);
            var npc = new NonPlayableCharacter(string.Empty, string.Empty) { IsPlayerVisible = false };
            var room = new Room(string.Empty, string.Empty);
            room.AddCharacter(npc);
            regionMaker[0, 0, 0] = room;
            var overworldMaker = new OverworldMaker(string.Empty, string.Empty, regionMaker);
            var game = Game.Create(string.Empty, string.Empty, string.Empty, x => overworldMaker.Make(), () => new PlayableCharacter(string.Empty, string.Empty), x => new CompletionCheckResult(false, string.Empty, string.Empty)).Invoke();

            var result = game.GetAllPlayerVisibleExaminables();

            Assert.AreEqual(4, result.Length);
        }
    }
}
