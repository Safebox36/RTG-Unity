namespace rtg.world.gen.terrain
{
    using rtg.api.world;

    /**
     * @author Zeno410
     */
    public class HeightVariation : HeightEffect
    {

        // not going to bother to set up a creator shell to make sure everything is set
        // set defaults to absurd values to crash if they're not set
        public float height = int.MaxValue;
        public float wavelength = 0;
        public int octave = -1;

        override public float added(RTGWorld rtgWorld, float x, float y)
        {

            return rtgWorld.simplex.octave(octave).noise2(x / wavelength, y / wavelength) * height;
        }
    }
}