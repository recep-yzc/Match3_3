using Cysharp.Threading.Tasks;

namespace _Game.Core.Abilities.ScaleUpDown.Scripts
{
    public interface IScaleUpDown
    {
        public UniTaskVoid ScaleUpDownAsync(ScaleUpDownDataSo scaleUpDownDataSo);
    }
}