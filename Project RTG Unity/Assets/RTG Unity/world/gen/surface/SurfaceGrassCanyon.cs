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

    public class SurfaceGrassCanyon : SurfaceBase
    {

        private byte claycolor;

        public SurfaceGrassCanyon(BiomeConfig config, IBlockState top, IBlockState fill, byte clayByte) : base(config, top, fill)
        {
            claycolor = clayByte;
        }

        override public void paintTerrain(ChunkPrimer primer, int i, int j, int x, int z, int depth, RTGWorld rtgWorld, float[] noise, float river, Biome[] _base)
        {

            Random rand = rtgWorld.rand;
            float c = CliffCalculator.calc(x, z, noise);
            bool cliff = c > 1.3f ? true : false;

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

                    if (depth > -1 && depth < 12)
                    {
                        if (cliff)
                        {
                            primer.setBlockState(x, k, z, BlockUtil.getStateClay(claycolor));
                        }
                        else
                        {
                            if (depth > 4)
                            {
                                primer.setBlockState(x, k, z, BlockUtil.getStateClay(claycolor));
                            }
                            else
                            {
                                if (depth == 0)
                                {
                                    primer.setBlockState(x, k, z, topBlock);
                                }
                                else
                                {
                                    primer.setBlockState(x, k, z, fillerBlock);
                                }
                            }
                        }
                    }
                    else if (k > 63)
                    {
                        primer.setBlockState(x, k, z, BlockUtil.getStateClay(claycolor));
                    }
                }
            }
        }
    }
}