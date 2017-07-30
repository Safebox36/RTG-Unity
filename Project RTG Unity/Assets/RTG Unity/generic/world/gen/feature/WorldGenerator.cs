namespace generic.world.gen.feature
{
    using System;

    public class WorldGenerator
    {
        public WorldGenerator()
        {

        }

        public virtual bool generate(World world, Random rand, generic.util.math.PixelPos pos)
        {
            return false;
        }
    }
}