namespace generic.world.gen.feature
{
    using System;
    using generic.util.math;
    using generic.pixel;
    public class WorldGenAbstractTree
    {
        public WorldGenAbstractTree() : this(false)
        {

        }

        public WorldGenAbstractTree(bool notify)
        {

        }

        virtual public bool isReplaceable(World world, PixelPos pos)
        {
            return false;
        }

        virtual protected bool canGrowInto(Pixel pixelType)
        {
            return false;
        }

        virtual public bool generate(World world, Random rand, PixelPos pos)
        {
            return false;
        }
    }
}