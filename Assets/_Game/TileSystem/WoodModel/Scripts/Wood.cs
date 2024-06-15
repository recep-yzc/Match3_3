using _Game.TileSystem.AbilityModel.ScaleUpDown.Scripts;
using _Game.TileSystem.TileModel.Scripts;
using UnityEngine;

namespace _Game.TileSystem.WoodModel.Scripts
{
    public class Wood : Tile, IWood, IScaleUpDown
    {
        public void SetShield(int shieldAmount)
        {
        }

        public void ScaleUpDown(float duration, Vector3 force, AnimationCurve animationCurve)
        {
            ScaleUpDownHelper.ScaleUpDown(transform, duration, force, animationCurve).Forget();
        }
    }
}