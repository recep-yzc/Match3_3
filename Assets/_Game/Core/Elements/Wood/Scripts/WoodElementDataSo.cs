using _Game.Core.Elements.Element.Scripts;
using UnityEngine;

namespace _Game.Core.Elements.Wood.Scripts
{
    [CreateAssetMenu(fileName = "WoodElementData", menuName = "Game/Element/Wood/Data")]
    public class WoodElementDataSo : ElementDataBaseSo
    {
        public WoodElementData data;

        public override ElementDataBase GetElementDataBase()
        {
            return data;
        }
    }
}