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

        public override Tile CreateTile(Vector2 coordinate)
        {
            var iGem = _diContainer.InstantiatePrefabForComponent<Wood>(woodPrefab);
            iGem.SetPosition(coordinate);
            iGem.SetParent(transform);
            return iGem;
        }
    }
}