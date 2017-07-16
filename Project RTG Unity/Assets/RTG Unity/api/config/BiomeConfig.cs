namespace rtg.api.config
{
    using UnityEngine;
    public class BiomeConfig : MonoBehaviour
    {
        /*
     * GLOBAL CONFIGS
     */

        public bool ALLOW_VILLAGES = true;
        public bool ALLOW_VOLCANOES = false;
        [Range(-1, int.MaxValue)]
        public int VOLCANO_CHANCE = -1;
        public bool USE_RTG_DECORATIONS = true;
        public bool USE_RTG_SURFACES = true;
        public int SURFACE_TOP_PIXEL;
        [Range(0, 15)]
        public int SURFACE_TOP_PIXEL_META = 0;
        public int SURFACE_FILLER_PIXEL;
        [Range(0, 15)]
        public int SURFACE_FILLER_PIXEL_META = 0;
        public int SURFACE_CLIFF_STONE_PIXEL;
        [Range(0, 15)]
        public int SURFACE_CLIFF_STONE_PIXEL_META = 0;
        public int SURFACE_CLIFF_COBBLE_PIXEL;
        [Range(0, 15)]
        public int SURFACE_CLIFF_COBBLE_PIXEL_META = 0;
        [Range(-1, 40)]
        public int CAVE_DENSITY = -1;
        [Range(-1, 40)]
        public int CAVE_FREQUENCY = -1;
        [Range(-1, 100)]
        public int RAVINE_FREQUENCY = -1;
        [Range(-1, 255)]
        public int BEACH_BIOME = -1;
        [Range(-1f, 5f)]
        public float TREE_DENSITY_MULTIPLIER = -1f;

        /*
         * OPTIONAL CONFIGS
         */

        public bool ALLOW_LOGS = true;
        public int SURFACE_MIX_PIXEL;
        [Range(0,15)]
        public int SURFACE_MIX_PIXEL_META = 0;
        public int SURFACE_MIX_FILLER_PIXEL;
        [Range(0, 15)]
        public int SURFACE_MIX_FILLER_PIXEL_META = 0;
        public bool ALLOW_PALM_TREES = true;
        public bool ALLOW_CACTUS = true;
        public bool ALLOW_COBWEBS = true;
        public bool ALLOW_WHEAT = true;
        [Range(0, int.MaxValue)]
        public int WHEAT_CHANCE = 0;
        [Range(0, int.MaxValue)]
        public int WHEAT_MIN_Y = 0;
        [Range(0, int.MaxValue)]
        public int WHEAT_MAX_Y = 0;
                    
        public static string formatSlug(string s)
        {

            s = s.ToLower();
            s = s.Replace("\\+", "plus");
            s = s.Replace("\\W", "");

            return s;
        }
    }
}