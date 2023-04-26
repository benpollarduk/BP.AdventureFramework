namespace BP.AdventureFramework.Utils.Generation.Simple.Themes
{
    /// <summary>
    /// Provides the forest theme.
    /// </summary>
    public sealed class Forest : ITheme
    {
        #region Implementation of ITheme

        /// <summary>
        /// Get the room nouns.
        /// </summary>
        public string[] RoomNouns { get; } =
        {
            "Grove", "Thicket", "Glade", "Clearing", "Copse", "Shelter", "Hollow", "Lodge", "Nest", "Nook",
            "Den", "Burrow", "Cave", "Dell", "Vale", "Valley", "Gulch", "Canyon", "Pass", "Ravine",
            "Waterfall", "Stream", "Brook", "River", "Creek", "Pond", "Lake", "Lagoon", "Falls", "Rapids",
            "Cliff", "Crag", "Mountain", "Hill", "Butte", "Mesa", "Ridge", "Summit", "Peak", "Plateau"
        };

        /// <summary>
        /// Get the room adjectives.
        /// </summary>
        public string[] RoomAdjectives { get; } =
        {
            "Lush", "Verdant", "Wild", "Dense", "Dappled", "Mossy", "Shadowy", "Enchanted", "Overgrown", "Whispering",
            "Thick", "Untamed", "Rustling", "Mysterious", "Foggy", "Mystical", "Mossy", "Sun-dappled", "Gloomy", "Ethereal",
            "Sylvan", "Primeval", "Towering", "Shimmering", "Sacred", "Sunlit", "Misty", "Mist-shrouded", "Windy", "Serene",
            "Ancient", "Enchanted", "Deep", "Vernal", "Lush green", "Emerald", "Peaceful", "Cool", "Crisp", "Fragrant",
            "Green", "Leafy", "Moss-covered", "Rustic", "Secluded", "Serene", "Silvery", "Spellbound", "Sprawling", "Sun-kissed",
            "Tranquil", "Undisturbed", "Unspoiled", "Vibrant", "Virgin", "Wandering", "Whispering", "Wide-eyed", "Winding", "Enchanted",
            "Otherworldly", "Untouched", "Still", "Solitary", "Spectacular", "Scenic", "Shaded", "Secretive", "Secluded", "Sacred",
            "Rustic", "Radiant", "Pristine", "Peaceful", "Paradise-like", "Organic", "Majestic", "Lushly vegetated", "Inviting", "Idyllic",
            "Humid", "Harmonious", "Fresh", "Flowering", "Ethereal", "Enchanting", "Dewy", "Cozy", "Calm", "Breath-taking"
        };

        /// <summary>
        /// Get the takeable item nouns.
        /// </summary>
        public string[] TakeableItemNouns { get; } =
        {
            "Bow", "Arrow", "Quiver", "Spear", "Knife", "Hatchet", "Axe", "Rope", "Lantern", "Compass",
            "Map", "Binoculars", "Flint", "Magnifying glass", "Whistle", "Compass", "Matchbox", "Hammer", "Saw",
            "Water bottle", "Flares", "Flashlight", "Tent", "Backpack", "Fishing rod", "Trap", "Bug spray", "Pocket knife",
            "Walking stick", "Candle", "Blanket", "Insect net", "Fire starter", "Camp stove", "Food rations", "Notebook", "Pen", "Fishing net",
            "Trowel", "Mallet", "Spade", "Camp chair", "Sleeping bag", "Cooking pot", "Pan", "Spatula", "Fork", "Spoon", "Water filter", "Compass", "Binoculars",
            "Pocket watch", "Axe handle", "Fishing line", "Knife sharpener", "Shovel", "Gloves", "Fishing reel",
            "Firewood", "Fireplace grate", "Bug net", "Saw blade"
        };

        /// <summary>
        /// Get the takeable item adjectives.
        /// </summary>
        public string[] TakeableItemAdjectives { get; } =
        {
            "Rusty", "Cracked", "Mossy", "Sharp", "Weathered", "Ornate", "Shiny", "Delicate", "Ancient", "Fragile",
            "Gleaming", "Worn", "Faint", "Ornamental", "Muddy", "Pristine", "Scratched", "Twisted", "Curved", "Glimmering",
            "Dull", "Polished", "Brittle", "Smooth", "Splintered", "Jagged", "Petrified", "Glistening", "Fuzzy", "Tarnished",
            "Bent", "Charred", "Sparkling", "Chipped", "Smudged", "Engraved", "Glinting", "Pitted", "Glazed", "Filthy",
            "Chewed", "Gnarled", "Faded", "Crimson", "Frosty", "Fierce", "Slimy", "Serrated", "Singed", "Dented",
            "Frayed", "Furry", "Moldy", "Stained", "Mangled", "Grubby", "Flecked", "Tattered", "Thorny", "Marbled",
            "Coarse", "Smoothed", "Grooved", "Spiked", "Veined", "Lustrous", "Scorched", "Hollow", "Pockmarked", "Tarnished",
            "Patterned", "Painted", "Sharpened", "Gritty", "Etched", "Fossilized", "Feathered", "Bejeweled", "Toothed", "Rusted",
            "Crystal", "Chiseled", "Jeweled", "Tangled", "Tarnished", "Carved", "Decorative", "Elegant", "Floral", "Spotted"
        };

        /// <summary>
        /// Get the non-takeable item nouns.
        /// </summary>
        public string[] NonTakeableItemNouns { get; } =
        {
            "Tree", "Stump", "Bush", "Rock", "Stream", "Mushroom", "Fern", "Pinecone", "Moss", "Twig",
            "Log", "Thicket", "Undergrowth", "Boulder", "Creek", "Foliage", "Wildflower", "Acorn", "Grass", "Weeds",
            "Vine", "Branch", "Hollow", "Trunk", "Leaf litter", "Canopy", "Sapling", "Hedge",
            "Ferns", "Lichen", "Liana", "Dead leaves", "Bark", "Conifer", "Decay", "Beech mast", "Birch bark",
            "Mistletoe", "Nut", "Pine needles", "Shrubbery", "Seedpod", "Seedling", "Sprig", "Thorn", "Underbrush",
        };

        /// <summary>
        /// Get the non-takeable item adjectives.
        /// </summary>
        public string[] NonTakeableItemAdjectives { get; } =
        {
            "Rusty", "Cracked", "Mossy", "Sharp", "Weathered", "Ornate", "Shiny", "Delicate", "Ancient", "Fragile",
            "Gleaming", "Worn", "Faint", "Ornamental", "Muddy", "Pristine", "Scratched", "Twisted", "Curved", "Glimmering",
            "Dull", "Polished", "Brittle", "Smooth", "Splintered", "Jagged", "Petrified", "Glistening", "Fuzzy", "Tarnished",
            "Bent", "Charred", "Sparkling", "Chipped", "Smudged", "Engraved", "Glinting", "Pitted", "Glazed", "Filthy",
            "Chewed", "Gnarled", "Faded", "Crimson", "Frosty", "Fierce", "Slimy", "Serrated", "Singed", "Dented",
            "Frayed", "Furry", "Moldy", "Stained", "Mangled", "Grubby", "Flecked", "Tattered", "Thorny", "Marbled",
            "Coarse", "Smoothed", "Grooved", "Spiked", "Veined", "Lustrous", "Scorched", "Hollow", "Pockmarked", "Tarnished",
            "Patterned", "Painted", "Sharpened", "Gritty", "Etched", "Fossilized", "Feathered", "Bejeweled", "Toothed", "Rusted",
            "Crystal", "Chiseled", "Jeweled", "Tangled", "Tarnished", "Carved", "Decorative", "Elegant", "Floral", "Spotted"
        };

        #endregion
    }
}
