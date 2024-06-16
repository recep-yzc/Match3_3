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

        public UniTaskVoid ShakeAsync(ShakeDataSo shakeDataSo)
        {
            return ShakeHelper.Shake(transform, shakeDataSo.duration, shakeDataSo.force, shakeDataSo.animationCurve);
        }

        public void Blast()
        {
            Debug.Log("blasted");
        }
    }
}