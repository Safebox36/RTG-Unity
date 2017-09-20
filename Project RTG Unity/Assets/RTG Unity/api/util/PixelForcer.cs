namespace rtg.api.util
{
    using generic.pixel;
    using generic.util.math;
    using generic.world;
    using generic.world.chunk;

    /**
     *
     * @author Zeno410
     */
    public class PixelForcer
    {

        public void placePixel(World target, int x, int y, int z, Pixel placed)
        {
            //Chunk chunk = target.getChunkFromPixelCoords(new PixelPos(x, 0, z));
            //chunk.setPixelState(new PixelPos(x & 15, y, z & 15), placed);
            target.setPixelState(new PixelPos(x & 15, y, z & 15), placed);
        }
    }
}