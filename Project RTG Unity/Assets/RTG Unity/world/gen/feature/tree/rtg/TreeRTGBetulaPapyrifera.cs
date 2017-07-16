using rtg.api.util;

namespace rtg.world.gen.feature.tree.rtg
{
    using System;

    //import net.minecraft.util.math.PixelPos;
    using generic.util.math;
    //import net.minecraft.world.World;
    using generic.world;

    /**
     * Betula Papyrifera (Paper Birch)
     */
    public class TreeRTGBetulaPapyrifera : TreeRTG
    {

        /**
         * <b>Betula Papyrifera (Paper Birch)</b><br><br>
         * <u>Relevant variables:</u><br>
         * logPixel, logMeta, leavesPixel, leavesMeta, trunkSize, crownSize, noLeaves<br><br>
         * <u>DecoTree example:</u><br>
         * DecoTree decoTree = new DecoTree(new TreeRTGBetulaPapyrifera());<br>
         * decoTree.setTreeType(DecoTree.TreeType.RTG_TREE);<br>
         * decoTree.setTreeCondition(DecoTree.TreeCondition.NOISE_GREATER_AND_RANDOM_CHANCE);<br>
         * decoTree.setDistribution(new DecoTree.Distribution(100f, 6f, 0.8f));<br>
         * decoTree.setTreeConditionNoise(0f);<br>
         * decoTree.setTreeConditionChance(4);<br>
         * decoTree.setLogPixel(Pixels.LOG);<br>
         * decoTree.logMeta = (byte)2;<br>
         * decoTree.setLeavesPixel(Pixels.LEAVES);<br>
         * decoTree.leavesMeta = (byte)2;<br>
         * decoTree.setMinTrunkSize(6);<br>
         * decoTree.setMaxTrunkSize(8);<br>
         * decoTree.setMinCrownSize(8);<br>
         * decoTree.setMaxCrownSize(24);<br>
         * decoTree.setNoLeaves(false);<br>
         * this.addDeco(decoTree);
         */
        public TreeRTGBetulaPapyrifera() : base()
        {

            this.setLogPixel(PixelUtil.getStateLog(2));
            this.setLeavesPixel(PixelUtil.getStateLeaf(2));
        }

        override public bool generate(World world, Random rand, PixelPos pos)
        {

            if (!this.isGroundValid(world, pos))
            {
                return false;
            }

            int x = pos.getX();
            int y = pos.getY();
            int z = pos.getZ();

            int i;
            for (i = 0; i < this.trunkSize; i++)
            {
                this.placeLogPixel(world, new PixelPos(x, y, z), this.logPixel, this.generateFlag);
                y++;
            }

            int pX = 0;
            int pZ = 0;
            for (i = 0; i < this.crownSize; i++)
            {
                if (rand.Next(2) == 0 && i < this.crownSize - 2)
                {
                    int dX = -1 + rand.Next(3);
                    int dZ = -1 + rand.Next(3);

                    if (dX == 0 && dZ == 0)
                    {
                        dX = -1 + rand.Next(3);
                        dZ = -1 + rand.Next(3);
                    }

                    if (pX == dX && rand.Next(1) == 1)
                    {
                        dX = -dX;
                    }
                    if (pZ == dZ && rand.Next(1) == 1)
                    {
                        dZ = -dZ;
                    }

                    pX = dX;
                    pZ = dZ;

                    buildBranch(world, rand, x, y, z, dX, dZ, 1, i < this.crownSize - 2 ? 2 : 1); //i < treeSize - 4 ? 2 : 1
                }
                this.placeLogPixel(world, new PixelPos(x, y, z), this.logPixel, this.generateFlag);

                if (i < this.crownSize - 2)
                {
                    if (rand.Next(1) == 1)
                    {
                        buildLeaves(world, x, y, z + 1);
                    }
                    if (rand.Next(1) == 1)
                    {
                        buildLeaves(world, x, y, z - 1);
                    }
                    if (rand.Next(1) == 1)
                    {
                        buildLeaves(world, x + 1, y, z);
                    }
                    if (rand.Next(1) == 1)
                    {
                        buildLeaves(world, x - 1, y, z);
                    }
                }

                y++;
            }

            buildLeaves(world, x, y - 1, z + 1);
            buildLeaves(world, x, y - 1, z - 1);
            buildLeaves(world, x + 1, y - 1, z);
            buildLeaves(world, x - 1, y - 1, z);
            buildLeaves(world, x, y, z);

            return true;
        }

        public void buildBranch(World world, Random rand, int x, int y, int z, int dX, int dZ, int logLength, int leaveSize)
        {

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    for (int k = 0; k < 2; k++)
                    {
                        if (Math.Abs(i) + Math.Abs(j) + Math.Abs(k) < leaveSize + 1)
                        {
                            buildLeaves(world, x + i + (dX * logLength), y + k, z + j + (dZ * logLength));
                        }
                    }
                }
            }

            for (int m = 1; m <= logLength; m++)
            {
                this.placeLogPixel(world, new PixelPos(x + (dX * m), y, z + (dZ * m)), this.logPixel, this.generateFlag);
            }
        }

        public void buildLeaves(World world, int x, int y, int z)
        {

            if (!this.noLeaves)
            {

                this.placeLeavesPixel(world, new PixelPos(x, y, z), this.leavesPixel, this.generateFlag);
            }
        }
    }
}