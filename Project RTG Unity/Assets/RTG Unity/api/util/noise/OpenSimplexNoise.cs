/**
 * A shell to contain multiple instances of OpenSimplexNoise and masquerade as the first instance
 * Names are currently odd for compatibility - this should eventually be called "suite" and
 * SimplexOctave should be OpenSimplexNoise
 */

namespace rtg.api.util.noise
{

    /**
     * @author Zeno410
     * @version $Revision: 1.3$
     * @see https://gist.github.com/KdotJPG/b1270127455a94ac5d19
     */
    public class OpenSimplexNoise : SimplexOctave
    {

        private readonly SimplexOctave[] octaves;
        private static readonly int OCTAVE_COUNT = 10;  // perhaps should be a variable
                                                        // but that creates coordination issues


        public OpenSimplexNoise(long seed)
        {
            octaves = new SimplexOctave[OCTAVE_COUNT];

            for (int i = 0; i < OCTAVE_COUNT; i++)
            {
                octaves[i] = new SimplexOctave(seed + i);
            }
        }

        /*
         * Aliases
         */

        //Alias for 1D
        public float noise1(float x)
        {
            return (float)octaves[0].Evaluate(x, 0.5f);
        }

        //Alias for 2D
        public float noise2(float x, float y)
        {
            return (float)octaves[0].Evaluate(x, y);
        }

        //Alias for 3D
        public float noise3(float x, float y, float z)
        {
            return (float)octaves[0].Evaluate(x, y, z);
        }

        //Alias for 3D (again)
        public double improvedNoise(double x, double y, double z)
        {
            return octaves[0].Evaluate(x, y, z);
        }

        /*
         * Standard functions
         */

        //2D OpenSimplex noise (KdotJPG)
        public double noise(double x, double y)
        {
            return (float)octaves[0].Evaluate(x, y);
        }

        //3D OpenSimplex Noise (DigitalShadow)
        public double noise(double x, double y, double z)
        {
            return octaves[0].Evaluate(x, y, z);
        }

        public SimplexOctave octave(int index)
        {
            return octaves[index];
        }
        public SimplexOctave mountain() { return octaves[0]; }

        public SimplexOctave riverJitter() { return octaves[1]; }

    }
}