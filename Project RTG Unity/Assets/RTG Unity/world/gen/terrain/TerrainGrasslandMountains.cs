namespace rtg.world.gen.terrain
{
    using rtg.api.world;

    public class TerrainGrasslandMountains : TerrainBase
    {

        public TerrainGrasslandMountains()
        {

        }

        override public float generateNoise(RTGWorld rtgWorld, int x, int y, float border, float river)
        {
            return terrainGrasslandMountains(x, y, rtgWorld.simplex, rtgWorld.cell, river, 7f, 120f, 68f);
        }
    }
}