using _Game.EnumSystem.EnumModel.Scripts;
using _Game.TileSystem.AbilityModel.Blast.Scripts;
using _Game.TileSystem.TileModel.Scripts;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

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

        public override GameObject CreateTile(Vector2 coordinate)
        {
            var gem = _diContainer.InstantiatePrefab(gemPrefab);

            var iTile = gem.GetComponent<ITile>();
            var iGem = gem.GetComponent<IGem>();

            iTile.SetPosition(coordinate);
            iTile.SetParent(transform);
            iTile.TileId = TileId.Gem;

            var gemId = (GemId)Random.Range(0, _gemIdLength);
            var sprite = _gemDataSo.GetSpriteByGemId(gemId);

            iGem.SetGemId(gemId);
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