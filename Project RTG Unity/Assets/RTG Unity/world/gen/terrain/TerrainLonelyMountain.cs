namespace rtg.world.gen.terrain
{
    using rtg.api.world;

    public class TerrainLonelyMountain : TerrainBase
    {

        private float width;
        private float strength;

        public TerrainLonelyMountain(float mountainWidth, float mountainStrength, float height)
        {

            width = mountainWidth;
            strength = mountainStrength;
            _base = height;
        }

        override public float generateNoise(RTGWorld rtgWorld, int x, int y, float border, float river)
        {

            return terrainLonelyMountain(x, y, rtgWorld.simplex, rtgWorld.cell, river, strength, width, _base);
        }
    }
}