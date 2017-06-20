namespace rtg.api.world
{

    using System;
    using UnityEngine;

    //import net.minecraft.world.World;
    using generic.world;
    using rtg.api.util.noise;
    //import rtg.api.util.noise.CellNoise;
    //import rtg.api.util.noise.OpenSimplexNoise;
    //import rtg.api.util.noise.SimplexCellularNoise;
    //import rtg.api.util.noise.SimplexOctave;

    /**
     * @author topisani
     */
    [RequireComponent(typeof(generic.init.Blocks))]
    [RequireComponent(typeof(generic.world.World))]
    public class RTGWorld : MonoBehaviour
    {

        //public read World world;
        public readonly OpenSimplexNoise simplex;
        public readonly CellNoise cell;
        public readonly System.Random rand;
        public readonly SimplexOctave surfaceJitter = new SimplexOctave(); //removed disk, might affect project

        public RTGWorld(World world)
        {
            //this.world = world;
            this.simplex = new OpenSimplexNoise(world.getSeed());
            this.cell = new SimplexCellularNoise(world.getSeed());
            this.rand = world.rand;
        }
    }
}