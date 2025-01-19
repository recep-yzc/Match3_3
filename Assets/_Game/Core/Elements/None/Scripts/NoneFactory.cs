using _Game.Core.Elements.Element.Scripts;
using _Game.Level.Scripts;
using UnityEngine;

namespace _Game.Core.Elements.None.Scripts
{
    public class NoneFactory : ElementFactoryBase
    {
        #region UnityActions

        protected override void Start()
        {
            ElementId = ElementId.None;
            ElementGlobalValues.FactoryList.TryAdd(ElementId, this);
        }

        #endregion

        public override GameObject Create(LevelGridData levelGridData)
        {
            return null;
        }
    }
}