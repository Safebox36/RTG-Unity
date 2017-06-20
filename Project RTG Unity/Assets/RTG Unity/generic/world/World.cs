namespace generic.world
{
    using UnityEngine;
    using System;

    public class World : MonoBehaviour
    {
        public int LevelSeed = (int)DateTime.Now.Ticks;
        internal System.Random rand;

        public int getSeed()
        {
            return LevelSeed;
        }
    }
}
