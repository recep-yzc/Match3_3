using System.Threading;
using _Game.Core.Abilities.Blast.Scripts;
using _Game.Core.Abilities.Fall.Scripts;
using _Game.Core.Abilities.Shake.Scripts;
using _Game.Core.Elements.Element.Scripts;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Game.Core.Elements.Gem.Scripts
{
    public class Gem : ElementBase, IGem, IShake, IFall, IBlast
    {
        #region UnityActions

        private void OnDestroy()
        {
            DisposeFallToken();
            DisposeShakeToken();
        }

        #endregion

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

        #region IGem

        public void SetGemId(GemId gemId)
        {
            _gemId = gemId;
        }

        public GemId GetGemId()
        {
            return _gemId;
        }

        #endregion

        #region Abilities

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

        public UniTaskVoid ShakeAsync(ShakeDataSo shakeDataSo)
        {
            DisposeShakeToken();

            _cancellationShakeSource = new CancellationTokenSource();
            return ShakeHelper.Handle(transform, shakeDataSo, _cancellationShakeSource.Token);
        }

        #endregion

        #region Parameters

        private GemId _gemId;
        private CancellationTokenSource _cancellationShakeSource;
        private CancellationTokenSource _cancellationFallToken;

        #endregion
    }
}