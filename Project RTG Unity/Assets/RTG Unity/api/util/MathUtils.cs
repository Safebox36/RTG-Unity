namespace rtg.api.util
{
    using System;
    public class MathUtils
    {

        public static int[] XY_INVERTED;

        public void temp()
        {
            int[] result = new int[256];
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    result[i * 16 + j] = j * 16 + i;
                }
            }
            for (int i = 0; i < 256; i++)
            {
                if (result[result[i]] != i) throw new Exception("" + i + "" + result[i] + " " + result[result[i]]);
            }
            XY_INVERTED = result;
        }

        public static int globalToLocal(int x)
        {
            return ((x % 16) + 16) % 16;
        }

        public static int globalToChunk(int x)
        {
            return (int)Math.Floor((double)x / 16d);
        }

        public static int globalToIndex(int x, int z)
        {
            return globalToLocal(x) * 16 + globalToLocal(z);
        }
    }
}