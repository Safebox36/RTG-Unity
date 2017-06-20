namespace rtg.world.gen.terrain
{
    using rtg.api.world;

    /**
     * @author Zeno410
     */
    public class GroundEffect : HeightEffect
    {

        // the standard ground effect
        private readonly float amplitude;

        public GroundEffect(float amplitude)
        {

            this.amplitude = amplitude;
        }

        override public float added(RTGWorld rtgWorld, float x, float y)
        {

            return TerrainBase.groundNoise(x, y, amplitude, rtgWorld.simplex);
        }
    }
}