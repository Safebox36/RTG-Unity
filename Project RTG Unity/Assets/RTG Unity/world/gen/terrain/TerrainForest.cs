namespace rtg.world.gen.terrain
{
    using rtg.api.world;

    public class TerrainForest : TerrainBase
    {
        public TerrainForest()
        {

        }

        override public float generateNoise(RTGWorld rtgWorld, int x, int y, float border, float river)
        {
            return terrainForest(x, y, rtgWorld.simplex, river, 70f);
        }
    }
}