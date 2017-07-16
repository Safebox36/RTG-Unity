namespace rtg.world.gen.terrain
{
    using rtg.api.world;
    using System;

    /**
     * @author Zeno410
     */
    public class LonelyMountainEffect : HeightEffect
    {

        // not going to bother to set up a creator shell to make sure everything is set
        // set defaults to absurd values to crash if they're not set
        // a trio of parameters frequently used together
        public float mountainHeight = int.MaxValue;
        public float mountainWavelength = 0;
        public float spikeHeight = int.MaxValue;
        public float spikeWavelength = 0;
        // octaves are standardized so they don't need to be set
        public int hillOctave = 0;//
        public int spikeOctave = 2;//
        private float adjustedBottom = TerrainBase.blendedHillHeight(0, 0f);

        override public float added(RTGWorld rtgWorld, float x, float y)
        {

            float noise = rtgWorld.simplex.octave(hillOctave).noise2(x / mountainWavelength, y / mountainWavelength);
            noise = Math.Abs(noise);
            noise = TerrainBase.blendedHillHeight(noise, 0f);
            //transform to be more mountainous
            noise = TerrainBase.unsignedPower(noise, 1.7f);
            noise = 1f - (1f - noise) / (1f - adjustedBottom);
            float spikeNoise = rtgWorld.simplex.octave(spikeOctave).noise2((float)x / spikeWavelength, (float)y / spikeWavelength);
            spikeNoise = Math.Abs(noise);
            spikeNoise = TerrainBase.blendedHillHeight(noise, 0f);
            spikeNoise *= noise;
            return noise * mountainHeight + spikeNoise * spikeHeight;
        }
    }
}