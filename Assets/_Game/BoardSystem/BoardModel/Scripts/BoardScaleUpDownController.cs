using _Game.TileSystem.AbilityModel.ScaleUpDown.Scripts;
using _Game.TileSystem.TileModel.Scripts;
using UnityEngine;
using Zenject;

namespace _Game.BoardSystem.BoardModel.Scripts
{
    public class BoardScaleUpDownController : MonoBehaviour
    {
        #region Private

        [Inject] private ScaleUpDownDataSo _scaleUpDownDataSo;

        #endregion

        public void TryScaleUpDown(TileData tileData)
        {
            if (!tileData.Tile.TryGetComponent(out IScaleUpDown scaleUpDown)) return;
            scaleUpDown.ScaleUpDownAsync(_scaleUpDownDataSo).Forget();
        }
    }
}