namespace BomberMan.Client.Data
{
    public class Explosion : GameElement
    {
        private const string ImageName = "explosion";

        private int _activeTicks = 300;

        public Explosion(int mapPositionX, int mapPositionY) : 
            base(ImageName, mapPositionX, mapPositionY)
        {
        }
        
        public bool isActive()
        {
            _activeTicks--;

            return _activeTicks > 0;
        }
    }
}
