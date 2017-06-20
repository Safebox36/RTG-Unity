namespace rtg.world.gen.terrain
{
    using rtg.api.world;

    /**
     * @author Zeno410
     */
    public class FunctionalTerrainBase : TerrainBase
    {

        protected HeightEffect height;

        override public float generateNoise(RTGWorld rtgWorld, int x, int y, float border, float river)
        {

            return riverized(height.added(rtgWorld, x, y) + _base, river);
        }
    }
}