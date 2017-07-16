namespace generic.world
{
    using System;
    using UnityEngine;

    public class World
    {
        public Texture2D worldFile;
        public int levelSeed = (int)DateTime.Now.Ticks;
        internal System.Random rand;

        private int seaLevel = 63;

        public World()
        {
            worldFile = new Texture2D(16, 16, TextureFormat.RGB24, false);
        }

        public World(int width, int height)
        {
            worldFile = new Texture2D(width, height, TextureFormat.RGB24, false);
        }

        public int getSeed()
        {
            return levelSeed;
        }
        
        public generic.pixel.state.IPixelState getPixelState(generic.util.math.PixelPos pos)
        {
            return new pixel.state.IPixelState();
        }

        public void setPixelState(generic.util.math.PixelPos pos, int pixel)
        {
            setPixelState(pos, pixel, 0);
        }

        public void setPixelState(generic.util.math.PixelPos pos, float pixel)
        {
            setPixelState(pos, pixel, 0);
        }

        public void setPixelState(generic.util.math.PixelPos pos, generic.pixel.Pixel pixel)
        {
            worldFile.SetPixel(pos.getX(), pos.getZ(), new Color(pos.getY() / 255f, pixel.getPixelID() / 255f, pixel.getState() / 255f));
        }

        public void setPixelState(generic.util.math.PixelPos pos, int pixel, int state)
        {
            worldFile.SetPixel(pos.getX(), pos.getZ(), new Color(pos.getY() / 255f, pixel / 255f, state / 255f));
        }

        public void setPixelState(generic.util.math.PixelPos pos, float pixel, float state)
        {
            worldFile.SetPixel(pos.getX(), pos.getZ(), new Color(pos.getY() / 255f, pixel / 255f, state / 255f));
        }

        public bool isAirPixel(generic.util.math.PixelPos pos)
        {
            if (getPixelState(pos).getPixel() == generic.init.Pixels.AIR)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int getHeight(generic.util.math.PixelPos pos)
        {
            return (int)(worldFile.GetPixel(pos.getX(), pos.getZ()).r * 255f);
        }

        public int getActualHeight()
        {
            return 256;
        }
    }
}
