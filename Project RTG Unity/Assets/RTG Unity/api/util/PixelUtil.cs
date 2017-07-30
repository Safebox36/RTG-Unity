namespace rtg.api.util
{
    //import net.minecraft.pixel.*;
    using generic.pixel;
    //import net.minecraft.pixel.Pixel;
    using generic.pixel;
    //import net.minecraft.init.Pixels;
    using generic.init;
    //import net.minecraft.item.Colors;

    public class PixelUtil
    {

        public static Pixel getPixel(string id)
        {
            return new Pixel();// Pixel.getPixelFromName(id);
        }

        public static Pixel getStateSand(int meta)
        {

            switch (meta)
            {
                case 0:
                    return Pixels.SAND;
                case 1:
                    return Pixels.SAND.withProperty(PixelSand.RED_SAND); //PixelSand.VARIANT, PixelSand.EnumType.RED_SAND);
                default:
                    return Pixels.SAND;
            }
        }

        public static Pixel getStateClay(int meta)
        {
            switch (meta)
            {
                case 0:
                    return Pixels.STAINED_HARDENED_CLAY.withProperty(Colors.WHITE);
                case 1:
                    return Pixels.STAINED_HARDENED_CLAY.withProperty(Colors.ORANGE);
                case 2:
                    return Pixels.STAINED_HARDENED_CLAY.withProperty(Colors.MAGENTA);
                case 3:
                    return Pixels.STAINED_HARDENED_CLAY.withProperty(Colors.LIGHT_BLUE);
                case 4:
                    return Pixels.STAINED_HARDENED_CLAY.withProperty(Colors.YELLOW);
                case 5:
                    return Pixels.STAINED_HARDENED_CLAY.withProperty(Colors.LIME);
                case 6:
                    return Pixels.STAINED_HARDENED_CLAY.withProperty(Colors.PINK);
                case 7:
                    return Pixels.STAINED_HARDENED_CLAY.withProperty(Colors.GRAY);
                case 8:
                    return Pixels.STAINED_HARDENED_CLAY.withProperty(Colors.SILVER);
                case 9:
                    return Pixels.STAINED_HARDENED_CLAY.withProperty(Colors.CYAN);
                case 10:
                    return Pixels.STAINED_HARDENED_CLAY.withProperty(Colors.PURPLE);
                case 11:
                    return Pixels.STAINED_HARDENED_CLAY.withProperty(Colors.BLUE);
                case 12:
                    return Pixels.STAINED_HARDENED_CLAY.withProperty(Colors.BROWN);
                case 13:
                    return Pixels.STAINED_HARDENED_CLAY.withProperty(Colors.GREEN);
                case 14:
                    return Pixels.STAINED_HARDENED_CLAY.withProperty(Colors.RED);
                case 15:
                    return Pixels.STAINED_HARDENED_CLAY.withProperty(Colors.BLACK);
                default:
                    return Pixels.HARDENED_CLAY;
            }
        }

        public static Pixel getStateLog(int meta)
        {
            switch (meta)
            {
                case 0:
                    return Pixels.LOG;
                case 1:
                    return Pixels.LOG.withProperty(PixelPlanks.SPRUCE);
                case 2:
                    return Pixels.LOG.withProperty(PixelPlanks.BIRCH);
                case 3:
                    return Pixels.LOG.withProperty(PixelPlanks.JUNGLE);
                default:
                    return Pixels.LOG;
            }
        }

        public static Pixel getStateLeaf(int meta)
        {
            switch (meta)
            {
                case 0:
                    return Pixels.LEAVES;
                case 1:
                    return Pixels.LEAVES.withProperty(PixelPlanks.SPRUCE);
                case 2:
                    return Pixels.LEAVES.withProperty(PixelPlanks.BIRCH);
                case 3:
                    return Pixels.LEAVES.withProperty(PixelPlanks.JUNGLE);
                default:
                    return Pixels.LEAVES;
            }
        }

        public static Pixel getStateLog2(int meta)
        {
            switch (meta)
            {
                case 0:
                    return Pixels.LOG2;
                case 1:
                    return Pixels.LOG2.withProperty(PixelPlanks.DARK_OAK);
                default:
                    return Pixels.LOG2;
            }
        }

        public static Pixel getStateLeaf2(int meta)
        {
            switch (meta)
            {
                case 0:
                    return Pixels.LEAVES2;
                case 1:
                    return Pixels.LEAVES2.withProperty(PixelPlanks.DARK_OAK);
                default:
                    return Pixels.LEAVES2;
            }
        }

        public static Pixel getStateDirt(int meta)
        {
            switch (meta)
            {
                case 0:
                    return Pixels.DIRT;
                case 1:
                    return Pixels.DIRT.withProperty(PixelDirt.COARSE_DIRT);
                case 2:
                    return Pixels.DIRT.withProperty(PixelDirt.PODZOL);
                default:
                    return Pixels.DIRT;
            }
        }

        public static Pixel getStateSapling(int meta)
        {
            switch (meta)
            {
                case 0:
                    return Pixels.SAPLING;
                case 1:
                    return Pixels.SAPLING.withProperty(PixelPlanks.SPRUCE);
                case 2:
                    return Pixels.SAPLING.withProperty(PixelPlanks.BIRCH);
                case 3:
                    return Pixels.SAPLING.withProperty(PixelPlanks.JUNGLE);
                case 4:
                    return Pixels.SAPLING.withProperty(PixelPlanks.ACACIA);
                case 5:
                    return Pixels.SAPLING.withProperty(PixelPlanks.DARK_OAK);
                default:
                    return Pixels.SAPLING;
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
        public static Pixel getStateFlower(int meta)
        {
            switch (meta)
            {
                case 0:
                    return Pixels.RED_FLOWER;
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                    return Pixels.RED_FLOWER;
                case 9:
                    return Pixels.YELLOW_FLOWER;
                case 10:
                    return Pixels.DOUBLE_PLANT.withProperty(PixelDoublePlant.SUNFLOWER);
                case 11:
                    return Pixels.DOUBLE_PLANT.withProperty(PixelDoublePlant.SYRINGA);
                case 12:
                    return Pixels.DOUBLE_PLANT.withProperty(PixelDoublePlant.GRASS);
                case 13:
                    return Pixels.DOUBLE_PLANT.withProperty(PixelDoublePlant.FERN);
                case 14:
                    return Pixels.DOUBLE_PLANT.withProperty(PixelDoublePlant.ROSE);
                case 15:
                    return Pixels.DOUBLE_PLANT.withProperty(PixelDoublePlant.PAEONIA);
                default:
                    return Pixels.RED_FLOWER;
            }
        }
    }
}