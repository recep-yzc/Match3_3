using _Game.Core.Abilities.Shake.Scripts;
using _Game.Core.Grid.Scripts;
using UnityEngine;
using Zenject;

namespace _Game.Core.Board.Scripts
{
    [DefaultExecutionOrder(-1)]
    public class BoardShakeController : MonoBehaviour
    {
        #region Parameters

        [Inject] private ShakeDataSo _shakeDataSo;

        #endregion

        public void TryShake(GridData gridData)
        {
            gridData.GetGridComponents<IShake>()?.ShakeAsync(_shakeDataSo).Forget();
        }
    }
}