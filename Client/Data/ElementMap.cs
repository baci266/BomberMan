using System;
using System.Collections.Generic;
using System.Linq;

namespace BomberMan.Client.Data
{
    public class ElementMap
    {
        private GameElement[,] Map { get; }

        private int _width;
        private int _height;

        public ElementMap(int width, int height)
        {
            _width = width;
            _height = height;
            
            Map = new GameElement[_height, _width];
        }
        
        public void AddElement(GameElement element)
        {
            Map[element.MapPositionY, element.MapPositionX] = element;
        }

        public void RemoveElement(GameElement element)
        {
            Map[element.MapPositionY, element.MapPositionX] = new Grass(element.MapPositionX, element.MapPositionY);
        }

        public List<GameElement> GetCloseElements(GameElement element)
        {
            List<GameElement> elements = new List<GameElement>();
            int x = element.MapPositionX;
            int y = element.MapPositionY;

            for (int i = y-1; i <= y+1; i++)
            {
                for (int j = x-1; j <= x+1; j++)
                {
                    if (Map[i,j] is Grass is false) elements.Add(Map[i,j]);
                }
            }

            return elements;
        }

        public List<Explosion> CreateExplosionForBomb(Bomb bomb)
        {
            List<Explosion> explosions = new List<Explosion>();
            
            int radius = bomb.ExplosionSize;
            int x = bomb.MapPositionX;
            int y = bomb.MapPositionY;
            
            // go in 4 directions and if hit wall or box then break
            for (int i = x - 1; i > x - radius; i--)
            {
                if (CheckForWallAndBox(i, y, explosions)) break;
            }
            
            for (int i = x + 1; i < x + radius; i++)
            {
                if (CheckForWallAndBox(i, y, explosions)) break;
            }
            
            for (int i = y - 1; i > y - radius; i--)
            {
                if (CheckForWallAndBox(x, i, explosions)) break;
            }
            
            for (int i = y; i < y + radius; i++)
            {
                if (CheckForWallAndBox(x, i, explosions)) break;
            }

            return explosions;
        }
        
        /// <returns>true if hit wall or box, otherwise false</returns>
        private bool CheckForWallAndBox(int x, int y, List<Explosion> explosions)
        {
            if (Map[y, x] is Wall) return true;
            
            if (Map[y, x] is Box)
            {
                explosions.Add(new Explosion(x, y));
                RemoveElement(Map[y, x]);
                return true;
            }
            
            explosions.Add(new Explosion(x, y));
            
            return false;
        }

        /// <summary>
        /// Get all game elements for rendering
        /// </summary>
        /// <param name="excludeWallsAndBoxes"></param>
        /// <returns></returns>
        public List<GameElement> GetAllElements(bool excludeWallsAndBoxes = false)
        {
            List<GameElement> elements = new List<GameElement>();
            
            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    if (excludeWallsAndBoxes && (Map[i,j] is Wall || Map[i,j] is Box)) continue;
                    elements.Add(Map[i,j]);
                }
            }

            return elements;
        }

        /// <summary>
        /// Gets free spaces and randomly pick one for finish tile
        /// </summary>
        /// <returns>Map position x and y</returns>
        public (int x, int y) GetFinishPosition()
        {
            Random random = new Random();
            List<GameElement> gameElements = GetAllElements(true);

            GameElement randElement = gameElements[random.Next(gameElements.Count)];
            
            return (randElement.MapPositionX, randElement.MapPositionY);
        }
    }
}