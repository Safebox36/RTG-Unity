/*
 * Available under the Lesser GPL License 3.0
 */

namespace rtg.world.biome
{
    //import net.minecraft.world.biome.Biome;
    using generic.world.biome;

    using rtg.world.biome.realistic;

    /**
     *
     * @author Zeno410
     */
    public interface IBiomeProviderRTG
    {
        int[] getBiomesGens(int x, int z, int par3, int par4);
        float getRiverStrength(int x, int y);
        Biome getBiomeGenAt(int par1, int par2);
        RealisticBiomeBase getBiomeDataAt(int par1, int par2);
        bool isBorderlessAt(int x, int y);
    }
}