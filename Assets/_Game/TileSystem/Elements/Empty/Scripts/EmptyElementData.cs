using System;
using _Game.TileSystem.Tile.Scripts;

namespace _Game.TileSystem.Elements.Empty.Scripts
{
    [Serializable]
    public class EmptyElementData : ElementData
    {
        public EmptyElementData()
        {
            tileId = TileId.Empty;
        }
    }
}