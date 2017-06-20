/*
 * Available under the Lesser GPL License 3.0
 */

namespace rtg.world.gen.terrain
{
    using rtg.api.world;

    /**
     * @author Zeno410
     */
    public abstract class HeightEffect
    {

        public abstract float added(RTGWorld rtgWorld, float x, float y);

        public HeightEffect plus(HeightEffect added)
        {

            return new SummedHeightEffects(this, added);
        }

        private class SummedHeightEffects : HeightEffect
        {

            private readonly HeightEffect one;
            private readonly HeightEffect two;

            public SummedHeightEffects(HeightEffect one, HeightEffect two)
            {

                this.one = one;
                this.two = two;
            }

            override public float added(RTGWorld rtgWorld, float x, float y)
            {

                return one.added(rtgWorld, x, y) + two.added(rtgWorld, x, y);
            }
        }
    }
}