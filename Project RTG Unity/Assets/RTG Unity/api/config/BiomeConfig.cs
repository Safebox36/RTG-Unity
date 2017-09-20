namespace rtg.api.config
{
    public class BiomeConfig
    {
        /*
     * GLOBAL CONFIGS
     */

        public bool ALLOW_VILLAGES = true;
        public bool ALLOW_VOLCANOES = false;
        public int VOLCANO_CHANCE = -1;
        public bool USE_RTG_DECORATIONS = true;
        public bool USE_RTG_SURFACES = true;
        public int SURFACE_TOP_PIXEL;
        public int SURFACE_TOP_PIXEL_META = 0;
        public int SURFACE_FILLER_PIXEL;
        public int SURFACE_FILLER_PIXEL_META = 0;
        public int SURFACE_CLIFF_STONE_PIXEL;
        public int SURFACE_CLIFF_STONE_PIXEL_META = 0;
        public int SURFACE_CLIFF_COBBLE_PIXEL;
        public int SURFACE_CLIFF_COBBLE_PIXEL_META = 0;
        public int CAVE_DENSITY = -1;
        public int CAVE_FREQUENCY = -1;
        public int RAVINE_FREQUENCY = -1;
        public int BEACH_BIOME = -1;
        public float TREE_DENSITY_MULTIPLIER = -1f;

        /*
         * OPTIONAL CONFIGS
         */

        public bool ALLOW_LOGS = true;
        public int SURFACE_MIX_PIXEL;
        public int SURFACE_MIX_PIXEL_META = 0;
        public int SURFACE_MIX_FILLER_PIXEL;
        public int SURFACE_MIX_FILLER_PIXEL_META = 0;
        public bool ALLOW_PALM_TREES = true;
        public bool ALLOW_CACTUS = true;
        public bool ALLOW_COBWEBS = true;
        public bool ALLOW_WHEAT = true;
        public int WHEAT_CHANCE = 0;
        public int WHEAT_MIN_Y = 0;
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