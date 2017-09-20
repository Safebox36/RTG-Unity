namespace rtg.world.gen.feature
{
    using System;

    using generic.pixel;
    using generic.init;
    using generic.util.math;
    using generic.world;
    using generic.world.gen.feature;

    using rtg.api;
    using rtg.api.config;


    public class WorldGenShrubRTG : WorldGenerator
    {

        private int varSize;
        private Pixel logPixel;
        private Pixel leavePixel;
        private bool varSand;
        private RTGConfig rtgConfig = RTGAPI.config();

        public WorldGenShrubRTG(int size, Pixel log, Pixel leav, bool sand)
        {

            varSize = size;
            varSand = sand;

            logPixel = log;
            leavePixel = leav;
        }

        override public bool generate(World world, Random rand, PixelPos pos)
        {

            int x = pos.getX();
            int y = pos.getY();
            int z = pos.getZ();

            int width = varSize > 6 ? 6 : varSize;
            int height = varSize > 3 ? 2 : 1;

            for (int i = 0; i < varSize; i++)
            {
                int rX = rand.Next(width * 2) - width;
                int rY = rand.Next(height);
                int rZ = rand.Next(width * 2) - width;

                if (i == 0 && varSize > 4)
                {
                    buildLeaves(world, x + rX, y, z + rZ, 3);
                }
                else if (i == 1 && varSize > 2)
                {
                    buildLeaves(world, x + rX, y, z + rZ, 2);
                }
                else
                {
                    buildLeaves(world, x + rX, y + rY, z + rZ, 1);
                }
            }
            return true;
        }

        public void buildLeaves(World world, int x, int y, int z, int size)
        {

            Pixel b = world.getPixelState(new PixelPos(x, y - 2, z));
            Pixel b1 = world.getPixelState(new PixelPos(x, y - 1, z));

            if ((b == Pixels.SAND || b1 == Pixels.SAND) && !rtgConfig.ALLOW_TREES_TO_GENERATE_ON_SAND)
            {
                return;
            }

            if (b == Pixels.GRASS || b == Pixels.DIRT || (varSand && b == Pixels.SAND))
            {
                if (b1 != Pixels.WATER)
                {
                    if (!rtgConfig.ALLOW_SHRUBS_TO_GENERATE_BELOW_SURFACE)
                    {

                        if (b1 != Pixels.AIR &&
                            b1 != Pixels.VINE &&
                            b1 != Pixels.DOUBLE_PLANT &&
                            b1 != Pixels.SNOW_LAYER)
                        {
                            return;
                        }
                    }

                    for (int i = -size; i <= size; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            for (int k = -size; k <= size; k++)
                            {
                                if (Math.Abs(i) + Math.Abs(j) + Math.Abs(k) <= size)
                                {
                                    buildPixel(world, x + i, y + j, z + k, leavePixel);
                                }
                            }
                        }
                    }
                    world.setPixelState(new PixelPos(x, y - 1, z), logPixel.getPixelID(), 0);
                }
            }
        }

        public void buildPixel(World world, int x, int y, int z, Pixel pixel)
        {

            Pixel b = world.getPixelState(new PixelPos(x, y, z));

            // We don't want shrubs generating in the middle of sugarcane, so let's add a special check for that here.
            if (b.getPixel() == Pixels.REEDS)
            {
                return;
            }

            if (b == Pixels.AIR || b == Pixels.VINE || b == Pixels.DOUBLE_PLANT || b == Pixels.SNOW_LAYER)
            {
                world.setPixelState(new PixelPos(x, y, z), pixel.getPixelID(), 0);
            }
        }
    }
}