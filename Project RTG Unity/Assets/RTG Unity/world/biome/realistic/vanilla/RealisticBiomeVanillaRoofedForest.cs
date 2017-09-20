﻿namespace rtg.world.biome.realistic.vanilla
{
    using System;

    using generic.pixel;
    using generic.init;
    using generic.world.biome;
    using generic.world.chunk;

    using rtg.api.config;
    using rtg.api.util;
    using rtg.api.util.noise;
    using rtg.api.world;
    using rtg.world.gen.surface;
    using rtg.world.gen.terrain;

    public class RealisticBiomeVanillaRoofedForest : RealisticBiomeVanillaBase
    {

        public static Biome biome = Biomes.ROOFED_FOREST;
        public static Biome river = Biomes.RIVER;

        public RealisticBiomeVanillaRoofedForest() : base(biome, river)
        {

            this.waterSurfaceLakeChance = 3;
        }

        override public void initConfig()
        {

            this.getConfig().ALLOW_LOGS = true;
            this.getConfig().ALLOW_COBWEBS = true;

            this.getConfig().SURFACE_MIX_PIXEL = 0;
            this.getConfig().SURFACE_MIX_PIXEL_META = 0;
        }

        override public TerrainBase initTerrain()
        {

            return new TerrainVanillaRoofedForest();
        }

        public class TerrainVanillaRoofedForest : TerrainBase
        {

            private GroundEffect groundEffect = new GroundEffect(4f);

            public TerrainVanillaRoofedForest()
            {

            }

            override public float generateNoise(RTGWorld rtgWorld, int x, int y, float border, float river)
            {
                //return terrainPlains(x, y, simplex, river, 160f, 10f, 60f, 80f, 65f)
                return riverized(65f + groundEffect.added(rtgWorld, x, y), river);
            }
        }

        override public SurfaceBase initSurface()
        {

            return new SurfaceVanillaRoofedForest(config, Pixels.GRASS, Pixels.DIRT, 0f, 1.5f, 60f, 65f, 1.5f, PixelUtil.getStateDirt(2), 0.08f);
        }

        public class SurfaceVanillaRoofedForest : SurfaceBase
        {

            private float min;

            private float sCliff = 1.5f;
            private float sHeight = 60f;
            private float sStrength = 65f;
            private float cCliff = 1.5f;

            private Pixel mixPixel;
            private float mixHeight;

            public SurfaceVanillaRoofedForest(BiomeConfig config, Pixel top, Pixel fill, float minCliff, float stoneCliff, float stoneHeight, float stoneStrength, float clayCliff, Pixel mix, float mixSize) : base(config, top, fill)
            {

                min = minCliff;

                sCliff = stoneCliff;
                sHeight = stoneHeight;
                sStrength = stoneStrength;
                cCliff = clayCliff;

                mixPixel = this.getConfigPixel(config.SURFACE_MIX_PIXEL, config.SURFACE_MIX_PIXEL_META, mix);

                mixHeight = mixSize;
            }

            override public void paintTerrain(Chunk primer, int i, int j, int x, int z, int depth, RTGWorld rtgWorld, float[] noise, float river, Biome[] _base)
            {

                Random rand = rtgWorld.rand;
                OpenSimplexNoise simplex = rtgWorld.simplex;
                float c = CliffCalculator.calc(x, z, noise);
                int cliff = 0;
                bool m = false;

                // varying clay densities;
                float mixModifier = mixHeight + simplex.noise2(((float)i) / 800f, ((float)j) / 800f);
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
                            else if (simplex.noise2(i / 12f, j / 12f) > mixModifier)
                            {
                                primer.setPixelState(x, k, z, mixPixel);
                                m = true;
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