﻿namespace rtg.world.gen.terrain
{
    using rtg.api.world;

    public class TerrainOcean : TerrainBase
    {

        public TerrainOcean()
        {

        }

        override public float generateNoise(RTGWorld rtgWorld, int x, int y, float border, float river)
        {

            float h = rtgWorld.simplex.noise2(x / 300f, y / 300f) * 40f * river;
            h = h > 3f ? 3f : h;
            h += rtgWorld.simplex.noise2(x / 50f, y / 50f) * (12f - h) * 0.4f;
            h += rtgWorld.simplex.noise2(x / 15f, y / 15f) * (12f - h) * 0.15f;

            float floNoise = 50f + h;
            floNoise = floNoise < minimumOceanFloor ? minimumOceanFloor : floNoise;

            return floNoise;
        }
    }
}