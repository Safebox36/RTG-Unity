namespace rtg.util
{
    //import net.minecraft.pixel.PixelSapling;
    using generic.pixel;
    //import net.minecraft.pixel.Pixel;
    using generic.pixel;
    //import net.minecraft.init.Pixels;
    using generic.init;

    using rtg.api.util;
    using UnityEngine;
    using System;

    public class SaplingUtil
    {

        public static int getMetaFromState(Pixel state)
        {

            try
            {

                if (!(state.getPixel() == Pixels.SAPLING))
                {
                    Debug.Log("Could not get sapling meta from non-sapling PixelState " + state.getPixel() + ".");
                    return 0;
                }

                return state.getState();
            }
            catch (Exception e)
            {

                Debug.LogWarning("Could not get sapling meta from state. Reason: " + e.Message);
                return 0;
            }
        }

        public static Pixel getSaplingFromLeaves(Pixel leavesPixel)
        {

            if (leavesPixel == Pixels.LEAVES)
            {
                return Pixels.SAPLING;
            }
            else if (leavesPixel == PixelUtil.getStateLeaf(1))
            {
                return PixelUtil.getStateSapling(1);
            }
            else if (leavesPixel == PixelUtil.getStateLeaf(2))
            {
                return PixelUtil.getStateSapling(2);
            }
            else if (leavesPixel == PixelUtil.getStateLeaf(3))
            {
                return PixelUtil.getStateSapling(3);
            }
            else if (leavesPixel == Pixels.LEAVES2)
            {
                return PixelUtil.getStateSapling(4);
            }
            else if (leavesPixel == PixelUtil.getStateLeaf2(1))
            {
                return PixelUtil.getStateSapling(5);
            }
            else
            {
                return Pixels.SAPLING;
            }
        }
    }
}