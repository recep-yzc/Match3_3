using System.Threading;
using _Game.Core.Abilities.ScaleUpDown.Scripts;
using _Game.Core.Elements.Element.Scripts;
using Cysharp.Threading.Tasks;

namespace _Game.Core.Elements.Wood.Scripts
{
    public class Wood : ElementBase, IWood, IScaleUpDown
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

        public void SetHealth(int shield)
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