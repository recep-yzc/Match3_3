using _Game.Core.Elements.Element.Scripts;
using UnityEngine;

namespace _Game.Core.Elements.Empty.Scripts
{
    [CreateAssetMenu(fileName = "EmptyElementDataSo", menuName = "Game/Element/Empty/Data")]
    public class EmptyElementDataSo : ElementDataBaseSo
    {
        public EmptyElementData data;

        public override ElementDataBase GetElementDataBase()
        {
            return data;
        }
    }
}