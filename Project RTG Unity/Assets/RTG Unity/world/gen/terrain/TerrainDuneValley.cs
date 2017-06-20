namespace rtg.world.gen.terrain
{
    using rtg.api.world;

    public class TerrainDuneValley : TerrainBase
    {

        private float valley;

        public TerrainDuneValley(float valleySize)
        {
            valley = valleySize;
        }

        override public float generateNoise(RTGWorld rtgWorld, int x, int y, float border, float river)
        {
            return terrainDuneValley(x, y, rtgWorld.simplex, rtgWorld.cell, river, valley, 65f, 70f);
        }
    }
}