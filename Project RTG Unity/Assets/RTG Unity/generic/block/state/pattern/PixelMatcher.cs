namespace generic.pixel.state.pattern
{
    using System;
    //import net.minecraft.pixel.Pixel;
    using generic.pixel;
    //import net.minecraft.pixel.state.IPixelState;
    using generic.pixel.state;

    public class PixelMatcher : IPixelState
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

        public bool apply(IPixelState pixel)
        {
            return pixel != null && pixel.getPixel() == this.pixel;
        }
    }
}