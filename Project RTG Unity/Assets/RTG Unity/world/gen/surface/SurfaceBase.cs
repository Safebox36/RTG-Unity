namespace rtg.world.gen.surface
{
    using generic.pixel;
    using generic.init;
    using generic.world.biome;
    using generic.world.chunk;

    using rtg.api;
    using rtg.api.config;
using rtg.api.util;
using rtg.api.world;

    using System;

public abstract class SurfaceBase
{

    protected Pixel topPixel;
    protected Pixel fillerPixel;
    protected Pixel cliffStonePixel;
    protected Pixel cliffCobblePixel;
    protected RTGConfig rtgConfig = RTGAPI.config();
    protected BiomeConfig biomeConfig;

    public Pixel shadowStonePixel;
    public Pixel shadowDesertPixel;

    public SurfaceBase(BiomeConfig config, Pixel top, byte topByte, Pixel fill, byte fillByte) : this(config, top, fill)
        {
            
    }

    public SurfaceBase(BiomeConfig config, Pixel top, Pixel fill)
    {

        if (config == null)
        {
            throw new Exception("Biome config in SurfaceBase is NULL.");
        }

        biomeConfig = config;
        topPixel = top;
        fillerPixel = fill;
        this.initCliffPixels();
        this.initShadowPixels();
        this.assignUserConfigs(config, top, fill);
    }

    virtual public void paintTerrain(Chunk primer, int i, int j, int x, int z, int depth, RTGWorld rtgWorld, float[] noise, float river, Biome[] _base)
    {

    }

    protected Pixel getShadowStonePixel(RTGWorld rtgWorld, int i, int j, int x, int y, int k)
    {
            return this.shadowStonePixel;
    }

    protected Pixel getShadowDesertPixel(RTGWorld rtgWorld, int i, int j, int x, int y, int k)
    {
                        return this.shadowDesertPixel;
    }

    protected Pixel hcStone(RTGWorld rtgWorld, int i, int j, int x, int y, int k)
    {

        return cliffStonePixel;
    }

    protected Pixel hcCobble(RTGWorld rtgWorld, int worldX, int worldZ, int chunkX, int chunkZ, int worldY)
    {
            return cliffCobblePixel;
    }

    public Pixel getTopPixel()
    {

        return this.topPixel;
    }

    public Pixel getFillerPixel()
    {

        return this.fillerPixel;
    }

    private void assignUserConfigs(BiomeConfig config, Pixel top, Pixel fill)
    {

        topPixel = getConfigPixel(config.SURFACE_TOP_PIXEL, config.SURFACE_TOP_PIXEL_META, top);
        fillerPixel = getConfigPixel(config.SURFACE_FILLER_PIXEL, config.SURFACE_FILLER_PIXEL_META, fill);
    }

    protected Pixel getConfigPixel(int userPixelId, int userPixelMeta, Pixel pixelDefault)
    {

        Pixel pixelReturn;

        try
        {

            Pixel pixelConfig = new Pixel(userPixelId);

            if (pixelConfig != null)
            {
                if (userPixelMeta == 0)
                {
                    pixelReturn = pixelConfig;
                }
                else
                {
                    pixelReturn = pixelConfig.withProperty(userPixelMeta);
                }
            }
            else
            {
                pixelReturn = pixelDefault;
            }
        }
        catch (Exception e)
        {
            pixelReturn = pixelDefault;
        }

        return pixelReturn;
    }

    protected void initCliffPixels()
    {

        cliffStonePixel = getConfigPixel(
            biomeConfig.SURFACE_CLIFF_STONE_PIXEL,
            biomeConfig.SURFACE_CLIFF_STONE_PIXEL_META,
            Pixels.STONE
        );

        cliffCobblePixel = getConfigPixel(
            biomeConfig.SURFACE_CLIFF_COBBLE_PIXEL,
            biomeConfig.SURFACE_CLIFF_COBBLE_PIXEL_META,
            Pixels.COBBLESTONE
        );
    }

    protected void initShadowPixels()
    {

        shadowStonePixel = getConfigPixel(
            rtgConfig.SHADOW_STONE_PIXEL_ID,
            rtgConfig.SHADOW_STONE_PIXEL_META,
            PixelUtil.getStateClay(9)
        );

        shadowDesertPixel = getConfigPixel(
            rtgConfig.SHADOW_DESERT_PIXEL_ID,
            rtgConfig.SHADOW_DESERT_PIXEL_META,
            PixelUtil.getStateClay(0)
        );
    }
}
}