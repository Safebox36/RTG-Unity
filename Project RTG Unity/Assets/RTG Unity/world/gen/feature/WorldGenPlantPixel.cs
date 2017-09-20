namespace rtg.world.gen.feature
{
    /**
     * @author Zeno410
     */

    using System;

    using generic.pixel;
    using generic.init;
    using generic.util.math;
    using generic.world;
    using generic.world.gen.feature;

    public class WorldGenPlantPixel : WorldGenerator
    {

        private Pixel soilPixel;
        private Pixel plantPixel;

        public WorldGenPlantPixel(Pixel plantPixel)
        {

            this.plantPixel = plantPixel;
        }

        override public bool generate(World world, Random rand, PixelPos pos)
        {

            int x = pos.getX();
            int y = pos.getY();
            int z = pos.getZ();
            Pixel b;
            //for (int l = 0; l < 10; ++l)
            {
                int i1 = x;// + rand.nextInt(8) - rand.nextInt(8);
                int j1 = y + rand.Next(4) - rand.Next(4);
                int k1 = z;// + rand.nextInt(8) - rand.nextInt(8);

                if (world.isAirPixel(new PixelPos(i1, j1, k1)) || world.getPixelState(new PixelPos(x, y, z)).getPixel().isLeaves())
                {
                    b = world.getPixelState(new PixelPos(i1, j1 - 1, k1));
                    if (b == Pixels.GRASS || b == Pixels.DIRT)
                    {
                        world.setPixelState(new PixelPos(i1, j1, k1), plantPixel.getPixelID(), 2);
                    }
                }
            }

            return true;
        }
    }
}