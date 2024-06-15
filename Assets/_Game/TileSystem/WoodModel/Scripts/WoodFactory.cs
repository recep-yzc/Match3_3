using _Game.TileSystem.TileModel.Scripts;
using UnityEngine;

namespace _Game.TileSystem.WoodModel.Scripts
{
    public class WoodTileFactory : TileFactory
    {
        [Header("References")] [SerializeField]
        private Wood woodPrefab;

        public override ITile CreateTile()
        {
            var iWood = Instantiate(woodPrefab);
            return iWood;
        }
    }
}