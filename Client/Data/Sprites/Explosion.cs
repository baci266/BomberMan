namespace BomberMan.Client.Data
{
    public class Explosion : GameElement
    {
        private const string ImageName = "explosion";

        public Explosion(int mapPositionX, int mapPositionY) : 
            base(ImageName, mapPositionX, mapPositionY)
        {
        }
    }
}
