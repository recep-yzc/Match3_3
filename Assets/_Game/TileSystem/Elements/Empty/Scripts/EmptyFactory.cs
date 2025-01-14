using System.Collections.Generic;
using System.Linq;
using _Game.TileSystem.Tile.Scripts;
using UnityEngine;
using Zenject;
using TileData = _Game.Level.Scripts.TileData;

namespace _Game.TileSystem.Elements.Empty.Scripts
{
    public class EmptyFactory : TileFactory
    {
        [Header("References")] [SerializeField]
        private EmptyElementDataSo emptyElementDataSo;

        [Header("References")] [SerializeField]
        private Empty emptyPrefab;

        protected override void Awake()
        {
            TileConstants.FactoryList.TryAdd(TileId.Empty, this);
        }

        public override GameObject Create(TileData tileData)
        {
            var empty = GetEmptyInPool();

            var iEmpty = empty.GetComponent<IEmpty>();
            var iTile = empty.GetComponent<ITile>();

            iTile.SetPosition(tileData.coordinate);
            iTile.SetParent(transform);
            iTile.SetTileId(TileId.Empty);

            var emptyElementData = emptyElementDataSo.data;

            iEmpty.SetSprite(emptyElementData.icon);

            return empty;
        }

        private GameObject GetEmptyInPool()
        {
            var empty = _createdEmptyList.FirstOrDefault(x => !x.activeInHierarchy);
            if (empty != null)
            {
                empty.SetActive(true);
                return empty;
            }

            empty = _diContainer.InstantiatePrefab(emptyPrefab);
            _createdEmptyList.Add(empty);

            return empty;
        }

        #region Parameters

        private readonly List<GameObject> _createdEmptyList = new();
        [Inject] private DiContainer _diContainer;

        #endregion
    }
}