using _Game.TileSystem.Tile.Scripts;
using UnityEngine;

namespace _Game.TileSystem.Elements.None.Scripts
{
    [CreateAssetMenu(fileName = "NoneElementDataSo", menuName = "Game/None/Data")]
    public class NoneElementDataSo : ElementDataSo
    {
        public NoneElementData data;

        public override ElementData GetElementData()
        {
            return data;
        }
    }
}