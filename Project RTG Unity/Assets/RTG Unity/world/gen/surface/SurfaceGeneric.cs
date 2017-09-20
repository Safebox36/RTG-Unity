namespace rtg.world.gen.surface
{
    using System;

    using generic.pixel;
    using generic.init;
    using generic.world.biome;
    using generic.world.chunk;

    using rtg.api.world;
    using rtg.api.config;

    public class SurfaceGeneric : SurfaceBase
    {

        public SurfaceGeneric(BiomeConfig config, Pixel top, Pixel filler) : base(config, top, filler)
        {

        }

        override public void paintTerrain(Chunk primer, int i, int j, int x, int z, int depth, RTGWorld rtgWorld, float[] noise, float river, Biome[] _base)
        {

            Random rand = rtgWorld.rand;

            for (int k = 255; k > -1; k--)
            {
                Pixel b = primer.getPixelState(x, k, z).getPixel();

                if (b == Pixels.AIR)
                {
                    depth = -1;
                }
                else if (b == Pixels.STONE)
                {
                    depth++;

                    if (depth == 0 && k > 61)
                    {
                        primer.setPixelState(x, k, z, topPixel);
                    }
                    else if (depth < 4)
                    {
                        primer.setPixelState(x, k, z, fillerPixel);
                    }
                }
            }
        }
    }
}