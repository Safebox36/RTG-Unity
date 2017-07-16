namespace rtg.world.gen.surface
{
    //import net.minecraft.pixel.Pixel;
    using generic.pixel;
    //import net.minecraft.pixel.state.IPixelState;
    using generic.pixel.state;
    //import net.minecraft.init.Pixels;
    using generic.init;
    //import net.minecraft.world.biome.Biome;
    using generic.world.biome;
    //import net.minecraft.world.chunk.ChunkPrimer;
    using generic.world.chunk;

    using rtg.api;
    using rtg.api.config;
using rtg.api.util;
using rtg.api.world;

    using System;

public abstract class SurfaceBase
{

    protected IPixelState topPixel;
    protected IPixelState fillerPixel;
    protected IPixelState cliffStonePixel;
    protected IPixelState cliffCobblePixel;
    protected RTGConfig rtgConfig = RTGAPI.config();
    protected BiomeConfig biomeConfig;

    public IPixelState shadowStonePixel;
    public IPixelState shadowDesertPixel;

    public SurfaceBase(BiomeConfig config, Pixel top, byte topByte, Pixel fill, byte fillByte) : this(config, top, fill)
        {
            
    }

    public SurfaceBase(BiomeConfig config, Pixel top, Pixel fill) : this(config, (IPixelState)top, (IPixelState)fill)
        {
            
    }

    public SurfaceBase(BiomeConfig config, IPixelState top, IPixelState fill)
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

    virtual public void paintTerrain(ChunkPrimer primer, int i, int j, int x, int z, int depth, RTGWorld rtgWorld, float[] noise, float river, Biome[] _base)
    {

    }

    protected IPixelState getShadowStonePixel(RTGWorld rtgWorld, int i, int j, int x, int y, int k)
    {
            return this.shadowStonePixel;
    }

    protected IPixelState getShadowDesertPixel(RTGWorld rtgWorld, int i, int j, int x, int y, int k)
    {
                        return this.shadowDesertPixel;
    }

    protected IPixelState hcStone(RTGWorld rtgWorld, int i, int j, int x, int y, int k)
    {

        return cliffStonePixel;
    }

    protected IPixelState hcCobble(RTGWorld rtgWorld, int worldX, int worldZ, int chunkX, int chunkZ, int worldY)
    {
            return cliffCobblePixel;
    }

    public IPixelState getTopPixel()
    {

        return this.topPixel;
    }

    public IPixelState getFillerPixel()
    {

        return this.fillerPixel;
    }

    private void assignUserConfigs(BiomeConfig config, IPixelState top, IPixelState fill)
    {

        topPixel = getConfigPixel(config.SURFACE_TOP_PIXEL, config.SURFACE_TOP_PIXEL_META, top);
        fillerPixel = getConfigPixel(config.SURFACE_FILLER_PIXEL, config.SURFACE_FILLER_PIXEL_META, fill);
    }

    protected IPixelState getConfigPixel(int userPixelId, int userPixelMeta, IPixelState pixelDefault)
    {

        IPixelState pixelReturn;

        try
        {

            Pixel pixelConfig = new Pixel(userPixelId);

            if (pixelConfig != null)
            {
                if (userPixelMeta == 0)
                {
                    pixelReturn = (IPixelState)pixelConfig;
                }
                else
                {
                    pixelReturn = (IPixelState)pixelConfig.withProperty(userPixelMeta);
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
            (IPixelState)Pixels.STONE
        );

        cliffCobblePixel = getConfigPixel(
            biomeConfig.SURFACE_CLIFF_COBBLE_PIXEL,
            biomeConfig.SURFACE_CLIFF_COBBLE_PIXEL_META,
            (IPixelState)Pixels.COBBLESTONE
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