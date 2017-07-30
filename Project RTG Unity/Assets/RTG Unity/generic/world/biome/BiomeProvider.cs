namespace generic.world.biome
{
    using System;
    using System.Collections.Generic;
    using generic.util.math;
    using UnityEngine;

    public class BiomeProvider
    {
        Texture2D biomeMap;

        public BiomeProvider() : this(new Texture2D(16,16))
        {

        }

        public BiomeProvider(Texture2D biomeMap)
        {

        }

        virtual public int[] getBiomesGens(int x, int z, int par3, int par4)
        {
            return null;
        }

        virtual public float getRiverStrength(int x, int z)
        {
            return 0f;
        }
        
        virtual public Biome getBiomeGenAt(int x, int z)
        {
            return null;
        }

        virtual public bool isBorderlessAt(int x, int z)
        {
            return false;
        }

        virtual public List<Biome> getBiomesToSpawnIn()
        {
            return null;
        }

        virtual public float getTemperatureAtHeight(float p_76939_1_, int p_76939_2_)
        {
            return 0f;
        }

        virtual public Biome[] getBiomesForGeneration(Biome[] biomes, int x, int z, int width, int height)
        {
            return null;
        }

        virtual public bool areBiomesViable(int x, int z, int radius, List<Biome> allowed)
        {
            return false;
        }

        virtual public PixelPos findBiomePosition(int x, int z, int range, List<Biome> biomes, System.Random random)
        {
            return null;
        }

        virtual public Biome getBiome(PixelPos atPos)
        {
        return null;
        }
    }
}