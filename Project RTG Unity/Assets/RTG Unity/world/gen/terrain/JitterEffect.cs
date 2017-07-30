namespace rtg.world.gen.terrain
{
    using rtg.api.util.noise;
    using rtg.api.world;
    using System;

    /**
     * This class returns a height effect with a jitter on the position.
     *
     * @author Zeno410
     */
    public class JitterEffect : HeightEffect
    {

        public float amplitude = int.MaxValue;
        public float wavelength = 0;
        public HeightEffect jittered;
        private SimplexOctave.Disk jitter = new SimplexOctave.Disk();
        private bool running = false;// this is to check for re-entrancy because this isn't re-entrant

        public JitterEffect()
        {

        }

        public JitterEffect(float amplitude, float wavelength, HeightEffect toJitter)
        {

            this.amplitude = amplitude;
            this.wavelength = wavelength;
            this.jittered = toJitter;
        }

        override public float added(RTGWorld rtgWorld, float x, float y)
        {

            running = true;
            rtgWorld.simplex.riverJitter().evaluateNoise((float)x / wavelength, (float)y / wavelength, jitter);
            int pX = (int)Math.Round(x + jitter.deltax() * amplitude);
            int pY = (int)Math.Round(y + jitter.deltay() * amplitude);
            running = false;
            return jittered.added(rtgWorld, pX, pY);
        }
    }
}