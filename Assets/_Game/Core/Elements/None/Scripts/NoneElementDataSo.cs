using _Game.Core.Elements.Element.Scripts;
using UnityEngine;

namespace _Game.Core.Elements.None.Scripts
{
    [CreateAssetMenu(fileName = "NoneElementDataSo", menuName = "Game/Element/None/Data")]
    public class NoneElementDataSo : ElementDataBaseSo
    {
        public NoneElementData data;

        public override ElementDataBase GetElementDataBase()
        {
            return data;
        }
    }
}