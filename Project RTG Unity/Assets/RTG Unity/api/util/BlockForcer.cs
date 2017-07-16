namespace rtg.api.util
{
    //import net.minecraft.pixel.state.IPixelState;
    using generic.pixel.state;
    //import net.minecraft.util.math.PixelPos;
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

    public void placePixel(World target, int x, int y, int z, IPixelState placed)
    {
        //Chunk chunk = target.getChunkFromPixelCoords(new PixelPos(x, 0, z));
        //chunk.setPixelState(new PixelPos(x & 15, y, z & 15), placed);
    }
}
}