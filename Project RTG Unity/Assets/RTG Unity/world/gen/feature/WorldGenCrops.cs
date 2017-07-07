namespace rtg.world.gen.feature
{
    using System;
    //import javax.annotation.ParametersAreNonnullByDefault;

    //import net.minecraft.block.Block;
    using generic.block;
    //import net.minecraft.block.BlockCrops;
    //import net.minecraft.block.BlockFarmland;
    //import net.minecraft.block.material.Material;
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

    public class WorldGenCrops : WorldGenerator
    {

        private Block farmType;
        private int farmSize;
        private int farmDensity;
        private int farmHeight;
        private bool farmWater;


        /*
         * 0 = potatoes, 1 = carrots, 2 = beetroot, 3 = wheat
         */
        public WorldGenCrops(int type, int size, int density, int height, Boolean water)
        {

            farmType = type == 0 ? Blocks.POTATOES : type == 1 ? Blocks.CARROTS : type == 2 ? Blocks.BEETROOTS : Blocks.WHEAT;
            farmSize = size;
            farmDensity = density;
            farmHeight = height;
            farmWater = water;
        }

        public bool generate(World world, Random rand, BlockPos blockPos)
        {
            return this.generate(world, rand, blockPos.getX(), blockPos.getY(), blockPos.getZ());
        }

        public bool generate(World world, Random rand, int x, int y, int z)
        {

            IBlockState b;
            while (y > 0)
            {
                b = world.getBlockState(new BlockPos(x, y, z));
                if (!world.isAirBlock(new BlockPos(x, y, z)) || b.getBlock().isLeaves(world, new BlockPos(x, y, z)))
                {
                    break;
                }
                y--;
            }

            b = world.getBlockState(new BlockPos(x, y, z));
            if (b.getBlock() != Blocks.GRASS && b.getBlock() != Blocks.DIRT)
            {
                return false;
            }

            for (int j = 0; j < 4; j++)
            {
                b = world.getBlockState(new BlockPos(j == 0 ? x - 1 : j == 1 ? x + 1 : x, y, j == 2 ? z - 1 : j == 3 ? z + 1 : z));
            }

            //int maxGrowth = (farmType).getMaxAge() + 1;

            int rx, ry, rz;
            for (int i = 0; i < farmDensity; i++)
            {
                rx = rand.Next(farmSize) - 2;
                ry = rand.Next(farmHeight) - 1;
                rz = rand.Next(farmSize) - 2;
                b = world.getBlockState(new BlockPos(x + rx, y + ry, z + rz));

                if ((b.getBlock() == Blocks.GRASS || b.getBlock() == Blocks.DIRT) && world.isAirBlock(new BlockPos(x + rx, y + ry + 1, z + rz)))
                {
                    world.setBlockState(new BlockPos(x + rx, y + ry, z + rz), Blocks.FARMLAND.getDefaultState().withProperty(rand.Next(8)));
                    world.setBlockState(new BlockPos(x + rx, y + ry + 1, z + rz), (farmType)/*.withAge(rand.Next(maxGrowth))*/);
                }
            }
            if (farmWater == true)
            {
                world.setBlockState(new BlockPos(x, y, z), Blocks.WATER.getDefaultState());
            }
            return true;
        }
    }
}