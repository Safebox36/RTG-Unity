namespace rtg.world.biome.realistic
{
    //import net.minecraft.world.biome.Biome;
    using generic.world.biome;

    using rtg.api;
    using rtg.api.config;

    using System;

    public class RealisticBiomePatcher
    {

        private int patchBiomeId;
        private RealisticBiomeBase realisticBiome;
        private Biome baseBiome;
        private RTGConfig rtgConfig = RTGAPI.config();

        public RealisticBiomePatcher()
        {

            this.patchBiomeId = rtgConfig.PATCH_BIOME_ID;

            if (this.patchBiomeId > -1)
            {

                try
                {
                    this.realisticBiome = RealisticBiomeBase.getBiome(this.patchBiomeId);
                }
                catch (Exception e)
                {
                    throw new Exception("Realistic patch biome " + this.patchBiomeId + " not found. Please make sure this biome is enabled.");
                }

                try
                {
                    this.baseBiome = realisticBiome.baseBiome;
                }
                catch (Exception e)
                {
                    throw new Exception("Base patch biome " + this.patchBiomeId + " not found. Please make sure this biome is enabled.");
                }
            }
        }

        public RealisticBiomeBase getPatchedRealisticBiome(string exceptionMessage)
        {

            if (this.patchBiomeId < 0)
            {
                throw new Exception(exceptionMessage);
            }
            else
            {

                if (this.realisticBiome == null)
                {
                    throw new Exception("Problem patching realistic biome.");
                }

                return this.realisticBiome;
            }
        }

        public Biome getPatchedBaseBiome(string exceptionMessage)
        {

            if (this.patchBiomeId < 0)
            {
                throw new Exception(exceptionMessage);
            }
            else
            {

                if (this.baseBiome == null)
                {
                    throw new Exception("Problem patching base biome.");
                }

                return this.baseBiome;
            }
        }
    }
}