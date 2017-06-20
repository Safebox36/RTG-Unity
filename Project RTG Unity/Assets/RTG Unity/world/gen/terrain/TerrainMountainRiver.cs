namespace rtg.world.gen.terrain
{
    using rtg.api.world;

    public class TerrainMountainRiver : TerrainBase
    {

        public TerrainMountainRiver()
        {

        }

        override public float generateNoise(RTGWorld rtgWorld, int x, int y, float border, float river)
        {
            return terrainMountainRiver(x, y, rtgWorld.simplex, rtgWorld.cell, river, 300f, 67f);
        }
    }
}