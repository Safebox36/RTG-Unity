﻿namespace rtg.world.gen.terrain
{
    using rtg.api.world;

    public class TerrainRollingHills : TerrainBase
    {

        private float minHeight = 63f;
        private float maxHeight = 80f;
        private float hillStrength = 30f;

        // 63f, 80f, 30f

        public TerrainRollingHills(float minHeight, float maxHeight, float hillStrength)
        {

            this.minHeight = minHeight;
            this.maxHeight = (maxHeight > rollingHillsMaxHeight) ? rollingHillsMaxHeight : ((maxHeight < this.minHeight) ? rollingHillsMaxHeight : maxHeight);
            this.hillStrength = hillStrength;
        }

        override public float generateNoise(RTGWorld rtgWorld, int x, int y, float border, float river)
        {

            _groundNoise = groundNoise(x, y, groundNoiseAmplitudeHills, rtgWorld.simplex);

            float m = hills(x, y, hillStrength, rtgWorld.simplex, river);

            float floNoise = riverized(minHeight + _groundNoise, river) + m;

            return floNoise + 0f;
        }
    }
}