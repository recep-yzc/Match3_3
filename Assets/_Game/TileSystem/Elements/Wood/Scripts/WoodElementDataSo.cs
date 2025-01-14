using _Game.TileSystem.Tile.Scripts;
using UnityEngine;

namespace _Game.TileSystem.Elements.Wood.Scripts
{
    [CreateAssetMenu(fileName = "WoodDataSo", menuName = "Game/Editor/Wood/Data")]
    public class WoodElementDataSo : ElementDataSo
    {
        public WoodElementData data;

        public override ElementData GetElementData()
        {
            return data;
        }
    }
}