namespace rtg.world.gen
{
    using rtg.world.biome.realistic;

    /**
     *
     * @author Zeno410
     */
    public class ChunkLandscape
    {
        public float[] noise = new float[256];
        public RealisticBiomeBase[] biome = new RealisticBiomeBase[256];
        public float[] river = new float[256];
    }
}