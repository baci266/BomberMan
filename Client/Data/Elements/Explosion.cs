namespace BomberMan.Client.Data
{
    public class Explosion : GameElement
    {
        private const string ImageName = "explosion";

        private int _activeTicks = GameUniverse.Fps * 3;

        public Explosion(int mapPositionX, int mapPositionY) : 
            base(ImageName, mapPositionX, mapPositionY)
        {
        }
        
        /// <summary>
        /// Substracts active ticks
        /// Call this method every tick!
        /// </summary>
        /// <returns>if explosion is still active</returns>
        public bool isActive()
        {
            _activeTicks--;

            return _activeTicks > 0;
        }
    }
}
