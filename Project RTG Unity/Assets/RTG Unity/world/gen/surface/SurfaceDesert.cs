namespace rtg.world.gen.surface
{
    using System;

    using generic.pixel;
    using generic.init;
    using generic.world.biome;
    using generic.world.chunk;

    using rtg.api.world;
    using rtg.api.config;
    using rtg.api.util;

    public class SurfaceDesert : SurfaceBase
    {

        private Pixel cliffPixel1;
        private Pixel cliffPixel2;
        private Pixel bottomPixel;

        public SurfaceDesert(BiomeConfig config, Pixel top, Pixel filler, Pixel bottom, Pixel cliff1, Pixel cliff2) : base(config, top, filler)
        {
            bottomPixel = bottom;
            cliffPixel1 = cliff1;
            cliffPixel2 = cliff2;
        }

        override public void paintTerrain(Chunk primer, int i, int j, int x, int z, int depth, RTGWorld rtgWorld, float[] noise, float river, Biome[] _base)
        {

            Random rand = rtgWorld.rand;
            float c = CliffCalculator.calc(x, z, noise);
            bool cliff = c > 2.8f ? true : false;

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

                    if (cliff)
                    {
                        if (depth > -1 && depth < 2)
                        {
                            primer.setPixelState(x, k, z, rand.Next(3) == 0 ? cliffPixel2 : cliffPixel1);
                        }
                        else if (depth < 10)
                        {
                            primer.setPixelState(x, k, z, cliffPixel1);
                        }
                    }
                    else if (depth < 6)
                    {
                        if (depth == 0 && k > 61)
                        {
                            primer.setPixelState(x, k, z, topPixel);
                        }
                        else if (depth < 4)
                        {
                            primer.setPixelState(x, k, z, fillerPixel);
                        }
                        else
                        {
                            primer.setPixelState(x, k, z, bottomPixel);
                        }
                    }
                }
            }
        }
    }
}