using _Game.TileSystem.Tile.Scripts;
using UnityEngine;

namespace _Game.TileSystem.Elements.Gem.Scripts
{
    [CreateAssetMenu(fileName = "GemDataSo", menuName = "Game/Gem/Data")]
    public class GemElementDataSo : ElementDataSo
    {
        public GemElementData data;

        public override ElementData GetElementData()
        {
            return data;
        }
    }
}