namespace rtg.world.gen.terrain
{
    using rtg.api.world;

    public class TerrainGrasslandFlats : TerrainBase
    {
        public TerrainGrasslandFlats()
        {

        }

        override public float generateNoise(RTGWorld rtgWorld, int x, int y, float border, float river)
        {
            return terrainGrasslandFlats(x, y, rtgWorld.simplex, river, 40f, 25f, 68f);
        }
    }
}