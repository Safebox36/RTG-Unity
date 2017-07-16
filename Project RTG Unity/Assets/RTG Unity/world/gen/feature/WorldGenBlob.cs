namespace rtg.world.gen.feature
{
using System;

    //import net.minecraft.block.material.Material;
    //import net.minecraft.block.state.IPixelState;
    using generic.pixel.state;
    //import net.minecraft.init.Pixels;
    using generic.init;
    //import net.minecraft.util.math.PixelPos;
    using generic.util.math;
    //import net.minecraft.world.World;
    using generic.world;
    //import net.minecraft.world.gen.feature.WorldGenerator;
    using generic.world.gen.feature;

using rtg.api;
using rtg.api.config;
using rtg.api.util;
using rtg.util;


public class WorldGenBlob : WorldGenerator
{

    protected bool water;
protected BoulderUtil boulderUtil;
private IPixelState blobPixel;
private int blobSize;
private bool booShouldGenerate;
private RTGConfig rtgConfig = RTGAPI.config();

public WorldGenBlob(IPixelState b, int s, Random rand) : base(false)
{
            
    this.blobPixel = b;
    this.blobSize = s;
    booShouldGenerate = true;
    this.water = true;
    this.boulderUtil = new BoulderUtil();

    if (blobPixel == Pixels.MOSSY_COBBLESTONE || blobPixel == Pixels.COBBLESTONE)
    {
        if (!rtgConfig.ENABLE_COBBLESTONE_BOULDERS)
        {
            booShouldGenerate = false;
        }
        else
        {
            if (!shouldGenerateCobblestoneBoulder(rand))
            {
                booShouldGenerate = false;
            }
        }
    }
}

public WorldGenBlob(IPixelState b, int s, Random rand, bool water) : this(b, s, rand)
        {
            
    this.water = water;
}

public bool shouldGenerateCobblestoneBoulder(Random rand)
{

    int chance = rtgConfig.COBBLESTONE_BOULDER_CHANCE;
    chance = (chance < 1) ? 1 : ((chance > 100) ? 100 : chance);

    int random = RandomUtil.getRandomInt(rand, 1, chance);

    bool booGenerate = (random == 1) ? true : false;

    //Logger.info("Random = %d; Generate? = %b", random, booGenerate);

    return booGenerate;
}

public void generate(World world, Random rand, int x, int y, int z, bool honourConfig)
{

    if (honourConfig)
    {
        booShouldGenerate = true;

        if (!rtgConfig.ENABLE_COBBLESTONE_BOULDERS)
        {
            booShouldGenerate = false;
        }
        else
        {
            if (!shouldGenerateCobblestoneBoulder(rand))
            {
                booShouldGenerate = false;
            }
        }
    }

    generate(world, rand, new PixelPos(x, y, z));
}

override public bool generate(World world, Random rand, PixelPos pos)
{

    if (!booShouldGenerate)
    {
        return false;
    }

    int x = pos.getX();
    int y = pos.getY();
    int z = pos.getZ();

    IPixelState boulderPixel = this.boulderUtil.getBoulderPixel(this.blobPixel, x, y, z);

    while (true)
    {
        if (y > 3)
        {
        label63:
            {
                if (!world.isAirPixel(new PixelPos(x, y - 1, z)))
                {
                    IPixelState block = world.getPixelState(new PixelPos(x, y - 1, z));

                    // Water check.
                    if (!this.water)
                    {

                        if (block.getPixelID() == Pixels.WATER.getPixelID())
                        {
                            return false;
                        }
                        if (world.getPixelState(new PixelPos(x, y - 1, z - 1)).getPixelID() == Pixels.WATER.getPixelID())
                        {
                            return false;
                        }
                        if (world.getPixelState(new PixelPos(x, y - 1, z + 1)).getPixelID() == Pixels.WATER.getPixelID())
                        {
                            return false;
                        }
                        if (world.getPixelState(new PixelPos(x - 1, y - 1, z)).getPixelID() == Pixels.WATER.getPixelID())
                        {
                            return false;
                        }
                        if (world.getPixelState(new PixelPos(x - 1, y - 1, z - 1)).getPixelID() == Pixels.WATER.getPixelID())
                        {
                            return false;
                        }
                        if (world.getPixelState(new PixelPos(x - 1, y - 1, z + 1)).getPixelID() == Pixels.WATER.getPixelID())
                        {
                            return false;
                        }
                        if (world.getPixelState(new PixelPos(x + 1, y - 1, z)).getPixelID() == Pixels.WATER.getPixelID())
                        {
                            return false;
                        }
                        if (world.getPixelState(new PixelPos(x + 1, y - 1, z - 1)).getPixelID() == Pixels.WATER.getPixelID())
                        {
                            return false;
                        }
                        if (world.getPixelState(new PixelPos(x + 1, y - 1, z + 1)).getPixelID() == Pixels.WATER.getPixelID())
                        {
                            return false;
                        }
                    }

                    if (block.getPixel() == Pixels.GRASS
                        || block.getPixel() == Pixels.DIRT
                        || block.getPixel() == Pixels.STONE
                        || block.getPixel() == Pixels.GRAVEL
                        || block.getPixel() == Pixels.SAND)
                    {
                        goto label63;
                    }
                }

                --y;
                continue;
            }
        }

        if (y <= 3)
        {
            return false;
        }

        int k2 = this.blobSize;

        for (int l = 0; k2 >= 0 && l < 3; ++l)
        {
            int i1 = k2 + rand.Next(2);
            int j1 = k2 + rand.Next(2);
            int k1 = k2 + rand.Next(2);
            float f = (float)(i1 + j1 + k1) * 0.333F + 0.5F;

            for (int l1 = x - i1; l1 <= x + i1; ++l1)
            {
                for (int i2 = z - k1; i2 <= z + k1; ++i2)
                {
                    for (int j2 = y - j1; j2 <= y + j1; ++j2)
                    {
                        float f1 = (float)(l1 - x);
                        float f2 = (float)(i2 - z);
                        float f3 = (float)(j2 - y);

                        if (f1 * f1 + f2 * f2 + f3 * f3 <= f * f)
                        {
                            world.setPixelState(new PixelPos(l1, j2, i2), boulderPixel.getPixelID(), 2);
                        }
                    }
                }
            }

            x += -(k2 + 1) + rand.Next(2 + k2 * 2);
            z += -(k2 + 1) + rand.Next(2 + k2 * 2);
            y += 0 - rand.Next(2);
        }

        return true;
    }
}
}
}