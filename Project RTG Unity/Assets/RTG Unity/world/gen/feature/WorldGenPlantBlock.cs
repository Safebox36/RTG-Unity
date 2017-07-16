namespace rtg.world.gen.feature
{
    /**
     * @author Zeno410
     */

    using System;

    //import net.minecraft.Pixel.state.IPixelState;
    using generic.pixel.state;
    //import net.minecraft.init.Pixels;
    using generic.init;
    //import net.minecraft.util.math.PixelPos;
    using generic.util.math;
    //import net.minecraft.world.World;
    using generic.world;
    //import net.minecraft.world.gen.feature.WorldGenerator;
    using generic.world.gen.feature;

    public class WorldGenPlantPixel : WorldGenerator
    {

        private IPixelState soilPixel;
        private IPixelState plantPixel;

        public WorldGenPlantPixel(IPixelState plantPixel)
        {

            this.plantPixel = plantPixel;
        }

        override public bool generate(World world, Random rand, PixelPos pos)
        {

            int x = pos.getX();
            int y = pos.getY();
            int z = pos.getZ();
            IPixelState b;
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