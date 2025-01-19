using System;
using _Game.Core.Elements.Element.Scripts;

namespace _Game.Core.Elements.Empty.Scripts
{
    [Serializable]
    public class EmptyElementData : ElementDataBase
    {
        public EmptyElementData()
        {
            elementId = ElementId.Empty;
        }
    }
}