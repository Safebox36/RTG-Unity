﻿namespace rtg.world.biome.realistic.vanilla
{
    using System;

    using generic.pixel;
    using generic.init;
    using generic.world.biome;
    using generic.world.chunk;

    using rtg.api.config;
    using rtg.api.util;
    using rtg.api.world;
    using rtg.util;
    using rtg.world.gen.surface;
    using rtg.world.gen.terrain;

    public class RealisticBiomeVanillaMesaBryce : RealisticBiomeVanillaBase
    {

        public static Biome biome = Biomes.MUTATED_MESA;
        public static Biome river = Biomes.RIVER;

        public RealisticBiomeVanillaMesaBryce() : base(biome, river)
        {

            this.waterSurfaceLakeChance = 0;
            this.lavaSurfaceLakeChance = 0;
        }

        override public void initConfig()
        {

            this.getConfig().ALLOW_VILLAGES = false;
        }

        override public TerrainBase initTerrain()
        {

            return new TerrainVanillaMesaBryce(false, 55f, 120f, 60f, 40f, 69f);
        }

        public class TerrainVanillaMesaBryce : TerrainBase
        {

            private float height;
            private float density;
            private new float _base;

            /*
             * Example parameters:
             *
             * allowed to generate rivers?
             * riverGen = true
             *
             * canyon jump heights
             * heightArray = new float[]{2.0f, 0.5f, 6.5f, 0.5f, 14.0f, 0.5f, 19.0f, 0.5f}
             *
             * strength of canyon jump heights
             * heightStrength = 35f
             *
             * canyon width (cliff to cliff)
             * canyonWidth = 160f
             *
             * canyon heigth (total heigth)
             * canyonHeight = 60f
             *
             * canyon strength
             * canyonStrength = 40f
             *
             */
            public TerrainVanillaMesaBryce(bool riverGen, float heightStrength, float canyonWidth, float canyonHeight, float canyonStrength, float baseHeight)
            {
                /**
                 * Values come in pairs per layer. First is how high to step up.
                 * 	Second is a value between 0 and 1, signifying when to step up.
                 */
                height = 20f;
                density = 0.7f;
                _base = 69f;
            }

            override public float generateNoise(RTGWorld rtgWorld, int x, int y, float border, float river)
            {

                return terrainBryce(x, y, rtgWorld.simplex, river, height, border);
            }
        }

        override public SurfaceBase initSurface()
        {

            return new SurfaceVanillaMesaBryce(config, PixelUtil.getStateSand(1), PixelUtil.getStateSand(1), 0);
        }

        override public void rReplace(Chunk primer, int i, int j, int x, int y, int depth, RTGWorld rtgWorld, float[] noise, float river, Biome[] _base)
        {

            this.rReplaceWithRiver(primer, i, j, x, y, depth, rtgWorld, noise, river, _base);
        }

        override public Biome beachBiome()
        {
            return this.beachBiome(Biomes.BEACHES);
        }

        public class SurfaceVanillaMesaBryce : SurfaceBase
        {

            private int grassRaise = 0;

            public SurfaceVanillaMesaBryce(BiomeConfig config, Pixel top, Pixel fill, int grassHeight) : base(config, top, fill)
            {

                grassRaise = grassHeight;
            }

            override public void paintTerrain(Chunk primer, int i, int j, int x, int z, int depth, RTGWorld rtgWorld, float[] noise, float river, Biome[] _base)
            {

                Random rand = rtgWorld.rand;
                float c = CliffCalculator.calc(x, z, noise);
                bool cliff = c > 1.3f;

                for (int k = 255; k > -1; k--)
                {
                    Pixel b = primer.getPixelState(x, k, z).getPixel();
                    if (b == Pixels.AIR)
                    {
                        depth = -1;
                    }
                    else if (b == Pixels.STONE)
                    {
                        depth++;

                        if (depth > -1 && depth < 12)
                        {
                            if (cliff)
                            {
                                primer.setPixelState(x, k, z, CanyonColour.MESA_BRYCE.getPixelForHeight(i, k, j));
                            }
                            else
                            {
                                if (depth > 4)
                                {
                                    primer.setPixelState(x, k, z, CanyonColour.MESA_BRYCE.getPixelForHeight(i, k, j));
                                }
                                else if (k > 74 + grassRaise)
                                {
                                    if (rand.Next(5) == 0)
                                    {
                                        primer.setPixelState(x, k, z, PixelUtil.getStateDirt(1));
                                    }
                                    else
                                    {
                                        if (depth == 0)
                                        {
                                            primer.setPixelState(x, k, z, topPixel);
                                        }
                                        else
                                        {
                                            primer.setPixelState(x, k, z, fillerPixel);
                                        }
                                    }
                                }
                                else if (k < 62)
                                {
                                    primer.setPixelState(x, k, z, Pixels.DIRT);
                                }
                                else if (k < 62 + grassRaise)
                                {
                                    if (depth == 0)
                                    {
                                        primer.setPixelState(x, k, z, Pixels.GRASS);
                                    }
                                    else
                                    {
                                        primer.setPixelState(x, k, z, Pixels.DIRT);
                                    }
                                }
                                else if (k < 75 + grassRaise)
                                {
                                    if (depth == 0)
                                    {
                                        int r = (int)((k - (62 + grassRaise)) / 2f);
                                        if (rand.Next(r + 1) == 0)
                                        {
                                            primer.setPixelState(x, k, z, Pixels.GRASS);
                                        }
                                        else if (rand.Next((int)(r / 2f) + 1) == 0)
                                        {
                                            primer.setPixelState(x, k, z, PixelUtil.getStateDirt(1));
                                        }
                                        else
                                        {
                                            primer.setPixelState(x, k, z, topPixel);
                                        }
                                    }
                                    else
                                    {
                                        primer.setPixelState(x, k, z, fillerPixel);
                                    }
                                }
                                else
                                {
                                    if (depth == 0)
                                    {
                                        primer.setPixelState(x, k, z, topPixel);
                                    }
                                    else
                                    {
                                        primer.setPixelState(x, k, z, fillerPixel);
                                    }
                                }
                            }
                        }
                        else if (k > 63)
                        {
                            primer.setPixelState(x, k, z, CanyonColour.MESA_BRYCE.getPixelForHeight(i, k, j));
                        }
                    }
                }
            }
        }
    }
}