using System;
using _Game.Core.Elements.Element.Scripts;

namespace _Game.Core.Elements.None.Scripts
{
    [Serializable]
    public class NoneElementData : ElementDataBase
    {
        public NoneElementData()
        {
            elementId = ElementId.None;
        }
    }
}