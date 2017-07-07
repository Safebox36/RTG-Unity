namespace rtg.api.util
{
    //import net.minecraft.block.state.IBlockState;
    using generic.block.state;
    //import net.minecraft.util.math.BlockPos;
    //import net.minecraft.world.World;
    using generic.world;
    //import net.minecraft.world.chunk.Chunk;
    using generic.world.chunk;

/**
 *
 * @author Zeno410
 */
public class BlockForcer
{

    public void placeBlock(World target, int x, int y, int z, IBlockState placed)
    {
        //Chunk chunk = target.getChunkFromBlockCoords(new BlockPos(x, 0, z));
        //chunk.setBlockState(new BlockPos(x & 15, y, z & 15), placed);
    }
}
}