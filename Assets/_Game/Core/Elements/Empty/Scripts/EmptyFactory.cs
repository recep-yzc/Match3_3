using System.Collections.Generic;
using _Game.Core.Elements.Element.Scripts;
using _Game.Level.Scripts;
using UnityEngine;
using Zenject;

namespace _Game.Core.Elements.Empty.Scripts
{
    public class EmptyFactory : ElementFactoryBase
    {
        [Header("References")] [SerializeField]
        private Empty emptyPrefab;

        #region UnityActions

        protected override void Start()
        {
            ElementId = ElementId.Empty;
            ElementGlobalValues.FactoryList.TryAdd(ElementId, this);
        }

        #endregion

        public override GameObject Create(LevelGridData levelGridData)
        {
            var emptyGameObject = GetGameObjectInPool(ref _createdEmptyList, emptyPrefab.gameObject);

            var iEmpty = emptyGameObject.GetComponent<IEmpty>();
            var iTile = emptyGameObject.GetComponent<IElement>();

            iTile.SetPosition(levelGridData.coordinate);
            iTile.SetParent(transform);
            iTile.SetElementId(ElementId);

            var emptyElementData = _empty.GetEmptyElementDataSo().data;
            iEmpty.SetSprite(emptyElementData.icon);

            return emptyGameObject;
        }

        #region Parameters

        [Inject] private EmptyController _empty;
        private List<GameObject> _createdEmptyList = new();

        #endregion
    }
}