namespace rtg.settings
{
    using UnityEngine;
    using generic.init;
    using generic.block;

    public class RTGSettings : MonoBehaviour
    {
        //# Configuration file

        //bedrock {
        [Header("Bedrock")]
        //# The block to use for the bottom of the Overworld.
        //#  [default: minecraft:bedrock]
        //S:"Bedrock block ID"=minecraft:bedrock
        [Tooltip("The block to use for the bottom of the Overworld\n[default: 7]")]
        [Range(0, 255)]
        public int bedrockBlockID = 7;

        //# The meta value of the bedrock block.
        //#  [range: 0 ~ 15, default: 0]
        //I:"Bedrock block meta value"=0
        [Tooltip("The meta value of the bedrock block.\n[range: 0 ~ 15, default: 0]")]
        [Range(0, 15)]
        public int bedrockMetaID = 0;

        //# 0 = Normal bedrock (rough pattern); 1-5 = Number of flat bedrock layers to generate
        //#  [range: 0 ~ 5, default: 0]
        //    I:"Number of flat bedrock layers"=0
        [Tooltip("0 = Normal bedrock (rough pattern); 1-5 = Number of flat bedrock layers to generate\n[range: 0 ~ 5, default: 0]")]
        [Range(0, 5)]
        public int bedrockLayerCount = 0;
        //}


        //biomes {
        [Header("Biomes")]
        //# If TRUE, uses the individual biome settings in the biome config files. If FALSE, disables all RTG decorations and uses vanilla decorations instead.
        //#  [default: true]
        //B:"Enable RTG Biome Decorations"=true
        [Tooltip("If TRUE, uses the individual biome settings in the biome config files. If FALSE, disables all RTG decorations and uses vanilla decorations instead.\n[default: true]")]
        public bool enableRTGBiomes = true;

        //# If TRUE, uses the individual biome settings in the biome config files. If FALSE, disables all RTG surfaces and uses vanilla surfaces instead.
        //#  [default: true]
        //B:"Enable RTG Biome Surfaces"=true
        [Tooltip("If TRUE, uses the individual biome settings in the biome config files. If FALSE, disables all RTG surfaces and uses vanilla surfaces instead.\n[default: true]")]
        public bool enableBiomeSurfaces = true;

        //# If RTG tries to generate an unsupported biome or a biome that has an ID conflict, it will generate this biome instead.
        //# If set to -1, RTG will crash instead of generating the patch biome. You might want to do this if you're making a mod pack
        //# and want to make sure all biomes are generating correctly.
        //# Default = Vanilla Plains
        //#  [range: -1 ~ 255, default: 1]
        //I:"Patch Biome ID"=1
        [Tooltip("If RTG tries to generate an unsupported biome or a biome that has an ID conflict, it will generate this biome instead. If set to -1, RTG will crash instead of generating the patch biome.\n[range: -1 ~ 255, default: 1]")]
        [Range(-1, 255)]
        public int patchBiomeID = 1;
        //}


        //boulders {
        [Header("Boulders")]
        //# 1 = Always generate if possible; 2 = 50% chance; 4 = 25% chance
        //#  [range: 1 ~ 100, default: 1]
        //I:"1/x chance that Cobblestone Boulders will generate if given the opportunity to do so during world gen"=1
        [Tooltip("1 = Always generate if possible; 2 = 50% chance; 4 = 25% chance\n[range: 1 ~ 100, default: 1]")]
        [Range(1, 100)]
        public int boulderSpawnChance = 1;

        //#  [default: true]
        //B:"Enable Cobblestone Boulders"=true
        [Tooltip("[default: true]")]
        public bool enableBoulders = true;

        //# Set this to TRUE to allow UBC to override cobblestone boulders.
        //# This setting doesn't have any effect if UBC is not installed.
        //#  [default: true]
        //B:"Enable UBC Boulders"=true
        //---disregard until further notice---
        //}


        //caves {
        [Header("Caves")]
        //# This setting controls the size of caves.
        //# HIGHER values = BIGGER caves & MORE lag. (14 = vanilla cave density)
        //#  [range: 1 ~ 40, default: 8]
        //I:"Cave Density"=8
        [Tooltip("This setting controls the size of caves. HIGHER values = BIGGER caves & MORE lag. (14 = vanilla cave density)\n[range: 1 ~ 40, default: 8]")]
        [Range(1, 40)]
        public int caveDensity = 8;

        //# This setting controls the number of caves that generate.
        //# LOWER values = MORE caves & MORE lag. (6 = vanilla cave frequency)
        //#  [range: 1 ~ 40, default: 16]
        //I:"Cave Frequency"=16
        [Tooltip("This setting controls the number of caves that generate. LOWER values = MORE caves & MORE lag. (6 = vanilla cave frequency)\n[range: 1 ~ 40, default: 16]")]
        [Range(1, 40)]
        public int caveFrequency = 16;

        //# Must be set to TRUE for the other cave settings to have any effect.
        //# If FALSE, RTG won't interfere with cave generation at all.
        //# WARNING! Setting this to FALSE may result in unpredictable cave generation.
        //#  [default: true]
        //B:"Enable Cave Modifications"=true
        //---disregard until further notice---

        //#  [default: true]
        //B:"Enable Caves"=true
        [Tooltip("[default: true]")]
        public bool enableCaves = true;
        //}


        //---disregard until further notice---
        //debugging {
        //# Instead of crashing when it experiences 'java.util.ConcurrentModificationException' (or any other exception)
        //# during structure generation, RTG will stop trying to generate that structure and continue generating the world.
        //# You should only set this to TRUE if you have been instructed to do so by an RTG developer, or if you know what you're doing.
        //#  [default: false]
        //B:"Crash on Structure Exceptions"=false

        //# WARNING: This should only be enabled if you know what you're doing.
        //#  [default: false]
        //B:"Enable Debugging"=false
        //}
        //------------------------------------


        //dunes {
        [Header("Dunes")]
        //# This setting controls the height of both sand dunes and snow dunes.
        //# Higher values = taller dunes.
        //#  [range: 1 ~ 12, default: 4]
        //I:"Height of Dunes"=4
        [Tooltip("This setting controls the height of both sand dunes and snow dunes. Higher values = taller dunes.\n[range: 1 ~ 12, default: 4]")]
        [Range(1, 12)]
        public int heightOfDunes = 4;
        //}


        //dungeons {
        [Header("Dungeons")]
        //# This setting controls the number of dungeons that generate.
        //# HIGHER values = MORE dungeons & MORE lag. (8 = vanilla dungeon frequency)
        //#  [range: 1 ~ 200, default: 8]
        //I:"Dungeon Frequency"=8
        [Tooltip("This setting controls the number of dungeons that generate. HIGHER values = MORE dungeons & MORE lag. (8 = vanilla dungeon frequency)\n[range: 1 ~ 200, default: 8]")]
        [Range(1, 200)]
        public int dungeonFrequency = 8;

        //#  [default: true]
        //B:"Generate Dungeons"=true
        [Tooltip("[default: true]")]
        public bool generateDungeons = true;
        //}


        //"flowing liquids" {
        [Header("Flowing Liquids")]
        //# Must be set to TRUE for the other flowing liquid settings to have any effect.
        //# If FALSE, RTG won't interfere with flowing liquid generation at all.
        //# (Flowing liquids are the water/lava streams that generate on the sides of hills and mountains.)
        //#  [default: true]
        //B:"Enable Flowing Liquid Modifications"=true
        //---disregard until further notice---

        //# 1/x chance that a lava stream will generate on the side of a hill or mountain.
        //# 0 = Never generate; 1 = Always generate if possible; 2 = 50% chance; 4 = 25% chance
        //#  [range: 0 ~ 2147483647, default: 200]
        //I:"Flowing Lava Chance"=200
        [Tooltip("1/x chance that a lava stream will generate on the side of a hill or mountain. 0 = Never generate; 1 = Always generate if possible; 2 = 50% chance; 4 = 25% chance\n[range: 0 ~ 2147483647, default: 200]")]
        [Range(0, 2147483647)]
        public int flowingLavaChance = 200;

        //# 1/x chance that a water stream will generate on the side of a hill or mountain.
        //# 0 = Never generate; 1 = Always generate if possible; 2 = 50% chance; 4 = 25% chance
        //#  [range: 0 ~ 2147483647, default: 200]
        //I:"Flowing Water Chance"=200
        [Tooltip("1/x chance that a water stream will generate on the side of a hill or mountain. 0 = Never generate; 1 = Always generate if possible; 2 = 50% chance; 4 = 25% chance\n[range: 0 ~ 2147483647, default: 200]")]
        [Range(0, 2147483647)]
        public int flowingWaterChance = 200;
        //}


        //---disregard until further notice---
        //gui {
        //#  [default: true]
        //B:"Enable World Type Notification Screen"=true
        //}
        //------------------------------------


        //"lakes (scenic)" {
        [Header("Lakes (Scenic)")]
        //# Biome ID for scenic lakes when frozen (default 11 = Frozen River)
        //#  [range: 0 ~ 254, default: 11]
        //I:"Biome for frozen scenic lakes"=11
        [Tooltip("Biome ID for scenic lakes when frozen (default 11 = Frozen River)\n[range: 0 ~ 254, default: 11]")]
        [Range(0, 254)]
        public int frozenScenicLakesBiomeID = 11;

        //# Biome ID for scenic lakes when not frozen (default 7 = River)
        //#  [range: 0 ~ 254, default: 7]
        //I:"Biome for scenic lakes"=7
        [Tooltip("Biome ID for scenic lakes when not frozen (default 7 = River)\n[range: 0 ~ 254, default: 7]")]
        [Range(0, 254)]
        public int scenicLakesBiomeID = 7;

        //# Defaults to 1 (standard frequency)
        //#  [range: 0.0 ~ 10.0, default: 1.0]
        //S:"Lake Frequency Multiplier"=1.0
        [Tooltip("Defaults to 1 (standard frequency)\n[range: 0.0f ~ 10.0f, default: 1.0f]")]
        [Range(0.0f, 10.0f)]
        public float lakeFrequencyMultiplier = 1.0f;

        //# Makes scenic lake shores bend and curve more. Defaults to 1
        //#  [range: 0.0 ~ 2.0, default: 1.0]
        //S:"Lake Shore Irregularity"=1.0
        [Tooltip("Makes scenic lake shores bend and curve more. Defaults to 1\n[range: 0.0f ~ 2.0f, default: 1.0f]")]
        [Range(0.0f, 2.0f)]
        public float lakeShoreIrregularity = 1.0f;

        //# Defaults to 1 (standard size)
        //#  [range: 0.0 ~ 10.0, default: 1.0]
        //S:"Lake Size Multiplier"=1.0
        [Tooltip("Defaults to 1 (standard size)\n[range: 0.0f ~ 10.0f, default: 1.0f]")]
        [Range(0.0f, 10.0f)]
        public float lakeSizeMultiplier = 1.0f;
        //}


        //"lakes (surface)" {
        [Header("Lakes (Surface)")]
        //# 1 = Always generate if possible; 2 = 50% chance; 4 = 25% chance
        //#  [range: 1 ~ 100, default: 10]
        //I:"1/x chance that Lava Surface Lakes will generate if given the opportunity to do so during world gen"=10
        [Tooltip("1 = Always generate if possible; 2 = 50% chance; 4 = 25% chance\n[range: 1 ~ 100, default: 10]")]
        [Range(1, 100)]
        public int surfaceLavaChance = 10;

        //# 1 = Always generate if possible; 2 = 50% chance; 4 = 25% chance
        //#  [range: 1 ~ 100, default: 10]
        //I:"1/x chance that Water Surface Lakes will generate if given the opportunity to do so during world gen"=10
        [Tooltip("1 = Always generate if possible; 2 = 50% chance; 4 = 25% chance\n[range: 1 ~ 100, default: 10]")]
        [Range(1, 100)]
        public int surfaceWaterChance = 10;

        //#  [default: true]
        //B:"Enable Lava Surface Lakes"=true
        [Tooltip("[default: true]")]
        public bool enableSurfaceLavaLakes = true;

        //#  [default: true]
        //B:"Enable Water Surface Lakes"=true
        [Tooltip("[default: true]")]
        public bool enableSurfaceWaterLakes = true;
        //}


        //"lakes (underground)" {
        [Header("Lakes (Underground)")]
        //# 1 = Always generate if possible; 2 = 50% chance; 4 = 25% chance
        //#  [range: 1 ~ 100, default: 10]
        //I:"1/x chance that Lava Underground Lakes will generate if given the opportunity to do so during world gen"=10
        [Tooltip("1 = Always generate if possible; 2 = 50% chance; 4 = 25% chance\n[range: 1 ~ 100, default: 10]")]
        [Range(1, 100)]
        public int undergroundLavaChance = 10;

        //# 1 = Always generate if possible; 2 = 50% chance; 4 = 25% chance
        //#  [range: 1 ~ 100, default: 10]
        //I:"1/x chance that Water Underground Lakes will generate if given the opportunity to do so during world gen"=10
        [Tooltip("1 = Always generate if possible; 2 = 50% chance; 4 = 25% chance\n[range: 1 ~ 100, default: 10]")]
        [Range(1, 100)]
        public int undergroundWaterChance = 10;

        //#  [default: true]
        //B:"Enable Lava Underground Lakes"=true
        [Tooltip("[default: true]")]
        public bool enableUndergroundLavaLakes = true;

        //#  [default: true]
        //B:"Enable Water Underground Lakes"=true
        [Tooltip("[default: true]")]
        public bool enableUndergroundWaterLakes = true;
        //}


        //---disregard until further notice---
        //mineshafts {
        //#  [default: true]
        //B:"Generate Mineshafts"=true
        //}
        //------------------------------------


        //---disregard until further notice---
        //"ocean monuments" {
        //# Must be set to TRUE for the other ocean monument settings to have any effect.
        //# If FALSE, RTG won't interfere with ocean monument generation at all.
        //# WARNING! Setting this to FALSE may result in ocean monuments generating in unpredictable locations, including those outside of oceanic biomes.
        //#  [default: true]
        //B:"Enable Ocean Monument Modifications"=true

        //#  [default: true]
        //B:"Generate Ocean Monuments"=true

        //# This setting determines the minimum distance, in chunks, between ocean monuments.
        //# LOWER values = MORE monuments & MORE lag. (5 = Vanilla separation)
        //# This value MUST be less than the 'spacing' value.
        //#  [range: 1 ~ 2147483647, default: 5]
        //I:"Ocean Monument Separation"=5

        //# This setting determines the size of the grid, in chunks, on which ocean monuments are generated.
        //# LOWER values = MORE monuments & MORE lag. (32 = Vanilla spacing)
        //# This value MUST be greater than the 'separation' value.
        //#  [range: 1 ~ 1024, default: 32]
        //I:"Ocean Monument Spacing"=32
        //}
        //------------------------------------


        //"ore gen" {
        [Header("Lakes (Underground)")]
        //# Some mods might not be compatible with the way RTG handles ore generation.
        //# If you're using one of those mods, you might need to set this to false.
        //# You should only change this if you're having problems with ore gen and you know what you're doing.
        //#  [default: true]
        //B:"Allow ore gen event cancellation"=true

        //#  [default: true]
        //B:"Generate Andesite Ore"=true
        [Tooltip("[default: true]")]
        public bool generateAndesiteOre = true;

        //#  [default: true]
        //B:"Generate Coal Ore"=true
        [Tooltip("[default: true]")]
        public bool generateCoalOre = true;

        //#  [default: true]
        //B:"Generate Diamond Ore"=true
        [Tooltip("[default: true]")]
        public bool generateDiamondOre = true;

        //#  [default: true]
        //B:"Generate Diorite Ore"=true
        [Tooltip("[default: true]")]
        public bool generateDioriteOre = true;

        //#  [default: true]
        //B:"Generate Dirt Ore"=true
        [Tooltip("[default: true]")]
        public bool generateDirtOre = true;

        //#  [default: true]
        //B:"Generate Emerald Ore"=true
        [Tooltip("[default: true]")]
        public bool generateEmeraldOre = true;

        //#  [default: true]
        //B:"Generate Gold Ore"=true
        [Tooltip("[default: true]")]
        public bool generateGoldOre = true;

        //#  [default: true]
        //B:"Generate Granite Ore"=true
        [Tooltip("[default: true]")]
        public bool generateGraniteOre = true;

        //#  [default: true]
        //B:"Generate Gravel Ore"=true
        [Tooltip("[default: true]")]
        public bool generateGravelOre = true;

        //#  [default: true]
        //B:"Generate Iron Ore"=true
        [Tooltip("[default: true]")]
        public bool generateIronOre = true;

        //#  [default: true]
        //B:"Generate Lapis Ore"=true
        [Tooltip("[default: true]")]
        public bool generateLapisOre = true;

        //#  [default: true]
        //B:"Generate Redstone Ore"=true
        //---disregard until further notice---

        //#  [default: true]
        //B:"Generate Silverfish Ore"=true
        //---disregard until further notice---
        //}


        //plateaus {
        [Header("Plateaus")]
        //# The block to use for Mesa & Savanna plateau gradients. Defaults to stained hardened clay.
        //# This can be any block, but it works best with blocks that have multiple colours, such as stained hardened clay.
        //# The various 'meta' options in this section will use this block to configure the plateau gradients.
        //#  [default: minecraft:stained_hardened_clay]
        //S:"Gradient Plateau Block ID"=minecraft:stained_hardened_clay
        [Tooltip("The block to use for Mesa & Savanna plateau gradients. Defaults to stained hardened clay. This can be any block, but it works best with blocks that have multiple colours, such as stained hardened clay. The various 'meta' options in this section will use this block to configure the plateau gradients.\n[default: 159]")]
        [Range(0, 255)]
        public int gradientPlateauBlockID = 159;

        //# Comma-separated list of meta values for the gradient plateau blocks used in the Mesa Bryce biome.
        //# -1 = Plateau block; 0-15 = Plateau gradient block
        //# 0 = White; 1 = Orange; 2 = Magenta; 3 = Light Blue; 4 = Yellow; 5 = Lime; 6 = Pink; 7 = Gray
        //# 8 = Light Gray; 9 = Cyan; 10 = Purple; 11 = Blue; 12 = Brown; 13 = Green; 14 = Red; 15 = Black
        //#  [default: -1,-1,0,1,0,0,0,14,0,8,0,1,8,0,-1,0,14,0,0,14,0,0,8]
        //S:"Gradient Plateau Block Meta Values (Mesa Bryce)"=-1,-1,0,1,0,0,0,14,0,8,0,1,8,0,-1,0,14,0,0,14,0,0,8
        [Tooltip("Comma-separated list of meta values for the gradient plateau blocks used in the Mesa Bryce biome. -1 = Plateau block; 0-15 = Plateau gradient block\n0 = White; 1 = Orange; 2 = Magenta; 3 = Light Blue; 4 = Yellow; 5 = Lime; 6 = Pink; 7 = Gray\n8 = Light Gray; 9 = Cyan; 10 = Purple; 11 = Blue; 12 = Brown; 13 = Green; 14 = Red; 15 = Black\n[default: -1,-1,0,1,0,0,0,14,0,8,0,1,8,0,-1,0,14,0,0,14,0,0,8]")]
        public string gradientPlateauMetaMesaBryceID = "-1,-1,0,1,0,0,0,14,0,8,0,1,8,0,-1,0,14,0,0,14,0,0,8";

        //# Comma-separated list of meta values for the gradient plateau blocks used in the Mesa biome variants (doesn't include Mesa Bryce).
        //# -1 = Plateau block; 0-15 = Plateau gradient block
        //# 0 = White; 1 = Orange; 2 = Magenta; 3 = Light Blue; 4 = Yellow; 5 = Lime; 6 = Pink; 7 = Gray
        //# 8 = Light Gray; 9 = Cyan; 10 = Purple; 11 = Blue; 12 = Brown; 13 = Green; 14 = Red; 15 = Black
        //#  [default: 0,1,8,14,1,8]
        //S:"Gradient Plateau Block Meta Values (Mesa)"=0,1,8,14,1,8
        [Tooltip("Comma-separated list of meta values for the gradient plateau blocks used in the Mesa biome variants (doesn't include Mesa Bryce). -1 = Plateau block; 0-15 = Plateau gradient block\n0 = White; 1 = Orange; 2 = Magenta; 3 = Light Blue; 4 = Yellow; 5 = Lime; 6 = Pink; 7 = Gray\n8 = Light Gray; 9 = Cyan; 10 = Purple; 11 = Blue; 12 = Brown; 13 = Green; 14 = Red; 15 = Black\n[default: 0,1,8,14,1,8]")]
        public string gradientPlateauMetaMesaID = "0,1,8,14,1,8";

        //# Comma-separated list of meta values for the gradient plateau blocks used in the Savanna biome variants.
        //# -1 = Plateau block; 0-15 = Plateau gradient block
        //# 0 = White; 1 = Orange; 2 = Magenta; 3 = Light Blue; 4 = Yellow; 5 = Lime; 6 = Pink; 7 = Gray
        //# 8 = Light Gray; 9 = Cyan; 10 = Purple; 11 = Blue; 12 = Brown; 13 = Green; 14 = Red; 15 = Black
        //#  [default: 0,0,0,0,8,8,12,12,8,0,8,12,12,8,12,8,0,0,8,12,12]
        //S:"Gradient Plateau Block Meta Values (Savanna)"=0,0,0,0,8,8,12,12,8,0,8,12,12,8,12,8,0,0,8,12,12
        [Tooltip("Comma-separated list of meta values for the gradient plateau blocks used in the Savanna biome variants. -1 = Plateau block; 0-15 = Plateau gradient block\n0 = White; 1 = Orange; 2 = Magenta; 3 = Light Blue; 4 = Yellow; 5 = Lime; 6 = Pink; 7 = Gray\n8 = Light Gray; 9 = Cyan; 10 = Purple; 11 = Blue; 12 = Brown; 13 = Green; 14 = Red; 15 = Black\n[default: 0,0,0,0,8,8,12,12,8,0,8,12,12,8,12,8,0,0,8,12,12]")]
        public string gradientPlateauMetaSavannaID = "0,0,0,0,8,8,12,12,8,0,8,12,12,8,12,8,0,0,8,12,12";

        //# An extra block to use for Mesa & Savanna plateau gradients. Defaults to hardened clay.
        //# When configuring the various 'meta' options in this section, use a value of '-1' to reference this block.
        //#  [default: minecraft:hardened_clay]
        //S:"Plateau Block ID"=minecraft:hardened_clay
        [Tooltip("An extra block to use for Mesa & Savanna plateau gradients. Defaults to hardened clay. When configuring the various 'meta' options in this section, use a value of '-1' to reference this block.\n[default: 172]")]
        [Range(0, 255)]
        public int plateauBlockID = 172;

        //# The meta value of the plateau block.
        //#  [range: 0 ~ 15, default: 0]
        //I:"Plateau Block Meta Value"=0
        [Tooltip("The meta value of the plateau block.\n[range: 0 ~ 15, default: 0]")]
        [Range(0, 15)]
        public int plateauMetaID = 0;

        //# If set to TRUE, Savanna biome variants will mostly use stone/cobblestone instead of gradient blocks for cliffs and plateaus.
        //# Savanna Plateau M will always use gradient blocks.
        //#  [default: true]
        //B:"Use stone for most Savanna biome variants"=true
        [Tooltip("If set to TRUE, Savanna biome variants will mostly use stone/cobblestone instead of gradient blocks for cliffs and plateaus. Savanna Plateau M will always use gradient blocks.\n[default: true]")]
        public bool useStoneForSavannaBiomeVariants = true;
        //}


        //ravines {
        [Header("Ravines")]
        //# Must be set to TRUE for the other ravine settings to have any effect.
        //# If FALSE, RTG won't interfere with ravine generation at all.
        //# WARNING! Setting this to FALSE may result in unpredictable ravine generation.
        //#  [default: true]
        //B:"Enable Ravine Modifications"=true
        //---disregard until further notice---

        //#  [default: false]
        //B:"Enable Ravines"=false
        [Tooltip("[default: false]")]
        public bool enableRavines = false;

        //# This setting controls the number of ravines that generate.
        //# LOWER values = MORE ravines & MORE lag. (50 = vanilla ravine frequency)
        //#  [range: 1 ~ 100, default: 50]
        //I:"Ravine Frequency"=50
        [Tooltip("This setting controls the number of ravines that generate. LOWER values = MORE ravines & MORE lag. (50 = vanilla ravine frequency)\n[range: 1 ~ 100, default: 50]")]
        [Range(1, 100)]
        public int ravineFrequency = 50;
        //}


        //rivers {
        [Header("Rivers")]
        //# Higher numbers make the large-scale cut-off noise have a greater effect. Defaults to 0.5
        //#  [range: 0.0 ~ 2.0, default: 0.5]
        //S:"Amplitude of Large-Scale River Cut Off"=0.5
        [Tooltip("Higher numbers make the large-scale cut-off noise have a greater effect. Defaults to 0.5\n[range: 0.0f ~ 2.0f, default: 0.5f]")]
        [Range(0.0f, 2.0f)]
        public float largeRiverCutOffAmplitude = 0.5f;

        //# Set this to FALSE to prevent RTG from generating lush river bank decorations in hot biomes, like Desert and Mesa.
        //# Lush decorations consist of tallgrass, trees, shrubs, and other flora.
        //#  [default: true]
        //B:"Enable Lush River Bank Decorations in Hot Biomes"=true
        [Tooltip("Set this to FALSE to prevent RTG from generating lush river bank decorations in hot biomes, like Desert and Mesa. Lush decorations consist of tallgrass, trees, shrubs, and other flora.\n[default: true]")]
        public bool enableLushRiverBankDecorationsInHotBiomes = true;

        //# Set this to FALSE to prevent RTG from generating lush river bank surfaces in hot biomes, like Desert and Mesa.
        //# Lush surfaces consist (almost exclusively) of grass blocks.
        //#  [default: true]
        //B:"Enable Lush River Bank Surfaces in Hot Biomes"=true
        [Tooltip("Set this to FALSE to prevent RTG from generating lush river bank surfaces in hot biomes, like Desert and Mesa. Lush surfaces consist (almost exclusively) of grass blocks.\n[default: true]")]
        public bool enableLushRiverBankSurfacesInHotBiomes = true;

        //# Higher numbers make rivers bend more. Defaults to 1
        //#  [range: 0.0 ~ 2.0, default: 1.0]
        //S:"Multiplier to River Bending"=1.0
        [Tooltip("Higher numbers make rivers bend more. Defaults to 1\n[range: 0.0f ~ 2.0f, default: 1.0f]")]
        [Range(0.0f, 2.0f)]
        public float riverBendingMultiplier = 1.0f;

        //# Multiplier to river frequencies. Defaults to 1
        //#  [range: 0.0 ~ 10.0, default: 1.0]
        //S:"River Frequency Multiplier"=1.0
        [Tooltip("Multiplier to river frequencies. Defaults to 1\n[range: 0.0f ~ 10.0f, default: 1.0f]")]
        [Range(0.0f, 10.0f)]
        public float riverFrequencyMultiplier = 1.0f;

        //# Defaults to 1 (standard width)
        //#  [range: 0.0 ~ 10.0, default: 1.0]
        //S:"River Width Multiplier"=1.0
        [Tooltip("Defaults to 1 (standard width)\n[range: 0.0f ~ 10.0f, default: 1.0f]")]
        [Range(0.0f, 10.0f)]
        public float riverWidthMultiplier = 1.0f;

        //# Higher numbers make grassy areas near rivers bigger, but also more rare. Defaults to 350
        //#  [range: 50.0 ~ 5000.0, default: 350.0]
        //S:"Scale of Large-Scale River Cut Off"=350.0
        [Tooltip("Higher numbers make grassy areas near rivers bigger, but also more rare. Defaults to 350\n[range: 50.0f ~ 5000.0f, default: 350.0f]")]
        [Range(50.0f, 5000.0f)]
        public float largeRiverCutOffScale = 350.0f;
        //}


        //saplings {
        [Header("Saplings")]
        //# Set this to TRUE to allow RTG's custom trees to grow from vanilla saplings.
        //# RTG's custom trees can be grown only from the saplings that their leaves would drop naturally, and only in the biomes where they naturally generate.
        //# For example, you can only grow a Swamp Willow in a Swamp biome, and only with an Oak sapling (because Swamp Willows have Oak leaves).
        //#  [default: true]
        //B:"Enable RTG Saplings"=true
        [Tooltip("Set this to TRUE to allow RTG's custom trees to grow from vanilla saplings. RTG's custom trees can be grown only from the saplings that their leaves would drop naturally, and only in the biomes where they naturally generate. For example, you can only grow a Swamp Willow in a Swamp biome, and only with an Oak sapling (because Swamp Willows have Oak leaves).\n[default: true]")]
        public bool enableRTGSaplings = true;

        //# 1/x chance that a vanilla sapling will grow one of RTG's custom trees.
        //# 1 = Always generate if possible; 2 = 50% chance; 4 = 25% chance
        //#  [range: 1 ~ 2147483647, default: 2]
        //I:"RTG Tree from Vanilla Sapling Chance"=2
        [Tooltip("1/x chance that a vanilla sapling will grow one of RTG's custom trees. 1 = Always generate if possible; 2 = 50% chance; 4 = 25% chance\n[range: 1 ~ 2147483647, default: 2]")]
        [Range(1, 2147483647)]
        public int saplingSpawnChance = 2;
        //}


        //"scattered features" {
        [Header("Scattered Features")]
        //# Must be set to TRUE for the other scattered feature settings to have any effect.
        //# If FALSE, RTG won't interfere with scattered feature generation at all.
        //# WARNING! Setting this to FALSE may result in unpredictable scattered feature generation.
        //#  [default: true]
        //B:"Enable Scattered Feature Modifications"=true
        //---disregard until further nortice---

        //#  [default: true]
        //B:"Generate Scattered Features"=true
        [Tooltip("[default: true]")]
        public bool generateScatteredFeatures = true;

        //# Scattered features = desert temples, jungle temples, and witch huts; 32 = Vanilla
        //#  [range: 4 ~ 2147483647, default: 48]
        //I:"Maximum distance between scattered features"=48
        [Tooltip("Scattered features = desert temples, jungle temples, and witch huts; 32 = Vanilla\n[range: 4 ~ 2147483647, default: 48]")]
        [Range(4, 2147483647)]
        public int maxDistanceBetweenScatteredFeatures = 48;

        //# Scattered features = desert temples, jungle temples, and witch huts; 8 = Vanilla
        //#  [range: 2 ~ 2147483647, default: 12]
        //I:"Minimum distance between scattered features"=12
        [Tooltip("Scattered features = desert temples, jungle temples, and witch huts; 8 = Vanilla\n[range: 2 ~ 2147483647, default: 12]")]
        [Range(2, 2147483647)]
        public int minDistanceBetweenScatteredFeatures = 12;
        //}


        //snow {
        [Header("Snow")]
        //# This applies to newly-generated chunks only. Snow layers will still appear in cold/snowy biomes after it snows.
        //#  [default: true]
        //B:"Enable Snow Layers"=true
        [Tooltip("This applies to newly-generated chunks only. Snow layers will still appear in cold/snowy biomes after it snows.\n[default: true]")]
        public bool enableSnowLayers = true;
        //}


        //---disregard until further notice---
        //strongholds {
        //# Must be set to TRUE for the other stronghold settings to have any effect.
        //# If FALSE, RTG won't interfere with stronghold generation at all.
        //# WARNING! Setting this to FALSE may result in unpredictable stronghold generation.
        //#  [default: true]
        //B:"Enable Stronghold Modifications"=true

        //#  [default: true]
        //B:"Generate Strongholds"=true

        //# This setting is the number of strongholds that exist per world.
        //# HIGHER values = MORE strongholds & MORE lag. (128 = Vanilla)
        //#  [range: 1 ~ 2147483647, default: 128]
        //I:"Stronghold Count"=128

        //# This setting determines how far strongholds are from the spawn and other strongholds.
        //# LOWER values = MORE strongholds & MORE lag. (32 = Vanilla)
        //#  [range: 1 ~ 2147483647, default: 32]
        //I:"Stronghold Distance"=32

        //# This setting determines how concentrated strongholds are around the spawn.
        //# LOWER values = LOWER concentration around spawn. (3 = Vanilla)
        //#  [range: 1 ~ 2147483647, default: 3]
        //I:"Stronghold Spread"=3
        //}
        //------------------------------------


        //"terrain shadowing" {
        [Header("Terrain Shadowing")]
        //# The block to use for desert terrain shadowing, typically seen on the cliffs of desert mountains. Defaults to stained hardened clay.
        //#  [default: minecraft:stained_hardened_clay]
        //S:"Desert shadow block ID"=minecraft:stained_hardened_clay
        [Tooltip("The block to use for desert terrain shadowing, typically seen on the cliffs of desert mountains. Defaults to stained hardened clay.\n[default: 159]")]
        [Range(0, 255)]
        public int desertShadowBlockID = 159;

        //# The meta value of the shadow block for desert cliffs. Defaults to 0 (white).
        //#  [range: 0 ~ 15, default: 0]
        //I:"Desert shadow block meta value"=0
        [Tooltip("The meta value of the shadow block for desert cliffs. Defaults to 0 (white).\n[range: 0 ~ 15, default: 0]")]
        [Range(0, 15)]
        public int desertShadowMetaID = 0;

        //# The block to use for stone terrain shadowing, typically seen on the cliffs of stone mountains. Defaults to stained hardened clay.
        //#  [default: minecraft:stained_hardened_clay]
        //S:"Stone shadow block ID"=minecraft:stained_hardened_clay
        [Tooltip("The block to use for stone terrain shadowing, typically seen on the cliffs of stone mountains. Defaults to stained hardened clay.\n[default: 159]")]
        [Range(0, 255)]
        public int stoneShadowBlockID = 159;

        //# The meta value of the shadow block for stone cliffs. Defaults to 9 (cyan).
        //#  [range: 0 ~ 15, default: 9]
        //I:"Stone shadow block meta value"=9
        [Tooltip("The meta value of the shadow block for stone cliffs. Defaults to 9 (cyan).\n[range: 0 ~ 15, default: 9]")]
        [Range(0, 15)]
        public int stoneShadowMetaID = 9;

        //---disregard until further notice---
        //# Set this to TRUE to allow UBC to override desert shadowing.
        //# This setting doesn't have any effect if UBC is not installed.
        //#  [default: true]
        //B:"UBC Mode (Desert)"=true

        //# Set this to TRUE to allow UBC to override stone shadowing.
        //# This setting doesn't have any effect if UBC is not installed.
        //#  [default: true]
        //B:"UBC Mode (Stone)"=true
        //------------------------------------
        //}


        //trees {
        [Header("Trees")]
        //# Set this to FALSE to prevent shrub trunks from generating below the surface.
        //#  [default: true]
        //B:"Allow Shrubs to Generate Below Surface"=true
        [Tooltip("Set this to FALSE to prevent shrub trunks from generating below the surface.\n[default: true]")]
        public bool allowShrubsBelowSurface = true;

        //# Set this to FALSE to prevent trees from generating on sand.
        //# This setting only affects trees generated by RTG. Trees generated by a biome's decorator
        //# will adhere to their own generation rules. (RTG's Palm Trees ignore this setting.)
        //#  [default: false]
        //B:"Allow Trees to Generate on Sand"=false
        [Tooltip("Set this to FALSE to prevent trees from generating on sand. This setting only affects trees generated by RTG. Trees generated by a biome's decorator will adhere to their own generation rules. (RTG's Palm Trees ignore this setting.)\n[default: false]")]
        public bool allowTreesOnSand = false;

        //---disregard until further notice---
        //# Set this to FALSE to prevent the trunks of RTG trees from using the 'all-bark' texture model.
        //# For more information, visit http://minecraft.gamepedia.com/Wood#Block_data
        //#  [default: true]
        //B:"Allow bark-covered logs"=true
        //------------------------------------

        //# This setting allows you to increase/decrease the number of RTG trees that generate.
        //# This setting only affects trees generated by RTG. Trees generated by a biome's decorator
        //# will adhere to their own density rules.
        //# You can override this setting on a per-biome basis by using the biome configs.
        //# 1.0 = Default tree generation; 2.0 = Twice as many trees; 0.5 = half as many trees
        //#  [range: 0.0 ~ 5.0, default: 1.0]
        //S:"RTG Tree Density Multiplier"=1.0
        [Tooltip("This setting allows you to increase/decrease the number of RTG trees that generate. This setting only affects trees generated by RTG. Trees generated by a biome's decorator will adhere to their own density rules. You can override this setting on a per-biome basis by using the biome configs. 1.0 = Default tree generation; 2.0 = Twice as many trees; 0.5 = half as many trees\n[range: 0.0f ~ 5.0f, default: 1.0f]")]
        [Range(0.0f, 5.0f)]
        public float RTGTreeDensityMultiplier = 1.0f;
        //}


        //villages {
        [Header("Villages")]
        //# Set this to FALSE to resolve issues with mods that also modify villages.
        //# If set to FALSE, the 'Minimum distance between villages', 'Maximum distance between villages' & 'Size of villages' settings will have no effect.
        //# WARNING! Setting this to FALSE may result in unpredictable village generation.
        //#  [default: true]
        //B:"Enable village modifications"=true
        //---disregard until further notice---

        //#  [default: true]
        //B:"Generate Villages"=true
        [Tooltip("[default: true]")]
        public bool generateVillages = true;

        //# Lower values = villages closer together; 32 = Vanilla
        //#  [range: 1 ~ 2147483647, default: 48]
        //I:"Maximum distance between villages"=48
        [Tooltip("Lower values = villages closer together; 32 = Vanilla\n[range: 1 ~ 2147483647, default: 48]")]
        [Range(1, 2147483647)]
        public int maxDistanceBetweenVillages = 48;

        //# Higher values = villages further apart; 8 = Vanilla
        //#  [range: 1 ~ 2147483647, default: 12]
        //I:"Minimum distance between villages"=12
        [Tooltip("Higher values = villages further apart; 8 = Vanilla\n[range: 1 ~ 2147483647, default: 12]")]
        [Range(1, 2147483647)]
        public int minDistanceBetweenVillages = 12;

        //# Higher values = bigger villages; 0 = Vanilla
        //#  [range: 0 ~ 10, default: 0]
        //I:"Size of villages"=0
        [Tooltip("Higher values = bigger villages; 0 = Vanilla\n[range: 0 ~ 10, default: 0]")]
        [Range(1, 2147483647)]
        public int villageSize = 0;
        //}


        //volcanoes {
        [Header("Volcanoes")]
        //# Set this to FALSE to prevent lava from flowing down the sides of volcanoes.
        //#  [default: true]
        //B:"Enable volcano eruptions"=true
        [Tooltip("Set this to FALSE to prevent lava from flowing down the sides of volcanoes.\n[default: true]")]
        public bool enableVolcanoEruptions = true;

        //# Set this to FALSE to prevent volcanoes from generating.
        //#  [default: true]
        //B:"Enable volcanoes"=true
        [Tooltip("Set this to FALSE to prevent volcanoes from generating.\n[default: true]")]
        public bool enableVolcanoes = true;

        //# 1/x chance that a volcano will generate in a biome that has volcanoes enabled.
        //# 1 = Always generate if possible; 2 = 50% chance; 4 = 25% chance
        //#  [range: 1 ~ 2147483647, default: 48]
        //I:"Volcano Chance"=48
        [Tooltip("1/x chance that a volcano will generate in a biome that has volcanoes enabled. 1 = Always generate if possible; 2 = 50% chance; 4 = 25% chance\n[range: 1 ~ 2147483647, default: 48]")]
        [Range(1, 2147483647)]
        public int volcanoChance = 48;

        //# The main block to use for the surface of the volcano.
        //#  [default: minecraft:obsidian]
        //S:"Volcano block ID"=minecraft:obsidian
        [Tooltip("The main block to use for the surface of the volcano.\n[default: 49]")]
        [Range(0, 255)]
        public int volcanoBlockID = 49;

        //# The meta value of the volcano block.
        //#  [range: 0 ~ 15, default: 0]
        //I:"Volcano block meta value"=0
        [Tooltip("The meta value of the volcano block.\n[range: 0 ~ 15, default: 0]")]
        [Range(0, 15)]
        public int volcanoMetaID = 0;

        //# The block ID of the 1st volcano mix block.
        //#  [default: minecraft:cobblestone]
        //S:"Volcano mix 1 block ID"=minecraft:cobblestone
        [Tooltip("The block ID of the 1st volcano mix block.\n[default: 4]")]
        [Range(0, 255)]
        public int volcanoMix1BlockID = 4;

        //# The meta value of the 1st volcano mix block.
        //#  [range: 0 ~ 15, default: 0]
        //I:"Volcano mix 1 block meta value"=0
        [Tooltip("The meta value of the 1st volcano mix block.\n[range: 0 ~ 15, default: 0]")]
        [Range(0, 15)]
        public int volcanoMix1MetaID = 0;

        //# The block ID of the 2nd volcano mix block.
        //#  [default: minecraft:gravel]
        //S:"Volcano mix 2 block ID"=minecraft:gravel
        [Tooltip("The block ID of the 2nd volcano mix block.\n[default: 13]")]
        [Range(0, 255)]
        public int volcanoMix2BlockID = 13;

        //# The meta value of the 2nd volcano mix block.
        //#  [range: 0 ~ 15, default: 0]
        //I:"Volcano mix 2 block meta value"=0
        [Tooltip("The meta value of the 2nd volcano mix block.\n[range: 0 ~ 15, default: 0]")]
        [Range(0, 15)]
        public int volcanoMix2MetaID = 0;

        //# The block ID of the 3rd volcano mix block.
        //#  [default: minecraft:coal_block]
        //S:"Volcano mix 3 block ID"=minecraft:coal_block
        [Tooltip("The block ID of the 3rd volcano mix block.\n[default: 173]")]
        [Range(0, 255)]
        public int volcanoMix3BlockID = 173;

        //# The meta value of the 3rd volcano mix block.
        //#  [range: 0 ~ 15, default: 0]
        //I:"Volcano mix 3 block meta value"=0
        [Tooltip("The meta value of the 3rd volcano mix block.\n[range: 0 ~ 15, default: 0]")]
        [Range(0, 15)]
        public int volcanoMix3MetaID = 0;
        //}
    }
}