using _Game.TileSystem.AbilityModel.Shake.Scripts;
using _Game.TileSystem.TileModel.Scripts;
using UnityEngine;

namespace _Game.TileSystem.GemModel.Scripts
{
    public class Gem : Tile, IGem, IShake
    {
        [Header("References")] [SerializeField]
        private SpriteRenderer spriteRenderer;
        
        public void SetSprite(Sprite sprite)
        {
            spriteRenderer.sprite = sprite;
        }

        public void Shake(float duration, float force, AnimationCurve animationCurve)
        {
            ShakeHelper.Shake(transform, duration, force, animationCurve).Forget();
        }
    }
}