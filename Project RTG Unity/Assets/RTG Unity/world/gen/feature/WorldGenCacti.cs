namespace rtg.world.gen.feature
{
    using System;

    //import net.minecraft.block.state.IBlockState;
    using generic.block.state;
    //import net.minecraft.init.Blocks;
    using generic.init;
    //import net.minecraft.util.math.BlockPos;
    using generic.util.math;
    //import net.minecraft.world.World;
    using generic.world;
    //import net.minecraft.world.gen.feature.WorldGenerator;
    using generic.world.gen.feature;

    public class WorldGenCacti : WorldGenerator
    {

        private bool sand;
        private int eHeight;
        private IBlockState soilBlock;

        public WorldGenCacti(bool sandOnly) : this(sandOnly, 0)
        {

        }

        public WorldGenCacti(bool sandOnly, int extraHeight) : this(sandOnly, extraHeight, (IBlockState)Blocks.SAND.getDefaultState())
        {

        }

        public WorldGenCacti(bool sandOnly, int extraHeight, IBlockState soilBlock)
        {
            sand = sandOnly;
            eHeight = extraHeight;
            this.setSoilBlock(soilBlock);
        }

        override public bool generate(World world, Random rand, BlockPos pos)
        {

            int x = pos.getX();
            int y = pos.getY();
            int z = pos.getZ();
            IBlockState b;
            //for (int l = 0; l < 10; ++l)
            {
                int i1 = x;// + rand.nextInt(8) - rand.nextInt(8);
                int j1 = y + rand.Next(4) - rand.Next(4);
                int k1 = z;// + rand.nextInt(8) - rand.nextInt(8);

                if (world.isAirBlock(new BlockPos(i1, j1, k1)))
                {
                    b = world.getBlockState(new BlockPos(i1, j1 - 1, k1));
                    if (b == this.soilBlock || (!sand && (b == Blocks.GRASS.getDefaultState() || b == Blocks.DIRT.getDefaultState())))
                    {
                        int l1 = 1 + rand.Next(rand.Next(3) + 1);
                        if (b == Blocks.GRASS.getDefaultState() || b == Blocks.DIRT.getDefaultState())
                        {
                            world.setBlockState(new BlockPos(i1, j1 - 1, k1), this.soilBlock, 2);
                        }

                        for (int i2 = 0; i2 < l1 + eHeight; ++i2)
                        {
                            //if (Blocks.CACTUS.canBlockStay(world, new BlockPos(i1, j1 + i2, k1)))     //fix later
                            //{
                            world.setBlockState(new BlockPos(i1, j1 + i2, k1), Blocks.CACTUS.getDefaultState(), 2);
                            //}
                        }
                    }
                }
            }

            return true;
        }

        public IBlockState getSoilBlock()
        {

            return soilBlock;
        }

        public WorldGenCacti setSoilBlock(IBlockState soilBlock)
        {

            this.soilBlock = soilBlock;
            return this;
        }
    }
}