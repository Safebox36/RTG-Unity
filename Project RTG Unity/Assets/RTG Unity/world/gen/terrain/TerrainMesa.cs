namespace rtg.world.gen.terrain
{
    using rtg.api.world;

    public class TerrainMesa : TerrainBase
    {

        public TerrainMesa()
        {

        }

        override public float generateNoise(RTGWorld rtgWorld, int x, int y, float border, float river)
        {
            return terrainMesa(x, y, rtgWorld.simplex, river, border);
        }
    }
}