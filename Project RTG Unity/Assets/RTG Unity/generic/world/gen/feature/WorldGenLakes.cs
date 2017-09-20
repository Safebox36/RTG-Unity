namespace generic.world.gen.feature
{
    using System;

    public class WorldGenLakes : rtg.world.gen.feature.WorldGenPond
    {
        public WorldGenLakes(pixel.Pixel fill) : base(fill)
        {

        }

        //Temporary for now, will implement a way to manipulate the pond size so as to make lakes larger.
        public new bool generate(World world, Random rand, generic.util.math.PixelPos pos)
        {
            return base.generate(world, rand, pos);
        }
    }
}