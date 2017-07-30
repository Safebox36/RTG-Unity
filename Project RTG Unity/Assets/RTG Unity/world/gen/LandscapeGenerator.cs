namespace rtg.world.gen
{
    //import net.minecraft.util.math.ChunkPos;
    using generic.util.math;
    //import net.minecraft.world.biome.Biome;
    using generic.world.biome;

    using rtg.api.util.noise;
    using rtg.api.world;
    using rtg.world.biome;
    using rtg.world.biome.realistic;

    using System;
    using System.Collections.Generic;

    /**
     *
     * @author Zeno410
     */
    class LandscapeGenerator
    {
        private readonly int sampleSize = 8;
        private readonly int sampleArraySize;
        private readonly int[] biomeData;
        private float[,] weightings;
        private readonly RTGWorld rtgWorld;
        private readonly OpenSimplexNoise simplex;
        private readonly CellNoise cell;
        private float[] weightedBiomes = new float[256];
        private BiomeAnalyzer analyzer = new BiomeAnalyzer();
        private Dictionary<ChunkPos, ChunkLandscape> storage = new Dictionary<ChunkPos, ChunkLandscape>(60 * 1000);
        private RealisticBiomePatcher biomePatcher = new RealisticBiomePatcher();

        public LandscapeGenerator(RTGWorld rtgWorld)
        {
            this.rtgWorld = rtgWorld;
            sampleArraySize = sampleSize * 2 + 5;
            biomeData = new int[sampleArraySize * sampleArraySize];
            this.simplex = rtgWorld.simplex;
            this.cell = rtgWorld.cell;
            setWeightings();
        }

        private void setWeightings()
        {
            weightings = new float[sampleArraySize * sampleArraySize, 256];
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    float limit = (float)Math.Pow((56f * 56f), .7);
                    // float limit = 56f;

                    for (int mapX = 0; mapX < sampleArraySize; mapX++)
                    {
                        for (int mapZ = 0; mapZ < sampleArraySize; mapZ++)
                        {
                            float xDist = (i - chunkCoordinate(mapX));
                            float zDist = (j - chunkCoordinate(mapZ));
                            float distanceSquared = xDist * xDist + zDist * zDist;
                            //float distance = (float)Math.sqrt(distanceSquared);
                            float distance = (float)Math.Pow(distanceSquared, .7);
                            float weight = 1f - distance / limit;
                            if (weight < 0) weight = 0;
                            weightings[mapX * sampleArraySize + mapZ, i * 16 + j] = weight;
                        }
                    }
                }
            }
        }

        private int chunkCoordinate(int biomeMapCoordinate)
        {
            return (biomeMapCoordinate - sampleSize) * 8;
        }

        int getBiomeDataAt(IBiomeProviderRTG cmr, int cx, int cz)
        {
            int cx2 = cx & 15;
            int cz2 = cz & 15;
            ChunkLandscape target = this.landscape(cmr, cx - cx2, cz - cz2);
            return target.biome[cx2 * 16 + cz2].baseBiome.getBiomeID();
        }

        /*
         * All of the 'cx' and 'cz' parameters have been flipped when passing them.
         * Prior to flipping, the terrain was being XZ-chunk-flipped. - WhichOnesPink
         */
        ChunkLandscape landscape(IBiomeProviderRTG cmr, int cx, int cz)
        {
            ChunkPos chunkPos = new ChunkPos(cx, cz);
            ChunkLandscape preExisting = this.storage[chunkPos];
            if (preExisting != null) return preExisting;
            ChunkLandscape result = new ChunkLandscape();
            getNewerNoise(cmr, cx, cz, result);
            int[] biomeIndices = cmr.getBiomesGens(cx, cz, 16, 16);
            analyzer.newRepair(biomeIndices, result.biome, this.biomeData, this.sampleSize, result.noise, result.river);
            //-cmr.getRiverStrength(cx * 16 + 7, cy * 16 + 7));
            storage[chunkPos] = result;
            return result;
        }

        private void getNewerNoise(IBiomeProviderRTG cmr, int cx, int cz, ChunkLandscape landscape)
        {
            // get area biome map

            for (int i = -sampleSize; i < sampleSize + 5; i++)
            {
                for (int j = -sampleSize; j < sampleSize + 5; j++)
                {
                    biomeData[(i + sampleSize) * sampleArraySize + (j + sampleSize)] =
                    cmr.getBiomeDataAt(cx + ((i * 8)), cz + ((j * 8))).baseBiome.getBiomeID();
                }
            }

            float river;

            // fill the old smallRender
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    float totalWeight = 0;
                    for (int mapX = 0; mapX < sampleArraySize; mapX++)
                    {
                        for (int mapZ = 0; mapZ < sampleArraySize; mapZ++)
                        {
                            float weight = weightings[mapX * sampleArraySize + mapZ, i * 16 + j];
                            if (weight > 0)
                            {
                                totalWeight += weight;
                                weightedBiomes[biomeData[mapX * sampleArraySize + mapZ]] += weight;
                            }
                        }
                    }
                    // normalize biome weights
                    for (int biomeIndex = 0; biomeIndex < weightedBiomes.Length; biomeIndex++)
                    {
                        weightedBiomes[biomeIndex] /= totalWeight;
                    }

                    landscape.noise[i * 16 + j] = 0f;

                    river = cmr.getRiverStrength(cx + i, cz + j);
                    landscape.river[i * 16 + j] = -river;
                    float totalBorder = 0f;

                    for (int k = 0; k < 256; k++)
                    {
                        if (weightedBiomes[k] > 0f)
                        {
                            totalBorder += weightedBiomes[k];
                            RealisticBiomeBase realisticBiome = RealisticBiomeBase.getBiome(k);

                            // Do we need to patch the biome?
                            if (realisticBiome == null)
                            {
                                realisticBiome = biomePatcher.getPatchedRealisticBiome(
                                    "NULL biome (" + k + ") found when getting newer noise.");
                            }

                            landscape.noise[i * 16 + j] += realisticBiome.rNoise(this.rtgWorld, cx + i, cz + j, weightedBiomes[k], river + 1f) * weightedBiomes[k];

                            // 0 for the next column
                            weightedBiomes[k] = 0f;
                        }
                    }
                    if (totalBorder < .999 || totalBorder > 1.001) throw new Exception("" + totalBorder);
                }
            }

            //fill biomes array with biomeData

            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    landscape.biome[i * 16 + j] = cmr.getBiomeDataAt(cx + (((i - 7) * 8 + 4)), cz + (((j - 7) * 8 + 4)));
                }
            }
        }
    }
}