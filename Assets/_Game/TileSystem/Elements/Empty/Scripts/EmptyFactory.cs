using System.Collections.Generic;
using _Game.TileSystem.Tile.Scripts;
using UnityEngine;
using Zenject;
using TileData = _Game.Level.Scripts.TileData;

namespace _Game.TileSystem.Elements.Empty.Scripts
{
    [DefaultExecutionOrder(-2)]
    public class EmptyFactory : TileFactory
    {
        [Header("References")] [SerializeField]
        private Empty emptyPrefab;

        #region UnityActions

        protected override void Start()
        {
            TileConstants.FactoryList.TryAdd(TileId.Empty, this);
        }

        #endregion

        public override GameObject Create(TileData tileData)
        {
            var emptyGameObject = GetGameObjectInPool(ref _createdEmptyList, emptyPrefab.gameObject);

            var iEmpty = emptyGameObject.GetComponent<IEmpty>();
            var iTile = emptyGameObject.GetComponent<ITile>();

            iTile.SetPosition(tileData.coordinate);
            iTile.SetParent(transform);
            iTile.SetTileId(TileId.Empty);

            var emptyElementData = _emptyController.GetEmptyElementDataSo().data;
            iEmpty.SetSprite(emptyElementData.icon);

            return emptyGameObject;
        }

        #region Parameters

        [Inject] private EmptyController _emptyController;
        private List<GameObject> _createdEmptyList = new();

        #endregion
    }
}