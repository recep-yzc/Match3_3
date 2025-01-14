using System;
using _Game.TileSystem.Tile.Scripts;

namespace _Game.TileSystem.Elements.None.Scripts
{
    [Serializable]
    public class NoneElementData : ElementData
    {
        public NoneElementData()
        {
            tileId = TileId.None;
        }
    }
}