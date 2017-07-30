namespace generic.world
{
    using System;
    using UnityEngine;

    public class World : MonoBehaviour
    {
        //Variables
        public struct pixelData
        {
            byte height;
            byte pixel;
            byte state;
            public int getHeight
            {
                get
                {
                    return Convert.ToInt16(height);
                }
            }

            public int getPixel
            {
                get
                {
                    return Convert.ToInt16(pixel);
                }
            }

            public int getState
            {
                get
                {
                    return Convert.ToInt16(state);
                }
            }

            public pixelData(int height, int pixel, int state)
            {
                this.height = Convert.ToByte(height);
                this.pixel = Convert.ToByte(pixel);
                this.state = Convert.ToByte(state);
            }
        }

        public int levelSeed = (int)DateTime.Now.Ticks;
        [Tooltip("Warning: Values higher than 2048 are not recommended.")]
        public Vector2 worldSize = new Vector2(15, 15);
        internal System.Random rand;

        private pixelData[,] worldFile;
        private int seaLevel = 63;

        //Constructors
        public World()
        {
            worldFile = new pixelData[16, 16];
        }

        public World(int width, int height)
        {
            worldFile = new pixelData[width, height];
        }

        public int getSeed()
        {
            return levelSeed;
        }

        //Get / Set Pixel
        public generic.pixel.Pixel getPixelState(generic.util.math.PixelPos pos)
        {
            return new pixel.Pixel(worldFile[pos.getX(), pos.getZ()].getPixel, worldFile[pos.getX(), pos.getZ()].getState);
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
            worldFile[pos.getX(), pos.getZ()] = new pixelData(pos.getY(), pixel.getPixelID(), pixel.getState());
        }

        public void setPixelState(generic.util.math.PixelPos pos, int pixel, int state)
        {
            worldFile[pos.getX(), pos.getZ()] = new pixelData(pos.getY(), pixel, state);
        }

        public void setPixelState(generic.util.math.PixelPos pos, float pixel, float state)
        {
            worldFile[pos.getX(), pos.getZ()] = new pixelData(pos.getY(), (int)pixel, (int)state);
        }

        //Check Pixel
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
            return worldFile[pos.getX(), pos.getZ()].getHeight;
        }

        public int getActualHeight()
        {
            return 256;
        }
    }
}
