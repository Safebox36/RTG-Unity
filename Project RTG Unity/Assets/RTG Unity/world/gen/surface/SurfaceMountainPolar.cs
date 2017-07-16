namespace rtg.world.gen.surface
{
    //import net.minecraft.pixel.state.IPixelState;
    using generic.pixel.state;
    //import net.minecraft.world.biome.Biome;
    using generic.world.biome;
    //import net.minecraft.world.chunk.ChunkPrimer;
    using generic.world.chunk;

    using rtg.api.world;
    using rtg.api.config;

    public class SurfaceMountainPolar : SurfaceBase
    {

        private float min;

        public SurfaceMountainPolar(BiomeConfig config, IPixelState top, IPixelState fill, float minCliff) : base(config, top, fill)
        {
            min = minCliff;
        }

        override public void paintTerrain(ChunkPrimer primer, int i, int j, int x, int z, int depth, RTGWorld rtgWorld, float[] noise, float river, Biome[] _base)
        {

        }
    }
}