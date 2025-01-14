using _Game.TileSystem.Tile.Scripts;
using UnityEngine;

namespace _Game.TileSystem.Elements.Empty.Scripts
{
    [CreateAssetMenu(fileName = "EmptyDataSo", menuName = "Game/Editor/Empty/Data")]
    public class EmptyElementDataSo : ElementDataSo
    {
        public EmptyElementData data;

        public override ElementData GetElementData()
        {
            return data;
        }
    }
}