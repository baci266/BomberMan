using System.Drawing;

namespace BomberMan.Client.Data
{
    public abstract class GameElement
    {
        public const int BaseTileSize = 50;
        private readonly string _imageName;
        private readonly int _tileWidth;
        private readonly int _tileHeight;
        public int TopPosition { get; set; }
        public int LeftPosition { get; set; }

        public int MapPositionX => LeftPosition / BaseTileSize;
        public int MapPositionY => TopPosition / BaseTileSize;
        public string CssClass => $"{_imageName} element unselectable";
        public string CssStyle => $"top: {TopPosition}px; left: {LeftPosition}px; width: {_tileWidth}px; height: {_tileHeight}px;";
        
        public Rectangle ElementRectangle => new Rectangle(LeftPosition, TopPosition, _tileWidth, _tileHeight);

        protected GameElement(string imageName, int mapPositionX, int mapPositionY, int tileWidth, int tileHeight)
        {
            _imageName = imageName;
            TopPosition = mapPositionY * BaseTileSize;
            LeftPosition = mapPositionX * BaseTileSize;
            _tileWidth = tileWidth;
            _tileHeight = tileHeight;
        }
        
        protected GameElement(string imageName, int mapPositionX, int mapPositionY)
        {
            _imageName = imageName;
            TopPosition = mapPositionY * BaseTileSize;
            LeftPosition = mapPositionX * BaseTileSize;
            _tileWidth = _tileHeight = BaseTileSize;
        }

        public bool IntersectWith(GameElement otherElement) 
            => ElementRectangle.IntersectsWith(otherElement.ElementRectangle);

        public static bool OnTheSameTile(GameElement element1, GameElement element2)
            => element1.MapPositionX == element2.MapPositionX && element1.MapPositionY == element2.MapPositionY;
    }
}