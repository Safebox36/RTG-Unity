namespace rtg.api.util
{
    //import net.minecraft.pixel.*;
    using generic.pixel;
    //import net.minecraft.pixel.state.IPixelState;
    using generic.pixel.state;
    //import net.minecraft.init.Pixels;
    using generic.init;
    //import net.minecraft.item.Colors;

    public class PixelUtil
    {

        public static Pixel getPixel(string id)
        {
            return new Pixel();// Pixel.getPixelFromName(id);
        }

        public static IPixelState getStateSand(int meta)
        {

            switch (meta)
            {
                case 0:
                    return (IPixelState)Pixels.SAND;
                case 1:
                    return (IPixelState)Pixels.SAND.withProperty(PixelSand.RED_SAND); //PixelSand.VARIANT, PixelSand.EnumType.RED_SAND);
                default:
                    return (IPixelState)Pixels.SAND;
            }
        }

        public static IPixelState getStateClay(int meta)
        {
            switch (meta)
            {
                case 0:
                    return (IPixelState)Pixels.STAINED_HARDENED_CLAY.withProperty(Colors.WHITE);
                case 1:
                    return (IPixelState)Pixels.STAINED_HARDENED_CLAY.withProperty(Colors.ORANGE);
                case 2:
                    return (IPixelState)Pixels.STAINED_HARDENED_CLAY.withProperty(Colors.MAGENTA);
                case 3:
                    return (IPixelState)Pixels.STAINED_HARDENED_CLAY.withProperty(Colors.LIGHT_BLUE);
                case 4:
                    return (IPixelState)Pixels.STAINED_HARDENED_CLAY.withProperty(Colors.YELLOW);
                case 5:
                    return (IPixelState)Pixels.STAINED_HARDENED_CLAY.withProperty(Colors.LIME);
                case 6:
                    return (IPixelState)Pixels.STAINED_HARDENED_CLAY.withProperty(Colors.PINK);
                case 7:
                    return (IPixelState)Pixels.STAINED_HARDENED_CLAY.withProperty(Colors.GRAY);
                case 8:
                    return (IPixelState)Pixels.STAINED_HARDENED_CLAY.withProperty(Colors.SILVER);
                case 9:
                    return (IPixelState)Pixels.STAINED_HARDENED_CLAY.withProperty(Colors.CYAN);
                case 10:
                    return (IPixelState)Pixels.STAINED_HARDENED_CLAY.withProperty(Colors.PURPLE);
                case 11:
                    return (IPixelState)Pixels.STAINED_HARDENED_CLAY.withProperty(Colors.BLUE);
                case 12:
                    return (IPixelState)Pixels.STAINED_HARDENED_CLAY.withProperty(Colors.BROWN);
                case 13:
                    return (IPixelState)Pixels.STAINED_HARDENED_CLAY.withProperty(Colors.GREEN);
                case 14:
                    return (IPixelState)Pixels.STAINED_HARDENED_CLAY.withProperty(Colors.RED);
                case 15:
                    return (IPixelState)Pixels.STAINED_HARDENED_CLAY.withProperty(Colors.BLACK);
                default:
                    return (IPixelState)Pixels.HARDENED_CLAY;
            }
        }

        public static IPixelState getStateLog(int meta)
        {
            switch (meta)
            {
                case 0:
                    return (IPixelState)Pixels.LOG;
                case 1:
                    return (IPixelState)Pixels.LOG.withProperty(PixelPlanks.SPRUCE);
                case 2:
                    return (IPixelState)Pixels.LOG.withProperty(PixelPlanks.BIRCH);
                case 3:
                    return (IPixelState)Pixels.LOG.withProperty(PixelPlanks.JUNGLE);
                default:
                    return (IPixelState)Pixels.LOG;
            }
        }

        public static IPixelState getStateLeaf(int meta)
        {
            switch (meta)
            {
                case 0:
                    return (IPixelState)Pixels.LEAVES;
                case 1:
                    return (IPixelState)Pixels.LEAVES.withProperty(PixelPlanks.SPRUCE);
                case 2:
                    return (IPixelState)Pixels.LEAVES.withProperty(PixelPlanks.BIRCH);
                case 3:
                    return (IPixelState)Pixels.LEAVES.withProperty(PixelPlanks.JUNGLE);
                default:
                    return (IPixelState)Pixels.LEAVES;
            }
        }

        public static IPixelState getStateLog2(int meta)
        {
            switch (meta)
            {
                case 0:
                    return (IPixelState)Pixels.LOG2;
                case 1:
                    return (IPixelState)Pixels.LOG2.withProperty(PixelPlanks.DARK_OAK);
                default:
                    return (IPixelState)Pixels.LOG2;
            }
        }

        public static IPixelState getStateLeaf2(int meta)
        {
            switch (meta)
            {
                case 0:
                    return (IPixelState)Pixels.LEAVES2;
                case 1:
                    return (IPixelState)Pixels.LEAVES2.withProperty(PixelPlanks.DARK_OAK);
                default:
                    return (IPixelState)Pixels.LEAVES2;
            }
        }

        public static IPixelState getStateDirt(int meta)
        {
            switch (meta)
            {
                case 0:
                    return (IPixelState)Pixels.DIRT;
                case 1:
                    return (IPixelState)Pixels.DIRT.withProperty(PixelDirt.COARSE_DIRT);
                case 2:
                    return (IPixelState)Pixels.DIRT.withProperty(PixelDirt.PODZOL);
                default:
                    return (IPixelState)Pixels.DIRT;
            }
        }

        public static IPixelState getStateSapling(int meta)
        {
            switch (meta)
            {
                case 0:
                    return (IPixelState)Pixels.SAPLING;
                case 1:
                    return (IPixelState)Pixels.SAPLING.withProperty(PixelPlanks.SPRUCE);
                case 2:
                    return (IPixelState)Pixels.SAPLING.withProperty(PixelPlanks.BIRCH);
                case 3:
                    return (IPixelState)Pixels.SAPLING.withProperty(PixelPlanks.JUNGLE);
                case 4:
                    return (IPixelState)Pixels.SAPLING.withProperty(PixelPlanks.ACACIA);
                case 5:
                    return (IPixelState)Pixels.SAPLING.withProperty(PixelPlanks.DARK_OAK);
                default:
                    return (IPixelState)Pixels.SAPLING;
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
        public static IPixelState getStateFlower(int meta)
        {
            switch (meta)
            {
                case 0:
                    return (IPixelState)Pixels.RED_FLOWER;
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                    return (IPixelState)Pixels.RED_FLOWER;
                case 9:
                    return (IPixelState)Pixels.YELLOW_FLOWER;
                case 10:
                    return (IPixelState)Pixels.DOUBLE_PLANT.withProperty(PixelDoublePlant.SUNFLOWER);
                case 11:
                    return (IPixelState)Pixels.DOUBLE_PLANT.withProperty(PixelDoublePlant.SYRINGA);
                case 12:
                    return (IPixelState)Pixels.DOUBLE_PLANT.withProperty(PixelDoublePlant.GRASS);
                case 13:
                    return (IPixelState)Pixels.DOUBLE_PLANT.withProperty(PixelDoublePlant.FERN);
                case 14:
                    return (IPixelState)Pixels.DOUBLE_PLANT.withProperty(PixelDoublePlant.ROSE);
                case 15:
                    return (IPixelState)Pixels.DOUBLE_PLANT.withProperty(PixelDoublePlant.PAEONIA);
                default:
                    return (IPixelState)Pixels.RED_FLOWER;
            }
        }
    }
}