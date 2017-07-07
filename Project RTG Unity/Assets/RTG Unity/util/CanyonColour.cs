namespace rtg.util
{
    using System.Collections.Generic;

    //import net.minecraft.block.Block;
    using generic.block;
    //import net.minecraft.block.state.IBlockState;
    using generic.block.state;

    using rtg.api;
    using rtg.api.config;
    using rtg.api.util.noise;

    /**
     *
     * @author topisani
     *
     */
    public class CanyonColour
    {

        public static readonly CanyonColour MESA = new CanyonColour(RTGConfig.getPlateauGradientBlockMetasFromConfigString(RTGAPI.config().MESA_GRADIENT_STRING.get()));
        public static readonly CanyonColour MESA_BRYCE = new CanyonColour(RTGConfig.getPlateauGradientBlockMetasFromConfigString(RTGAPI.config().MESA_BRYCE_GRADIENT_STRING.get()));
        public static readonly CanyonColour SAVANNA = new CanyonColour(RTGConfig.getPlateauGradientBlockMetasFromConfigString(RTGAPI.config().SAVANNA_GRADIENT_STRING.get()));

        private static Dictionary<CanyonColour, IBlockState[]> colourBlocks = new Dictionary<CanyonColour, IBlockState[]>();
        private static OpenSimplexNoise simplex;
        private byte[] bytes;

        private static IBlockState plateauBlock = (IBlockState)generic.init.Blocks.HARDENED_CLAY;  //Block.getBlockFromName(RTGAPI.config().PLATEAU_BLOCK_ID.get()).getStateFromMeta(RTGAPI.config().PLATEAU_BLOCK_META.get());
        private static Block plateauGradientBlock = (IBlockState)generic.init.Blocks.STAINED_HARDENED_CLAY; //Block.getBlockFromName(RTGAPI.config().PLATEAU_GRADIENT_BLOCK_ID.get());

        CanyonColour(byte[] bytes)
        {
            this.bytes = bytes;
        }

        public static void init(long l)
        {

            simplex = new OpenSimplexNoise(l);

            foreach (CanyonColour colour in colourBlocks.Keys)
            {

                IBlockState[] c = new IBlockState[256];
                int j;

                for (int i = 0; i < 256; i++)
                {

                    byte b = colour.bytes[i % colour.bytes.Length];
                    c[i] = (b == -1) ? plateauBlock : (IBlockState)plateauGradientBlock.getStateFromMeta(b);
                }

                colourBlocks[colour] = c;
            }
        }

        public IBlockState getBlockForHeight(int x, int y, int z)
        {

            return getBlockForHeight(x, (float)y, z);
        }

        public IBlockState getBlockForHeight(int x, float y, int z)
        {

            y = (y < 0) ? 0 : (y > 255) ? 255 : y;

            return colourBlocks[this][(int)y];
        }
    }
}