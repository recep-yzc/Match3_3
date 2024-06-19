using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Game.TileSystem.AbilityModel.ScaleUpDown.Scripts
{
    public static class ScaleUpDownHelper
    {
        public static async UniTaskVoid Handle(Transform transform, float duration, Vector3 force,
            AnimationCurve animationCurve)
        {
            transform.localScale = Vector3.one;

            var elapsedTime = 0f;
            var startScale = transform.localScale;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                var normalizedTime = elapsedTime / duration;
                var curveValue = animationCurve.Evaluate(normalizedTime);
                var scale = startScale + curveValue * force;

                transform.localScale = scale;
                await UniTask.Yield(PlayerLoopTiming.Update);
            }
        }
    }
}