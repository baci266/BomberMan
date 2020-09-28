namespace BomberMan.Client.Data
{
    public class Enemy : MovingElement
    {
        private const string ImageName = "enemy";
        
        public Movement Movement { get; set; }
        
        public Enemy(int mapPositionX, int mapPositionY) :
            base(ImageName, mapPositionX, mapPositionY)
        {
        }
    }
}
