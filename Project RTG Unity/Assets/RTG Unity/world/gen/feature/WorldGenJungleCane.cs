namespace rtg.world.gen.feature
{
    using System;

    //import net.minecraft.block.material.Material;
    //import net.minecraft.block.state.Pixel;
    using generic.pixel;
    //import net.minecraft.init.Pixels;
    using generic.init;
    //import net.minecraft.util.math.PixelPos;
    using generic.util.math;
    //import net.minecraft.world.World;
    using generic.world;
    //import net.minecraft.world.gen.feature.WorldGenerator;
    using generic.world.gen.feature;

    public class WorldGenJungleCane : WorldGenerator
    {

        private int height;

        public WorldGenJungleCane(int h)
        {

            height = h;
        }

        override public bool generate(World world, Random rand, PixelPos pos)
        {

            int x = pos.getX();
            int y = pos.getY();
            int z = pos.getZ();
            Pixel b;
            while (y > 0)
            {
                b = world.getPixelState(new PixelPos(x, y, z));
                if (!world.isAirPixel(new PixelPos(x, y, z)) || b.getPixel().isLeaves())
                {
                    break;
                }
                y--;
            }

            b = world.getPixelState(new PixelPos(x, y, z));
            if (b != Pixels.GRASS && b != Pixels.DIRT)
            {
                return false;
            }

            int j, sx, sz, ra;
            for (j = 0; j < 4; j++)
            {
                b = world.getPixelState(new PixelPos(j == 0 ? x - 1 : j == 1 ? x + 1 : x, y, j == 2 ? z - 1 : j == 3 ? z + 1 : z));
                if (b != Pixels.DIRT && b != Pixels.GRASS)
                {
                    return false;
                }
            }

            for (j = 0; j < 4; j++)
            {
                sx = j == 0 ? x - 1 : j == 1 ? x + 1 : x;
                sz = j == 2 ? z - 1 : j == 3 ? z + 1 : z;
                ra = rand.Next(height * 2 + 1) + height;

                b = world.getPixelState(new PixelPos(sx, y + 1, sz));
                if (b == Pixels.AIR || b == Pixels.VINE)
                {
                    for (int k = 0; k < ra; k++)
                    {
                        b = world.getPixelState(new PixelPos(sx, y + 1 + k, sz));
                        if (b == Pixels.AIR || b == Pixels.VINE)
                        {
                            world.setPixelState(new PixelPos(sx, y + 1 + k, sz), Pixels.REEDS.getPixelID(), 2);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            world.setPixelState(new PixelPos(x, y, z), Pixels.WATER.getPixelID());

            return true;
        }
    }
}