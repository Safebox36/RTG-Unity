namespace generic.world.biome
{
    public class Biome
    {
        private int ID;
        private float temperature;
        private float baseHeight = 0.1f;
        private float heightVariation = 0.3f;
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
            BEACH
        }

        public Biome() : this(0)
        {

        }

        public Biome(int ID)
        {
            this.ID = ID;
        }

        public Biome(int ID, float temperature)
        {
            this.ID = ID;
            this.temperature = temperature;
        }
        
        public Biome(int ID, params Type[] tags)
        {
            this.ID = ID;
            this.tags = tags;
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

        public Biome getBiome(int ID)
        {
            this.ID = ID;
            return this;
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
    }
}
