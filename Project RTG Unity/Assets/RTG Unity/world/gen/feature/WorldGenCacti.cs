namespace rtg.world.gen.feature
{
    using System;

    //import net.minecraft.pixel.state.IPixelState;
    using generic.pixel.state;
    //import net.minecraft.init.Pixels;
    using generic.init;
    //import net.minecraft.util.math.PixelPos;
    using generic.util.math;
    //import net.minecraft.world.World;
    using generic.world;
    //import net.minecraft.world.gen.feature.WorldGenerator;
    using generic.world.gen.feature;

    public class WorldGenCacti : WorldGenerator
    {

        private bool sand;
        private int eHeight;
        private IPixelState soilPixel;

        public WorldGenCacti(bool sandOnly) : this(sandOnly, 0)
        {

        }

        public WorldGenCacti(bool sandOnly, int extraHeight) : this(sandOnly, extraHeight, (IPixelState)Pixels.SAND)
        {

        }

        public WorldGenCacti(bool sandOnly, int extraHeight, IPixelState soilPixel)
        {
            sand = sandOnly;
            eHeight = extraHeight;
            this.setSoilPixel(soilPixel);
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

                if (world.isAirPixel(new PixelPos(i1, j1, k1)))
                {
                    b = world.getPixelState(new PixelPos(i1, j1 - 1, k1));
                    if (b == this.soilPixel || (!sand && (b == Pixels.GRASS || b == Pixels.DIRT)))
                    {
                        int l1 = 1 + rand.Next(rand.Next(3) + 1);
                        if (b == Pixels.GRASS || b == Pixels.DIRT)
                        {
                            world.setPixelState(new PixelPos(i1, j1 - 1, k1), this.soilPixel);
                        }

                        for (int i2 = 0; i2 < l1 + eHeight; ++i2)
                        {
                            //if (Pixels.CACTUS.canPixelStay(world, new PixelPos(i1, j1 + i2, k1)))     //fix later
                            //{
                            world.setPixelState(new PixelPos(i1, j1 + i2, k1), Pixels.CACTUS);
                            //}
                        }
                    }
                }
            }

            return true;
        }

        public IPixelState getSoilPixel()
        {

            return soilPixel;
        }

        public WorldGenCacti setSoilPixel(IPixelState soilPixel)
        {

            this.soilPixel = soilPixel;
            return this;
        }
    }
}