using _Game.TileSystem.AbilityModel.ScaleUpDown.Scripts;
using _Game.TileSystem.TileModel.Scripts;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Game.TileSystem.WoodModel.Scripts
{
    public class Wood : Tile, IWood, IScaleUpDown
    {
        public void SetShield(int shieldAmount)
        {
        }

        public UniTaskVoid ScaleUpDownAsync(float duration, Vector3 force, AnimationCurve animationCurve)
        {
            return ScaleUpDownHelper.ScaleUpDown(transform, duration, force, animationCurve);
        }
    }
}