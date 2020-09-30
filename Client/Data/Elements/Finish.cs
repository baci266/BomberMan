namespace BomberMan.Client.Data
{
    public class Finish : GameElement
    {
        private const string ImageName = "finish";
        
        public Finish(int mapPositionX, int mapPositionY) :
            base(ImageName, mapPositionX, mapPositionY)
        {
        }
    }
}