namespace rtg.world.gen.terrain
{
    using rtg.api.world;

    /**
     * This creates an effect of scattered hills with irregular surfaces
     *
     * @author Zeno410
     */
    public class BumpyHillsEffect : HeightEffect
    {

        // not going to bother to set up a creator shell to make sure everything is set
        // set defaults to absurd values to crash if they're not set
        // a trio of parameters frequently used together
        public float hillHeight = int.MaxValue;
        public float hillWavelength = 0;
        public float spikeHeight = int.MaxValue;
        public float spikeWavelength = 0;


        // octaves are standardized so they don't need to be set
        public int hillOctave = 0;//
        public int spikeOctave = 2;//

        override public float added(RTGWorld rtgWorld, float x, float y)
        {
            float noise = rtgWorld.simplex.octave(hillOctave).noise2(x / hillWavelength, y / hillWavelength);
            noise = TerrainBase.blendedHillHeight(noise);
            float spikeNoise = rtgWorld.simplex.octave(spikeOctave).noise2(x / spikeWavelength, y / spikeWavelength);
            spikeNoise = TerrainBase.blendedHillHeight(spikeNoise * noise);
            return noise * hillHeight + spikeNoise * spikeHeight;
        }
    }
}