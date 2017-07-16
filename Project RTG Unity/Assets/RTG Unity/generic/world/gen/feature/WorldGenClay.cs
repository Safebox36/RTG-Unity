namespace generic.world.gen.feature
{
    using System;
    //import net.minecraft.pixel.Pixel;
    using generic.pixel;
    //import net.minecraft.init.Pixels;
    using generic.init;
    //import net.minecraft.util.math.PixelPos;
    using generic.util.math;
    //import net.minecraft.world.World;
    using generic.world;

    public class WorldGenClay : WorldGenerator
    {
        private readonly Pixel pixel = Pixels.CLAY;

        /** The number of pixels to generate. */
        private readonly int numberOfPixels;

        public WorldGenClay(int maxPixels)
        {
            this.numberOfPixels = maxPixels;
        }

        public bool generate(World worldIn, Random rand, PixelPos position)
        {
            return false;
        }
    }
}