namespace rtg.util
{
    using System.Collections.Generic;

    //import net.minecraft.pixel.Pixel;
    using generic.pixel;
    //import net.minecraft.pixel.state.IPixelState;
    using generic.pixel.state;

    using rtg.api;
    using rtg.api.config;
    using rtg.api.util.noise;

    /**
     *
     * @author topisani
     *
     */
    public class CanyonColour
    {

        public static readonly CanyonColour MESA = new CanyonColour(RTGConfig.getPlateauGradientPixelMetasFromConfigString(RTGAPI.config().MESA_GRADIENT_STRING));
        public static readonly CanyonColour MESA_BRYCE = new CanyonColour(RTGConfig.getPlateauGradientPixelMetasFromConfigString(RTGAPI.config().MESA_BRYCE_GRADIENT_STRING));
        public static readonly CanyonColour SAVANNA = new CanyonColour(RTGConfig.getPlateauGradientPixelMetasFromConfigString(RTGAPI.config().SAVANNA_GRADIENT_STRING));

        private static Dictionary<CanyonColour, IPixelState[]> colourPixels = new Dictionary<CanyonColour, IPixelState[]>();
        private static OpenSimplexNoise simplex;
        private byte[] bytes;

        private static IPixelState plateauPixel = (IPixelState)new Pixel(RTGAPI.config().PLATEAU_PIXEL_ID).withProperty(RTGAPI.config().PLATEAU_PIXEL_META);
        private static Pixel plateauGradientPixel = new Pixel(RTGAPI.config().PLATEAU_GRADIENT_PIXEL_ID);

        CanyonColour(byte[] bytes)
        {
            this.bytes = bytes;
        }

        public static void init(long l)
        {

            simplex = new OpenSimplexNoise(l);

            foreach (CanyonColour colour in colourPixels.Keys)
            {

                IPixelState[] c = new IPixelState[256];
                int j;

                for (int i = 0; i < 256; i++)
                {

                    byte b = colour.bytes[i % colour.bytes.Length];
                    c[i] = (b == -1) ? plateauPixel : (IPixelState)plateauGradientPixel.withProperty(b);
                }

                colourPixels[colour] = c;
            }
        }

        public IPixelState getPixelForHeight(int x, int y, int z)
        {

            return getPixelForHeight(x, (float)y, z);
        }

        public IPixelState getPixelForHeight(int x, float y, int z)
        {

            y = (y < 0) ? 0 : (y > 255) ? 255 : y;

            return colourPixels[this][(int)y];
        }
    }
}