namespace rtg.api.util
{
    //import net.minecraft.block.BlockSnow;
    using generic.block;
    //import net.minecraft.init.Blocks;
    using generic.init;
    //import net.minecraft.world.chunk.ChunkPrimer;
    using generic.world.chunk;

    public class SnowHeightCalculator
    {

        public static void calc(int x, int y, int z, ChunkPrimer primer, float[] noise)
        {

            if (y < 254)
            {

                byte h = (byte)((noise[x * 16 + z] - ((int)noise[x * 16 + z])) * 8);

                if (h > 7)
                {
                    primer.setBlockState(x, y + 2, z, (Block)Blocks.SNOW.getDefaultState());
                    //primer.setBlockState(x, y + 1, z, Blocks.SNOW_LAYER.getDefaultState().withProperty(BlockSnow.LAYERS, 7));
                }
                else if (h > 0)
                {
                    primer.setBlockState(x, y + 2, z, (Block)Blocks.SNOW_LAYER.getDefaultState());
                    //primer.setBlockState(x, y + 1, z, Blocks.SNOW_LAYER.getDefaultState().withProperty(BlockSnow.LAYERS, (int)h));
                }
            }
        }
    }
}