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

    using rtg.api.world;
    using rtg.api.config;
    using rtg.api.util;

    public class SurfaceRedDesert : SurfaceBase
    {

        private IBlockState cliffBlock1;
        private IBlockState bottomBlock;

        public SurfaceRedDesert(BiomeConfig config) : base(config, BlockUtil.getStateSand(1), BlockUtil.getStateSand(1))
        {

            bottomBlock = (IBlockState)Blocks.SANDSTONE.getDefaultState();
            cliffBlock1 = BlockUtil.getStateClay(14);
        }

        override public void paintTerrain(ChunkPrimer primer, int i, int j, int x, int z, int depth, RTGWorld rtgWorld, float[] noise, float river, Biome[] _base)
        {

            Random rand = rtgWorld.rand;
            float c = CliffCalculator.calc(x, z, noise);
            bool cliff = c > 1.4f ? true : false;

            for (int k = 255; k > -1; k--)
            {
                Block b = primer.getBlockState(x, k, z).getBlock();
                if (b == Blocks.AIR)
                {
                    depth = -1;
                }
                else if (b == Blocks.STONE)
                {
                    depth++;

                    if (cliff)
                    {
                        if (depth < 6)
                        {
                            primer.setBlockState(x, k, z, cliffBlock1);
                        }
                    }
                    else if (depth < 6)
                    {
                        if (depth == 0 && k > 61)
                        {
                            primer.setBlockState(x, k, z, topBlock);
                        }
                        else if (depth < 4)
                        {
                            primer.setBlockState(x, k, z, fillerBlock);
                        }
                        else
                        {
                            primer.setBlockState(x, k, z, (Block)bottomBlock.getBlock().getDefaultState());
                        }
                    }
                }
            }
        }
    }
}