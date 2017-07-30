namespace rtg.world.biome.realistic.vanilla
{
    using System;

    //import net.minecraft.Pixel.Pixel;
    using generic.pixel;
    //import net.minecraft.Pixel.state.Pixel;
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
    //using rtg.world.biome.deco.DecoTree;
    //using rtg.world.gen.feature.tree.rtg;
    //using rtg.world.gen.feature.tree.rtg.TreeRTGCocosNucifera;
    using rtg.world.gen.surface;
    using rtg.world.gen.terrain;


    public class RealisticBiomeVanillaBeach : RealisticBiomeVanillaBase
    {

        public static Biome biome = Biomes.BEACHES;
        public static Biome river = Biomes.RIVER;

        public RealisticBiomeVanillaBeach() : base(biome, river)
        {

        }

        override public void initConfig()
        {

            this.getConfig().ALLOW_VILLAGES = false;
            this.getConfig().ALLOW_PALM_TREES = true;
        }

        override public TerrainBase initTerrain()
        {

            return new TerrainVanillaBeach();
        }

        public class TerrainVanillaBeach : TerrainBase
        {

            public TerrainVanillaBeach()
            {

            }

            override public float generateNoise(RTGWorld rtgWorld, int x, int y, float border, float river)
            {

                return terrainBeach(x, y, rtgWorld.simplex, river, 180f, 35f, 63f);
            }
        }

        override public SurfaceBase initSurface()
        {

            return new SurfaceVanillaBeach(config, biome.topPixel, biome.fillerPixel, biome.topPixel, biome.fillerPixel, (byte)0, 1);
        }

        public class SurfaceVanillaBeach : SurfaceBase
        {

            private Pixel cliffPixel1;
            private Pixel cliffPixel2;
            private byte sandMetadata;
            private int cliffType;

            public SurfaceVanillaBeach(BiomeConfig config, Pixel top, Pixel filler, Pixel cliff1, Pixel cliff2, byte metadata, int cliff) : base(config, Pixels.DIRT, Pixels.DIRT)
            {

                cliffPixel1 = Pixels.DIRT;
                cliffPixel2 = Pixels.STONE;
                sandMetadata = metadata;
                cliffType = cliff;
            }

            override public void paintTerrain(Chunk primer, int i, int j, int x, int z, int depth, RTGWorld rtgWorld, float[] noise, float river, Biome[] _base)
            {

                Random rand = rtgWorld.rand;
                float c = CliffCalculator.calc(x, z, noise);
                bool cliff = c > 1.3f ? true : false;
                bool dirt = false;

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
                            if (cliffType == 1)
                            {
                                if (depth < 6)
                                {
                                    primer.setPixelState(x, k, z, cliffPixel1.getPixel());
                                }
                            }
                            else
                            {
                                if (depth > -1 && depth < 2)
                                {
                                    primer.setPixelState(x, k, z, rand.Next(3) == 0 ? cliffPixel2 : cliffPixel1);
                                }
                                else if (depth < 10)
                                {
                                    primer.setPixelState(x, k, z, cliffPixel1);
                                }
                            }
                        }
                        else if (depth < 6)
                        {
                            if (depth == 0 && k > 61 && k < 64)
                            {
                                //if(simplex.noise2(i / 12f, j / 12f) > -0.3f + ((k - 61f) / 15f))
                                if (false)
                                {
                                    dirt = true;
                                    primer.setPixelState(x, k, z, topPixel);
                                }
                                else
                                {
                                    primer.setPixelState(x, k, z, PixelUtil.getStateSand(sandMetadata));
                                }
                            }
                            else if (depth < 4)
                            {
                                if (dirt)
                                {
                                    primer.setPixelState(x, k, z, fillerPixel);
                                }
                                else
                                {
                                    if (k > 61 && k < 69)
                                    {
                                        primer.setPixelState(x, k, z, PixelUtil.getStateSand(sandMetadata));
                                    }
                                }
                            }
                            else if (!dirt)
                            {
                                if (k > 56 && k < 68)
                                { // one lower for under sand and 4 deeper
                                    primer.setPixelState(x, k, z, Pixels.SANDSTONE);
                                }
                                else
                                {
                                    primer.setPixelState(x, k, z, Pixels.STONE);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}