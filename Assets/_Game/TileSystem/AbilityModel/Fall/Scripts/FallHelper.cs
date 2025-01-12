using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Game.TileSystem.AbilityModel.Fall.Scripts
{
    public static class FallHelper
    {
        public static async UniTask Handle(Transform transform, Vector3 targetPosition, FallDataSo fallDataSo,
            CancellationToken cancellationToken)
        {
            var duration = fallDataSo.duration;
            var animationCurve = fallDataSo.animationCurve;
            var lastFallOffset = fallDataSo.lastFallOffset;
            var lastFallDuration = fallDataSo.lastFallDuration;

            var startPosition = transform.position;

            await MoveToTarget(transform, startPosition, targetPosition, duration, cancellationToken);

            var startY = targetPosition.y;
            var endY = targetPosition.y - lastFallOffset;

            await YoyoMove(transform, targetPosition, startY, endY, lastFallDuration, animationCurve,
                cancellationToken);
            await YoyoMove(transform, targetPosition, endY, startY, lastFallDuration, animationCurve,
                cancellationToken);
        }

        private static async UniTask MoveToTarget(Transform transform, Vector3 startPosition, Vector3 targetPosition,
            float duration, CancellationToken cancellationToken)
        {
            var elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                var normalizedTime = elapsedTime / duration;
                var position = Vector3.Lerp(startPosition, targetPosition, normalizedTime);

                transform.position = position;
                await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);
            }
        }

        private static async UniTask YoyoMove(Transform transform, Vector3 targetPosition, float startY, float endY,
            float duration, AnimationCurve animationCurve, CancellationToken cancellationToken)
        {
            var elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                var normalizedTime = elapsedTime / duration;
                var easedTime = animationCurve.Evaluate(normalizedTime);
                var newY = Mathf.Lerp(startY, endY, easedTime);

                targetPosition.y = newY;
                transform.position = targetPosition;
                await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);
            }
        }
    }
}