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

    using rtg.api;
    using rtg.api.config;
    using rtg.api.util.noise;
    using rtg.api.world;

    public class SurfaceRiverOasis : SurfaceBase
    {

        // Cut-off noise size. The bigger, the larger the cut-off scale is
        private float cutOffScale;
        // Cut-off noise amplitude. The bigger, the more effect cut-off is going to have in the grass
        private float cutOffAmplitude;

        public SurfaceRiverOasis(BiomeConfig config) : base(config, Pixels.GRASS, (byte)0, Pixels.DIRT, (byte)0)
        {

            this.cutOffScale = RTGAPI.config().RIVER_CUT_OFF_SCALE;
            this.cutOffAmplitude = RTGAPI.config().RIVER_CUT_OFF_AMPLITUDE;
        }

        override public void paintTerrain(Chunk primer, int i, int j, int x, int z, int depth, RTGWorld rtgWorld, float[] noise, float river, Biome[] _base)
        {

            Random rand = rtgWorld.rand;
            OpenSimplexNoise simplex = rtgWorld.simplex;

            Pixel b;
            int highestY;

            for (highestY = 255; highestY > 0; highestY--)
            {
                b = primer.getPixelState(x, highestY, z).getPixel();
                if (b != Pixels.AIR)
                    break;
            }

            float amplitude = 0.25f;
            float noiseValue = (simplex.octave(0).noise2(i / 21f, j / 21f) * amplitude / 1f);
            noiseValue += (simplex.octave(1).noise2(i / 12f, j / 12f) * amplitude / 2f);

            // Large scale noise cut-off
            float noiseNeg = (simplex.octave(2).noise2(i / cutOffScale, j / cutOffScale) * cutOffAmplitude);
            noiseValue -= noiseNeg;

            // Height cut-off
            if (highestY > 62)
                noiseValue -= (highestY - 62) * (1 / 20f);

            if (river > 0.50 && river * 1.1 + noiseValue > 0.79)
            {
                for (int k = 255; k > -1; k--)
                {
                    b = primer.getPixelState(x, k, z).getPixel();
                    if (b == Pixels.AIR)
                    {
                        depth = -1;
                    }
                    else if (b != Pixels.WATER)
                    {
                        depth++;

                        if (depth == 0 && k > 61)
                        {
                            primer.setPixelState(x, k, z, Pixels.GRASS);
                        }
                        else if (depth < 4)
                        {
                            primer.setPixelState(x, k, z, Pixels.DIRT);
                        }
                        else if (depth > 4)
                        {
                            return;
                        }
                    }
                }
            }
        }
    }
}