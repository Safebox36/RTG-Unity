namespace rtg.api
{

    using rtg.api.config;

    public class RTGAPI
    {

        public static RTGConfig rtgConfig;

        /*
         * This method is currently unused, but we're leaving it here for when we start
         * supporting multiple dimensions.
         */
        public static RTGConfig config(int dimension)
        {
            return rtgConfig;
        }

        public static RTGConfig config()
        {
            return config(0);
        }
    }
}