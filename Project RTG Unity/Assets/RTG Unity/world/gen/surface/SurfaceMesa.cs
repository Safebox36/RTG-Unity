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

    using rtg.api.util.noise;
    using rtg.api.world;
    using rtg.api.config;
    using rtg.util;
    using rtg.api.util;

    public class SurfaceMesa : SurfaceBase
    {

        private int[] claycolor = new int[100];

        public SurfaceMesa(BiomeConfig config, Block top, byte topByte, Block fill, byte fillByte) : base(config, top, fill)
        {

            int[] c = new int[] { 1, 8, 0 };
            OpenSimplexNoise simplex = new OpenSimplexNoise(2L);

            float n;
            for (int i = 0; i < 100; i++)
            {
                n = simplex.noise1(i / 3f) * 3f + simplex.noise1(i / 1f) * 0.3f + 1.5f;
                n = n >= 3f ? 2.9f : n < 0f ? 0f : n;
                claycolor[i] = c[(int)n];
            }
        }

        public byte getClayColorForHeight(int k)
        {

            k -= 60;
            k = k < 0 ? 0 : k > 99 ? 99 : k;
            return (byte)claycolor[k];
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
                            primer.setBlockState(x, k, z, CanyonColour.MESA.getBlockForHeight(i, k, j));
                        }
                        else
                        {
                            if (depth > 4)
                            {
                                primer.setBlockState(x, k, z, CanyonColour.MESA.getBlockForHeight(i, k, j));
                            }
                            else if (k > 77)
                            {
                                if (rand.Next(5) == 0)
                                {
                                    primer.setBlockState(x, k, z, (Block)Blocks.DIRT.getDefaultState());
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
                            else if (k < 69)
                            {
                                primer.setBlockState(x, k, z, (Block)Blocks.DIRT.getDefaultState());
                            }
                            else if (k < 78)
                            {
                                if (depth == 0)
                                {
                                    if (k < 72 && rand.Next(k - 69 + 1) == 0)
                                    {
                                        primer.setBlockState(x, k, z, (Block)Blocks.DIRT.getDefaultState());
                                    }
                                    else if (rand.Next(5) == 0)
                                    {
                                        primer.setBlockState(x, k, z, (Block)Blocks.DIRT.getDefaultState());
                                    }
                                    else
                                    {
                                        primer.setBlockState(x, k, z, topBlock);
                                    }
                                }
                                else
                                {
                                    primer.setBlockState(x, k, z, fillerBlock);
                                }
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
                        primer.setBlockState(x, k, z, CanyonColour.MESA.getBlockForHeight(i, k, j));
                    }
                }
            }
        }
    }
}