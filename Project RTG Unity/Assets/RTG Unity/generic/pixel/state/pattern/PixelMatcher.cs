namespace generic.pixel.pattern
{
    using System;
    //import net.minecraft.pixel.Pixel;
    using generic.pixel;
    //import net.minecraft.pixel.Pixel;
    using generic.pixel;

    public class PixelMatcher : Pixel
    {
        private readonly Pixel pixel;

        private PixelMatcher(Pixel pixelType)
        {
            this.pixel = pixelType;
        }

        public static PixelMatcher forPixel(Pixel pixelType)
        {
            return new PixelMatcher(pixelType);
        }

        public bool apply(Pixel pixel)
        {
            return pixel != null && pixel.getPixel() == this.pixel;
        }
    }
}