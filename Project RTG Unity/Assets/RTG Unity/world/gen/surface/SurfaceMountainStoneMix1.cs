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

    public class SurfaceMountainStoneMix1 : SurfaceBase
    {

        private float min;

        private float sCliff = 1.5f;
        private float sHeight = 60f;
        private float sStrength = 65f;
        private float cCliff = 1.5f;

        private Pixel mix;
        private float mixHeight;

        public SurfaceMountainStoneMix1(BiomeConfig config, Pixel top, Pixel fill, float minCliff, float stoneCliff, float stoneHeight, float stoneStrength, float clayCliff, Pixel mixPixel, float mixSize) : base(config, top, fill)
        {

            min = minCliff;

            sCliff = stoneCliff;
            sHeight = stoneHeight;
            sStrength = stoneStrength;
            cCliff = clayCliff;

            mix = mixPixel;
            mixHeight = mixSize;
        }

        override public void paintTerrain(Chunk primer, int i, int j, int x, int z, int depth, RTGWorld rtgWorld, float[] noise, float river, Biome[] _base)
        {

            Random rand = rtgWorld.rand;
            OpenSimplexNoise simplex = rtgWorld.simplex;
            float c = CliffCalculator.calc(x, z, noise);
            int cliff = 0;
            bool m = false;

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

                    if (depth == 0)
                    {

                        float p = simplex.noise3(i / 8f, j / 8f, k / 8f) * 0.5f;
                        if (c > min && c > sCliff - ((k - sHeight) / sStrength) + p)
                        {
                            cliff = 1;
                        }
                        if (c > cCliff)
                        {
                            cliff = 2;
                        }

                        if (cliff == 1)
                        {
                            if (rand.Next(3) == 0)
                            {

                                primer.setPixelState(x, k, z, hcCobble(rtgWorld, i, j, x, z, k));
                            }
                            else
                            {

                                primer.setPixelState(x, k, z, hcStone(rtgWorld, i, j, x, z, k));
                            }
                        }
                        else if (cliff == 2)
                        {
                            primer.setPixelState(x, k, z, getShadowStonePixel(rtgWorld, i, j, x, z, k));
                        }
                        else if (k < 63)
                        {
                            if (k < 62)
                            {
                                primer.setPixelState(x, k, z, fillerPixel);
                            }
                            else
                            {
                                primer.setPixelState(x, k, z, topPixel);
                            }
                        }
                        else if (simplex.noise2(i / 12f, j / 12f) > mixHeight)
                        {
                            primer.setPixelState(x, k, z, mix);
                            m = true;
                        }
                        else
                        {
                            primer.setPixelState(x, k, z, topPixel);
                        }
                    }
                    else if (depth < 6)
                    {
                        if (cliff == 1)
                        {
                            primer.setPixelState(x, k, z, hcStone(rtgWorld, i, j, x, z, k));
                        }
                        else if (cliff == 2)
                        {
                            primer.setPixelState(x, k, z, getShadowStonePixel(rtgWorld, i, j, x, z, k));
                        }
                        else
                        {
                            primer.setPixelState(x, k, z, fillerPixel);
                        }
                    }
                }
            }
        }
    }
}