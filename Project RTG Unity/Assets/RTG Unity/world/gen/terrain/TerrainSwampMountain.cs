namespace rtg.world.gen.terrain
{
    using rtg.api.world;

    public class TerrainSwampMountain : TerrainBase
    {

        private float heigth;
        private float width;

        public TerrainSwampMountain(float mountainHeight, float mountainWidth)
        {
            heigth = mountainHeight;
            width = mountainWidth;
        }

        override public float generateNoise(RTGWorld rtgWorld, int x, int y, float border, float river)
        {
            return terrainSwampMountain(x, y, rtgWorld.simplex, rtgWorld.cell, river, width, heigth, 150f, 32f, 56f);
        }
    }
}