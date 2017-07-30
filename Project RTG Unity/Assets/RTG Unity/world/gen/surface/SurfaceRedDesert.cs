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

    using rtg.api.world;
    using rtg.api.config;
    using rtg.api.util;

    public class SurfaceRedDesert : SurfaceBase
    {

        private Pixel cliffPixel1;
        private Pixel bottomPixel;

        public SurfaceRedDesert(BiomeConfig config) : base(config, PixelUtil.getStateSand(1), PixelUtil.getStateSand(1))
        {

            bottomPixel = Pixels.SANDSTONE;
            cliffPixel1 = PixelUtil.getStateClay(14);
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
                        if (depth < 6)
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
                            primer.setPixelState(x, k, z, bottomPixel.getPixel());
                        }
                    }
                }
            }
        }
    }
}