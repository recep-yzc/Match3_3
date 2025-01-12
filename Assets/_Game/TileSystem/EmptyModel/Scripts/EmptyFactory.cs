using System.Collections.Generic;
using System.Linq;
using _Game.TileSystem.TileModel.Scripts;
using UnityEngine;
using Zenject;

namespace _Game.TileSystem.EmptyModel.Scripts
{
    public class EmptyFactory : TileFactory
    {
        [Header("References")] [SerializeField]
        private Empty emptyPrefab;

        public override GameObject CreateTile(TileLevelData tileLevelData)
        {
            var empty = GetEmptyInPool();
            var iTile = empty.GetComponent<ITile>();

            iTile.SetPosition(tileLevelData.coordinate);
            iTile.SetParent(transform);
            iTile.SetTileId(TileId.Empty);

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