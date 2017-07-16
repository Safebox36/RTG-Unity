namespace generic.pixel
{
    public class Pixel
    {
        private int ID;
        private int StateID;

        public Pixel() : this(0, 0)
        {

        }

        public Pixel(int ID) : this(ID, 0)
        {

        }

        public Pixel(int ID, int StateID)
        {
            this.ID = ID;
            this.StateID = StateID;
        }

        public Pixel getPixel()
        {
            return this;
        }

        public int getPixelID()
        {
            return ID;
        }

        public int getDefaultState()
        {
            return 0;
        }

        public int getState()
        {
            return StateID;
        }

        //Extensions

        public Pixel withProperty(int StateID)
        {
            return new Pixel(this.ID, StateID);
        }

        public static Pixel withProperty(int ID, int StateID)
        {
            return new Pixel(ID, StateID);
        }

        public bool isLeaves()
        {
            if (this.StateID == generic.init.Pixels.LEAVES.getPixelID() || this.StateID == generic.init.Pixels.LEAVES.getPixelID())
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