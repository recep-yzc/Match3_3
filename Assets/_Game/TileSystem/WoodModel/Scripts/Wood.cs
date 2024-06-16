using _Game.TileSystem.AbilityModel.ScaleUpDown.Scripts;
using _Game.TileSystem.TileModel.Scripts;
using Cysharp.Threading.Tasks;

namespace _Game.TileSystem.WoodModel.Scripts
{
    public class Wood : Tile, IWood, IScaleUpDown
    {
        public void SetShield(int shieldAmount)
        {
        }

        public UniTaskVoid ScaleUpDownAsync(ScaleUpDownDataSo scaleUpDownDataSo)
        {
            return ScaleUpDownHelper.ScaleUpDown(transform, scaleUpDownDataSo.duration, scaleUpDownDataSo.force,
                scaleUpDownDataSo.animationCurve);
        }
    }
}