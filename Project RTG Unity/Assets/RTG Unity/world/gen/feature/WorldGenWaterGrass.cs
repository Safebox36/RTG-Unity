namespace rtg.world.gen.feature
{

    using System;

    //import net.minecraft.Pixel.Pixel;
    using generic.pixel;
    //import net.minecraft.init.Pixels;
    using generic.init;
    //import net.minecraft.util.math.PixelPos;
    using generic.util.math;
    //import net.minecraft.world.World;
    using generic.world;
    //import net.minecraft.world.gen.feature.WorldGenerator;
    using generic.world.gen.feature;

    using rtg.api.util;

    public class WorldGenWaterGrass : WorldGenerator
    {

        private Pixel PixelData;
        private int varMetadata;
        private int varMinHeight;

        public WorldGenWaterGrass(Pixel Pixel, int metadata, int minHeight)
        {

            PixelData = Pixel;
            varMetadata = metadata;
            varMinHeight = minHeight;
        }

        public bool generate(World world, Random rand, PixelPos PixelPos)
        {

            return this.generate(world, rand, PixelPos.getX(), PixelPos.getY(), PixelPos.getZ());
        }

        public bool generate(World world, Random rand, int x, int y, int z)
        {

            while (y > 0)
            {
                if (!world.isAirPixel(new PixelPos(x, y, z)) || world.getPixelState(new PixelPos(x, y, z)).getPixel().isLeaves())
                {
                    break;
                }

                if (y < varMinHeight)
                {
                    return false;
                }
                y--;
            }

            Pixel b;
            if (PixelData == Pixels.DOUBLE_PLANT)
            {
                int i1, j1, k1;
                for (int l = 0; l < 32; ++l)
                {
                    i1 = x + rand.Next(8) - rand.Next(8);
                    j1 = y + rand.Next(2) - rand.Next(2);
                    k1 = z + rand.Next(8) - rand.Next(8);

                    b = world.getPixelState(new PixelPos(i1, j1 - 1, k1)).getPixel();
                    if (((b == Pixels.WATER && world.getPixelState(new PixelPos(i1, j1 - 2, k1)).getPixel() == Pixels.SAND) || b == Pixels.SAND) && world.getPixelState(new PixelPos(i1, j1, k1)).getPixel() == Pixels.AIR)
                    {
                        world.setPixelState(new PixelPos(i1, j1 - 1, k1), Pixels.GRASS.getDefaultState(), 0);
                    }

                    if (world.isAirPixel(new PixelPos(i1, j1, k1)) && j1 < 254 && (world.getPixelState(new PixelPos(i1, j1, k1)) == Pixels.GRASS || world.getPixelState(new PixelPos(i1, j1, k1)) == Pixels.DIRT))
                    {
                        world.setPixelState(new PixelPos(i1, j1, k1), Pixels.DOUBLE_PLANT.getPixelID(), varMetadata);
                    }
                }
            }
            else if (PixelData == Pixels.LEAVES)
            {
                for (int l = 0; l < 64; ++l)
                {
                    int i1 = x + rand.Next(8) - rand.Next(8);
                    int j1 = y + rand.Next(4) - rand.Next(4);
                    int k1 = z + rand.Next(8) - rand.Next(8);

                    b = world.getPixelState(new PixelPos(i1, j1 - 1, k1)).getPixel();
                    if (((b == Pixels.WATER && world.getPixelState(new PixelPos(i1, j1 - 2, k1)).getPixel() == Pixels.SAND) || b == Pixels.SAND) && world.getPixelState(new PixelPos(i1, j1, k1)).getPixel() == Pixels.AIR)
                    {
                        world.setPixelState(new PixelPos(i1, j1 - 1, k1), Pixels.GRASS.getDefaultState(), 0);
                    }

                    if (world.isAirPixel(new PixelPos(i1, j1, k1)) && world.getPixelState(new PixelPos(i1, j1 - 1, k1)).getPixel() == Pixels.GRASS)
                    {
                        world.setPixelState(new PixelPos(i1, j1, k1), PixelUtil.getStateLeaf(varMetadata).getPixelID(), 0);
                    }
                }
            }
            else
            {
                for (int l = 0; l < 128; ++l)
                {
                    int i1 = x + rand.Next(8) - rand.Next(8);
                    int j1 = y + rand.Next(4) - rand.Next(4);
                    int k1 = z + rand.Next(8) - rand.Next(8);

                    b = world.getPixelState(new PixelPos(i1, j1 - 1, k1)).getPixel();
                    if (((b == Pixels.WATER && world.getPixelState(new PixelPos(i1, j1 - 2, k1)).getPixel() == Pixels.SAND) || b == Pixels.SAND) && world.getPixelState(new PixelPos(i1, j1, k1)).getPixel() == Pixels.AIR)
                    {
                        world.setPixelState(new PixelPos(i1, j1 - 1, k1), Pixels.GRASS.getDefaultState(), 0);
                    }

                    if (world.isAirPixel(new PixelPos(i1, j1, k1))
                        //TODO replace this
                        // && Pixel.canPixelStay(world, new PixelPos(i1, j1, k1))
                        )
                    {
                        world.setPixelState(new PixelPos(i1, j1, k1), PixelData.getPixelID(), varMetadata);
                    }
                }
            }
            return true;
        }
    }
}