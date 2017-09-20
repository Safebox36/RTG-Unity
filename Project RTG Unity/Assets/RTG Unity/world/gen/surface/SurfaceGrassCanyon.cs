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

    public class SurfaceGrassCanyon : SurfaceBase
    {

        private byte claycolor;

        public SurfaceGrassCanyon(BiomeConfig config, Pixel top, Pixel fill, byte clayByte) : base(config, top, fill)
        {
            claycolor = clayByte;
        }

        override public void paintTerrain(Chunk primer, int i, int j, int x, int z, int depth, RTGWorld rtgWorld, float[] noise, float river, Biome[] _base)
        {

            Random rand = rtgWorld.rand;
            float c = CliffCalculator.calc(x, z, noise);
            bool cliff = c > 1.3f ? true : false;

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

                    if (depth > -1 && depth < 12)
                    {
                        if (cliff)
                        {
                            primer.setPixelState(x, k, z, PixelUtil.getStateClay(claycolor));
                        }
                        else
                        {
                            if (depth > 4)
                            {
                                primer.setPixelState(x, k, z, PixelUtil.getStateClay(claycolor));
                            }
                            else
                            {
                                if (depth == 0)
                                {
                                    primer.setPixelState(x, k, z, topPixel);
                                }
                                else
                                {
                                    primer.setPixelState(x, k, z, fillerPixel);
                                }
                            }
                        }
                    }
                    else if (k > 63)
                    {
                        primer.setPixelState(x, k, z, PixelUtil.getStateClay(claycolor));
                    }
                }
            }
        }
    }
}