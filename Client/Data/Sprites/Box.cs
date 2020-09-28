namespace BomberMan.Client.Data
{
    public class Box : GameElement
    {
        private const string ImageName = "box";
        public Box(int mapPositionX, int mapPositionY) :
            base(ImageName, mapPositionX, mapPositionY)
        {
        }
    }
}