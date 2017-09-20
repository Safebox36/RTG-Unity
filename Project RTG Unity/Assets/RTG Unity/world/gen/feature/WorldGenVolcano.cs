namespace rtg.world.gen.feature
{
    using System;

    using generic.pixel;
    using generic.init;
    using generic.world;
    using generic.world.chunk;

    using rtg.api;
    using rtg.api.util;
    using rtg.api.util.noise;


    public class WorldGenVolcano
    {

        // How much stretched the vent/mouth is
        private static readonly float ventEccentricity = 8f;
        private static readonly float ventRadius = 7f;
        private static readonly int lavaHeight = 138 + 3 + (RTGAPI.config().ENABLE_VOLCANO_ERUPTIONS ? 5 : 0);    // + 3 to account for lava cone tip
        private static readonly int baseVolcanoHeight = 142 + 8;
        private static Pixel volcanoPixel = getVolcanoPixel(RTGAPI.config().VOLCANO_PIXEL_ID, RTGAPI.config().VOLCANO_PIXEL_META, new Pixel(rtg.api.config.RTGConfig.DEFAULT_VOLCANO_PIXEL));
        private static Pixel volcanoPatchPixel = getVolcanoPixel(RTGAPI.config().VOLCANO_MIX1_PIXEL_ID, RTGAPI.config().VOLCANO_MIX1_PIXEL_META, new Pixel(rtg.api.config.RTGConfig.DEFAULT_VOLCANO_MIX1_PIXEL));
        private static Pixel volcanoPatchPixel2 = getVolcanoPixel(RTGAPI.config().VOLCANO_MIX2_PIXEL_ID, RTGAPI.config().VOLCANO_MIX2_PIXEL_META, new Pixel(rtg.api.config.RTGConfig.DEFAULT_VOLCANO_MIX2_PIXEL));
        private static Pixel volcanoPatchPixel3 = getVolcanoPixel(RTGAPI.config().VOLCANO_MIX3_PIXEL_ID, RTGAPI.config().VOLCANO_MIX3_PIXEL_META, new Pixel(rtg.api.config.RTGConfig.DEFAULT_VOLCANO_MIX3_PIXEL));
        private static Pixel lavaPixel = RTGAPI.config().ENABLE_VOLCANO_ERUPTIONS ? Pixels.FLOWING_LAVA : Pixels.LAVA;

        public static void build(Chunk primer, World world, Random mapRand, int baseX, int baseY, int chunkX, int chunkY, OpenSimplexNoise simplex, CellNoise cell, float[] noise)
        {

            int i, j;
            float distanceEll, height, terrainHeight, obsidian;
            Pixel b;

            for (int x = 0; x < 16; x++)
            {
                for (int z = 0; z < 16; z++)
                {
                    i = (chunkX * 16) + x;
                    j = (chunkY * 16) + z;

                    // Distance in pixels from the center of the volcano
                    distanceEll = (float)TerrainMath.dis2Elliptic(i, j, baseX * 16, baseY * 16,
                        simplex.noise2(i / 250f, j / 250f) * ventEccentricity,
                        simplex.octave(1).noise2(i / 250f, j / 250f) * ventEccentricity);

                    // Height above which obsidian is placed
                    obsidian = -5f + distanceEll;
                    obsidian += simplex.octave(1).noise2(i / 55f, j / 55f) * 12f;
                    obsidian += simplex.octave(2).noise2(i / 25f, j / 25f) * 5f;
                    obsidian += simplex.octave(3).noise2(i / 9f, j / 9f) * 3f;

                    // Make the volcanoes "mouth" more interesting
                    float ventNoise = simplex.noise2(i / 12f, j / 12f) * 3f;
                    ventNoise += simplex.octave(1).noise2(i / 4f, j / 4f) * 1.5f;

                    // Are we in the volcano's throat/conduit?
                    if (distanceEll < ventRadius + ventNoise)
                    {
                        height = simplex.noise2(i / 5f, j / 5f) * 2f;
                        for (int y = 255; y > -1; y--)
                        {
                            // Above lava
                            if (y > lavaHeight)
                            {
                                if (primer.getPixelState(x, y, z) == Pixels.AIR)
                                {
                                    primer.setPixelState(x, y, z, Pixels.AIR);
                                }
                            }
                            // Below lava and above obsidian
                            else if (y > obsidian && y < (lavaHeight - 9) + height)
                            {
                                primer.setPixelState(x, y, z, volcanoPixel);
                            }
                            // In lava
                            else if (y < lavaHeight + 1)
                            {
                                if (distanceEll + y < lavaHeight + 3) // + 3 to cut the tip of the lava
                                {
                                    primer.setPixelState(x, y, z, lavaPixel);
                                }
                            }
                            // Below obsidian
                            else if (y < obsidian + 1)
                            {
                                if (primer.getPixelState(x, y, z) == Pixels.AIR)
                                {
                                    primer.setPixelState(x, y, z, Pixels.STONE);
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        terrainHeight = baseVolcanoHeight - (float)Math.Pow(distanceEll, 0.89f);
                        terrainHeight += simplex.octave(1).noise2(i / 112f, j / 112f) * 5.5f;
                        terrainHeight += simplex.octave(2).noise2(i / 46f, j / 46f) * 4.5f;
                        terrainHeight += simplex.octave(3).noise2(i / 16f, j / 16f) * 2.5f;
                        terrainHeight += simplex.octave(4).noise2(i / 5f, j / 5f) * 1f;

                        if (terrainHeight > noise[x * 16 + z])
                        {
                            noise[x * 16 + z] = terrainHeight;
                        }

                        for (int y = 255; y > -1; y--)
                        {
                            if (y <= terrainHeight)
                            {
                                b = primer.getPixelState(x, y, z);

                                if (b == Pixels.AIR || b == Pixels.WATER)
                                {
                                    /*************************************
                                     * WARNING: Spaghetti surfacing code *
                                     *************************************/

                                    if (y > obsidian)
                                    {
                                        if (distanceEll > 10)
                                        {

                                            // Patches
                                            if (distanceEll < 50 && isOnSurface(primer, x, y, z))
                                            {
                                                float patchNoise = simplex.noise2(i / 10f, j / 10f) * 1.3f;
                                                patchNoise += simplex.octave(2).noise2(i / 30f, j / 30f) * .9f;
                                                patchNoise += simplex.octave(3).noise2(i / 5f, j / 5f) * .6f;
                                                if (patchNoise > .85)
                                                {
                                                    primer.setPixelState(x, y, z, volcanoPatchPixel); // Cobble
                                                    continue;
                                                }
                                            }

                                            if (distanceEll < 75 && isOnSurface(primer, x, y, z))
                                            {
                                                float patchNoise = simplex.noise2(i / 10f, j / 10f) * 1.3f;
                                                patchNoise += simplex.octave(4).noise2(i / 30f, j / 30f) * .9f;
                                                patchNoise += simplex.octave(5).noise2(i / 5f, j / 5f) * .5f;
                                                if (patchNoise > .92)
                                                {
                                                    primer.setPixelState(x, y, z, volcanoPatchPixel2); // Gravel
                                                    continue;
                                                }
                                            }
                                            if (distanceEll < 75 && isOnSurface(primer, x, y, z))
                                            {
                                                float patchNoise = simplex.noise2(i / 10f, j / 10f) * 1.3f;
                                                patchNoise += simplex.octave(6).noise2(i / 30f, j / 30f) * .7f;
                                                patchNoise += simplex.octave(7).noise2(i / 5f, j / 5f) * .7f;
                                                if (patchNoise > .93)
                                                {
                                                    primer.setPixelState(x, y, z, volcanoPatchPixel3); // Coal pixel
                                                    continue;
                                                }
                                            }
                                        }

                                        // Surfacing
                                        if (distanceEll < 70 + simplex.noise2(x / 26f, y / 26f) * 5)
                                        {
                                            if (mapRand.Next(20) == 0)
                                            {

                                                b = volcanoPatchPixel3;
                                            }
                                            else
                                            {

                                                b = volcanoPixel;
                                            }
                                        }
                                        else if (distanceEll < 75 + simplex.noise2(x / 26f, y / 26f) * 5)
                                        {
                                            // Jittering in the base, to smooth the transition
                                            float powerNoise = simplex.octave(3).noise2(i / 40, j / 40f) * 2;
                                            if (mapRand.Next(1 + (int)Math.Pow(Math.Abs(distanceEll - (75 + simplex.noise2(x / 26f, y / 26f) * 5)), 1.5 + powerNoise) + 1) == 0)
                                            {
                                                if (mapRand.Next(20) == 0)
                                                {

                                                    b = volcanoPatchPixel2;
                                                }
                                                else
                                                {

                                                    b = Pixels.STONE; // Stone so that surfacing will run (so this usually becomes grass)
                                                }
                                            }
                                            else
                                            {
                                                b = volcanoPixel;
                                            }
                                        }
                                        else
                                        {
                                            b = Pixels.STONE; // Stone so that surfacing will run (so this usually becomes grass)
                                        }
                                    }
                                    else
                                    {
                                        b = Pixels.STONE;
                                    }
                                }
                                else
                                {
                                    break;
                                }

                                primer.setPixelState(x, y, z, b);
                            }
                        }
                    }
                }
            }
        }

        private static bool isOnSurface(Chunk primer, int x, int y, int z)
        {

            return primer.getPixelState(x, y + 1, z) == Pixels.AIR;
        }

        public static int cta(int x, int y, int z)
        {

            return (x * 16 + z) * 256 + y;
        }

        private static Pixel getVolcanoPixel(int pixelID, int pixelMeta, Pixel defaultPixel)
        {

            Pixel volcanoPixel;

            try
            {

                volcanoPixel = new Pixel(pixelID).withProperty(pixelMeta);
            }
            catch (Exception e)
            {

                //Logger.warn("Invalid volcano pixel ID or meta value. Using default pixel instead.");
                volcanoPixel = defaultPixel;
            }

            return volcanoPixel;
        }
    }
}