using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Game.TileSystem.AbilityModel.ScaleUpDown.Scripts
{
    public interface IScaleUpDown
    {
        public UniTaskVoid ScaleUpDownAsync(ScaleUpDownDataSo scaleUpDownDataSo);
    }
}