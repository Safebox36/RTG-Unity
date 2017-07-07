namespace generic.world.gen.feature
{
    using System;

    public class WorldGenerator
    {
        public WorldGenerator() : this(false)
        {

        }
        
        public WorldGenerator(bool ignore)  //don't know what this is for, thus far it's always set to false
        {

        }

        public virtual bool generate(World world, System.Random rand, generic.util.math.BlockPos pos)
        {
            return false;
        }
    }
}