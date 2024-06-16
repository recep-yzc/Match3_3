using _Game.TileSystem.TileModel.Scripts;
using UnityEngine;

namespace _Game.TileSystem.GemModel.Scripts
{
    public interface IGem : ITile
    {
        public GemId GemId { get; set; }
        public void SetSprite(Sprite sprite);
    }
}