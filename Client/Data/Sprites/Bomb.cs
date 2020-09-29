namespace BomberMan.Client.Data
{
    public class Bomb : GameElement
    {
        private const string ImageName = "bomb";

        private int _ticksToExplode = 50;

        public int ExplosionSize = 3;

        public Bomb(int mapPositionX, int mapPositionY) :
            base(ImageName, mapPositionX, mapPositionY)
        {
        }

        public bool Exploded()
        {
            _ticksToExplode--;

            return _ticksToExplode <= 0;
        }
    }
}