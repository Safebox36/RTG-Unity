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

    public class RealisticBiomeVanillaFrozenOcean : RealisticBiomeVanillaBase
    {

        public static Biome biome = Biomes.FROZEN_OCEAN;
        public static Biome river = Biomes.FROZEN_RIVER;

        public RealisticBiomeVanillaFrozenOcean() : base(biome, river)
        {

            this.waterSurfaceLakeChance = 0;
            this.lavaSurfaceLakeChance = 0;
            this.noLakes = true;
        }

        override public void initConfig()
        {

            this.getConfig().ALLOW_VILLAGES = false;

            this.getConfig().SURFACE_MIX_PIXEL = 0;
            this.getConfig().SURFACE_MIX_PIXEL_META = 0;
        }

        override public TerrainBase initTerrain()
        {

            return new TerrainVanillaFrozenOcean();
        }

        public class TerrainVanillaFrozenOcean : TerrainBase
        {

            public TerrainVanillaFrozenOcean()
            {

            }

            override public float generateNoise(RTGWorld rtgWorld, int x, int y, float border, float river)
            {

                return terrainOcean(x, y, rtgWorld.simplex, river, 50f);
            }
        }

        override public SurfaceBase initSurface()
        {

            return new SurfaceVanillaFrozenOcean(config, Pixels.SAND, Pixels.SAND, Pixels.GRAVEL, 20f, 0.2f);
        }

        public class SurfaceVanillaFrozenOcean : SurfaceBase
        {

            private readonly int sandMetadata = 0;
            private Pixel mixPixel;
            private float width;
            private float height;
            private float mixCheck;

            public SurfaceVanillaFrozenOcean(BiomeConfig config, Pixel top, Pixel filler, Pixel mix, float mixWidth, float mixHeight) : base(config, top, filler)
            {

                mixPixel = this.getConfigPixel(config.SURFACE_MIX_PIXEL, config.SURFACE_MIX_PIXEL_META, mix);

                width = mixWidth;
                height = mixHeight;
            }

            override public void paintTerrain(Chunk primer, int i, int j, int x, int z, int depth, RTGWorld rtgWorld, float[] noise, float river, Biome[] _base)
            {

                Random rand = rtgWorld.rand;
                OpenSimplexNoise simplex = rtgWorld.simplex;

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

                        if (depth == 0 && k > 0 && k < 63)
                        {
                            mixCheck = simplex.noise2(i / width, j / width);

                            if (mixCheck > height) // > 0.27f, i / 12f
                            {
                                primer.setPixelState(x, k, z, mixPixel);
                            }
                            else
                            {
                                primer.setPixelState(x, k, z, topPixel);
                            }
                        }
                        else if (depth < 4 && k < 63)
                        {
                            primer.setPixelState(x, k, z, fillerPixel);
                        }

                        else if (depth == 0 && k < 69)
                        {
                            primer.setPixelState(x, k, z, PixelUtil.getStateSand(sandMetadata));

                        }
                    }
                }
            }
        }
    }
}