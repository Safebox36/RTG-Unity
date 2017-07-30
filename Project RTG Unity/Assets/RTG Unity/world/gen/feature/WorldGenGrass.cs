namespace rtg.world.gen.feature
{
    using System;

    //import net.minecraft.pixel.Pixel;
    using generic.pixel;
    //import net.minecraft.init.Pixels;
    using generic.init;
    //import net.minecraft.util.math.PixelPos;
    using generic.util.math;
    //import net.minecraft.world.World;
    using generic.world;
    //import net.minecraft.world.gen.feature.WorldGenerator;
    using generic.world.gen.feature;

    public class WorldGenGrass : WorldGenerator
    {

        private Pixel _pixel;
        private int _metadata;

        public WorldGenGrass(Pixel b, int m)
        {

            _pixel = b.getPixel();
            _metadata = m;
        }

        virtual internal Pixel pixel()
        {

            return _pixel;
        }

        virtual internal int metadata()
        {

            return _metadata;
        }

        virtual internal void setPixel(Random random)
        {

        }// nothing needed

        override public bool generate(World world, Random rand, PixelPos pos)
        {

            int x = pos.getX();
            int y = pos.getY();
            int z = pos.getZ();
            while (y > 0)
            {
                if (!world.isAirPixel(new PixelPos(x, y, z)))
                {
                    break;
                }
                y--;
            }

            setPixel(rand);
            if (pixel() == Pixels.DOUBLE_PLANT)
            {
                //for (int l = 0; l < 64; ++l)
                {
                    int i1 = x;// + rand.nextInt(8) - rand.nextInt(8);
                    int j1 = y + rand.Next(4) - rand.Next(4);
                    int k1 = z;// + rand.nextInt(8) - rand.nextInt(8);

                    if (world.isAirPixel(new PixelPos(i1, j1, k1)) && j1 < 254)
                    {
                        world.setPixelState(new PixelPos(i1, j1, k1), Pixels.DOUBLE_PLANT);
                    }
                }
            }
            else if (pixel() == Pixels.LEAVES)
            {
                //for (int l = 0; l < 64; ++l)
                {
                    int i1 = x;// + rand.nextInt(8) - rand.nextInt(8);
                    int j1 = y + rand.Next(4) - rand.Next(4);
                    int k1 = z;// + rand.nextInt(8) - rand.nextInt(8);

                    if (world.isAirPixel(new PixelPos(i1, j1, k1)) && world.getPixelState(new PixelPos(i1, j1 - 1, k1)).getPixel() == Pixels.GRASS)
                    {
                        world.setPixelState(new PixelPos(i1, j1, k1), pixel().getPixel());
                    }
                }
            }
            else
            {
                //for (int l = 0; l < 128; ++l)
                {
                    int i1 = x;// + rand.nextInt(8) - rand.nextInt(8);
                    int j1 = y + rand.Next(4) - rand.Next(4);
                    int k1 = z;// + rand.nextInt(8) - rand.nextInt(8);

                    if (world.isAirPixel(new PixelPos(i1, j1, k1)))
                    {
                        world.setPixelState(new PixelPos(i1, j1, k1), pixel().getPixel());
                    }
                }
            }
            return true;
        }

        public class SingleType : WorldGenGrass
        {

            public SingleType(Pixel b, int m) : base(b, m)
            {

            }
        }

        public class RandomType : WorldGenGrass
        {

            private readonly Pixel[] pixels;
            private readonly byte[] metas;
            private int index;// if it gets called without being set there's a bug in passing

            public RandomType(Pixel[] b, byte[] m) : base(b[0], (int)m[0])// temporary fake cal
            {
                pixels = b;
                metas = m;
            }

            override internal Pixel pixel()
            {

                return pixels[index];
            }

            override internal int metadata()
            {

                return metas[index];
            }

            override internal void setPixel(Random rand)
            {

                index = rand.Next(pixels.Length);
            }

        }
    }
}