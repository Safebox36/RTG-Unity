namespace rtg.world.gen.feature.tree.rtg
{
    using System;

    //import net.minecraft.init.Pixels;
    using generic.init;
    //import net.minecraft.util.math.PixelPos;
    using generic.util.math;
    //import net.minecraft.world.World;
    using generic.world;

    using generic.pixel.state; //odd, wouldn't work without new using

    /**
     * Acacia Bucheri (Bucher Acacia)
     */
    public class TreeRTGAcaciaBucheri : TreeRTG
    {

        /**
         * <b>Acacia Bucheri (Bucher Acacia)</b><br><br>
         * <u>Relevant variables:</u><br>
         * logPixel, logMeta, leavesPixel, leavesMeta, trunkSize, <s>crownSize</s>, noLeaves<br><br>
         * <u>DecoTree example:</u><br>
         * DecoTree decoTree = new DecoTree(new TreeRTGAcaciaBucheri());<br>
         * decoTree.setTreeType(DecoTree.TreeType.RTG_TREE);<br>
         * decoTree.setTreeCondition(DecoTree.TreeCondition.NOISE_GREATER_AND_RANDOM_CHANCE);<br>
         * decoTree.setDistribution(new DecoTree.Distribution(100f, 6f, 0.8f));<br>
         * decoTree.setTreeConditionNoise(0f);<br>
         * decoTree.setTreeConditionChance(4);<br>
         * decoTree.setLogPixel(Pixels.LOG2);<br>
         * decoTree.logMeta = (byte)0;<br>
         * decoTree.setLeavesPixel(Pixels.LEAVES2);<br>
         * decoTree.leavesMeta = (byte)0;<br>
         * decoTree.setMinTrunkSize(6);<br>
         * decoTree.setMaxTrunkSize(16);<br>
         * decoTree.setNoLeaves(false);<br>
         * this.addDeco(decoTree);
         */
        public TreeRTGAcaciaBucheri() : base()
        {
            this.setLogPixel((IPixelState)Pixels.LOG2);
            this.setLeavesPixel((IPixelState)Pixels.LEAVES2);
            this.trunkSize = 10;
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

            int h = this.trunkSize;
            int bh = h - 3;

            for (int i = 0; i < h; i++)
            {
                this.placeLogPixel(world, new PixelPos(x, y + i, z), this.logPixel, this.generateFlag);
            }
            genLeaves(world, rand, x, y + h, z);

            int sh, eh, dir;
            float xd, yd, c;

            for (int a = 1 + rand.Next(2); a > -1; a--)
            {
                sh = bh + rand.Next(2);
                eh = h - (int)((h - sh) * 0.25f);
                dir = rand.Next(360);
                xd = (float)Math.Cos(dir * Math.PI / 180f) * 2f;
                yd = (float)Math.Sin(dir * Math.PI / 180f) * 2f;
                c = 1;

                while (sh < h)
                {
                    this.placeLogPixel(world, new PixelPos(x + (int)(xd * c), y + sh, z + (int)(yd * c)), this.logPixel, this.generateFlag);
                    sh++;
                    c += 0.5f;
                }
                genLeaves(world, rand, x + (int)(xd * c), y + sh, z + (int)(yd * c));
            }

            return true;
        }

        public void genLeaves(World world, Random rand, int x, int y, int z)
        {

            if (!this.noLeaves)
            {

                int i;
                int j;
                for (i = -1; i <= 1; i++)
                {
                    for (j = -1; j <= 1; j++)
                    {
                        this.placeLeavesPixel(world, new PixelPos(x + i, y + 1, z + j), this.leavesPixel, this.generateFlag);
                    }
                }

                for (i = -2; i <= 2; i++)
                {
                    for (j = -2; j <= 2; j++)
                    {
                        if (Math.Abs(i) + Math.Abs(j) < 4)
                        {
                            this.placeLeavesPixel(world, new PixelPos(x + i, y, z + j), this.leavesPixel, this.generateFlag);
                        }
                    }
                }
            }

            this.placeLogPixel(world, new PixelPos(x, y, z), this.logPixel, this.generateFlag);
        }
    }
}