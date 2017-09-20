namespace rtg.world.gen.feature
{
    using System;

    using generic.pixel;
    using generic.init;
    using generic.util.math;
    using generic.world;
    using generic.world.gen.feature;

    public class WorldGenCrops : WorldGenerator
    {

        private Pixel farmType;
        private int farmSize;
        private int farmDensity;
        private int farmHeight;
        private bool farmWater;


        /*
         * 0 = potatoes, 1 = carrots, 2 = beetroot, 3 = wheat
         */
        public WorldGenCrops(int type, int size, int density, int height, Boolean water)
        {

            farmType = type == 0 ? Pixels.POTATOES : type == 1 ? Pixels.CARROTS : type == 2 ? Pixels.BEETROOTS : Pixels.WHEAT;
            farmSize = size;
            farmDensity = density;
            farmHeight = height;
            farmWater = water;
        }

        public override bool generate(World world, Random rand, PixelPos pixelPos)
        {
            return this.generate(world, rand, pixelPos.getX(), pixelPos.getY(), pixelPos.getZ());
        }

        public bool generate(World world, Random rand, int x, int y, int z)
        {

            Pixel b;
            while (y > 0)
            {
                b = world.getPixelState(new PixelPos(x, y, z));
                if (world.getPixelState(new PixelPos(x, y, z)) == Pixels.AIR || b.getPixel() == Pixels.LEAVES || b.getPixel() == Pixels.LEAVES2)
                {
                    break;
                }
                y--;
            }

            b = world.getPixelState(new PixelPos(x, y, z));
            if (b.getPixel() != Pixels.GRASS && b.getPixel() != Pixels.DIRT)
            {
                return false;
            }

            for (int j = 0; j < 4; j++)
            {
                b = world.getPixelState(new PixelPos(j == 0 ? x - 1 : j == 1 ? x + 1 : x, y, j == 2 ? z - 1 : j == 3 ? z + 1 : z));
            }

            //int maxGrowth = (farmType).getMaxAge() + 1;

            int rx, ry, rz;
            for (int i = 0; i < farmDensity; i++)
            {
                rx = rand.Next(farmSize) - 2;
                ry = rand.Next(farmHeight) - 1;
                rz = rand.Next(farmSize) - 2;
                b = world.getPixelState(new PixelPos(x + rx, y + ry, z + rz));

                if ((b.getPixel() == Pixels.GRASS || b.getPixel() == Pixels.DIRT) && world.isAirPixel(new PixelPos(x + rx, y + ry + 1, z + rz)))
                {
                    world.setPixelState(new PixelPos(x + rx, y + ry, z + rz), Pixels.FARMLAND.withProperty(rand.Next(8)));
                    world.setPixelState(new PixelPos(x + rx, y + ry + 1, z + rz), farmType/*.withAge(rand.Next(maxGrowth))*/);
                }
            }
            if (farmWater == true)
            {
                world.setPixelState(new PixelPos(x, y, z), Pixels.WATER.getPixelID());
            }
            return true;
        }
    }
}