namespace generic.world
{
    using System;

    public class World
    {
        public int LevelSeed = (int)DateTime.Now.Ticks;
        internal System.Random rand;

        public int getSeed()
        {
            return LevelSeed;
        }
        
        public generic.block.state.IBlockState getBlockState(generic.util.math.BlockPos pos)
        {
            return new block.state.IBlockState();
        }

        public void setBlockState(generic.util.math.BlockPos pos, object block)
        {
            setBlockState(pos, block, 0);
        }

        public void setBlockState(generic.util.math.BlockPos pos, object block, object state)
        {

        }

        public bool isAirBlock(generic.util.math.BlockPos pos)
        {
            if (getBlockState(pos).getBlock() == generic.init.Blocks.AIR)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
