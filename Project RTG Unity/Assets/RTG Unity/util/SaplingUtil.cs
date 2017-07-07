namespace rtg.util
{
    //import net.minecraft.block.BlockSapling;
    using generic.block;
    //import net.minecraft.block.state.IBlockState;
    using generic.block.state;
    //import net.minecraft.init.Blocks;
    using generic.init;

    using rtg.api.util;
    using UnityEngine;
    using System;

public class SaplingUtil
{

    public static int getMetaFromState(IBlockState state)
    {

        try
        {

            if (!(state.getBlock() == Blocks.SAPLING)) {
                Debug.Log("Could not get sapling meta from non-sapling BlockState " + state.getBlock() + ".");
                return 0;
            }

            return (int)state.getStateFromMeta();
        }
        catch (Exception e)
        {

            Debug.LogWarning("Could not get sapling meta from state. Reason: " + e.Message);
            return 0;
        }
    }

    public static IBlockState getSaplingFromLeaves(IBlockState leavesBlock)
    {

        if (leavesBlock == Blocks.LEAVES.getDefaultState())
        {
            return (IBlockState)Blocks.SAPLING.getDefaultState();
        }
        else if (leavesBlock == BlockUtil.getStateLeaf(1))
        {
            return BlockUtil.getStateSapling(1);
        }
        else if (leavesBlock == BlockUtil.getStateLeaf(2))
        {
            return BlockUtil.getStateSapling(2);
        }
        else if (leavesBlock == BlockUtil.getStateLeaf(3))
        {
            return BlockUtil.getStateSapling(3);
        }
        else if (leavesBlock == Blocks.LEAVES2.getDefaultState())
        {
            return BlockUtil.getStateSapling(4);
        }
        else if (leavesBlock == BlockUtil.getStateLeaf2(1))
        {
            return BlockUtil.getStateSapling(5);
        }
        else
        {
            return (IBlockState)Blocks.SAPLING.getDefaultState();
        }
    }
}
}