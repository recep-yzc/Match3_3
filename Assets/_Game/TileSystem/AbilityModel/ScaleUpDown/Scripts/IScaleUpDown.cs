using UnityEngine;

namespace _Game.TileSystem.AbilityModel.ScaleUpDown.Scripts
{
    public interface IScaleUpDown
    {
        public void ScaleUpDown(float duration, Vector3 force, AnimationCurve animationCurve);
    }
}