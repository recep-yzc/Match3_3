using UnityEngine;

namespace _Game.Core.Elements.Gem.Scripts
{
    public interface IGem
    {
        public void SetGemId(GemId gemId);
        public GemId GetGemId();
        public void SetSprite(Sprite sprite);
    }
}