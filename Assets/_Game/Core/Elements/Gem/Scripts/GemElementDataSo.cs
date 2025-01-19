using _Game.Core.Elements.Element.Scripts;
using UnityEngine;

namespace _Game.Core.Elements.Gem.Scripts
{
    [CreateAssetMenu(fileName = "GemElementDataSo", menuName = "Game/Element/Gem/Data")]
    public class GemElementDataSo : ElementDataBaseSo
    {
        public GemElementData data;

        public override ElementDataBase GetElementDataBase()
        {
            return data;
        }
    }
}