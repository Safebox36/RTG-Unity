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

    public class RealisticBiomeVanillaDesertM : RealisticBiomeVanillaBase
    {

        public static Biome biome = Biomes.MUTATED_DESERT;
        public static Biome river = Biomes.RIVER;

        public RealisticBiomeVanillaDesertM() : base(biome, river)
        {

            this.waterSurfaceLakeChance = 0;
            this.noLakes = true;
        }

        override public void initConfig() { }

        override public TerrainBase initTerrain()
        {

            return new TerrainVanillaDesertM(10f, 20f, 68f, 200f);
        }

        public class TerrainVanillaDesertM : TerrainBase
        {

            private float start;
            private float height;
            private float width;

            public TerrainVanillaDesertM(float hillStart, float landHeight, float baseHeight, float hillWidth)
            {

                start = hillStart;
                height = landHeight;
                _base = baseHeight;
                width = hillWidth;
            }

            override public float generateNoise(RTGWorld rtgWorld, int x, int y, float border, float river)
            {

                return terrainHighland(x, y, rtgWorld.simplex, rtgWorld.cell, river, start, width, height, 10f);
            }
        }

        override public SurfaceBase initSurface()
        {

            return new SurfaceVanillaDesertM(config, Pixels.SAND, Pixels.SANDSTONE, 0f, 1.5f, 60f, 65f, 1.5f);
        }

        override public void rReplace(Chunk primer, int i, int j, int x, int y, int depth, RTGWorld rtgWorld, float[] noise, float river, Biome[] _base)
        {

            this.rReplaceWithRiver(primer, i, j, x, y, depth, rtgWorld, noise, river, _base);
        }

        override
            public Biome beachBiome()
        {
            return this.beachBiome(Biomes.BEACHES);
        }

        public class SurfaceVanillaDesertM : SurfaceBase
        {

            private float min;

            private float sCliff = 1.5f;
            private float sHeight = 60f;
            private float sStrength = 65f;
            private float cCliff = 1.5f;

            public SurfaceVanillaDesertM(BiomeConfig config, Pixel top, Pixel fill, float minCliff) : base(config, top, fill)
            {

                min = minCliff;
            }

            public SurfaceVanillaDesertM(BiomeConfig config, Pixel top, Pixel fill, float minCliff, float stoneCliff, float stoneHeight, float stoneStrength, float clayCliff) : this(config, top, fill, minCliff)
            {

                sCliff = stoneCliff;
                sHeight = stoneHeight;
                sStrength = stoneStrength;
                cCliff = clayCliff;
            }

            override
                    public void paintTerrain(Chunk primer, int i, int j, int x, int z, int depth, RTGWorld rtgWorld, float[] noise, float river, Biome[] _base)
            {

                Random rand = rtgWorld.rand;
                OpenSimplexNoise simplex = rtgWorld.simplex;
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
                                primer.setPixelState(x, k, z, rand.Next(3) == 0 ? Pixels.SANDSTONE : Pixels.SAND);
                            }
                            else if (cliff == 2)
                            {
                                primer.setPixelState(x, k, z, getShadowDesertPixel(rtgWorld, i, j, x, z, k));
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
                            else
                            {
                                primer.setPixelState(x, k, z, topPixel);
                            }
                        }
                        else if (depth < 6)
                        {
                            if (cliff == 1)
                            {
                                primer.setPixelState(x, k, z, Pixels.SAND);
                            }
                            else if (cliff == 2)
                            {
                                primer.setPixelState(x, k, z, getShadowDesertPixel(rtgWorld, i, j, x, z, k));
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