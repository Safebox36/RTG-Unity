namespace rtg.world.gen.terrain
{
    using rtg.api.world;


    public class TerrainMarsh : TerrainBase
    {

        public TerrainMarsh()
        {

        }

        override public float generateNoise(RTGWorld rtgWorld, int x, int y, float border, float river)
        {
            return terrainMarsh(x, y, rtgWorld.simplex, 62f);
        }
    }
}