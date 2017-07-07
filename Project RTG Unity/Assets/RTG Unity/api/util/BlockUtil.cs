namespace rtg.api.util
{
    //import net.minecraft.block.*;
    using generic.block;
    //import net.minecraft.block.state.IBlockState;
    using generic.block.state;
    //import net.minecraft.init.Blocks;
    using generic.init;
    //import net.minecraft.item.EnumDyeColor;

    public class BlockUtil
    {

        public static Block getBlock(string id)
        {
            return Block.getBlockFromName(id);
        }

        public static IBlockState getStateSand(int meta)
        {

            switch (meta)
            {
                case 0:
                    return (IBlockState)Blocks.SAND.getDefaultState();
                case 1:
                    return (IBlockState)Blocks.SAND.getDefaultState().withProperty(Blocks.SAND, EnumDyeColor.RED); //BlockSand.VARIANT, BlockSand.EnumType.RED_SAND);
                default:
                    return (IBlockState)Blocks.SAND.getDefaultState();
            }
        }

        public static IBlockState getStateClay(int meta)
        {
            switch (meta)
            {
                case 0:
                    return (IBlockState)Blocks.STAINED_HARDENED_CLAY.getDefaultState().withProperty(EnumDyeColor.WHITE);
                case 1:
                    return (IBlockState)Blocks.STAINED_HARDENED_CLAY.getDefaultState().withProperty(EnumDyeColor.ORANGE);
                case 2:
                    return (IBlockState)Blocks.STAINED_HARDENED_CLAY.getDefaultState().withProperty(EnumDyeColor.MAGENTA);
                case 3:
                    return (IBlockState)Blocks.STAINED_HARDENED_CLAY.getDefaultState().withProperty(EnumDyeColor.LIGHT_BLUE);
                case 4:
                    return (IBlockState)Blocks.STAINED_HARDENED_CLAY.getDefaultState().withProperty(EnumDyeColor.YELLOW);
                case 5:
                    return (IBlockState)Blocks.STAINED_HARDENED_CLAY.getDefaultState().withProperty(EnumDyeColor.LIME);
                case 6:
                    return (IBlockState)Blocks.STAINED_HARDENED_CLAY.getDefaultState().withProperty(EnumDyeColor.PINK);
                case 7:
                    return (IBlockState)Blocks.STAINED_HARDENED_CLAY.getDefaultState().withProperty(EnumDyeColor.GRAY);
                case 8:
                    return (IBlockState)Blocks.STAINED_HARDENED_CLAY.getDefaultState().withProperty(EnumDyeColor.SILVER);
                case 9:
                    return (IBlockState)Blocks.STAINED_HARDENED_CLAY.getDefaultState().withProperty(EnumDyeColor.CYAN);
                case 10:
                    return (IBlockState)Blocks.STAINED_HARDENED_CLAY.getDefaultState().withProperty(EnumDyeColor.PURPLE);
                case 11:
                    return (IBlockState)Blocks.STAINED_HARDENED_CLAY.getDefaultState().withProperty(EnumDyeColor.BLUE);
                case 12:
                    return (IBlockState)Blocks.STAINED_HARDENED_CLAY.getDefaultState().withProperty(EnumDyeColor.BROWN);
                case 13:
                    return (IBlockState)Blocks.STAINED_HARDENED_CLAY.getDefaultState().withProperty(EnumDyeColor.GREEN);
                case 14:
                    return (IBlockState)Blocks.STAINED_HARDENED_CLAY.getDefaultState().withProperty(EnumDyeColor.RED);
                case 15:
                    return (IBlockState)Blocks.STAINED_HARDENED_CLAY.getDefaultState().withProperty(EnumDyeColor.BLACK);
                default:
                    return (IBlockState)Blocks.HARDENED_CLAY.getDefaultState();
            }
        }

        public static IBlockState getStateLog(int meta)
        {
            switch (meta)
            {
                case 0:
                    return (IBlockState)Blocks.LOG.getDefaultState();
                case 1:
                    return (IBlockState)Blocks.LOG.getDefaultState().withProperty(BlockPlanks.SPRUCE);
                case 2:
                    return (IBlockState)Blocks.LOG.getDefaultState().withProperty(BlockPlanks.BIRCH);
                case 3:
                    return (IBlockState)Blocks.LOG.getDefaultState().withProperty(BlockPlanks.JUNGLE);
                default:
                    return (IBlockState)Blocks.LOG.getDefaultState();
            }
        }

        public static IBlockState getStateLeaf(int meta)
        {
            switch (meta)
            {
                case 0:
                    return (IBlockState)Blocks.LEAVES.getDefaultState();
                case 1:
                    return (IBlockState)Blocks.LEAVES.getDefaultState().withProperty(BlockPlanks.SPRUCE);
                case 2:
                    return (IBlockState)Blocks.LEAVES.getDefaultState().withProperty(BlockPlanks.BIRCH);
                case 3:
                    return (IBlockState)Blocks.LEAVES.getDefaultState().withProperty(BlockPlanks.JUNGLE);
                default:
                    return (IBlockState)Blocks.LEAVES.getDefaultState();
            }
        }

        public static IBlockState getStateLog2(int meta)
        {
            switch (meta)
            {
                case 0:
                    return (IBlockState)Blocks.LOG2.getDefaultState();
                case 1:
                    return (IBlockState)Blocks.LOG2.getDefaultState().withProperty(BlockPlanks.DARK_OAK);
                default:
                    return (IBlockState)Blocks.LOG2.getDefaultState();
            }
        }

        public static IBlockState getStateLeaf2(int meta)
        {
            switch (meta)
            {
                case 0:
                    return (IBlockState)Blocks.LEAVES2.getDefaultState();
                case 1:
                    return (IBlockState)Blocks.LEAVES2.getDefaultState().withProperty(BlockPlanks.DARK_OAK);
                default:
                    return (IBlockState)Blocks.LEAVES2.getDefaultState();
            }
        }

        public static IBlockState getStateDirt(int meta)
        {
            switch (meta)
            {
                case 0:
                    return (IBlockState)Blocks.DIRT.getDefaultState();
                case 1:
                    return (IBlockState)Blocks.DIRT.getDefaultState().withProperty(BlockDirt.COARSE_DIRT);
                case 2:
                    return (IBlockState)Blocks.DIRT.getDefaultState().withProperty(BlockDirt.PODZOL);
                default:
                    return (IBlockState)Blocks.DIRT.getDefaultState();
            }
        }

        public static IBlockState getStateSapling(int meta)
        {
            switch (meta)
            {
                case 0:
                    return (IBlockState)Blocks.SAPLING.getDefaultState();
                case 1:
                    return (IBlockState)Blocks.SAPLING.getDefaultState().withProperty(BlockPlanks.SPRUCE);
                case 2:
                    return (IBlockState)Blocks.SAPLING.getDefaultState().withProperty(BlockPlanks.BIRCH);
                case 3:
                    return (IBlockState)Blocks.SAPLING.getDefaultState().withProperty(BlockPlanks.JUNGLE);
                case 4:
                    return (IBlockState)Blocks.SAPLING.getDefaultState().withProperty(BlockPlanks.ACACIA);
                case 5:
                    return (IBlockState)Blocks.SAPLING.getDefaultState().withProperty(BlockPlanks.DARK_OAK);
                default:
                    return (IBlockState)Blocks.SAPLING.getDefaultState();
            }
        }

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
        public static IBlockState getStateFlower(int meta)
        {
            switch (meta)
            {
                case 0:
                    return (IBlockState)Blocks.RED_FLOWER.getDefaultState();
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                    return (IBlockState)Blocks.RED_FLOWER.getStateFromMeta(meta);
                case 9:
                    return (IBlockState)Blocks.YELLOW_FLOWER.getDefaultState();
                case 10:
                    return (IBlockState)Blocks.DOUBLE_PLANT.getDefaultState().withProperty(BlockDoublePlant.SUNFLOWER);
                case 11:
                    return (IBlockState)Blocks.DOUBLE_PLANT.getDefaultState().withProperty(BlockDoublePlant.SYRINGA);
                case 12:
                    return (IBlockState)Blocks.DOUBLE_PLANT.getDefaultState().withProperty(BlockDoublePlant.GRASS);
                case 13:
                    return (IBlockState)Blocks.DOUBLE_PLANT.getDefaultState().withProperty(BlockDoublePlant.FERN);
                case 14:
                    return (IBlockState)Blocks.DOUBLE_PLANT.getDefaultState().withProperty(BlockDoublePlant.ROSE);
                case 15:
                    return (IBlockState)Blocks.DOUBLE_PLANT.getDefaultState().withProperty(BlockDoublePlant.PAEONIA);
                default:
                    return (IBlockState)Blocks.RED_FLOWER.getStateFromMeta(meta);
            }
        }
    }
}