namespace rtg.world.gen.surface
{
    using System;

    //import net.minecraft.block.Block;
    using generic.block;
    //import net.minecraft.init.Blocks;
    using generic.init;
    //import net.minecraft.world.biome.Biome;
    using generic.world.biome;
    //import net.minecraft.world.chunk.ChunkPrimer;
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

        public SurfaceRiverOasis(BiomeConfig config) : base(config, Blocks.GRASS, (byte)0, Blocks.DIRT, (byte)0)
        {

            this.cutOffScale = RTGAPI.config().RIVER_CUT_OFF_SCALE.get();
            this.cutOffAmplitude = RTGAPI.config().RIVER_CUT_OFF_AMPLITUDE.get();
        }

        override public void paintTerrain(ChunkPrimer primer, int i, int j, int x, int z, int depth, RTGWorld rtgWorld, float[] noise, float river, Biome[] _base)
        {

            Random rand = rtgWorld.rand;
            OpenSimplexNoise simplex = rtgWorld.simplex;

            Block b;
            int highestY;

            for (highestY = 255; highestY > 0; highestY--)
            {
                b = primer.getBlockState(x, highestY, z).getBlock();
                if (b != Blocks.AIR)
                    break;
            }

            float amplitude = 0.25f;
            float noiseValue = (float)(simplex.octave(0).Evaluate(i / 21f, j / 21f) * amplitude / 1f);
            noiseValue += (float)(simplex.octave(1).Evaluate(i / 12f, j / 12f) * amplitude / 2f);

            // Large scale noise cut-off
            float noiseNeg = (float)(simplex.octave(2).Evaluate(i / cutOffScale, j / cutOffScale) * cutOffAmplitude);
            noiseValue -= noiseNeg;

            // Height cut-off
            if (highestY > 62)
                noiseValue -= (highestY - 62) * (1 / 20f);

            if (river > 0.50 && river * 1.1 + noiseValue > 0.79)
            {
                for (int k = 255; k > -1; k--)
                {
                    b = primer.getBlockState(x, k, z).getBlock();
                    if (b == Blocks.AIR)
                    {
                        depth = -1;
                    }
                    else if (b != Blocks.WATER)
                    {
                        depth++;

                        if (depth == 0 && k > 61)
                        {
                            primer.setBlockState(x, k, z, (Block)Blocks.GRASS.getDefaultState());
                        }
                        else if (depth < 4)
                        {
                            primer.setBlockState(x, k, z, (Block)Blocks.DIRT.getDefaultState());
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