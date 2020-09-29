namespace BomberMan.Client.Data
{
    public class Player : MovingElement
    {
        private const string ImageName = "player";
        private const int PlayerWidth = 30;
        private const int PlayerHeight = 40;
        
        public bool isDead { get; set; }
        
        public Player(int mapPositionX, int mapPositionY) :
            base(ImageName, mapPositionX, mapPositionY, PlayerWidth, PlayerHeight )
        {
        }
    }
}