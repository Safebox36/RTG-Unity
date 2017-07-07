namespace generic.world.chunk
{
    public class ChunkPrimer : Chunk
    {
        public ChunkPrimer(World worldIn, int x, int z) : base(worldIn, x, z)
        {

        }

        public block.Block getBlockState(int index)
        {
            return new block.Block();
        }

        public block.Block getBlockState(int x, int y, int z)
        {
            return new block.Block();
        }

        public void setBlockState(int index, block.Block state)
        {

        }

        public void setBlockState(int x, int y, int z, block.Block state)
        {

        }
    }
}