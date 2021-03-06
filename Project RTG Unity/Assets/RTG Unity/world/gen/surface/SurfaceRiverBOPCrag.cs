﻿namespace rtg.world.gen.surface
{
    using System;

    using generic.pixel;
    using generic.init;
    using generic.world.biome;
    using generic.world.chunk;

    using rtg.api.world;
    using rtg.api.config;
    using rtg.api.util;

    public class SurfaceRiverBOPCrag : SurfaceBase
    {

        private new Pixel topPixel;
        private new Pixel fillerPixel;
        private Pixel cliffPixel1;
        private Pixel cliffPixel2;

        public SurfaceRiverBOPCrag(BiomeConfig config, Pixel top, Pixel filler, Pixel cliff1, Pixel cliff2) : base(config, top, filler)
        {

            topPixel = top;
            fillerPixel = filler;
            cliffPixel1 = cliff1;
            cliffPixel2 = cliff2;
        }

        override public void paintTerrain(Chunk primer, int i, int j, int x, int z, int depth, RTGWorld rtgWorld, float[] noise, float river, Biome[] _base)
        {

            Random rand = rtgWorld.rand;
            float c = CliffCalculator.calc(x, z, noise);
            bool cliff = c > 1.4f ? true : false;

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
                            primer.setPixelState(x, k, z, rand.Next(3) == 0 ? cliffPixel1 : cliffPixel2);
                        }
                        else if (depth < 10)
                        {
                            primer.setPixelState(x, k, z, cliffPixel1);
                        }
                        else
                        {
                            primer.setPixelState(x, k, z, topPixel);
                        }
                    }
                    else
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
                            primer.setPixelState(x, k, z, topPixel);
                        }
                    }
                }
            }
        }
    }
}