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

    public class RealisticBiomeVanillaColdBeach : RealisticBiomeVanillaBase
    {

        public static Biome biome = Biomes.COLD_BEACH;
        public static Biome river = Biomes.FROZEN_RIVER;

        public RealisticBiomeVanillaColdBeach() : base(biome, river)
        {

        }

        override public void initConfig()
        {

            this.getConfig().ALLOW_VILLAGES = false;
        }

        override public TerrainBase initTerrain()
        {

            return new TerrainVanillaColdBeach();
        }

        public class TerrainVanillaColdBeach : TerrainBase
        {

            public TerrainVanillaColdBeach()
            {

            }

            override public float generateNoise(RTGWorld rtgWorld, int x, int y, float border, float river)
            {

                return terrainBeach(x, y, rtgWorld.simplex, river, 180f, 35f, 63f);
            }
        }

        override public SurfaceBase initSurface()
        {

            return new SurfaceVanillaColdBeach(config, biome.topPixel, biome.fillerPixel, biome.topPixel, biome.fillerPixel, (byte)0, 1);
        }

        public class SurfaceVanillaColdBeach : SurfaceBase
        {

            private Pixel cliffPixel1;
            private Pixel cliffPixel2;
            private byte sandMetadata;
            private int cliffType;

            public SurfaceVanillaColdBeach(BiomeConfig config, Pixel top, Pixel filler, Pixel cliff1, Pixel cliff2, byte metadata, int cliff) : base(config, top, filler)
            {

                cliffPixel1 = cliff1;
                cliffPixel2 = cliff2;
                sandMetadata = metadata;
                cliffType = cliff;
            }

            override public void paintTerrain(Chunk primer, int i, int j, int x, int z, int depth, RTGWorld rtgWorld, float[] noise, float river, Biome[] _base)
            {

                Random rand = rtgWorld.rand;
                OpenSimplexNoise simplex = rtgWorld.simplex;
                float c = CliffCalculator.calc(x, z, noise);
                bool cliff = c > 1.3f ? true : false;
                bool dirt = false;

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
                            if (cliffType == 1)
                            {
                                if (depth < 6)
                                {
                                    primer.setPixelState(x, k, z, cliffPixel1.getPixel());
                                }
                            }
                            else
                            {
                                if (depth > -1 && depth < 2)
                                {
                                    primer.setPixelState(x, k, z, rand.Next(3) == 0 ? cliffPixel2 : cliffPixel1);
                                }
                                else if (depth < 10)
                                {
                                    primer.setPixelState(x, k, z, cliffPixel1);
                                }
                            }
                        }
                        else if (depth < 6)
                        {
                            if (depth == 0 && k > 61)
                            {
                                if (simplex.noise2(i / 12f, j / 12f) > -0.3f + ((k - 61f) / 15f))
                                {
                                    dirt = true;
                                    primer.setPixelState(x, k, z, topPixel);
                                }
                                else
                                {
                                    if (k < 69)
                                    {
                                        primer.setPixelState(x, k, z, PixelUtil.getStateSand(sandMetadata));
                                    } // else probably steep shore so leave stone

                                }
                            }
                            else if (depth < 4)
                            {
                                if (dirt)
                                {
                                    primer.setPixelState(x, k, z, fillerPixel);
                                }
                                else
                                {
                                    if (k < 69)
                                    {
                                        primer.setPixelState(x, k, z, PixelUtil.getStateSand(sandMetadata));
                                    }
                                }
                            }
                            else if (!dirt)
                            {
                                primer.setPixelState(x, k, z, Pixels.SANDSTONE);
                            }
                        }
                    }
                }
            }
        }
    }
}