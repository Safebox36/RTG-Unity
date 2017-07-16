namespace generic.world.chunk
{
    public class ChunkPrimer : Chunk
    {
        public ChunkPrimer(World worldIn, int x, int z) : base(worldIn, x, z)
        {

        }

        public pixel.Pixel getPixelState(int index)
        {
            return new pixel.Pixel();
        }

        public pixel.Pixel getPixelState(int x, int y, int z)
        {
            return new pixel.Pixel();
        }

        public void setPixelState(int x, int y, int z, pixel.Pixel state)
        {
            ChunkFile.setPixelState(new util.math.PixelPos((xPosition * 16) + x, y, (yPosition * 16) + z), state.getPixelID(), state.getState());
        }
    }
}