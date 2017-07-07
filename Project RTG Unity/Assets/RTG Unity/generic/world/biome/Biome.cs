namespace generic.world.biome
{
    public class Biome
    {
        private object ID;

        public Biome() : this(0)
        {

        }

        public Biome(object ID)
        {
            this.ID = ID;
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
    }
}
