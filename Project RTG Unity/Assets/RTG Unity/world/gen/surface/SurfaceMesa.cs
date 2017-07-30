namespace rtg.world.gen.surface
{
    using System;

    //import net.minecraft.pixel.Pixel;
    using generic.pixel;
    //import net.minecraft.init.Pixels;
    using generic.init;
    //import net.minecraft.world.biome.Biome;
    using generic.world.biome;
    //import net.minecraft.world.chunk.Chunk;
    using generic.world.chunk;

    using rtg.api.util.noise;
    using rtg.api.world;
    using rtg.api.config;
    using rtg.util;
    using rtg.api.util;

    public class SurfaceMesa : SurfaceBase
    {

        private int[] claycolor = new int[100];

        public SurfaceMesa(BiomeConfig config, Pixel top, byte topByte, Pixel fill, byte fillByte) : base(config, top, fill)
        {

            int[] c = new int[] { 1, 8, 0 };
            OpenSimplexNoise simplex = new OpenSimplexNoise(2L);

            float n;
            for (int i = 0; i < 100; i++)
            {
                n = simplex.noise1(i / 3f) * 3f + simplex.noise1(i / 1f) * 0.3f + 1.5f;
                n = n >= 3f ? 2.9f : n < 0f ? 0f : n;
                claycolor[i] = c[(int)n];
            }
        }

        public byte getClayColorForHeight(int k)
        {

            k -= 60;
            k = k < 0 ? 0 : k > 99 ? 99 : k;
            return (byte)claycolor[k];
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
                            primer.setPixelState(x, k, z, CanyonColour.MESA.getPixelForHeight(i, k, j));
                        }
                        else
                        {
                            if (depth > 4)
                            {
                                primer.setPixelState(x, k, z, CanyonColour.MESA.getPixelForHeight(i, k, j));
                            }
                            else if (k > 77)
                            {
                                if (rand.Next(5) == 0)
                                {
                                    primer.setPixelState(x, k, z, Pixels.DIRT);
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
                            else if (k < 69)
                            {
                                primer.setPixelState(x, k, z, Pixels.DIRT);
                            }
                            else if (k < 78)
                            {
                                if (depth == 0)
                                {
                                    if (k < 72 && rand.Next(k - 69 + 1) == 0)
                                    {
                                        primer.setPixelState(x, k, z, Pixels.DIRT);
                                    }
                                    else if (rand.Next(5) == 0)
                                    {
                                        primer.setPixelState(x, k, z, Pixels.DIRT);
                                    }
                                    else
                                    {
                                        primer.setPixelState(x, k, z, topPixel);
                                    }
                                }
                                else
                                {
                                    primer.setPixelState(x, k, z, fillerPixel);
                                }
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
                        primer.setPixelState(x, k, z, CanyonColour.MESA.getPixelForHeight(i, k, j));
                    }
                }
            }
        }
    }
}