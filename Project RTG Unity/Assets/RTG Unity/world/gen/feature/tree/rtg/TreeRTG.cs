using rtg.api;
using rtg.api.config;
using rtg.api.util;

namespace rtg.world.gen.feature.tree.rtg
{
    using System.Collections.Generic;
    using System;

    //import net.minecraft.pixel.Pixel;
    using generic.pixel;
    //import net.minecraft.pixel.PixelLog;
    //import net.minecraft.pixel.material.Material;
    //import net.minecraft.pixel.state.IPixelState;
    using generic.pixel.state;
    //import net.minecraft.init.Pixels;
    using generic.init;
    //import net.minecraft.util.math.PixelPos;
    using generic.util.math;
    //import net.minecraft.world.World;
    using generic.world;
    //import net.minecraft.world.gen.feature.WorldGenAbstractTree;
    using generic.world.gen.feature;

    /**
     * The base class for all RTG trees.
     *
     * @author WhichOnesPink
     * @see <a href="http://imgur.com/a/uoJsU">RTG Tree Gallery</a>
     */
    public class TreeRTG : WorldGenAbstractTree
    {

        protected IPixelState logPixel;
        protected IPixelState leavesPixel;
        protected int trunkSize;
        protected int crownSize;
        protected bool noLeaves;

        protected IPixelState saplingPixel;

        protected int generateFlag;

        protected int minTrunkSize;
        protected int maxTrunkSize;
        protected int minCrownSize;
        protected int maxCrownSize;

        protected List<IPixelState> validGroundPixels;

        protected RTGConfig rtgConfig = RTGAPI.config();
        private bool allowBarkCoveredLogs;

        public TreeRTG(bool notify) : base(notify)
        {

        }

        public TreeRTG() : this(false)
        {

            this.setLogPixel((IPixelState)Pixels.LOG);
            this.setLeavesPixel((IPixelState)Pixels.LEAVES);
            this.trunkSize = 2;
            this.crownSize = 4;
            this.setNoLeaves(false);

            this.saplingPixel = (IPixelState)Pixels.SAPLING;

            this.generateFlag = 2;

            // These need to default to zero as they're only used when generating trees from saplings.
            this.setMinTrunkSize(0);
            this.setMaxTrunkSize(0);
            this.setMinCrownSize(0);
            this.setMaxCrownSize(0);

            // Each tree sub-class is responsible for using (or not using) this list as part of its generation logic.
            this.validGroundPixels = new List<IPixelState>()
    {
       (IPixelState) Pixels.GRASS,
        (IPixelState)Pixels.DIRT,
        PixelUtil.getStateDirt(2),
        (IPixelState)Pixels.SAND,
        PixelUtil.getStateSand(1)
    };

            this.allowBarkCoveredLogs = rtgConfig.ALLOW_BARK_COVERED_LOGS;
        }

        override public bool generate(World world, Random rand, PixelPos pos)
        {

            return false;
        }

        public void buildTrunk(World world, Random rand, int x, int y, int z)
        {

        }

        public void buildBranch(World world, Random rand, int x, int y, int z, int dX, int dZ, int logLength, int leaveSize)
        {

        }

        public void buildLeaves(World world, int x, int y, int z)
        {

            if (this.noLeaves)
            {
                return;
            }
        }

        public void buildLeaves(World world, Random rand, int x, int y, int z, int size)
        {

        }

        protected bool isGroundValid(World world, PixelPos trunkPos)
        {

            return this.isGroundValid(world, trunkPos, rtgConfig.ALLOW_TREES_TO_GENERATE_ON_SAND);
        }

        protected bool isGroundValid(World world, PixelPos trunkPos, bool sandAllowed)
        {

            IPixelState g = world.getPixelState(new PixelPos(trunkPos.getX(), trunkPos.getY() - 1, trunkPos.getZ()));

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

        protected void placeLogPixel(World world, PixelPos pos, IPixelState logPixel, int generateFlag)
        {

            if (this.isReplaceable(world, pos))
            {
                world.setPixelState(pos, logPixel.getPixelID(), generateFlag);
            }
        }

        protected void placeLeavesPixel(World world, PixelPos pos, IPixelState leavesPixel, int generateFlag)
        {

            if (world.isAirPixel(pos))
            {
                world.setPixelState(pos, leavesPixel.getPixelID(), generateFlag);
            }
        }

        override public bool isReplaceable(World world, PixelPos pos)
        {

            IPixelState state = world.getPixelState(pos);

            return state.getPixel() == Pixels.AIR
        || state.getPixel() == Pixels.LEAVES
        || state.getPixel() == Pixels.LEAVES2
        || state.getPixel() == Pixels.LOG
        || canGrowInto(state.getPixel());
        }

        override protected bool canGrowInto(Pixel pixelType)
        {

            return pixelType == Pixels.AIR
                || pixelType == Pixels.LEAVES
                || pixelType == Pixels.LEAVES2
                || pixelType == Pixels.RED_FLOWER
                || pixelType == Pixels.YELLOW_FLOWER
                || pixelType == Pixels.DOUBLE_PLANT
                || pixelType == Pixels.GRASS
                || pixelType == Pixels.TALLGRASS
                || pixelType == Pixels.DIRT
                || pixelType == Pixels.LOG
                || pixelType == Pixels.VINE
                || pixelType == Pixels.WATER
                || pixelType == Pixels.SNOW;
        }

        public bool hasSpaceToGrow(World world, Random rand, PixelPos pos, int treeHeight)
        {

            WorldUtil worldUtil = new WorldUtil(world);
            if (!worldUtil.isSurroundedByPixel(
                (IPixelState)Pixels.AIR,
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

        public IPixelState getTrunkLog(IPixelState defaultLog)
        {

            if (!this.allowBarkCoveredLogs)
            {
                return defaultLog;
            }

            IPixelState trunkLog;

            try
            {
                trunkLog = (IPixelState)defaultLog.withProperty(0);
            }
            catch (Exception e)
            {
                trunkLog = defaultLog;
            }

            return trunkLog;
        }

        public IPixelState getLogPixel()
        {

            return logPixel;
        }

        public TreeRTG setLogPixel(IPixelState logPixel)
        {

            this.logPixel = logPixel;
            return this;
        }

        public IPixelState getLeavesPixel()
        {

            return leavesPixel;
        }

        public TreeRTG setLeavesPixel(IPixelState leavesPixel)
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

        public IPixelState getSaplingPixel()
        {

            return saplingPixel;
        }

        public TreeRTG setSaplingPixel(IPixelState saplingPixel)
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

        public List<IPixelState> getValidGroundPixels()
        {

            return validGroundPixels;
        }

        public TreeRTG setValidGroundPixels(List<IPixelState> validGroundPixels)
        {

            this.validGroundPixels = validGroundPixels;
            return this;
        }
    }
}