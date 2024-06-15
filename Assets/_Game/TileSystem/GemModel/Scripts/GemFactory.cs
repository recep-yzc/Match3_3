using _Game.TileSystem.TileModel.Scripts;
using UnityEngine;
using Zenject;

namespace _Game.TileSystem.GemModel.Scripts
{
    public class GemFactory : TileFactory
    {
        [Header("References")] [SerializeField]
        private Gem gemPrefab;

        #region Private

        [Inject] private DiContainer _diContainer;

        #endregion

        public override Tile CreateTile(Vector2 coordinate)
        {
            var iGem = _diContainer.InstantiatePrefabForComponent<Gem>(gemPrefab);
            iGem.SetPosition(coordinate);
            iGem.SetParent(transform);
            return iGem;
        }
    }
}