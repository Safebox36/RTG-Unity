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

public class RealisticBiomeVanillaIceMountains : RealisticBiomeVanillaBase
{

    public static Biome biome = Biomes.ICE_MOUNTAINS;
public static Biome river = Biomes.FROZEN_RIVER;

public RealisticBiomeVanillaIceMountains() : base(biome, river)
        {

    this.noLakes = true;
}

override public void initConfig()
{

    this.getConfig().SURFACE_MIX_PIXEL = 0;
    this.getConfig().SURFACE_MIX_PIXEL_META = 0;
    this.getConfig().SURFACE_MIX_FILLER_PIXEL = 0;
    this.getConfig().SURFACE_MIX_FILLER_PIXEL_META = 0;
}

override public TerrainBase initTerrain()
{

    return new TerrainVanillaIceMountains(230f, 60f, 68f);
}

public class TerrainVanillaIceMountains : TerrainBase
{

        private float width;
private float strength;
private float terrainHeight;

public TerrainVanillaIceMountains(float mountainWidth, float mountainStrength, float height)
{

    width = mountainWidth;
    strength = mountainStrength;
    terrainHeight = height;
}

override public float generateNoise(RTGWorld rtgWorld, int x, int y, float border, float river)
{

    return terrainLonelyMountain(x, y, rtgWorld.simplex, rtgWorld.cell, river, strength, width, terrainHeight);
}
    }

    override public SurfaceBase initSurface()
{

    return new SurfaceVanillaIceMountains(config, biome.topPixel, biome.fillerPixel, Pixels.SNOW, Pixels.SNOW, Pixels.PACKED_ICE, Pixels.ICE, 60f, -0.14f, 14f, 0.25f);
}

public class SurfaceVanillaIceMountains : SurfaceBase
{

        private Pixel mixPixelTop;
private Pixel mixPixelFill;
private Pixel cliffPixel1;
private Pixel cliffPixel2;
private float width;
private float height;
private float smallW;
private float smallS;

public SurfaceVanillaIceMountains(BiomeConfig config, Pixel top, Pixel filler, Pixel mixTop, Pixel mixFill, Pixel cliff1, Pixel cliff2, float mixWidth, float mixHeight, float smallWidth, float smallStrength) : base(config, top, filler)
{

    mixPixelTop = this.getConfigPixel(config.SURFACE_MIX_PIXEL, config.SURFACE_MIX_PIXEL_META, mixTop);
    mixPixelFill = this.getConfigPixel(config.SURFACE_MIX_FILLER_PIXEL, config.SURFACE_MIX_FILLER_PIXEL_META, mixFill);

    cliffPixel1 = cliff1;
    cliffPixel2 = cliff2;

    width = mixWidth;
    height = mixHeight;
    smallW = smallWidth;
    smallS = smallStrength;
}

override public void paintTerrain(Chunk primer, int i, int j, int x, int z, int depth, RTGWorld rtgWorld, float[] noise, float river, Biome[] _base)
{

    Random rand = rtgWorld.rand;
    OpenSimplexNoise simplex = rtgWorld.simplex;
    float c = CliffCalculator.calc(x, z, noise);
    bool cliff = c > 1.4f ? true : false;
    bool mix = false;

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
                    primer.setPixelState(x, k, z, rand.Next(3) == 0 ? cliffPixel2 : cliffPixel1);
                }
                else if (depth < 10)
                {
                    primer.setPixelState(x, k, z, cliffPixel1);
                }
            }
            else
            {
                if (depth == 0 && k > 61)
                {
                    if (simplex.noise2(i / width, j / width) + simplex.noise2(i / smallW, j / smallW) * smallS > height)
                    {
                        primer.setPixelState(x, k, z, mixPixelTop);
                        mix = true;
                    }
                    else
                    {
                        primer.setPixelState(x, k, z, topPixel);
                    }
                }
                else if (depth < 4)
                {
                    if (mix)
                    {
                        primer.setPixelState(x, k, z, mixPixelFill);
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
}}