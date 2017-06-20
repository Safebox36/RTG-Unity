namespace rtg.world.gen.terrain
{
    using rtg.api.world;

    public class TerrainPlains : TerrainBase
    {

        public TerrainPlains()
        {

        }

        override public float generateNoise(RTGWorld rtgWorld, int x, int y, float border, float river)
        {
            return terrainPlains(x, y, rtgWorld.simplex, river, 160f, 10f, 60f, 200f, 66f);
        }
    }
}