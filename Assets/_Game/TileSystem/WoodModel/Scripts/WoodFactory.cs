using _Game.TileSystem.TileModel.Scripts;
using UnityEngine;
using Zenject;

namespace _Game.TileSystem.WoodModel.Scripts
{
    public class WoodFactory : TileFactory
    {
        [Header("References")] [SerializeField]
        private Wood woodPrefab;

        #region Private

        [Inject] private DiContainer _diContainer;

        #endregion

        public override GameObject CreateTile(Vector2 coordinate)
        {
            var wood = _diContainer.InstantiatePrefab(woodPrefab);
            var iWood = wood.GetComponent<IWood>();
            var iTile = wood.GetComponent<ITile>();

            iTile.SetPosition(coordinate);
            iTile.SetParent(transform);
            iTile.SetTileId(TileId.Wood);

            iWood.SetShield(2);

            return wood;
        }
    }
}