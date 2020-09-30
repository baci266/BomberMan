namespace BomberMan.Client.Data
{
    public class Player : MovingElement
    {
        private const string ImageName = "player";
        private const int PlayerWidth = 30;
        private const int PlayerHeight = 40;
        
        public bool isDead { get; set; }
        
        public int MapCenterPositionX => (LeftPosition + (PlayerWidth/2))/ BaseTileSize;
        
        public int MapCenterPositionY => (TopPosition + (PlayerHeight/2))/ BaseTileSize;

        public Player(int mapPositionX, int mapPositionY) :
            base(ImageName, mapPositionX, mapPositionY, PlayerWidth, PlayerHeight )
        {
            Speed = 5;
        }
    }
}