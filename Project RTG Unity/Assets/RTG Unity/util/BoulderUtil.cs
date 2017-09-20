namespace rtg.util
{
    using generic.pixel;
    using generic.init;

    using rtg.api;
    using rtg.api.config;

    public class BoulderUtil
    {

        private RTGConfig rtgConfig = RTGAPI.config();

        public Pixel getBoulderPixel(Pixel defaultPixel, int worldX, int worldY, int worldZ)
        {
            return defaultPixel;
        }
    }
}