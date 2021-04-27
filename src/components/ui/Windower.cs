namespace Abyss_Call
{
    class Windower : Component
    {
        public int TileWidth { get; set; }
        public int TileHeight { get; set; }
        public int Width => TileWidth * 16 * Game.SPS;
        public int Height => TileHeight * 16 * Game.SPS;
    }
}
