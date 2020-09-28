namespace BomberMan.Client.Data
{
    public class Bomb : GameElement
    {
        private const string ImageName = "bomb";

        public Bomb(int mapPositionX, int mapPositionY) :
            base(ImageName, mapPositionX, mapPositionY)
        {
        }
    }
}