namespace rtg.world.gen.terrain
{
    using rtg.api.world;

    /**
     * // just adds a ant increase
     *
     * @author Zeno410
     */
    public class RaiseEffect : HeightEffect
    {
        // just adds a number
        public readonly float height;

        public RaiseEffect(float height)
        {
            this.height = height;
        }

        override public float added(RTGWorld rtgWorld, float x, float y)
        {
            return height;
        }
    }
}