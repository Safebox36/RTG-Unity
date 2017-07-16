namespace generic.world.chunk
{
    public class Chunk
    {
        public readonly int xPosition;
        public readonly int yPosition;
        public World ChunkFile;

        public Chunk(World worldIn, int x, int z)
        {
            ChunkFile = worldIn;
            xPosition = x;
            yPosition = z;
        }
    }
}