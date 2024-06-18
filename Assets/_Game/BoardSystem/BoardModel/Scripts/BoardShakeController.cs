using _Game.TileSystem.AbilityModel.Shake.Scripts;
using _Game.TileSystem.TileModel.Scripts;
using UnityEngine;
using Zenject;

namespace _Game.BoardSystem.BoardModel.Scripts
{
    public class BoardShakeController : MonoBehaviour
    {
        #region Private

        [Inject] private ShakeDataSo _shakeDataSo;

        #endregion

        public void TryShake(TileData tileData)
        {
            if (!tileData.Tile.TryGetComponent(out IShake shake)) return;
            shake.ShakeAsync(_shakeDataSo).Forget();
        }
    }
}