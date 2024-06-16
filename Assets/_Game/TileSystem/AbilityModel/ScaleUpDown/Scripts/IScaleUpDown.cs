using Cysharp.Threading.Tasks;

namespace _Game.TileSystem.AbilityModel.ScaleUpDown.Scripts
{
    public interface IScaleUpDown
    {
        public UniTaskVoid ScaleUpDownAsync(ScaleUpDownDataSo scaleUpDownDataSo);
    }
}