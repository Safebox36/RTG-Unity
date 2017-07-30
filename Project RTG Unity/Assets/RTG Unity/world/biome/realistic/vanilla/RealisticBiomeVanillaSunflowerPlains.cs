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

    public class RealisticBiomeVanillaSunflowerPlains : RealisticBiomeVanillaBase
    {

        public static Biome biome = Biomes.MUTATED_PLAINS;
        public static Biome river = Biomes.RIVER;

        public RealisticBiomeVanillaSunflowerPlains() : base(biome, river)
        {

        }

        override public void initConfig() { }

        override public TerrainBase initTerrain()
        {

            return new TerrainVanillaSunflowerPlains();
        }

        public class TerrainVanillaSunflowerPlains : TerrainBase
        {

            private GroundEffect groundEffect = new GroundEffect(4f);

            public TerrainVanillaSunflowerPlains()
            {

            }

            override public float generateNoise(RTGWorld rtgWorld, int x, int y, float border, float river)
            {
                //return terrainPlains(x, y, simplex, river, 160f, 10f, 60f, 200f, 65f);
                return riverized(65f + groundEffect.added(rtgWorld, x, y), river);
            }
        }

        override public SurfaceBase initSurface()
        {

            return new SurfaceVanillaSunflowerPlains(config, biome.topPixel, biome.fillerPixel);
        }

        public class SurfaceVanillaSunflowerPlains : SurfaceBase
        {

            public SurfaceVanillaSunflowerPlains(BiomeConfig config, Pixel top, Pixel filler) : base(config, top, filler)
            {

            }

            override public void paintTerrain(Chunk primer, int i, int j, int x, int z, int depth, RTGWorld rtgWorld, float[] noise, float river, Biome[] _base)
            {

                Random rand = rtgWorld.rand;
                OpenSimplexNoise simplex = rtgWorld.simplex;
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