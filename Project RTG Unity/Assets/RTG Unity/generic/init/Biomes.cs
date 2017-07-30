namespace generic.init
{
    public class Biomes
    {
        //Normal
        public static world.biome.Biome OCEAN = new world.biome.Biome(0, 0.5f, world.biome.Biome.Type.OCEAN);
        public static world.biome.Biome PLAINS = new world.biome.Biome(1, 0.8f, world.biome.Biome.Type.PLAINS);
        public static world.biome.Biome DESERT = new world.biome.Biome(2, 2.0f, world.biome.Biome.Type.HOT, world.biome.Biome.Type.DRY, world.biome.Biome.Type.SANDY);
        public static world.biome.Biome EXTREME_HILLS = new world.biome.Biome(3, 0.2f, world.biome.Biome.Type.MOUNTAIN, world.biome.Biome.Type.HILLS);
        public static world.biome.Biome FOREST = new world.biome.Biome(4, 0.7f, world.biome.Biome.Type.FOREST);
        public static world.biome.Biome TAIGA = new world.biome.Biome(5, 0.25f, world.biome.Biome.Type.COLD, world.biome.Biome.Type.CONIFEROUS, world.biome.Biome.Type.FOREST);
        public static world.biome.Biome SWAMPLAND = new world.biome.Biome(6, 0.8f, world.biome.Biome.Type.WET, world.biome.Biome.Type.SWAMP);
        public static world.biome.Biome RIVER = new world.biome.Biome(7, 0.5f, world.biome.Biome.Type.RIVER);
        //public static world.biome.Biome HELL = new world.biome.Biome(8, 0.0f, world.biome.Biome.Type.HOT, world.biome.Biome.Type.DRY, world.biome.Biome.Type.HELL);
        //public static world.biome.Biome SKY = new world.biome.Biome(9, 0.5f, world.biome.Biome.Type.COLD, world.biome.Biome.Type.DRY, world.biome.Biome.Type.SKY);
        public static world.biome.Biome FROZEN_OCEAN = new world.biome.Biome(10, 0.0f, world.biome.Biome.Type.COLD, world.biome.Biome.Type.OCEAN, world.biome.Biome.Type.SNOWY);
        public static world.biome.Biome FROZEN_RIVER = new world.biome.Biome(11, 0.0f, world.biome.Biome.Type.COLD, world.biome.Biome.Type.RIVER, world.biome.Biome.Type.SNOWY);
        public static world.biome.Biome ICE_FLATS = new world.biome.Biome(12, 0.0f, world.biome.Biome.Type.COLD, world.biome.Biome.Type.SNOWY, world.biome.Biome.Type.WASTELAND);
        public static world.biome.Biome ICE_MOUNTAINS = new world.biome.Biome(13, ICE_FLATS.getTemperature(), world.biome.Biome.Type.COLD, world.biome.Biome.Type.SNOWY, world.biome.Biome.Type.MOUNTAIN);
        public static world.biome.Biome MUSHROOM_ISLAND = new world.biome.Biome(14, 0.9f, world.biome.Biome.Type.MUSHROOM);        //can be used for bonus or secret islands
        public static world.biome.Biome MUSHROOM_ISLAND_SHORE = new world.biome.Biome(15, 0.9f, world.biome.Biome.Type.MUSHROOM, world.biome.Biome.Type.BEACH);  //can be used for bonus or secret islands
        public static world.biome.Biome BEACHES = new world.biome.Biome(16, 0.8f, world.biome.Biome.Type.BEACH);
        public static world.biome.Biome DESERT_HILLS = new world.biome.Biome(17, DESERT.getTemperature(), world.biome.Biome.Type.HOT, world.biome.Biome.Type.DRY, world.biome.Biome.Type.SANDY, world.biome.Biome.Type.HILLS);
        public static world.biome.Biome FOREST_HILLS = new world.biome.Biome(18, FOREST.getTemperature(), world.biome.Biome.Type.FOREST, world.biome.Biome.Type.HILLS);
        public static world.biome.Biome TAIGA_HILLS = new world.biome.Biome(19, TAIGA.getTemperature(), world.biome.Biome.Type.COLD, world.biome.Biome.Type.CONIFEROUS, world.biome.Biome.Type.FOREST, world.biome.Biome.Type.HILLS);
        public static world.biome.Biome SMALLER_EXTREME_HILLS = new world.biome.Biome(20, 0.2f, world.biome.Biome.Type.HILLS); //Not entirely sure what's supposed to be here, so I'll assume it's supposed to be HILLS for now.
        public static world.biome.Biome JUNGLE = new world.biome.Biome(21, 0.95f, world.biome.Biome.Type.HOT, world.biome.Biome.Type.WET, world.biome.Biome.Type.DENSE, world.biome.Biome.Type.JUNGLE);
        public static world.biome.Biome JUNGLE_HILLS = new world.biome.Biome(22, JUNGLE.getTemperature(), world.biome.Biome.Type.HOT, world.biome.Biome.Type.WET, world.biome.Biome.Type.DENSE, world.biome.Biome.Type.JUNGLE, world.biome.Biome.Type.HILLS);
        public static world.biome.Biome JUNGLE_EDGE = new world.biome.Biome(23, 0.93f, world.biome.Biome.Type.HOT, world.biome.Biome.Type.WET, world.biome.Biome.Type.JUNGLE, world.biome.Biome.Type.FOREST);
        public static world.biome.Biome DEEP_OCEAN = new world.biome.Biome(24, 0.5f, world.biome.Biome.Type.OCEAN);
        public static world.biome.Biome STONE_BEACH = new world.biome.Biome(25, 0.2f, world.biome.Biome.Type.BEACH);
        public static world.biome.Biome COLD_BEACH = new world.biome.Biome(26, 0.05f, world.biome.Biome.Type.COLD, world.biome.Biome.Type.BEACH, world.biome.Biome.Type.SNOWY);
        public static world.biome.Biome BIRCH_FOREST = new world.biome.Biome(27, 0.6f, world.biome.Biome.Type.FOREST);
        public static world.biome.Biome BIRCH_FOREST_HILLS = new world.biome.Biome(28, BIRCH_FOREST.getTemperature(), world.biome.Biome.Type.FOREST, world.biome.Biome.Type.HILLS);
        public static world.biome.Biome ROOFED_FOREST = new world.biome.Biome(29, 0.7f, world.biome.Biome.Type.SPOOKY, world.biome.Biome.Type.DENSE, world.biome.Biome.Type.FOREST);
        public static world.biome.Biome TAIGA_COLD = new world.biome.Biome(30, -0.5f, world.biome.Biome.Type.COLD, world.biome.Biome.Type.CONIFEROUS, world.biome.Biome.Type.FOREST, world.biome.Biome.Type.SNOWY);
        public static world.biome.Biome TAIGA_COLD_HILLS = new world.biome.Biome(31, TAIGA_COLD.getTemperature(), world.biome.Biome.Type.COLD, world.biome.Biome.Type.CONIFEROUS, world.biome.Biome.Type.FOREST, world.biome.Biome.Type.SNOWY, world.biome.Biome.Type.HILLS);
        public static world.biome.Biome REDWOOD_TAIGA = new world.biome.Biome(32, 0.3f, world.biome.Biome.Type.COLD, world.biome.Biome.Type.CONIFEROUS, world.biome.Biome.Type.FOREST);
        public static world.biome.Biome REDWOOD_TAIGA_HILLS = new world.biome.Biome(33, REDWOOD_TAIGA.getTemperature(), world.biome.Biome.Type.COLD, world.biome.Biome.Type.CONIFEROUS, world.biome.Biome.Type.FOREST, world.biome.Biome.Type.HILLS);
        public static world.biome.Biome EXTREME_HILLS_WITH_TREES = new world.biome.Biome(34, EXTREME_HILLS.getTemperature(), world.biome.Biome.Type.MOUNTAIN, world.biome.Biome.Type.FOREST, world.biome.Biome.Type.SPARSE);
        public static world.biome.Biome SAVANNA = new world.biome.Biome(35, 1.2f, world.biome.Biome.Type.HOT, world.biome.Biome.Type.SAVANNA, world.biome.Biome.Type.PLAINS, world.biome.Biome.Type.SPARSE);
        public static world.biome.Biome SAVANNA_ROCK = new world.biome.Biome(36, SAVANNA.getTemperature(), world.biome.Biome.Type.HOT, world.biome.Biome.Type.SAVANNA, world.biome.Biome.Type.PLAINS, world.biome.Biome.Type.SPARSE);
        public static world.biome.Biome MESA = new world.biome.Biome(37, 2.0f, world.biome.Biome.Type.MESA, world.biome.Biome.Type.SANDY);
        public static world.biome.Biome MESA_ROCK = new world.biome.Biome(38, 2.0f, world.biome.Biome.Type.MESA, world.biome.Biome.Type.SPARSE, world.biome.Biome.Type.SANDY);
        public static world.biome.Biome MESA_CLEAR_ROCK = new world.biome.Biome(39, MESA_ROCK.getTemperature(), world.biome.Biome.Type.MESA, world.biome.Biome.Type.SANDY);
        public static world.biome.Biome VOID = new world.biome.Biome(127, 0.0f);

        //Mutated
        public static world.biome.Biome MUTATED_PLAINS = new world.biome.Biome(129, PLAINS.getTemperature(), world.biome.Biome.Type.PLAINS, world.biome.Biome.Type.RARE);
        public static world.biome.Biome MUTATED_DESERT = new world.biome.Biome(130, DESERT.getTemperature(), world.biome.Biome.Type.HOT, world.biome.Biome.Type.DRY, world.biome.Biome.Type.SANDY, world.biome.Biome.Type.RARE);
        public static world.biome.Biome MUTATED_EXTREME_HILLS = new world.biome.Biome(131, EXTREME_HILLS.getTemperature(), world.biome.Biome.Type.MOUNTAIN, world.biome.Biome.Type.SPARSE, world.biome.Biome.Type.RARE);
        public static world.biome.Biome MUTATED_FOREST = new world.biome.Biome(132, FOREST.getTemperature(), world.biome.Biome.Type.FOREST, world.biome.Biome.Type.HILLS, world.biome.Biome.Type.RARE);
        public static world.biome.Biome MUTATED_TAIGA = new world.biome.Biome(133, TAIGA.getTemperature(), world.biome.Biome.Type.COLD, world.biome.Biome.Type.CONIFEROUS, world.biome.Biome.Type.FOREST, world.biome.Biome.Type.MOUNTAIN, world.biome.Biome.Type.RARE);
        public static world.biome.Biome MUTATED_SWAMPLAND = new world.biome.Biome(134, SWAMPLAND.getTemperature(), world.biome.Biome.Type.WET, world.biome.Biome.Type.SWAMP, world.biome.Biome.Type.HILLS, world.biome.Biome.Type.RARE);
        public static world.biome.Biome MUTATED_ICE_FLATS = new world.biome.Biome(140, ICE_FLATS.getTemperature(), world.biome.Biome.Type.COLD, world.biome.Biome.Type.SNOWY, world.biome.Biome.Type.HILLS, world.biome.Biome.Type.RARE);
        public static world.biome.Biome MUTATED_JUNGLE = new world.biome.Biome(149, JUNGLE.getTemperature(), world.biome.Biome.Type.HOT, world.biome.Biome.Type.WET, world.biome.Biome.Type.DENSE, world.biome.Biome.Type.JUNGLE, world.biome.Biome.Type.MOUNTAIN, world.biome.Biome.Type.RARE);
        public static world.biome.Biome MUTATED_JUNGLE_EDGE = new world.biome.Biome(151, JUNGLE_EDGE.getTemperature(), world.biome.Biome.Type.HOT, world.biome.Biome.Type.SPARSE, world.biome.Biome.Type.JUNGLE, world.biome.Biome.Type.HILLS, world.biome.Biome.Type.RARE);
        public static world.biome.Biome MUTATED_BIRCH_FOREST = new world.biome.Biome(155, BIRCH_FOREST.getTemperature(), world.biome.Biome.Type.FOREST, world.biome.Biome.Type.DENSE, world.biome.Biome.Type.HILLS, world.biome.Biome.Type.RARE);
        public static world.biome.Biome MUTATED_BIRCH_FOREST_HILLS = new world.biome.Biome(156, BIRCH_FOREST_HILLS.getTemperature(), world.biome.Biome.Type.FOREST, world.biome.Biome.Type.DENSE, world.biome.Biome.Type.MOUNTAIN, world.biome.Biome.Type.RARE);
        public static world.biome.Biome MUTATED_ROOFED_FOREST = new world.biome.Biome(157, ROOFED_FOREST.getTemperature(), world.biome.Biome.Type.SPOOKY, world.biome.Biome.Type.DENSE, world.biome.Biome.Type.FOREST, world.biome.Biome.Type.MOUNTAIN, world.biome.Biome.Type.RARE);
        public static world.biome.Biome MUTATED_TAIGA_COLD = new world.biome.Biome(158, TAIGA_COLD.getTemperature(), world.biome.Biome.Type.COLD, world.biome.Biome.Type.CONIFEROUS, world.biome.Biome.Type.FOREST, world.biome.Biome.Type.SNOWY, world.biome.Biome.Type.MOUNTAIN, world.biome.Biome.Type.RARE);
        public static world.biome.Biome MUTATED_REDWOOD_TAIGA = new world.biome.Biome(160, REDWOOD_TAIGA.getTemperature(), world.biome.Biome.Type.DENSE, world.biome.Biome.Type.FOREST, world.biome.Biome.Type.RARE);
        public static world.biome.Biome MUTATED_REDWOOD_TAIGA_HILLS = new world.biome.Biome(161, REDWOOD_TAIGA_HILLS.getTemperature(), world.biome.Biome.Type.DENSE, world.biome.Biome.Type.FOREST, world.biome.Biome.Type.HILLS, world.biome.Biome.Type.RARE);
        public static world.biome.Biome MUTATED_EXTREME_HILLS_WITH_TREES = new world.biome.Biome(162, EXTREME_HILLS_WITH_TREES.getTemperature(), world.biome.Biome.Type.MOUNTAIN, world.biome.Biome.Type.SPARSE, world.biome.Biome.Type.RARE);
        public static world.biome.Biome MUTATED_SAVANNA = new world.biome.Biome(163, SAVANNA.getTemperature(), world.biome.Biome.Type.HOT, world.biome.Biome.Type.DRY, world.biome.Biome.Type.SPARSE, world.biome.Biome.Type.SAVANNA, world.biome.Biome.Type.MOUNTAIN, world.biome.Biome.Type.RARE);
        public static world.biome.Biome MUTATED_SAVANNA_ROCK = new world.biome.Biome(164, SAVANNA_ROCK.getTemperature(), world.biome.Biome.Type.HOT, world.biome.Biome.Type.DRY, world.biome.Biome.Type.SPARSE, world.biome.Biome.Type.SAVANNA, world.biome.Biome.Type.HILLS, world.biome.Biome.Type.RARE);
        public static world.biome.Biome MUTATED_MESA = new world.biome.Biome(165, MESA.getTemperature(), world.biome.Biome.Type.HOT, world.biome.Biome.Type.DRY, world.biome.Biome.Type.SPARSE, world.biome.Biome.Type.SAVANNA, world.biome.Biome.Type.MOUNTAIN, world.biome.Biome.Type.RARE);
        public static world.biome.Biome MUTATED_MESA_ROCK = new world.biome.Biome(166, MESA_ROCK.getTemperature(), world.biome.Biome.Type.HOT, world.biome.Biome.Type.DRY, world.biome.Biome.Type.SPARSE, world.biome.Biome.Type.HILLS, world.biome.Biome.Type.RARE);
        public static world.biome.Biome MUTATED_MESA_CLEAR_ROCK = new world.biome.Biome(167, MESA_CLEAR_ROCK.getTemperature(), world.biome.Biome.Type.HOT, world.biome.Biome.Type.DRY, world.biome.Biome.Type.SPARSE, world.biome.Biome.Type.SAVANNA, world.biome.Biome.Type.MOUNTAIN, world.biome.Biome.Type.RARE);
    }
}