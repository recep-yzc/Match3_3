using _Game.TileSystem.TileModel.Scripts;
using UnityEngine;
using Zenject;

namespace _Game.TileSystem.EmptyModel.Scripts
{
    public class EmptyFactory : TileFactory
    {
        [Header("References")] [SerializeField]
        private Empty emptyPrefab;

        #region Parameters

        [Inject] private DiContainer _diContainer;

        #endregion

        public override GameObject CreateTile(TileLevelData tileLevelData)
        {
            var empty = _diContainer.InstantiatePrefab(emptyPrefab);
            var iTile = empty.GetComponent<ITile>();

            iTile.SetPosition(tileLevelData.coordinate);
            iTile.SetParent(transform);
            iTile.SetTileId(TileId.Empty);

            return empty;
        }
    }
}