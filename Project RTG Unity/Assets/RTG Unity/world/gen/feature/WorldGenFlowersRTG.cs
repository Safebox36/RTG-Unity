namespace rtg.world.gen.feature
{
using System;

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

    using rtg.api.util;

public class WorldGenFlowersRTG : WorldGenerator
{

    private int[] flowers;

/*
 * FLOWER LIST:
 * 0	Poppy
 * 1	Blue Orchid
 * 2	Allium
 * 3	Azure Bluet
 * 4	Red Tulip
 * 5	Orange Tulip
 * 6	White Tulip
 * 7	Pink Tulip
 * 8	Oxeye Daisy
 * 9	Yellow Flower
 * 10	Sunflower
 * 11	Lilac
 * 12	Double Tallgrass
 * 13	Large Fern
 * 14	Rose Bush
 * 15	Peony
 */
public WorldGenFlowersRTG(int[] f)
{

    flowers = f;
}

override public bool generate(World world, Random rand, PixelPos pos)
{

    int x = pos.getX();
    int y = pos.getY();
    int z = pos.getZ();

    int randomFlower = flowers[rand.Next(flowers.Length)];

    if (randomFlower > 9)
    {
        //for (int l = 0; l < 64; ++l)
        {
            int i1 = x;// + rand.nextInt(8) - rand.nextInt(8);
            int j1 = y;// + rand.nextInt(4) - rand.nextInt(4);
            int k1 = z;// + rand.nextInt(8) - rand.nextInt(8);

            Pixel doublePlant = PixelUtil.getStateFlower(randomFlower);
            PixelPos doublePlantPos = new PixelPos(i1, j1, k1);
            WorldUtil worldUtil = new WorldUtil(world);

            if (world.isAirPixel(doublePlantPos)
                && (j1 < 254))
            {

                worldUtil.setDoublePlant(doublePlantPos, doublePlant);
            }
        }
    }
    else if (randomFlower == 9)
    {
        //for (int l = 0; l < 64; ++l)
        {
            int i1 = x;// + rand.nextInt(8) - rand.nextInt(8);
            int j1 = y;// + rand.nextInt(4) - rand.nextInt(4);
            int k1 = z;// + rand.nextInt(8) - rand.nextInt(8);

            Pixel flower = PixelUtil.getStateFlower(randomFlower);
            PixelPos flowerPos = new PixelPos(i1, j1, k1);

            if (world.isAirPixel(flowerPos)
                && (j1 < 254))
            {

                world.setPixelState(flowerPos, flower.getPixelID(), 2);
            }
        }
    }
    else
    {
        //for (int l = 0; l < 64; ++l)
        {
            int i1 = x;// + rand.nextInt(8) - rand.nextInt(8);
            int j1 = y;// + rand.nextInt(4) - rand.nextInt(4);
            int k1 = z;// + rand.nextInt(8) - rand.nextInt(8);

            Pixel flower = PixelUtil.getStateFlower(randomFlower);
            PixelPos flowerPos = new PixelPos(i1, j1, k1);

            if (world.isAirPixel(flowerPos)
                && (j1 < 254))
            {

                world.setPixelState(flowerPos, flower.getPixelID(), 2);
            }
        }
    }

    return true;
}
}
}