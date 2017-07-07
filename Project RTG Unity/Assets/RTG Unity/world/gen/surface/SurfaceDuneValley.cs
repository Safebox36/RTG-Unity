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

    public class SurfaceDuneValley : SurfaceBase
    {

        private float valley;
        private bool dirt;
        private bool mix;

        public SurfaceDuneValley(BiomeConfig config, IBlockState top, IBlockState fill, float valleySize, bool d, bool m) : base(config, top, fill)
        {
            valley = valleySize;
            dirt = d;
            mix = m;
        }

        override public void paintTerrain(ChunkPrimer primer, int i, int j, int x, int z, int depth, RTGWorld rtgWorld, float[] noise, float river, Biome[] _base)
        {

            Random rand = rtgWorld.rand;
            OpenSimplexNoise simplex = rtgWorld.simplex;
            float h = (simplex.noise2(i / valley, j / valley) + 0.25f) * 65f;
            h = h < 1f ? 1f : h;
            float m = simplex.noise2(i / 12f, j / 12f);
            bool sand = false;

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
                        if (k > 90f + simplex.noise2(i / 24f, j / 24f) * 10f - h || (m < -0.28f && mix))
                        {
                            primer.setBlockState(x, k, z, (Block)Blocks.SAND.getDefaultState());
                            //base[x * 16 + z] = RealisticBiomeVanillaBase.vanillaDesert;
                            sand = true;
                        }
                        else if (dirt && m < 0.22f || k < 62)
                        {
                            primer.setBlockState(x, k, z, BlockUtil.getStateDirt(1));
                        }
                        else
                        {
                            primer.setBlockState(x, k, z, topBlock);
                        }
                    }
                    else if (depth < 6)
                    {
                        if (sand)
                        {
                            if (depth < 4)
                            {
                                primer.setBlockState(x, k, z, (Block)Blocks.SAND.getDefaultState());
                            }
                            else
                            {
                                primer.setBlockState(x, k, z, (Block)Blocks.SANDSTONE.getDefaultState());
                            }
                        }
                        else
                        {
                            primer.setBlockState(x, k, z, fillerBlock);
                        }
                    }
                }
            }
        }
    }
}