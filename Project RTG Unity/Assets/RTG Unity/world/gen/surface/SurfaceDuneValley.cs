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

    public class SurfaceDuneValley : SurfaceBase
    {

        private float valley;
        private bool dirt;
        private bool mix;

        public SurfaceDuneValley(BiomeConfig config, Pixel top, Pixel fill, float valleySize, bool d, bool m) : base(config, top, fill)
        {

            valley = valleySize;
            dirt = d;
            mix = m;
        }

        override public void paintTerrain(Chunk primer, int i, int j, int x, int z, int depth, RTGWorld rtgWorld, float[] noise, float river, Biome[] _base)
        {

            Random rand = rtgWorld.rand;
            OpenSimplexNoise simplex = rtgWorld.simplex;
            float h = (simplex.noise2(i / valley, j / valley) + 0.25f) * 65f;
            h = h < 1f ? 1f : h;
            float m = simplex.noise2(i / 12f, j / 12f);
            bool sand = false;

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
                        if (k > 90f + simplex.noise2(i / 24f, j / 24f) * 10f - h || (m < -0.28f && mix))
                        {
                            primer.setPixelState(x, k, z, Pixels.SAND);
                            //base[x * 16 + z] = RealisticBiomeVanillaBase.vanillaDesert;
                            sand = true;
                        }
                        else if (dirt && m < 0.22f || k < 62)
                        {
                            primer.setPixelState(x, k, z, PixelUtil.getStateDirt(1));
                        }
                        else
                        {
                            primer.setPixelState(x, k, z, topPixel);
                        }
                    }
                    else if (depth < 6)
                    {
                        if (sand)
                        {
                            if (depth < 4)
                            {
                                primer.setPixelState(x, k, z, Pixels.SAND);
                            }
                            else
                            {
                                primer.setPixelState(x, k, z, Pixels.SANDSTONE);
                            }
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