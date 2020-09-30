namespace BomberMan.Client.Data
{
    public class Bomb : GameElement
    {
        private const string ImageName = "bomb";

        private int _ticksToExplode = GameUniverse.Fps * 3;

        public int ExplosionSize = 3;
        
        public bool Remove { get => _ticksToExplode <= 0; }

        public Bomb(int mapPositionX, int mapPositionY) :
            base(ImageName, mapPositionX, mapPositionY)
        {
        }

        public void Tick()
        {
            _ticksToExplode--;
        }
    }
}