using UnityEngine;

namespace _Game.TileSystem.AbilityModel.Shake.Scripts
{
    public interface IShake
    {
        public void Shake(float duration, float force, AnimationCurve animationCurve);
    }
}