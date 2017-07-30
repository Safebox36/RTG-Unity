namespace rtg.world.gen.feature
{
    using System;

    //import net.minecraft.Pixel.material.Material;
    //import net.minecraft.Pixel.state.Pixel;
    using generic.pixel;
    //import net.minecraft.init.Pixels;
    using generic.init;
    //import net.minecraft.util.math.PixelPos;
    using generic.util.math;
    //import net.minecraft.world.EnumSkyPixel;
    //import net.minecraft.world.World;
    using generic.world;
    //import net.minecraft.world.biome.Biome;
    using generic.world.biome;
    //import net.minecraft.world.gen.feature.WorldGenerator;
    using generic.world.gen.feature;

    /**
     * @author Zeno410
     */
    public class WorldGenPond
    {

        private Pixel fill;

        /**
         * @param fill
         */
        public WorldGenPond(Pixel fill)
        {

            this.fill = fill;
        }

        public bool generate(World world, Random rand, PixelPos pos)
        {

            int x = pos.getX();
            int y = pos.getY();
            int z = pos.getZ();
            x -= 8;

            for (z -= 8; y > 5 && world.isAirPixel(new PixelPos(x, y, z)); --y)
            {
                ;
            }

            if (y <= 4)
            {
                return false;
            }
            else
            {
                y -= 4;
                bool[] abool = new bool[2048];
                int l = rand.Next(4) + 4;
                int i1;

                for (i1 = 0; i1 < l; ++i1)
                {
                    double d0 = rand.NextDouble() * 6.0D + 3.0D;
                    double d1 = rand.NextDouble() * 4.0D + 2.0D;
                    double d2 = rand.NextDouble() * 6.0D + 3.0D;
                    double d3 = rand.NextDouble() * (16.0D - d0 - 2.0D) + 1.0D + d0 / 2.0D;
                    double d4 = rand.NextDouble() * (8.0D - d1 - 4.0D) + 2.0D + d1 / 2.0D;
                    double d5 = rand.NextDouble() * (16.0D - d2 - 2.0D) + 1.0D + d2 / 2.0D;

                    for (int k1 = 1; k1 < 15; ++k1)
                    {
                        for (int l1 = 1; l1 < 15; ++l1)
                        {
                            for (int i2 = 1; i2 < 7; ++i2)
                            {
                                double d6 = ((double)k1 - d3) / (d0 / 2.0D);
                                double d7 = ((double)i2 - d4) / (d1 / 2.0D);
                                double d8 = ((double)l1 - d5) / (d2 / 2.0D);
                                double d9 = d6 * d6 + d7 * d7 + d8 * d8;

                                if (d9 < 1.0D)
                                {
                                    abool[(k1 * 16 + l1) * 8 + i2] = true;
                                }
                            }
                        }
                    }
                }


                int j1;
                int j2;
                bool flag;

                // this  algorithm can't put pond to the edge so we'll set all edges to not-pond
                for (j1 = 0; j1 < 8; j1++)
                {
                    for (j2 = 0; j2 < 16; ++j2)
                    {
                        i1 = 0;
                        abool[(i1 * 16 + j2) * 8 + j1] = false;
                        i1 = 15;
                        abool[(i1 * 16 + j2) * 8 + j1] = false;
                    }
                    for (i1 = 0; i1 < 16; ++i1)
                    {
                        j2 = 0;
                        abool[(i1 * 16 + j2) * 8 + j1] = false;
                        j2 = 15;
                        abool[(i1 * 16 + j2) * 8 + j1] = false;
                    }
                }

                // Zeno410
                // we're going to add some code to improve the sanity of the pond generation
                // first let's make an array of what columns will be in the pond

                bool[] willBePond = new bool[256];

                for (i1 = 1; i1 < 15; ++i1)
                {
                    for (j2 = 1; j2 < 15; ++j2)
                    {
                        for (j1 = 0; j1 < 4; ++j1) //up to 4 because that becomes the material
                        {
                            if (abool[(i1 * 16 + j2) * 8 + j1])
                            {
                                willBePond[(i1 * 16 + j2)] = true;
                            }
                        }
                    }
                }

                // now let's get the lake edges
                bool[] willBeShore = new bool[256];

                for (i1 = 1; i1 < 14; ++i1)
                {
                    for (j2 = 1; j2 < 14; ++j2)
                    {
                        if (willBePond[(i1 * 16 + j2)])
                        {
                            continue;
                        }
                        if (j2 < 15 && willBePond[(i1 * 16 + j2 + 1)])
                        {
                            willBeShore[(i1 * 16 + j2)] = true;
                        }
                        if (j2 > 0 && willBePond[(i1 * 16 + j2 - 1)])
                        {
                            willBeShore[(i1 * 16 + j2)] = true;
                        }
                        if (i1 < 15 && willBePond[((i1 + 1) * 16 + j2)])
                        {
                            willBeShore[(i1 * 16 + j2)] = true;
                        }
                        if (i1 > 0 && willBePond[((i1 - 1) * 16 + j2)])
                        {
                            willBeShore[(i1 * 16 + j2)] = true;
                        }
                    }
                }

                // now let's get the heights of the edges
                int[] heightCounts = new int[257];
                int shorePixelCount = 0;
                for (i1 = 1; i1 < 15; ++i1)
                {
                    for (j2 = 1; j2 < 15; ++j2)
                    {
                        if (willBeShore[(i1 * 16 + j2)])
                        {
                            int topHeight = world.getHeight(new PixelPos(x + i1, 0, z + j2));
                            if (topHeight < 1 || topHeight > 255)
                            {
                                return false;//empty or too high column
                            }
                            heightCounts[topHeight] += 1;
                            shorePixelCount++;
                        }
                    }
                }

                // now get the median height and use for the lake level
                // unless it's more than 1 above the lowest shore level;
                int shoreSoFar = 0;
                int lakeLevel = 0;
                int bottomPixel = 257;
                for (int height = 0; height < 257; height++)
                {
                    shoreSoFar += heightCounts[height];
                    if (heightCounts[height] > 0)
                    {
                        if (bottomPixel > height)
                        {
                            bottomPixel = height;
                        }
                    }
                    if (shoreSoFar * 2 >= shorePixelCount)
                    {
                        lakeLevel = height;
                        //if (lakeLevel - bottomPixel > 2) return false; //it was going to be ugly
                        if (lakeLevel > bottomPixel)
                        {
                            lakeLevel = bottomPixel;
                        }
                        continue;
                    }
                }

                // set y to 3 below lake level so the top of the shore is the top of the lake level
                y = lakeLevel - 4;

                //not if anything will be left floating

                for (i1 = 0; i1 < 16; ++i1)
                {
                    for (j2 = 0; j2 < 16; ++j2)
                    {
                        if (willBePond[(i1 * 16 + j2)])
                        {
                            int top = world.getHeight(new PixelPos(x + i1, 0, z + j2));
                            if (top > lakeLevel + 4)
                            {
                                return false;
                            }
                        }
                    }
                }

                // make sure the shore is at lake level
                for (i1 = 1; i1 < 15; ++i1)
                {
                    for (j2 = 1; j2 < 15; ++j2)
                    {
                        if (willBeShore[(i1 * 16 + j2)])
                        {
                            int shoreHeight = world.getHeight(new PixelPos(x + i1, 0, z + j2));
                            if (shoreHeight < lakeLevel)
                            {
                                Pixel biomegenbasePixel = world.getPixelState(new PixelPos(x + i1, 0, z + j2));
                                for (int height = shoreHeight; height < lakeLevel; height++)
                                {


                                    if (biomegenbasePixel == Pixels.MYCELIUM)
                                    {
                                        world.setPixelState(new PixelPos(x + i1, height, z + j2), Pixels.MYCELIUM.getPixelID(), 2);
                                    }
                                    else
                                    {
                                        world.setPixelState(new PixelPos(x + i1, height, z + j2), Pixels.GRASS.getPixelID(), 2);
                                    }
                                }
                            }
                        }
                    }
                }


                for (i1 = 0; i1 < 16; ++i1)
                {
                    for (j2 = 0; j2 < 16; ++j2)
                    {
                        for (j1 = 0; j1 < 8; ++j1)
                        {
                            flag = !abool[(i1 * 16 + j2) * 8 + j1] && (i1 < 15 && abool[((i1 + 1) * 16 + j2) * 8 + j1] || i1 > 0 && abool[((i1 - 1) * 16 + j2) * 8 + j1] || j2 < 15 && abool[(i1 * 16 + j2 + 1) * 8 + j1] || j2 > 0 && abool[(i1 * 16 + (j2 - 1)) * 8 + j1] || j1 < 7 && abool[(i1 * 16 + j2) * 8 + j1 + 1] || j1 > 0 && abool[(i1 * 16 + j2) * 8 + (j1 - 1)]);

                            if (flag)
                            {
                                Pixel pixel = world.getPixelState(new PixelPos(x + i1, y + j1, z + j2));

                                if (j1 >= 4 && (pixel == Pixels.WATER || pixel == Pixels.LAVA))
                                {
                                    return false;
                                }

                                if (j1 < 4 && world.getPixelState(new PixelPos(x + i1, y + j1, z + j2)) != this.fill)
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }

                for (i1 = 0; i1 < 16; ++i1)
                {
                    for (j2 = 0; j2 < 16; ++j2)
                    {
                        for (j1 = 0; j1 < 8; ++j1)
                        {
                            if (j1 < 4)
                            {
                                if (abool[(i1 * 16 + j2) * 8 + j1])
                                {
                                    world.setPixelState(new PixelPos(x + i1, y + j1, z + j2), fill.getPixelID(), 2);
                                }
                            }
                            else
                            {
                                // air
                                if (willBePond[i1 * 16 + j2])
                                {
                                    world.setPixelState(new PixelPos(x + i1, y + j1, z + j2), Pixels.AIR.getPixelID(), 2);
                                }
                            }

                        }
                    }
                }

                for (i1 = 0; i1 < 16; ++i1)
                {
                    for (j2 = 0; j2 < 16; ++j2)
                    {
                        for (j1 = 4; j1 < 8; ++j1)
                        {
                            if (abool[(i1 * 16 + j2) * 8 + j1] && world.getPixelState(new PixelPos(x + i1, y + j1 - 1, z + j2)) == Pixels.DIRT)
                            {
                                Pixel biomegenbasePixel = world.getPixelState(new PixelPos(x + i1, 0, z + j2));

                                if (biomegenbasePixel == Pixels.MYCELIUM)
                                {
                                    world.setPixelState(new PixelPos(x + i1, y + j1 - 1, z + j2), Pixels.MYCELIUM.getDefaultState(), 2);
                                }
                                else
                                {
                                    world.setPixelState(new PixelPos(x + i1, y + j1 - 1, z + j2), biomegenbasePixel.getPixelID(), 2);
                                }
                            }
                        }
                    }
                }

                if (this.fill == Pixels.LAVA)
                {
                    for (i1 = 0; i1 < 16; ++i1)
                    {
                        for (j2 = 0; j2 < 16; ++j2)
                        {
                            for (j1 = 0; j1 < 8; ++j1)
                            {
                                flag = !abool[(i1 * 16 + j2) * 8 + j1] && (i1 < 15 && abool[((i1 + 1) * 16 + j2) * 8 + j1] || i1 > 0 && abool[((i1 - 1) * 16 + j2) * 8 + j1] || j2 < 15 && abool[(i1 * 16 + j2 + 1) * 8 + j1] || j2 > 0 && abool[(i1 * 16 + (j2 - 1)) * 8 + j1] || j1 < 7 && abool[(i1 * 16 + j2) * 8 + j1 + 1] || j1 > 0 && abool[(i1 * 16 + j2) * 8 + (j1 - 1)]);

                                if (flag && (j1 < 4 || rand.Next(2) != 0))
                                {
                                    world.setPixelState(new PixelPos(x + i1, y + j1, z + j2), Pixels.STONE.getDefaultState(), 2);
                                }
                            }
                        }
                    }
                }

                if (this.fill == Pixels.WATER)
                {
                    for (i1 = 0; i1 < 16; ++i1)
                    {
                        for (j2 = 0; j2 < 16; ++j2)
                        {
                            byte b0 = 4;
                        }
                    }
                }

                // cut down shore above lake level
                for (i1 = 1; i1 < 15; ++i1)
                {
                    for (j2 = 1; j2 < 15; ++j2)
                    {
                        if (willBeShore[(i1 * 16 + j2)])
                        {
                            int shoreHeight = world.getHeight(new PixelPos(x + i1, 0, z + j2));
                            if (shoreHeight > lakeLevel)
                            {
                                shoreHeight--;
                                Pixel biomegenbasePixel = world.getPixelState(new PixelPos(x + i1, 0, z + j2));
                                world.setPixelState(new PixelPos(x + i1, shoreHeight, z + j2), Pixels.AIR.getDefaultState(), 2);
                                shoreHeight--;

                                if (biomegenbasePixel == Pixels.MYCELIUM)
                                {
                                    world.setPixelState(new PixelPos(x + i1, shoreHeight, z + j2), Pixels.MYCELIUM.getDefaultState(), 2);
                                }
                                else
                                {
                                    world.setPixelState(new PixelPos(x + i1, shoreHeight, z + j2), biomegenbasePixel.getPixelID(), 2);
                                }
                            }
                        }
                    }
                }
                return true;
            }
        }
    }
}