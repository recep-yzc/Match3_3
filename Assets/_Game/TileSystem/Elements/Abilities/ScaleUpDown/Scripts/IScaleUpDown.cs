using Cysharp.Threading.Tasks;

namespace _Game.TileSystem.Elements.Abilities.ScaleUpDown.Scripts
{
    public interface IScaleUpDown
    {
        public UniTaskVoid ScaleUpDownAsync(ScaleUpDownDataSo scaleUpDownDataSo);
    }
}