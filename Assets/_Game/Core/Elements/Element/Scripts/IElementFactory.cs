using _Game.Level.Scripts;
using UnityEngine;

namespace _Game.Core.Elements.Element.Scripts
{
    public interface IElementFactory
    {
        public GameObject Create(LevelGridData levelGridData);
    }
}