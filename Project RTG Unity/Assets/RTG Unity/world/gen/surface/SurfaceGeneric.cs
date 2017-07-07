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

    public class SurfaceGeneric : SurfaceBase
    {

        public SurfaceGeneric(BiomeConfig config, IBlockState top, IBlockState filler) : base(config, top, filler)
        {

        }

        override public void paintTerrain(ChunkPrimer primer, int i, int j, int x, int z, int depth, RTGWorld rtgWorld, float[] noise, float river, Biome[] _base)
        {

            Random rand = rtgWorld.rand;

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

                    if (depth == 0 && k > 61)
                    {
                        primer.setBlockState(x, k, z, topBlock);
                    }
                    else if (depth < 4)
                    {
                        primer.setBlockState(x, k, z, fillerBlock);
                    }
                }
            }
        }
    }
}