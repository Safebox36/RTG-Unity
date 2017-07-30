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
    using rtg.world.gen.terrain;
    using rtg.world.gen.terrain;

    public class RealisticBiomeVanillaExtremeHillsPlus : RealisticBiomeVanillaBase
    {

        public static Biome biome = Biomes.EXTREME_HILLS_WITH_TREES;
        public static Biome river = Biomes.RIVER;

        public RealisticBiomeVanillaExtremeHillsPlus() : base(biome, river)
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
        }

        override public TerrainBase initTerrain()
        {

            return new TerrainVanillaExtremeHillsPlus(150f, 80f, 90f);
        }

        public class TerrainVanillaExtremeHillsPlus : TerrainBase
        {

            private float width;
            private float strength;
            private float spikeWidth = 40;
            private float spikeHeight = 70;
            private HeightEffect heightEffect;

            public TerrainVanillaExtremeHillsPlus(float mountainWidth, float mountainStrength, float height)
            {

                width = mountainWidth;
                strength = mountainStrength;
                _base = height;
                MountainsWithPassesEffect mountainEffect = new MountainsWithPassesEffect();
                mountainEffect.mountainHeight = strength;
                mountainEffect.mountainWavelength = width;
                mountainEffect.spikeHeight = this.spikeHeight;
                mountainEffect.spikeWavelength = this.spikeWidth;

                heightEffect = new JitterEffect(7f, 10f, mountainEffect);
                heightEffect = new JitterEffect(3f, 6f, heightEffect);
                //this(mountainWidth, mountainStrength, depthLake, 260f, 68f);
            }

            override public float generateNoise(RTGWorld rtgWorld, int x, int y, float border, float river)
            {

                return riverized(heightEffect.added(rtgWorld, x, y) + _base, river);
            }
        }

        override public SurfaceBase initSurface()
        {

            return new SurfaceVanillaExtremeHillsPlus(config, Pixels.GRASS, Pixels.DIRT, 0f, 1.5f, 60f, 65f, 1.5f, Pixels.GRAVEL, 0.08f);
        }

        public class SurfaceVanillaExtremeHillsPlus : SurfaceBase
        {

            private float min;

            private float sCliff = 1.5f;
            private float sHeight = 60f;
            private float sStrength = 65f;
            private float cCliff = 1.5f;

            private Pixel mixPixel;
            private float mixHeight;

            public SurfaceVanillaExtremeHillsPlus(BiomeConfig config, Pixel top, Pixel fill, float minCliff, float stoneCliff, float stoneHeight, float stoneStrength, float clayCliff, Pixel mix, float mixSize) : base(config, top, fill)
            {

                min = minCliff;

                sCliff = stoneCliff;
                sHeight = stoneHeight;
                sStrength = stoneStrength;
                cCliff = clayCliff;

                mixPixel = this.getConfigPixel(config.SURFACE_MIX_PIXEL, config.SURFACE_MIX_PIXEL_META, mix);
                mixHeight = mixSize;
            }

            override public void paintTerrain(Chunk primer, int i, int j, int x, int z, int depth, RTGWorld rtgWorld, float[] noise, float river, Biome[] _base)
            {

                Random rand = rtgWorld.rand;
                OpenSimplexNoise simplex = rtgWorld.simplex;
                float c = CliffCalculator.calc(x, z, noise);
                int cliff = 0;
                bool m = false;

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
                            else if (simplex.noise2(i / 12f, j / 12f) > mixHeight)
                            {
                                primer.setPixelState(x, k, z, mixPixel);
                                m = true;
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
                                primer.setPixelState(x, k, z, hcStone(rtgWorld, i, j, x, z, k));
                            }
                            else if (cliff == 2)
                            {
                                primer.setPixelState(x, k, z, getShadowStonePixel(rtgWorld, i, j, x, z, k));
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