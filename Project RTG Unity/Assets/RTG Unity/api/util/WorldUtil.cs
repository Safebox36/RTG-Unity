namespace rtg.api.util
{
    using System;

    //import net.minecraft.block.BlockDoublePlant;
    using generic.block;
    //import net.minecraft.block.material.Material;
    //import net.minecraft.block.state.IBlockState;
    using generic.block.state;
    //import net.minecraft.util.math.BlockPos;
    using generic.util.math;
    //import net.minecraft.world.World;
    using generic.world;

    public class WorldUtil
    {

        private World world;
        private bool appalachia;

        public WorldUtil(World world)
        {

            this.world = world;
            //ModPresenceTester apl = new ModPresenceTester("appalachia");
            //this.appalachia = apl.present();
        }

        /**
         * Checks a given coordinate to see if it is surrounded by a given block, usually air.
         */
        public bool isSurroundedByBlock(IBlockState checkBlock, int checkDistance, SurroundCheckType checkType, Random rand, int x, int y, int z)
        {

            switch (checkType)
            {
                case SurroundCheckType.FULL: // Checks the entire radius around the coord.

                    for (int ix = -checkDistance; ix <= checkDistance; ix++)
                    {
                        for (int iz = -checkDistance; iz <= checkDistance; iz++)
                        {

                            if (x == ix && z == iz)
                            {
                                continue;
                            }

                            if (this.world.getBlockState(new BlockPos(x + ix, y, z + iz)) != checkBlock)
                            {
                                return false;
                            }
                        }
                    }

                    break;

                case SurroundCheckType.CARDINAL: // Checks the N/E/S/W directions around the coord.

                    for (int i = checkDistance; i > 0; i--)
                    {

                        if (this.world.getBlockState(new BlockPos(x, y, z + i)) != checkBlock)
                        {
                            return false;
                        }
                        if (this.world.getBlockState(new BlockPos(x, y, z - i)) != checkBlock)
                        {
                            return false;
                        }
                        if (this.world.getBlockState(new BlockPos(x + i, y, z)) != checkBlock)
                        {
                            return false;
                        }
                        if (this.world.getBlockState(new BlockPos(x - i, y, z)) != checkBlock)
                        {
                            return false;
                        }
                    }

                    break;

                case SurroundCheckType.ORDINAL: // Checks the NE/SE/SW/NW directions around the coord.

                    for (int i = checkDistance; i > 0; i--)
                    {

                        if (this.world.getBlockState(new BlockPos(x + i, y, z + i)) != checkBlock)
                        {
                            return false;
                        }
                        if (this.world.getBlockState(new BlockPos(x + i, y, z - i)) != checkBlock)
                        {
                            return false;
                        }
                        if (this.world.getBlockState(new BlockPos(x - i, y, z + i)) != checkBlock)
                        {
                            return false;
                        }
                        if (this.world.getBlockState(new BlockPos(x - i, y, z - i)) != checkBlock)
                        {
                            return false;
                        }
                    }

                    break;

                case SurroundCheckType.UP: // Checks above coord.

                    IBlockState b;
                    for (int i = checkDistance; i > 0; i--)
                    {

                        b = this.world.getBlockState(new BlockPos(x, y + i, z));

                        if (b != checkBlock)
                        {
                            return false;
                        }
                    }

                    break;

                default:
                    break;
            }

            return true;
        }

        /**
         * Checks to see if a given block is above a given coordinate.
         * Use isSurroundedByBlock() with SurroundCheckType.UP if you want to check all blocks above.
         */
        public bool isBlockAbove(IBlockState checkBlock, int checkDistance, World world, int x, int y, int z, bool materialCheck)
        {

            //Material checkBlockMaterial = checkBlock.getMaterial();
            IBlockState blockAbove;
            //Material m;

            for (int i = 1; i <= checkDistance; i++)
            {

                blockAbove = world.getBlockState(new BlockPos(x, y + checkDistance, z));

                //if (materialCheck)
                //{
                //    m = blockAbove.getMaterial();
                //    if (m != checkBlockMaterial)
                //    {
                //        return false;
                //    }
                //}
                //else if (blockAbove != checkBlock)
                //{
                return false;
                //}
            }

            return true;
        }

        public void setDoublePlant(BlockPos lowerPos, IBlockState doublePlant, int flag)
        {
            this.world.setBlockState(lowerPos, doublePlant.withProperty(0), flag);
            //this.world.setBlockState(lowerPos.up(), doublePlant.withProperty(BlockDoublePlant.HALF, BlockDoublePlant.UPPER), flag);
        }

        public void setDoublePlant(BlockPos lowerPos, IBlockState doublePlant)
        {
            this.setDoublePlant(lowerPos, doublePlant, 2);
        }

        public bool canSnowAt(BlockPos pos, bool checkLight)
        {

            //if (!this.world.canSnowAt(pos, true))
            //{
            //    return false;
            //}

            if (this.appalachia)
            {
                BlockPos groundPos = pos.down();
                string groundBlockName = this.world.getBlockState(groundPos).getBlock().ToString();
                if (groundBlockName.Contains("fallen") && groundBlockName.Contains("leaves"))
                {
                    return false;
                }
            }

            return true;
        }

        public enum SurroundCheckType
        {
            FULL,
            CARDINAL,
            ORDINAL,
            UP
        }
    }
}