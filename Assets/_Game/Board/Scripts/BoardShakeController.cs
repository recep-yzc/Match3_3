using _Game.TileSystem.Elements.Abilities.Shake.Scripts;
using _Game.TileSystem.Tile.Scripts;
using UnityEngine;
using Zenject;

namespace _Game.Board.Scripts
{
    [DefaultExecutionOrder(-1)]
    public class BoardShakeController : MonoBehaviour
    {
        #region Parameters

        [Inject] private ShakeDataSo _shakeDataSo;

        #endregion

        public void TryShake(TileData tileData)
        {
            tileData.GetTileComponents<IShake>()?.ShakeAsync(_shakeDataSo).Forget();
        }
    }
}