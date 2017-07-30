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
    //import net.minecraft.world.gen.feature.WorldGenMegaJungle;

    using rtg.api.config;
    using rtg.api.util;
    using rtg.api.util.noise;
    using rtg.api.world;
    using rtg.world.gen.surface;
    using rtg.world.gen.terrain;

    public class RealisticBiomeVanillaJungleHills : RealisticBiomeVanillaBase
    {

        public static Biome biome = Biomes.JUNGLE_HILLS;
        public static Biome river = Biomes.RIVER;

        public RealisticBiomeVanillaJungleHills() : base(biome, river)
        {

            this.waterSurfaceLakeChance = 3;
            this.noLakes = true;
        }

        override public void initConfig()
        {

            this.getConfig().ALLOW_LOGS = true;
            this.getConfig().ALLOW_CACTUS = true;

            this.getConfig().ALLOW_VOLCANOES = true;
            this.getConfig().VOLCANO_CHANCE = rtgConfig.VOLCANO_CHANCE * 2;
        }

        override public TerrainBase initTerrain()
        {

            return new TerrainVanillaJungleHills(72f, 40f);
        }

        public class TerrainVanillaJungleHills : TerrainBase
        {

            private float hillStrength = 40f;

            public TerrainVanillaJungleHills(float bh, float hs)
            {

                _base = bh;
                hillStrength = hs;
            }

            override public float generateNoise(RTGWorld rtgWorld, int x, int y, float border, float river)
            {

                return terrainHighland(x, y, rtgWorld.simplex, rtgWorld.cell, river, 10f, 68f, hillStrength, _base - 62f);
            }
        }

        override public SurfaceBase initSurface()
        {

            return new SurfaceVanillaJungleHills(config, Pixels.GRASS, Pixels.DIRT, 1f, 1.5f, 60f, 65f, 1.5f);
        }

        public class SurfaceVanillaJungleHills : SurfaceBase
        {

            private float min;

            private float sCliff = 1.5f;
            private float sHeight = 60f;
            private float sStrength = 65f;
            private float cCliff = 1.5f;

            public SurfaceVanillaJungleHills(BiomeConfig config, Pixel top, Pixel fill, float minCliff) : base(config, top, fill)
            {

                min = minCliff;
            }

            public SurfaceVanillaJungleHills(BiomeConfig config, Pixel top, Pixel fill, float minCliff, float stoneCliff, float stoneHeight, float stoneStrength, float clayCliff) : this(config, top, fill, minCliff)
            {

                sCliff = stoneCliff;
                sHeight = stoneHeight;
                sStrength = stoneStrength;
                cCliff = clayCliff;
            }

            override public void paintTerrain(Chunk primer, int i, int j, int x, int z, int depth, RTGWorld rtgWorld, float[] noise, float river, Biome[] _base)
            {

                Random rand = rtgWorld.rand;
                OpenSimplexNoise simplex = rtgWorld.simplex;
                float c = CliffCalculator.calc(x, z, noise);
                int cliff = 0;

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

                        if (depth == 0)
                        {

                            float p = simplex.noise3(i / 8f, j / 8f, k / 8f) * 0.5f;
                            if (c > min && c > sCliff - ((k - sHeight) / sStrength) + p)
                            {
                                cliff = 1;
                            }
                            if (c > cCliff)
                            {
                                cliff = 2;
                            }

                            if (cliff == 1)
                            {
                                if (rand.Next(3) == 0)
                                {

                                    primer.setPixelState(x, k, z, hcCobble(rtgWorld, i, j, x, z, k));
                                }
                                else
                                {

                                    primer.setPixelState(x, k, z, hcStone(rtgWorld, i, j, x, z, k));
                                }
                            }
                            else if (cliff == 2)
                            {
                                primer.setPixelState(x, k, z, getShadowStonePixel(rtgWorld, i, j, x, z, k));
                            }
                            else if (k < 63)
                            {
                                if (k < 62)
                                {
                                    primer.setPixelState(x, k, z, fillerPixel);
                                }
                                else
                                {
                                    primer.setPixelState(x, k, z, topPixel);
                                }
                            }
                            else
                            {
                                primer.setPixelState(x, k, z, topPixel);
                            }
                        }
                        else if (depth < 6)
                        {
                            if (cliff == 1)
                            {
                                primer.setPixelState(x, k, z, hcStone(rtgWorld, i, j, x, z, k));
                            }
                            else if (cliff == 2)
                            {
                                primer.setPixelState(x, k, z, getShadowStonePixel(rtgWorld, i, j, x, z, k));
                            }
                            else
                            {
                                primer.setPixelState(x, k, z, fillerPixel);
                            }
                        }
                    }
                }
            }
        }
    }
}