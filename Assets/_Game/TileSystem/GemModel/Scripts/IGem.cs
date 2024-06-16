using UnityEngine;

namespace _Game.TileSystem.GemModel.Scripts
{
    public interface IGem
    {
        public GemId GemId { get; set; }
        public void SetGemId(GemId gemId);
        public void SetSprite(Sprite sprite);
    }
}