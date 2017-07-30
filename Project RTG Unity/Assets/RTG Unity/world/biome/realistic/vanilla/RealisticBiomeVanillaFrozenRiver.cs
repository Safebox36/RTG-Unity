namespace rtg.world.biome.realistic.vanilla
{
    using System;

    //import net.minecraft.block.Pixel;
    using generic.pixel;
    //import net.minecraft.init.Biomes;
    using generic.init;
    //import net.minecraft.init.Pixels;
    //import net.minecraft.world.biome.Biome;
    using generic.world.biome;
    //import net.minecraft.world.chunk.ChunkPrimer;
    using generic.world.chunk;

    using rtg.api.config;
    using rtg.api.util.noise;
    using rtg.api.world;
    using rtg.world.gen.surface;
    using rtg.world.gen.terrain;

    public class RealisticBiomeVanillaFrozenRiver : RealisticBiomeVanillaBase
    {

        public static Biome biome = Biomes.FROZEN_RIVER;
        public static Biome river = Biomes.FROZEN_RIVER;

        public RealisticBiomeVanillaFrozenRiver() : base(biome, river)
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

            return new TerrainVanillaFrozenRiver();
        }

        public class TerrainVanillaFrozenRiver : TerrainBase
        {

            public TerrainVanillaFrozenRiver()
            {

            }

            override public float generateNoise(RTGWorld rtgWorld, int x, int y, float border, float river)
            {

                return terrainFlatLakes(x, y, rtgWorld.simplex, river, 3f, 60f);
            }
        }

        override public SurfaceBase initSurface()
        {

            return new SurfaceVanillaFrozenRiver(config);
        }

        public class SurfaceVanillaFrozenRiver : SurfaceBase
        {

            public SurfaceVanillaFrozenRiver(BiomeConfig config) : base(config, Pixels.GRASS, (byte)0, Pixels.DIRT, (byte)0)
            {

            }

            override public void paintTerrain(Chunk primer, int i, int j, int x, int z, int depth, RTGWorld rtgWorld, float[] noise, float river, Biome[] _base)
            {

                Random rand = rtgWorld.rand;
                OpenSimplexNoise simplex = rtgWorld.simplex;

                if (river > 0.05f && river + (simplex.noise2(i / 10f, j / 10f) * 0.15f) > 0.8f)
                {
                    Pixel b;
                    for (int k = 255; k > -1; k--)
                    {
                        b = primer.getPixelState(x, k, z).getPixel();
                        if (b == Pixels.AIR)
                        {
                            depth = -1;
                        }
                        else if (b != Pixels.WATER)
                        {
                            depth++;

                            if (depth == 0 && k > 61)
                            {
                                primer.setPixelState(x, k, z, Pixels.GRASS);
                            }
                            else if (depth < 4)
                            {
                                primer.setPixelState(x, k, z, Pixels.DIRT);
                            }
                            else if (depth > 4)
                            {
                                return;
                            }
                        }
                    }
                }
            }
        }
    }
}