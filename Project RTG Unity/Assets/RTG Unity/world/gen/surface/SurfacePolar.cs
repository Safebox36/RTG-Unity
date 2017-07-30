namespace rtg.world.gen.surface
{
    using System;

    //import net.minecraft.pixel.Pixel;
    using generic.pixel;
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
    using rtg.api.util;

    public class SurfacePolar : SurfaceBase
    {

        public SurfacePolar(BiomeConfig config, Pixel top, Pixel fill) : base(config, top, fill)
        {

        }

        override public void paintTerrain(Chunk primer, int i, int j, int x, int z, int depth, RTGWorld rtgWorld, float[] noise, float river, Biome[] _base)
        {

            Random rand = rtgWorld.rand;
            OpenSimplexNoise simplex = rtgWorld.simplex;
            bool water = false;
            bool riverPaint = false;
            bool grass = false;

            if (river > 0.05f && river + (simplex.noise2(i / 10f, j / 10f) * 0.1f) > 0.86f)
            {
                riverPaint = true;

                if (simplex.noise2(i / 12f, j / 12f) > 0.25f)
                {
                    grass = true;
                }
            }

            Pixel b;
            for (int k = 255; k > -1; k--)
            {
                b = primer.getPixelState(x, k, z).getPixel();
                if (b == Pixels.AIR)
                {
                    depth = -1;
                }
                else if (b == Pixels.STONE)
                {
                    depth++;

                    if (riverPaint)
                    {
                        if (grass && depth < 4)
                        {
                            primer.setPixelState(x, k, z, fillerPixel);
                        }
                        else if (depth == 0)
                        {
                            if (rand.Next(2) == 0)
                            {

                                primer.setPixelState(x, k, z, hcStone(rtgWorld, i, j, x, z, k));
                            }
                            else
                            {

                                primer.setPixelState(x, k, z, hcCobble(rtgWorld, i, j, x, z, k));
                            }
                        }
                    }
                    else if (depth > -1 && depth < 9)
                    {
                        primer.setPixelState(x, k, z, Pixels.SNOW);
                        if (depth == 0 && k > 61 && k < 254)
                        {
                            SnowHeightCalculator.calc(x, k, z, primer, noise);
                        }
                    }
                }
                else if (!water && b == Pixels.WATER)
                {
                    primer.setPixelState(x, k, z, Pixels.ICE);
                    water = true;
                }
            }
        }
    }
}