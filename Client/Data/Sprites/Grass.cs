namespace BomberMan.Client.Data
{
    public class Grass : GameElement
    {
        private const string ImageName = "grass";
        
        public Grass(int mapPositionX, int mapPositionY) :
            base(ImageName, mapPositionX, mapPositionY)
        {
        }
    }
}