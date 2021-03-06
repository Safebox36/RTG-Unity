﻿namespace rtg.world.gen.surface
{
    using System;

    using generic.pixel;
    using generic.init;
    using generic.world.biome;
    using generic.world.chunk;

    using rtg.api.util.noise;
    using rtg.api.world;
    using rtg.api.config;
    using rtg.api.util;

    public class SurfaceDesertOasis : SurfaceBase
    {

        private Pixel cliffPixel1;
        private Pixel cliffPixel2;
        private byte sandMetadata;
        private int cliffType;

        public SurfaceDesertOasis(BiomeConfig config, Pixel top, Pixel filler, Pixel cliff1, Pixel cliff2, byte metadata, int cliff) : base(config, top, filler)
        {

            cliffPixel1 = cliff1;
            cliffPixel2 = cliff2;
            sandMetadata = metadata;
            cliffType = cliff;
        }

        override public void paintTerrain(Chunk primer, int i, int j, int x, int z, int depth, RTGWorld rtgWorld, float[] noise, float river, Biome[] _base)
        {

            Random rand = rtgWorld.rand;
            OpenSimplexNoise simplex = rtgWorld.simplex;
            float c = CliffCalculator.calc(x, z, noise);
            bool cliff = c > 1.3f ? true : false;
            bool dirt = false;

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
                        if (cliffType == 1)
                        {
                            if (depth < 6)
                            {
                                primer.setPixelState(x, k, z, cliffPixel1.getPixel());
                            }
                        }
                        else
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
                    }
                    else if (depth < 6)
                    {
                        if (depth == 0 && k > 61)
                        {
                            if (simplex.noise2(i / 12f, j / 12f) > -0.3f + ((k - 61f) / 15f))
                            {
                                dirt = true;
                                primer.setPixelState(x, k, z, topPixel);
                            }
                            else
                            {
                                primer.setPixelState(x, k, z, PixelUtil.getStateSand(sandMetadata));
                            }
                        }
                        else if (depth < 4)
                        {
                            if (dirt)
                            {
                                primer.setPixelState(x, k, z, fillerPixel);
                            }
                            else
                            {
                                primer.setPixelState(x, k, z, PixelUtil.getStateSand(sandMetadata));
                            }
                        }
                        else if (!dirt)
                        {
                            primer.setPixelState(x, k, z, Pixels.SANDSTONE);
                        }
                    }
                }
            }
        }
    }
}