namespace generic.util.math
{
    using UnityEngine;
    public class BlockPos : MonoBehaviour
    {
        private Vector3 blockPos;

        //constructors
        public BlockPos(double x, double y, double z)
        {
            blockPos = new Vector3((float)x, (float)y, (float)z);
        }
        public BlockPos(GameObject source)
        {
            blockPos = source.transform.position;
        }
        public BlockPos(int x, int y, int z)
        {

        }
        public BlockPos(Vector3 source)
        {
            blockPos = source;
        }
		
		//get methods
		public int getX()
		{
			return (int)blockPos.x;
		}
		
		public int getY()
		{
			return (int)blockPos.y;
		}
		
		public int getZ()
		{
			return (int)blockPos.z;
		}

        //maths
        public BlockPos add(double x, double y, double z)
        {
            blockPos += new Vector3((float)x, (float)y, (float)z);
            return this;
        }
        public BlockPos add(int x, int y, int z)
        {
            blockPos += new Vector3(x, y, z);
            return this;
        }
        public BlockPos add(Vector3 vec)
        {
            blockPos += vec;
            return this;
        }
        public Vector3 crossProduct(Vector3 vec)
        {
            blockPos = Vector3.Cross(blockPos, vec);
            return blockPos;
        }
        public BlockPos crossProductBP(Vector3 vec)
        {
            blockPos = Vector3.Cross(blockPos, vec);
            return this;
        }

        public BlockPos multiply(int factor)
        {
            blockPos *= factor;
            return this;
        }

        public BlockPos subtract(Vector3 vec)
        {
            blockPos -= vec;
            return this;
        }



        //up
        public BlockPos up()
        {
            blockPos = Vector3.up;
            return this;
        }
        public BlockPos up(int n)
        {
            blockPos = (Vector3.up * n);
            return this;
        }

        //down
        public BlockPos down()
        {
            blockPos = Vector3.down;
            return this;
        }
        public BlockPos down(int n)
        {
            blockPos = (Vector3.down * n);
            return this;
        }

        //left
        public BlockPos west()
        {
            blockPos = Vector3.left;
            return this;
        }
        public BlockPos west(int n)
        {
            blockPos = (Vector3.left * n);
            return this;
        }

        //right
        public BlockPos east()
        {
            blockPos = Vector3.right;
            return this;
        }
        public BlockPos east(int n)
        {
            blockPos = (Vector3.right * n);
            return this;
        }

        //north
        public BlockPos north()
        {
            blockPos = Vector3.forward;
            return this;
        }
        public BlockPos north(int n)
        {
            blockPos = (Vector3.forward * n);
            return this;
        }

        //south
        public BlockPos south()
        {
            blockPos = Vector3.back;
            return this;
        }
        public BlockPos south(int n)
        {
            blockPos = (Vector3.back * n);
            return this;
        }
    }
}