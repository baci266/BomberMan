using System.Collections.Generic;

namespace BomberMan.Client.Data
{
    public abstract class MovingElement : GameElement
    {
        public int Speed { get; set; } = 1;
        protected MovingElement(string imageName, int mapPositionX, int mapPositionY, int tileWidth, int tileHeight) : base(imageName, mapPositionX, mapPositionY, tileWidth, tileHeight)
        {
        }

        protected MovingElement(string imageName, int mapPositionX, int mapPositionY) : base(imageName, mapPositionX, mapPositionY)
        {
        }
        
        public void Move(Movement movement)
        {
            TopPosition += movement.MoveY;
            LeftPosition += movement.MoveX;
        }

        public bool CanMove(Movement movement, List<GameElement> obstacles)
        {
            TopPosition += movement.MoveY;
            LeftPosition += movement.MoveX;

            bool result = true;

            foreach (var obstacle in obstacles)
            {
                if (IntersectWith(obstacle))
                {
                    result = false;
                    break;
                }
            }
            
            TopPosition -= movement.MoveY;
            LeftPosition -= movement.MoveX;

            return result;
        }
    }
}