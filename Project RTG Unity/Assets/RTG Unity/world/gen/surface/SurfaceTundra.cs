namespace rtg.world.gen.surface
{
    using System;

    //import net.minecraft.block.Block;
    using generic.block;
    //import net.minecraft.block.state.IBlockState;
    using generic.block.state;
    //import net.minecraft.init.Blocks;
    using generic.init;
    //import net.minecraft.world.biome.Biome;
    using generic.world.biome;
    //import net.minecraft.world.chunk.ChunkPrimer;
    using generic.world.chunk;

    using rtg.api.util.noise;
    using rtg.api.world;
    using rtg.api.config;
    using rtg.api.util;

    public class SurfaceTundra : SurfaceBase
    {

        public SurfaceTundra(BiomeConfig config, IBlockState top, IBlockState fill) : base(config, top, fill)
        {

        }

        override public void paintTerrain(ChunkPrimer primer, int i, int j, int x, int z, int depth, RTGWorld rtgWorld, float[] noise, float river, Biome[] _base)
        {

            Random rand = rtgWorld.rand;
            OpenSimplexNoise simplex = rtgWorld.simplex;
            float p = simplex.noise2(i / 8f, j / 8f) * 0.5f;
            float c = CliffCalculator.calc(x, z, noise);
            int cliff = 0;

            Block b;
            for (int k = 255; k > -1; k--)
            {
                b = primer.getBlockState(x, k, z).getBlock();
                if (b == Blocks.AIR)
                {
                    depth = -1;
                }
                else if (b == Blocks.STONE)
                {
                    depth++;

                    if (depth == 0)
                    {

                        if (c > 0.45f && c > 1.5f - ((k - 60f) / 65f) + p)
                        {
                            cliff = 1;
                        }
                        if (c > 1.5f)
                        {
                            cliff = 2;
                        }
                        if (k > 110 + (p * 4) && c < 0.3f + ((k - 100f) / 50f) + p)
                        {
                            cliff = 3;
                        }

                        if (cliff == 1)
                        {
                            if (rand.Next(3) == 0)
                            {

                                primer.setBlockState(x, k, z, hcCobble(rtgWorld, i, j, x, z, k));
                            }
                            else
                            {

                                primer.setBlockState(x, k, z, hcStone(rtgWorld, i, j, x, z, k));
                            }
                        }
                        else if (cliff == 2)
                        {
                            primer.setBlockState(x, k, z, getShadowStoneBlock(rtgWorld, i, j, x, z, k));
                        }
                        else if (cliff == 3)
                        {
                            primer.setBlockState(x, k, z, (Block)Blocks.SNOW.getDefaultState());
                        }
                        else if (simplex.noise2(i / 50f, j / 50f) + p * 0.6f > 0.24f)
                        {
                            primer.setBlockState(x, k, z, BlockUtil.getStateDirt(2));
                        }
                        else
                        {
                            primer.setBlockState(x, k, z, (Block)Blocks.GRASS.getDefaultState());
                        }
                    }
                    else if (depth < 6)
                    {
                        if (cliff == 1)
                        {
                            primer.setBlockState(x, k, z, hcStone(rtgWorld, i, j, x, z, k));
                        }
                        else if (cliff == 2)
                        {
                            primer.setBlockState(x, k, z, getShadowStoneBlock(rtgWorld, i, j, x, z, k));
                        }
                        else if (cliff == 3)
                        {
                            primer.setBlockState(x, k, z, (Block)Blocks.SNOW.getDefaultState());
                        }
                        else
                        {
                            primer.setBlockState(x, k, z, (Block)Blocks.DIRT.getDefaultState());
                        }
                    }
                }
            }
        }
    }
}