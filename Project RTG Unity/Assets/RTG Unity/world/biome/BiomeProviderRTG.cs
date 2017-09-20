namespace rtg.world.biome
{
    using System.Collections.Generic;
    using System;

    using generic.util.math;
    using generic.world;
    using generic.world.biome;
    using generic.world.gen.layer;


    using rtg.api;
    using rtg.api.util.noise;
    using rtg.api.world;
    using rtg.world.biome.realistic;

    using UnityEngine;

    public class BiomeProviderRTG : BiomeProvider, IBiomeProviderRTG
    {
        private static int[] incidences = new int[100];
        private static int references = 0;
        /** A GenLayer containing the indices into Biome.biomeList[] */
        private GenLayer genBiomes;
        private GenLayer biomeIndexLayer;
        private List<Biome> biomesToSpawnIn;
        private RTGWorld rtgWorld;
        private OpenSimplexNoise simplex;
        private CellNoise cell;
        //private SimplexCellularNoise simplexCell;
        private VoronoiCellNoise river;
        private float[] borderNoise;
        private RealisticBiomePatcher biomePatcher;
        private double riverValleyLevel = 60.0 / 450.0;//60.0/450.0;
        private float riverSeparation = 1875;
        private float largeBendSize = 140;
        private float smallBendSize = 30;

        public BiomeProviderRTG(Texture2D world) : base(world)
        {

            //this.rtgWorld = new RTGWorld(world);
            this.biomesToSpawnIn = new List<Biome>();
            this.borderNoise = new float[256];
            this.biomePatcher = new RealisticBiomePatcher();
            this.riverSeparation /= RTGAPI.config().RIVER_FREQUENCY_MULTIPLIER;
            this.riverValleyLevel *= RTGAPI.config().riverSizeMultiplier();
            this.largeBendSize *= RTGAPI.config().RIVER_BENDINESS_MULTIPLIER;
            this.smallBendSize *= RTGAPI.config().RIVER_BENDINESS_MULTIPLIER;

            long seed = this.rtgWorld.world.getSeed();

            simplex = new OpenSimplexNoise(seed);
            cell = new SimplexCellularNoise(seed);
            //simplexCell = new SimplexCellularNoise(seed);
            river = new VoronoiCellNoise(seed);
            testCellBorder();
        }

        private static void testCellBorder()
        {
            double[] result = new double[2];
            result[0] = 0.5;
            result[1] = 1;
            if (cellBorder(result, 0.5, 1) < 0) throw new Exception();
        }

        private static double cellBorder(double[] results, double width, double depth)
        {
            double c = (results[1] - results[0]) / results[1];
            if (c < 0) throw new Exception();
            /*
                int slot = (int)Math.floor(c*100.0);
                incidences[slot] += 1;
                references ++;
                if (references>40000) {
                    String result = "";
                    for (int i = 0; i< 100; i ++) {
                        result += " " + incidences[i];
                    }
                    throw new RuntimeException(result);
                }
            */
            if (c < width) return ((c / width) - 1f) * depth;
            else return 0;
        }

        override public int[] getBiomesGens(int x, int z, int par3, int par4)
        {

            int[] d = new int[par3 * par4];

            for (int i = 0; i < par3; i++)
            {
                for (int j = 0; j < par4; j++)
                {
                    d[i * par3 + j] = getBiomeGenAt(x + i, z + j).getBiomeID();
                }
            }
            return d;
        }

        override public float getRiverStrength(int x, int z)
        {
            //New river curve function. No longer creates worldwide curve correlations along cardinal axes.
            SimplexOctave.Disk jitter = new SimplexOctave.Disk();
            simplex.riverJitter().evaluateNoise((float)x / 240.0, (float)z / 240.0, jitter);
            double pX = x + jitter.deltax() * largeBendSize;
            double pZ = z + jitter.deltay() * largeBendSize;

            simplex.octave(2).evaluateNoise((float)x / 80.0, (float)z / 80.0, jitter);
            pX += jitter.deltax() * smallBendSize;
            pZ += jitter.deltay() * smallBendSize;

            double xRiver = pX / riverSeparation;
            double zRiver = pZ / riverSeparation;

            //New cellular noise.
            //TODO move the initialization of the results in a way that's more efficient but still thread safe.
            //double[] results = cell.river().eval(xRiver,zRiver );
            //return (float) cellBorder(results, riverValleyLevel, 1.0);
            return river.octave(0).border2(xRiver, zRiver, riverValleyLevel, 1f);
        }

        /**
         * @see IBiomeProviderRTG
         */
        override public Biome getBiomeGenAt(int x, int z)
        {
            return this.getBiome(new PixelPos(x, 0, z));
        }

        /**
         * @see IBiomeProviderRTG
         */
        //Can't override for some reason, shouldn't matter too much at the moment.
        public RealisticBiomeBase getBiomeDataAt(int par1, int par2)
        {
            /*long coords = ChunkCoordIntPair.chunkXZ2Int(par1, par2);
            if (biomeDataMap.containsKey(coords)) {
                return biomeDataMap.get(coords);
            }*/
            RealisticBiomeBase output;

            output = RealisticBiomeBase.getBiome(this.getBiomeGenAt(par1, par2).getBiomeID());
            if (output == null) output = biomePatcher.getPatchedRealisticBiome("No biome " + par1 + " " + par2);

            /*if (biomeDataMap.size() > 4096) {
                biomeDataMap.clear();
            }
            biomeDataMap.put(coords, output);*/

            return output;
        }

        /**
         * @see IBiomeProviderRTG
         */
        override public bool isBorderlessAt(int x, int z)
        {

            int bx, bz;
            for (bx = -2; bx <= 2; bx++)
            {
                for (bz = -2; bz <= 2; bz++)
                {
                    borderNoise[getBiomeDataAt(x + bx * 16, z + bz * 16).baseBiome.getBiomeID()] += 0.04f;
                }
            }

            bz = 0;
            for (bx = 0; bx < 256; bx++)
            {
                if (borderNoise[bx] > 0.98f) bz = 1;
                borderNoise[bx] = 0;
            }
            return bz == 1;
        }

        public bool diff(float sample1, float sample2, float _base)
        {
            return (sample1 < _base && sample2 > _base) || (sample1 > _base && sample2 < _base);
        }

        public float[] getRainfall(float[] par1ArrayOfFloat, int par2, int par3, int par4, int par5)
        {
            if (par1ArrayOfFloat == null || par1ArrayOfFloat.Length < par4 * par5)
            {
                par1ArrayOfFloat = new float[par4 * par5];
            }

            int[] aint = this.biomeIndexLayer.getInts(par2, par3, par4, par5);

            for (int i1 = 0; i1 < par4 * par5; ++i1)
            {
                float f = 0;
                int biome = aint[i1];

                try
                {
                    if (biome > 255) throw new Exception(biomeIndexLayer.ToString());
                    f = RealisticBiomeBase.getBiome(biome).baseBiome.getRainfall() / 65536.0F;
                }
                catch (Exception e)
                {
                    if (biome > 255) throw new Exception(biomeIndexLayer.ToString());
                    if (RealisticBiomeBase.getBiome(biome) == null)
                    {
                        f = biomePatcher.getPatchedRealisticBiome("Problem with biome " + biome + " from " +
                            e.Message).baseBiome.getRainfall() / 65536.0F;
                    }
                }
                if (f > 1.0F) f = 1.0F;
                par1ArrayOfFloat[i1] = f;
            }
            return par1ArrayOfFloat;
        }

        override public List<Biome> getBiomesToSpawnIn()
        {
            return this.biomesToSpawnIn;
        }

        override public float getTemperatureAtHeight(float p_76939_1_, int p_76939_2_)
        {
            return p_76939_1_;
        }

        override public Biome[] getBiomesForGeneration(Biome[] biomes, int x, int z, int width, int height)
        {
            if (biomes.Length < width * height)
            {
                biomes = new Biome[width * height];
            }

            int[] aint = this.genBiomes.getInts(x, z, width, height);

            for (int i1 = 0; i1 < width * height; ++i1)
            {
                biomes[i1] = Biome.getBiome(aint[i1]);

                if (biomes[i1] == null)
                {
                    biomes[i1] = biomePatcher.getPatchedBaseBiome(
                        "BPRTG.getBiomesForGeneration() could not find biome " + aint[i1]);
                }
            }
            return biomes;
        }

        override public bool areBiomesViable(int x, int z, int radius, List<Biome> allowed)
        {

            float centerNoise = getNoiseAt(x, z);
            if (centerNoise < 62) return false;

            float lowestNoise = centerNoise;
            float highestNoise = centerNoise;
            for (int i = -2; i <= 2; i++)
            {
                for (int j = -2; j <= 2; j++)
                {
                    if (i != 0 && j != 0)
                    {
                        float n = getNoiseAt(x + i * 16, z + j * 16);
                        if (n < lowestNoise) lowestNoise = n;
                        if (n > highestNoise) highestNoise = n;
                    }
                }
            }
            return highestNoise - lowestNoise < 22;
        }

        public float getNoiseAt(int x, int y)
        {

            float river = getRiverStrength(x, y) + 1f;
            if (river < 0.5f) return 59f;
            return getBiomeDataAt(x, y).rNoise(this.rtgWorld, x, y, 1f, river);
        }

        override public PixelPos findBiomePosition(int x, int z, int range, List<Biome> biomes, System.Random random)
        {
            int i = x - range >> 2;
            int j = z - range >> 2;
            int k = x + range >> 2;
            int l = z + range >> 2;
            int i1 = k - i + 1;
            int j1 = l - j + 1;
            int[] aint = this.genBiomes.getInts(i, j, i1, j1);
            PixelPos blockpos = null;
            int k1 = 0;

            for (int l1 = 0; l1 < i1 * j1; ++l1)
            {
                int i2 = i + l1 % i1 << 2;
                int j2 = j + l1 / i1 << 2;
                Biome biome = Biome.getBiome(aint[l1]);

                if (biomes.Contains(biome) && (blockpos == null || random.Next(k1 + 1) == 0))
                {
                    blockpos = new PixelPos(i2, 0, j2);
                    ++k1;
                }
            }
            return blockpos;
        }
    }
}