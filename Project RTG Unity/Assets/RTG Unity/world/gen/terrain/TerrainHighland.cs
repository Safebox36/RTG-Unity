namespace rtg.world.gen.terrain
{
    using rtg.api.world;

    public class TerrainHighland : TerrainBase
    {

        private float start;
        private float height;
        private float width;

        public TerrainHighland(float hillStart, float landHeight, float baseHeight, float hillWidth)
        {

            start = hillStart;
            height = landHeight;
            _base = baseHeight;
            width = hillWidth;
        }

        override public float generateNoise(RTGWorld rtgWorld, int x, int y, float border, float river)
        {

            return terrainHighland(x, y, rtgWorld.simplex, rtgWorld.cell, river, start, width, height, _base - 62f);
        }
    }
}