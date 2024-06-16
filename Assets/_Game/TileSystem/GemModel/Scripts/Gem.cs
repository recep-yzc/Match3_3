using _Game.TileSystem.AbilityModel.Blast.Scripts;
using _Game.TileSystem.AbilityModel.Shake.Scripts;
using _Game.TileSystem.TileModel.Scripts;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Game.TileSystem.GemModel.Scripts
{
    public class Gem : Tile, IGem, IShake, IBlast
    {
        [Header("References")] [SerializeField]
        private SpriteRenderer spriteRenderer;

        [ShowInInspector] public GemId GemId { get; set; }

        public void SetSprite(Sprite sprite)
        {
            spriteRenderer.sprite = sprite;
        }

        public UniTaskVoid ShakeAsync(float duration, float force, AnimationCurve animationCurve)
        {
            return ShakeHelper.Shake(transform, duration, force, animationCurve);
        }

        public void Blast()
        {
            Debug.Log("blasted");
        }
    }
}