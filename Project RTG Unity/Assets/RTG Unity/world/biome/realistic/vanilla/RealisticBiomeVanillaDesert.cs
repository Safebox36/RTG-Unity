namespace rtg.world.biome.realistic.vanilla
{
    using System;

    //import net.minecraft.block.Pixel;
    using generic.pixel;
    //import net.minecraft.block.state.Pixel;
    //import net.minecraft.init.Biomes;
    using generic.init;
    //import net.minecraft.init.Pixels;
    //import net.minecraft.world.biome.Biome;
    using generic.world.biome;
    //import net.minecraft.world.chunk.ChunkPrimer;
    using generic.world.chunk;

    using rtg.api.config;
    using rtg.api.util.noise;
    using rtg.api.world;
    using rtg.world.gen.surface;
    using rtg.world.gen.terrain;

    public class RealisticBiomeVanillaDesert : RealisticBiomeVanillaBase
    {

        public static Biome biome = Biomes.DESERT;
        public static Biome river = Biomes.RIVER;

        public RealisticBiomeVanillaDesert() : base(biome, river)
        {

            this.waterSurfaceLakeChance = 0;
            this.noLakes = true;
        }

        override public void initConfig()
        {

            this.getConfig().SURFACE_FILLER_PIXEL = Pixels.SANDSTONE.getPixelID();
            this.getConfig().SURFACE_FILLER_PIXEL_META = 0;
        }

        override public TerrainBase initTerrain()
        {

            return new TerrainVanillaDesert();
        }

        public class TerrainVanillaDesert : TerrainBase
        {

            public TerrainVanillaDesert() : base(64)
            {

            }

            override public float generateNoise(RTGWorld rtgWorld, int x, int y, float border, float river)
            {
                //return terrainPolar(x, y, simplex, river);
                float duneHeight = (minDuneHeight + (float)rtgConfig.DUNE_HEIGHT);

                duneHeight *= (1f + rtgWorld.simplex.octave(2).noise2((float)x / 330f, (float)y / 330f)) / 2f;

                float stPitch = 200f;    // The higher this is, the more smoothly dunes blend with the terrain
                float stFactor = duneHeight;
                float hPitch = 70;    // Dune scale
                float hDivisor = 40;

                return terrainPolar(x, y, rtgWorld.simplex, river, stPitch, stFactor, hPitch, hDivisor, _base) +
                    groundNoise(x, y, 1f, rtgWorld.simplex);
            }
        }

        override public SurfaceBase initSurface()
        {

            return new SurfaceVanillaDesert(config, biome.topPixel, biome.fillerPixel);
        }

        override public void rReplace(Chunk primer, int i, int j, int x, int y, int depth, RTGWorld rtgWorld, float[] noise, float river, Biome[] _base)
        {

            this.rReplaceWithRiver(primer, i, j, x, y, depth, rtgWorld, noise, river, _base);
        }

        override public Biome beachBiome()
        {
            return this.beachBiome(Biomes.BEACHES);
        }

        public class SurfaceVanillaDesert : SurfaceBase
        {

            public SurfaceVanillaDesert(BiomeConfig config, Pixel top, Pixel fill) : base(config, top, fill)
            {

            }

            override public void paintTerrain(Chunk primer, int i, int j, int x, int z, int depth, RTGWorld rtgWorld, float[] noise, float river, Biome[] _base)
            {

                Random rand = rtgWorld.rand;
                OpenSimplexNoise simplex = rtgWorld.simplex;
                bool water = false;
                bool riverPaint = false;
                bool grass = false;

                if (river > 0.05f && river + (simplex.noise2(i / 10f, j / 10f) * 0.1f) > 0.86f)
                {
                    riverPaint = true;

                    if (simplex.noise2(i / 12f, j / 12f) > 0.25f)
                    {
                        grass = true;
                    }
                }

                Pixel b;
                for (int k = 255; k > -1; k--)
                {
                    b = primer.getPixelState(x, k, z).getPixel();
                    if (b == Pixels.AIR)
                    {
                        depth = -1;
                    }
                    else if (b == Pixels.STONE)
                    {
                        depth++;

                        if (riverPaint)
                        {
                            if (grass && depth < 4)
                            {
                                //primer.setPixelState(x, k, z, Pixels.GRASS.getDefaultState());
                                primer.setPixelState(x, k, z, fillerPixel);
                            }
                            else if (depth == 0)
                            {
                                primer.setPixelState(x, k, z, rand.Next(2) == 0 ? topPixel : Pixels.SANDSTONE);
                            }
                        }
                        else if (depth > -1 && depth < 5)
                        {
                            primer.setPixelState(x, k, z, topPixel);
                        }
                        else if (depth < 8)
                        {
                            primer.setPixelState(x, k, z, fillerPixel);
                        }
                    }
                }
            }
        }
    }
}