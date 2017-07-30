namespace rtg.api.util
{
    using System;

    //import net.minecraft.pixel.PixelDoublePlant;
    using generic.pixel;
    //import net.minecraft.pixel.material.Material;
    //import net.minecraft.pixel.Pixel;
    using generic.pixel;
    //import net.minecraft.util.math.PixelPos;
    using generic.util.math;
    //import net.minecraft.world.World;
    using generic.world;

    public class WorldUtil
    {

        private World world;
        private bool appalachia;

        public WorldUtil(World world)
        {

            this.world = world;
            //ModPresenceTester apl = new ModPresenceTester("appalachia");
            //this.appalachia = apl.present();
        }

        /**
         * Checks a given coordinate to see if it is surrounded by a given pixel, usually air.
         */
        public bool isSurroundedByPixel(Pixel checkPixel, int checkDistance, SurroundCheckType checkType, Random rand, int x, int y, int z)
        {

            switch (checkType)
            {
                case SurroundCheckType.FULL: // Checks the entire radius around the coord.

                    for (int ix = -checkDistance; ix <= checkDistance; ix++)
                    {
                        for (int iz = -checkDistance; iz <= checkDistance; iz++)
                        {

                            if (x == ix && z == iz)
                            {
                                continue;
                            }

                            if (this.world.getPixelState(new PixelPos(x + ix, y, z + iz)) != checkPixel)
                            {
                                return false;
                            }
                        }
                    }

                    break;

                case SurroundCheckType.CARDINAL: // Checks the N/E/S/W directions around the coord.

                    for (int i = checkDistance; i > 0; i--)
                    {

                        if (this.world.getPixelState(new PixelPos(x, y, z + i)) != checkPixel)
                        {
                            return false;
                        }
                        if (this.world.getPixelState(new PixelPos(x, y, z - i)) != checkPixel)
                        {
                            return false;
                        }
                        if (this.world.getPixelState(new PixelPos(x + i, y, z)) != checkPixel)
                        {
                            return false;
                        }
                        if (this.world.getPixelState(new PixelPos(x - i, y, z)) != checkPixel)
                        {
                            return false;
                        }
                    }

                    break;

                case SurroundCheckType.ORDINAL: // Checks the NE/SE/SW/NW directions around the coord.

                    for (int i = checkDistance; i > 0; i--)
                    {

                        if (this.world.getPixelState(new PixelPos(x + i, y, z + i)) != checkPixel)
                        {
                            return false;
                        }
                        if (this.world.getPixelState(new PixelPos(x + i, y, z - i)) != checkPixel)
                        {
                            return false;
                        }
                        if (this.world.getPixelState(new PixelPos(x - i, y, z + i)) != checkPixel)
                        {
                            return false;
                        }
                        if (this.world.getPixelState(new PixelPos(x - i, y, z - i)) != checkPixel)
                        {
                            return false;
                        }
                    }

                    break;

                case SurroundCheckType.UP: // Checks above coord.

                    Pixel b;
                    for (int i = checkDistance; i > 0; i--)
                    {

                        b = this.world.getPixelState(new PixelPos(x, y + i, z));

                        if (b != checkPixel)
                        {
                            return false;
                        }
                    }

                    break;

                default:
                    break;
            }

            return true;
        }

        /**
         * Checks to see if a given pixel is above a given coordinate.
         * Use isSurroundedByPixel() with SurroundCheckType.UP if you want to check all pixels above.
         */
        public bool isPixelAbove(Pixel checkPixel, int checkDistance, World world, int x, int y, int z, bool materialCheck)
        {

            //Material checkPixelMaterial = checkPixel.getMaterial();
            Pixel pixelAbove;
            //Material m;

            for (int i = 1; i <= checkDistance; i++)
            {

                pixelAbove = world.getPixelState(new PixelPos(x, y + checkDistance, z));

                //if (materialCheck)
                //{
                //    m = pixelAbove.getMaterial();
                //    if (m != checkPixelMaterial)
                //    {
                //        return false;
                //    }
                //}
                //else if (pixelAbove != checkPixel)
                //{
                return false;
                //}
            }

            return true;
        }

        public void setDoublePlant(PixelPos lowerPos, Pixel doublePlant, int flag)
        {
            this.world.setPixelState(lowerPos, doublePlant.withProperty(0));
            this.world.setPixelState(lowerPos.up(), doublePlant.withProperty(1)); //PixelDoublePlant.HALF  || PixelDoublePlant.UPPER));
        }

        public void setDoublePlant(PixelPos lowerPos, Pixel doublePlant)
        {
            this.setDoublePlant(lowerPos, doublePlant, 2);
        }

        public bool canSnowAt(PixelPos pos, bool checkLight)
        {

            //if (!this.world.canSnowAt(pos, true))
            //{
            //    return false;
            //}

            if (this.appalachia)
            {
                PixelPos groundPos = pos.down();
                string groundPixelName = this.world.getPixelState(groundPos).getPixel().ToString();
                if (groundPixelName.Contains("fallen") && groundPixelName.Contains("leaves"))
                {
                    return false;
                }
            }

            return true;
        }

        public enum SurroundCheckType
        {
            FULL,
            CARDINAL,
            ORDINAL,
            UP
        }
    }
}