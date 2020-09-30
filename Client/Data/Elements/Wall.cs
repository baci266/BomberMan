namespace BomberMan.Client.Data
{
    public class Wall : GameElement
    {
        private const string ImageName = "wall";
        public Wall(int mapPositionX, int mapPositionY) :
            base(ImageName, mapPositionX, mapPositionY)
        {
        }
    }
}