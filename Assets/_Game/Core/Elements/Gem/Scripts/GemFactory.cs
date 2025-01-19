using System.Collections.Generic;
using _Game.Core.Elements.Element.Scripts;
using _Game.Level.Scripts;
using UnityEngine;
using Zenject;

namespace _Game.Core.Elements.Gem.Scripts
{
    public class GemFactory : ElementFactoryBase
    {
        [Header("References")] [SerializeField]
        private Gem gemPrefab;

        #region UnityActions

        protected override void Start()
        {
            ElementId = ElementId.Gem;
            ElementGlobalValues.FactoryList.TryAdd(ElementId, this);
        }

        #endregion

        public override GameObject Create(LevelGridData levelGridData)
        {
            var gemElementData = (GemElementData)levelGridData.elementDataBase;
            var gemGameObject = GetGameObjectInPool(ref _createdGemList, gemPrefab.gameObject);

            var iTile = gemGameObject.GetComponent<IElement>();
            var iGem = gemGameObject.GetComponent<IGem>();

            iTile.SetPosition(levelGridData.coordinate);
            iTile.SetParent(transform);
            iTile.SetElementId(ElementId);

            var gemElementDataSo = _gem.GetGemDataSo(gemElementData.gemId);

            iGem.SetGemId(gemElementDataSo.data.gemId);
            iGem.SetSprite(gemElementDataSo.data.icon);

            return gemGameObject;
        }

        #region Parameters

        private List<GameObject> _createdGemList = new();
        [Inject] private GemController _gem;

        #endregion
    }
}