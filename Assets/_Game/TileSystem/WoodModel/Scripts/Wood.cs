using _Game.TileSystem.AbilityModel.ScaleUpDown.Scripts;
using _Game.TileSystem.TileModel.Scripts;
using Cysharp.Threading.Tasks;

namespace _Game.TileSystem.WoodModel.Scripts
{
    public class Wood : Tile, IWood, IScaleUpDown
    {
        #region Public

        public int Shield { get; set; }

        #endregion

        public UniTaskVoid ScaleUpDownAsync(ScaleUpDownDataSo scaleUpDownDataSo)
        {
            return ScaleUpDownHelper.Handle(transform, scaleUpDownDataSo.duration, scaleUpDownDataSo.force,
                scaleUpDownDataSo.animationCurve);
        }

        public void SetShield(int shield)
        {
            Shield = shield;
        }
    }
}