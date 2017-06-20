namespace generic.block
{
    using UnityEngine;
    public class Block : MonoBehaviour
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
    }
}