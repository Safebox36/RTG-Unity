namespace generic.world.biome
{
    public class Biome
    {
        private int ID;
        private float temperature;
        private float baseHeight = 0.1f;
        private float heightVariation = 0.3f;
        public pixel.Pixel topPixel;
        public pixel.Pixel fillerPixel;
        private Type[] tags;

        //Types taken from Forge API
        public enum Type
        { /*Temperature-based tags. Specifying neither implies a biome is temperate*/
            HOT,
            COLD,
            /*Tags specifying the amount of vegetation a biome has. Specifying neither implies a biome to have moderate amounts*/
            SPARSE,
            DENSE,
            /*Tags specifying how moist a biome is. Specifying neither implies the biome as having moderate humidity*/
            WET,
            DRY,
            /*Tree-based tags, SAVANNA refers to dry, desert-like trees (Such as Acacia), CONIFEROUS refers to snowy trees (Such as Spruce) and JUNGLE refers to jungle trees.
             * Specifying no tag implies a biome has temperate trees (Such as Oak)*/
            SAVANNA,
            CONIFEROUS,
            JUNGLE,

            /*Tags specifying the nature of a biome*/
            SPOOKY,
            DEAD,
            LUSH,
            HELL,
            SKY,
            MUSHROOM,
            MAGICAL,
            RARE,

            OCEAN,
            RIVER,
            /**A general tag for all water-based biomes. Shown as present if OCEAN or RIVER are.**/
            WATER,

            /*Generic types which a biome can be*/
            MESA,
            FOREST,
            PLAINS,
            MOUNTAIN,
            HILLS,
            SWAMP,
            SANDY,
            SNOWY,
            WASTELAND,
            BEACH,
            VOID
        }

        public Biome() : this(0)
        {

        }

        public Biome(int ID)
        {
            this.ID = ID;
        }


        public Biome(int ID, float temperature, params Type[] tags)
        {
            this.ID = ID;
            this.temperature = temperature;
            this.tags = tags;
        }

        public Biome getBiome()
        {
            return this;
        }

        public static Biome getBiome(int ID)
        {
            switch (ID)
            {
                //Normal
                case 0:
                    return generic.init.Biomes.OCEAN;
                case 1:
                    return generic.init.Biomes.PLAINS;
                case 2:
                    return generic.init.Biomes.DESERT;
                case 3:
                    return generic.init.Biomes.EXTREME_HILLS;
                case 4:
                    return generic.init.Biomes.FOREST;
                case 5:
                    return generic.init.Biomes.TAIGA;
                case 6:
                    return generic.init.Biomes.SWAMPLAND;
                case 7:
                    return generic.init.Biomes.RIVER;
                /*case 8:
                    return generic.init.Biomes.HELL;
                case 9:
                    return generic.init.Biomes.SKY;*/
                case 10:
                    return generic.init.Biomes.FROZEN_OCEAN;
                case 11:
                    return generic.init.Biomes.FROZEN_RIVER;
                case 12:
                    return generic.init.Biomes.ICE_FLATS;
                case 13:
                    return generic.init.Biomes.ICE_MOUNTAINS;
                case 14:
                    return generic.init.Biomes.MUSHROOM_ISLAND;
                case 15:
                    return generic.init.Biomes.MUSHROOM_ISLAND_SHORE;
                case 16:
                    return generic.init.Biomes.BEACHES;
                case 17:
                    return generic.init.Biomes.DESERT_HILLS;
                case 18:
                    return generic.init.Biomes.FOREST_HILLS;
                case 19:
                    return generic.init.Biomes.TAIGA_HILLS;
                case 20:
                    return generic.init.Biomes.SMALLER_EXTREME_HILLS;
                case 21:
                    return generic.init.Biomes.JUNGLE;
                case 22:
                    return generic.init.Biomes.JUNGLE_HILLS;
                case 23:
                    return generic.init.Biomes.JUNGLE_EDGE;
                case 24:
                    return generic.init.Biomes.DEEP_OCEAN;
                case 25:
                    return generic.init.Biomes.STONE_BEACH;
                case 26:
                    return generic.init.Biomes.COLD_BEACH;
                case 27:
                    return generic.init.Biomes.BIRCH_FOREST;
                case 28:
                    return generic.init.Biomes.BIRCH_FOREST_HILLS;
                case 29:
                    return generic.init.Biomes.ROOFED_FOREST;
                case 30:
                    return generic.init.Biomes.TAIGA_COLD;
                case 31:
                    return generic.init.Biomes.TAIGA_COLD_HILLS;
                case 32:
                    return generic.init.Biomes.REDWOOD_TAIGA;
                case 33:
                    return generic.init.Biomes.REDWOOD_TAIGA_HILLS;
                case 34:
                    return generic.init.Biomes.EXTREME_HILLS_WITH_TREES;
                case 35:
                    return generic.init.Biomes.SAVANNA;
                case 36:
                    return generic.init.Biomes.SAVANNA_ROCK;
                case 37:
                    return generic.init.Biomes.MESA;
                case 38:
                    return generic.init.Biomes.MESA_ROCK;
                case 39:
                    return generic.init.Biomes.MESA_CLEAR_ROCK;
                //Mutated
                case 129:
                    return generic.init.Biomes.MUTATED_PLAINS;
                case 130:
                    return generic.init.Biomes.MUTATED_DESERT;
                case 131:
                    return generic.init.Biomes.MUTATED_EXTREME_HILLS;
                case 132:
                    return generic.init.Biomes.MUTATED_FOREST;
                case 133:
                    return generic.init.Biomes.MUTATED_TAIGA;
                case 134:
                    return generic.init.Biomes.MUTATED_SWAMPLAND;
                case 140:
                    return generic.init.Biomes.MUTATED_ICE_FLATS;
                case 149:
                    return generic.init.Biomes.MUTATED_JUNGLE;
                case 151:
                    return generic.init.Biomes.JUNGLE_EDGE;
                case 155:
                    return generic.init.Biomes.MUTATED_BIRCH_FOREST;
                case 156:
                    return generic.init.Biomes.MUTATED_BIRCH_FOREST_HILLS;
                case 157:
                    return generic.init.Biomes.MUTATED_ROOFED_FOREST;
                case 158:
                    return generic.init.Biomes.MUTATED_TAIGA_COLD;
                case 160:
                    return generic.init.Biomes.MUTATED_REDWOOD_TAIGA;
                case 161:
                    return generic.init.Biomes.MUTATED_REDWOOD_TAIGA_HILLS;
                case 162:
                    return generic.init.Biomes.MUTATED_EXTREME_HILLS_WITH_TREES;
                case 163:
                    return generic.init.Biomes.MUTATED_SAVANNA;
                case 164:
                    return generic.init.Biomes.MUTATED_SAVANNA_ROCK;
                case 165:
                    return generic.init.Biomes.MUTATED_MESA;
                case 166:
                    return generic.init.Biomes.MUTATED_MESA_ROCK;
                case 167:
                    return generic.init.Biomes.MUTATED_MESA_CLEAR_ROCK;
                default:
                    return generic.init.Biomes.VOID;
            }
        }

        public int getBiomeID()
        {
            return (int)ID;
        }

        public int getRainfall()
        {
            return 0;
        }

        public float getTemperature()
        {
            return temperature;
        }

        public float getBaseHeight()
        {
            return baseHeight;
        }

        public float getHeightVariation()
        {
            return heightVariation;
        }

        public bool isBiomeOfType(Type type)
        {
            bool returnVal = false;
            foreach (Type bt in tags)
            {
                if (bt == type)
                {
                    returnVal = true;
                }
                else
                {
                    returnVal = false;
                }
            }
            return returnVal;
        }

        public void presetPixels(pixel.Pixel topPixel, pixel.Pixel fillerPixel)
        {
            this.topPixel = topPixel;
            this.fillerPixel = fillerPixel;
        }
    }
}
