using _Game.TileSystem.AbilityModel.Shake;
using _Game.TileSystem.AbilityModel.Shake.Scripts;
using Cysharp.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;

namespace _Game.TileSystem.TileModel.Scripts
{
    public class Tile : MonoBehaviour, ITile, IShake
    {
        [Header("References")] [SerializeField]
        private SpriteRenderer spriteRenderer;

        public void SetIcon(Sprite icon)
        {
            spriteRenderer.sprite = icon;
        }

        public async UniTaskVoid Shake(float duration, float force, AnimationCurve animationCurve)
        {
            transform.rotation = quaternion.identity;

            var elapsedTime = 0f;
            var startRotationZ = transform.rotation.eulerAngles.z;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                var normalizedTime = elapsedTime / duration;
                var curveValue = animationCurve.Evaluate(normalizedTime);
                var rotationZ = startRotationZ + curveValue * force;

                var currentEulerAngles = transform.rotation.eulerAngles;
                transform.rotation = Quaternion.Euler(currentEulerAngles.x, currentEulerAngles.y, rotationZ);

                await UniTask.Yield(PlayerLoopTiming.Update);
            }
        }
    }
}