using System;

namespace rtg.api.util.noise
{

    /**
     *
     * @author Zeno410
     */
    public class VoronoiCellNoise : CellNoise
    {

        private VoronoiCellOctave[] octaves = new VoronoiCellOctave[5];
        public readonly int OCTAVE_COUNT = 5;

        public VoronoiCellNoise(long seed)
        {
            for (int i = 0; i < 5; i++)
            {
                octaves[i] = new VoronoiCellOctave(seed + i, (short)0, true);
            }
        }

        public VoronoiCellOctave octave(int index)
        {
            return octaves[index];
        }

        public VoronoiCellOctave river() { return octave(0); }

        public float noise(double x, double z, double depth)
        {
            return river().noise(x, z, depth);
        }

        CellOctave CellNoise.octave(int index)
        {
            throw new NotImplementedException();
        }

        CellOctave CellNoise.river()
        {
            throw new NotImplementedException();
        }
    }
}