namespace rtg.world.gen.terrain
{
    using rtg.api.world;

    /**
     * @author Zeno410
     */
    public class PlateauEffect : HeightEffect
    {
        // similar to HillockEffect except that the transition is smooth
        // and it can pass through a subordinate effect after multiply by the BlendedHill noise

        // not going to bother to set up a creator shell to make sure everything is set
        // set defaults to absurd values to crash if they're not set
        // a trio of parameters frequently used together
        public float height = int.MaxValue;
        public float wavelength = 0;
        public float bottomSimplexValue = int.MaxValue;// normal range is -1 to 1;
                                                       //usually numbers above 0 are often preferred to avoid dead basins
        public float topSimplexValue = int.MaxValue;
        public int octave;
        public HeightEffect subordinate;

        override public float added(RTGWorld rtgWorld, float x, float y)
        {
            float noise = (float)rtgWorld.simplex.octaves[octave].Evaluate(x / wavelength, y / wavelength);
            if (noise > topSimplexValue)
            {
                noise = 1f;
            }
            else if (noise < bottomSimplexValue)
            {
                noise = 0f;
            }
            else
            {
                noise = (noise - bottomSimplexValue) / (topSimplexValue - bottomSimplexValue);
            }
            if (subordinate == null)
            {
                return noise * height;
            }
            float added = subordinate.added(rtgWorld, x, y);
            return noise * (height + added);
        }
    }
}