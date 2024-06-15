using _Game.TileSystem.TileModel.Scripts;
using UnityEngine;

namespace _Game.TileSystem.GemModel.Scripts
{
    public class GemTileFactory : TileFactory
    {
        [Header("References")] [SerializeField]
        private Gem gemPrefab;

        public override ITile CreateTile()
        {
            var iGem = Instantiate(gemPrefab);
            return iGem;
        }
    }
}