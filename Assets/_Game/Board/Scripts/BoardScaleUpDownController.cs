using _Game.TileSystem.Elements.Abilities.ScaleUpDown.Scripts;
using _Game.TileSystem.Tile.Scripts;
using UnityEngine;
using Zenject;

namespace _Game.Board.Scripts
{
    public class BoardScaleUpDownController : MonoBehaviour
    {
        #region Parameters

        [Inject] private ScaleUpDownDataSo _scaleUpDownDataSo;

        #endregion

        public void TryScaleUpDown(TileData tileData)
        {
            tileData.GetTileComponents<IScaleUpDown>()?.ScaleUpDownAsync(_scaleUpDownDataSo).Forget();
        }
    }
}