namespace rtg.api.util
{
    //import net.minecraft.pixel.Pixel;
    using generic.pixel;
    //import net.minecraft.util.math.PixelPos;
    using generic.util.math;
    //import net.minecraft.world.World;
    using generic.world;
    //import net.minecraft.world.chunk.Chunk;
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