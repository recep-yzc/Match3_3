using System.Threading;
using _Game.TileSystem.AbilityModel.Blast.Scripts;
using _Game.TileSystem.AbilityModel.Fall.Scripts;
using _Game.TileSystem.AbilityModel.Shake.Scripts;
using _Game.TileSystem.TileModel.Scripts;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Game.TileSystem.GemModel.Scripts
{
    public class Gem : Tile, IGem, IShake, IFall, IBlast
    {
        private void OnDestroy()
        {
            DisposeFallToken();
            DisposeShakeToken();
        }

        public void Blast()
        {
            gameObject.SetActive(false);
        }

        public UniTask FallAsync(Vector2 target, FallDataSo fallDataSo)
        {
            DisposeFallToken();

            _cancellationFallToken = new CancellationTokenSource();
            return FallHelper.Handle(transform, target, fallDataSo, _cancellationFallToken.Token);
        }

        public void SetGemId(GemId gemId)
        {
            GemId = gemId;
        }

        public UniTaskVoid ShakeAsync(ShakeDataSo shakeDataSo)
        {
            DisposeShakeToken();

            _cancellationShakeSource = new CancellationTokenSource();
            return ShakeHelper.Handle(transform, shakeDataSo, _cancellationShakeSource.Token);
        }

        private void DisposeShakeToken()
        {
            _cancellationShakeSource?.Cancel();
            _cancellationShakeSource?.Dispose();
        }

        private void DisposeFallToken()
        {
            _cancellationFallToken?.Cancel();
            _cancellationFallToken?.Dispose();
        }

        #region Parameters

        public GemId GemId { get; set; }
        private CancellationTokenSource _cancellationShakeSource;
        private CancellationTokenSource _cancellationFallToken;

        #endregion
    }
}