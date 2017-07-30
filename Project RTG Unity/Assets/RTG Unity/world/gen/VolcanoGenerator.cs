namespace rtg.world.gen
{
    using System;
    using System.Collections.Generic;

    //import net.minecraft.util.math.ChunkPos;
    using generic.util.math;
    //import net.minecraft.world.World;
    using generic.world;
    //import net.minecraft.world.biome.Biome;
    using generic.world.biome;
    //import net.minecraft.world.chunk.ChunkPrimer;
    using generic.world.chunk;

    using rtg.api;
    using rtg.api.config;
    using rtg.api.util.noise;
    using rtg.world.biome;
    using rtg.world.biome.realistic;
    using rtg.world.gen.feature;

    /**
     *
     * @author Zeno410
     */
    public class VolcanoGenerator
    {

        private Random mapRand;
        private long seed;
        protected RTGConfig rtgConfig = RTGAPI.config();
        private static long l;
        private static long l1;

        private static List<ChunkPos> noVolcano = new List<ChunkPos>(2000);

        public VolcanoGenerator(long seed)
        {
            this.seed = seed;
            mapRand = new Random((int)this.seed);
            l = (mapRand.Next() / 2L) * 2L + 1L;
            l1 = (mapRand.Next() / 2L) * 2L + 1L;
        }
        public void generateMapGen(Chunk primer, long unusedSeed, World world, IBiomeProviderRTG cmr, Random unusedMapRand, int chunkX, int chunkY, OpenSimplexNoise simplex, CellNoise cell, float[] noise)
        {

            // Have volcanoes been disabled in the global config?
            if (!rtgConfig.ENABLE_VOLCANOES) return;

            int mapGenRadius = 5;
            int volcanoGenRadius = 15;

            //if (!seed.equals(currentSeed)) {
            //currentSeed = seed;
            //}


            // Volcanoes generation
            for (int baseX = chunkX - volcanoGenRadius; baseX <= chunkX + volcanoGenRadius; baseX++)
            {
                for (int baseY = chunkY - volcanoGenRadius; baseY <= chunkY + volcanoGenRadius; baseY++)
                {
                    ChunkPos probe = new ChunkPos(baseX, baseY);
                    if (noVolcano.Contains(probe)) continue;
                    noVolcano.Add(probe);
                    mapRand = new Random((int)((long)baseX * l + (long)baseY * l1 ^ seed));
                    rMapVolcanoes(primer, world, cmr, baseX, baseY, chunkX, chunkY, simplex, cell, noise);
                }
            }
        }

        public void rMapVolcanoes(Chunk primer, World world, IBiomeProviderRTG cmr, int baseX, int baseY, int chunkX, int chunkY, OpenSimplexNoise simplex, CellNoise cell, float[] noise)
        {

            // Have volcanoes been disabled in the global config?
            if (!rtgConfig.ENABLE_VOLCANOES) return;

            // Let's go ahead and generate the volcano. Exciting!!! :D
            if (baseX % 4 == 0 && baseY % 4 == 0)
            {
                int biomeId = cmr.getBiomeGenAt(baseX * 16, baseY * 16).getBiomeID();
                RealisticBiomeBase realisticBiome = RealisticBiomeBase.getBiome(biomeId);

                // Do we need to patch the biome?
                if (realisticBiome == null)
                {
                    RealisticBiomePatcher biomePatcher = new RealisticBiomePatcher();
                    realisticBiome = biomePatcher.getPatchedRealisticBiome(
                        "NULL biome found when mapping volcanoes.");
                }
                if (!realisticBiome.getConfig().ALLOW_VOLCANOES) return;
                // Have volcanoes been disabled via frequency?
                // Use the global frequency unless the biome frequency has been explicitly set.
                int chance = realisticBiome.getConfig().VOLCANO_CHANCE == -1 ? rtgConfig.VOLCANO_CHANCE : realisticBiome.getConfig().VOLCANO_CHANCE;
                if (chance < 1) return;
                if (mapRand.Next(chance) > 0) return;

                float river = cmr.getRiverStrength(baseX * 16, baseY * 16) + 1f;
                if (river > 0.98f && cmr.isBorderlessAt(baseX * 16, baseY * 16))
                {

                    // we have to pull it out of noVolcano. We do it this way to avoid having to make a ChunkPos twice
                    ChunkPos probe = new ChunkPos(baseX, baseY);
                    noVolcano.Remove(probe);

                    long i1 = mapRand.Next() / 2L * 2L + 1L;
                    long j1 = mapRand.Next() / 2L * 2L + 1L;
                    mapRand = new Random((int)((long)chunkX * i1 + (long)chunkY * j1 ^ world.getSeed()));

                    WorldGenVolcano.build(primer, world, mapRand, baseX, baseY, chunkX, chunkY, simplex, cell, noise);
                }
            }
        }
    }
}