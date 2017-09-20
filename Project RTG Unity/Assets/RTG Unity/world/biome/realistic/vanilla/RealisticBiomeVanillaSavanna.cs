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

    public class RealisticBiomeVanillaSavanna : RealisticBiomeVanillaBase
    {

        public static Biome biome = Biomes.SAVANNA;
        public static Biome river = Biomes.RIVER;

        public RealisticBiomeVanillaSavanna() : base(biome, river)
        {

        }

        override public void initConfig()
        {

            this.getConfig().ALLOW_LOGS = true;

            this.getConfig().SURFACE_MIX_PIXEL = 0;
            this.getConfig().SURFACE_MIX_PIXEL_META = 0;
        }

        override public TerrainBase initTerrain()
        {

            return new TerrainVanillaSavanna();
        }

        public class TerrainVanillaSavanna : TerrainBase
        {

            private GroundEffect groundEffect = new GroundEffect(4f);

            public TerrainVanillaSavanna()
            {

            }

            override public float generateNoise(RTGWorld rtgWorld, int x, int y, float border, float river)
            {
                //return terrainPlains(x, y, simplex, river, 160f, 10f, 60f, 100f, 66f);
                return riverized(65f + groundEffect.added(rtgWorld, x, y), river);
            }
        }

        override public SurfaceBase initSurface()
        {

            return new SurfaceVanillaSavanna(config, Pixels.GRASS, Pixels.DIRT, Pixels.GRASS, 13f, 0.27f);
        }

        override public Biome beachBiome()
        {
            return this.beachBiome(Biomes.BEACHES);
        }

        public class SurfaceVanillaSavanna : SurfaceBase
        {

            private Pixel mixPixel;
            private float width;
            private float height;

            public SurfaceVanillaSavanna(BiomeConfig config, Pixel top, Pixel filler, Pixel mix, float mixWidth, float mixHeight) : base(config, top, filler)
            {

                mixPixel = this.getConfigPixel(config.SURFACE_MIX_PIXEL, config.SURFACE_MIX_PIXEL_META, mix);

                width = mixWidth;
                height = mixHeight;
            }

            override public void paintTerrain(Chunk primer, int i, int j, int x, int z, int depth, RTGWorld rtgWorld, float[] noise, float river, Biome[] _base)
            {

                Random rand = rtgWorld.rand;
                OpenSimplexNoise simplex = rtgWorld.simplex;
                float c = CliffCalculator.calc(x, z, noise);
                bool cliff = c > 1.4f;

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
                                if (simplex.noise2(i / width, j / width) > height) // > 0.27f, i / 12f
                                {
                                    primer.setPixelState(x, k, z, mixPixel);
                                }
                                else
                                {
                                    primer.setPixelState(x, k, z, topPixel);
                                }
                            }
                            else if (depth < 4)
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