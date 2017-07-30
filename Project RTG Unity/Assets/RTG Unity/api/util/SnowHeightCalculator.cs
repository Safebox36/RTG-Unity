namespace rtg.api.util
{
    //import net.minecraft.pixel.PixelSnow;
    //import net.minecraft.init.Pixels;
    using generic.init;
    //import net.minecraft.world.chunk.Chunk;
    using generic.world.chunk;

    public class SnowHeightCalculator
    {

        public static void calc(int x, int y, int z, Chunk primer, float[] noise)
        {

            if (y < 254)
            {

                byte h = (byte)((noise[x * 16 + z] - ((int)noise[x * 16 + z])) * 8);

                if (h > 7)
                {
                    primer.setPixelState(x, y + 2, z, Pixels.SNOW);
                    primer.setPixelState(x, y + 1, z, Pixels.SNOW_LAYER.withProperty(7));
                }
                else if (h > 0)
                {
                    primer.setPixelState(x, y + 2, z, Pixels.SNOW_LAYER);
                    primer.setPixelState(x, y + 1, z, Pixels.SNOW_LAYER.withProperty(h));
                }
            }
        }
    }
}