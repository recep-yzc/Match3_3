using _Game.EnumSystem.EnumModel.Scripts;
using _Game.TileSystem.TileModel.Scripts;
using UnityEngine;
using Zenject;

namespace _Game.TileSystem.GemModel.Scripts
{
    public class GemFactory : TileFactory
    {
        [Header("References")] [SerializeField]
        private Gem gemPrefab;

        #region First

        private void Start()
        {
            _gemIdLength = EnumHelper.GetEnumLength<GemId>();
        }

        #endregion

        public override GameObject CreateTile(TileLevelData tileLevelData)
        {
            var gem = _diContainer.InstantiatePrefab(gemPrefab);

            var iTile = gem.GetComponent<ITile>();
            var iGem = gem.GetComponent<IGem>();

            iTile.SetPosition(tileLevelData.coordinate);
            iTile.SetParent(transform);
            iTile.TileId = TileId.Gem;

            var sprite = _gemDataSo.GetSpriteByGemId(tileLevelData.gemId);

            iGem.SetGemId(tileLevelData.gemId);
            iGem.SetSprite(sprite);

            return gem;
        }

        #region Private

        [Inject] private DiContainer _diContainer;
        [Inject] private GemDataSo _gemDataSo;
        private int _gemIdLength;

        #endregion
    }
}