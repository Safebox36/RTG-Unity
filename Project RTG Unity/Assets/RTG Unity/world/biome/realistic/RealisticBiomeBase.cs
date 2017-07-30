namespace rtg.world.biome.realistic
{
    using System.Collections.Generic;
    using System;

    //import net.minecraft.Pixel.PixelLeaves;
    //import net.minecraft.Pixel.state.Pixel;
    using generic.pixel;
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
    using rtg.api.world;
    using rtg.util;
    using rtg.world.biome;
    using rtg.world.gen.feature;
    //using rtg.world.gen.feature.tree.rtg.TreeRTG;
    using rtg.world.gen.surface;
    using rtg.world.gen.terrain;

    public abstract class RealisticBiomeBase
    {

        protected RTGConfig rtgConfig = RTGAPI.config();
        private static readonly RealisticBiomeBase[] arrRealisticBiomeIds = new RealisticBiomeBase[256];

        public readonly Biome baseBiome;
        public readonly Biome riverBiome;
        public readonly Biome _beachBiome;
        public BiomeConfig config;
        public TerrainBase terrain;
        public SurfaceBase surface;
        public SurfaceBase surfaceRiver;
        public SurfaceBase surfaceGeneric;

        public int waterSurfaceLakeChance; //Lower = more frequent
        public int lavaSurfaceLakeChance; //Lower = more frequent

        public int waterUndergroundLakeChance; //Lower = more frequent
        public int lavaUndergroundLakeChance; //Lower = more frequent

        public bool generateVillages;

        public bool generatesEmeralds;
        public bool generatesSilverfish;
        
        //public ArrayList<TreeRTG> rtgTrees;

        // lake calculations

        private float lakeInterval = 989.0f;
        private float lakeShoreLevel = 0.15f;
        private float lakeWaterLevel = 0.11f;// the lakeStrength below which things should be below water
        private float lakeDepressionLevel = 0.30f;// the lakeStrength below which land should start to be lowered
        public bool noLakes = false;
        public bool noWaterFeatures = false;

        private float largeBendSize = 100;
        private float mediumBendSize = 40;
        private float smallBendSize = 15;

        public bool disallowStoneBeaches = false; // this is for rugged biomes that should have sand beaches
        public bool disallowAllBeaches = false;

        public RealisticBiomeBase(Biome biome, Biome river)
        {

            arrRealisticBiomeIds[biome.getBiomeID()] = this;

            baseBiome = biome;
            riverBiome = river;
            this.config = new BiomeConfig();
            _beachBiome = this.beachBiome();

            waterSurfaceLakeChance = 10;
            lavaSurfaceLakeChance = 0; // Disabled.

            waterUndergroundLakeChance = 1;
            lavaUndergroundLakeChance = 1;

            generateVillages = true;

            generatesEmeralds = false;
            generatesSilverfish = false;
            //rtgTrees = new ArrayList<>();

            // set the water feature constants with the config changes
            this.lakeInterval *= rtgConfig.LAKE_FREQUENCY_MULTIPLIER;
            this.lakeWaterLevel *= rtgConfig.lakeSizeMultiplier();
            this.lakeShoreLevel *= rtgConfig.lakeSizeMultiplier();
            this.lakeDepressionLevel *= rtgConfig.lakeSizeMultiplier();

            this.largeBendSize *= rtgConfig.LAKE_SHORE_BENDINESS_MULTIPLIER;
            this.mediumBendSize *= rtgConfig.LAKE_SHORE_BENDINESS_MULTIPLIER;
            this.smallBendSize *= rtgConfig.LAKE_SHORE_BENDINESS_MULTIPLIER;

            this.init();
        }

        private void init()
        {
            initConfig();
            this.terrain = initTerrain();
            this.surface = initSurface();
            this.surfaceRiver = new SurfaceRiverOasis(config);
            this.surfaceGeneric = new SurfaceGeneric(config, this.surface.getTopPixel(), this.surface.getFillerPixel());
        }

        public abstract void initConfig();
        public abstract TerrainBase initTerrain();
        public abstract SurfaceBase initSurface();

        public BiomeConfig getConfig()
        {
            return this.config;
        }

        public static RealisticBiomeBase getBiome(int id)
        {
            return arrRealisticBiomeIds[id];
        }

        public static RealisticBiomeBase[] arr()
        {
            return arrRealisticBiomeIds;
        }

        /*
         * Returns the beach biome to use for this biome.
         * By default, it uses the beach that has been set in the biome config.
         * If automatic beach detection is enabled (-1), it uses the supplied preferred beach.
         */
        protected Biome beachBiome(Biome preferredBeach)
        {

            Biome beach;
            int configBeachId = this.getConfig().BEACH_BIOME;

            if (configBeachId > -1 && configBeachId < 256)
            {
                beach = Biome.getBiome(configBeachId);
            }
            else
            {
                beach = preferredBeach;
            }

            return beach;
        }

        /*
         * Returns the beach biome to use for this biome, with a dynamically-calculated preferred beach.
         */
        virtual public Biome beachBiome()
        {
            return this.beachBiome(BiomeAnalyzer.getPreferredBeachForBiome(this.baseBiome));
        }

        public void rMapVolcanoes(Chunk primer, World world, IBiomeProviderRTG cmr, Random mapRand, int baseX, int baseY, int chunkX, int chunkY, OpenSimplexNoise simplex, CellNoise cell, float[] noise)
        {

            // Have volcanoes been disabled in the global config?
            if (!rtgConfig.ENABLE_VOLCANOES) return;

            // Have volcanoes been disabled in the biome config?
            int biomeId = cmr.getBiomeGenAt(baseX * 16, baseY * 16).getBiomeID();
            RealisticBiomeBase realisticBiome = getBiome(biomeId);
            // Do we need to patch the biome?
            if (realisticBiome == null)
            {
                RealisticBiomePatcher biomePatcher = new RealisticBiomePatcher();
                realisticBiome = biomePatcher.getPatchedRealisticBiome(
                    "NULL biome (" + biomeId + ") found when mapping volcanoes.");
            }
            if (!realisticBiome.getConfig().ALLOW_VOLCANOES) return;

            // Have volcanoes been disabled via frequency?
            // Use the global frequency unless the biome frequency has been explicitly set.
            int chance = realisticBiome.getConfig().VOLCANO_CHANCE == -1 ? rtgConfig.VOLCANO_CHANCE : realisticBiome.getConfig().VOLCANO_CHANCE;
            if (chance < 1) return;

            // If we've made it this far, let's go ahead and generate the volcano. Exciting!!! :D
            if (baseX % 4 == 0 && baseY % 4 == 0 && mapRand.Next(chance) == 0)
            {

                float river = cmr.getRiverStrength(baseX * 16, baseY * 16) + 1f;
                if (river > 0.98f && cmr.isBorderlessAt(baseX * 16, baseY * 16))
                {
                    long i1 = mapRand.Next() / 2L * 2L + 1L;
                    long j1 = mapRand.Next() / 2L * 2L + 1L;
                    mapRand = new Random((int)((long)chunkX * i1 + (long)chunkY * j1 ^ world.getSeed()));

                    WorldGenVolcano.build(primer, world, mapRand, baseX, baseY, chunkX, chunkY, simplex, cell, noise);
                }
            }
        }

        public void generateMapGen(Chunk primer, long seed, World world, IBiomeProviderRTG cmr, Random mapRand, int chunkX, int chunkY, OpenSimplexNoise simplex, CellNoise cell, float[] noise)
        {

            // Have volcanoes been disabled in the global config?
            if (!rtgConfig.ENABLE_VOLCANOES) return;

            int mapGenRadius = 5;
            int volcanoGenRadius = 15;

            mapRand = new Random((int)seed);
            long l = (mapRand.Next() / 2L) * 2L + 1L;
            long l1 = (mapRand.Next() / 2L) * 2L + 1L;

            // Volcanoes generation
            for (int baseX = chunkX - volcanoGenRadius; baseX <= chunkX + volcanoGenRadius; baseX++)
            {
                for (int baseY = chunkY - volcanoGenRadius; baseY <= chunkY + volcanoGenRadius; baseY++)
                {
                    mapRand = new Random((int)((long)baseX * l + (long)baseY * l1 ^ seed));
                    rMapVolcanoes(primer, world, cmr, mapRand, baseX, baseY, chunkX, chunkY, simplex, cell, noise);
                }
            }
        }

        public float rNoise(RTGWorld rtgWorld, int x, int y, float border, float river)
        {
            // we now have both lakes and rivers lowering land
            if (noWaterFeatures)
            {
                float borderForRiver = border * 2;
                if (borderForRiver > 1f) borderForRiver = 1;
                river = 1f - (1f - borderForRiver) * (1f - river);
                return terrain.generateNoise(rtgWorld, x, y, border, river);
            }
            float lakeStrength = lakePressure(rtgWorld, x, y, border);
            float _lakeFlattening = lakeFlattening(lakeStrength, lakeShoreLevel, lakeDepressionLevel);
            // we add some flattening to the rivers. The lakes are pre-flattened.
            float riverFlattening = river * 1.25f - 0.25f;
            if (riverFlattening < 0) riverFlattening = 0;
            if ((river < 1) && (_lakeFlattening < 1))
            {
                riverFlattening = (1f - riverFlattening) / riverFlattening + (1f - _lakeFlattening) / _lakeFlattening;
                riverFlattening = (1f / (riverFlattening + 1f));
            }
            else if (_lakeFlattening < riverFlattening) riverFlattening = _lakeFlattening;
            // the lakes have to have a little less flattening to avoid the rocky edges
            _lakeFlattening = lakeFlattening(lakeStrength, lakeWaterLevel, lakeDepressionLevel);

            if ((river < 1) && (_lakeFlattening < 1))
            {
                river = (1f - river) / river + (1f - _lakeFlattening) / _lakeFlattening;
                river = (1f / (river + 1f));
            }
            else if (_lakeFlattening < river) river = _lakeFlattening;
            // flatten terrain to set up for the water features
            float terrainNoise = terrain.generateNoise(rtgWorld, x, y, border, riverFlattening);
            // place water features
            return this.erodedNoise(rtgWorld, x, y, river, border, terrainNoise, _lakeFlattening);
        }

        public static readonly float actualRiverProportion = 300f / 1600f;
        public float erodedNoise(RTGWorld rtgWorld, int x, int y, float river, float border, float biomeHeight, double lakeFlattening)
        {

            float r;
            // put a flat spot in the middle of the river
            float riverFlattening = river; // moved the flattening to terrain stage
            if (riverFlattening < 0) riverFlattening = 0;

            // check if rivers need lowering
            //if (riverFlattening < actualRiverProportion) {
            r = riverFlattening / actualRiverProportion;
            //}

            //if (1>0) return 62f+r*10f;
            if ((r < 1f && biomeHeight > 57f))
            {
                return (biomeHeight * (r)) + ((57f + rtgWorld.simplex.noise2(x / 12f, y / 12f) *
                    2f + rtgWorld.simplex.noise2(x / 8f, y / 8f) * 1.5f) * (1f - r));
            }
            else return biomeHeight;
        }

        public float lakeFlattening(RTGWorld rtgWorld, int x, int y, float border)
        {
            return lakeFlattening(lakePressure(rtgWorld, x, y, border), lakeWaterLevel, lakeDepressionLevel);
        }

        public float lakePressure(RTGWorld rtgWorld, int x, int y, float border)
        {
            if (noLakes) return 1f;
            SimplexOctave.Disk jitter = new SimplexOctave.Disk();
            rtgWorld.simplex.riverJitter().evaluateNoise((float)x / 240.0, (float)y / 240.0, jitter);
            double pX = x + jitter.deltax() * largeBendSize;
            double pY = y + jitter.deltay() * largeBendSize;
            rtgWorld.simplex.mountain().evaluateNoise((float)x / 80.0, (float)y / 80.0, jitter);
            pX += jitter.deltax() * mediumBendSize;
            pY += jitter.deltay() * mediumBendSize;
            rtgWorld.simplex.octave(4).evaluateNoise((float)x / 30.0, (float)y / 30.0, jitter);
            pX += jitter.deltax() * smallBendSize;
            pY += jitter.deltay() * smallBendSize;
            //double results =simplexCell.river().noise(pX / lakeInterval, pY / lakeInterval,1.0);
            double[] lakeResults = rtgWorld.cell.river().eval((float)pX / lakeInterval, (float)pY / lakeInterval);
            float results = 1f - (float)((lakeResults[1] - lakeResults[0]) / lakeResults[1]);
            if (results > 1.01) throw new Exception("" + lakeResults[0] + " , " + lakeResults[1]);
            if (results < -.01) throw new Exception("" + lakeResults[0] + " , " + lakeResults[1]);
            //return simplexCell.river().noise((float)x/ lakeInterval, (float)y/ lakeInterval,1.0);
            return results;
        }

        public float lakeFlattening(float pressure, float bottomLevel, float topLevel)
        {
            // this number indicates a multiplier to height
            if (pressure > topLevel) return 1;
            if (pressure < bottomLevel) return 0;
            return (float)Math.Pow((pressure - bottomLevel) / (topLevel - bottomLevel), 1.0);
        }

        virtual public void rReplace(Chunk primer, int i, int j, int x, int y, int depth, RTGWorld rtgWorld, float[] noise, float river, Biome[] _base)
        {

            float riverRegion = this.noWaterFeatures ? 0f : river;

            if (rtgConfig.ENABLE_RTG_BIOME_SURFACES && this.getConfig().USE_RTG_SURFACES)
            {

                this.surface.paintTerrain(primer, i, j, x, y, depth, rtgWorld, noise, riverRegion, _base);
            }
            else
            {

                this.surfaceGeneric.paintTerrain(primer, i, j, x, y, depth, rtgWorld, noise, riverRegion, _base);
            }
        }

        protected void rReplaceWithRiver(Chunk primer, int i, int j, int x, int y, int depth, RTGWorld rtgWorld, float[] noise, float river, Biome[] _base)
        {

            float riverRegion = this.noWaterFeatures ? 0f : river;

            if (rtgConfig.ENABLE_RTG_BIOME_SURFACES && this.getConfig().USE_RTG_SURFACES)
            {

                this.surface.paintTerrain(primer, i, j, x, y, depth, rtgWorld, noise, riverRegion, _base);

                if (rtgConfig.ENABLE_LUSH_RIVER_BANK_SURFACES_IN_HOT_BIOMES)
                {

                    this.surfaceRiver.paintTerrain(primer, i, j, x, y, depth, rtgWorld, noise, riverRegion, _base);
                }
            }
            else
            {

                this.surfaceGeneric.paintTerrain(primer, i, j, x, y, depth, rtgWorld, noise, riverRegion, _base);
            }
        }

        public float r3Dnoise(float z)
        {

            return 0f;
        }

        public TerrainBase getTerrain()
        {

            return this.terrain;
        }

        public SurfaceBase getSurface()
        {

            return this.surface;
        }

        public bool compareTerrain(RTGWorld rtgWorld, TerrainBase oldTerrain)
        {

            OpenSimplexNoise simplex = new OpenSimplexNoise(4444);
            SimplexCellularNoise cell = new SimplexCellularNoise(4444);
            Random rand = new Random(4444);

            float oldNoise;

            TerrainBase newTerrain = this.initTerrain();
            float newNoise;

            for (int x = -64; x <= 64; x++)
            {
                for (int z = -64; z <= 64; z++)
                {

                    oldNoise = oldTerrain.generateNoise(rtgWorld, x, z, 0.5f, 0.5f);
                    newNoise = newTerrain.generateNoise(rtgWorld, x, z, 0.5f, 0.5f);

                    //Logger.info("%s (%d) = oldNoise = %f | newNoise = %f", this.baseBiome.getBiomeName(), Biome.getIdForBiome(this.baseBiome), oldNoise, newNoise);

                    if (oldNoise != newNoise)
                    {
                        throw new Exception(
                            "Terrains do not match in biome ID " + this.baseBiome.getBiomeID() + " (" + this.baseBiome.ToString() + ")."
                        );
                    }
                }
            }

            return true;
        }
    }
}