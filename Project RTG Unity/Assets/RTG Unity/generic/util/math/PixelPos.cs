namespace generic.util.math
{
    using UnityEngine;
    public class PixelPos
    {
        private Vector3 pixelPos;

        //constructors
        public PixelPos(double x, double y, double z)
        {
            pixelPos = new Vector3((float)x, (float)y, (float)z);
        }
        public PixelPos(GameObject source)
        {
            pixelPos = source.transform.position;
        }
        public PixelPos(int x, int y, int z)
        {
            pixelPos = new Vector3(x, y, z);
        }
        public PixelPos(Vector3 source)
        {
            pixelPos = source;
        }
		
		//get methods
		public int getX()
		{
			return (int)pixelPos.x;
		}
		
		public int getY()
		{
			return (int)pixelPos.y;
		}
		
		public int getZ()
		{
			return (int)pixelPos.z;
		}

        //maths
        public PixelPos add(double x, double y, double z)
        {
            pixelPos += new Vector3((float)x, (float)y, (float)z);
            return this;
        }
        public PixelPos add(int x, int y, int z)
        {
            pixelPos += new Vector3(x, y, z);
            return this;
        }
        public PixelPos add(Vector3 vec)
        {
            pixelPos += vec;
            return this;
        }
        public Vector3 crossProduct(Vector3 vec)
        {
            pixelPos = Vector3.Cross(pixelPos, vec);
            return pixelPos;
        }
        public PixelPos crossProductBP(Vector3 vec)
        {
            pixelPos = Vector3.Cross(pixelPos, vec);
            return this;
        }

        public PixelPos multiply(int factor)
        {
            pixelPos *= factor;
            return this;
        }

        public PixelPos subtract(Vector3 vec)
        {
            pixelPos -= vec;
            return this;
        }



        //up
        public PixelPos up()
        {
            pixelPos = Vector3.up;
            return this;
        }
        public PixelPos up(int n)
        {
            pixelPos = (Vector3.up * n);
            return this;
        }

        //down
        public PixelPos down()
        {
            pixelPos = Vector3.down;
            return this;
        }
        public PixelPos down(int n)
        {
            pixelPos = (Vector3.down * n);
            return this;
        }

        //left
        public PixelPos west()
        {
            pixelPos = Vector3.left;
            return this;
        }
        public PixelPos west(int n)
        {
            pixelPos = (Vector3.left * n);
            return this;
        }

        //right
        public PixelPos east()
        {
            pixelPos = Vector3.right;
            return this;
        }
        public PixelPos east(int n)
        {
            pixelPos = (Vector3.right * n);
            return this;
        }

        //north
        public PixelPos north()
        {
            pixelPos = Vector3.forward;
            return this;
        }
        public PixelPos north(int n)
        {
            pixelPos = (Vector3.forward * n);
            return this;
        }

        //south
        public PixelPos south()
        {
            pixelPos = Vector3.back;
            return this;
        }
        public PixelPos south(int n)
        {
            pixelPos = (Vector3.back * n);
            return this;
        }
    }
}