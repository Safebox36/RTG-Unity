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
    using rtg.api.util;
    using rtg.api.util.noise;
    using rtg.api.world;
    using rtg.world.gen.surface;
    using rtg.world.gen.terrain;

    public class RealisticBiomeVanillaOcean : RealisticBiomeVanillaBase
    {

        public static Biome biome = Biomes.OCEAN;
        public static Biome river = Biomes.RIVER;

        public RealisticBiomeVanillaOcean() : base(biome, river)
        {

            this.waterSurfaceLakeChance = 0;
            this.lavaSurfaceLakeChance = 0;
            this.noLakes = true;
            this.noWaterFeatures = true;
        }

        override public void initConfig()
        {

            this.getConfig().ALLOW_VILLAGES = false;

            this.getConfig().SURFACE_MIX_PIXEL = 0;
            this.getConfig().SURFACE_MIX_PIXEL_META = 0;
        }

        override public TerrainBase initTerrain()
        {

            return new TerrainVanillaOcean();
        }

        public class TerrainVanillaOcean : TerrainBase
        {

            public TerrainVanillaOcean()
            {

            }

            override public float generateNoise(RTGWorld rtgWorld, int x, int y, float border, float river)
            {

                return terrainOcean(x, y, rtgWorld.simplex, river, 50f);
            }
        }

        override public SurfaceBase initSurface()
        {

            return new SurfaceVanillaOcean(config, Pixels.SAND, Pixels.SAND, Pixels.GRAVEL, 20f, 0.2f);
        }

        public class SurfaceVanillaOcean : SurfaceBase
        {

            private readonly int sandMetadata = 0;
            private Pixel mixPixel;
            private float width;
            private float height;
            private float mixCheck;

            public SurfaceVanillaOcean(BiomeConfig config, Pixel top, Pixel filler, Pixel mix, float mixWidth, float mixHeight) : base(config, top, filler)
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