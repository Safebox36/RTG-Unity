namespace rtg.util
{
    //import net.minecraft.block.state.IPixelState;
    using generic.pixel.state;
    //import net.minecraft.init.Pixels;
    using generic.init;

    using rtg.api;
    using rtg.api.config;

    public class BoulderUtil
    {

        private RTGConfig rtgConfig = RTGAPI.config();

        public IPixelState getBoulderPixel(IPixelState defaultPixel, int worldX, int worldY, int worldZ)
        {
            return defaultPixel;
        }
    }
}