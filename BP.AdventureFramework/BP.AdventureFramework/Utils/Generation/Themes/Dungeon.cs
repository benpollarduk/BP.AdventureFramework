namespace BP.AdventureFramework.Utils.Generation.Simple.Themes
{
    /// <summary>
    /// Provides the dungeon theme.
    /// </summary>
    public sealed class Dungeon : ITheme
    {
        #region Implementation of ITheme

        /// <summary>
        /// Get the room nouns.
        /// </summary>
        public string[] RoomNouns { get; } =
        {
            "Corridor", "Chamber", "Cell", "Dungeon", "Tunnel", "Passageway", "Cavern", "Crypt", "Vault", "Tombs",
            "Laboratory", "Study", "Library", "Observatory", "Workshop", "Armory", "Forge", "Foundry", "Smithy", "Quarry",
            "Barracks", "Mess Hall", "Training Room", "Command Center", "War Room", "Watchtower", "Belfry", "Gatehouse", "Drawbridge", "Moat",
            "Temple", "Shrine", "Altar", "Sacrificial Chamber", "Reliquary", "Sanctuary", "Catacomb", "Necropolis", "Mausoleum", "Ossuary",
            "Torture Chamber", "Interrogation Room", "Iron Maiden Chamber", "Rack Room", "Pendulum Room", "Saw Room", "Acid Bath Room", "Stretching Room", "Spike Chair Room", "Guillotine Room",
            "Underground Lake", "Underground River", "Underground Waterfall", "Underground Stream", "Underground Pool", "Underground Grotto", "Underground Oasis", "Underground Reservoir", "Underground Fountain",
            "Throne Room", "Audience Chamber", "Council Chamber", "Banquet Hall", "Feast Hall", "Ballroom", "Reception Hall", "Great Hall", "Meeting Room", "Conference Room",
            "Guard Room", "Prison", "Dungeon Cell Block", "Execution Chamber", "Interrogation Chamber", "Armory Room", "Guard Barracks", "Guard Tower", "Gate Room", "Fortified Wall",
            "Treasury", "Vault Room", "Storage Room", "Cache Room", "Wine Cellar", "Beer Hall", "Distillery Room", "Spirits Room", "Brewery Room", "Barrel Room", "Tavern",
            "Mine", "Quarry", "Shaft", "Tunnel", "Cave", "Adit", "Stope", "Lode", "Vein", "Drift"
        };


        /// <summary>
        /// Get the room adjectives.
        /// </summary>
        public string[] RoomAdjectives { get; } =
        {
            "Dim", "Shadowy", "Gloomy", "Damp", "Cramped", "Narrow", "Chilly", "Drafty", "Foul-smelling", "Decrepit",
            "Spacious", "Grand", "Stately", "Imposing", "Majestic", "Vast", "Opulent", "Luxurious", "Extravagant", "Magnificent",
            "Rubble-strewn", "Crumbling", "Derelict", "Abandoned", "Desolate", "Haunted", "Ruined", "Collapsed", "Decayed", "Shattered",
            "Labyrinthine", "Twisting", "Mazelike", "Complex", "Intricate", "Confusing", "Convolute", "Circuitous", "Meandering", "Serpentine",
            "Fiery", "Burning", "Blazing", "Scorching", "Flaming", "Smoking", "Volcanic", "Incendiary", "Combustible", "Explosive",
            "Frigid", "Frozen", "Icy", "Glacial", "Polar", "Cold", "Chilled", "Frosty", "Arctic", "Hypothermic",
            "Flooded", "Submerged", "Waterlogged", "Drowned", "Sodden", "Soaked", "Dampened", "Muddy", "Boggy", "Swampy",
            "Rancid", "Putrid", "Fetid", "Rank", "Noisome", "Sickening", "Nauseating", "Offensive", "Malodorous", "Stinking",
            "Echoing", "Resonant", "Reverberating", "Booming", "Thunderous", "Rumbling", "Clanging", "Rattling", "Roaring", "Deafening",
            "Gilded", "Ornate", "Exquisite", "Elaborate", "Fancy", "Intricate", "Artistic", "Elegant", "Refined", "Graceful",
            "Blood-stained", "Gore-spattered", "Smeared", "Stained", "Besmirched", "Sullied", "Tainted", "Polluted", "Unclean", "Defiled",
            "Musty", "Moldy", "Dusty", "Stale", "Moldering", "Decaying", "Rotten", "Putrefying", "Miasmic", "Rancid"
        };


        /// <summary>
        /// Get the takeable item nouns.
        /// </summary>
        public string[] TakeableItemNouns { get; } =
        {
            "Sword", "Shield", "Axe", "Mace", "Spear", "Dagger", "Bow", "Arrow", "Crossbow", "Bolt",
            "Helmet", "Breastplate", "Gauntlets", "Greaves", "Boots", "Cloak", "Amulet", "Ring", "Potion", "Scroll",
            "Gold", "Silver", "Platinum", "Copper", "Diamond", "Ruby", "Emerald", "Sapphire", "Topaz", "Opal",
            "Staff", "Wand", "Orb", "Book", "Lantern", "Torch", "Candle", "Chalice", "Statue", "Painting",
            "Rope", "Grappling Hook", "Crowbar", "Lockpick", "Compass", "Map", "Spyglass", "Tinderbox", "Flint and Steel", "Backpack",
            "Food Rations", "Water Skin", "Cooking Pot", "Bedroll", "Tent", "Climbing Gear", "Horse", "Saddle", "Ladder", "Pickaxe",
            "Gems", "Jewelry", "Antique", "Artifact", "Relic", "Idol", "Totem", "Bone", "Skull", "Fossil",
            "Trap Kit", "Poison Kit", "Alchemy Kit", "Herbalism Kit", "Artificer's Tools", "Carpenter's Tools", "Mason's Tools", "Smith's Tools", "Leatherworker's Tools", "Weaver's Tools",
            "Key", "Treasure Map", "Quest Item", "Artifact Fragment", "Crystal Shard", "Enchanted Scroll", "Spellbook", "Magic Mirror", "Crystal Ball", "Magic Wand",
            "Holy Water", "Garlic", "Wooden Stake", "Silver Bullet", "Blessed Cross", "Religious Symbol", "Sacred Text", "Prayer Beads", "Amulet of Warding", "Holy Relic",
            "Skeletal Remains", "Zombie Flesh", "Goblin Ear", "Orc Tooth", "Troll Hide", "Dragon Scale", "Demon Horn", "Devil Tail", "Giant's Club", "Beholder's Eye",
            "Mummy Wrappings", "Vampire Fang", "Lich Dust", "Werewolf Fur", "Ghoul Blood", "Gargoyle Wing", "Harpy Feather", "Minotaur Horn", "Medusa Head", "Cyclops Eye"
        };


        /// <summary>
        /// Get the takeable item adjectives.
        /// </summary>
        public string[] TakeableItemAdjectives { get; } =
        {
            "Leather", "Metallic", "Silken", "Cotton", "Woolen", "Sturdy", "Velvet", "Linen", "Satin", "Fur",
            "Brass", "Bronze", "Gold", "Silver", "Copper", "Iron", "Steel", "Platinum", "Titanium", "Aluminum",
            "Mahogany", "Oak", "Cherry", "Bamboo", "Pine", "Maple", "Cedar", "Walnut", "Rosewood", "Ebony",
            "Crystal", "Glass", "Porcelain", "Marble", "Granite", "Quartz", "Jade", "Amber", "Onyx", "Sapphire",
            "Plastic", "Rubber", "Nylon", "Polyester", "Acrylic", "Polypropylene", "PVC", "Silicone", "Resin", "Epoxy",
            "Paper", "Cardboard", "Chipboard", "Parchment", "Vellum", "Velum", "Kraft", "Mulberry paper", "Handmade paper", "Newsprint",
            "Clay", "Ceramic", "Porcelain", "Earthenware", "Terracotta", "Stoneware", "Glass", "Crystal", "Jasper", "Opal",
            "Amethyst", "Emerald", "Topaz", "Ruby", "Pearl", "Diamond", "Sapphire", "Turquoise", "Agate", "Aquamarine",
            "Granite", "Limestone", "Sandstone", "Slate", "Marble", "Travertine", "Alabaster", "Basalt", "Obsidian", "Quartzite",
            "Concrete", "Brick", "Mortar", "Cement", "Plaster", "Stucco", "Gypsum", "Cinder block", "Asphalt", "Tar",
            "Rope", "Twine", "Hemp", "Jute", "Nylon", "Polyester", "Cotton", "Linen", "Silk", "Wool"
        };


        /// <summary>
        /// Get the non-takeable item nouns.
        /// </summary>
        public string[] NonTakeableItemNouns { get; } =
        {
            "Table", "Chair", "Throne", "Altar", "Pedestal", "Pillar", "Statue", "Fountain", "Rubble", "Wall",
            "Stairs", "Bridge", "Trapdoor", "Chasm", "Cage", "Coffin", "Shrine", "Grave", "Sarcophagus", "Well",
            "Lever", "Switch", "Button", "Wheel", "Pulley", "Chain", "Lock", "Keyhole", "Gate", "Door",
            "Window", "Arrow Slit", "Balcony", "Catwalk", "Gallery", "Ledge", "Railing", "Ramp", "Stained Glass", "Tapestry",
            "Brazier", "Candlestick", "Chandelier", "Fireplace", "Forge", "Kiln", "Oven", "Pit", "Torch", "Barricade",
            "Bed", "Chest", "Crate", "Cupboard", "Drawer", "Shelf", "Barrel", "Cask", "Jar", "Vase",
            "Anvil", "Bellows", "Cart", "Cauldron", "Chainmail", "Coat of Arms", "Flag", "Gong", "Hammer", "Ingot",
            "Mirror", "Painting", "Portrait", "Shield", "Sign", "Skeleton", "Skull", "Spear Rack", "Trophy", "Wagon Wheel",
            "Bookshelf", "Desk", "Easel", "Globe", "Lamp", "Quill", "Rug", "Safe", "Seating", "Writing Utensils",
            "Barbecue", "Grinding Stone", "Oxen Yoke", "Plow", "Saddle Rack", "Shovel", "Spinning Wheel", "Tack", "Trough", "Water Trough",
            "Bones", "Carvings", "Etchings", "Inscriptions", "Runes", "Symbols", "Tombs", "Ruins", "Writings", "Engravings",
            "Dais", "Jail Cell", "Guard Tower", "Lighthouse", "Sentry Post", "Stocks", "Well Shaft", "Windmill", "Bell Tower", "Clock Tower",
            "Cannon", "Catapult", "Mortar", "Ballista", "Trebuchet", "Watchtower", "Fortification", "Barracks", "Armory", "Training Ground"
        };

        /// <summary>
        /// Get the non-takeable item adjectives.
        /// </summary>
        public string[] NonTakeableItemAdjectives { get; } =
        {
            "Leather", "Metallic", "Silken", "Cotton", "Woolen", "Sturdy", "Velvet", "Linen", "Satin", "Fur",
            "Brass", "Bronze", "Gold", "Silver", "Copper", "Iron", "Steel", "Platinum", "Titanium", "Aluminum",
            "Mahogany", "Oak", "Cherry", "Bamboo", "Pine", "Maple", "Cedar", "Walnut", "Rosewood", "Ebony",
            "Crystal", "Glass", "Porcelain", "Marble", "Granite", "Quartz", "Jade", "Amber", "Onyx", "Sapphire",
            "Plastic", "Rubber", "Nylon", "Polyester", "Acrylic", "Polypropylene", "PVC", "Silicone", "Resin", "Epoxy",
            "Paper", "Cardboard", "Chipboard", "Parchment", "Vellum", "Velum", "Kraft", "Mulberry paper", "Handmade paper", "Newsprint",
            "Clay", "Ceramic", "Porcelain", "Earthenware", "Terracotta", "Stoneware", "Glass", "Crystal", "Jasper", "Opal",
            "Amethyst", "Emerald", "Topaz", "Ruby", "Pearl", "Diamond", "Sapphire", "Turquoise", "Agate", "Aquamarine",
            "Granite", "Limestone", "Sandstone", "Slate", "Marble", "Travertine", "Alabaster", "Basalt", "Obsidian", "Quartzite",
            "Concrete", "Brick", "Mortar", "Cement", "Plaster", "Stucco", "Gypsum", "Cinder block", "Asphalt", "Tar",
            "Rope", "Twine", "Hemp", "Jute", "Nylon", "Polyester", "Cotton", "Linen", "Silk", "Wool"
        };

        #endregion
    }
}
