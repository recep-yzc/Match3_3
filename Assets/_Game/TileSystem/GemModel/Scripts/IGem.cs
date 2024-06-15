using _Game.TileSystem.TileModel.Scripts;
using UnityEngine;

namespace _Game.TileSystem.GemModel.Scripts
{
    public interface IGem : ITile
    {
        public void SetSprite(Sprite sprite);
    }
}