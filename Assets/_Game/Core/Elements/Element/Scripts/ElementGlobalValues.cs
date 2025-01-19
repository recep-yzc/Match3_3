using System.Collections.Generic;

namespace _Game.Core.Elements.Element.Scripts
{
    public abstract class ElementGlobalValues
    {
        public static readonly Dictionary<ElementId, IElementFactory> FactoryList = new();
    }
}