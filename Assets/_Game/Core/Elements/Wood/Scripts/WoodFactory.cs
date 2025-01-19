using System.Collections.Generic;
using _Game.Core.Elements.Element.Scripts;
using _Game.Level.Scripts;
using UnityEngine;
using Zenject;

namespace _Game.Core.Elements.Wood.Scripts
{
    public class WoodFactory : ElementFactoryBase
    {
        [Header("References")] [SerializeField]
        private Wood woodPrefab;

        #region UnityActions

        protected override void Start()
        {
            ElementId = ElementId.Wood;
            ElementGlobalValues.FactoryList.TryAdd(ElementId, this);
        }

        #endregion

        public override GameObject Create(LevelGridData levelGridData)
        {
            var wood = GetGameObjectInPool(ref _createdWoodList, woodPrefab.gameObject);

            var iWood = wood.GetComponent<IWood>();
            var iTile = wood.GetComponent<IElement>();

            iTile.SetPosition(levelGridData.coordinate);
            iTile.SetParent(transform);
            iTile.SetElementId(ElementId);

            var woodDataSo = _woodController.GetWoodDataSo();

            iWood.SetHealth(2);
            iWood.SetSprite(woodDataSo.data.icon);

            return wood;
        }

        #region Parameters

        [Inject] private WoodController _woodController;
        private List<GameObject> _createdWoodList = new();

        #endregion
    }
}