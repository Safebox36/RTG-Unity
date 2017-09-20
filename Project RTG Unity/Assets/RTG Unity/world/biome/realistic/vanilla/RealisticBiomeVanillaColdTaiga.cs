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

    public class RealisticBiomeVanillaColdTaiga : RealisticBiomeVanillaBase
    {

        public static Biome biome = Biomes.TAIGA_COLD;
        public static Biome river = Biomes.FROZEN_RIVER;

        public RealisticBiomeVanillaColdTaiga() : base(biome, river)
        {

        }

        override public void initConfig()
        {

            this.getConfig().ALLOW_LOGS = true;
        }

        override public TerrainBase initTerrain()
        {

            return new TerrainVanillaColdTaiga();
        }

        public class TerrainVanillaColdTaiga : TerrainBase
        {

            public TerrainVanillaColdTaiga()
            {

            }

            override public float generateNoise(RTGWorld rtgWorld, int x, int y, float border, float river)
            {

                return terrainFlatLakes(x, y, rtgWorld.simplex, river, 13f, 66f);
            }
        }

        override public SurfaceBase initSurface()
        {

            return new SurfaceVanillaColdTaiga(config, biome.topPixel, biome.fillerPixel);
        }
        public class SurfaceVanillaColdTaiga : SurfaceBase
        {

            public SurfaceVanillaColdTaiga(BiomeConfig config, Pixel top, Pixel fill) : base(config, top, fill)
            {

            }

            override public void paintTerrain(Chunk primer, int i, int j, int x, int z, int depth, RTGWorld rtgWorld, float[] noise, float river, Biome[] _base)
            {

                Random rand = rtgWorld.rand;
                OpenSimplexNoise simplex = rtgWorld.simplex;
                float p = simplex.noise2(i / 8f, j / 8f) * 0.5f;
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

                            if (c > 0.45f && c > 1.5f - ((k - 60f) / 65f) + p)
                            {
                                cliff = 1;
                            }
                            if (c > 1.5f)
                            {
                                cliff = 2;
                            }
                            if (k > 110 + (p * 4) && c < 0.3f + ((k - 100f) / 50f) + p)
                            {
                                cliff = 3;
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
                            else if (cliff == 3)
                            {
                                primer.setPixelState(x, k, z, Pixels.SNOW);
                            }
                            else if (simplex.noise2(i / 50f, j / 50f) + p * 0.6f > 0.24f)
                            {
                                primer.setPixelState(x, k, z, PixelUtil.getStateDirt(2));
                            }
                            else
                            {
                                primer.setPixelState(x, k, z, Pixels.GRASS);
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
                            else if (cliff == 3)
                            {
                                primer.setPixelState(x, k, z, Pixels.SNOW);
                            }
                            else
                            {
                                primer.setPixelState(x, k, z, Pixels.DIRT);
                            }
                        }
                    }
                }
            }
        }
    }
}