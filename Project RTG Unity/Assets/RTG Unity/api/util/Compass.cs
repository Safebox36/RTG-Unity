namespace rtg.api.util
{
    using System;
    using System.Collections.Generic;

    /**
     * provide integer access to compass directions, and clockwise/counterclockwise options
     * @author Zeno410
     */
    public class Compass
    {
        private Direction[] _directions = new Direction[8];

        public Compass()
        {
            _directions[0] = Direction.UP;
            _directions[1] = Direction.UP_RIGHT;
            _directions[2] = Direction.RIGHT;
            _directions[3] = Direction.DOWN_RIGHT;
            _directions[4] = Direction.DOWN;
            _directions[5] = Direction.DOWN_LEFT;
            _directions[6] = Direction.LEFT;
            _directions[7] = Direction.UP_LEFT;
        }

        public Direction direction(int index)
        {
            return _directions[index];
        }
        public int index(Direction compassDirection)
        {
            for (int i = 0; i < 8; i++)
            {
                if (_directions[i].Equals(compassDirection)) return i;
            }
            throw new Exception("nonexistent compass direction");
        }

        public Direction clockwise(Direction start)
        {
            int result = index(start) + 1;
            return _directions[result % 8];
        }

        public Direction counterClockwise(Direction start)
        {
            int result = index(start) + 7;
            return _directions[result % 8];
        }

        public Direction opposite(Direction start)
        {
            int result = index(start) + 4;
            return _directions[result % 8];
        }

        public List<Direction> directions()
        {
            List<Direction> result = new List<Direction>(8);
            for (int i = 0; i < 8; i++) result.Add(direction(i));
            return result;
        }
    }
}