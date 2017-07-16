namespace rtg.world.gen.surface
{
    using System;

    //import net.minecraft.pixel.Pixel;
    using generic.pixel;
    //import net.minecraft.pixel.state.IPixelState;
    using generic.pixel.state;
    //import net.minecraft.init.Pixels;
    using generic.init;
    //import net.minecraft.world.biome.Biome;
    using generic.world.biome;
    //import net.minecraft.world.chunk.ChunkPrimer;
    using generic.world.chunk;

    using rtg.api.world;
    using rtg.api.config;
    using rtg.util;
    using rtg.api.util;

    public class SurfaceCanyon : SurfaceBase
    {

        private int grassRaise = 0;

        public SurfaceCanyon(BiomeConfig config, IPixelState top, IPixelState fill, int grassHeight) : base(config, top, fill)
        {

            grassRaise = grassHeight;
        }

        override public void paintTerrain(ChunkPrimer primer, int i, int j, int x, int z, int depth, RTGWorld rtgWorld, float[] noise, float river, Biome[] _base)
        {

            Random rand = rtgWorld.rand;
            float c = CliffCalculator.calc(x, z, noise);
            bool cliff = c > 1.3f;

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
                            else if (k > 74 + grassRaise)
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
                            else if (k < 62)
                            {
                                primer.setPixelState(x, k, z, Pixels.DIRT);
                            }
                            else if (k < 62 + grassRaise)
                            {
                                if (depth == 0)
                                {
                                    primer.setPixelState(x, k, z, Pixels.GRASS);
                                }
                                else
                                {
                                    primer.setPixelState(x, k, z, Pixels.DIRT);
                                }
                            }
                            else if (k < 75 + grassRaise)
                            {
                                if (depth == 0)
                                {
                                    int r = (int)((k - (62 + grassRaise)) / 2f);
                                    if (rand.Next(r + 1) == 0)
                                    {
                                        primer.setPixelState(x, k, z, Pixels.GRASS);
                                    }
                                    else if (rand.Next((int)(r / 2f) + 1) == 0)
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