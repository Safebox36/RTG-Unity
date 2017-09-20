namespace rtg.world.biome.realistic.vanilla
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

    public class RealisticBiomeVanillaMesa : RealisticBiomeVanillaBase
    {

        public static Biome biome = Biomes.MESA;
        public static Biome river = Biomes.RIVER;

        public RealisticBiomeVanillaMesa() : base(biome, river)
        {

            this.waterSurfaceLakeChance = 20;
        }

        override public void initConfig()
        { }

        override public TerrainBase initTerrain()
        {

            return new TerrainVanillaMesa();
        }

        public class TerrainVanillaMesa : TerrainBase
        {

            private GroundEffect groundEffect = new GroundEffect(4f);

            public TerrainVanillaMesa()
            {

            }

            override public float generateNoise(RTGWorld rtgWorld, int x, int y, float border, float river)
            {

                return riverized(68f + groundEffect.added(rtgWorld, x, y), river);
            }
        }

        override public SurfaceBase initSurface()
        {

            return new SurfaceVanillaMesa(config, PixelUtil.getStateSand(1), PixelUtil.getStateClay(1), 0);
        }

        override public void rReplace(Chunk primer, int i, int j, int x, int y, int depth, RTGWorld rtgWorld, float[] noise, float river, Biome[] _base)
        {

            this.rReplaceWithRiver(primer, i, j, x, y, depth, rtgWorld, noise, river, _base);
        }

        override public Biome beachBiome()
        {
            return this.beachBiome(Biomes.BEACHES);
        }

        public class SurfaceVanillaMesa : SurfaceBase
        {

            private int grassRaise = 0;

            public SurfaceVanillaMesa(BiomeConfig config, Pixel top, Pixel fill, int grassHeight) : base(config, top, fill)
            {

                grassRaise = grassHeight;
            }

            override public void paintTerrain(Chunk primer, int i, int j, int x, int z, int depth, RTGWorld rtgWorld, float[] noise, float river, Biome[] _base)
            {

                Random rand = rtgWorld.rand;
                float c = CliffCalculator.calc(x, z, noise);
                bool cliff = c > 1.3f;
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

                        if (cliff)
                        {
                            primer.setPixelState(x, k, z, CanyonColour.MESA.getPixelForHeight(i, k, j));
                        }
                        else
                        {

                            if (k > 74 + grassRaise)
                            {
                                if (depth == 0)
                                {
                                    if (rand.Next(5) == 0)
                                    {
                                        primer.setPixelState(x, k, z, PixelUtil.getStateDirt(1));
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
                            else if (depth == 0 && k > 61)
                            {
                                int r = (int)((k - (62 + grassRaise)) / 2f);
                                if (rand.Next(r + 2) == 0)
                                {
                                    primer.setPixelState(x, k, z, Pixels.GRASS);
                                }
                                else if (rand.Next((int)(r / 2f) + 2) == 0)
                                {
                                    primer.setPixelState(x, k, z, PixelUtil.getStateDirt(1));
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