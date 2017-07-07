namespace generic.block
{
    public class Block
    {
        private object ID;
        private object StateID;

        public Block() : this(0, 0)
        {

        }

        public Block(object ID) : this(ID, 0)
        {

        }

        public Block(object ID, object StateID)
        {
            this.ID = ID;
            this.StateID = StateID;
        }

        public Block getBlock()
        {
            return this;
        }

        public object getDefaultState()
        {
            return 0;
        }
        
        public object getStateFromMeta()
        {
            return StateID;
        }

        public object getStateFromMeta(object i)
        {
            //disregard of i until system is figured out.
            return StateID;
        }

        public static Block getBlockFromName(string name)
        {
            return new Block();
        }

        public static Block getBlockFromName(int ID)
        {
            return new Block();
        }
    }

    static class BlockExt
    {
        public static object withProperty(this object property, object value)
        {
            return new Block(property, value);
        }

        public static object withProperty(this object ignore, object property, object value)
        {
            return new Block(property, value);
        }

        public static bool isLeaves(this object block, generic.world.World world, generic.util.math.BlockPos pos)
        {
            if (world.getBlockState(pos).getBlock() == generic.init.Blocks.LEAVES || world.getBlockState(pos).getBlock() == generic.init.Blocks.LEAVES2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}