namespace rtg.world.gen.feature.tree.rtg
{
    using System;

    using generic.pixel;
    using generic.util.math;
    using generic.world;


    /**
     * Rhizophora Mucronata (Asiatic Mangrove)
     */
    public class TreeRTGRhizophoraMucronata : TreeRTG
    {

        protected int minBranches;
        protected int maxBranches;
        protected float branchLength;
        protected float verStart;
        protected float verRand;
        protected Pixel trunkLog;

        /**
         * <b>Rhizophora Mucronata (Asiatic Mangrove)</b><br><br>
         * <u>Relevant variables:</u><br>
         * logPixel, logMeta, leavesPixel, leavesMeta, trunkSize, crownSize, noLeaves<br>
         * minBranches, maxBranches, branchLength, verStart, verRand<br><br>
         * <u>DecoTree example:</u><br>
         * DecoTree decoTree = new DecoTree(new TreeRTGRhizophoraMucronata(3, 4, 13f, 0.32f, 0.1f));<br>
         * decoTree.setTreeType(DecoTree.TreeType.RTG_TREE);<br>
         * decoTree.setTreeCondition(DecoTree.TreeCondition.NOISE_GREATER_AND_RANDOM_CHANCE);<br>
         * decoTree.setDistribution(new DecoTree.Distribution(100f, 6f, 0.8f));<br>
         * decoTree.setTreeConditionNoise(0f);<br>
         * decoTree.setTreeConditionChance(4);<br>
         * decoTree.setLogPixel(Pixels.LOG);<br>
         * decoTree.logMeta = (byte)3;<br>
         * decoTree.setLeavesPixel(Pixels.LEAVES);<br>
         * decoTree.leavesMeta = (byte)3;<br>
         * decoTree.setMinTrunkSize(3);<br>
         * decoTree.setMaxTrunkSize(4);<br>
         * decoTree.setMinCrownSize(10);<br>
         * decoTree.setMaxCrownSize(27);<br>
         * decoTree.setNoLeaves(false);<br>
         * this.addDeco(decoTree);
         */
        public TreeRTGRhizophoraMucronata(int minBranches, int maxBranches, float branchLength, float verStart, float verRand) : this()
        {

            this.minBranches = minBranches;
            this.maxBranches = maxBranches;
            this.branchLength = branchLength;
            this.verStart = verStart;
            this.verRand = verRand;
        }

        public TreeRTGRhizophoraMucronata() : base()
        {

            this.minBranches = 3;
            this.maxBranches = 4;
            this.branchLength = 13f;
            this.verStart = 0.32f;
            this.verRand = 0.1f;
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

            this.trunkLog = this.getTrunkLog(this.logPixel);

            int branch = this.minBranches + rand.Next(this.maxBranches - this.minBranches + 1);

            if (this.trunkSize > 0)
            {
                for (int k = 0; k < 3; k++)
                {
                    generateBranch(world, rand, x, y + this.trunkSize, z, (120 * k) - 40 + rand.Next(80), 1.6f + (float)rand.NextDouble() * 0.1f, this.trunkSize * 2f, 1f, true);
                }
            }

            for (int i = y + this.trunkSize; i < y + this.crownSize; i++)
            {
                this.placeLogPixel(world, new PixelPos(x, i, z), this.logPixel, this.generateFlag);
            }

            float horDir, verDir;
            int eX, eY, eZ;
            for (int j = 0; j < branch; j++)
            {
                horDir = (120 * j) - 60 + rand.Next(120);
                verDir = verStart + (float)rand.NextDouble() * verRand;
                generateBranch(world, rand, x, y + this.crownSize, z, horDir, verDir, branchLength, 1f, false);

                eX = x + (int)(Math.Cos(horDir * Math.PI / 180D) * verDir * branchLength);
                eZ = z + (int)(Math.Sin(horDir * Math.PI / 180D) * verDir * branchLength);
                eY = y + this.crownSize + (int)((1f - verDir) * branchLength);

                for (int m = 0; m < 1; m++)
                {
                    generateLeaves(world, rand, eX, eY, eZ, 4f, 1.2f);
                }
            }

            return true;
        }

        /*
         * horDir = number between -180D and 180D
         * verDir = number between 1F (horizontal) and 0F (vertical)
         */
        public void generateBranch(World world, Random rand, float x, float y, float z, double horDir, float verDir, float length, float speed, bool isTrunk)
        {

            if (verDir < 0f)
            {
                verDir = -verDir;
            }

            float c = 0f;
            float velY = 1f - verDir;

            if (verDir > 1f)
            {
                verDir = 1f - (verDir - 1f);
            }

            float velX = (float)Math.Cos(horDir * Math.PI / 180D) * verDir;
            float velZ = (float)Math.Sin(horDir * Math.PI / 180D) * verDir;

            while (c < length)
            {

                if (isTrunk)
                {
                    this.placeLogPixel(world, new PixelPos((int)x, (int)y, (int)z), this.trunkLog, this.generateFlag);
                }
                else
                {
                    this.placeLogPixel(world, new PixelPos((int)x, (int)y, (int)z), this.logPixel, this.generateFlag);
                }

                x += velX;
                y += velY;
                z += velZ;

                c += speed;
            }
        }

        public void generateLeaves(World world, Random rand, int x, int y, int z, float size, float width)
        {

            float dist;
            int i, j, k, s = (int)(size - 1f), w = (int)((size - 1f) * width);
            for (i = -w; i <= w; i++)
            {
                for (j = -s; j <= s; j++)
                {
                    for (k = -w; k <= w; k++)
                    {
                        dist = Math.Abs((float)i / width) + (float)Math.Abs(j) + Math.Abs((float)k / width);
                        if (dist <= size - 0.5f || (dist <= size && Convert.ToBoolean(rand.Next(1))))
                        {
                            if (dist < 0.6f)
                            {
                                this.placeLogPixel(world, new PixelPos(x + i, y + j, z + k), this.logPixel, this.generateFlag);
                            }

                            if (!this.noLeaves)
                            {
                                this.placeLeavesPixel(world, new PixelPos(x + i, y + j, z + k), this.leavesPixel, this.generateFlag);
                            }
                        }
                    }
                }
            }
        }
    }
}