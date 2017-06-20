namespace rtg.world.gen.terrain
{
    using rtg.api.world;

    public class TerrainFlatLakes : TerrainBase
    {
        public TerrainFlatLakes()
        {

        }

        override public float generateNoise(RTGWorld rtgWorld, int x, int y, float border, float river)
        {
            return terrainFlatLakes(x, y, rtgWorld.simplex, river, 3f, 62f);
        }
    }
}