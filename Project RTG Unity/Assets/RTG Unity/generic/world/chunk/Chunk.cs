namespace generic.world.chunk
{
    public class Chunk
    {
        public readonly int xPosition;
        public readonly int yPosition;
        private World worldFile;

        public Chunk()
        {

        }

        public Chunk(World worldFile, int x, int z)
        {
            this.worldFile = worldFile;
            xPosition = x;
            yPosition = z;
        }

        public pixel.Pixel getPixelState(int index)
        {
            return new pixel.Pixel();
        }

        public pixel.Pixel getPixelState(int x, int y, int z)
        {
            return worldFile.getPixelState(new util.math.PixelPos(x, y, z));
        }

        public void setPixelState(int x, int y, int z, pixel.Pixel state)
        {
            worldFile.setPixelState(new util.math.PixelPos((xPosition * 16) + x, y, (yPosition * 16) + z), state.getPixelID(), state.getState());
        }
    }
}