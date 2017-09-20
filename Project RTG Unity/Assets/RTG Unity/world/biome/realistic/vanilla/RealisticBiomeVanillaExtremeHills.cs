namespace rtg.world.biome.realistic.vanilla
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

    public class RealisticBiomeVanillaExtremeHills : RealisticBiomeVanillaBase
    {

        public static Biome biome = Biomes.EXTREME_HILLS;
        public static Biome river = Biomes.RIVER;

        public RealisticBiomeVanillaExtremeHills() : base(biome, river)
        {

            this.generatesEmeralds = true;
            this.generatesSilverfish = true;
            this.noLakes = true;
            this.noWaterFeatures = true;
        }

        override public void initConfig()
        {

            this.getConfig().ALLOW_LOGS = true;

            this.getConfig().SURFACE_MIX_PIXEL = 0;
            this.getConfig().SURFACE_MIX_PIXEL_META = 0;
            this.getConfig().SURFACE_MIX_FILLER_PIXEL = 0;
            this.getConfig().SURFACE_MIX_FILLER_PIXEL_META = 0;
        }

        override public TerrainBase initTerrain()
        {

            return new TerrainVanillaExtremeHills(10f, 120f, 10f, 200f);
        }

        public class TerrainVanillaExtremeHills : TerrainBase
        {

            private float start;
            private float height;
            private float width;

            public TerrainVanillaExtremeHills(float hillStart, float landHeight, float baseHeight, float hillWidth)
            {

                start = hillStart;
                height = landHeight;
                _base = baseHeight;
                width = hillWidth;
            }

            override public float generateNoise(RTGWorld rtgWorld, int x, int y, float border, float river)
            {

                return terrainHighland(x, y, rtgWorld.simplex, rtgWorld.cell, river, start, width, height, _base);
            }
        }

        override public SurfaceBase initSurface()
        {

            return new SurfaceVanillaExtremeHills(config, biome.topPixel, biome.fillerPixel, Pixels.GRASS, Pixels.DIRT, 60f, -0.14f, 14f, 0.25f);
        }

        public class SurfaceVanillaExtremeHills : SurfaceBase
        {

            private Pixel mixPixelTop;
            private Pixel mixPixelFill;
            private float width;
            private float height;
            private float smallW;
            private float smallS;

            public SurfaceVanillaExtremeHills(BiomeConfig config, Pixel top, Pixel filler, Pixel mixTop, Pixel mixFill, float mixWidth, float mixHeight, float smallWidth, float smallStrength) : base(config, top, filler)
            {

                mixPixelTop = this.getConfigPixel(config.SURFACE_MIX_PIXEL, config.SURFACE_MIX_PIXEL_META, mixTop);
                mixPixelFill = this.getConfigPixel(config.SURFACE_MIX_FILLER_PIXEL, config.SURFACE_MIX_FILLER_PIXEL_META, mixFill);

                width = mixWidth;
                height = mixHeight;
                smallW = smallWidth;
                smallS = smallStrength;
            }

            override public void paintTerrain(Chunk primer, int i, int j, int x, int z, int depth, RTGWorld rtgWorld, float[] noise, float river, Biome[] _base)
            {

                Random rand = rtgWorld.rand;
                OpenSimplexNoise simplex = rtgWorld.simplex;
                float c = CliffCalculator.calc(x, z, noise);
                bool cliff = c > 1.4f ? true : false;
                bool mix = false;

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

                        if (cliff)
                        {
                            if (depth > -1 && depth < 2)
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
                            else if (depth < 10)
                            {
                                primer.setPixelState(x, k, z, hcStone(rtgWorld, i, j, x, z, k));
                            }
                        }
                        else
                        {
                            if (depth == 0 && k > 61)
                            {
                                if (simplex.noise2(i / width, j / width) + simplex.noise2(i / smallW, j / smallW) * smallS > height)
                                {
                                    primer.setPixelState(x, k, z, mixPixelTop);
                                    mix = true;
                                }
                                else
                                {
                                    primer.setPixelState(x, k, z, topPixel);
                                }
                            }
                            else if (depth < 4)
                            {
                                if (mix)
                                {
                                    primer.setPixelState(x, k, z, mixPixelFill);
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
}