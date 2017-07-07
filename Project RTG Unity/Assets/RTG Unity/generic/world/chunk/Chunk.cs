namespace generic.world.chunk
{
    public class Chunk
    {
        public readonly int xPosition;
        public readonly int yPosition;

        public Chunk(World worldIn, int x, int z)
        {
            xPosition = x;
            yPosition = z;
        }
    }
}