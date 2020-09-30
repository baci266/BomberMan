using System;

namespace BomberMan.Client.Data
{
    public class Movement
    {
        private int _speed;
        private int _xDirection;
        private int _yDirection;

        private Movement(int xDirection, int yDirection, int speed)
        {
            _xDirection = xDirection;
            _yDirection = yDirection;
            _speed = speed;
        }

        public int MoveX => _xDirection * _speed;
        public int MoveY => _yDirection * _speed;

        public static Movement CreateRandomMovement(int speed)
        {
            Array values = Enum.GetValues(typeof(Direction));
            int random = new Random().Next(values.Length);
            Direction randomDirection = (Direction)values.GetValue(random);

            return CreateFromDirection(randomDirection, speed);
        }

        public static Movement CreateFromDirection(Direction direction, int speed)
        {
            int xDirection = 0;
            int yDirection = 0;
            
            if (direction == Direction.Up)
            {
                yDirection = -1;
            }
            else if (direction == Direction.Down)
            {
                yDirection = 1;
            }
            else if (direction == Direction.Left)
            {
                xDirection = -1;
            }
            else if (direction == Direction.Right)
            {
                xDirection = 1;
            }
            
            return new Movement(xDirection, yDirection, speed);
        }
    }
}