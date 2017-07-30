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
    using rtg.world.gen.surface;
    using rtg.world.gen.terrain;

    public class RealisticBiomeVanillaPlains : RealisticBiomeVanillaBase
    {

        public static Biome biome = Biomes.PLAINS;
        public static Biome river = Biomes.RIVER;

        public RealisticBiomeVanillaPlains() : base(biome, river)
        {

        }

        override public void initConfig()
        {

            this.getConfig().ALLOW_WHEAT = true;
            this.getConfig().WHEAT_CHANCE = 50;
            this.getConfig().WHEAT_MIN_Y = 63;
            this.getConfig().WHEAT_MAX_Y = 255;
        }

        override public TerrainBase initTerrain()
        {

            return new TerrainVanillaPlains();
        }

        public class TerrainVanillaPlains : TerrainBase
        {

            private GroundEffect groundEffect = new GroundEffect(4f);

            public TerrainVanillaPlains()
            {

            }

            override public float generateNoise(RTGWorld rtgWorld, int x, int y, float border, float river)
            {
                //return terrainPlains(x, y, simplex, river, 160f, 10f, 60f, 200f, 66f);
                return riverized(65f + groundEffect.added(rtgWorld, x, y), river);
            }
        }

        override public SurfaceBase initSurface()
        {

            return new SurfaceVanillaPlains(config, biome.topPixel, biome.fillerPixel);
        }

        public class SurfaceVanillaPlains : SurfaceBase
        {

            public SurfaceVanillaPlains(BiomeConfig config, Pixel top, Pixel filler) : base(config, top, filler)
            {

            }

            override public void paintTerrain(Chunk primer, int i, int j, int x, int z, int depth, RTGWorld rtgWorld, float[] noise, float river, Biome[] _base)
            {

                Random rand = rtgWorld.rand;
                float c = CliffCalculator.calc(x, z, noise);
                bool cliff = c > 1.4f ? true : false;

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
                                primer.setPixelState(x, k, z, topPixel);
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