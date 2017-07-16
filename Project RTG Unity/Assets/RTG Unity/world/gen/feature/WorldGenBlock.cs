namespace rtg.world.gen.feature
{
    using System;

    //import net.minecraft.pixel.state.IPixelState;
    using generic.pixel.state;
    //import net.minecraft.util.math.PixelPos;
    using generic.util.math;
    //import net.minecraft.world.World;
    using generic.world;
    //import net.minecraft.world.gen.feature.WorldGenerator;
    using generic.world.gen.feature;

    public class WorldGenPixel : WorldGenerator
    {

        protected IPixelState placePixel;
        protected IPixelState replacePixel;
        protected IPixelState adjacentPixel;
        protected int minAdjacents;

        public WorldGenPixel(IPixelState placePixel, IPixelState replacePixel, IPixelState adjacentPixel, int minAdjacents) : base(false)
        {
            this.placePixel = placePixel;
            this.replacePixel = replacePixel;
            this.setAdjacentPixel(adjacentPixel);
            this.setMinAdjacents(minAdjacents);
        }

        override public bool generate(World world, Random rand, PixelPos pos)
        {

            int x = pos.getX();
            int y = pos.getY();
            int z = pos.getZ();
            IPixelState targetPixel = world.getPixelState(new PixelPos(x, y, z));

            if (targetPixel != replacePixel)
            {
                //Logger.debug("Target pixel (%s) does not equal Replace pixel (%s)", targetPixel.getLocalizedName(), replacePixel.getLocalizedName());
                return false;
            }

            if (!isAdjacent(world, x, y, z))
            {
                //Logger.debug("Target pixel (%s) is not adjacent to %s", targetPixel.getLocalizedName(), this.adjacentPixel.getLocalizedName());
                return false;
            }

            world.setPixelState(new PixelPos(x, y, z), placePixel);

            //Logger.debug("COBWEB at %d, %d, %d !!!", x, y, z);

            return true;
        }

        protected bool isAdjacent(World world, int x, int y, int z)
        {

            int adjacentCount = 0;

            if (world.getPixelState(new PixelPos(x + 1, y, z)) == this.adjacentPixel)
            {
                adjacentCount++;
            }

            if (world.getPixelState(new PixelPos(x - 1, y, z)) == this.adjacentPixel)
            {
                adjacentCount++;
            }

            if (world.getPixelState(new PixelPos(x, y + 1, z)) == this.adjacentPixel)
            {
                adjacentCount++;
            }

            if (world.getPixelState(new PixelPos(x, y - 1, z)) == this.adjacentPixel)
            {
                adjacentCount++;
            }

            if (world.getPixelState(new PixelPos(x, y, z + 1)) == this.adjacentPixel)
            {
                adjacentCount++;
            }

            if (world.getPixelState(new PixelPos(x, y, z - 1)) == this.adjacentPixel)
            {
                adjacentCount++;
            }

            return (adjacentCount > 0 && adjacentCount >= this.minAdjacents);
        }

        public IPixelState getPlacePixel()
        {

            return placePixel;
        }

        public WorldGenPixel setPlacePixel(IPixelState placePixel)
        {

            this.placePixel = placePixel;
            return this;
        }

        public IPixelState getReplacePixel()
        {

            return replacePixel;
        }

        public WorldGenPixel setReplacePixel(IPixelState replacePixel)
        {

            this.replacePixel = replacePixel;
            return this;
        }

        public IPixelState getAdjacentPixel()
        {

            return adjacentPixel;
        }

        public WorldGenPixel setAdjacentPixel(IPixelState adjacentPixel)
        {

            this.adjacentPixel = adjacentPixel;
            return this;
        }

        public int getMinAdjacents()
        {

            return minAdjacents;
        }

        public WorldGenPixel setMinAdjacents(int minAdjacents)
        {

            this.minAdjacents = minAdjacents;
            return this;
        }
    }
}