using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Game.TileSystem.AbilityModel.Shake.Scripts
{
    public interface IShake
    {
        public UniTaskVoid ShakeAsync(float duration, float force, AnimationCurve animationCurve);
    }
}