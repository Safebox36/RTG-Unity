namespace rtg.world.gen.terrain
{
    using rtg.api.world;

    public class TerrainBeach : TerrainBase
    {

        public TerrainBeach()
        {

        }

        override public float generateNoise(RTGWorld rtgWorld, int x, int y, float border, float river)
        {
            return terrainBeach(x, y, rtgWorld.simplex, river, 180f, 35f, 60f);
        }
    }
}