using System.Threading;
using Cysharp.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;

namespace _Game.Core.Abilities.Shake.Scripts
{
    public static class ShakeHelper
    {
        public static async UniTaskVoid Handle(Transform transform, ShakeDataSo shakeDataSo,
            CancellationToken cancellationToken)
        {
            var duration = shakeDataSo.duration;
            var animationCurve = shakeDataSo.animationCurve;
            var force = shakeDataSo.force;

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
                await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);
            }
        }
    }
}