namespace rtg.world.gen.surface
{
    //import net.minecraft.block.Block;
    using generic.block;
    //import net.minecraft.block.state.IBlockState;
    using generic.block.state;
    //import net.minecraft.init.Blocks;
    using generic.init;
    //import net.minecraft.world.biome.Biome;
    using generic.world.biome;
    //import net.minecraft.world.chunk.ChunkPrimer;
    using generic.world.chunk;

    //import rtg.api.RTGAPI;
    using rtg.api;
    //import rtg.api.config.BiomeConfig;
    using rtg.api.config;
    //import rtg.api.config.RTGConfig;
    //import rtg.api.util.BlockUtil;
    using rtg.api.util;
    //import rtg.api.util.ModPresenceTester;
    //import rtg.api.world.RTGWorld;
    using rtg.api.world;
    //import rtg.util.UBColumnCache;

    using System;

    public abstract class SurfaceBase
{

    //private final static ModPresenceTester undergroundBiomesMod = new ModPresenceTester("undergroundbiomes");
    // Create UBColumnCache only if UB is present
    //private static UBColumnCache ubColumnCache = undergroundBiomesMod.present() ? new UBColumnCache() : null;
    protected IBlockState topBlock;
    protected IBlockState fillerBlock;
    protected IBlockState cliffStoneBlock;
    protected IBlockState cliffCobbleBlock;
    protected RTGConfig rtgConfig = RTGAPI.config();
    protected BiomeConfig biomeConfig;

    public IBlockState shadowStoneBlock;
    public IBlockState shadowDesertBlock;

    public SurfaceBase(BiomeConfig config, Block top, byte topByte, Block fill, byte fillByte) : this(config, (Block)top.getStateFromMeta(topByte), (Block)fill.getStateFromMeta(fillByte))
    {

    }

        public SurfaceBase(BiomeConfig config, Block top, Block fill) : this(config, (IBlockState)top.getDefaultState(), (IBlockState)fill.getDefaultState())
        {

        }

        public SurfaceBase(BiomeConfig config, IBlockState top, IBlockState fill)
    {

        if (config == null)
        {
            throw new Exception("Biome config in SurfaceBase is NULL.");
        }

        biomeConfig = config;
        topBlock = top;
        fillerBlock = fill;
        this.initCliffBlocks();
        this.initShadowBlocks();
        this.assignUserConfigs(config, top, fill);
    }

    public virtual void paintTerrain(ChunkPrimer primer, int i, int j, int x, int z, int depth, RTGWorld rtgWorld, float[] noise, float river, Biome[] _base)
    {

    }

    protected IBlockState getShadowStoneBlock(RTGWorld rtgWorld, int i, int j, int x, int y, int k)
    {

        //if ((undergroundBiomesMod.present()) && rtgConfig.ENABLE_UBC_STONE_SHADOWING.get())
        //{

        //    return new Block(Blocks.STONE);
        //}
        //else
        //{

            return this.shadowStoneBlock;
        //}
    }

    protected IBlockState getShadowDesertBlock(RTGWorld rtgWorld, int i, int j, int x, int y, int k)
    {

        //if ((undergroundBiomesMod.present()) && rtgConfig.ENABLE_UBC_DESERT_SHADOWING.get())
        //{

        //    return Blocks.STONE.getDefaultState();
        //}
        //else
        //{

            return this.shadowDesertBlock;
        //}
    }

    protected IBlockState hcStone(RTGWorld rtgWorld, int i, int j, int x, int y, int k)
    {

        return cliffStoneBlock;
    }

    protected IBlockState hcCobble(RTGWorld rtgWorld, int worldX, int worldZ, int chunkX, int chunkZ, int worldY)
    {

        //if ((undergroundBiomesMod.present()))
        //{

        //    return ubColumnCache.column(worldX, worldZ).cobblestone(worldY);
        //}
        //else
        //{

            return cliffCobbleBlock;
        //}
    }

    public IBlockState getTopBlock()
    {

        return this.topBlock;
    }

    public IBlockState getFillerBlock()
    {

        return this.fillerBlock;
    }

    private void assignUserConfigs(BiomeConfig config, IBlockState top, IBlockState fill)
    {

        topBlock = (IBlockState)getConfigBlock(config.SURFACE_TOP_BLOCK.get(), config.SURFACE_TOP_BLOCK_META.get(), top);
        fillerBlock = (IBlockState)getConfigBlock(config.SURFACE_FILLER_BLOCK.get(), config.SURFACE_FILLER_BLOCK_META.get(), fill);
    }

    protected IBlockState getConfigBlock(string userBlockId, int userBlockMeta, IBlockState blockDefault)
    {

            IBlockState blockReturn;

            try
            {

                Block blockConfig = Block.getBlockFromName(userBlockId);                  //no idea, figure this out

                if (blockConfig != null)
                {
                    if (userBlockMeta == 0)
                    {
                        blockReturn = (IBlockState)blockConfig.getDefaultState();
                    }
                    else
                    {
                        blockReturn = (IBlockState)blockConfig.getStateFromMeta(userBlockMeta);
                    }
                }
                else
                {
                    blockReturn = blockDefault;
                }
            }
            catch (Exception e)
            {
            blockReturn = blockDefault;
        }

        return blockReturn;
    }

    protected void initCliffBlocks()
    {

        cliffStoneBlock = getConfigBlock(
            biomeConfig.SURFACE_CLIFF_STONE_BLOCK.get(),
            biomeConfig.SURFACE_CLIFF_STONE_BLOCK_META.get(),
            (IBlockState)Blocks.STONE.getDefaultState()
        );

        cliffCobbleBlock = getConfigBlock(
            biomeConfig.SURFACE_CLIFF_COBBLE_BLOCK.get(),
            biomeConfig.SURFACE_CLIFF_COBBLE_BLOCK_META.get(),
            (IBlockState)Blocks.COBBLESTONE.getDefaultState()
        );
    }

    protected void initShadowBlocks()
    {

            shadowStoneBlock = getConfigBlock(
                rtgConfig.SHADOW_STONE_BLOCK_ID.get(),
                rtgConfig.SHADOW_STONE_BLOCK_META.get(),
                (IBlockState)Blocks.CLAY
            //BlockUtil.getStateClay(9)
        );

        shadowDesertBlock = getConfigBlock(
            rtgConfig.SHADOW_DESERT_BLOCK_ID.get(),
            rtgConfig.SHADOW_DESERT_BLOCK_META.get(),
                (IBlockState)Blocks.CLAY
            //BlockUtil.getStateClay(0)
        );
    }
}
}