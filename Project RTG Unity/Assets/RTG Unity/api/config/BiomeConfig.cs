namespace rtg.api.config
{
    using property;
    public class BiomeConfig : Config
    {
        /*
     * GLOBAL CONFIGS
     */

        public readonly ConfigPropertyBoolean ALLOW_VILLAGES;
        public readonly ConfigPropertyBoolean ALLOW_VOLCANOES;
        public readonly ConfigPropertyInt VOLCANO_CHANCE;
        public readonly ConfigPropertyBoolean USE_RTG_DECORATIONS;
        public readonly ConfigPropertyBoolean USE_RTG_SURFACES;
        public readonly ConfigPropertyString SURFACE_TOP_BLOCK;
        public readonly ConfigPropertyInt SURFACE_TOP_BLOCK_META;
        public readonly ConfigPropertyString SURFACE_FILLER_BLOCK;
        public readonly ConfigPropertyInt SURFACE_FILLER_BLOCK_META;
        public readonly ConfigPropertyString SURFACE_CLIFF_STONE_BLOCK;
        public readonly ConfigPropertyInt SURFACE_CLIFF_STONE_BLOCK_META;
        public readonly ConfigPropertyString SURFACE_CLIFF_COBBLE_BLOCK;
        public readonly ConfigPropertyInt SURFACE_CLIFF_COBBLE_BLOCK_META;
        public readonly ConfigPropertyInt CAVE_DENSITY;
        public readonly ConfigPropertyInt CAVE_FREQUENCY;
        public readonly ConfigPropertyInt RAVINE_FREQUENCY;
        public readonly ConfigPropertyInt BEACH_BIOME;
        public readonly ConfigPropertyFloat TREE_DENSITY_MULTIPLIER;

        /*
         * OPTIONAL CONFIGS
         */

        public readonly ConfigPropertyBoolean ALLOW_LOGS;
        public readonly ConfigPropertyString SURFACE_MIX_BLOCK;
        public readonly ConfigPropertyInt SURFACE_MIX_BLOCK_META;
        public readonly ConfigPropertyString SURFACE_MIX_FILLER_BLOCK;
        public readonly ConfigPropertyInt SURFACE_MIX_FILLER_BLOCK_META;
        public readonly ConfigPropertyBoolean ALLOW_PALM_TREES;
        public readonly ConfigPropertyBoolean ALLOW_CACTUS;
        public readonly ConfigPropertyBoolean ALLOW_COBWEBS;
        public readonly ConfigPropertyBoolean ALLOW_WHEAT;
        public readonly ConfigPropertyInt WHEAT_CHANCE;
        public readonly ConfigPropertyInt WHEAT_MIN_Y;
        public readonly ConfigPropertyInt WHEAT_MAX_Y;

        public BiomeConfig()
        {

            /*
             * GLOBAL CONFIGS
             */

            ALLOW_VILLAGES = new ConfigPropertyBoolean(
                ConfigProperty.Type.BOOLEAN,
                "Allow Villages",
                "Villages",
                "Set this to FALSE to prevent villages from generating in this biome.",
                true
            );
            this.addProperty(ALLOW_VILLAGES);

            ALLOW_VOLCANOES = new ConfigPropertyBoolean(
                ConfigProperty.Type.BOOLEAN,
                "Allow volcanoes",
                "Volcanoes",
                "Set this to TRUE to allow volcanoes to generate in this biome.",
                false
            );
            this.addProperty(ALLOW_VOLCANOES);

            VOLCANO_CHANCE = new ConfigPropertyInt(
                ConfigProperty.Type.INTEGER,
                "Volcano Chance",
                "Volcanoes",
                "1/x chance that a volcano will generate if this biome has volcanoes enabled."
                    + '\n' + "1 = Always generate if possible; 2 = 50% chance; 4 = 25% chance"
                    + '\n' + "Set to -1 to use global setting. Set to 0 to disable volcanoes for this biome.",
                -1, -1, int.MaxValue
            );
            this.addProperty(VOLCANO_CHANCE);

            USE_RTG_DECORATIONS = new ConfigPropertyBoolean(
                ConfigProperty.Type.BOOLEAN,
                "Use RTG Decorations",
                "Decorations",
                "If FALSE, no RTG decorations will generate in this biome. Instead, only vanilla decorations will generate."
                    + '\n' + "RTG decorations include custom trees, shrubs, boulders, etc.",
                true
            );
            this.addProperty(USE_RTG_DECORATIONS);

            USE_RTG_SURFACES = new ConfigPropertyBoolean(
                ConfigProperty.Type.BOOLEAN,
                "Use RTG Surfaces",
                "Surfaces",
                "If FALSE, no RTG surfaces will be used in this biome. Instead, only vanilla surfaces will be used."
                    + '\n' + "RTG surfaces include custom top & filler blocks, and 'mix' blocks like podzol in Forests.",
                true
            );
            this.addProperty(USE_RTG_SURFACES);

            SURFACE_TOP_BLOCK = new ConfigPropertyString(
                ConfigProperty.Type.STRING,
                "Top Block ID",
                "Surfaces.Top Block",
                "If you want to change this biome's top block, enter a valid block ID here (e.g. minecraft:grass)."
                    + '\n' +
                    "For more info, visit http://minecraft.gamepedia.com/Data_values#Block_IDs",
                ""
            );
            this.addProperty(SURFACE_TOP_BLOCK);

            SURFACE_TOP_BLOCK_META = new ConfigPropertyInt(
                ConfigProperty.Type.INTEGER,
                "Top Block Meta (Data Value)",
                "Surfaces.Top Block",
                "If you're using a custom top block, enter its numeric data value here."
                    + '\n' +
                    "For example, if you want to use red wool for this biome's top block, you would enter minecraft:wool for the Top Block ID,"
                    + '\n' +
                    "and you would enter 6 here, because red wool has a data value of 6. (For most blocks, this value will be 0.)"
                    + '\n' +
                    "For more info, visit http://minecraft.gamepedia.com/Data_values",
                0, 0, 15
            );
            this.addProperty(SURFACE_TOP_BLOCK_META);

            SURFACE_FILLER_BLOCK = new ConfigPropertyString(
                ConfigProperty.Type.STRING,
                "Filler Block ID",
                "Surfaces.Filler Block",
                "If you want to change this biome's filler block (the block underneath the top block), enter a valid block ID here (e.g. minecraft:dirt)."
                    + '\n' +
                    "For more info, visit http://minecraft.gamepedia.com/Data_values#Block_IDs",
                ""
            );
            this.addProperty(SURFACE_FILLER_BLOCK);

            SURFACE_FILLER_BLOCK_META = new ConfigPropertyInt(
                ConfigProperty.Type.INTEGER,
                "Filler Block Meta (Data Value)",
                "Surfaces.Filler Block",
                "If you're using a custom filler block, enter its numeric data value here."
                    + '\n' +
                    "For example, if you want to use red wool for this biome's filler block, you would enter minecraft:wool for the Filler Block ID,"
                    + '\n' +
                    "and you would enter 6 here, because red wool has a data value of 6. (For most blocks, this value will be 0.)"
                    + '\n' +
                    "For more info, visit http://minecraft.gamepedia.com/Data_values",
                0, 0, 15
            );
            this.addProperty(SURFACE_FILLER_BLOCK_META);

            SURFACE_CLIFF_STONE_BLOCK = new ConfigPropertyString(
                ConfigProperty.Type.STRING,
                "Cliff Stone Block ID",
                "Surfaces.Cliff Stone Block",
                "Cliff blocks are the blocks that are used on the cliffs of mountains (usually a blend of stone & cobblestone)."
                    + '\n' +
                    "If you want to change this biome's cliff stone block, enter a valid block ID here (e.g. minecraft:stone)."
                    + '\n' +
                    "For more info, visit http://minecraft.gamepedia.com/Data_values#Block_IDs",
                ""
            );
            this.addProperty(SURFACE_CLIFF_STONE_BLOCK);

            SURFACE_CLIFF_STONE_BLOCK_META = new ConfigPropertyInt(
                ConfigProperty.Type.INTEGER,
                "Cliff Stone Block Meta (Data Value)",
                "Surfaces.Cliff Stone Block",
                "If you're using a custom cliff stone block, enter its numeric data value here."
                    + '\n' +
                    "For example, if you want to use red wool for this biome's cliff stone block, you would enter minecraft:wool for the Cliff Stone Block ID,"
                    + '\n' +
                    "and you would enter 6 here, because red wool has a data value of 6. (For most blocks, this value will be 0.)"
                    + '\n' +
                    "For more info, visit http://minecraft.gamepedia.com/Data_values",
                0, 0, 15
            );
            this.addProperty(SURFACE_CLIFF_STONE_BLOCK_META);

            SURFACE_CLIFF_COBBLE_BLOCK = new ConfigPropertyString(
                ConfigProperty.Type.STRING,
                "Cliff Cobble Block ID",
                "Surfaces.Cliff Cobble Block",
                "Cliff blocks are the blocks that are used on the cliffs of mountains (usually a blend of stone & cobblestone)."
                    + '\n' +
                    "If you want to change this biome's cliff cobble block, enter a valid block ID here (e.g. minecraft:cobblestone)."
                    + '\n' +
                    "For more info, visit http://minecraft.gamepedia.com/Data_values#Block_IDs",
                ""
            );
            this.addProperty(SURFACE_CLIFF_COBBLE_BLOCK);

            SURFACE_CLIFF_COBBLE_BLOCK_META = new ConfigPropertyInt(
                ConfigProperty.Type.INTEGER,
                "Cliff Cobble Block Meta (Data Value)",
                "Surfaces.Cliff Cobble Block",
                "If you're using a custom cliff cobble block, enter its numeric data value here."
                    + '\n' +
                    "For example, if you want to use red wool for this biome's cliff cobble block, you would enter minecraft:wool for the Cliff Cobble Block ID,"
                    + '\n' +
                    "and you would enter 6 here, because red wool has a data value of 6. (For most blocks, this value will be 0.)"
                    + '\n' +
                    "For more info, visit http://minecraft.gamepedia.com/Data_values",
                0, 0, 15
            );
            this.addProperty(SURFACE_CLIFF_COBBLE_BLOCK_META);

            CAVE_DENSITY = new ConfigPropertyInt(
                ConfigProperty.Type.INTEGER,
                "Cave Density",
                "Caves",
                "This setting controls the size of caves."
                    + '\n' + "HIGHER values = BIGGER caves & MORE lag. (14 = vanilla cave density)"
                    + '\n' + "Set to -1 to use global setting. Set to 0 to disable caves for this biome.",
                -1, -1, 40
            );
            this.addProperty(CAVE_DENSITY);

            CAVE_FREQUENCY = new ConfigPropertyInt(
                ConfigProperty.Type.INTEGER,
                "Cave Frequency",
                "Caves",
                "This setting controls the number of caves that generate."
                    + '\n' + "LOWER values = MORE caves & MORE lag. (6 = vanilla cave frequency)"
                    + '\n' + "Set to -1 to use global setting. Set to 0 to disable caves for this biome.",
                -1, -1, 40
            );
            this.addProperty(CAVE_FREQUENCY);

            RAVINE_FREQUENCY = new ConfigPropertyInt(
                ConfigProperty.Type.INTEGER,
                "Ravine Frequency",
                "Ravines",
                "This setting controls the number of ravines that generate."
                    + '\n' + "LOWER values = MORE ravines & MORE lag. (50 = vanilla ravine frequency)"
                    + '\n' + "Set to -1 to use global setting. Set to 0 to disable ravines for this biome.",
                -1, -1, 100
            );
            this.addProperty(RAVINE_FREQUENCY);

            BEACH_BIOME = new ConfigPropertyInt(
                ConfigProperty.Type.INTEGER, "Beach Biome", "Beaches",
                "Biome ID to use for this biome's beach."
                    + '\n'
                    + "The only 'officially supported' values for this setting are:"
                    + '\n'
                    + "-1 = Automatic beach generation (RECOMMENDED)"
                    + '\n'
                    + "16 = Vanilla Beach"
                    + '\n'
                    + "26 = Vanilla Cold Beach"
                    + '\n'
                    + "25 = Vanilla Stone Beach"
                    + '\n'
                    + "The ID of this biome = No beach"
                    + '\n'
                    + "Other biome IDs are allowed, but they have not been tested, may yield undesirable results, and will not be supported."
                    + '\n'
                    + "Note: If this biome has been hardcoded by RTG to use a specific beach, this setting will have no effect.",
                -1, -1, 255
            );
            this.addProperty(BEACH_BIOME);

            TREE_DENSITY_MULTIPLIER = new ConfigPropertyFloat(
                ConfigProperty.Type.FLOAT,
                "RTG Tree Density Multiplier",
                "Trees",
                "This setting allows you to increase/decrease the number of RTG trees that generate in this biome."
                    + '\n' +
                    "This setting overrides the global setting (see /.minecraft/config/RTG/rtg.cfg) and only affects trees generated by RTG."
                    + '\n' +
                    "Trees generated by this biome's decorator will adhere to their own density rules."
                    + '\n' +
                    "Set to -1.0 to use the global setting."
                    + '\n' +
                    "1.0 = Default tree generation; 2.0 = Twice as many trees; 0.5 = half as many trees; 0 = No trees",
                -1.0f, -1.0f, 5.0f
            );
            this.addProperty(TREE_DENSITY_MULTIPLIER);

            /*
             * OPTIONAL CONFIGS
             *
             * These properties get 'added' by the individual biomes when relevant, so don't 'add' them here.
             */

            SURFACE_MIX_BLOCK = new ConfigPropertyString(
                ConfigProperty.Type.STRING,
                "Mix Block ID",
                "Surfaces.Mix Top Block",
                "If you want to change this biome's mix block, enter a valid block ID here (e.g. minecraft:grass)."
                    + '\n' +
                    "For more info, visit http://minecraft.gamepedia.com/Data_values#Block_IDs",
                ""
            );

            SURFACE_MIX_BLOCK_META = new ConfigPropertyInt(
                ConfigProperty.Type.INTEGER,
                "Mix Block Meta (Data Value)",
                "Surfaces.Mix Top Block",
                "If you're using a custom mix block, enter its numeric data value here."
                    + '\n' +
                    "For example, if you want to use podzol for this biome's mix block, you would enter minecraft:dirt for the Mix Block ID,"
                    + '\n' +
                    "and you would enter 2 here, because podzol has a data value of 2. (For most blocks, this value will be 0.)"
                    + '\n' +
                    "For more info, visit http://minecraft.gamepedia.com/Data_values",
                0, 0, 15
            );

            SURFACE_MIX_FILLER_BLOCK = new ConfigPropertyString(
                ConfigProperty.Type.STRING,
                "Mix Filler Block ID",
                "Surfaces.Mix Filler Block",
                "If you want to change this biome's mix filler block (the block underneath the mix block), enter a valid block ID here (e.g. minecraft:dirt)."
                    + '\n' +
                    "For more info, visit http://minecraft.gamepedia.com/Data_values#Block_IDs",
                ""
            );

            SURFACE_MIX_FILLER_BLOCK_META = new ConfigPropertyInt(
                ConfigProperty.Type.INTEGER,
                "Mix Filler Block Meta (Data Value)",
                "Surfaces.Mix Filler Block",
                "If you're using a custom mix filler block, enter its numeric data value here."
                    + '\n' +
                    "For example, if you want to use podzol for this biome's mix filler block, you would enter minecraft:dirt for the Mix Filler Block ID,"
                    + '\n' +
                    "and you would enter 2 here, because podzol has a data value of 2. (For most blocks, this value will be 0.)"
                    + '\n' +
                    "For more info, visit http://minecraft.gamepedia.com/Data_values",
                0, 0, 15
            );

            ALLOW_LOGS = new ConfigPropertyBoolean(ConfigProperty.Type.BOOLEAN, "Allow Logs", "Decorations.Logs", "", true);
            ALLOW_PALM_TREES = new ConfigPropertyBoolean(ConfigProperty.Type.BOOLEAN, "Allow Palm Trees", "Decorations.Palm Trees", "", true);
            ALLOW_CACTUS = new ConfigPropertyBoolean(ConfigProperty.Type.BOOLEAN, "Allow Cactus", "Decorations.Cactus", "", true);
            ALLOW_COBWEBS = new ConfigPropertyBoolean(ConfigProperty.Type.BOOLEAN, "Allow Cobwebs", "Decorations.Cobwebs", "", true);
            ALLOW_WHEAT = new ConfigPropertyBoolean(ConfigProperty.Type.BOOLEAN, "Allow Wheat", "Decorations.Wheat", "", true);
            WHEAT_CHANCE = new ConfigPropertyInt(ConfigProperty.Type.INTEGER, "Wheat (Chance)", "Decorations.Wheat", "", 0, 0, int.MaxValue);
            WHEAT_MIN_Y = new ConfigPropertyInt(ConfigProperty.Type.INTEGER, "Wheat (Min Y)", "Decorations.Wheat", "", 0, 0, int.MaxValue);
            WHEAT_MAX_Y = new ConfigPropertyInt(ConfigProperty.Type.INTEGER, "Wheat (Max Y)", "Decorations.Wheat", "", 0, 0, int.MaxValue);
        }

        public static string formatSlug(string s)
        {

            s = s.ToLower();
            s = s.Replace("\\+", "plus");
            s = s.Replace("\\W", "");

            return s;
        }
    }
}