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
//import rtg.util.CanyonColour;
using rtg.api.util;

public class SurfaceCanyon : SurfaceBase
{

    private int grassRaise = 0;

public SurfaceCanyon(BiomeConfig config, IBlockState top, IBlockState fill, int grassHeight) : base(config, top, fill)
{
    grassRaise = grassHeight;
}

override public void paintTerrain(ChunkPrimer primer, int i, int j, int x, int z, int depth, RTGWorld rtgWorld, float[] noise, float river, Biome[] _base)
{

    Random rand = rtgWorld.rand;
    float c = CliffCalculator.calc(x, z, noise);
    bool cliff = c > 1.3f;

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
                    primer.setBlockState(x, k, z, Blocks.STAINED_HARDENED_CLAY);
                }
                else
                {
                    if (depth > 4)
                    {
                        primer.setBlockState(x, k, z, Blocks.STAINED_HARDENED_CLAY);
                    }
                    else if (k > 74 + grassRaise)
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
                    else if (k < 62)
                    {
                        primer.setBlockState(x, k, z, (Block)Blocks.DIRT.getDefaultState());
                    }
                    else if (k < 62 + grassRaise)
                    {
                        if (depth == 0)
                        {
                            primer.setBlockState(x, k, z, (Block)Blocks.GRASS.getDefaultState());
                        }
                        else
                        {
                            primer.setBlockState(x, k, z, (Block)Blocks.DIRT.getDefaultState());
                        }
                    }
                    else if (k < 75 + grassRaise)
                    {
                        if (depth == 0)
                        {
                            int r = (int)((k - (62 + grassRaise)) / 2f);
                            if (rand.Next(r + 1) == 0)
                            {
                                primer.setBlockState(x, k, z, (Block)Blocks.GRASS.getDefaultState());
                            }
                            else if (rand.Next((int)(r / 2f) + 1) == 0)
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
                primer.setBlockState(x, k, z, Blocks.STAINED_HARDENED_CLAY);
            }
        }
    }
}
}
}