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
    using rtg.api.world;
    using rtg.util;
    using rtg.world.gen.surface;
    using rtg.world.gen.terrain;

    public class RealisticBiomeVanillaSavannaPlateau : RealisticBiomeVanillaBase
    {

        public static Biome biome = Biomes.SAVANNA_ROCK;
        public static Biome river = Biomes.RIVER;

        public RealisticBiomeVanillaSavannaPlateau() : base(biome, river)
        {

            this.noLakes = true;
        }

        override public void initConfig() { }

        override public TerrainBase initTerrain()
        {

            return new TerrainVanillaSavannaPlateau(true, 35f, 160f, 60f, 40f, 69f);
        }

        public class TerrainVanillaSavannaPlateau : TerrainBase
        {

            private bool booRiver;
            private float[] height;
            private int heightLength;
            private float strength;
            private float smooth;
            private float cWidth;
            private float cHeigth;
            private float cStrength;
            private float _base;

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
            public TerrainVanillaSavannaPlateau(bool riverGen, float heightStrength, float canyonWidth, float canyonHeight, float canyonStrength, float baseHeight)
            {

                booRiver = true;
                /**    Values come in pairs per layer. First is how high to step up.
                 * 	Second is a value between 0 and 1, signifying when to step up.
                 */
                height = new float[] { 12.0f, 0.5f, 8f, 0.7f };
                strength = 10f;
                heightLength = height.Length;
            }

            override public float generateNoise(RTGWorld rtgWorld, int x, int y, float border, float river)
            {

                return terrainPlateau(x, y, rtgWorld.simplex, river, height, border, strength, heightLength, 50f, true);
            }
        }

        override public SurfaceBase initSurface()
        {

            return new SurfaceVanillaSavannaPlateau(config, biome.topPixel, biome.fillerPixel, 0);
        }

        override public Biome beachBiome()
        {
            return this.beachBiome(Biomes.BEACHES);
        }

        public class SurfaceVanillaSavannaPlateau : SurfaceBase
        {

            private int grassRaise = 0;

            public SurfaceVanillaSavannaPlateau(BiomeConfig config, Pixel top, Pixel fill, int grassHeight) : base(config, top, fill)
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
                            if (!rtgConfig.STONE_SAVANNAS)
                            {
                                primer.setPixelState(x, k, z, CanyonColour.SAVANNA.getPixelForHeight(i, k, j));
                            }
                            else
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