namespace rtg.api.world
{
    using System;

    //import net.minecraft.world.World;
    using generic.world;

    using rtg.api.util.noise;

    /**
     * @author topisani
     */
    public class RTGWorld
    {

        public readonly World world;
        public readonly OpenSimplexNoise simplex;
        public readonly CellNoise cell;
        public readonly Random rand;
        public readonly SimplexOctave.Disk surfaceJitter = new SimplexOctave.Disk();

        public RTGWorld(World world)
        {
            this.world = world;
            this.simplex = new OpenSimplexNoise(world.getSeed());
            this.cell = new SimplexCellularNoise(world.getSeed());
            this.rand = world.rand;
        }
    }
}