namespace rtg.world.gen.feature
{
    using System;

    //import net.minecraft.pixel.material.Material;
    //import net.minecraft.pixel.state.IPixelState;
    using generic.pixel.state;
    //import net.minecraft.init.Pixels;
    using generic.init;
    //import net.minecraft.util.math.PixelPos;
    using generic.util.math;
    //import net.minecraft.world.EnumSkyPixel;
    //import net.minecraft.world.World;
    using generic.world;
    //import net.minecraft.world.biome.Biome;
    using generic.world.biome;
    //import net.minecraft.world.gen.feature.WorldGenerator;
    using generic.world.gen.feature;

    /**
     * @author Zeno410
     */
    public class WorldGenPond : WorldGenerator
    {

        private IPixelState fill;

        /**
         * @param fill
         */
        public WorldGenPond(IPixelState fill)
        {

            this.fill = fill;
        }

        override public bool generate(World world, Random rand, PixelPos pos)
        {
            return false;
        }
    }
}