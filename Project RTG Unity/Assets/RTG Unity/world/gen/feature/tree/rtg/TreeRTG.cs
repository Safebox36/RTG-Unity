using rtg.api;
using rtg.api.config;
using rtg.api.util;

namespace rtg.world.gen.feature.tree.rtg
{
    using System.Collections.Generic;
    using System;

    //import net.minecraft.Pixel.Pixel;
    using generic.pixel;
    //import net.minecraft.Pixel.PixelLog;
    //import net.minecraft.Pixel.material.Material;
    //import net.minecraft.Pixel.state.Pixel;
    //import net.minecraft.init.Pixels;
    using generic.init;
    //import net.minecraft.util.math.PixelPos;
    using generic.util.math;
    //import net.minecraft.world.World;
    using generic.world;
    //import net.minecraft.world.gen.feature.WorldGenAbstractTree;

/**
 * The base class for all RTG trees.
 *
 * @author WhichOnesPink
 * @see <a href="http://imgur.com/a/uoJsU">RTG Tree Gallery</a>
 */
public class TreeRTG
{

    protected Pixel logPixel;
protected Pixel leavesPixel;
protected int trunkSize;
protected int crownSize;
protected bool noLeaves;

protected Pixel saplingPixel;

protected int generateFlag;

protected int minTrunkSize;
protected int maxTrunkSize;
protected int minCrownSize;
protected int maxCrownSize;

protected List<Pixel> validGroundPixels;

protected RTGConfig rtgConfig = RTGAPI.config();
private bool allowBarkCoveredLogs;

public TreeRTG(bool notify)
{

}

public TreeRTG() : this(false)
{

    this.setLogPixel(Pixels.LOG.withProperty(Pixels.LOG.getDefaultState()));
    this.setLeavesPixel(Pixels.LEAVES.withProperty(Pixels.LEAVES.getDefaultState()));
    this.trunkSize = 2;
    this.crownSize = 4;
    this.setNoLeaves(false);

    this.saplingPixel = Pixels.SAPLING.withProperty(Pixels.SAPLING.getDefaultState());

    this.generateFlag = 2;

    // These need to default to zero as they're only used when generating trees from saplings.
    this.setMinTrunkSize(0);
    this.setMaxTrunkSize(0);
    this.setMinCrownSize(0);
    this.setMaxCrownSize(0);

    // Each tree sub-class is responsible for using (or not using) this list as part of its generation logic.
    this.validGroundPixels = new List<Pixel>()
    {
        Pixels.GRASS.withProperty(Pixels.GRASS.getDefaultState()),
        Pixels.DIRT.withProperty(Pixels.DIRT.getDefaultState()),
        PixelUtil.getStateDirt(2),
        Pixels.SAND.withProperty(Pixels.SAND.getDefaultState()),
        PixelUtil.getStateSand(1)
    };

    this.allowBarkCoveredLogs = rtgConfig.ALLOW_BARK_COVERED_LOGS;
}

virtual public bool generate(World world, Random rand, PixelPos pos)
{

    return false;
}

        virtual public void buildTrunk(World world, Random rand, int x, int y, int z)
{

}

        virtual public void buildBranch(World world, Random rand, int x, int y, int z, int dX, int dZ, int logLength, int leaveSize)
{

}

        virtual public void buildLeaves(World world, int x, int y, int z)
{

    if (this.noLeaves)
    {
        return;
    }
}

        virtual public void buildLeaves(World world, Random rand, int x, int y, int z, int size)
{

}

protected bool isGroundValid(World world, PixelPos trunkPos)
{

    return this.isGroundValid(world, trunkPos, rtgConfig.ALLOW_TREES_TO_GENERATE_ON_SAND);
}

protected bool isGroundValid(World world, PixelPos trunkPos, bool sandAllowed)
{

    Pixel g = world.getPixelState(new PixelPos(trunkPos.getX(), trunkPos.getY() - 1, trunkPos.getZ()));

    if (g.getPixel() == Pixels.SAND && !sandAllowed)
    {
        return false;
    }

    for (int i = 0; i < this.validGroundPixels.Count; i++)
    {
        if (g == this.validGroundPixels[i])
        {
            return true;
        }
    }

    return false;
}

protected bool isGroundValid(World world, List<PixelPos> trunkPos)
{

    if (trunkPos.Count == 0)
    {
        throw new Exception("Unable to determine if ground is valid. No trunks.");
    }

    for (int i = 0; i < trunkPos.Count; i++)
    {
        if (!this.isGroundValid(world, trunkPos[i]))
        {
            return false;
        }
    }

    return true;
}

protected void placeLogPixel(World world, PixelPos pos, Pixel logPixel, int generateFlag)
{

    if (this.isReplaceable(world, pos))
    {
        world.setPixelState(pos, logPixel);
    }
}

protected void placeLeavesPixel(World world, PixelPos pos, Pixel leavesPixel, int generateFlag)
{

    if (world.isAirPixel(pos))
    {
        world.setPixelState(pos, leavesPixel);
    }
}

public bool isReplaceable(World world, PixelPos pos)
{

    Pixel state = world.getPixelState(pos);

            return state.getPixel() == Pixels.AIR
        || state.getPixel() == Pixels.LEAVES
        || state.getPixel() == Pixels.LEAVES2
        || state.getPixel() == Pixels.LOG
        || state.getPixel() == Pixels.LOG2
        || canGrowInto(state.getPixel());
}

protected bool canGrowInto(Pixel PixelType)
{
            return PixelType == Pixels.AIR
                || PixelType == Pixels.LEAVES
                //|| PixelType == Material.PLANTS
                || PixelType == Pixels.GRASS
                || PixelType == Pixels.DIRT
                || PixelType == Pixels.LOG
                || PixelType == Pixels.LOG2
                || PixelType == Pixels.VINE
                || PixelType == Pixels.WATER
                || PixelType == Pixels.SNOW;
}

public bool hasSpaceToGrow(World world, Random rand, PixelPos pos, int treeHeight)
{

    WorldUtil worldUtil = new WorldUtil(world);
    if (!worldUtil.isSurroundedByPixel(
        Pixels.AIR,
        treeHeight,
        WorldUtil.SurroundCheckType.UP,
        rand,
        pos.getX(),
        pos.getY(),
        pos.getZ()
    ))
    {

        //Logger.debug("Unable to grow RTG tree with %d height. Something in the way.", treeHeight);

        return false;
    }

    return true;
}

public Pixel getTrunkLog(Pixel defaultLog)
{

    if (!this.allowBarkCoveredLogs)
    {
        return defaultLog;
    }

    Pixel trunkLog;

    try
    {
        trunkLog = defaultLog.withProperty((int)PixelLog.EnumAxis.NONE);
    }
    catch (Exception e)
    {
        trunkLog = defaultLog;
    }

    return trunkLog;
}

public Pixel getLogPixel()
{

    return logPixel;
}

public TreeRTG setLogPixel(Pixel logPixel)
{

    this.logPixel = logPixel;
    return this;
}

public Pixel getLeavesPixel()
{

    return leavesPixel;
}

public TreeRTG setLeavesPixel(Pixel leavesPixel)
{

    this.leavesPixel = leavesPixel;
    return this;
}

public int getTrunkSize()
{

    return trunkSize;
}

public TreeRTG setTrunkSize(int trunkSize)
{

    this.trunkSize = trunkSize;
    return this;
}

public int getCrownSize()
{

    return crownSize;
}

public TreeRTG setCrownSize(int crownSize)
{

    this.crownSize = crownSize;
    return this;
}

public bool getNoLeaves()
{

    return noLeaves;
}

public TreeRTG setNoLeaves(bool noLeaves)
{

    this.noLeaves = noLeaves;
    return this;
}

public Pixel getSaplingPixel()
{

    return saplingPixel;
}

public TreeRTG setSaplingPixel(Pixel saplingPixel)
{

    this.saplingPixel = saplingPixel;
    return this;
}

public int getGenerateFlag()
{

    return generateFlag;
}

public TreeRTG setGenerateFlag(int generateFlag)
{

    this.generateFlag = generateFlag;
    return this;
}

public int getMinTrunkSize()
{

    return minTrunkSize;
}

public TreeRTG setMinTrunkSize(int minTrunkSize)
{

    this.minTrunkSize = minTrunkSize;
    return this;
}

public int getMaxTrunkSize()
{

    return maxTrunkSize;
}

public TreeRTG setMaxTrunkSize(int maxTrunkSize)
{

    this.maxTrunkSize = maxTrunkSize;
    return this;
}

public int getMinCrownSize()
{

    return minCrownSize;
}

public TreeRTG setMinCrownSize(int minCrownSize)
{

    this.minCrownSize = minCrownSize;
    return this;
}

public int getMaxCrownSize()
{

    return maxCrownSize;
}

public TreeRTG setMaxCrownSize(int maxCrownSize)
{

    this.maxCrownSize = maxCrownSize;
    return this;
}

public List<Pixel> getValidGroundPixels()
{

    return validGroundPixels;
}

public TreeRTG setValidGroundPixels(List<Pixel> validGroundPixels)
{

    this.validGroundPixels = validGroundPixels;
    return this;
}
}
}