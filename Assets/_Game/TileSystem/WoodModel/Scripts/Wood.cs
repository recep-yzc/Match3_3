using System.Threading;
using _Game.TileSystem.AbilityModel.ScaleUpDown.Scripts;
using _Game.TileSystem.TileModel.Scripts;
using Cysharp.Threading.Tasks;

namespace _Game.TileSystem.WoodModel.Scripts
{
    public class Wood : Tile, IWood, IScaleUpDown
    {
        private void OnDestroy()
        {
            DisposeScaleUpDownToken();
        }

        public UniTaskVoid ScaleUpDownAsync(ScaleUpDownDataSo scaleUpDownDataSo)
        {
            DisposeScaleUpDownToken();

            _cancellationScaleUpDownToken = new CancellationTokenSource();
            return ScaleUpDownHelper.Handle(transform, scaleUpDownDataSo, _cancellationScaleUpDownToken.Token);
        }

        public void SetShield(int shield)
        {
            Shield = shield;
        }

        private void DisposeScaleUpDownToken()
        {
            _cancellationScaleUpDownToken?.Cancel();
            _cancellationScaleUpDownToken?.Dispose();
        }

        #region Parameters

        public int Shield { get; set; }
        private CancellationTokenSource _cancellationScaleUpDownToken;

        #endregion
    }
}