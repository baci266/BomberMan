using System.Collections.Generic;

namespace BomberMan.Client.Data
{
    public class ElementMap
    {
        private GameElement[,] Map { get; }

        public ElementMap(int width, int height)
        {
            Map = new GameElement[height, width];
        }
        
        public void AddElement(GameElement element)
        {
            Map[element.MapPositionY, element.MapPositionX] = element;
        }

        public void RemoveElement(GameElement element)
        {
            Map[element.MapPositionY, element.MapPositionX] = null;
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
                    if (Map[i,j] != null) elements.Add(Map[i,j]);
                }
            }

            return elements;
        }

        public List<GameElement> GetAllElements()
        {
            List<GameElement> elements = new List<GameElement>();
            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    if (Map[i, j] != null)
                    {
                        elements.Add(Map[i,j]);
                    }
                }
            }

            return elements;
        }
    }
}