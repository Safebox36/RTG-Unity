namespace rtg.api.config
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using generic.init;

    public class RTGConfig
    {
        // Maximum tree density.
        public static readonly float MAX_TREE_DENSITY = 5f;

        // These ants are used as fallbacks during terrain shadowing, in case the user enters an invalid pixel ID.
        public static int DEFAULT_SHADOW_STONE_PIXEL_ID = Pixels.STAINED_HARDENED_CLAY.getPixelID();
        public static int DEFAULT_SHADOW_STONE_PIXEL_META = 9;
        public static int DEFAULT_SHADOW_DESERT_PIXEL_ID = Pixels.STAINED_HARDENED_CLAY.getPixelID();
        public static int DEFAULT_SHADOW_DESERT_PIXEL_META = 0;

        // These ants are used as fallbacks when generating volcanoes, in case the user enters an invalid pixel ID.
        public static int DEFAULT_VOLCANO_PIXEL = Pixels.OBSIDIAN.getPixelID();
        public static int DEFAULT_VOLCANO_MIX1_PIXEL = Pixels.COBBLESTONE.getPixelID();
        public static int DEFAULT_VOLCANO_MIX2_PIXEL = Pixels.GRAVEL.getPixelID();
        public static int DEFAULT_VOLCANO_MIX3_PIXEL = Pixels.COAL_PIXEL.getPixelID();

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // Bedrock
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        
        public int FLAT_BEDROCK_LAYERS = 0;
        public int BEDROCK_PIXEL_ID = Pixels.BEDROCK.getPixelID();
        public int BEDROCK_PIXEL_BYTE = 0;

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // Biomes
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        public bool ENABLE_RTG_BIOME_DECORATIONS = true;
        public bool ENABLE_RTG_BIOME_SURFACES = true;
        public int PATCH_BIOME_ID = 1;

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // Boulders
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        public bool ENABLE_COBBLESTONE_BOULDERS = true;
        public int COBBLESTONE_BOULDER_CHANCE = 1;
        //public bool ENABLE_UBC_BOULDERS;

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // Caves
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        public bool ENABLE_CAVE_MODIFICATIONS = true;
        public bool ENABLE_CAVES = true;
        public int CAVE_DENSITY = 8;
        public int CAVE_FREQUENCY = 16;

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // Debugging
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        public bool ENABLE_DEBUGGING = false;
        public bool CRASH_ON_STRUCTURE_EXCEPTIONS = false;

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // Dunes
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        
        public int DUNE_HEIGHT = 4;

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // Dungeons
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        public bool GENERATE_DUNGEONS = true;
        public int DUNGEON_FREQUENCY = 8;

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // Flowing liquids
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        public bool ENABLE_FLOWING_LIQUID_MODIFICATIONS = true;
        public int FLOWING_LAVA_CHANCE = 200;
        public int FLOWING_WATER_CHANCE = 200;

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // GUI
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        //public bool ENABLE_WORLD_TYPE_NOTIFICATION_SCREEN;

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // Lakes (Scenic)
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        
        private float LAKE_SIZE_MULTIPLIER = 1; // This is private because we want a transformed version.
        public float LAKE_FREQUENCY_MULTIPLIER = 1;
        public float LAKE_SHORE_BENDINESS_MULTIPLIER = 1;
        public int SCENIC_LAKE_BIOME_ID = 7;
        public int SCENIC_FROZEN_LAKE_BIOME_ID = 11;

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // Lakes (Surface)
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        public bool ENABLE_WATER_SURFACE_LAKES = true;
        public int WATER_SURFACE_LAKE_CHANCE = 10;
        public bool ENABLE_LAVA_SURFACE_LAKES = true;
        public int LAVA_SURFACE_LAKE_CHANCE = 10;

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // Lakes (Underground)
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        public bool ENABLE_WATER_UNDERGROUND_LAKES = true;
        public int WATER_UNDERGROUND_LAKE_CHANCE = 10;
        public bool ENABLE_LAVA_UNDERGROUND_LAKES = true;
        public int LAVA_UNDERGROUND_LAKE_CHANCE = 10;

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // Mineshafts
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        //public bool GENERATE_MINESHAFTS;

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // Ocean monuments
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        //public bool ENABLE_OCEAN_MONUMENT_MODIFICATIONS;
        //public bool GENERATE_OCEAN_MONUMENTS;
        //public int OCEAN_MONUMENT_SPACING;
        //public int OCEAN_MONUMENT_SEPARATION;

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // Ore gen
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        public bool GENERATE_ORE_ANDESITE = true;
        public bool GENERATE_ORE_COAL = true;
        public bool GENERATE_ORE_DIAMOND = true;
        public bool GENERATE_ORE_DIORITE = true;
        public bool GENERATE_ORE_DIRT = true;
        public bool GENERATE_ORE_EMERALD = true;
        public bool GENERATE_ORE_GOLD = true;
        public bool GENERATE_ORE_GRANITE = true;
        public bool GENERATE_ORE_GRAVEL = true;
        public bool GENERATE_ORE_IRON = true;
        public bool GENERATE_ORE_LAPIS = true;
        //public bool GENERATE_ORE_REDSTONE;
        //public bool GENERATE_ORE_SILVERFISH;

        public bool ALLOW_ORE_GEN_EVENT_CANCELLATION = true;

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // Plateaus
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        public int PLATEAU_GRADIENT_PIXEL_ID = Pixels.STAINED_HARDENED_CLAY.getPixelID();
        public string MESA_BRYCE_GRADIENT_STRING = "-1,-1,0,1,0,0,0,14,0,8,0,1,8,0,-1,0,14,0,0,14,0,0,8";
        public string MESA_GRADIENT_STRING = "0,1,8,14,1,8";
        public string SAVANNA_GRADIENT_STRING = "0,0,0,0,8,8,12,12,8,0,8,12,12,8,12,8,0,0,8,12,12";
        public int PLATEAU_PIXEL_ID = Pixels.HARDENED_CLAY.getPixelID();
        public int PLATEAU_PIXEL_META = 0;
        public bool STONE_SAVANNAS = true;

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // Ravines
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        //public bool ENABLE_RAVINE_MODIFICATIONS;
        public bool ENABLE_RAVINES = false;
        public int RAVINE_FREQUENCY = 50;

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // Rivers
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        
        private float RIVER_SIZE_MULTIPLIER = 1; // This is private because we want a transformed version.
        public float RIVER_FREQUENCY_MULTIPLIER = 1;
        public float RIVER_BENDINESS_MULTIPLIER = 1;
        public float RIVER_CUT_OFF_SCALE = 350;
        public float RIVER_CUT_OFF_AMPLITUDE = 0.5f;
        public bool ENABLE_LUSH_RIVER_BANK_DECORATIONS_IN_HOT_BIOMES = true;
        public bool ENABLE_LUSH_RIVER_BANK_SURFACES_IN_HOT_BIOMES = true;

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // Saplings
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        public bool ENABLE_RTG_SAPLINGS = true;
        public int RTG_TREE_CHANCE = 2;

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // Scattered features
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        public bool ENABLE_SCATTERED_FEATURE_MODIFICATIONS = true;
        public bool GENERATE_SCATTERED_FEATURES = true;
        public int MIN_DISTANCE_SCATTERED_FEATURES = 12;
        public int MAX_DISTANCE_SCATTERED_FEATURES = 48;

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // Snow
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        public bool ENABLE_SNOW_LAYERS = true;

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // Strongholds
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        //public bool ENABLE_STRONGHOLD_MODIFICATIONS;
        public bool GENERATE_STRONGHOLDS = true;
        public int STRONGHOLD_COUNT = 128;
        public int STRONGHOLD_DISTANCE = 32;
        public int STRONGHOLD_SPREAD = 3;

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // Terrain shadowing
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        public int SHADOW_STONE_PIXEL_ID = DEFAULT_SHADOW_STONE_PIXEL_ID;
        public int SHADOW_STONE_PIXEL_META = DEFAULT_SHADOW_STONE_PIXEL_META;
        public int SHADOW_DESERT_PIXEL_ID = DEFAULT_SHADOW_DESERT_PIXEL_ID;
        public int SHADOW_DESERT_PIXEL_META = DEFAULT_SHADOW_DESERT_PIXEL_META;
        //public bool ENABLE_UBC_STONE_SHADOWING;
        //public bool ENABLE_UBC_DESERT_SHADOWING;

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // Trees
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        public bool ALLOW_TREES_TO_GENERATE_ON_SAND = true;
        public bool ALLOW_SHRUBS_TO_GENERATE_BELOW_SURFACE = true;
        public bool ALLOW_BARK_COVERED_LOGS = true;
        public float TREE_DENSITY_MULTIPLIER = 1;

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // Villages
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        //public bool ENABLE_VILLAGE_MODIFICATIONS;
        public bool GENERATE_VILLAGES = true;
        public int VILLAGE_SIZE = 0;
        public int MIN_DISTANCE_VILLAGES = 12;
        public int MAX_DISTANCE_VILLAGES = 48;

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        // Volcanoes
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        public int VOLCANO_PIXEL_ID = Pixels.OBSIDIAN.getPixelID();
        public int VOLCANO_PIXEL_META = 0;
        public int VOLCANO_MIX1_PIXEL_ID = Pixels.COBBLESTONE.getPixelID();
        public int VOLCANO_MIX1_PIXEL_META = 0;
        public int VOLCANO_MIX2_PIXEL_ID = Pixels.GRAVEL.getPixelID();
        public int VOLCANO_MIX2_PIXEL_META = 0;
        public int VOLCANO_MIX3_PIXEL_ID = Pixels.COAL_PIXEL.getPixelID();
        public int VOLCANO_MIX3_PIXEL_META = 0;
        public bool ENABLE_VOLCANOES = true;
        public bool ENABLE_VOLCANO_ERUPTIONS = true;
        public int VOLCANO_CHANCE = 48;

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        public static byte[] getPlateauGradientPixelMetasFromConfigString(string configString)
        {
            string[] strings = configString.Split(',');
            List<byte> byteList = new List<byte>() { };

            for (int i = 0; i < strings.Length; i++)
            {
                strings[i] = strings[i].Trim();
                MatchCollection m = Regex.Matches(strings[i], "-1|0|1|2|3|4|5|6|7|8|9|10|11|12|13|14|15");
                if (m.Count > 0)
                {
                    byteList.Add(byte.Parse(strings[i]));
                }
            }

            byte[] bytes = byteList.ToArray();
            return bytes;
        }

        private static string getPlateauGradientPixelMetasComment(string biomeName)
        {
            string comment =
                "Comma-separated list of meta values for the gradient plateau pixels used in the " + biomeName + "."
                    + '\n' +
                    "-1 = Plateau pixel; 0-15 = Plateau gradient pixel"
                    + '\n' +
                    "0 = White; 1 = Orange; 2 = Magenta; 3 = Light Blue; 4 = Yellow; 5 = Lime; 6 = Pink; 7 = Gray"
                    + '\n' +
                    "8 = Light Gray; 9 = Cyan; 10 = Purple; 11 = Blue; 12 = Brown; 13 = Green; 14 = Red; 15 = Black";

            return comment;
        }

        public float lakeSizeMultiplier()
        {
            // With the river system changing frequency also shinks size and that will
            // confuse the heck out of users.
            return LAKE_SIZE_MULTIPLIER * LAKE_FREQUENCY_MULTIPLIER;
        }

        public float riverSizeMultiplier()
        {
            // With the river system changing frequency also shinks size and that will
            // confuse the heck out of users.
            return RIVER_SIZE_MULTIPLIER * RIVER_FREQUENCY_MULTIPLIER;
        }
    }
}