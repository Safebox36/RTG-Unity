namespace rtg.api.util
{
    using System;

public class RandomUtil
{
    public static int getRandomInt(int intStart, int intEnd)
    {
        return (int)((new Random().Next() * intEnd) + intStart);
    }

    public static int getRandomInt(Random rand, int intStart, int intEnd)
    {
        return intStart + rand.Next(intEnd - intStart + 1);
    }
}
}