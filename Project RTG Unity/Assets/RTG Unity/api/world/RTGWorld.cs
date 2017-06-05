namespace rtg.api.world
{

    using System;

    //import net.minecraft.world.World;

    using rtg.api.util.noise;
    //import rtg.api.util.noise.CellNoise;
    //import rtg.api.util.noise.OpenSimplexNoise;
    //import rtg.api.util.noise.SimplexCellularNoise;
    //import rtg.api.util.noise.SimplexOctave;

    /**
     * @author topisani
     */
    public class RTGWorld
    {

        //public read World world;
    public readonly OpenSimplexNoise simplex;
    public readonly CellNoise cell;
    public readonly Random rand;
    public readonly SimplexOctave surfaceJitter = new SimplexOctave(); //removed disk, might affect project

        /*public RTGWorld(World world)
        {
            //this.world = world;
            this.simplex = new OpenSimplexNoise(world.getSeed());
            this.cell = new SimplexCellularNoise(world.getSeed());
            this.rand = world.rand;
        }*/
    }
}