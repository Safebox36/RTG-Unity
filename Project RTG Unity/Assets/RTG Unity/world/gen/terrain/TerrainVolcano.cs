namespace rtg.world.gen.terrain
{
    using rtg.api.world;

    public class TerrainVolcano : TerrainBase
    {

        public TerrainVolcano()
        {

        }

        override public float generateNoise(RTGWorld rtgWorld, int x, int y, float border, float river)
        {
            return terrainVolcano(x, y, rtgWorld.simplex, rtgWorld.cell, border, 70f);
        }
    }
}