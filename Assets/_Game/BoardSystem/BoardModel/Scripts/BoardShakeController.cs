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
            tileData.GetTileComponents<IShake>()?.ShakeAsync(_shakeDataSo).Forget();
        }
    }
}